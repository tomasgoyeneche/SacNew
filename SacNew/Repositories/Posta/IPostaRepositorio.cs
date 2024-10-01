using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IPostaRepositorio
    {
        Task<List<Posta>> ObtenerTodasLasPostasAsync();

        Task<List<Posta>> BuscarPostasAsync(string textoBusqueda);

        Task AgregarPostaAsync(Posta nuevaPosta);

        Task ActualizarPostaAsync(Posta postaActualizada);

        Task EliminarPostaAsync(int idPosta);
    }
}