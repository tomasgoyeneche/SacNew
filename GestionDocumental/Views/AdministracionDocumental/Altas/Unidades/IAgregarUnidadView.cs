using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IAgregarUnidadView
    {
        void MostrarEmpresa(string nombreEmpresa, int idEmpresa);

        void CargarTractores(List<Tractor> tractores);
        void CargarTrafico(List<Trafico> traficos);
        void CargarSemis(List<Semi> semis);
        int IdTraficoSeleccionado { get; }

        int IdEmpresa { get; }
        int? IdTractorSeleccionado { get; }
        int? IdSemiSeleccionado { get; }
        void MostrarMensaje(string mensaje);

        void Cerrar();
    }
}