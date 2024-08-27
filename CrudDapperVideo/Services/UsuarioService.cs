using AutoMapper;
using CrudDapperVideo.Dto;
using CrudDapperVideo.Models;
using Dapper;
using Npgsql;

namespace CrudDapperVideo.Services
{
    public class UsuarioService : IUsuarioInterfaces
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId)
        {
            ResponseModel<UsuarioListarDto> response = new();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from \"Usuarios\" where \"Id\" = @Id", new { Id = usuarioId });

                if (usuarioBanco == null)
                {
                    response.Mensagem = "Nenhum usuário localizado.";
                    response.Status = false;
                    return response;
                }

                var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuarioBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários Localizado com sucesso.";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDto>> response = new();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("select * from \"Usuarios\"");

                if(!usuariosBanco.Any())
                {
                    response.Mensagem = "Nenhum usuário localizado.";
                    response.Status = false;
                    return response;
                }

                var usuarioMapeado = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários Localizados com sucesso.";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("insert into \"Usuarios\" (\"NomeCompleto\",\"Email\",\"Cargo\",\"Salario\",\"CPF\",\"Situacao\",\"Senha\") values (@NomeCompleto,@Email,@Cargo,@Salario,@CPF,@Situacao,@Senha)", usuarioCriarDto);

                if(usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro !";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeado = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeado;
                response.Mensagem = "Usuários listados com sucesso.";
            }

            return response;
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(NpgsqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("select * from \"Usuarios\"");
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("update \"Usuarios\" set \"NomeCompleto\"=@NomeCompleto,\"Email\"=@Email,\"Cargo\"=@Cargo,\"Salario\"=@Salario,\"CPF\"=@CPF,\"Situacao\"=@Situacao where \"Id\" = @Id", usuarioEditarDto);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição !";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeado = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeado;
                response.Mensagem = "Usuários listados com sucesso.";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioListarDto>> response = new();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("delete from \"Usuarios\" where \"Id\" = @Id", new { Id = usuarioId });

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a remoção !";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeado = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeado;
                response.Mensagem = "Usuários listados com sucesso.";
            }

            return response;
        }
    }
}
