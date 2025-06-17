using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IImportarProgramaView : IViewConMensajes
    {
        BindingList<PedidoImportacion> ObtenerPedidosImportados();
        void MostrarErrores(List<ErrorImportacionDto> errores);
        void HabilitarGuardar(bool habilitar);

    }
}
