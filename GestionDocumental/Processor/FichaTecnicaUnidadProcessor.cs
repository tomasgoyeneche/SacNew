using Core.Repositories;
using Shared.Models;
using Shared.Models.DTOs;
using System.IO;

namespace GestionOperativa.Processor
{
    public class FichaTecnicaUnidadProcessor : IFichaTecnicaUnidadProcessor
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IEmpresaSeguroRepositorio _empresaSeguroRepositorio;

        public FichaTecnicaUnidadProcessor(
            IUnidadRepositorio unidadRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio,
            IConfRepositorio confRepositorio,
            IEmpresaSeguroRepositorio empresaSeguroRepositorio)
        {
            _unidadRepositorio = unidadRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
            _confRepositorio = confRepositorio;
            _empresaSeguroRepositorio = empresaSeguroRepositorio;
        }

        public async Task<FichaTecnicaUnidadDto?> ObtenerFichaTecnicaAsync(int idUnidad)
        {
            var unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(idUnidad);
            if (unidad == null) return null;

            Shared.Models.Tractor? tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(unidad.IdTractor);
            Semi? semi = await _semiRepositorio.ObtenerSemiPorIdAsync(unidad.IdSemi);

            if (tractor == null || semi == null) return null;

            var dtoUnidad = await _unidadRepositorio.ObtenerPorIdDtoAsync(idUnidad);
            var dtoTractor = await _tractorRepositorio.ObtenerPorIdDtoAsync(unidad.IdTractor);
            var dtoSemi = await _semiRepositorio.ObtenerPorIdDtoAsync(unidad.IdSemi);

            List<EmpresaSeguroDto> segurosTractor = await _empresaSeguroRepositorio.ObtenerSeguroPorEmpresaYEntidadAsync(tractor.IdEmpresa, 2);
            List<EmpresaSeguroDto> segurosSemi = await _empresaSeguroRepositorio.ObtenerSeguroPorEmpresaYEntidadAsync(semi.IdEmpresa, 2);

            string seguroTractor = string.Join("\n \n", segurosTractor.Select(s =>
                $"{s.cia}, {s.TipoCobertura}, N° {s.numeroPoliza} con Vigencia Anual: {s.vigenciaAnual:dd/MM/yyyy} y Certificado Mensual: {s.certificadoMensual:dd/MM/yyyy}"));

            string seguroSemi = string.Join("\n \n", segurosSemi.Select(s =>
                $"{s.cia}, {s.TipoCobertura}, N° {s.numeroPoliza} con Vigencia Anual: {s.vigenciaAnual:dd/MM/yyyy} y Certificado Mensual: {s.certificadoMensual:dd/MM/yyyy}"));

            // Ruta imágenes
            var rutaFotoBase = (await _confRepositorio.ObtenerRutaPorIdAsync(6))?.Ruta ?? "";
            var rutaConfigBase = (await _confRepositorio.ObtenerRutaPorIdAsync(4))?.Ruta ?? "";

            var rutaFotoNomina = Path.Combine(rutaFotoBase, "nomina", $"{dtoTractor.Patente}_{dtoSemi.Patente}.jpg");
            var rutaConfiguracionTractor = Path.Combine(rutaConfigBase, "tractor", $"{dtoTractor.Configuracion}.bmp");
            var rutaConfiguracionSemi = Path.Combine(rutaConfigBase, "semi", $"{dtoSemi.Configuracion}.bmp");

            return new FichaTecnicaUnidadDto
            {
                EmpresaUnidad = dtoUnidad.Empresa_Unidad,
                CuitUnidad = dtoUnidad.Cuit_Unidad,
                TipoEmpresa = dtoUnidad.Tipo_Empresa,
                TaraTotal = dtoUnidad.TaraTotal,
                Calibrado = dtoUnidad.VerifMensual,
                Checklist = dtoUnidad.Checklist,
                MasYPF = dtoUnidad.MasYPF,

                PatenteTractor = dtoTractor.Patente,
                AnioTractor = dtoTractor.Anio,
                MarcaTractor = dtoTractor.Marca,
                ModeloTractor = dtoTractor.Modelo,
                TaraTractor = dtoTractor.Tara,
                Hp = dtoTractor.Hp,
                Combustible = dtoTractor.Combustible,
                Cmt = dtoTractor.Cmt,
                EmpresaTractor = dtoTractor.Empresa_Nombre,
                CuitTractor = dtoTractor.Empresa_Cuit,
                TipoEmpresaTractor = dtoTractor.Empresa_Tipo,
                ConfiguracionTractor = dtoTractor.Configuracion,
                FechaAltaTractor = dtoTractor.FechaAlta,
                VtvTractor = dtoTractor.Vtv,

                PatenteSemi = dtoSemi.Patente,
                AnioSemi = dtoSemi.Anio,
                MarcaSemi = dtoSemi.Marca,
                ModeloSemi = dtoSemi.Modelo,
                TaraSemi = dtoSemi.Tara,
                ConfiguracionSemi = dtoSemi.Configuracion,
                EmpresaSemi = dtoSemi.Empresa_Nombre,
                CuitSemi = dtoSemi.Empresa_Cuit,
                TipoEmpresaSemi = dtoSemi.Empresa_Tipo,
                FechaAltaSemi = dtoSemi.FechaAlta,
                TipoCarga = dtoSemi.Tipo_Carga,
                Compartimientos = dtoSemi.Compartimientos,
                LitroNominal = dtoSemi.LitroNominal,
                Cubicacion = dtoSemi.Cubicacion,
                Material = dtoSemi.Material,
                CertificadoCompatibilidad = dtoSemi.CertificadoCompatibilidad,
                Inv = dtoSemi.Inv,
                VTVCisterna = dtoSemi.Vtv,
                CisternaEspesor = dtoSemi.CisternaEspesor,
                VisualInterna = dtoSemi.VisualInterna,
                VisualExterna = dtoSemi.VisualExterna,
                Estanqueidad = dtoSemi.Estanqueidad,

                RutaFotoNomina = rutaFotoNomina,
                RutaConfiguracionTractor = rutaConfiguracionTractor,
                RutaConfiguracionSemi = rutaConfiguracionSemi,

                // Seguros
                SeguroTractor = seguroTractor,
                SeguroSemi = seguroSemi
            };
        }
    }
}