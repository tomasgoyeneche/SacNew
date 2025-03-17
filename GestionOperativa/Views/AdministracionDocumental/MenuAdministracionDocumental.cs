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

        private async void bAltaEmpresas_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("empresa");
        }

        private async void bAltaChofer_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("chofer");
        }

        private async void bTractores_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("tractor");
        }

        private async void bSemi_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("semi");
        }

        private async Task CargarEntidadAsync(string entidad)
        {
            await _presenter.CargarMenuEntidad(entidad);
        }

        private async void bFotosChoferes_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarFotosChoferesAsync();
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

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarRelevamiento(int cantidad)
        {
            LRegistrosContabilizados.Visible = true;
            LRegistrosContabilizados.Text = $"Relevado: {cantidad}";
        }

        private async void bPdfUnidades_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarDocumentosUnidadesAsync();

        }
    }
}