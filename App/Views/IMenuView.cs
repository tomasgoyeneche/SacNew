using Core.Interfaces;

namespace App.Views
{
    public interface IMenuView : IViewConMensajes
    {
        void MostrarDiaDeHoy(string? diaDeHoy);

        void MostrarNombreUsuario(string? nombreUsuario);
    }
}