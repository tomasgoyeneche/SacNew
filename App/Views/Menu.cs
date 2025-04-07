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
            if (_sesionService.Permisos.Contains("0003-Postas") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
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
            if (_sesionService.Permisos.Contains("0020-AbmUsuarios") || _sesionService.Permisos.Contains("0000 -SuperAdmin"))
            {
                _navigationService.ShowDialog<MenuUsuariosForm>();
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a la Administración de Usuarios.", "Permiso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bAbmLocaciones_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0021-AbmLocaciones") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                _navigationService.ShowDialog<MenuLocaciones>();
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a la Administración de Locaciones.", "Permiso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bAdminDocumental_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0002-AdministracionDocumental") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                _navigationService.ShowDialog<MenuAdministracionDocumental>();
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a la Administración Documental.", "Permiso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bGuardia_Click(object sender, EventArgs e)
        {
        }
    }
}