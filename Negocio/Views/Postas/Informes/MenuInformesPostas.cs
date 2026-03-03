using Core.Services;
using DevExpress.XtraEditors;
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

        private async void bExportaTransoft_Click(object sender, EventArgs e)
        {
            string fechaDesdeStr = DevExpress.XtraEditors.XtraInputBox.Show(
               "Por favor ingrese la fecha DESDE (formato: dd/MM/yyyy):",
               "Fecha Desde",
               DateTime.Today.ToString("dd/MM/yyyy")
           );

            if (string.IsNullOrWhiteSpace(fechaDesdeStr) || !DateTime.TryParse(fechaDesdeStr, out DateTime desde))
            {
                XtraMessageBox.Show("Por favor ingrese una fecha válida para 'Desde'.");
                return;
            }

            string fechaHastaStr = DevExpress.XtraEditors.XtraInputBox.Show(
                "Por favor ingrese la fecha HASTA (formato: dd/MM/yyyy):",
                "Fecha Hasta",
                DateTime.Today.ToString("dd/MM/yyyy")
            );

            if (string.IsNullOrWhiteSpace(fechaHastaStr) || !DateTime.TryParse(fechaHastaStr, out DateTime hasta))
            {
                XtraMessageBox.Show("Por favor ingrese una fecha válida para 'Hasta'.");
                return;
            }

            if (hasta < desde)
            {
                XtraMessageBox.Show("'Hasta' debe ser igual o mayor a 'Desde'.");
                return;
            }

            await _presenter.ExportaTransoftConsumos(desde, hasta);
        }
    }
}