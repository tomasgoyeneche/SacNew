using DevExpress.XtraEditors;
using GestionFlota.Presenters.Informes;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionFlota.Views.Postas.Informes.ConsultarConsumos
{
    public partial class MostrarExportaConsumosForm : DevExpress.XtraEditors.XtraForm, IResultadosConsumoView
    {
        private readonly ResultadosConsumoPresenter _presenter;
        private List<InformeConsumoPocDto> _resultados = new();

        public MostrarExportaConsumosForm(ResultadosConsumoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarResultados(List<InformeConsumoPocDto> resultados)
        {
            _resultados = resultados;
            gridControlConsumos.DataSource = resultados;
            gridViewConsumos.BestFitColumns();

            ConfigurarColumnas();
            _presenter.CalcularYMostrarTotales(resultados);
        }

        public void MostrarTotales(List<TotalConsumoDto> totales)
        {
            gridControlTotales.DataSource = totales;
            gridViewTotales.BestFitColumns();
            // Configuración visual del grid
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public List<InformeConsumoPocDto> ObtenerResultados()
        {
            return _resultados;
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await _presenter.ExportarAExcelAsync();
        }

        private void ConfigurarColumnas()
        {
            var columnasVisibles = new List<string>
    {
        "NumeroPoc", "Chofer_Nombre", "Codigo_Posta", "Empresa_Nombre", "Tractor_Patente", "Semi_Patente",
        "Concepto_Codigo", "Odometro", "Comentario", "FechaCreacion", "FechaCierre", "Usuario",
        "NumeroVale", "LitrosAutorizados", "LitrosCargados", "Observaciones", "Dolar",
        "PrecioDolar", "PrecioTotal", "FechaCarga", "Estado"
    };

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewConsumos.Columns)
            {
                col.Visible = columnasVisibles.Contains(col.FieldName);
            }
        }
    }
}