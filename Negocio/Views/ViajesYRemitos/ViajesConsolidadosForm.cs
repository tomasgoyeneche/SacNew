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
    }
}