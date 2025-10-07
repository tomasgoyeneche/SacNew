using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class ViajesConsolidadosForm : DevExpress.XtraEditors.XtraForm, IViajesConsolidadosView
    {
        public readonly ViajesConsolidadosPresenter _presenter;

        public ViajesConsolidadosForm(ViajesConsolidadosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void CargarProgramas(List<VistaProgramaGridDto> programas)
        {
            gridControlProgramas.DataSource = programas;
            gridViewProgramas.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarRemitosFaltantes(int faltanCarga, int faltanEntrega)
        {
            txtCarga.Text = $"Carga: {faltanCarga}";
            txtEntrega.Text = $"Entrega: {faltanEntrega}";
        }

        private async void gridViewProgramas_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0 && gridViewProgramas.GetFocusedRow() is Shared.Models.VistaProgramaGridDto vistaGrid) // Es doble click sobre una celda válida
            {
                if (vistaGrid != null)
                {
                    var ruteo = await _presenter.MapearARuteoAsync(vistaGrid.Id);
                    if (ruteo != null)
                    {
                        await _presenter.AbrirEditarProgramaAsync(ruteo);
                    }
                }
            }
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            string mesStr = DevExpress.XtraEditors.XtraInputBox.Show(
                "Ingrese el mes (1-12):",
                "Mes",
                DateTime.Today.Month.ToString()
            );

            if (string.IsNullOrWhiteSpace(mesStr) || !int.TryParse(mesStr, out int mes) || mes < 1 || mes > 12)
            {
                XtraMessageBox.Show("Por favor ingrese un mes válido (1-12).");
                return;
            }

            string anioStr = DevExpress.XtraEditors.XtraInputBox.Show(
                "Ingrese el año (ejemplo: 2025):",
                "Año",
                DateTime.Today.Year.ToString()
            );

            if (string.IsNullOrWhiteSpace(anioStr) || !int.TryParse(anioStr, out int anio) || anio < 2000 || anio > 2100)
            {
                XtraMessageBox.Show("Por favor ingrese un año válido (ejemplo: 2025).");
                return;
            }

            // Llamar al presenter
            await _presenter.ExportarProgramasPorMesAsync(mes, anio);
        }
    }
}