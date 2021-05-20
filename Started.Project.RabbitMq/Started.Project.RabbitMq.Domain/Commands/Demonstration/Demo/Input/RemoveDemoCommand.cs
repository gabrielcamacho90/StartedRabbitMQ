using Started.Project.RabbitMq.Domain.Commands.Interfaces;

namespace Started.Project.RabbitMq.Domain.Commands.Demonstration.Demo.Input
{
    public class RemoveDemoCommand : ICommand
    {
        //Parametros de entrada
        public int CodDemo { get; set; }

        /// <summary>
        /// Valida se informações do command são validas
        /// </summary>
        /// <returns></returns>
        public bool IsInvalid()
        {

            if (CodDemo > 0)
                return false;

            return true;

        }
    }
}