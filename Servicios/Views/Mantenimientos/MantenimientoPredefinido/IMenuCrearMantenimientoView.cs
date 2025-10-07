using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        void CargarTiposMantenimiento(List<TipoMantenimiento> tipos);
        void CargarAplicaA();
        void CargarFrecuencias();
        void OcultarFrecuencias();
        void CargarTareasPredefinidas(List<Tarea> tareas);
        void CargarTareasAsignadas(List<Tarea> tareas);
        void SeleccionarFrecuencia(string frecuencia, int? valorIntervalo);

        void Cerrar();
    }
}
