using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarKilometrosView : IViewConMensajes
    {
        void CargarLocaciones(List<Locacion> locaciones);

        int IdLocacionDestino { get; }
        decimal Kilometros { get; }
    }
}