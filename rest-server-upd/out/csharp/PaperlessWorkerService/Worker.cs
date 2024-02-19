using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Tesseract;

namespace PaperlessWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMQService _consumer;
        private readonly IFileOperation _fileOperation;
        private readonly IDocumentService _documentService;
        private readonly IElasticSearchService _elasticSearchService;

        public const string folderName = "images/";
        public const string trainedDataFolderName = "tessdata";

        public Worker(ILogger<Worker> logger, IRabbitMQService consumer, IFileOperation fileOperation, IDocumentService documentService, IElasticSearchService elasticSearchService)
        {
            _logger = logger;
            _consumer = consumer;
            _fileOperation = fileOperation;
            _documentService = documentService;
            _elasticSearchService = elasticSearchService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "paperless",
                Password = "paperless"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("document", exclusive: false);
            //Set Event object which listen message from chanel which is sent by producer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var documentKey = JsonConvert.DeserializeObject<string>(message);
                var stream = await _fileOperation.GetFile(documentKey);


                if (stream.Length > 0)
                {
                    using (var fileStream = new FileStream(folderName + documentKey, FileMode.Create))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                string tessPath = Path.Combine(trainedDataFolderName, "");
                string result = "";

                using (var engine = new TesseractEngine(tessPath, "DEU", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(folderName + documentKey))
                    {

                        var page = engine.Process(img);
                        result = page.GetText();
                        Console.WriteLine(result);
                    }
                }
                Console.WriteLine(result);

                var document = _documentService.GetByKey(documentKey);
                document.OcrResult = result;
                _documentService.Update(document);

                var elasticDocument = new ElasticDocument() { Content = result, Key = documentKey };
                _elasticSearchService.PutDocument(elasticDocument);
            };
            //read the message
            channel.BasicConsume(queue: "document", autoAck: true, consumer: consumer);

            Console.ReadKey();

        }
        public static byte[] ReadStream(Stream responseStream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}