using RabbitMQ.Client;
using System;
using System.Text;

namespace Send
{
    public class Send
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "helloWorld",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                    string message = "Hello World!";

                    var messageBody = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "helloWorld",
                                         basicProperties: null,
                                         body: messageBody);

                    Console.WriteLine(string.Format("Sending Message {0}", message));

                    Console.WriteLine("Press enter to exit");

                    Console.ReadLine();
                }
            }
        }
    }
}
