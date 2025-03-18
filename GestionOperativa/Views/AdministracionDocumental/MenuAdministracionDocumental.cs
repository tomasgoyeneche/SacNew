using Core.Services;
using GestionOperativa.Presenters;

namespace GestionOperativa.Views.AdministracionDocumental
{
    public partial class MenuAdministracionDocumental : Form, IMenuAdministracionDocumentalView
    {
        private readonly INavigationService _navigationService;
        private readonly MenuAdministracionDocumentalPresenter _presenter;

        public MenuAdministracionDocumental(MenuAdministracionDocumentalPresenter presenter, INavigationService navigationService)
        {
            _navigationService = navigationService;

            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
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

        public void MostrarRelevamientoVencimientos(int cantidad)
        {
            lRegistrosVencimientos.Visible = true; lRegistrosVencimientos.Text = $"Relevado: {cantidad}";
        }

        private async void bAltaChofer_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("chofer");
        }

        private async void bAltaEmpresas_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("empresa");
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

        private async void bPdfUnidades_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarDocumentosUnidadesAsync();
        }

        private async void bSemi_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("semi");
        }

        private async void bTractores_Click(object sender, EventArgs e)
        {
            await CargarEntidadAsync("tractor");
        }

        private async void bUnidadesEnNominaFotos_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarFotosUnidadesAsync();
        }

        private async void bVencimientosChoferes_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarVencimientosChoferesAsync();
        }

        private async void bVencimientosSemi_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarVencimientosSemisAsync();
        }

        private async void bVencimientosTractor_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarVencimientosTractoresAsync();
        }

        private async void bVencimientosUnidades_Click(object sender, EventArgs e)
        {
            await _presenter.VerificarVencimientosUnidadesAsync();
        }

        private async Task CargarEntidadAsync(string entidad)
        {
            await _presenter.CargarMenuEntidad(entidad);
        }
    }
}