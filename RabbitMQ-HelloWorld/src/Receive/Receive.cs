﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive
{
    public class Receive
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "helloWorld",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received Message {0}", message);
                    };
                    channel.BasicConsume(queue: "helloWorld", noAck: true, consumer: consumer);

                    Console.WriteLine("Press enter to exit");

                    Console.ReadLine();
                }
            }
        }
    }
}
