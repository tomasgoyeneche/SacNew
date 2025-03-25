using Core.Services;
using GestionFlota.Views.Postas.LimiteDeCredito;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.DatosVolvo;
using SacNew.Views.GestionFlota.Postas.Informes;
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
            this.Hide();
            _navigationService.ShowDialog<MenuAbmPostas>();
            this.Show();
        }

        private void bMenuConceptos_Click(object sender, EventArgs e)
        {
            this.Hide();
            _navigationService.ShowDialog<MenuConceptos>();
            this.Show();
        }

        private void bIngresaConsumos_Click(object sender, EventArgs e)
        {
            this.Hide();
            _navigationService.ShowDialog<MenuFormaIngresarConsumos>();
            this.Show();
        }

        private void bIngresaConsumosYPF_Click(object sender, EventArgs e)
        {
            this.Hide();
            _navigationService.ShowDialog<ImportarConsumosYPF>();
            this.Show();
        }

        private void bDatosVolvo_Click(object sender, EventArgs e)
        {
            this.Hide();
            _navigationService.ShowDialog<ImportDatosVolvo>();
            this.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            _navigationService.ShowDialog<MenuInformesPostas>();
            this.Show();
        }

        private void bLimiteCredito_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<LimiteCreditoForm>();
        }
    }
}