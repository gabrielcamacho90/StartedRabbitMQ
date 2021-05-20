using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Output
{
    public class RemoveDemoCommandResult : ICommandResult
    {
        //Parametros de sa�da
        public bool Success { get; set; }
    }
}