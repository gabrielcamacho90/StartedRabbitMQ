using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input;
using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Example
{
    public class GetCreateDemoCommandExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CreateDemoCommand().GetExample();
        }
    }
}
