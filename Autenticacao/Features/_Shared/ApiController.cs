using Microsoft.AspNetCore.Mvc;
using RNovaTech.Features.Shared.Extensions;
using System.Net;

namespace AplicacaoWebApi.Features.Shared
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ResponseOk()
           => Response(HttpStatusCode.OK);

        protected IActionResult ResponseOk(object result)
           => Response(HttpStatusCode.OK, result);

        protected IActionResult ResponseCreated(object result)
            => Response(HttpStatusCode.Created, result);

        protected IActionResult ResponseBadRequest(object result, string erros)
            => Response(HttpStatusCode.BadRequest, result, erros: erros);

        protected IActionResult ResponseUnauthorized(string erros)
            => Response(HttpStatusCode.Unauthorized, null, erros: erros);

        protected IActionResult ResponseForbidden(string erros)
            => Response(HttpStatusCode.Forbidden, erros: erros);

        protected IActionResult ResponseNotFound(string erros)
            => Response(HttpStatusCode.NotFound, erros: erros);
        protected IActionResult ResponseInternalError(string erros)
            => Response(HttpStatusCode.InternalServerError, erros: erros);

        private new JsonResult Response (HttpStatusCode statusCode,object? dados = null,string? erros = null)
        {
            bool sucesso = statusCode.IsSuccess();
            CustomResult result;

            if (string.IsNullOrWhiteSpace(erros))
            {
                result = dados != null
                    ? new CustomResult(sucesso, dados)
                    : new CustomResult(sucesso);
            }
            else 
            {
               List<string> colecaoErros = new List<string>();

               if (!string.IsNullOrWhiteSpace(erros))
                    colecaoErros.Add(erros);
               result = new CustomResult(sucesso,dados,colecaoErros);


            }
            return new JsonResult(result) { StatusCode = (int)statusCode };
            
        }
    }
}
