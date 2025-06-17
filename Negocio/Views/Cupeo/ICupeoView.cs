using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface ICupeoView : IViewConMensajes
    {
        void MostrarCupeoDisp(List<Cupeo> cargados);
        void MostrarCupeoAsignados(List<Cupeo> vacios);

        //void MostrarResumen(List<CupeoResumen> resumen);

        //void MostrarHistorial(List<NominaHistorial> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);
    }
}
