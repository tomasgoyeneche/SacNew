using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IAgregarEditarSeguroView
    {
        int IdEmpresa { get; }
        int IdEmpresaSeguroEntidad { get; }
        int IdCia { get; }
        int IdCobertura { get; }
        string NumeroPoliza { get; }
        DateTime CertificadoMensual { get; }
        DateTime VigenciaAnual { get; }

        void CargarEntidades(List<EmpresaSeguroEntidad> entidades);
        void CargarCias(List<Cia> cias);
        void CargarCoberturas(List<Cobertura> coberturas);
        void MostrarMensaje(string mensaje);
        void InicializarValores(EmpresaSeguro seguro);
    }
}
