using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.Informes;
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

namespace GestionFlota.Views.Postas.Informes.ConsumoUnidad
{
    public partial class ConsumoPorUnidadForm : DevExpress.XtraEditors.XtraForm, IConsumoUnidadView
    {
        private readonly ConsumoPorUnidadPresenter _presenter;

        public ConsumoPorUnidadForm(ConsumoPorUnidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void ConsumoPorUnidad_Load(object sender, EventArgs e)
        {
            await _presenter.CargarPeriodosAsync();
        }

        public int IdPeriodoSeleccionado => (int)cmbPeriodos.SelectedValue;

        public void CargarPeriodos(List<Periodo> periodos)
        {
            cmbPeriodos.DataSource = periodos;
            cmbPeriodos.DisplayMember = "NombrePeriodo";
            cmbPeriodos.ValueMember = "IdPeriodo";
        }

        public void MostrarConsumos(List<InformeConsumoUnidad> consumos)
        {
            gridControlDatos.DataSource = consumos;

            // Ocultamos columnas no deseadas desde el GridView
            var view = dgvDatos;
            view.Columns["idPeriodo"].Visible = false;

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido
            // Formatear las celdas para agregar el símbolo de porcentaje
        }

        private void dgvConsumos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica si la columna requiere el símbolo de porcentaje
            var columnName = dgvDatos.Columns[e.ColumnIndex].Name;

            if (columnName.StartsWith("Porcentaje", StringComparison.OrdinalIgnoreCase) && e.Value is decimal decimalValue)
            {
                // Aplica formato con el símbolo de porcentaje
                e.Value = $"{decimalValue}%";
                e.FormattingApplied = true;
            }
        }

        public List<InformeConsumoUnidad> ObtenerConsumos()
        {
            return (List<InformeConsumoUnidad>)dgvDatos.DataSource;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarConsumosAsync();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumosAsync();
        }

        private async void btnExportarExcel_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog { Filter = "Excel Files|*.xlsx" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await _presenter.ExportarConsumosAExcelAsync(saveFileDialog.FileName);
                }
            }
        }
    }
}