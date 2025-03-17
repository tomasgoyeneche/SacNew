using Microsoft.Reporting.WinForms;

namespace App.Views
{
    public partial class VisualizadorReportesForm : Form
    {
        public VisualizadorReportesForm()
        {
            InitializeComponent();
        }

        public async Task MostrarReporte(LocalReport report)
        {
            // Asegúrate de maximizar la ventana cada vez
            this.WindowState = FormWindowState.Maximized;

            // Configurar el modo de impresión y ocultar la barra de herramientas
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ShowToolBar = true;

            // Limpiar el ReportViewer antes de asignar un nuevo reporte
            reportViewer.Reset();
            // Asignar el nuevo reporte
            reportViewer.LocalReport.ReportPath = report.ReportPath;
            reportViewer.LocalReport.DataSources.Clear();

            foreach (var dataSource in report.DataSources)
            {
                reportViewer.LocalReport.DataSources.Add(dataSource);
            }

            // Refrescar el reporte
            reportViewer.RefreshReport();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}