using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Servicios.Views.Mantenimientos
{
    public interface IEditarOrdenTrabajoView : IViewConMensajes
    {
        int IdOrdenTrabajo { get; set; }
        DateTime FechaEmision { get; set; }

        int? IdUnidad { get; set; }
        int? IdNomina { get; set; }

        int? IdLugarReparacion { get; set; }

        DateTime? FechaIngreso { get; set; }
        DateTime? FechaFin { get; set; }

        decimal? OdometroIngreso { get; set; }
        decimal? OdometroSalida { get; set; }
        decimal? Costo { get; set; } //
        decimal? Horas { get; set; } //
        string Observaciones { get; set; }

        void CargarUnidades(List<UnidadDto> unidades);
        void CargarLugares(List<LugarReparacion> lugares);
        void CargarMantenimientosPredefinidos(List<Shared.Models.Mantenimiento> mantenimientos);
        void CargarMantenimientosOrden(List<OrdenTrabajoMantenimiento> mantenimientos);
        void CargarComprobantes(List<OrdenTrabajoComprobante> comprobantes);
        void ActualizarEstadoUI(int fase);
        void Cerrar();
    }
}
