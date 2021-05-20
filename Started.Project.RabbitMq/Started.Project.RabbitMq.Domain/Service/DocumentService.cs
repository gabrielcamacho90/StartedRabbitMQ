using Microsoft.Extensions.Configuration;
using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Application.Service;
using Started.Project.RabbitMq.Domain.Entities;
using Started.Project.RabbitMq.Domain.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Started.Project.RabbitMq.Domain.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration _configuration;
        private ISilentLoginApplication _silentLogiApplication;
        private IDocumentControl _documentControl;
        private IUploadApplication _uploadApplication;

        public DocumentService(IConfiguration configuration)
        {
            _configuration = configuration;

            _silentLogiApplication = new SilentLoginApplication
           (
               _configuration["ECM:Url"],
               _configuration["ECM:AppCode"],
               _configuration["ECM:Token"],
               _configuration["ECM:Identification"],
               _configuration["ECM:AdhocUser"]
           );
            _uploadApplication = new UploadApplication
                (
                    _configuration["ECM:AppCode"],
                    _configuration["ECM:Token"],
                    _configuration["ECM:Identification"],
                    _configuration["ECM:Url"],
                    _configuration["ECM:AdhocUser"]
                );

            _documentControl = new DocumentControl(_silentLogiApplication, _uploadApplication);
        }

        public async Task<ResponseDefaultApi> SaveDocumentsToInstance(int codFlowExecute, string pathFile)
        {
            List<long> idsCreated = new List<long>();
            long indId = 0;

            try
            {
                indId = await UploadDocuments(new InstanceAttachments { CodFlowExecute = codFlowExecute, DsFilePath = pathFile, DocType = "Geral" });
                idsCreated.Add(indId);

                return new ResponseDefaultApi { Status = "S", Message = "OK" };
            }
            catch (Exception ex)
            {
                // Deleta arquivos se foram criados
                if (!idsCreated.Count.Equals(0))
                {
                    foreach (long docToDelete in idsCreated)
                        _silentLogiApplication.Delete(docToDelete);
                }

                return new ResponseDefaultApi { Status = "N", Message = ex.Message };
            }
        }

        private async Task<long> UploadDocuments(InstanceAttachments file)
        {
            var base4File = string.Empty;

            if (!string.IsNullOrEmpty(file.DsFilePath))
            {
                string urlFile = file.DsFilePath;
                file.DsFileName = Path.GetFileName(urlFile);

                // Indexar imagens com respectivo Path ou base64
                using (var handler = new HttpClientHandler())
                using (var client = new HttpClient(handler))
                {
                    var bytes = await client.GetByteArrayAsync(file.DsFilePath);
                    base4File = Convert.ToBase64String(bytes);
                }
            }
            else
                throw new Exception($"Documento não encontrado para a instância: {file.CodFlowExecute}.");

            // Enviar para fila que deve ser processado pela TCG
            Dictionary<string, string> recoveredPages = new Dictionary<string, string>();
            recoveredPages.Add(file.DsFileName, base4File);

            return _documentControl.CreateDocumentWithUpload(recoveredPages, GetpreIndexDocumentFlow(file.DocType), GetPosIndexDocumentFlow(), GetIndexFields(file.CodFlowExecute.ToString()));
        }

        private Dictionary<string, string> GetIndexFields(string codFlowExecute)
        {
            return new Dictionary<string, string>()
            {
                {"C24BA8236", codFlowExecute}
            };
        }

        private SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.DocumentWorkflow GetpreIndexDocumentFlow(string docType)
        {
            return new SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.DocumentWorkflow()
            {
                DoctypeCommand = docType,
                QueueName = _configuration["DocumentFlow:PreIndexQueue"],
                SituationName = _configuration["DocumentFlow:PreIndexSituation"],
                PendencyName = _configuration["DocumentFlow:PreIndexPendency"]
            };
        }
        private SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.DocumentWorkflow GetPosIndexDocumentFlow()
        {
            return new SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.DocumentWorkflow()
            {
                QueueName = _configuration["DocumentFlow:PosIndexQueue"],
                SituationName = _configuration["DocumentFlow:PosIndexSituation"],
                PendencyName = _configuration["DocumentFlow:PosIndexPendency"]

            };
        }
    }
}
