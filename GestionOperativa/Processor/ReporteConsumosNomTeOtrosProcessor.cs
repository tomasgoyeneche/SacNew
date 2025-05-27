using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using Shared.Models;
using Shared.Models.DTOs;
using Shared.Models.DTOs.Reports;

namespace GestionOperativa.Processor
{
    internal class ReporteConsumosNomTeOtrosProcessor : IReporteConsumosNomTeOtrosProcessor
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IEmpresaSeguroRepositorio _empresaSeguroRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IPlanillaRepositorio _planillaRepositorio;

        public ReporteConsumosNomTeOtrosProcessor(
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

        public async Task<ReporteControlOperativoConsumos?> ObtenerReporteConsumosNomina(int idNomina, int idPoc, DateTime fecha)
        {
            ReporteControlOperativoConsumos reporte = new ReporteControlOperativoConsumos();

            if (idNomina != 0)
            {
                Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idNomina, fecha);
                UnidadDto? unidadDto = await _unidadRepositorio.ObtenerPorIdDtoAsync(nomina.IdUnidad);
                Unidad? unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(nomina.IdUnidad);
                TractorDto? tractorDto = await _tractorRepositorio.ObtenerPorIdDtoAsync(unidad.IdTractor);
                SemiDto? semiDto = await _semiRepositorio.ObtenerPorIdDtoAsync(unidad.IdSemi);
                ChoferDto? chofer = await _choferRepositorio.ObtenerPorIdDtoAsync(nomina.IdChofer);

                List<EmpresaSeguroDto?> segurosUnidades = await _empresaSeguroRepositorio.ObtenerSeguroPorEmpresaYEntidadAsync(unidad.IdEmpresa, 2);
                List<EmpresaSeguroDto?> segurosChofer = await _empresaSeguroRepositorio.ObtenerSeguroPorEmpresaYEntidadAsync(unidad.IdEmpresa, 1);

                List<VencimientosDto> vencimientosDtos = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(nomina);

                // Llenar el DTO
                PlanillaNominaConsumosDto datosNomina = new PlanillaNominaConsumosDto
                {
                    Empresa = unidadDto?.Empresa_Unidad,
                    PatenteTractor = tractorDto?.Patente,
                    VTVTractor = tractorDto?.Vtv,
                    SeguroTractor = segurosUnidades.FirstOrDefault(s => s.Centralizado == "Si")?.vigenciaAnual,
                    PatenteSemi = semiDto?.Patente,
                    VTV = semiDto?.Vtv,
                    SeguroSemi = segurosUnidades.FirstOrDefault(s => s.Centralizado == "Si")?.vigenciaAnual,
                    Estanqueidad = semiDto?.Estanqueidad,
                    VisualInterna = semiDto?.VisualInterna,
                    VisualExterna = semiDto?.VisualExterna,
                    Espesor = semiDto?.CisternaEspesor,
                    Checklist = unidadDto?.Checklist,
                    Calibrado = unidadDto?.Calibrado,
                    MasYPF = unidadDto?.MasYPF,
                    SeguroUnidad = segurosUnidades.FirstOrDefault(s => s.Centralizado == "Si")?.vigenciaAnual,
                    Chofer = $"{chofer?.Nombre} {chofer?.Apellido}",
                    Licencia = chofer?.Licencia,
                    PsicofisicoApto = chofer?.PsicofisicoApto,
                    PsicofisicoCurso = chofer?.PsicofisicoCurso,
                    SeguroDeVida = segurosChofer.FirstOrDefault(s => s.TipoCobertura == "Seguro de Vida")?.vigenciaAnual,
                    Art = segurosChofer.FirstOrDefault(s => s.TipoCobertura == "ART")?.vigenciaAnual
                };

                reporte.VencimientosNomina.DataSource = new List<PlanillaNominaConsumosDto> { datosNomina };
                reporte.VencimientosNomina.DataMember = "";

                var vencimientosVencidos = vencimientosDtos
                  .Where(v => v.FechaVencimiento < fecha)
                  .Select(v => $"{v.Descripcion}: {v.FechaVencimiento:dd/MM/yyyy}")
                  .ToList();

                var fechaLimite = fecha.AddMonths(1);
                var proximosVencimientos = vencimientosDtos
                    .Where(v => v.FechaVencimiento >= fecha && v.FechaVencimiento <= fechaLimite)
                    .Select(v => $"{v.Descripcion}: {v.FechaVencimiento:dd/MM/yyyy}")
                    .ToList();

                string strVencimientosVencidos = string.Join(Environment.NewLine, vencimientosVencidos);
                string strProximosVencimientos = string.Join(Environment.NewLine, proximosVencimientos);

                reporte.Parameters["vencimientosvencidos"].Value = strVencimientosVencidos;
                reporte.Parameters["proximosvencimientos"].Value = strProximosVencimientos;
            }

            Planilla? planilla = await _planillaRepositorio.ObtenerPorIdAsync(3);
            // Obtener los datos desde el repositorio
            List<PlanillaPreguntaDto> preguntas = await _planillaRepositorio.ObtenerPreguntasPorPlanilla(3);
            // Crear una instancia del nuevo reporte DevExpress

            List<PlanillaPreguntaDto> preguntasReporte = preguntas.Select(p => new PlanillaPreguntaDto
            {
                Orden = p.Orden,
                Texto = p.Texto,
                DescripcionUnidadTipo = p.DescripcionUnidadTipo,
                Conforme = p.Conforme,
                Observaciones = FormatearObservaciones(p.Observaciones),
                NoConforme = p.NoConforme
            }).OrderBy(p => p.Orden, new AlfanumericStringComparer())
            .ToList();

            reporte.DataSource = new List<Planilla?> { planilla };
            reporte.DataMember = "";

            reporte.DetailReport.DataSource = preguntasReporte;
            reporte.DetailReport.DataMember = ""; // si es lista directa

            reporte.Parameters["fechaPoc"].Value = fecha;
            reporte.Parameters["numeroPoc"].Value = idPoc;

            return reporte;
        }

        public async Task<ReporteIngresoTe?> ObtenerReporteTeOtros(int idPoc, DateTime fecha, TransitoEspecial transitoEspecial)
        {
            var reporte = new ReporteIngresoTe();
            reporte.DataSource = new List<TransitoEspecial> { transitoEspecial };
            reporte.DataMember = "";
            reporte.Parameters["numeroPoc"].Value = idPoc;
            reporte.Parameters["fechaCreacion"].Value = fecha;
            reporte.Parameters["numeroPoc"].Visible = false; // opcional, por si querés ocultarlo desde acá también
            reporte.Parameters["fechaCreacion"].Visible = false; // opcional, por si querés ocultarlo desde acá también
            return reporte;
        }


        public async Task<ReporteVerifMensual?> ObtenerReporteVerifMensual(int idNomina, int idPoc, DateTime fecha)
        {
            ReporteVerifMensual reporte = new ReporteVerifMensual();

            if (idNomina != 0)
            {
                Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idNomina, fecha);
                UnidadDto? unidadDto = await _unidadRepositorio.ObtenerPorIdDtoAsync(nomina.IdUnidad);
                Unidad? unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(nomina.IdUnidad);
                TractorDto? tractorDto = await _tractorRepositorio.ObtenerPorIdDtoAsync(unidad.IdTractor);
                SemiDto? semiDto = await _semiRepositorio.ObtenerPorIdDtoAsync(unidad.IdSemi);
                ChoferDto? chofer = await _choferRepositorio.ObtenerPorIdDtoAsync(nomina.IdChofer);

                // Llenar el DTO
                PlanillaNominaConsumosDto datosNomina = new PlanillaNominaConsumosDto
                {
                    Empresa = unidadDto?.Empresa_Unidad,
                    PatenteTractor = tractorDto?.Patente,
                    PatenteSemi = semiDto?.Patente,
                    Chofer = $"{chofer?.Nombre} {chofer?.Apellido}",
                };

                reporte.DetailReportNomina.DataSource = new List<PlanillaNominaConsumosDto> { datosNomina };
                reporte.DetailReportNomina.DataMember = "";
            }

            Planilla planilla = await _planillaRepositorio.ObtenerPorIdAsync(2);
            // Obtener los datos desde el repositorio
            List<PlanillaPreguntaDto> preguntas = await _planillaRepositorio.ObtenerPreguntasPorPlanilla(2);
            // Crear una instancia del nuevo reporte DevExpress

            List<PlanillaPreguntaDto> preguntasReporte = preguntas.Select(p => new PlanillaPreguntaDto
            {
                Orden = p.Orden,
                Texto = p.Texto,
                Conforme = p.Conforme,
                Observaciones = FormatearObservaciones(p.Observaciones),
                NoConforme = p.NoConforme,
                EsEncabezado = EsEntero(p.Orden)
            }).OrderBy(p => p.Orden, new AlfanumericStringComparer())
            .ToList();


            reporte.DataSource = new List<Planilla?> { planilla };
            reporte.DataMember = "";

            reporte.PreguntasPlanilla.DataSource = preguntasReporte;
            reporte.PreguntasPlanilla.DataMember = ""; // si es lista directa

            reporte.Parameters["fechaPoc"].Value = fecha;
            reporte.Parameters["numeroPoc"].Value = idPoc;

            return reporte;
        }

        private string FormatearObservaciones(string observaciones)
        {
            if (string.IsNullOrEmpty(observaciones))
                return "";

            // Si es N/A, devolver vacío
            if (observaciones.Trim().Equals("N/A", StringComparison.OrdinalIgnoreCase))
                return "";

            if (observaciones.Trim().Equals("SI-NO-N/A", StringComparison.OrdinalIgnoreCase))
            {
                return "SI | NO";
            }

            if (observaciones.Trim().Equals("MATERIAL", StringComparison.OrdinalIgnoreCase))
            {
                return "N/A";
            }

            // Eliminar guiones finales si hay uno solo
            if (observaciones.EndsWith("-") && observaciones.Count(c => c == '-') == 1)
            {
                return observaciones.TrimEnd('-');
            }
            else
            {
                // Reemplazar los guiones por barras
                return observaciones.TrimEnd('-').Replace("-", " | ");
            }
        }

        private bool EsEntero(string orden)
        {
            if (string.IsNullOrEmpty(orden))
                return false;

            // Si contiene un punto o una coma, no es entero
            if (orden.Contains(".") || orden.Contains(","))
                return false;

            return int.TryParse(orden, out _);
        }
    }
}