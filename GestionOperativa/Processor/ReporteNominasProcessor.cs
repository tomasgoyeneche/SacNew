using Core.Reports;
using Core.Repositories;
using GestionOperativa.Reports;
using Shared.Models;

namespace GestionOperativa.Processor
{
    public class ReporteNominasProcessor : IReporteNominasProcessor
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IEmpresaSeguroRepositorio _empresaSeguroRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IPlanillaRepositorio _planillaRepositorio;

        public ReporteNominasProcessor(
            IUnidadRepositorio unidadRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio,
            IConfRepositorio confRepositorio,
            IEmpresaSeguroRepositorio empresaSeguroRepositorio,
            IChoferRepositorio choferRepositorio,
            IPlanillaRepositorio planillaRepositorio,
            INominaRepositorio nominaRepositorio)
        {
            _unidadRepositorio = unidadRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
            _confRepositorio = confRepositorio;
            _empresaSeguroRepositorio = empresaSeguroRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _choferRepositorio = choferRepositorio;
            _planillaRepositorio = planillaRepositorio;
        }

        public async Task<ReporteNominaActual?> ObtenerReporteNominaActual()
        {
            List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
            List<ChoferDto> choferes = await _choferRepositorio.ObtenerTodosLosChoferesDto();
            // Crear una instancia del nuevo reporte DevExpress
            ReporteNominaActual reporte = new ReporteNominaActual();
            reporte.DetailReport.DataSource = unidades;
            reporte.DetailReport.DataMember = "";

            reporte.DetailReport1.DataSource = choferes;
            reporte.DetailReport1.DataMember = "";

            return reporte;
        }

        public async Task<ReporteNominaMetanolActiva?> ObtenerReporteNominaMetanol()
        {
            List<NominaMetanolActivaDto> flotaUnidades = await _unidadRepositorio.ObtenerNominaMetanolActiva();

            // Crear una instancia del nuevo reporte DevExpress
            ReporteNominaMetanolActiva reporte = new ReporteNominaMetanolActiva();
            reporte.DataSource = flotaUnidades;
            reporte.DataMember = "";

            return reporte;
        }
    }
}