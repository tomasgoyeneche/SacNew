using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Processor;
using GestionOperativa.Views.AdministracionDocumental;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using GestionOperativa.Views.AdministracionDocumental.Relevamientos;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Presenters
{
    public class MenuAdministracionDocumentalPresenter : BasePresenter<IMenuAdministracionDocumentalView>
    {
        private readonly IDocumentacionProcessor _documentacionService;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        public MenuAdministracionDocumentalPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IDocumentacionProcessor documentacionService,
            IConfRepositorio confRepositorio,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio
        ) : base(sesionService, navigationService)
        {
            _documentacionService = documentacionService;
            _confRepositorio = confRepositorio;
            _choferRepositorio = choferRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _semiRepositorio = semiRepositorio;
            _tractorRepositorio = tractorRepositorio;
        }

        public async Task CargarMenuEntidad(string numeroEntidad)
        {
            await AbrirFormularioAsync<MenuAltaForm>(async form =>
            {
                await form.CargarEntidades(numeroEntidad);
            });
        }

        public async Task CargarMenuConfeccionNominas()
        {
            await AbrirFormularioAsync<MenuAltaNominaForm>(async form =>
            {
                await form._presenter.CargarEmpresasAsync();
            });
        }

        public async Task CargarMenuFichaTecnica()
        {
            await AbrirFormularioAsync<FichaTecnicaUnidades>(async form =>
            {
                await form._presenter.InicializarAsync();
            });
        }

        private async Task<string?> ObtenerRutaAsync(int idRuta)
        {
            var rutaBase = await _confRepositorio.ObtenerRutaPorIdAsync(idRuta);
            return rutaBase?.Ruta;
        }

        public async Task VerificarFotosChoferesAsync()
        {
            string? rutaBase = await ObtenerRutaAsync(1);
            if (string.IsNullOrEmpty(rutaBase))
            {
                _view.MostrarMensaje("No se encontró la ruta de los archivos de choferes.");
                return;
            }

            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\FotosChoferes.csv";

            await _documentacionService.VerificarArchivosFaltantesAsync<ChoferDto>(
                 async () => await _choferRepositorio.ObtenerTodosLosChoferesDto(),
                 chofer => Path.Combine(rutaBase, $"{chofer.Documento}.jpg"),
                 rutaCsv,
                 chofer => new
                 {
                     chofer.Empresa_Nombre,
                     chofer.Empresa_Cuit,
                     Nombre = $"{chofer.Apellido}, {chofer.Nombre}",
                     chofer.Documento,
                     ArchivoFaltante = $"S:\\Choferes\\{chofer.Documento}.jpg"
                 },
                 _view.MostrarRelevamiento
             );
        }

        public async Task VerificarLegajosChoferesAsync()
        {
            string? rutaBase = await ObtenerRutaAsync(5);
            if (string.IsNullOrEmpty(rutaBase))
            {
                _view.MostrarMensaje("No se encontró la ruta de los legajos de choferes.");
                return;
            }

            string rutaRaiz = Path.Combine(rutaBase, "Chofer");
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\LegajosChoferes.csv";

            var subCarpetas = new Dictionary<string, string>
        {
            { "Alta Temprana", "AltaTemprana" },
            { "Apto", "Apto" },
            { "Curso", "Curso" },
            { "DNI", "DNI" },
            { "Licencia", "Licencia" }
        };

            await _documentacionService.VerificarArchivosFaltantesAsync<ChoferDto>(
             async () => await _choferRepositorio.ObtenerTodosLosChoferesDto(),
             chofer =>
             {
                 var archivosFaltantes = subCarpetas
                     .Where(sub => !File.Exists(Path.Combine(rutaRaiz, sub.Value, $"{chofer.Documento}.pdf")))
                     .Select(sub => $"{sub.Key}: S:\\LegajosDigitalizados\\Chofer\\{sub.Value}\\{chofer.Documento}.pdf")
                     .ToList();

                 return archivosFaltantes.Any() ? string.Join(" | ", archivosFaltantes) : null;
             },
             rutaCsv,
             chofer => new
             {
                 chofer.Empresa_Nombre,
                 chofer.Empresa_Cuit,
                 Nombre = $"{chofer.Apellido}, {chofer.Nombre}",
                 chofer.Documento,
                 ArchivosFaltantes = subCarpetas
                     .Where(sub => !File.Exists(Path.Combine(rutaRaiz, sub.Value, $"{chofer.Documento}.pdf")))
                     .Select(sub => $"{sub.Key}: S:\\LegajosDigitalizados\\Chofer\\{sub.Value}\\{chofer.Documento}.pdf")
                     .DefaultIfEmpty("Completo")
                     .Aggregate((a, b) => $"{a} | {b}")
             },
             _view.MostrarRelevamiento
         );
        }

        public async Task VerificarLegajosEmpresasAsync()
        {
            string? rutaBase = await ObtenerRutaAsync(5);
            if (string.IsNullOrEmpty(rutaBase))
            {
                _view.MostrarMensaje("No se encontró la ruta de los legajos de empresas.");
                return;
            }

            string rutaRaiz = Path.Combine(rutaBase, "EMPRESA");
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\LegajosEmpresas.csv";

            var subCarpetas = new Dictionary<string, string>
        {
            { "ART", "ART" },
            { "CUIT", "CUIT" }
        };

            await _documentacionService.VerificarArchivosFaltantesAsync<EmpresaDto>(
              async () => await _empresaRepositorio.ObtenerTodasLasEmpresasAsync(),
              empresa =>
              {
                  var archivosFaltantes = subCarpetas
                      .Where(sub => !File.Exists(Path.Combine(rutaRaiz, sub.Value, $"{empresa.Cuit}.pdf")))
                      .Select(sub => $"{sub.Key}: Q:\\LegajosDigitalizados\\EMPRESA\\{sub.Value}\\{empresa.Cuit}.pdf")
                      .ToList();

                  return archivosFaltantes.Any() ? string.Join(" | ", archivosFaltantes) : null;
              },
              rutaCsv,
              empresa => new
              {
                  empresa.RazonSocial,
                  empresa.NombreFantasia,
                  empresa.Cuit,
                  ArchivosFaltantes = subCarpetas
                      .Where(sub => !File.Exists(Path.Combine(rutaRaiz, sub.Value, $"{empresa.Cuit}.pdf")))
                      .Select(sub => $"{sub.Key}: S:\\LegajosDigitalizados\\EMPRESA\\{sub.Value}\\{empresa.Cuit}.pdf")
                      .DefaultIfEmpty("Completo")
                      .Aggregate((a, b) => $"{a} | {b}")
              },
              _view.MostrarRelevamiento
          );
        }

        public async Task VerificarFotosUnidadesAsync()
        {
            string? rutaBase = await ObtenerRutaAsync(6);
            if (string.IsNullOrEmpty(rutaBase))
            {
                _view.MostrarMensaje("No se encontró la ruta de las fotos de unidades.");
                return;
            }

            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\FotosUnidades.csv";

            var subCarpetas = new Dictionary<string, Func<UnidadDto, string>>
            {
                { "Tractor", unidad => Path.Combine(rutaBase, "Tractor", $"{unidad.Tractor_Patente}.jpg") },
                { "Semi", unidad => Path.Combine(rutaBase, "Semi", $"{unidad.Semirremolque_Patente}.jpg") },
                { "Nomina", unidad => Path.Combine(rutaBase, "Nomina", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.jpg") }
            };

            await _documentacionService.VerificarArchivosFaltantesAsync<UnidadDto>(
                async () => await _unidadRepositorio.ObtenerUnidadesDtoAsync(),
                unidad =>
                {
                    var archivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: S:\\Fotos\\{sub.Key}\\{Path.GetFileName(sub.Value(unidad))}")
                        .ToList();

                    return archivosFaltantes.Any() ? string.Join(" | ", archivosFaltantes) : null;
                },
                rutaCsv,
                unidad => new
                {
                    unidad.Empresa_Unidad,
                    unidad.Cuit_Unidad,
                    NombreUnidad = $"{unidad.Tractor_Patente} - {unidad.Semirremolque_Patente}",
                    ArchivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: S:\\Fotos\\{sub.Key}\\{Path.GetFileName(sub.Value(unidad))}")
                        .DefaultIfEmpty("Completo")
                        .Aggregate((a, b) => $"{a} | {b}")
                },
                _view.MostrarRelevamiento
            );
        }

        public async Task VerificarDocumentosUnidadesAsync()
        {
            string? rutaBase = await ObtenerRutaAsync(5);
            if (string.IsNullOrEmpty(rutaBase))
            {
                _view.MostrarMensaje("No se encontró la ruta de los documentos de unidades.");
                return;
            }

            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\DocumentosUnidades.csv";

            var subCarpetas = new Dictionary<string, Func<UnidadDto, string>>
            {
              //  { "Nomina - Calibrado", unidad => Path.Combine(rutaBase, "Nomina", "Calibrado", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },
                { "Nomina - Checklist", unidad => Path.Combine(rutaBase, "Nomina", "Checklist", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },
                { "Nomina - MAS", unidad => Path.Combine(rutaBase, "Nomina", "Mas", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },
                { "Nomina - Tara", unidad => Path.Combine(rutaBase, "Nomina", "Tara", $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}.pdf") },

                { "Tractor - Cédula", unidad => Path.Combine(rutaBase, "Tractor", "Cedula", $"{unidad.Tractor_Patente}.pdf") },
                { "Tractor - Ruta", unidad => Path.Combine(rutaBase, "Tractor", "Ruta", $"{unidad.Tractor_Patente}.pdf") },
                { "Tractor - Título", unidad => Path.Combine(rutaBase, "Tractor", "Titulo", $"{unidad.Tractor_Patente}.pdf") },
                { "Tractor - VTV", unidad => Path.Combine(rutaBase, "Tractor", "VTV", $"{unidad.Tractor_Patente}.pdf") },

                { "Semi - Cédula", unidad => Path.Combine(rutaBase, "Semi", "Cedula", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Cubicación", unidad => Path.Combine(rutaBase, "Semi", "Cubicacion", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Espesor", unidad => Path.Combine(rutaBase, "Semi", "Espesor", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Estanqueidad", unidad => Path.Combine(rutaBase, "Semi", "Estanqueidad", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Litros nominales", unidad => Path.Combine(rutaBase, "Semi", "Litros_Nominales", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Ruta", unidad => Path.Combine(rutaBase, "Semi", "Ruta", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Título", unidad => Path.Combine(rutaBase, "Semi", "Titulo", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Visual Interna", unidad => Path.Combine(rutaBase, "Semi", "Visual_Interna", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - Visual Externa", unidad => Path.Combine(rutaBase, "Semi", "Visual_Externa", $"{unidad.Semirremolque_Patente}.pdf") },
                { "Semi - VTV", unidad => Path.Combine(rutaBase, "Semi", "VTV", $"{unidad.Semirremolque_Patente}.pdf") }
            };

            await _documentacionService.VerificarArchivosFaltantesAsync<UnidadDto>(
                async () => await _unidadRepositorio.ObtenerUnidadesDtoAsync(),
                unidad =>
                {
                    var archivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: S:\\LegajosDigitalizados\\{sub.Key.Replace(" - ", "\\")}\\{Path.GetFileName(sub.Value(unidad))}")
                        .ToList();

                    return archivosFaltantes.Any() ? string.Join(" | ", archivosFaltantes) : null;
                },
                rutaCsv,
                unidad => new
                {
                    unidad.Empresa_Unidad,
                    unidad.Cuit_Unidad,
                    NombreUnidad = $"{unidad.Tractor_Patente} - {unidad.Semirremolque_Patente}",
                    ArchivosFaltantes = subCarpetas
                        .Where(sub => !File.Exists(sub.Value(unidad)))
                        .Select(sub => $"{sub.Key}: S:\\LegajosDigitalizados\\{sub.Key.Replace(" - ", "\\")}\\{Path.GetFileName(sub.Value(unidad))}")
                        .DefaultIfEmpty("Completo")
                        .Aggregate((a, b) => $"{a} | {b}")
                },
                _view.MostrarRelevamiento
            );
        }

        //Vencimientos de la base de datos

        public async Task VerificarVencimientosChoferesAsync()
        {
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\VencimientosChoferes.csv";

            var camposBase = new Dictionary<string, Func<ChoferDto, object>>
    {
        { "NombreFantasia", chofer => chofer.Empresa_Nombre },
        { "CUIT", chofer => chofer.Empresa_Cuit },
        { "Nombre", chofer => $"{chofer.Apellido}, {chofer.Nombre}" },
        { "Documento", chofer => chofer.Documento }
    };

            var camposVencimiento = new Dictionary<string, Func<ChoferDto, DateTime?>>
    {
        { "PsicofisicoApto", chofer => chofer.PsicofisicoApto },
        { "PsicofisicoCurso", chofer => chofer.PsicofisicoCurso },
        { "Licencia", chofer => chofer.Licencia },
    };

            await _documentacionService.ExportarVencimientosAsync<ChoferDto>(
                async () => await _choferRepositorio.ObtenerTodosLosChoferesDto(),
                rutaCsv,
                camposBase,
                camposVencimiento,
                _view.MostrarRelevamientoVencimientos
            );
        }

        public async Task VerificarVencimientosTractoresAsync()
        {
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\VencimientosTractores.csv";

            var camposBase = new Dictionary<string, Func<TractorDto, object>>
            {
                { "NombreFantasia", unidad => unidad.Empresa_Nombre },
                { "CUIT", unidad => unidad.Empresa_Cuit },
                { "Patente", unidad => unidad.Patente }
            };

            var camposVencimiento = new Dictionary<string, Func<TractorDto, DateTime?>>
            {
                { "VTV", unidad => unidad.Vtv }
            };

            await _documentacionService.ExportarVencimientosAsync<TractorDto>(
                async () => await _tractorRepositorio.ObtenerTodosLosTractoresDto(),
                rutaCsv,
                camposBase,
                camposVencimiento,
                _view.MostrarRelevamientoVencimientos
            );
        }

        public async Task VerificarVencimientosSemisAsync()
        {
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\VencimientosSemiremolques.csv";

            var camposBase = new Dictionary<string, Func<SemiDto, object>>
        {
            { "NombreFantasia", unidad => unidad.Empresa_Nombre },
            { "CUIT", unidad => unidad.Empresa_Cuit },
            { "Patente", unidad => unidad.Patente }
        };

            var camposVencimiento = new Dictionary<string, Func<SemiDto, DateTime?>>
        {
            { "VTV", unidad => unidad.Vtv },
            { "CisternaEspesor", unidad => unidad.CisternaEspesor },
            { "VisualInterna", unidad => unidad.VisualInterna },
            { "VisualExterna", unidad => unidad.VisualExterna },
            { "Estanqueidad", unidad => unidad.Estanqueidad }
        };

            await _documentacionService.ExportarVencimientosAsync<SemiDto>(
                async () => await _semiRepositorio.ObtenerTodosLosSemisDto(),
                rutaCsv,
                camposBase,
                camposVencimiento,
                _view.MostrarRelevamientoVencimientos
            );
        }

        public async Task VerificarVencimientosUnidadesAsync()
        {
            string rutaCsv = "C:\\Compartida\\SAC\\Exportaciones\\VencimientosUnidades.csv";

            var camposBase = new Dictionary<string, Func<UnidadDto, object>>
            {
                { "NombreFantasia", unidad => unidad.Empresa_Unidad },
                { "CUIT", unidad => unidad.Cuit_Unidad },
                { "PatenteTractor", unidad => unidad.Tractor_Patente },
                { "PatenteSemi", unidad => unidad.Semirremolque_Patente },
            };

            var camposVencimiento = new Dictionary<string, Func<UnidadDto, DateTime?>>
            {
                { "VerifMensual", unidad => unidad.VerifMensual },
                { "Checklist", unidad => unidad.Checklist },
                { "MasYPF", unidad => unidad.MasYPF }
            };

            await _documentacionService.ExportarVencimientosAsync<UnidadDto>(
                async () => await _unidadRepositorio.ObtenerUnidadesDtoAsync(),
                rutaCsv,
                camposBase,
                camposVencimiento,
                _view.MostrarRelevamientoVencimientos
            );
        }
    }
}