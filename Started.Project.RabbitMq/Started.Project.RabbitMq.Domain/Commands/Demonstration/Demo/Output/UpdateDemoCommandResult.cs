using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Output
{
    public class UpdateDemoCommandResult : ICommandResult
    {
        //Parametros de saída
        public bool Success { get; set; }
    }
}