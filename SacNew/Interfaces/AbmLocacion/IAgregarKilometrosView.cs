using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarKilometrosView
    {
        void CargarLocaciones(List<Locacion> locaciones);

        int IdLocacionDestino { get; }
        decimal Kilometros { get; }

        void MostrarMensaje(string mensaje);
    }
}