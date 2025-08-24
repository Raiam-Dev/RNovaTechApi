using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace RNovaTech.Features.Shared.Atributo
{
    public class FirebaseAutenticacaoAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string? token = ExtrairToken(context);
            if (token == null) return;

            FirebaseToken? decodedToken = await ValidarToken(token, context);
            if (decodedToken == null) return;

            ConfigurarClaimsUsuario(decodedToken, context);
        }
        private string? ExtrairToken(AuthorizationFilterContext context)
        {
            string? authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Bearer ") || authHeader.Substring(7) == "")
            {
                context.Result = new UnauthorizedResult();
                return null;
            }
            string token = authHeader!.Substring("Bearer ".Length).Trim();
            return token;
        }
        private async Task<FirebaseToken?> ValidarToken(string? token, AuthorizationFilterContext context)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                if (decodedToken == null)
                {
                    Unauthorized(context);
                    return null;
                }
                return decodedToken;
            }
            catch (FirebaseAuthException)
            {
                Unauthorized(context);
                return null;
            }
            catch (Exception erro)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Erro ao autenticar: " + erro.Message });
                return null;
            }
        }
        private void ConfigurarClaimsUsuario(FirebaseToken decodedToken, AuthorizationFilterContext context)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, decodedToken.Uid),
               //new Claim(ClaimTypes.Name, nome),
                 new Claim("email", decodedToken.Claims["email"]?.ToString() ?? "Unknown")
            };

            var identity = new ClaimsIdentity(claims, "Firebase");
            var principal = new ClaimsPrincipal(identity);
            context.HttpContext.User = principal;
        } 
        private void Unauthorized(AuthorizationFilterContext context) 
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
