using System;

namespace Started.Project.RabbitMq.Domain.Queries.Demonstration
{
    public class GetDemoQueryResult
    {
        public int CodDemo { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Register { get; set; }
    }
}