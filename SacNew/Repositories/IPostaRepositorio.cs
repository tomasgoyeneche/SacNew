using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IPostaRepositorio
    {
        List<Posta> ObtenerTodasLasPostas();

        List<Posta> BuscarPostas(string textoBusqueda);

        void AgregarPosta(Posta nuevaPosta);

        void ActualizarPosta(Posta postaActualizada);

        void EliminarPosta(int idPosta);  // Nuevo método para eliminar
    }
}