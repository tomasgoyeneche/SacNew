using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using GestionOperativa.Views.AdministracionDocumental.Altas.Tractores;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Presenters.Tractor
{
    public class AgregarEditarTractorPresenter : BasePresenter<IAgregarEditarTractorView>
    {
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IEmpresaSeguroRepositorio _empresaSeguroRepositorio;

        public AgregarEditarTractorPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ITractorRepositorio tractorRepositorio,
            IConfRepositorio confRepositorio,
            IEmpresaSeguroRepositorio empresaSeguroRepositorio)
            : base(sesionService, navigationService)
        {
            _tractorRepositorio = tractorRepositorio;
            _confRepositorio = confRepositorio;
            _empresaSeguroRepositorio = empresaSeguroRepositorio;
        }

        public async Task CargarDatosParaMostrarAsync(int idTractor)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var tractor = await _tractorRepositorio.ObtenerPorIdDtoAsync(idTractor);
                await VerificarArchivosTractorAsync(tractor.Patente, tractor.Configuracion, tractor.Modelo, tractor.Marca);
                var añosVencimiento = await ObtenerAniosVencimientoAsync();

                int añoBase = tractor.Anio.HasValue ? tractor.Anio.Value.Year : 0;
                int añoFinal = añoBase > 0 ? añoBase + añosVencimiento : 0;

                _view.MostrarDatosTractor(tractor);
                _view.MostrarVencimiento(añoFinal > 0 ? añoFinal.ToString() : "Sin fecha");

                Shared.Models.Tractor tractor1 = await _tractorRepositorio.ObtenerTractorPorIdAsync(idTractor);
                List<EmpresaSeguroDto> seguros = await _empresaSeguroRepositorio.ObtenerSeguroPorEmpresaYEntidadAsync(tractor1.IdEmpresa, 2);
                _view.MostrarSeguros(seguros);
            });
        }

        private async Task VerificarArchivosTractorAsync(string patente, string configuracion, string modelo, string marca)
        {
            var rutaFoto = await ObtenerRutaPorIdAsync(6, "Tractor", $"{patente}.jpg");
            _view.ConfigurarFotoTractor(!string.IsNullOrEmpty(rutaFoto) && File.Exists(rutaFoto), rutaFoto);

            var rutaConfig = await ObtenerRutaPorIdAsync(4, "Tractor", $"{configuracion}.bmp");
            _view.ConfigurarFotoConfiguracion(!string.IsNullOrEmpty(rutaConfig) && File.Exists(rutaConfig), rutaConfig);

            string modeloguiones = modelo.Replace(" ", "_");

            var rutaManual = await ObtenerRutaPorIdAsync(3, $"{marca}", modeloguiones + ".pdf");
            _view.ConfigurarFotoManual(!string.IsNullOrEmpty(rutaManual) && File.Exists(rutaManual), rutaManual);

            var subCarpetas = new Dictionary<string, Action<bool, string?>>
        {
            { "Cedula", _view.ConfigurarBotonCedula },
            { "Ruta", _view.ConfigurarBotonRuta },
            { "Titulo", _view.ConfigurarBotonTitulo },
            { "Vtv", _view.ConfigurarBotonVTV },
        };

            foreach (var (subCarpeta, accion) in subCarpetas)
            {
                var rutaArchivo = await ObtenerRutaPorIdAsync(5, $"Tractor\\{subCarpeta}", patente + ".pdf");
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
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(7);
            return conf != null && int.TryParse(conf.Ruta, out int años) ? años : 10; // 🔹 Si falla, por defecto 10 años
        }

        public async Task EditarDatos(int idTractor, string SatelitalNombre)
        {
            await AbrirFormularioAsync<ModificarDatosTractorForm>(async form =>
            {
                await form._presenter.InicializarAsync(idTractor, SatelitalNombre);
            });
            await CargarDatosParaMostrarAsync(idTractor); // Refrescar la vista después de agregar
        }

        public async void CambiarTransportista(int idTractor, string tipoEntidad)
        {
            await AbrirFormularioAsync<CambiarTransportistaForm>(async form =>
            {
                await form.CargarDatosAsync(idTractor, tipoEntidad);
            });

            // 🔄 Refrescar vista una vez cambiado el transportista
            await CargarDatosParaMostrarAsync(idTractor);
        }

        public async void CambiarConfiguracion(int idTractor, string entidad)
        {
            await AbrirFormularioAsync<ModificarConfiguracionSemiForm>(async form =>
            {
                await form._presenter.CargarDatosAsync(idTractor, entidad);
            });

            // 🔄 Refrescar vista una vez cambiado el transportista
            await CargarDatosParaMostrarAsync(idTractor);
        }

        public async Task EditarVencimientos(int idTractor)
        {
            await AbrirFormularioAsync<ModificarVencimientosForm>(async form =>
            {
                await form._presenter.InicializarAsync("tractor", idTractor);
            });
            await CargarDatosParaMostrarAsync(idTractor); // Refrescar la vista después de agregar
        }
    }
}