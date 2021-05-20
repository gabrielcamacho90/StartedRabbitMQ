using Started.Project.RabbitMq.Domain.Entities;
using System.Threading.Tasks;

namespace Started.Project.RabbitMq.Domain.Interface
{
    public interface IDocumentService
    {
        Task<ResponseDefaultApi> SaveDocumentsToInstance(int codFlowExecute, string pathFile);
    }
}
