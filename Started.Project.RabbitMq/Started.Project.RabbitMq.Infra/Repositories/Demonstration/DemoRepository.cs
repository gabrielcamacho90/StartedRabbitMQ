using Dapper;
using Started.Project.RabbitMq.Domain.Entities.Demonstration;
using Started.Project.RabbitMq.Domain.Queries.Demonstration;
using Started.Project.RabbitMq.Domain.Repositories.Demonstration;
using Started.Project.RabbitMq.Infra.DataContexts;
using Started.Project.RabbitMq.Infra.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Started.Project.RabbitMq.Infra.Respositories.Demonstration
{
    public class DemoRepository : Repository, IDemoRepository
    {
        private readonly DataContext _context;

        public DemoRepository(DataContext context)
        {
            _context = context;
        }

        public int Insert(Demo demo)
        {
            return _context
                    .Connection
                    .Query<int>(GetFileQuery(), demo)
                    .Single();
        }

        public bool Delete(int codDemo)
        {
            _context
                .Connection
                .Query<int>(GetFileQuery(), new { @CodDemo = codDemo });

            return true;
        }

        public IEnumerable<ListDemoQueryResult> ListDemoQueryResult()
        {
            return _context
                        .Connection
                        .Query<ListDemoQueryResult>(
                            GetFileQuery()
                        );
        }

        public GetDemoQueryResult GetDemoQueryResult(int codDemo)
        {
            return _context
                        .Connection
                        .Query<GetDemoQueryResult>(
                            GetFileQuery(),
                            new { CodDemo = codDemo }
                        ).FirstOrDefault();
        }
    }
}