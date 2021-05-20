using RabbitMQ.Client;
using System;
using System.Text;

namespace Started.RabbitMq.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = Protocols.DefaultProtocol.DefaultPort,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                RequestedHeartbeat = TimeSpan.FromSeconds(60)
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("TesteExchange", ExchangeType.Topic, false);

                    channel.QueueDeclare("TesteQueue", true, false, false, null);

                    channel.QueueBind("TesteQueue", "TesteExchange", "TesteBind");

                    var message = "Olá Teste RabbitMQ - DOIS TRES";
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish("TesteExchange", "TesteBind", properties, body);

                    Console.WriteLine($" [x] sent {message}");
                }

                Console.WriteLine("Pressione [Enter] para sair!");
                Console.ReadLine();
            }
        }
    }
}
