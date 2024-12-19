using SacNew.Models;
using SacNew.Presenters.AbmUsuarios;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public partial class AgregarEditarUsuarioForm : Form, IAgregarEditarUsuarioView
    {
        public AgregarEditarUsuarioPresenter _presenter;

        public AgregarEditarUsuarioForm(AgregarEditarUsuarioPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string Nombre => txtNombreCompleto.Text.Trim();
        public string Usuario => txtUsuario.Text.Trim();

        public string Contrasena => txtContrasena.Text.Trim();

        public int Posta => Convert.ToInt32(cmbPostas.SelectedValue);

        public void MostrarDatosUsuario(Usuario usuario)
        {
            txtNombreCompleto.Text = usuario.NombreCompleto;
            txtUsuario.Text = usuario.NombreUsuario;
            cmbPostas.SelectedValue = usuario.idPosta;
        }

        public void CargarPostas(List<Posta> postas)
        {
            cmbPostas.DataSource = postas;
            cmbPostas.DisplayMember = "Descripcion";
            cmbPostas.ValueMember = "IdPosta";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarUsuarioAsync();
            this.Dispose();
        }
    }
}