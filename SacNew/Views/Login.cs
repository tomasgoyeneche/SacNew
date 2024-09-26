using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Views;
using System.Data.SqlClient;

namespace SacNew
{
    public partial class Login : Form, ILoginView
    {
        private LoginPresenter _presenter;
        public Login()
        {
            InitializeComponent();
         
        }

        public void SetPresenter(LoginPresenter presenter)
        {
            _presenter = presenter;
        }
        public string NombreUsuario => tLogUsu.Text;
        public string Contrasena => tLogPass.Text;
        private void Login_Load(object sender, EventArgs e)
        {

        }
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

        private void loginCloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
