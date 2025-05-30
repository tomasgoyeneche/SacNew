using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views
{
    public interface IAgregarEditarVaporizadoView : IViewConMensajes
    {
        void MostrarDatosGuardia(string texto);
        void CargarPlantas(List<VaporizadoZona> plantas);
        void CargarMotivos(List<VaporizadoMotivo> motivos);
        void CargarDatos(Vaporizado vaporizado);
        int CantidadCisternas { get; }
        int? IdMotivo { get; }
        DateTime? FechaInicio { get; }
        DateTime? FechaFin { get; }
        string TiempoVaporizado { get; set; }
        string NroCertificado { get; }
        int? IdPlanta { get; }
        string RemitoDanes { get; }
        string Observaciones { get; }
        string NroPresupuesto { get; }
        string NroImporte { get; }
        void SetNroPresupuestoVisible(bool visible);
        void SetNroImporteVisible(bool visible);
        void SetTiempoVaporizado(string tiempo);
        void Cerrar();
    }
}
