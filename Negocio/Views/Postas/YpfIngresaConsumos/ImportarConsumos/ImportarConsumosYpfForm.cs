using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos;
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

namespace GestionFlota.Views.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public partial class ImportarConsumosYpfForm : DevExpress.XtraEditors.XtraForm, IImportarConsumosYpfView
    {
        private readonly ImportarConsumosYpfPresenter _presenter;

        public ImportarConsumosYpfForm(ImportarConsumosYpfPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public Periodo? PeriodoSeleccionado => cmbPeriodos.SelectedItem as Periodo;

        public void CargarPeriodos(List<Periodo> periodos)
        {
            cmbPeriodos.DataSource = periodos;
            cmbPeriodos.DisplayMember = "NombrePeriodo";
            cmbPeriodos.ValueMember = "IdPeriodo";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarConsumos(List<ImportConsumoYpfEnRuta> consumos)
        {
            gridControlDatos.DataSource = consumos;

            // Ocultamos columnas no deseadas desde el GridView
            var view = dgvDatos;
            view.Columns["IdImportConsumoYPF"].Visible = false;
            view.Columns["Chequeado"].Visible = false;

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido
        }

        public List<ImportConsumoYpfEnRuta> ObtenerConsumos()
        {
            return dgvDatos.DataSource as List<ImportConsumoYpfEnRuta> ?? new List<ImportConsumoYpfEnRuta>();
        }

        private async void ImportarConsumosYPF_Load(object sender, EventArgs e)
        {
            await _presenter.CargarPeriodosAsync();
        }

        private async void btnExportarExcel_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog { Filter = "Archivos Excel|*.xlsx" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await _presenter.ExportarConsumosAExcelAsync(saveFileDialog.FileName);
                }
            }
        }

        private async void btnImportar_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog { Filter = "Excel Files|*.xlsx;*.xls" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await _presenter.ImportarConsumosAsync(openFileDialog.FileName);
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumosAsync();
            this.Dispose();
        }
    }
}