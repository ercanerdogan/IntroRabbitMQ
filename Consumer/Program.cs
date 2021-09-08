using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "sample",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[x] Received a message : {message}");

                };

                channel.BasicConsume(
                    queue: "sample",
                    autoAck : true,
                    consumer : consumer);

                Console.WriteLine(" Press any key to exit.");
                Console.ReadKey();

            }
        }
    }
}
