using DevExpress.XtraReports.UI;

namespace Configuraciones.Views
{
    public partial class VisualizadorReportesDevForm : DevExpress.XtraEditors.XtraForm
    {
        public VisualizadorReportesDevForm()
        {
            InitializeComponent();
        }

        public void MostrarReporteDevExpress(XtraReport reporte)
        {
            this.WindowState = FormWindowState.Maximized;

            documentViewer1.ShowPageMargins = false;
            documentViewer1.DocumentSource = reporte;
            bbiZoom.EditValue = 82;

            reporte.CreateDocument();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (documentViewer1.DocumentSource is XtraReport reporte)
            {
                // Asegurarse de generar el documento si no fue generado todavía
                reporte.CreateDocument();

                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.Filter = "Archivo PDF|*.pdf";
                    save.Title = "Guardar reporte como PDF";
                    save.FileName = "Reporte.pdf";

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        reporte.ExportToPdf(save.FileName);
                        MessageBox.Show("Reporte exportado correctamente.", "Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}