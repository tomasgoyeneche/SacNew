using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using GestionOperativa.Views.AdministracionDocumental.Altas.Semis;
using GestionOperativa.Views.AdministracionDocumental.Altas.Tractores;
using System.IO;

namespace GestionOperativa.Presenters
{
    public class AgregarEditarSemiPresenter : BasePresenter<IAgregarEditarSemiView>
    {
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IConfRepositorio _confRepositorio;

        public AgregarEditarSemiPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ISemiRepositorio semiRepositorio,
            IConfRepositorio confRepositorio)
            : base(sesionService, navigationService)
        {
            _semiRepositorio = semiRepositorio;
            _confRepositorio = confRepositorio;
        }

        public async Task CargarDatosParaMostrarAsync(int idSemi)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var semi = await _semiRepositorio.ObtenerPorIdDtoAsync(idSemi);
                await VerificarArchivosSemiAsync(semi.Patente, semi.Configuracion);
                var añosVencimiento = await ObtenerAniosVencimientoAsync();

                int añoBase = semi.Anio.HasValue ? semi.Anio.Value.Year : 0;
                int añoFinal = añoBase > 0 ? añoBase + añosVencimiento : 0;

                _view.MostrarDatosSemi(semi);
                _view.MostrarVencimiento(añoFinal > 0 ? añoFinal.ToString() : "Sin fecha");
            });
        }

        private async Task VerificarArchivosSemiAsync(string patente, string configuracion)
        {
            var rutaFoto = await ObtenerRutaPorIdAsync(6, "Semi", $"{patente}.jpg");
            _view.ConfigurarFotoSemi(!string.IsNullOrEmpty(rutaFoto) && File.Exists(rutaFoto), rutaFoto);

            var rutaConfig = await ObtenerRutaPorIdAsync(4, "Semi", $"{configuracion}.bmp");
            _view.ConfigurarFotoConfiguracion(!string.IsNullOrEmpty(rutaConfig) && File.Exists(rutaConfig), rutaConfig);

            var subCarpetas = new Dictionary<string, Action<bool, string?>>
            {
            { "Cedula", _view.ConfigurarBotonCedula },
            { "Ruta", _view.ConfigurarBotonRuta },
            { "Titulo", _view.ConfigurarBotonTitulo },
            { "Vtv", _view.ConfigurarBotonVTV },
            { "Litros_Nominales", _view.ConfigurarBotonLitrosNominales },
            { "Cubicacion", _view.ConfigurarBotonCubicacion },
            { "Espesor", _view.ConfigurarBotonEspesor },
            { "Visual_Externa", _view.ConfigurarBotonVisualExt },
            { "Visual_Interna", _view.ConfigurarBotonVisualInt },
            { "Inv", _view.ConfigurarBotonInv },
            { "Estanqueidad", _view.ConfigurarBotonEstanqueidad },
            };

            foreach (var (subCarpeta, accion) in subCarpetas)
            {
                var rutaArchivo = await ObtenerRutaPorIdAsync(5, $"Semi\\{subCarpeta}", patente + ".pdf");
                bool archivoExiste = !string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo);
                accion(archivoExiste, rutaArchivo);
            }
        }

        private async Task<string?> ObtenerRutaPorIdAsync(int idConf, string subDirectorio, string archivo)
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(idConf);
            return conf?.Ruta != null ? Path.Combine(conf.Ruta, subDirectorio, archivo) : null;
        }

        private async Task<int> ObtenerAniosVencimientoAsync()
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(8);
            return conf != null && int.TryParse(conf.Ruta, out int años) ? años : 16; // 🔹 Si falla, por defecto 10 años
        }

        public async Task EditarDatos(int idSemi)
        {
            await AbrirFormularioAsync<ModificarDatosSemiForm>(async form =>
            {
                await form._presenter.InicializarAsync(idSemi);
            });
            await CargarDatosParaMostrarAsync(idSemi); // Refrescar la vista después de agregar
        }

        public async void CambiarTransportista(int idSemi, string tipoEntidad)
        {
            await AbrirFormularioAsync<CambiarTransportistaForm>(async form =>
            {
                await form.CargarDatosAsync(idSemi, tipoEntidad);
            });

            // 🔄 Refrescar vista una vez cambiado el transportista
            await CargarDatosParaMostrarAsync(idSemi);
        }

        public async void CambiarConfiguracion(int idSemi, string entidad)
        {
            await AbrirFormularioAsync<ModificarConfiguracionSemiForm>(async form =>
            {
                await form._presenter.CargarDatosAsync(idSemi, entidad);
            });

            // 🔄 Refrescar vista una vez cambiado el transportista
            await CargarDatosParaMostrarAsync(idSemi);
        }
    }
}