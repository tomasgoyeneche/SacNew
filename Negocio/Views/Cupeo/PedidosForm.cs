using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class PedidosForm : DevExpress.XtraEditors.XtraForm, IPedidosView
    {
        public readonly PedidosPresenter _presenter;

        public PedidosForm(PedidosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void CargarPedidos(List<PedidoDto> pedidos)
        {
            gridControlProg.DataSource = pedidos;

            var view = gridViewProg;

            view.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void gridViewProg_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0 && gridViewProg.GetFocusedRow() is PedidoDto pedido) // Es doble click sobre una celda válida
            {
                await _presenter.EliminarPedido(pedido.IdPedido);
            }
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}