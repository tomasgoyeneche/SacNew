using SacNew.Models;
using SacNew.Presenters;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public partial class IngresaGasoil : Form, IIngresaGasoilView
    {
        public readonly IngresaGasoilPresenter _presenter;
        public IngresaGasoil(IngresaGasoilPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        public Concepto TipoGasoilSeleccionado => cmbTipoGasoil.SelectedItem as Concepto;
        public decimal? Litros => decimal.TryParse(txtLitros.Text, out var result) ? result : null;
        public string NumeroVale => txtNumeroVale.Text.Trim();
        public string Observaciones => txtObservaciones.Text.Trim();
        public DateTime FechaCarga => dtpFechaCarga.Value;
        public void CargarTiposGasoil(List<Concepto> tiposGasoil)
        {
            cmbTipoGasoil.DataSource = tiposGasoil;
            cmbTipoGasoil.DisplayMember = "Descripcion";
            cmbTipoGasoil.ValueMember = "IdConsumo";
        }

        public void MostrarTotalCalculado(decimal total)
        {
            txtTotal.Text = $"{total:C}";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void Cerrar()
        {
            Dispose();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (Litros.HasValue)
            {
                _presenter.CalcularTotal(Litros.Value);
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

    }
}