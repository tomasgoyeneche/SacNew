using GestionFlota.Presenters.IngresarConsumos;
using Shared.Models;

namespace GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public partial class OtrosConsumosForm : Form, IOtrosConsumosView
    {
        public readonly IngresaOtrosConsumosPresenter _presenter;

        public OtrosConsumosForm(IngresaOtrosConsumosPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        public Concepto TipoConsumoSeleccionado => cmbTipoConsumo.SelectedItem as Concepto;
        public string RemitoExterno => txtRemitoExterno.Text.Trim();
        public DateTime FechaRemito => dtpFechaRemito.Value;
        public decimal? Cantidad => decimal.TryParse(txtCantidad.Text, out var result) ? result : null;
        public string Aclaraciones => txtAclaracion.Text.Trim();

        public bool Dolar => dolarCheck.Checked;

        public void CargarTiposConsumo(List<Concepto> tiposConsumo)
        {
            cmbTipoConsumo.DataSource = tiposConsumo;
            cmbTipoConsumo.DisplayMember = "Descripcion";
            cmbTipoConsumo.ValueMember = "IdConsumo";

            dtpFechaRemito.Value = DateTime.Now;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void Cerrar()
        {
            Dispose();
        }

        public void MostrarTotalCalculado(decimal total)
        {
            txtImporteTotal.Text = $"{total:C}";
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (Cantidad.HasValue)
            {
                _presenter.CalcularTotal(Cantidad.Value);
            }
            else
            {
                MostrarMensaje("Debe ingresar un valor válido para los litros.");
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumoAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        public void InicializarParaEdicion(ConsumoOtros consumo)
        {
            cmbTipoConsumo.SelectedValue = consumo.IdConsumo;
            txtRemitoExterno.Text = consumo.NumeroVale;
            dtpFechaRemito.Value = consumo.FechaRemito;
            txtCantidad.Text = consumo.Cantidad?.ToString("N2") ?? "0.00";
            txtImporteTotal.Text = $"{consumo.ImporteTotal:C}";
            txtAclaracion.Text = consumo.Aclaracion;
            dolarCheck.Checked = consumo.Dolar;
        }
    }
}