using Core.Interfaces;
using Shared.Models.DTOs;

namespace SacNew.Interfaces
{
    public interface IMenuIngresaConsumosView : IViewConMensajes
    {
        void MostrarPOC(List<POCDto> listaPOC);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}