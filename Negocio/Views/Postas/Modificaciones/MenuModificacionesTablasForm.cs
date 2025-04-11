using Core.Services;
using GestionFlota.Views.Postas.Modificaciones.ReabrirPoc;

namespace GestionFlota.Views.Postas.Modificaciones
{
    public partial class MenuModificacionesTablasForm : Form
    {
        private readonly INavigationService _navigationService;

        public MenuModificacionesTablasForm(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
        }

        private void bReabrirPoc_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ReabrirPocForm>();
        }
    }
}