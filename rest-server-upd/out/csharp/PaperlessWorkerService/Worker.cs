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

        public const string folderName = "images/";
        public const string trainedDataFolderName = "tessdata";

        public Worker(ILogger<Worker> logger, IRabbitMQService consumer, IFileOperation fileOperation)
        {
            _logger = logger;
            _consumer = consumer;
            _fileOperation = fileOperation;
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
                var documentId = JsonConvert.DeserializeObject<string>(message);
                var file = await _fileOperation.GetFile(documentId);

                string name = documentId;
                var image = file.ResponseStream;

                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream(folderName + documentId, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                }

                string tessPath = Path.Combine(trainedDataFolderName, "");
                string result = "";

                using (var engine = new TesseractEngine(tessPath, "DEU", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(folderName + name))
                    {

                        var page = engine.Process(img);
                        result = page.GetText();
                        Console.WriteLine(result);
                    }
                }
                Console.WriteLine(result);

                Console.WriteLine($"Document message received: {file}");
            };
            //read the message
            channel.BasicConsume(queue: "document", autoAck: true, consumer: consumer);

            Console.ReadKey();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _consumer.GetEvent();
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        public void OCR(IFormFile fileData)
        {

            string name = fileData.FileName;
            var image = fileData;

            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(folderName + image.FileName, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            string tessPath = Path.Combine(trainedDataFolderName, "");
            string result = "";

            using (var engine = new TesseractEngine(tessPath, "DEU", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(folderName + name))
                {

                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }
            Console.WriteLine(result);

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