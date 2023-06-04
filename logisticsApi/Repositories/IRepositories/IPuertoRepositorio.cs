using logisticsApi.Models;

namespace logisticsApi.Repositories.IRepositories
{
    public interface IPuertoRepositorio
    {
        ICollection<Puertos> GetPuertos();

        Puertos GetPuerto(int puertoId);

        bool ExistePuerto(string nombre);
        bool ExistePuerto(int id);

        bool CrearPuerto(Puertos puerto);
        bool ActualizarPuerto(Puertos puerto);
        bool BorrarPuerto(Puertos puerto);

        bool Guardar();
    }
}
