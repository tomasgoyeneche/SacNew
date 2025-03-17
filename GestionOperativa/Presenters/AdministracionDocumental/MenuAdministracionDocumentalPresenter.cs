using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Processor;
using GestionOperativa.Views.AdministracionDocumental;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class MenuAdministracionDocumentalPresenter : BasePresenter<IMenuAdministracionDocumentalView>
    {
        private readonly IDocumentacionProcessor _documentacionService;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;

        public MenuAdministracionDocumentalPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IDocumentacionProcessor documentacionService,
            IConfRepositorio confRepositorio,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IEmpresaRepositorio empresaRepositorio
        ) : base(sesionService, navigationService)
        {
            _documentacionService = documentacionService;
            _confRepositorio = confRepositorio;
            _choferRepositorio = choferRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _unidadRepositorio = unidadRepositorio;
        }

        public async Task CargarMenuEntidad(string numeroEntidad)
        {
            await AbrirFormularioAsync<MenuAltaForm>(async form =>
            {
                await form.CargarEntidades(numeroEntidad);
            });
        }

        public async Task VerificarFotosChoferesAsync()
        {
            var rutaBase = await _confRepositorio.ObtenerRutaPorIdAsync(1);
            if (rutaBase == null || string.IsNullOrEmpty(rutaBase.Ruta))
            {
                _view.MostrarMensaje("No se encontró la ruta de los archivos de choferes.");
                return;
            }

            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\FotosChoferes.csv";

            await _documentacionService.VerificarArchivosFaltantesAsync(
                _choferRepositorio.ObtenerTodosLosChoferesDto,
                chofer => Path.Combine(rutaBase.Ruta, $"{chofer.Documento}.jpg"),
                rutaCsv,
                chofer => new
                {
                    Empresa = chofer.Empresa_Nombre,
                    CuitEmpresa = chofer.Empresa_Cuit,
                    Nombre = $"{chofer.Apellido}, {chofer.Nombre}",
                    chofer.Documento,
                    ArchivoFaltante = $"Q:\\Choferes\\{chofer.Documento}.jpg"
                },
                _view.MostrarRelevamiento
            );
        }

        public async Task VerificarLegajosChoferesAsync()
        {
            var rutaBase = await _confRepositorio.ObtenerRutaPorIdAsync(5);
            if (rutaBase == null || string.IsNullOrEmpty(rutaBase.Ruta))
            {
                _view.MostrarMensaje("No se encontró la ruta de los legajos de choferes.");
                return;
            }

            string rutaRaiz = Path.Combine(rutaBase.Ruta, "Chofer");
            string rutaSeguroEmpresa = Path.Combine(rutaBase.Ruta, "EMPRESA", "ART");
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\LegajosChoferes.csv";

            var subCarpetas = new Dictionary<string, string>
            {
                { "Alta Temprana", "AltaTemprana" },
                { "Apto", "Apto" },
                { "Curso", "Curso" },
                { "DNI", "DNI" },
                { "Licencia", "Licencia" }
            };

            Func<ChoferDto, List<string>> obtenerArchivosFaltantes = chofer =>
            {
                var archivosFaltantes = subCarpetas
                    .Where(sub => !File.Exists(Path.Combine(rutaRaiz, sub.Value, $"{chofer.Documento}.pdf")))
                    .Select(sub => $"{sub.Key}: Q:\\LegajosDigitalizados\\Chofer\\{sub.Value}\\{chofer.Documento}.pdf")
                    .ToList();

                string rutaSeguro = chofer.CoberturaCentralizada
                    ? Path.Combine(rutaSeguroEmpresa, $"{chofer.Empresa_Cuit}.pdf")
                    : Path.Combine(rutaRaiz, "Seguro", $"{chofer.Documento}.pdf");

                if (!File.Exists(rutaSeguro))
                {
                    string carpetaSeguro = chofer.CoberturaCentralizada ? "EMPRESA\\ART" : "Chofer\\Seguro";
                    archivosFaltantes.Add($"Seguro: Q:\\LegajosDigitalizados\\{carpetaSeguro}\\{Path.GetFileName(rutaSeguro)}");
                }

                return archivosFaltantes;
            };

            await _documentacionService.VerificarArchivosFaltantesAsync(
                _choferRepositorio.ObtenerTodosLosChoferesDto,
                chofer =>
                {
                    var archivosFaltantes = obtenerArchivosFaltantes(chofer);
                    return archivosFaltantes.Count > 0 ? string.Join(" | ", archivosFaltantes) : null;
                },
                rutaCsv,
                chofer => new
                {
                    chofer.Empresa_Nombre,
                    chofer.Empresa_Cuit,
                    Nombre = $"{chofer.Apellido}, {chofer.Nombre}",
                    chofer.Documento,
                    ArchivosFaltantes = obtenerArchivosFaltantes(chofer).Count > 0
                        ? string.Join(" | ", obtenerArchivosFaltantes(chofer))
                        : "Completo"
                },
                _view.MostrarRelevamiento
            );
        }

        public async Task VerificarLegajosEmpresasAsync()
        {
            var rutaBase = await _confRepositorio.ObtenerRutaPorIdAsync(5);
            if (rutaBase == null || string.IsNullOrEmpty(rutaBase.Ruta))
            {
                _view.MostrarMensaje("No se encontró la ruta de los legajos de empresas.");
                return;
            }

            string rutaRaiz = Path.Combine(rutaBase.Ruta, "EMPRESA");
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\LegajosEmpresas.csv";

            var subCarpetas = new Dictionary<string, string>
            {
                { "ART", "ART" },
                { "CUIT", "CUIT" }
            };

            Func<EmpresaDto, List<string>> obtenerArchivosFaltantes = empresa =>
            {
                return subCarpetas
                    .Where(sub => !File.Exists(Path.Combine(rutaRaiz, sub.Value, $"{empresa.Cuit}.pdf")))
                    .Select(sub => $"{sub.Key}: Q:\\LegajosDigitalizados\\EMPRESA\\{sub.Value}\\{empresa.Cuit}.pdf")
                    .ToList();
            };

            await _documentacionService.VerificarArchivosFaltantesAsync(
                _empresaRepositorio.ObtenerTodasLasEmpresasAsync,
                empresa =>
                {
                    var archivosFaltantes = obtenerArchivosFaltantes(empresa);
                    return archivosFaltantes.Count > 0 ? string.Join(" | ", archivosFaltantes) : null;
                },
                rutaCsv,
                empresa => new
                {
                    empresa.RazonSocial,
                    empresa.NombreFantasia,
                    empresa.Cuit,
                    ArchivosFaltantes = obtenerArchivosFaltantes(empresa).Count > 0
                        ? string.Join(" | ", obtenerArchivosFaltantes(empresa))
                        : "Completo"
                },
                _view.MostrarRelevamiento
            );
        }

        public async Task VerificarFotosUnidadesAsync()
        {
            var rutaBase = await _confRepositorio.ObtenerRutaPorIdAsync(6);
            if (rutaBase == null || string.IsNullOrEmpty(rutaBase.Ruta))
            {
                _view.MostrarMensaje("No se encontró la ruta de las fotos de unidades.");
                return;
            }

            string rutaRaiz = rutaBase.Ruta;
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\FotosUnidades.csv";

            var subCarpetas = new Dictionary<string, Func<UnidadDto, string>>
            {
                { "Tractor", unidad => Path.Combine(rutaRaiz, "Tractor", $"{unidad.Tractor_Patente}.jpg") },
                { "Semi", unidad => Path.Combine(rutaRaiz, "Semi", $"{unidad.Semirremolque_Patente}.jpg") },
                { "Nomina", unidad => Path.Combine(rutaRaiz, "Nomina", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.jpg") }
            };

            await _documentacionService.VerificarArchivosFaltantesAsync(
                _unidadRepositorio.ObtenerUnidadesDtoAsync,
                unidad =>
                {
                    var archivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: Q:\\Fotos\\{sub.Key}\\{Path.GetFileName(sub.Value(unidad))}")
                        .ToList();

                    return archivosFaltantes.Count > 0 ? string.Join(" | ", archivosFaltantes) : null;
                },
                rutaCsv,
                unidad => new
                {
                    unidad.Empresa_Unidad,
                    unidad.Cuit_Unidad,
                    NombreUnidad = $"{unidad.Tractor_Patente} - {unidad.Semirremolque_Patente}",
                    ArchivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: Q:\\Fotos\\{sub.Key}\\{Path.GetFileName(sub.Value(unidad))}")
                        .DefaultIfEmpty("Completo")
                        .Aggregate((a, b) => $"{a} | {b}")
                },
                _view.MostrarRelevamiento
            );
        }

        public async Task VerificarDocumentosUnidadesAsync()
        {
            var rutaBase = await _confRepositorio.ObtenerRutaPorIdAsync(5); // Ruta base de legajos digitalizados
            if (rutaBase == null || string.IsNullOrEmpty(rutaBase.Ruta))
            {
                _view.MostrarMensaje("No se encontró la ruta de los documentos de unidades.");
                return;
            }

            string rutaRaiz = rutaBase.Ruta;
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\DocumentosUnidades.csv";

            // 🔹 Definir subcarpetas y cómo construir los nombres de archivo
            var subCarpetas = new Dictionary<string, Func<UnidadDto, string>>
            {
                // 📂 Documentos de NOMINA (Basados en patente tractor + semi)
                { "Nomina - Calibrado", unidad => Path.Combine(rutaRaiz, "Nomina", "Calibrado", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },
                { "Nomina - Checklist", unidad => Path.Combine(rutaRaiz, "Nomina", "Checklist", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },
                { "Nomina - MAS", unidad => Path.Combine(rutaRaiz, "Nomina", "Mas", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },
                { "Nomina - Tara", unidad => Path.Combine(rutaRaiz, "Nomina", "Tara", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },

                // 📂 Documentos de TRACTOR (Basados en patente del tractor)
                { "Tractor - Cédula", unidad => Path.Combine(rutaRaiz, "Tractor", "Cedula", $"{unidad.Tractor_Patente}.pdf") },
                { "Tractor - Ruta", unidad => Path.Combine(rutaRaiz, "Tractor", "Ruta", $"{unidad.Tractor_Patente}.pdf") },
                { "Tractor - Título", unidad => Path.Combine(rutaRaiz, "Tractor", "Titulo", $"{unidad.Tractor_Patente}.pdf") },
                { "Tractor - VTV", unidad => Path.Combine(rutaRaiz, "Tractor", "VTV", $"{unidad.Tractor_Patente}.pdf") },

                // 📂 Documentos de SEMIRREMOLQUE (Basados en patente del semi)
                { "Semi - Cédula", unidad => Path.Combine(rutaRaiz, "Semi", "Cedula", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Cubicación", unidad => Path.Combine(rutaRaiz, "Semi", "Cubicacion", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Espesor", unidad => Path.Combine(rutaRaiz, "Semi", "Espesor", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Estanqueidad", unidad => Path.Combine(rutaRaiz, "Semi", "Estanqueidad", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Litros nominales", unidad => Path.Combine(rutaRaiz, "Semi", "Litros_Nominales", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Ruta", unidad => Path.Combine(rutaRaiz, "Semi", "Ruta", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Título", unidad => Path.Combine(rutaRaiz, "Semi", "Titulo", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Visual Interna", unidad => Path.Combine(rutaRaiz, "Semi", "Visual_Interna", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Visual Externa", unidad => Path.Combine(rutaRaiz, "Semi", "Visual_Externa", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - VTV", unidad => Path.Combine(rutaRaiz, "Semi", "VTV", $"{unidad.Semirremolque_Patente}.pdf") }
            };

            await _documentacionService.VerificarArchivosFaltantesAsync(
                _unidadRepositorio.ObtenerUnidadesDtoAsync,
                unidad =>
                {
                    var archivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: Q:\\LegajosDigitalizados\\{sub.Key.Replace(" - ", "\\")}\\{Path.GetFileName(sub.Value(unidad))}")
                        .ToList();

                    return archivosFaltantes.Count > 0 ? string.Join(" | ", archivosFaltantes) : null;
                },
                rutaCsv,
                unidad => new
                {
                    unidad.Empresa_Unidad,
                    unidad.Cuit_Unidad,
                    NombreUnidad = $"{unidad.Tractor_Patente} - {unidad.Semirremolque_Patente}",
                    ArchivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: Q:\\LegajosDigitalizados\\{sub.Key.Replace(" - ", "\\")}\\{Path.GetFileName(sub.Value(unidad))}")
                        .DefaultIfEmpty("Completo")
                        .Aggregate((a, b) => $"{a} | {b}")
                },
                _view.MostrarRelevamiento
            );
        }
    }
}