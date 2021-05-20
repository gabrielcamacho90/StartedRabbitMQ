using System.Collections.Generic;

namespace Started.Project.RabbitMq.Api.Support
{
    public class ErrorResponse
    {
        public ErrorResponse(Error error)
        {
            Error = error;
        }

        public ErrorResponse(string code, string message)
        {
            Error = new Error(code, message);
        }

        public ErrorResponse(string code, string message, Dictionary<string, string> errorList)
        {
            Error = new Error(code, message);
            if (errorList.Count > 0)
                Error.IncludeDetails(errorList);
        }

        public Error Error { get; private set; }
    }
}
