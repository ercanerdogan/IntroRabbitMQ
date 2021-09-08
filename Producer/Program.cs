using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            
            using(var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "sample",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                string message = "This is sample message";

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange:"",
                    routingKey:"sample",
                    basicProperties:null,
                    body:body
                );

                Console.WriteLine($"[x] Sent a message : {message}");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
                
            


        }
    }
}
