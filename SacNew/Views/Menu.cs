using Microsoft.Extensions.DependencyInjection;
using SacNew.Services;
using SacNew.Views.Configuraciones.AbmLocaciones;
using SacNew.Views.GestionFlota.Postas;

namespace SacNew.Views
{
    public partial class Menu : Form
    {
        private readonly ISesionService _sesionService;
        private readonly IServiceProvider _serviceProvider;

        public Menu(ISesionService sesionService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            txtUserName.Text = $"{_sesionService.NombreCompleto}";
            lDiaDeHoy.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        private void picBoxFlotaPostas_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains(2) && !_sesionService.Permisos.Contains(3))
            {
                using (var postasMenu = _serviceProvider.GetService<MenuPostas>())
                {
                
                    postasMenu.ShowDialog();
                    
                }
            }
            else
            {
                MessageBox.Show(@"No tienes permisos para acceder a las Postas.", @"Permiso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void picBoxMenuAbmLocacion_Click(object sender, EventArgs e)
        {
            using (var menuLocaciones = _serviceProvider.GetService<MenuLocaciones>())
            {
                menuLocaciones.ShowDialog();
            }
        }
    }
}