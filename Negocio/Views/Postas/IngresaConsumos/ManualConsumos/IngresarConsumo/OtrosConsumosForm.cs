using DevExpress.XtraEditors.Controls;
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

        public decimal? PrecioManual =>
    decimal.TryParse(txtImporteTotal.Text, out var p) ? p : null;
        public int TipoConsumoSeleccionado => Convert.ToInt32(cmbTipoConsumo.EditValue);
        public string RemitoExterno => txtRemitoExterno.Text.Trim();
        public DateTime FechaRemito => dtpFechaRemito.Value;
        public decimal? Cantidad => decimal.TryParse(txtCantidad.Text, out var result) ? result : null;
        public string Aclaraciones => txtAclaracion.Text.Trim();

        public bool Dolar => dolarCheck.Checked;

        public void CargarTiposConsumo(List<Concepto> tiposConsumo, string poc)
        {
            cmbTipoConsumo.Properties.DataSource = tiposConsumo;
            cmbTipoConsumo.Properties.DisplayMember = "Descripcion";
            cmbTipoConsumo.Properties.ValueMember = "IdConsumo";

            cmbTipoConsumo.Properties.Columns.Clear();
            cmbTipoConsumo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Consumo"));
            cmbTipoConsumo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Proveedor"));

            cmbTipoConsumo.EditValue = -1;
            dtpFechaRemito.Value = DateTime.Now;
            labelPoc.Text = poc;
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
            cmbTipoConsumo.EditValue = consumo.IdConsumo;
            txtRemitoExterno.Text = consumo.NumeroVale;
            dtpFechaRemito.Value = consumo.FechaRemito;
            txtCantidad.Text = consumo.Cantidad?.ToString("N2") ?? "0.00";
            txtImporteTotal.Text = $"{consumo.ImporteTotal}";
            txtAclaracion.Text = consumo.Aclaracion;
            dolarCheck.Checked = consumo.Dolar;
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (Cantidad.HasValue)
            {
                _presenter.CalcularTotal(Cantidad.Value);
            }
        }

        private void cmbTipoConsumo_EditValueChanged(object sender, EventArgs e)
        {
            var concepto = cmbTipoConsumo.GetSelectedDataRow() as Concepto;
            if (concepto == null)
                return;

            if (concepto.IdConsumoTipo == 3)
            {
                txtImporteTotal.Enabled = false;
                txtImporteTotal.Text = concepto.PrecioActual.ToString("N2");

                if (Cantidad.HasValue)
                    _presenter.CalcularTotal(Cantidad.Value); // Usa el precio del concepto
            }
            else
            {
                txtImporteTotal.Enabled = true;
                txtImporteTotal.Text = "";   // limpiar total hasta que el usuario ingrese precio
            }
        }
    }
}