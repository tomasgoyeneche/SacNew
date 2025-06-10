using Core.Interfaces;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IDisponibilidadView : IViewConMensajes
    {
        DateTime FechaSeleccionada { get; }
        void CargarDisponibilidades(List<Disponibilidad> disponibilidades);
        void ConfigurarControles();


        //void MostrarResumen(List<DispoResumen> resumen);

        //void MostrarHistorial(List<NominaHistorial> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);
        void Close();
    }
}
