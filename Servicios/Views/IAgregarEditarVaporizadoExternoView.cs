using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views
{
    public interface IAgregarEditarVaporizadoExternoView : IViewConMensajes
    {
        void MostrarDatos(string texto);

        void CargarMotivos(List<VaporizadoMotivo> motivos);

        void CargarDatos(Vaporizado vaporizado, int idUnidad);
        void CargarUnidades(List<UnidadDto> unidades);
        int CantidadCisternas { get; }
        int? IdMotivo { get; }
        int? IdUnidad { get; }

        DateTime? FechaInicio { get; }
        DateTime? FechaFin { get; }
        string TiempoVaporizado { get; set; }
        string NroCertificado { get; }
        string RemitoDanes { get; }
        string Observaciones { get; }

        void SetTiempoVaporizado(string tiempo);

        void Cerrar();
    }
}
