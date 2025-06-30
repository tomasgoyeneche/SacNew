using Core.Services;
using GestionFlota.Presenters.Informes;
using GestionFlota.Views.Postas.Informes;
using GestionFlota.Views.Postas.Informes.ConsumoUnidad;
using GestionFlota.Views.Postas.Modificaciones.ConsultarConsumos;

namespace SacNew.Views.GestionFlota.Postas.Informes
{
    public partial class MenuInformesPostas : Form, IMenuInformesView
    {
        private readonly INavigationService _navigationService;
        private readonly MenuInformesPresenter _presenter;

        public MenuInformesPostas(MenuInformesPresenter presenter, INavigationService navigationService)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            _navigationService = navigationService;
            InitializeComponent();
        }

        private void bInformeUnidad_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ConsumoPorUnidadForm>();
        }

        private void bConsultarConsumos_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<FiltrarConsumosForm>();
        }

        private async void bCierreDiario_Click(object sender, EventArgs e)
        {
            string FechaString = Microsoft.VisualBasic.Interaction.InputBox("Por favor ingrese la fecha (formato: dd/mm/yyyy):", "Ingrese Fecha", "");

            if (string.IsNullOrEmpty(FechaString) || !DateTime.TryParse(FechaString, out DateTime desdeFecha))
            {
                MessageBox.Show("Por favor ingrese una fecha válida para 'Fecha'.");
                return;
            }

            await _presenter.BuscarConsumosAsync(desdeFecha);
        }
    }
}