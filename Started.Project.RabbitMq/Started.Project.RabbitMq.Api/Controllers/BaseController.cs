using Microsoft.AspNetCore.Mvc;
using Started.Project.RabbitMq.Api.Support;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Started.Project.RabbitMq.Api.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Não autorizado", typeof(ErrorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Solicitação é inválida", typeof(ErrorResponse))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Ocorreu algum erro interno no servidor", typeof(ErrorResponse))]
    [SwaggerResponse((int)(HttpStatusCode)429, "Muitas requisições em um determinado período de tempo", typeof(ErrorResponse))]
    public class BaseController : Controller
    {

    }
}
