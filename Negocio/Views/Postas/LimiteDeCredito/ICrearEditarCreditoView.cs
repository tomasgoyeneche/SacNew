using Shared.Models;

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