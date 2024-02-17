using Business.Abstract;
using Entities.Concrete;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PaperlessWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMQService _consumer;

        public Worker(ILogger<Worker> logger, IRabbitMQService consumer)
        {
            _logger = logger;
            _consumer = consumer;
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
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var document = JsonConvert.DeserializeObject<Document>(message);
                Console.WriteLine($"Document message received: {document.Title}");
            };
            //read the message
            channel.BasicConsume(queue: "document", autoAck: true, consumer: consumer);
            Console.WriteLine("Hello\n\n\n\n\n\n\n");
            Console.ReadKey();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _consumer.GetEvent();
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}