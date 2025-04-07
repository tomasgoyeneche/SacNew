using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas
{
    public class CambiarTransportistaPresenter : BasePresenter<ICambiarTransportistaView>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        public CambiarTransportistaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaRepositorio empresaRepositorio,
            IChoferRepositorio choferRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio)
            : base(sesionService, navigationService)
        {
            _empresaRepositorio = empresaRepositorio;
            _choferRepositorio = choferRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
        }

        public async Task CargarDatosAsync(int idEntidad, string tipoEntidad)
        {
            var empresas = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
            _view.CargarEmpresas(empresas);

            int idEmpresaActual = tipoEntidad.ToLower() switch
            {
                "chofer" => (await _choferRepositorio.ObtenerPorIdAsync(idEntidad))?.IdEmpresa ?? 0,
                "tractor" => (await _tractorRepositorio.ObtenerTractorPorIdAsync(idEntidad))?.IdEmpresa ?? 0,
                "semi" => (await _semiRepositorio.ObtenerSemiPorIdAsync(idEntidad))?.IdEmpresa ?? 0,
                _ => 0
            };

            _view.SeleccionarEmpresaActual(idEmpresaActual);
        }

        public async Task GuardarAsync(int idEntidad, string tipoEntidad)
        {
            int idEmpresaNueva = _view.IdEmpresaSeleccionada;

            await EjecutarConCargaAsync(async () =>
            {
                switch (tipoEntidad.ToLower())
                {
                    case "chofer":
                        await _choferRepositorio.ActualizarEmpresaChoferAsync(idEntidad, idEmpresaNueva);
                        break;

                    case "tractor":
                        await _tractorRepositorio.ActualizarEmpresaTractorAsync(idEntidad, idEmpresaNueva);
                        break;

                    case "semi":
                        await _semiRepositorio.ActualizarEmpresaSemiAsync(idEntidad, idEmpresaNueva);
                        break;

                    default:
                        _view.MostrarMensaje("Entidad no reconocida.");
                        return;
                }

                _view.MostrarMensaje("Transportista actualizado correctamente.");
                _view.Cerrar();
            });
        }
    }
}