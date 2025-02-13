using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
