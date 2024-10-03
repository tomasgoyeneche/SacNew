using SacNew.Models.DTOs;

namespace SacNew.Interfaces
{
    public interface IMenuIngresaConsumosView
    {
        void MostrarPOC(List<POCDto> listaPOC);

        void MostrarMensaje(string mensaje);

        void MostrarNombreUsuario(string nombre);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}