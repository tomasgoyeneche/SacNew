using SacNew.Interfaces;
using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public interface IImportarConsumosYpfView : IViewConMensajes
    {
        void CargarPeriodos(List<Periodo> periodos);
        Periodo? PeriodoSeleccionado { get; }
        List<ImportConsumoYpfEnRuta> ObtenerConsumos();

        void MostrarConsumos(List<ImportConsumoYpfEnRuta> consumos);

    }
}
