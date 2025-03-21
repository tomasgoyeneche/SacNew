using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views.Postas.LimiteDeCredito
{
    public interface ICrearEditarCreditoView
    {
        int IdEmpresa { get; }
        decimal CreditoAsignado { get; }
        DateTime PeriodoSeleccionado { get; }

        void CargarEmpresas(List<EmpresaDto> empresas);
        void MostrarCreditoActual(decimal? credito);
        void MostrarMensaje(string mensaje);
        
        void Close();   
    }
}
