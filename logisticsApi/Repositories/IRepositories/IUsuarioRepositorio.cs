using logisticsApi.Models;
using logisticsApi.Models.Dtos;

namespace logisticsApi.Repositories.IRepositories
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuarios> GetUsuarios();

        Usuarios GetUsuario(int usuarioId);

        bool IsUniqueUser(string usuario);
        Task<UsuariosLoginRespuestaDto> Login(UsuariosLoginDto usuariosLoginDto);
        Task<Usuarios> Registro(UsuariosRegistroDto usuariosRegistroDto);

    }
}
