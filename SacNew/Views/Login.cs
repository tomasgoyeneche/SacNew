using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views;

namespace SacNew
{
    public partial class Login : Form, ILoginView
    {
        private readonly LoginPresenter _presenter;

        public Login(LoginPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string NombreUsuario => tLogUsu.Text;

        public string Contrasena => tLogPass.Text;

        public void MostrarMensaje(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        public void RedirigirAlMenu(Menu menuForm)
        {
            this.Hide();
            menuForm.ShowDialog();
            Environment.Exit(0);
        }

        private async void bIniciarSesion_Click(object sender, EventArgs e)
        {
            await _presenter.AutenticarUsuarioAsync();
        }
    }
}