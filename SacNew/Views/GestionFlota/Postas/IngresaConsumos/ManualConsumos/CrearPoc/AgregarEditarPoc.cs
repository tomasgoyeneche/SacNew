using SacNew.Models;
using SacNew.Models.DTOs;
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

        public int IdUnidad => Convert.ToInt32(cmbNomina.SelectedValue);
        public int IdChofer => Convert.ToInt32(cmbChofer.SelectedValue);
        public int IdPeriodo => Convert.ToInt32(cmbPeriodo.SelectedValue);
        public string NumeroPOC => txtNumeroPOC.Text.Trim();
        public double Odometro => string.IsNullOrEmpty(txtOdometro.Text) ? 0 : Convert.ToDouble(txtOdometro.Text.Trim());
        public string Comentario => txtComentario.Text.Trim();
        public DateTime FechaCreacion => dtpFechaCreacion.Value;
        public int IdUsuario => _presenter.IdUsuario;

        public void CargarNominas(List<UnidadPatenteDto> unidades)
        {
            cmbNomina.DataSource = unidades;
            cmbNomina.DisplayMember = "DescripcionUnidad";
            cmbNomina.ValueMember = "IdUnidad";

            cmbNomina.SelectedValue = _presenter.PocActual?.IdUnidad ?? -1;
        }

        public void CargarChoferes(List<chofer> choferes)
        {
            cmbChofer.DataSource = choferes;
            cmbChofer.DisplayMember = "NombreApellido";
            cmbChofer.ValueMember = "IdChofer";

            cmbChofer.SelectedValue = _presenter.PocActual?.IdChofer ?? -1;
        }

        public void CargarPeriodo(List<Periodo> periodos)
        {
            cmbPeriodo.DataSource = periodos;
            cmbPeriodo.DisplayMember = "NombrePeriodo";
            cmbPeriodo.ValueMember = "idPeriodo";
            cmbPeriodo.SelectedValue = _presenter.PocActual?.IdPeriodo ?? -1;
        }

        public void MostrarDatosPOC(POC poc)
        {
            txtNumeroPOC.Text = poc.NumeroPoc;
            txtOdometro.Text = poc.Odometro.ToString();
            txtComentario.Text = poc.Comentario;
            cmbNomina.SelectedValue = poc.IdUnidad;
            cmbPeriodo.SelectedValue = poc.IdPeriodo;
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