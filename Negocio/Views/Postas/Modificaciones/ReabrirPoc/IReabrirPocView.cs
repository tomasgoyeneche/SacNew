using Core.Interfaces;
using Shared.Models.DTOs;

namespace GestionFlota.Views.Postas.Modificaciones.ReabrirPoc
{
    public interface IReabrirPocView : IViewConMensajes
    {
        void MostrarPOC(List<POCDto> listaPOC);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}