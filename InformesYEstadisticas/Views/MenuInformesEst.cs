using DevExpress.XtraEditors;
using InformesYEstadisticas.Presenters;
using InformesYEstadisticas.Views;

namespace InformesYEstadisticas
{
    public partial class MenuInformesEst : DevExpress.XtraEditors.XtraForm, IMenuInformesEstView
    {
        private readonly MenuInformesEstPresenter _presenter;

        public MenuInformesEst(MenuInformesEstPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void bRelevamientoInicial_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarFichaVacia();
        }

        private async void bVerifMensual_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarVerifMensual();
        }

        private async void bControlOpCons_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarReporteConsumos();
        }

        private void bResumenNomMetanol_Click(object sender, EventArgs e)
        {
        }

        private async void bExpoTransoft_Click(object sender, EventArgs e)
        {
            // Solicitar FECHA DESDE
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

            // Solicitar FECHA HASTA
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

            // Llama al presenter para exportar
            await _presenter.ExportarTransoftAsync(desde, hasta);
        }

        private async void bExpoTransoftMetanol_Click(object sender, EventArgs e)
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

            await _presenter.ExportarTransoftMetanolAsync(desde, hasta);
        }

        private async void bAcumulado_Click(object sender, EventArgs e)
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

            await _presenter.ExportarAcumuladoAsync(desde, hasta);
        }
    }
}