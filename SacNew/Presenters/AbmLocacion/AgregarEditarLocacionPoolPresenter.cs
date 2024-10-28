using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.Configuraciones.AbmLocaciones;

namespace SacNew.Presenters
{
    public class AgregarEditarLocacionPoolPresenter : BasePresenter<IAgregarEditarLocacionPoolView>
    {
        private readonly ILocacionPoolRepositorio _locacionPoolRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private int _idPool;

        public AgregarEditarLocacionPoolPresenter(
            ILocacionPoolRepositorio locacionPoolRepositorio,
            ILocacionRepositorio locacionRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _locacionPoolRepositorio = locacionPoolRepositorio;
            _locacionRepositorio = locacionRepositorio;
        }

        public async Task InicializarAsync(int idPool)
        {
            _idPool = idPool;

            await EjecutarConCargaAsync(async () =>
            {
                var todasLasLocaciones = await _locacionRepositorio.ObtenerTodasAsync();
                var locacionesAsignadas = await _locacionPoolRepositorio.ObtenerPorIdPoolAsync(idPool);

                var idsAsignadas = locacionesAsignadas.Select(l => l.IdLocacion).ToHashSet();
                var locacionesDisponibles = todasLasLocaciones.Where(l => !idsAsignadas.Contains(l.IdLocacion));

                _view.CargarLocacionesDisponibles(locacionesDisponibles);
                _view.CargarLocacionesAsignadas(locacionesAsignadas);
            });
        }

        public async Task AgregarLocacionAlPoolAsync()
        {
            var locacionSeleccionada = _view.LocacionSeleccionadaDisponible;
            if (locacionSeleccionada == null)
            {
                _view.MostrarMensaje("Debe seleccionar una locación disponible.");
                return;
            }

            var locacionPool = new LocacionPool
            {
                IdPool = _idPool,
                IdLocacion = locacionSeleccionada.IdLocacion,
                Activo = true
            };

            await EjecutarConCargaAsync(
                () => _locacionPoolRepositorio.AgregarLocacionAlPoolAsync(locacionPool),
                async () => await InicializarAsync(_idPool)
            );
        }

        public async Task EliminarLocacionDelPoolAsync()
        {
            var locacionSeleccionada = _view.LocacionSeleccionadaAsignada;
            if (locacionSeleccionada == null)
            {
                _view.MostrarMensaje("Debe seleccionar una locación asignada.");
                return;
            }

            var locacionPool = await _locacionPoolRepositorio.ObtenerRelacionAsync(_idPool, locacionSeleccionada.IdLocacion);
            if (locacionPool == null)
            {
                _view.MostrarMensaje("No se encontró la relación para eliminar.");
                return;
            }

            await EjecutarConCargaAsync(
                () => _locacionPoolRepositorio.EliminarLocacionDelPoolAsync(locacionPool.IdLocacionPool),
                async () => await InicializarAsync(_idPool)
            );
        }
    }
}