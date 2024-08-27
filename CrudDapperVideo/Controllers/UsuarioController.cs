using CrudDapperVideo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapperVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterfaces _usuarioInterfaces;
        public UsuarioController(IUsuarioInterfaces usuarioInterfaces)
        {
            _usuarioInterfaces = usuarioInterfaces;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterfaces.BuscarUsuarios();

            if (usuarios == null) 
            { 
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }
    }
}
