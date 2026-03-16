using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDocumental.Views.Novedades
{
    public interface INovedadesChoferesCalendarioView : IViewConMensajes
    {
        // Navegación del período
        int IdTraficoSeleccionado { get; }
        DateTime MesSeleccionado { get; }     // Cualquier fecha dentro del mes seleccionado (ej: 2026-02-01)

        int PromedioAusenciasChofer { set; }
        void SetMesSeleccionado(DateTime fechaDelMes);

        // Scheduler (la vista no expone el control, expone un binder)
        void ConfigurarScheduler();
        void BindearScheduler(
            List<UnidadChoferResourceDto> choferes,
            List<UnidadChoferSchedulerDto> ausencias,
            DateTime desde,
            DateTime hasta);
    }
}
