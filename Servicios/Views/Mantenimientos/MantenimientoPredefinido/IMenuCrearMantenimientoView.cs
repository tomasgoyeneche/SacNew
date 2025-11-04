using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimientos
{
    public interface IMenuCrearMantenimientoView : IViewConMensajes
    {
        int IdMantenimiento { get; set; }
        string Nombre { get; set; }
        int IdTipoMantenimiento { get; set; }
        string AplicaA { get; set; }
        string Descripcion { get; set; }
        int? KilometrosIntervalo { get; set; }
        int? DiasIntervalo { get; set; }

        decimal HorasTotales { get; set; }
        decimal ManoObraTotal { get; set; }
        decimal RepuestosTotales { get; set; }

        void CargarTiposMantenimiento(List<TipoMantenimiento> tipos, string tipoVista);

        void CargarAplicaA();

        void CargarFrecuencias();

        void OcultarFrecuencias();

        void CargarTareasPredefinidas(List<Tarea> tareas);

        void CargarTareasAsignadas(List<Tarea> tareas);

        void SeleccionarFrecuencia(string frecuencia, int? valorIntervalo);

        void CargarTareasAsignadasOrdenes(List<OrdenTrabajoTarea> tareasOrdenes);

        void Cerrar();
    }
}