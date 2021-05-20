using Started.Project.RabbitMq.Domain.Commands.Interfaces;
using System;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input
{
    public class CreateDemoCommand : ICommand
    {
        //Parametros de input
        public string Name { get; private set; }
        public DateTime Register { get; private set; }

        public bool IsInvalid()
        {
            return string.IsNullOrEmpty(Name);
        }

        public CreateDemoCommand GetExample()
        {
            return new CreateDemoCommand
            {
                Name = "Demo Name",
                Register = DateTime.Parse("2020-01-31 00:00:00")
            };
        }
    }
}