using SacNew.Models.DTOs;
using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SacNew.Interfaces;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public interface IIngresoManualYPFView : IViewConMensajes
    {
        DateTime FechaVale { get; }
        int TipoGasoilSeleccionado { get; }
        List<TicketInfo> Tickets { get; }

        TicketInfo TicketUrea { get; }
        void CargarTiposGasoil(List<Concepto> tiposGasoil);
    }
}
