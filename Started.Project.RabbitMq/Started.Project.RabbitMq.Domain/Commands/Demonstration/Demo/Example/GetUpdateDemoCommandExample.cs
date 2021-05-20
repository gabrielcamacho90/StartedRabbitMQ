using Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input;
using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Example
{
    public class GetUpdateDemoCommandExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UpdateDemoCommand().GetExample();
        }
    }
}
