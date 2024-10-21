using Microsoft.Extensions.DependencyInjection;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos;

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
            var menuAbmPostas = _serviceProvider.GetService<MenuAbmPostas>();
            if (menuAbmPostas != null)
            {
                menuAbmPostas.ShowDialog();
            }
            else
            {
                // Handle the case where menuAbmPostas is null
                MessageBox.Show("No se pudo cargar el menú ABM de postas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bMenuConceptos_Click(object sender, EventArgs e)
        {
            using (var MenuConceptos = _serviceProvider.GetService<MenuConceptos>())
            {
                if (MenuConceptos != null)
                {
                    MenuConceptos.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"No se pudo cargar el menú de Conceptos.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bIngresaConsumos_Click(object sender, EventArgs e)
        {
            using (var menuConsumos = _serviceProvider.GetService<MenuIngresaConsumos>())
            {
                if (menuConsumos != null)
                {
                    menuConsumos.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"No se pudo cargar el menú de Consumos.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bIngresaConsumosYPF_Click(object sender, EventArgs e)
        {
            using (var menuImportaConsumosYpf = _serviceProvider.GetService<ImportarConsumosYPF>())
            {
                if (menuImportaConsumosYpf != null)
                {
                    menuImportaConsumosYpf.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"No se pudo cargar el menú de consumos YPF.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}