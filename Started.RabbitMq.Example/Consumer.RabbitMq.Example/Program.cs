using Consumer.RabbitMq.Example.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using System;
using System.Text;
using System.Text.Json;

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
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                RequestedHeartbeat = TimeSpan.FromSeconds(60)
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var instanceInfo = JsonSerializer.Deserialize<InstanceInfo>(message);

                        var client = new RestClient("https://localhost:44369/v1/InstanceFile/SaveDocuments");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", "{\n\t\"InstanceId\": " + instanceInfo.InstanceId + ",\n\t\"PathFile\": \"https://devprojects.orquestraecm.com.br/stable/download/manual.pdf\"\n}", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        Console.WriteLine($"Instância Processada: {instanceInfo.InstanceId} | Arquivo Anexado: {instanceInfo.PathFile} | Status: {response.Content}");
                    };

                    channel.BasicConsume("QueueFileInstance", true, consumer);

                    Console.WriteLine("Pressione [Enter] para sair!");
                    Console.ReadLine();
                    channel.Close();
                }

                connection.Close();
            }
        }
    }
}
