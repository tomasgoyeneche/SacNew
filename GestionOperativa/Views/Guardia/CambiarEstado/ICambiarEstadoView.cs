using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views
{
    public interface ICambiarEstadoView : IViewConMensajes
    {
        DateTime? FechaCompleta { get; }
        DateTime? FechaTractor { get; }
        DateTime? FechaChofer { get; }
        DateTime? FechaReingreso { get; }
        DateTime? FechaCarga { get; }


        void InicializarBotones(GuardiaDto guardia, bool admin);

        void Close();
    }
}