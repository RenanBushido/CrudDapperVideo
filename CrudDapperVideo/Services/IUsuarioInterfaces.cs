using CrudDapperVideo.Dto;
using CrudDapperVideo.Models;

namespace CrudDapperVideo.Services
{
    public interface IUsuarioInterfaces
    {
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios();
    }
}
