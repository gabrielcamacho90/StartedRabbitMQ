using Started.Project.RabbitMq.Domain.Entities.Demonstration;
using Started.Project.RabbitMq.Domain.Queries.Demonstration;
using System.Collections.Generic;

namespace Started.Project.RabbitMq.Domain.Repositories.Demonstration
{
    public interface IDemoRepository
    {
        int Insert(Demo demo);

        bool Delete(int codDemo);

        IEnumerable<ListDemoQueryResult> ListDemoQueryResult();

        GetDemoQueryResult GetDemoQueryResult(int codDemo);
    }
}