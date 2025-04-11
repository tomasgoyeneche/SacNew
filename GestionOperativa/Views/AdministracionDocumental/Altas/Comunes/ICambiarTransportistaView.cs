using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    // Interface para la vista
    public interface ICambiarTransportistaView
    {
        void CargarEmpresas(List<EmpresaDto> empresas);

        void SeleccionarEmpresaActual(int idEmpresa);

        int IdEmpresaSeleccionada { get; }

        void MostrarMensaje(string mensaje);

        void Cerrar();
    }
}