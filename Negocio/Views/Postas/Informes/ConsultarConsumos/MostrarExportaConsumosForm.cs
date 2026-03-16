using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
        private HashSet<string> _clavesValorizadas = new();

        public MostrarExportaConsumosForm(ResultadosConsumoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarResultados(List<InformeConsumoPocDto> resultados, bool exportaTransoft)
        {
            gridControlConsumos.DataSource = resultados;
            gridViewConsumos.BestFitColumns();

            bValorizaConsumos.Visible = exportaTransoft;
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

        public bool ConfirmarValorizacion(string mensaje)
        {
            return XtraMessageBox.Show(this,
                mensaje,
                "Confirmar valorización",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void MarcarRegistrosValorizados(List<string> claves)
        {
            _clavesValorizadas = claves.ToHashSet();
            gridViewConsumos.RefreshData();
        }

        public List<InformeConsumoPocDto> ObtenerResultados()
        {
            var lista = new List<InformeConsumoPocDto>();

            for (int i = 0; i < gridViewConsumos.DataRowCount; i++)
            {
                var row = gridViewConsumos.GetRow(i) as InformeConsumoPocDto;
                if (row != null)
                    lista.Add(row);
            }

            return lista;
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await _presenter.ExportarAExcelAsync();
        }


        private void gridViewConsumos_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            var row = gridViewConsumos.GetRow(e.RowHandle) as InformeConsumoPocDto;
            if (row == null)
                return;

            string clave = row.Tipo_Consumo == "GASOIL"
                ? $"G_{row.IdRegistro}"
                : $"O_{row.IdRegistro}";

            if (_clavesValorizadas.Contains(clave))
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
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

        private async void bValorizaConsumos_Click(object sender, EventArgs e)
        {
            await _presenter.ValorizaYCuentaConsumos();
        }
    }
}