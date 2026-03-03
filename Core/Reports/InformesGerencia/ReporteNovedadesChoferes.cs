using DevExpress.XtraReports.UI;
using System.ComponentModel;

namespace Core.Reports
{
    public partial class ReporteNovedadesChoferes : DevExpress.XtraReports.UI.XtraReport
    {
        public ReporteNovedadesChoferes()
        {
            InitializeComponent();
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            XRSubreport sub = (XRSubreport)sender;

            // Asignar DataSource SOLO acá
            sub.ReportSource.DataSource = this.DataSource;

            // Obtener valores del reporte padre
            //int diasPeriodo = Convert.ToInt32(this.Parameters["pDiasPeriodo"].Value);
            //int totalChoferes = Convert.ToInt32(this.Parameters["pTotalChoferes"].Value);

            //// Calcular
            //int total = diasPeriodo * totalChoferes;

            // Pasar parámetro al subreporte
            sub.ReportSource.Parameters["pTotalDiasConTodo"].Value =
                this.Parameters["pTotalDiasPeriodo"].Value;

            // Pasar el parámetro explícitamente (por las dudas)
            sub.ReportSource.Parameters["pTotalDias"].Value =
                this.Parameters["pTotalDias"].Value;
        }

        //private void xrChart1_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    var chart = (DevExpress.XtraReports.UI.XRChart)sender;
        //    var series = chart.Series[0]; // asegurate de tener 1 serie creada
        //    series.Points.Clear();

        //    decimal diasPeriodo = Convert.ToDecimal(Parameters["pDiasPeriodo"].Value);
        //    decimal totalChoferes = Convert.ToDecimal(Parameters["pTotalChoferes"].Value);
        //    decimal totalDias = Convert.ToDecimal(Parameters["pTotalDias"].Value);

        //    decimal capacidad = diasPeriodo * totalChoferes;
        //    decimal trabajados = capacidad - totalDias;
        //    decimal noTrabajados = totalDias;

        //    series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Trabajados", trabajados));
        //    series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("No trabajados", noTrabajados));

        //}
    }
}