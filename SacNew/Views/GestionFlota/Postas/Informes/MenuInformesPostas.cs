using SacNew.Services;

namespace SacNew.Views.GestionFlota.Postas.Informes
{
    public partial class MenuInformesPostas : Form
    {
        private readonly INavigationService _navigationService;

        public MenuInformesPostas(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
        }

        private void bInformeUnidad_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ConsumoPorUnidad>();
        }
    }
}