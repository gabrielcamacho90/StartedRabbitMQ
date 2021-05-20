using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Handlers.Interfaces
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}