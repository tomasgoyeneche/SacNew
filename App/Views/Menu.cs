using App.Presenters;
using App.Views;
using Configuraciones.Views.AbmUsuarios;
using DevExpress.XtraEditors;
using GestionOperativa.Views.AdministracionDocumental;
using InformesYEstadisticas;
using SacNew.Views.Configuraciones.AbmLocaciones;
using SacNew.Views.Configuraciones.AbmUsuarios;
using SacNew.Views.GestionFlota.Postas;

namespace SacNew.Views
{
    public partial class Menu : Form, IMenuView
    {
        private readonly MenuPresenter _presenter;

        public Menu(MenuPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            _presenter.Inicializar();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarDiaDeHoy(string dia)
        {
            lDiaDeHoy.Text = dia;
        }

        public void MostrarNombreUsuario(string nombre)
        {
            txtUserName.Text = nombre;
        }

        private void bMenuPostas_Click(object sender, EventArgs e)
        {
            _presenter.AbrirFormularioConPermisosAsync<MenuPostas>("0003-Postas");
        }

        private void bAbmUsuar_Click(object sender, EventArgs e)
        {
            _presenter.AbrirFormularioConPermisosAsync<MenuUsuarioForm>("0020-AbmUsuarios");
        }

        private void bAbmLocaciones_Click(object sender, EventArgs e)
        {
            _presenter.AbrirFormularioConPermisosAsync<MenuLocaciones>("0021-AbmLocaciones");
        }

        private void bAdminDocumental_Click(object sender, EventArgs e)
        {
            _presenter.AbrirFormularioConPermisosAsync<MenuAdministracionDocumental>("0002-AdministracionDocumental");
        }

        private void bGuardia_Click(object sender, EventArgs e)
        {
            _presenter.AbrirGuardia("0001 - GuardiaBB", 2);
        }

        private void bInformes_Click(object sender, EventArgs e)
        {
            _presenter.AbrirFormularioConPermisosAsync<MenuInformesEst>("0016-Informes");
        }

        private void bNovedadesChoferes_Click(object sender, EventArgs e)
        {
            _presenter.AbrirNovedades("Chofer", "0014-NovedadesChoferes");
        }

        private void bNovedadesUnidades_Click(object sender, EventArgs e)
        {
            _presenter.AbrirNovedades("Unidad", "0015-NovedadesUnidades");
        }

        private void bAlertas_Click(object sender, EventArgs e)
        {
            _presenter.AbrirAlertas("0009-Alertas");
        }

        private void bGuardiaPh_Click(object sender, EventArgs e)
        {
            _presenter.AbrirGuardia("0004-GuardiaPH", 3);
        }

        private void bAdministracionBb_Click(object sender, EventArgs e)
        {
            _presenter.AbrirAdministracion("0005-AdministracionBB", 2);
        }

        private void bAdministracionPh_Click(object sender, EventArgs e)
        {
            _presenter.AbrirAdministracion("0027-AdministracionPH", 3);
        }

        private void bVaporizados_Click(object sender, EventArgs e)
        {
            _presenter.AbrirVaporizados("0011-Vaporizados");
        }

        private void bRuteo_Click(object sender, EventArgs e)
        {
            _presenter.AbrirRuteo("0006-Ruteo");
        }

        private void bDisponibilidad_Click(object sender, EventArgs e)
        {
            _presenter.AbrirDisponibilidad("0007-Disponibilidad");
        }

        private void bCupos_Click(object sender, EventArgs e)
        {
            _presenter.AbrirCupeo("0008-Cupos");
        }

        private void bViajesRemitos_Click(object sender, EventArgs e)
        {
            _presenter.AbrirViajesYRemitos("0010-ViajesYRemitos");
        }
    }
}