using Core.Services;
using GestionOperativa.Views.AdministracionDocumental;
using SacNew.Views.Configuraciones.AbmLocaciones;
using SacNew.Views.Configuraciones.AbmUsuarios;
using SacNew.Views.GestionFlota.Postas;

namespace SacNew.Views
{
    public partial class Menu : Form
    {
        private readonly ISesionService _sesionService;
        private readonly INavigationService _navigationService;

        public Menu(ISesionService sesionService, INavigationService navigationService)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _navigationService = navigationService;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            txtUserName.Text = $"{_sesionService.NombreCompleto}";
            lDiaDeHoy.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        private void bMenuPostas_Click(object sender, EventArgs e)
        {

            if (_sesionService.Permisos.Contains(2) && !_sesionService.Permisos.Contains(3))
            {
                _navigationService.ShowDialog<MenuPostas>();
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a las Postas.", "Permiso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bAbmUsuar_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuUsuariosForm>();

        }

        private void bAbmLocaciones_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuLocaciones>();
        }

        private void bAdminDocumental_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuAdministracionDocumental>();
        }
    }
}