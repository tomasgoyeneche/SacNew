using Microsoft.Reporting.WinForms;
using System.IO;

namespace Core.Services
{
    public class ReportService : IReportService
    {
        private readonly string _reportsDirectory;

        public ReportService()
        {
            // Determinar la ruta base de los reportes relativa al ejecutable
            _reportsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
        }

        public LocalReport CrearReporte(string reportName, Dictionary<string, object> dataSources)
        {
            // Ruta del reporte
            var reportPath = Path.Combine(_reportsDirectory, $"{reportName}.rdlc");
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"El reporte no existe: {reportPath}");
            }

            var localReport = new LocalReport
            {
                ReportPath = reportPath
            };

            // Configurar los data sources dinámicamente
            foreach (var dataSource in dataSources)
            {
                if (dataSource.Value == null)
                    continue;

                localReport.DataSources.Add(new ReportDataSource(dataSource.Key, dataSource.Value));
            }

            return localReport;
        }
    }
}