using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc
{
    public partial class AgregarEditarPoc : Form, IAgregarEditarPOCView
    {
        public readonly AgregarEditarPOCPresenter _presenter;

        public AgregarEditarPoc(AgregarEditarPOCPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdNomina => Convert.ToInt32(cmbNomina.SelectedValue);
        public int IdPosta => Convert.ToInt32(cmbPosta.SelectedValue);
        public string NumeroPOC => txtNumeroPOC.Text.Trim();
        public double Odometro => string.IsNullOrEmpty(txtOdometro.Text) ? 0 : Convert.ToDouble(txtOdometro.Text.Trim());
        public string Comentario => txtComentario.Text.Trim();
        public DateTime FechaCreacion => dtpFechaCreacion.Value;
        public int IdUsuario => _presenter.IdUsuario;

        public void CargarNominas(List<Nomina> nominas)
        {
            cmbNomina.DataSource = nominas;
            cmbNomina.DisplayMember = "DescripcionNomina";
            cmbNomina.ValueMember = "IdNomina";

            cmbNomina.SelectedValue = _presenter.PocActual?.IdNomina ?? -1;
        }

        public void CargarPostas(List<Posta> postas)
        {
            cmbPosta.DataSource = postas;
            cmbPosta.DisplayMember = "Descripcion";
            cmbPosta.ValueMember = "Id";
            cmbPosta.SelectedValue = _presenter.PocActual?.IdPosta ?? -1;
        }

        public void MostrarDatosPOC(POC poc)
        {
            txtNumeroPOC.Text = poc.NumeroPOC;
            txtOdometro.Text = poc.Odometro.ToString();
            txtComentario.Text = poc.Comentario;
            cmbNomina.SelectedValue = poc.IdNomina;
            cmbPosta.SelectedValue = poc.IdPosta;
            dtpFechaCreacion.Value = poc.FechaCreacion;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await ManejarErroresAsync(async () =>
            {
                await _presenter.GuardarPOCAsync();
                Close();
            });
        }

        private async void AgregarEditarPOC_Load(object sender, EventArgs e)
        {
            await ManejarErroresAsync(async () =>
            {
                await _presenter.InicializarAsync();
            });

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtOdometro_TextChanged(object sender, EventArgs e)
        {
            txtOdometro.TextChanged -= txtOdometro_TextChanged;
            if (txtOdometro.Text.Contains('.'))
            {
                int selectionStart = txtOdometro.SelectionStart;
                txtOdometro.Text = txtOdometro.Text.Replace('.', ',');
                txtOdometro.SelectionStart = selectionStart;
            }

            txtOdometro.TextChanged += txtOdometro_TextChanged;
        }
        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async Task ManejarErroresAsync(Func<Task> accion)
        {
            try
            {
                await accion();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}