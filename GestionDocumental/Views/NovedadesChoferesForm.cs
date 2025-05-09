using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using GestionDocumental.Presenters;
using GestionFlota.Presenters;
using Shared.Models;
using Shared.Models.DTOs;
using System.Globalization;
using System.Threading.Tasks;

namespace GestionDocumental.Views
{
    public partial class NovedadesChoferesForm : DevExpress.XtraEditors.XtraForm, INovedadesChoferesView
    {
        private readonly NovedadesChoferesPresenter _presenter;

        public NovedadesChoferesForm(NovedadesChoferesPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarNovedades(List<NovedadesChoferesDto> listaNovedades)
        {
            gridControlNovedades.DataSource = listaNovedades;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewNovedades;
            view.Columns["idEstadoChofer"].Visible = false;
            view.Columns["idChofer"].Visible = false;
            view.Columns["idEstado"].Visible = false;
            view.Columns["Abreviado"].Visible = false;

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

        private async void NovedadesChoferesForm_Load(object sender, EventArgs e)
        {
            await _presenter.CargarNovedadesAsync(dispoCheck.Checked);
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {

            NovedadesChoferesDto row = gridViewNovedades.GetFocusedRow() as NovedadesChoferesDto;
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
            NovedadesChoferesDto row = gridViewNovedades.GetFocusedRow() as NovedadesChoferesDto;
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
            await _presenter.CargarNovedadesAsync(dispoCheck.Checked);
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