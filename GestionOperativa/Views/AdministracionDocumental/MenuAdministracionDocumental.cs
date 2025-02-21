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

        private async void bFotosChoferes_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarFotosChoferesAsync();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarRelevamiento(int cantidad)
        {
            LRegistrosContabilizados.Visible = true;
            LRegistrosContabilizados.Text = $"Relevado: {cantidad}";
        }

        private async void bPdfChoferes_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarLegajosChoferesAsync();
        }

        private async void bPdfEmpresa_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarLegajosEmpresasAsync();
        }

        private async void bUnidadesEnNominaFotos_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarFotosUnidadesAsync();
        }

        private void bPdfUnidades_Click(object sender, EventArgs e)
        {
        }
    }
}