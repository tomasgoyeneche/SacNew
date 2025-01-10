using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public partial class IngresaGasoil : Form, IIngresaGasoilView
    {
        public readonly IngresaGasoilPresenter _presenter;

        public IngresaGasoil(IngresaGasoilPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
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
            //cmbTipoGasoil.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmbTipoGasoil.AutoCompleteSource = AutoCompleteSource.ListItems;
            dtpFechaCarga.Value = DateTime.Now;
        }

        public void MostrarLitrosAutorizados(decimal litrosAutorizados, decimal kilometros)
        {
            txtLitrosAutorizados.Text = $"La cantidad de litros autorizados restante es de, {litrosAutorizados.ToString("N2")}Lts para un viaje de {kilometros.ToString("N2")}Km";
        }

        public void MostrarTotalCalculado(decimal total)
        {
            txtTotal.Text = $"{total:C}";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Informacion de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ConfirmarGuardado(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void Cerrar()
        {
            DialogResult = DialogResult.OK;
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

        public void MostrarConsumosAnteriores(List<ConsumoGasoilAutorizadoDto> consumos)
        {
            dataGridViewAnteriores.DataSource = consumos.Select(c => new
            {
                c.NumeroPoc,
                c.NumeroVale,
                c.LitrosAutorizados,
                c.LitrosCargados,
                c.Observaciones,
                c.FechaCarga
            }).ToList();
        }

        public void MostrarConsumosTotales(List<ConsumoGasoilAutorizadoDto> consumos)
        {
            dataGridViewTotales.DataSource = consumos.Select(c => new
            {
                c.NumeroPoc,
                c.NumeroVale,
                c.LitrosAutorizados,
                c.LitrosCargados,
                c.Observaciones,
                c.FechaCarga
            }).ToList();
        }

        public void ActualizarLabelAnterior(decimal restante)
        {
            lblAnterior.Text = $"Restante Anterior: {restante:N2} L";

            if (restante < 0)
            {
                lblAnterior.ForeColor = Color.Brown;
            }
            else if (restante < 1000)
            {
                lblAnterior.ForeColor = Color.Orange;
            }
            else
            {
                lblAnterior.ForeColor = Color.LightGreen;
            }
        }

        public void ActualizarLabelTotal(decimal restante)
        {
            lblTotal.Text = $"Restante Total: {restante:N2} L";
            if (restante < 0)
            {
                lblTotal.ForeColor = Color.Brown;
            }
            else if (restante < 1000)
            {
                lblTotal.ForeColor = Color.Orange;
            }
            else
            {
                lblTotal.ForeColor = Color.LightGreen;
            }
        }

        private void guna2ControlBox6_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}