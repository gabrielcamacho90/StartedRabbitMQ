using Started.Project.RabbitMq.Domain.Commands.Interfaces;
using System;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input
{
    public class UpdateDemoCommand : ICommand
    {
        //Parametros de entrada
        public int CodDemo { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public DateTime Register { get; private set; }
        public DateTime Validate { get; private set; }

        public bool isInvalid()
        {
            if (CodDemo > 0)
                return false;

            return true;
        }

        public UpdateDemoCommand GetExample()
        {
            return new UpdateDemoCommand()
            {
                CodDemo = 10,
                Name = "Demo Name",
                Active = true,
                Register = DateTime.Parse("2020-01-31 00:00:00"),
                Validate = DateTime.Parse("2020-01-31 00:00:00")
            };
        }
    }
}