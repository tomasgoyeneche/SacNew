using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using GestionOperativa.Views.AdministracionDocumental.Altas.Choferes;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Presenters.Choferes
{
    public class AgregarEditarChoferPresenter : BasePresenter<IAgregarEditarChoferView>
    {
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaSeguroRepositorio _empresaSeguroRepositorio;

        private readonly IConfRepositorio _confRepositorio;

        public AgregarEditarChoferPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IChoferRepositorio choferRepositorio,
            IEmpresaSeguroRepositorio empresaSeguroRepositorio,
            IConfRepositorio confRepositorio)
            : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _confRepositorio = confRepositorio;
            _empresaSeguroRepositorio = empresaSeguroRepositorio;
        }

        public async Task CargarDatosParaMostrarAsync(int choferId)
        {
            await EjecutarConCargaAsync(async () =>
            {
                ChoferDto chofer = await _choferRepositorio.ObtenerPorIdDtoAsync(choferId);
                await VerificarArchivosChoferAsync(chofer.Documento);
                _view.MostrarDatosChofer(chofer);
                Chofer chofer1 = await _choferRepositorio.ObtenerPorIdAsync(choferId);
                List<EmpresaSeguroDto> choferesSeguro = await _empresaSeguroRepositorio.ObtenerSeguroPorEmpresaYEntidadAsync(chofer1.IdEmpresa, 1);
                _view.MostrarSeguros(choferesSeguro);
            });
        }

        private async Task VerificarArchivosChoferAsync(string dni)
        {
            var rutaFoto = await ObtenerRutaPorIdAsync(1, "", dni + ".jpg");
            bool fotoExiste = !string.IsNullOrEmpty(rutaFoto) && File.Exists(rutaFoto);
            _view.ConfigurarFotoChofer(fotoExiste, rutaFoto);

            var subCarpetas = new Dictionary<string, Action<bool, string?>>
        {
            { "AltaTemprana", _view.ConfigurarBotonAltaTemprana },
            { "Apto", _view.ConfigurarBotonApto },
            { "Curso", _view.ConfigurarBotonCurso },
            { "DNI", _view.ConfigurarBotonDNI },
            { "Licencia", _view.ConfigurarBotonLicencia },
            { "Seguro", _view.ConfigurarBotonSeguro }
        };

            foreach (var (subCarpeta, accion) in subCarpetas)
            {
                var rutaArchivo = await ObtenerRutaPorIdAsync(5, $"Chofer\\{subCarpeta}", dni + ".pdf");
                bool archivoExiste = !string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo);
                accion(archivoExiste, rutaArchivo);
            }
        }

        private async Task<string?> ObtenerRutaPorIdAsync(int idConf, string subDirectorio, string archivo)
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(idConf);
            return conf == null || string.IsNullOrEmpty(conf.Ruta)
                ? null
                : Path.Combine(conf.Ruta, subDirectorio, archivo);
        }

        public async Task EditarDatosChofer(int idChofer)
        {
            await AbrirFormularioAsync<ModificarDatosChoferForm>(async form =>
            {
                await form._presenter.InicializarAsync(idChofer);
            });
            await CargarDatosParaMostrarAsync(idChofer); // Refrescar la vista después de agregar
        }

        public async Task EditarVencimientos(int idChofer)
        {
            await AbrirFormularioAsync<ModificarVencimientosForm>(async form =>
            {
                await form._presenter.InicializarAsync("chofer", idChofer);
            });
            await CargarDatosParaMostrarAsync(idChofer); // Refrescar la vista después de agregar
        }

        public async void CambiarTransportista(int idChofer, string tipoEntidad)
        {
            await AbrirFormularioAsync<CambiarTransportistaForm>(async form =>
            {
                await form.CargarDatosAsync(idChofer, tipoEntidad);
            });

            // 🔄 Refrescar vista una vez cambiado el transportista
            await CargarDatosParaMostrarAsync(idChofer);
        }
    }
}