using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IRuteoView : IViewConMensajes
    {
        void MostrarRuteoCargados(List<Shared.Models.Ruteo> cargados);
        void MostrarRuteoVacios(List<Shared.Models.Ruteo> vacios);

        void MostrarResumen(List<RuteoResumen> resumen);

        //void MostrarHistorial(List<NominaHistorial> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);
    }
}
