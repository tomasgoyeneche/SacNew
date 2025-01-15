using Core.Services;
using GestionOperativa.Presenters;

namespace GestionOperativa.Views.AdministracionDocumental
{
    public partial class MenuAdministracionDocumental : Form, IMenuAdministracionDocumentalView
    {
        private readonly MenuAdministracionDocumentalPresenter _presenter;
        private readonly INavigationService _navigationService;

        public MenuAdministracionDocumental(MenuAdministracionDocumentalPresenter presenter, INavigationService navigationService)
        {
            _navigationService = navigationService;

            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        private void bAltaEmpresas_Click(object sender, EventArgs e)
        {
            _presenter.CargarMenuEntidad("empresa");
        }

        private void bAltaChofer_Click(object sender, EventArgs e)
        {
            _presenter.CargarMenuEntidad("chofer");
        }
    }
}