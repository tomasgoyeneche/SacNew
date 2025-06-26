using Core.Interfaces;
using Shared.Models;
using System.ComponentModel;

namespace GestionFlota.Views
{
    public interface IImportarProgramaView : IViewConMensajes
    {
        BindingList<PedidoImportacion> ObtenerPedidosImportados();

        void MostrarErrores(List<ErrorImportacionDto> errores);

        void HabilitarGuardar(bool habilitar);
    }
}