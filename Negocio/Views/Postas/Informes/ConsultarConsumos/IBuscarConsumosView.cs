using Shared.Models.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views.Postas.Informes.ConsultarConsumos
{
    public interface IBuscarConsumosView
    {
        int? IdConcepto { get; }
        int? IdPosta { get; }
        int? IdEmpresa { get; }
        int? IdUnidad { get; }
        int? IdChofer { get; }
        string NumeroPoc { get; }
        string Estado{ get; }
        DateTime? FechaCreacionDesde { get; }
        DateTime? FechaCreacionHasta { get; }
        DateTime? FechaCierreDesde { get; }
        DateTime? FechaCierreHasta { get; }

        void CargarConceptos(List<Concepto> conceptos);
        void CargarPostas(List<Posta> postas);
        void CargarEmpresas(List<EmpresaDto> empresas);
        void CargarUnidades(List<UnidadPatenteDto> unidades);
        void CargarChoferes(List<Chofer> choferes);

        void MostrarMensaje(string mensaje);
    }
}
