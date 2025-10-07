using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using Shared.Models;
using System.ComponentModel;

namespace GestionFlota.Views
{
    public partial class ImportarPrograma : DevExpress.XtraEditors.XtraForm, IImportarProgramaView
    {
        public readonly ImportarProgramaPresenter _presenter;

        public ImportarPrograma(ImportarProgramaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public BindingList<PedidoImportacion> ObtenerPedidosImportados()
        {
            var lista = gridControlPrograma.DataSource as BindingList<PedidoImportacion>;
            if (lista == null || lista.Count == 0)
            {
                MostrarMensaje("No hay datos para importar.");
                return new BindingList<PedidoImportacion>();
            }

            foreach (var pedido in lista)
            {
                // Hacer Trim a todos los string excepto Observaciones y NombreCliente
                pedido.Producto = pedido.Producto?.Trim();
                pedido.AlbaranDespacho = pedido.AlbaranDespacho?.Trim();
                pedido.PedidoOR = pedido.PedidoOR?.Trim();
                pedido.NombreCliente = pedido.NombreCliente.Trim();
                pedido.Transporte = pedido.Transporte?.Trim();
                pedido.CodTransporte = pedido.CodTransporte?.Trim();
                pedido.Dni = pedido.Dni?.Trim();
                pedido.Chofer = pedido.Chofer?.Trim();
                pedido.Tractor = pedido.Tractor?.Trim();
                pedido.Semi = pedido.Semi?.Trim();
                if(pedido.Observaciones != null)
                {
                    pedido.Observaciones = pedido.Observaciones.Trim();
                }
            }

            return lista;
        }

        public void MostrarErrores(List<ErrorImportacionDto> errores)
        {
            gridControlErrores.DataSource = errores;
            gridViewErrores.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bImportarPrograma_Click(object sender, EventArgs e)
        {
            if (gridControlPrograma.DataSource == null)
            {
                gridControlPrograma.DataSource = DataHelper.GetData(0);
            }
            gridViewPrograma.PasteFromClipboard();
        }

        private void ImportarPrograma_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            gridControlPrograma.DataSource = DataHelper.GetData(0);
            gridControlErrores.DataSource = null;
            gridViewPrograma.OptionsSelection.MultiSelect = true;
            gridViewPrograma.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
        }

        private async void bRevisar_Click(object sender, EventArgs e)
        {
            await _presenter.RevisarAsync();
        }

        public void HabilitarGuardar(bool habilitar)
        {
            btnGuardar.Enabled = habilitar;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
            LimpiarPedidos();
        }

        private async void gridControlErrores_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewErrores.GetFocusedRow() is ErrorImportacionDto error)
            {
                await _presenter.GestionarErrorDeSinonimoAsync(error);
            }
        }

        public void LimpiarPedidos()
        {
            gridControlPrograma.DataSource = new BindingList<PedidoImportacion>();
        }
    }
}