using Core.Services;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class MenuFormaIngresarConsumos : Form
    {
        private readonly INavigationService _navigationService;

        public MenuFormaIngresarConsumos(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private void bIngresaDatosMv_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuIngresaConsumos>();
        }

        private void bImportaDatosMv_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ImportaDatosMv>();
        }
    }
}