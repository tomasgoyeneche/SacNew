using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public interface IAgregarKilometrosView : IViewConMensajes
    {
        void CargarLocaciones(List<Locacion> locaciones);

        int IdLocacionDestino { get; }
        decimal Kilometros { get; }
    }
}