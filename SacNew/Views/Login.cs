using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views;
using System;
using System.Data.SqlClient;

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

        //public void SetPresenter(LoginPresenter presenter)
        //{
        //    _presenter = presenter;
        //}
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
