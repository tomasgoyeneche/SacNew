using SacNew.Models.DTOs;

namespace SacNew.Interfaces
{
    public interface IMenuIngresaConsumosView : IViewConMensajes, IViewConUsuario
    {
        void MostrarPOC(List<POCDto> listaPOC);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}