using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input;
using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Example
{
    public class GetRemoveDemoCommandExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new RemoveDemoCommand() { CodDemo = 10 };
        }
    }
}
