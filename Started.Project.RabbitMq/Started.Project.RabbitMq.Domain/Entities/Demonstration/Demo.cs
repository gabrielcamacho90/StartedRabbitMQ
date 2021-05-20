using System;
namespace Started.Project.RabbitMq.Domain.Entities.Demonstration
{
    public class Demo : Entity
    {
        #region Constructors
        public Demo(string name)
        {
            Name = name;
            Active = true;
            Register = DateTime.Now;

            RegisterValidate();
        }

        #endregion Constructors

        #region Properties

        public int CodDemo { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public DateTime Register { get; private set; }
        public DateTime Validate { get; private set; }

        #endregion Properties

        #region Methods
        public void RegisterValidate()
        {
            if (Validate == null)
                Validate = Register.AddMonths(3);
        }

        #endregion
    }
}