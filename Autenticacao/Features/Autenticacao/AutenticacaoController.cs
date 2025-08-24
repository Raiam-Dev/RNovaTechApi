using AplicacaoWebApi.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNovaTech.Features._Shared;
using RNovaTech.Features.Shared.Atributo;
using RNovaTech.Infra.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace RNovaTech.Features.Autenticacao
{

    //[FirebaseAutenticacao]
    [Route("api/[controller]")]
    public class AutenticacaoController : ApiController
    {
        private readonly DbContextProducao _dbContext;
        public AutenticacaoController(DbContextProducao dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("cadastrar")]
        [SwaggerOperation(
            Summary = "Cadastra um novo usuário",
            Description = "Registra um novo usuário no sistema utilizando o token do Firebase."
        )]
        public async Task<IActionResult> Cadastrar([FromServices] FirebaseTokenService tokenService)
        {
            if (tokenService == null) return BadRequest();

            var usuario = tokenService.GetCurrentUser(HttpContext);

            if(usuario == null) return ResponseUnauthorized("Usuário não autenticado");

            var usuarioExiste = await _dbContext.Usuarios
                .AnyAsync(u => u.Uid == usuario.Uid || u.Email == usuario.Email);

            if (usuarioExiste) return ResponseNotFound(erros: "Usuario já cadastrado");

            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return ResponseOk();
        }
    }
}
