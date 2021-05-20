using Microsoft.AspNetCore.Mvc;
using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Example;
using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input;
using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Output;
using Started.Project.RabbitMq.Domain.Commands.Interfaces;
using Started.Project.RabbitMq.Domain.Handler.Commands.Demonstration;
using Started.Project.RabbitMq.Domain.Queries.Demonstration;
using Started.Project.RabbitMq.Domain.Repositories.Demonstration;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net;

namespace Started.Project.RabbitMq.Api.Controllers.Demonstration
{
    public class DemoController : BaseController
    {

        private readonly IDemoRepository _repository;

        private readonly DemoHandler _handler;

        public DemoController(IDemoRepository repository, DemoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }


        /// <summary>
        /// Busca pelo código
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(GetDemoQueryResult))]
        [HttpGet]
        [Route("V1/Demo/{id}")]
        public GetDemoQueryResult Get(int id)
        {
            return _repository.GetDemoQueryResult(id);
        }

        /// <summary>
        /// Pega todos
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(List<ListDemoQueryResult>))]
        [HttpGet]
        [Route("V1/Demo")]
        public IEnumerable<ListDemoQueryResult> Get()
        {
            return _repository.ListDemoQueryResult();
        }

        /// <summary>
        /// Cria uma nova entidade
        /// </summary>
        /// <param name="command">todo: describe command parameter on Create</param>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(CreateDemoCommand), typeof(GetCreateDemoCommandExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(CreateDemoCommandResult))]
        [HttpPost]
        [Route("V1/Demo")]
        public ICommandResult Create([FromBody] CreateDemoCommand command)
        {
            return _handler.Handle(command);
        }

        /// <summary>
        /// Remove entidade
        /// </summary>
        /// <param name="command">todo: describe command parameter on Remove</param>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(RemoveDemoCommand), typeof(GetRemoveDemoCommandExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(RemoveDemoCommandResult))]
        [HttpDelete]
        [Route("V1/Demo")]
        public ICommandResult Remove([FromBody] RemoveDemoCommand command)
        {
            return _handler.Handle(command);
        }

        /// <summary>
        /// Atualiza entidade
        /// </summary>
        /// <param name="command">todo: describe command parameter on Update</param>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(UpdateDemoCommand), typeof(GetUpdateDemoCommandExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(UpdateDemoCommandResult))]
        [HttpPut]
        [Route("V1/Demo")]
        public ICommandResult Update(UpdateDemoCommand command)
        {
            return _handler.Handle(command);
        }
    }
}