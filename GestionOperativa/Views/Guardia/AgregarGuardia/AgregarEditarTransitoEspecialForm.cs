using DevExpress.XtraEditors;
using GestionOperativa.Presenters;

namespace GestionOperativa
{
    public partial class AgregarEditarTransitoEspecialForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarTransitoEspecialView
    {
        public readonly AgregarEditarTransitoEspecialPresenter _presenter;

        public AgregarEditarTransitoEspecialForm(AgregarEditarTransitoEspecialPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
            CargarZonas();
        }

        private void CargarZonas()
        {
            cmbZona.Items.AddRange(new[]
            {
            "Administración", "Danés", "Servipetro", "Mercado Victoria", "Area de descanso", "Estacionamiento"
        });
        }

        public DateTime Fecha { set => lblFechaIngreso.Text = value.ToString("dd/MM/yyyy"); }
        public string Cuit => txtCuit.Text;
        public string RazonSocial => txtTransportista.Text;
        public string Nombre => txtNombre.Text;
        public string Apellido => txtApellido.Text;
        public string Documento => txtDni.Text;
        public DateTime? Licencia => dtpLicencia.EditValue as DateTime?;
        public DateTime? Art => dtpArt.EditValue as DateTime?;
        public DateTime? Seguro => dtpSeguro.EditValue as DateTime?;
        public string Tractor { get => txtTractor.Text; set => txtTractor.Text = value; }
        public string Semi => txtSemi.Text;
        public string Zona => cmbZona.Text;

        public void Close() => Dispose();

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}