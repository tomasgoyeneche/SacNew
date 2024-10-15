using Microsoft.Extensions.DependencyInjection;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos;

namespace SacNew.Views.GestionFlota.Postas
{
    public partial class MenuPostas : Form
    {
        private readonly ISesionService _sesionService;
        private readonly IServiceProvider _serviceProvider;

        public MenuPostas(ISesionService sesionService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;
        }

        private void MenuPostas_Load(object sender, EventArgs e)
        {
            lMenuPostasNombre.Text = _sesionService.NombreCompleto;
        }

        private void bAbmPostasMenu_Click(object sender, EventArgs e)
        {
            using (var MenuAbmPostas = _serviceProvider.GetService<MenuAbmPostas>())
            {
                this.Hide();
                MenuAbmPostas.ShowDialog();
                this.Show();
            }
        }

        private void bMenuConceptos_Click(object sender, EventArgs e)
        {
            var MenuConceptos = _serviceProvider.GetService<MenuConceptos>();
            if (MenuConceptos != null)
            {
                this.Hide();
                MenuConceptos.ShowDialog();
                this.Show();
            }
            else
            {
                // Handle the case where MenuConceptos is null
                MessageBox.Show("No se pudo cargar el menú de conceptos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bIngresaConsumos_Click(object sender, EventArgs e)
        {
            using (var menuConsumos = _serviceProvider.GetService<MenuIngresaConsumos>())
            {
                this.Hide();
                menuConsumos.ShowDialog();
                this.Show();
            }
        }

       
    }
}