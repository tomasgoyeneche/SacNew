using GestionOperativa.Presenters.AdministracionDocumental;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class MenuAltaNominaForm : Form, IMenuAltaNominaView
    {
        public MenuAltaNominaPresenter _presenter;

        public MenuAltaNominaForm(MenuAltaNominaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void bNominaMetanol_Click(object sender, EventArgs e)
        {
            _presenter.GenerarReporteFlotaAsync();
        }
    }
}