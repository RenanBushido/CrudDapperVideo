using CrudDapperVideo.Dto;
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

            if (usuarios.Status == false) 
            { 
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int usuarioId)
        {
            var usuario = await _usuarioInterfaces.BuscarUsuarioPorId(usuarioId);

            if(usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuarios = await _usuarioInterfaces.CriarUsuario(usuarioCriarDto);

            if (usuarios.Status == false) return NotFound(usuarios);

            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuarios = await _usuarioInterfaces.EditarUsuario(usuarioEditarDto);

            if (usuarios.Status == false) return NotFound(usuarios);

            return Ok(usuarios);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuarios = await _usuarioInterfaces.RemoverUsuario(usuarioId);

            if (usuarios.Status == false) return NotFound(usuarios);

            return Ok(usuarios);
        }
    }
}
