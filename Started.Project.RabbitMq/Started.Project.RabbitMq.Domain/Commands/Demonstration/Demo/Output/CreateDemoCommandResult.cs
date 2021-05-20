using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Output
{
    public class CreateDemoCommandResult : ICommandResult
    {
        //Parametros de saída
        public int CodDemo { get; set; }
    }
}