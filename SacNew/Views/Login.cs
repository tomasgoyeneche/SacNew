using SacNew.Interfaces;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views;

namespace SacNew
{
    public partial class Login : Form, ILoginView
    {
        private readonly LoginPresenter _presenter;

        public Login(IUsuarioRepositorio usuarioRepositorio, IPermisoRepositorio permisoRepositorio, ISesionService sesionService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _presenter = new LoginPresenter(this, usuarioRepositorio, permisoRepositorio, sesionService, serviceProvider);
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

        private void bIniciarSesion_Click(object sender, EventArgs e)
        {
            _presenter.AutenticarUsuario();
        }
    }
}