using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiDocker.Business;
using ApiDocker.Model;
using ApiDocker.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiDocker.Repository;

namespace ApiDocker.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class DirectorShipController : ControllerBase
    {
        private readonly ILogger<DirectorShipController> _logger;
        private IDirectorShipRepository _directorShipRepository;


        public DirectorShipController(ILogger<DirectorShipController> logger, IDirectorShipRepository directorShipRepository)
        {
            _logger = logger;
            _directorShipRepository = directorShipRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] DirectorShip model)
        {
            DirectorShip user = new();
            // Recupera o usuário
            user = _directorShipRepository.FindByUserName(model.Dir_userName, model.Dir_pwd);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Dir_pwd = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Authorize(Roles = "diretor")]
        public async Task<ActionResult<List<DirectorShip>>> Get()
        {
            return _directorShipRepository.FindAll();
        }

        [HttpPost]
        [Authorize(Roles = "diretor")]
        public async Task<ActionResult<DirectorShip>> post([FromBody] DirectorShip director)
        {
            return _directorShipRepository.Create(director);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "diretor")]
        public async Task<ActionResult<int>> Delete(long id)
        {
            return _directorShipRepository.Delete(id);
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        //[HttpGet]
        //[Route("authenticated")]
        //[Authorize]
        //public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        //[HttpGet]
        //[Route("employee")]
        //[Authorize(Roles = "diretor,professor")]
        //public string Employee() => "Funcionário";
    }
}
