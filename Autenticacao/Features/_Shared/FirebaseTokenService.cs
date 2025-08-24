using RNovaTech.Domain.Entidades;

namespace RNovaTech.Features._Shared
{
    public interface IFirebaseTokenService
    {
        Usuario? GetCurrentUser(HttpContext httpContext);
    }

    public class FirebaseTokenService : IFirebaseTokenService
    {
        public Usuario? GetCurrentUser(HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user.Identity?.IsAuthenticated != true) return null;

            var uid = user.FindFirst("uid")?.Value;
            var nome = user.FindFirst("nome")?.Value;
            var email = user.FindFirst("email")?.Value;

            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email)) return null;

            return new Usuario
            {
                Uid = uid,
                Nome = nome,
                Email = email
            };
        }
    }
}
