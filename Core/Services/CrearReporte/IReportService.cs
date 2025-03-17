using Microsoft.Reporting.WinForms;

namespace Core.Services
{
    public interface IReportService
    {
        LocalReport CrearReporte(string reportName, Dictionary<string, object> dataSources);
    }
}