using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer.RabbitMq.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = Protocols.DefaultProtocol.DefaultPort,
                UserName = "admin",
                Password = "admin",
                VirtualHost = "/",
                RequestedHeartbeat = TimeSpan.FromSeconds(60)
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Aguardando Logs");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [x] Recebido: {message}");
                    };

                    channel.BasicConsume("TesteQueue", true, consumer);

                    Console.WriteLine("Pressione [Enter] para sair!");
                    Console.ReadLine();
                    channel.Close();
                }

                connection.Close();
            }
        }
    }
}
