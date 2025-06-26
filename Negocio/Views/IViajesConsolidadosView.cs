using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IViajesConsolidadosView : IViewConMensajes
    {
        void CargarProgramas(List<VistaProgramaGridDto> programas);
        void MostrarMensaje(string mensaje);
        void MostrarRemitosFaltantes(int faltanCarga, int faltanEntrega);
    }
}
