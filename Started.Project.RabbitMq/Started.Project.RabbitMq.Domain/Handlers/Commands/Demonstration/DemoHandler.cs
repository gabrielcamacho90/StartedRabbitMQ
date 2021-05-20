using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input;
using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Output;
using Started.Project.RabbitMq.Domain.Commands.Interfaces;
using Started.Project.RabbitMq.Domain.Entities.Demonstration;
using Started.Project.RabbitMq.Domain.Handlers.Interfaces;
using Started.Project.RabbitMq.Domain.Repositories.Demonstration;


namespace Started.Project.RabbitMq.Domain.Handler.Commands.Demonstration
{
    public class DemoHandler :
                    ICommandHandler<CreateDemoCommand>,
                    ICommandHandler<RemoveDemoCommand>,
                    ICommandHandler<UpdateDemoCommand>
    {
        private readonly IDemoRepository _repository;

        public DemoHandler(IDemoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Método resposável por inserir
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommandResult Handle(CreateDemoCommand command)
        {
            //1 - Validar parametros passados no command
            if (command.IsInvalid()) return null;

            //2 - Criar a entidade
            var demo = new Demo(command.Name);

            //3 - Adicionar entidade
            var codDemo = _repository.Insert(demo);

            //4 - Praparar resposta
            return new CreateDemoCommandResult() { CodDemo = codDemo };
        }

        /// <summary>
        /// Método resposável por remover 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommandResult Handle(RemoveDemoCommand command)
        {
            //1 - Validar parámetros passados no command
            if (command.IsInvalid()) return null;

            //2 - Remove demo
            var sucesso = _repository.Delete(command.CodDemo);

            //3 - Praparar resposta
            return new RemoveDemoCommandResult() { Success = sucesso };
        }

        /// <summary>
        /// Método resposável por atualizar
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommandResult Handle(UpdateDemoCommand command)
        {
            return new UpdateDemoCommandResult() { Success = true };
        }
    }
}