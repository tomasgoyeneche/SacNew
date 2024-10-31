using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.DatosVolvo;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos;

namespace SacNew.Views.GestionFlota.Postas
{
    public partial class MenuPostas : Form
    {
        private readonly ISesionService _sesionService;
        private readonly INavigationService _navigationService;

        public MenuPostas(ISesionService sesionService, INavigationService navigationService)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _navigationService = navigationService;
        }

        private void MenuPostas_Load(object sender, EventArgs e)
        {
            lMenuPostasNombre.Text = _sesionService.NombreCompleto;
        }

        private void bAbmPostasMenu_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuAbmPostas>();
        }

        private void bMenuConceptos_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuConceptos>();
        }

        private void bIngresaConsumos_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuIngresaConsumos>();
        }

        private void bIngresaConsumosYPF_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ImportarConsumosYPF>();
        }

        private void bDatosVolvo_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ImportDatosVolvo>();
        }
    }
}