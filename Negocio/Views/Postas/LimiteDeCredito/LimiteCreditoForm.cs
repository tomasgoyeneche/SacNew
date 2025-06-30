using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views.Postas.LimiteDeCredito
{
    public partial class LimiteCreditoForm : Form, ICrearEditarCreditoView
    {
        private readonly CrearEditarCreditoPresenter _presenter;

        public LimiteCreditoForm(CrearEditarCreditoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
            _presenter.InicializarAsync();
            // 🔹 Evento para actualizar crédito al cambiar el mes/año
        }

        public int IdEmpresa => (int)cmbEmpresas.SelectedValue;
        public decimal CreditoAsignado => decimal.TryParse(txtCredito.Text, out decimal credito) ? credito : 0;
        public DateTime PeriodoSeleccionado => dtpPeriodo.Value;

        public void CargarEmpresas(List<EmpresaDto> empresas)
        {
            cmbEmpresas.DataSource = empresas;
            cmbEmpresas.DisplayMember = "NombreFantasia";
            cmbEmpresas.ValueMember = "IdEmpresa";

            dtpPeriodo.Value = DateTime.Now;
        }

        public void MostrarCreditoActual(decimal? credito)
        {
            txtCredito.Text = credito?.ToString("N2") ?? "0";
        }

        public void MostrarMensaje(string mensaje)
        {
            MensajeDialog.Show(mensaje);
        }

        public void Close()
        {
            Dispose();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarCreditoAsync();
        }

        private async void dtpPeriodo_ValueChanged(object sender, EventArgs e)
        {
            await _presenter.VerificarCreditoExistente();
        }

        private async void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            await _presenter.VerificarCreditoExistente();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}