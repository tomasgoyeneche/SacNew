using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Unidades;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class AgregarEditarUnidadPresenter : BasePresenter<IAgregarEditarUnidadView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IConfRepositorio _confRepositorio;

        public AgregarEditarUnidadPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IUnidadRepositorio unidadRepositorio,
            IConfRepositorio confRepositorio)
            : base(sesionService, navigationService)
        {
            _unidadRepositorio = unidadRepositorio;
            _confRepositorio = confRepositorio;
        }

        public async Task CargarDatosUnidadAsync(int idUnidad)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var unidad = await _unidadRepositorio.ObtenerPorIdDtoAsync(idUnidad);
                if (unidad == null)
                {
                    _view.MostrarMensaje("No se encontró la unidad.");
                    return;
                }

                _view.MostrarDatosUnidad(unidad);

                await VerificarArchivosUnidadAsync(unidad);
            });
        }

        private async Task VerificarArchivosUnidadAsync(UnidadDto unidad)
        {
            string patenteUnidad = $"{unidad.Tractor_Patente}_{unidad.Semirremolque_Patente}";

            // 📌 Foto de la Unidad
            var rutaFoto = await ObtenerRutaPorIdAsync(6, "Nomina", $"{patenteUnidad}.jpg");
            _view.ConfigurarFotoUnidad(!string.IsNullOrEmpty(rutaFoto) && File.Exists(rutaFoto), rutaFoto);

            // 📌 Imagen Configuración Tractor
            var rutaConfigTractor = await ObtenerRutaPorIdAsync(4, "Tractor", $"{unidad.Tractor_Configuracion}.bmp");
            _view.ConfigurarFotoConfiguracionTractor(!string.IsNullOrEmpty(rutaConfigTractor) && File.Exists(rutaConfigTractor), rutaConfigTractor);

            // 📌 Imagen Configuración Semirremolque
            var rutaConfigSemi = await ObtenerRutaPorIdAsync(4, "Semi", $"{unidad.Semirremolque_Configuracion}.bmp");
            _view.ConfigurarFotoConfiguracionSemi(!string.IsNullOrEmpty(rutaConfigSemi) && File.Exists(rutaConfigSemi), rutaConfigSemi);

            // 📌 Documentos PDF (Se buscan por patenteUnidad)
            var subCarpetas = new Dictionary<string, Action<bool, string?>>
            {
                { "Tara", _view.ConfigurarBotonTaraTotal },
            { "Mas", _view.ConfigurarBotonMasYPF },
            { "Checklist", _view.ConfigurarBotonChecklist },
            { "verfmensual", _view.ConfigurarBotonCalibrado }
        };

            foreach (var (subCarpeta, accion) in subCarpetas)
            {
                var rutaArchivo = await ObtenerRutaPorIdAsync(5, $"Nomina\\{subCarpeta}", patenteUnidad + ".pdf");
                bool archivoExiste = !string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo);
                accion(archivoExiste, rutaArchivo);
            }
        }

        private async Task<string?> ObtenerRutaPorIdAsync(int idConf, string subDirectorio, string archivo)
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(idConf);
            return conf?.Ruta != null ? Path.Combine(conf.Ruta, subDirectorio, archivo) : null;
        }
    }
}