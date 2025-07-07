using DevExpress.XtraEditors;
using GestionDocumental.Presenters;

namespace GestionDocumental.Views
{
    public partial class NovedadesForm : DevExpress.XtraEditors.XtraForm, INovedadesView
    {
        public readonly NovedadesPresenter _presenter;
        public bool activoChecked => dispoCheck.Checked;

        public NovedadesForm(NovedadesPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarNovedades(object listaNovedades)
        {
            gridControlNovedades.DataSource = null;
            gridViewNovedades.Columns.Clear();

            gridControlNovedades.DataSource = listaNovedades;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewNovedades;

            if (_presenter._Entidad == "Chofer")
            {
                foreach (var col in new[] { "idEstadoChofer", "idChofer", "idEstado", "Abreviado" })
                {
                    if (view.Columns[col] != null)
                        view.Columns[col].Visible = false;
                }
                labelNovedades.Text = "Novedades Choferes";
            }
            else if (_presenter._Entidad == "Unidad")
            {
                foreach (var col in new[] { "idUnidadMantenimiento", "idUnidad", "idMantenimientoEstado", "Abreviado" })
                {
                    if (view.Columns[col] != null)
                        view.Columns[col].Visible = false;
                }
                labelNovedades.Text = "Mantenimiento Unidades";
            }

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido

            gridViewNovedades.OptionsView.EnableAppearanceEvenRow = true;
            gridViewNovedades.OptionsView.EnableAppearanceOddRow = true;
            gridViewNovedades.Appearance.Row.Font = new Font("Segoe UI", 9);
            gridViewNovedades.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75f);
            gridViewNovedades.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewNovedades.Appearance.Row.Options.UseFont = true;
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var row = gridViewNovedades.GetFocusedRow();
            if (row != null)
            {
                await _presenter.EliminarNovedadAsync(row);
            }
            else
            {
                MostrarMensaje("Seleccione una Novedad para eliminar.");
            }
        }

        private async void btnAgregarNovedad_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarNovedadAsync();
        }

        private async void btnEditarNovedad_Click(object sender, EventArgs e)
        {
            var row = gridViewNovedades.GetFocusedRow();
            if (row != null)
            {
                await _presenter.EditarNovedadAsync(row);
            }
            else
            {
                MostrarMensaje("Seleccione una Novedad para editar.");
            }
        }

        public void CargarMesesDisponibles(List<(int Mes, int Anio)> meses)
        {
            cmbMesAnio.DisplayMember = "Display";
            cmbMesAnio.ValueMember = "Value";
            cmbMesAnio.DataSource = meses
                .Select(x => new
                {
                    Display = $"{x.Mes} / {x.Anio}",
                    Value = x
                }).ToList();
        }

        public (int Mes, int Anio) ObtenerMesSeleccionado()
        {
            if (cmbMesAnio.SelectedValue is ValueTuple<int, int> seleccionado)
                return seleccionado;

            return (0, 0);
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await _presenter.ExportarCsvAsync();
        }

        private async void dispoCheck_CheckedChanged(object sender, EventArgs e)
        {
            await _presenter.CargarNovedadesAsync(dispoCheck.Checked, _presenter._Entidad);
        }

        //Exportar a excel devexpress

        //private void btnPruebaExcel_Click(object sender, EventArgs e)
        //{
        //    string rutaArchivo = @"C:\Compartida\Exportaciones\PruebaExportDvg.xlsx";

        //    gridViewNovedades.ExportToXlsx(rutaArchivo, new XlsxExportOptionsEx()
        //    {
        //        ExportType = DevExpress.Export.ExportType.WYSIWYG // o DataAware si querés solo datos
        //    });
        //}
    }
}