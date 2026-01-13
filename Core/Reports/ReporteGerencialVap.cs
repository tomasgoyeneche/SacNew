using DevExpress.XtraReports.UI;
using Shared.Models;
using System.ComponentModel;

namespace Core.Reports
{
    public partial class ReporteGerencialVap : DevExpress.XtraReports.UI.XtraReport
    {
        public ReporteGerencialVap()
        {
            InitializeComponent();
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            var sub = (XRSubreport)sender;

            var dataPadre = (IEnumerable<VaporizadoDto>)this.DataSource;

            // Total general (sin filtrar)
            int totalGeneral = dataPadre.Count();

            // Filtrados TransitoEspecial == "No"
            var dataNo = dataPadre
                .Where(x => x.TransitoEspecial == "No")
                .ToList();

            int totalNo = dataNo.Count;

            // Armo el datasource del subreporte
            var resumen = dataNo
                .GroupBy(x => x.Motivo)
                .Select(g => new ReportVaporizadoResumen
                {
                    Empresa = "Chenyi S.A",
                    Motivo = g.Key,
                    Cantidad = g.Count(),

                    // % sobre TransitoEspecial == "No"
                    Porcentaje = totalNo == 0
                        ? 0
                        : Math.Round((decimal)g.Count() * 100 / totalNo, 2),

                    // % sobre el total general
                    PorcentajeTotal = totalGeneral == 0
                        ? 0
                        : Math.Round((decimal)g.Count() * 100 / totalGeneral, 2)
                })
                .OrderByDescending(x => x.Cantidad)
                .ToList();

            // Asigno datasource SOLO al subreporte
            sub.ReportSource.DataSource = resumen;
            sub.ReportSource.DataMember = "";

            sub.ReportSource.Parameters["pTitulo"].Value =
                "Resumen – Equipos Propios";
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            var sub = (XRSubreport)sender;

            var dataPadre = (IEnumerable<VaporizadoDto>)this.DataSource;

            // Total general (sin filtrar)
            int totalGeneral = dataPadre.Count();

            // Filtrados TransitoEspecial == "No"
            var dataNo = dataPadre
                .Where(x => x.TransitoEspecial != "No")
                .ToList();

            int totalNo = dataNo.Count;

            // Armo el datasource del subreporte
            var resumen = dataNo
                .GroupBy(x => x.Motivo)
                .Select(g => new ReportVaporizadoResumen
                {
                    Empresa = "Transito Especial",
                    Motivo = g.Key,
                    Cantidad = g.Count(),

                    // % sobre TransitoEspecial == "No"
                    Porcentaje = totalNo == 0
                        ? 0
                        : Math.Round((decimal)g.Count() * 100 / totalNo, 2),

                    // % sobre el total general
                    PorcentajeTotal = totalGeneral == 0
                        ? 0
                        : Math.Round((decimal)g.Count() * 100 / totalGeneral, 2)
                })
                .OrderByDescending(x => x.Cantidad)
                .ToList();

            // Asigno datasource SOLO al subreporte
            sub.ReportSource.DataSource = resumen;
            sub.ReportSource.DataMember = "";

            sub.ReportSource.Parameters["pTitulo"].Value =
                "Resumen – Tránsito Especial";
        }

        private void xrSubreport3_BeforePrint(object sender, CancelEventArgs e)
        {
            var sub = (XRSubreport)sender;

            var dataPadre = (IEnumerable<VaporizadoDto>)this.DataSource;

            // Total general
            int totalGeneral = dataPadre.Count();

            // Total por empresa (para calcular el % por empresa)
            var totalPorEmpresa = dataPadre
                .GroupBy(x => x.Empresa)
                .ToDictionary(g => g.Key, g => g.Count());

            // Armo el datasource del subreporte
            var resumen = dataPadre
                .GroupBy(x => new { x.Empresa, x.Motivo })
                .Select(g => new ReportVaporizadoResumen
                {
                    Empresa = g.Key.Empresa,
                    Motivo = g.Key.Motivo,
                    Cantidad = g.Count(),

                    // % sobre el total de la empresa
                    Porcentaje = totalPorEmpresa[g.Key.Empresa] == 0
                        ? 0
                        : Math.Round(
                            (decimal)g.Count() * 100 / totalPorEmpresa[g.Key.Empresa], 2
                        ),

                    // % sobre el total general
                    PorcentajeTotal = totalGeneral == 0
                        ? 0
                        : Math.Round(
                            (decimal)g.Count() * 100 / totalGeneral, 2
                        )
                })
                .OrderBy(x => x.Empresa)
                .ThenByDescending(x => x.Cantidad)
                .ToList();

            sub.ReportSource.DataSource = resumen;
            sub.ReportSource.DataMember = "";

            sub.ReportSource.Parameters["pTitulo"].Value =
                "Resumen General por Empresa y Motivo";
        }
    }
}