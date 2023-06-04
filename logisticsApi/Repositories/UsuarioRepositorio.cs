using logisticsApi.Infrastructure;
using logisticsApi.Models;
using logisticsApi.Models.Dtos;
using logisticsApi.Repositories.IRepositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace logisticsApi.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly LogisticsDbContext _context;
        private string claveSecreta;

        public UsuarioRepositorio(LogisticsDbContext context, IConfiguration config)
        {
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }

        public Usuarios GetUsuario(int usuarioId)
        {
           return   _context.usuarios.FirstOrDefault(u=> u.UsuarioID== usuarioId);
        }

        public ICollection<Usuarios> GetUsuarios()
        {
            return _context.usuarios.OrderBy(u=>u.NombreUsuario).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuariobd = _context.usuarios.FirstOrDefault(u => u.NombreUsuario == usuario);
            if(usuariobd == null) 
            {
                return true;
            }
                return false;
           
        }

        public async Task<UsuariosLoginRespuestaDto> Login(UsuariosLoginDto usuariosLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuariosLoginDto.Password);

            var usuario = _context.usuarios.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == usuariosLoginDto.NombreUsuario.ToLower()
                && u.Password == passwordEncriptado
                );
            // VAlidamos si el usuario no existe con la combinación correcta de usuario y contraseña
            if(usuario == null)
            {
                return new UsuariosLoginRespuestaDto()
                {
                    Token = "",
                    Usuarios = null
                };
            }

            // Aqui existe el usuario entonces procedemos a procesar el login
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Role,usuario.Role),
                }),
                Expires= DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuariosLoginRespuestaDto usuariosLoginRespuestaDto = new UsuariosLoginRespuestaDto()
            {
                Token= manejadorToken.WriteToken(token),
                Usuarios=usuario
            };

            return usuariosLoginRespuestaDto;
                 

        }

        public async Task<Usuarios> Registro(UsuariosRegistroDto usuariosRegistroDto)
        {
            var passwordEncriptado = obtenermd5(usuariosRegistroDto.Password);
            Usuarios usuario = new Usuarios()
            {
                NombreUsuario=usuariosRegistroDto.NombreUsuario,
                Password=passwordEncriptado,
                Nombre=usuariosRegistroDto.Nombre,
                Role = usuariosRegistroDto.Role
            };
            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return usuario;
        }


        // Metodo para encriptar contraseña
        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data =System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
