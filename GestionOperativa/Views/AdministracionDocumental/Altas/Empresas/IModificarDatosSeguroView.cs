using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public interface IModificarDatosSeguroView : IViewConMensajes
    {
        int IdSeguroEmpresa { get; }
        int IdEmpresa { get; }
        int IdCia { get; }
        int IdCobertura { get; }
        string NumeroPoliza { get; }
        DateTime VigenciaHasta { get; }
        DateTime PagoDesde { get; }
        DateTime PagoHasta { get; }

        void CargarDatosSeguro(EmpresaSeguro seguro, List<Cia> cias, List<Cobertura> coberturas);
    }
}