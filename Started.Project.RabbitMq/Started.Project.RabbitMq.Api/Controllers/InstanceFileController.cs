using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Started.Project.RabbitMq.Domain.Entities;
using Started.Project.RabbitMq.Domain.Interface;
using Started.Project.RabbitMq.Domain.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Started.Project.RabbitMq.Api.Controllers
{
    public class InstanceFileController : BaseController
    {
        private ILogger<InstanceInfo> _logger;
        private readonly IDocumentService _documentService;


        public InstanceFileController(ILogger<InstanceInfo> logger, IDocumentService documentService)
        {
            _logger = logger;
            _documentService = documentService;
        }

        /// <summary>
        /// Busca arquivo publicado
        /// </summary>
        /// <returns></returns>        
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(string))]
        [HttpGet]
        [Route("v1/InstanceFile")]
        public string Get()
        {
            return "https://devprojects.orquestrabpm.com.br/Default/attachments/107/Teste_79372542021513.png";
        }

        /// <summary>
        /// Envia arquivo publicado
        /// </summary>
        /// <returns></returns>        
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(InstanceInfo))]
        [HttpPost]
        [Route("v1/InstanceFile/SetPublisher")]
        public IActionResult SetPublisherRabbitMq([FromBody] InstanceInfo instanceInfo)
        {
            try
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
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "QueueFileInstance",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonSerializer.Serialize(instanceInfo);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "QueueFileInstance",
                                         basicProperties: null,
                                         body: body);
                }

                return Accepted(instanceInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar enviar para fila", ex);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Salvar documentos no ECM, a partir de Instância
        /// </summary>
        /// <param name="dataRequest"></param>
        /// <returns></returns>
        [Route("v1/InstanceFile/SaveDocuments")]
        [HttpPost]
        public async Task<IActionResult> SaveDocuments([FromBody] InstanceInfo instanceInfo)
        {
            var result = await _documentService.SaveDocumentsToInstance(instanceInfo.InstanceId, instanceInfo.PathFile);
            return Ok(result);
        }

    }
}
