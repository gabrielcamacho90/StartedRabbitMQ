using Newtonsoft.Json;
using System.Collections.Generic;

namespace Started.Project.RabbitMq.Api.Support
{
    public class Error
    {
        public Error(string Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
            _details = new List<Error>();
        }

        public string Code { get; private set; }
        public string Message { get; private set; }

        [JsonIgnore]
        private readonly List<Error> _details;
        public IReadOnlyCollection<Error> Details => _details;

        internal void IncludeDetails(IDictionary<string, string> errorList)
        {
            foreach (var itemError in errorList)
            {
                _details.Add(new Error(itemError.Key, itemError.Value));
            }
        }
    }
}
