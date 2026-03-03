using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas
{
    public class AgregarUnidadPresenter : BasePresenter<IAgregarUnidadView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITraficoRepositorio _traficoRepositorio;

        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        public AgregarUnidadPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IUnidadRepositorio unidadRepositorio,
            ITraficoRepositorio traficoRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio)
            : base(sesionService, navigationService)
        {
            _unidadRepositorio = unidadRepositorio;
            _traficoRepositorio = traficoRepositorio;   
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
        }

        public async Task InicializarAsync(int idEmpresa, string nombreEmpresa)
        {
            _view.MostrarEmpresa(nombreEmpresa, idEmpresa);
            await EjecutarConCargaAsync(async () =>
            {
                List<Trafico> traficos = await _traficoRepositorio.ObtenerTodosAsync();
                _view.CargarTrafico(traficos);
            });
        }

        public async Task CargarDatosPorTraficoAsync(int idTrafico)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<Shared.Models.Tractor> tractores = await _tractorRepositorio.ObtenerTractoresLibresAsync();
                List<Semi> semis = await _semiRepositorio.ObtenerSemisLibresAsync();

                // 🔥 Filtrado en capa de presentación
                List<Shared.Models.Tractor> tractoresFiltrados = tractores
                    .Where(t => t.IdTrafico == idTrafico)
                    .ToList();

                List<Semi> semisFiltrados = semis
                    .Where(s => s.IdTrafico == idTrafico)
                    .ToList();

                _view.CargarTractores(tractoresFiltrados);
                _view.CargarSemis(semisFiltrados);
            });
        }

        public async Task GuardarUnidadAsync()
        {
            if (!_view.IdTractorSeleccionado.HasValue || !_view.IdSemiSeleccionado.HasValue)
            {
                _view.MostrarMensaje("Debe seleccionar un tractor y un semirremolque.");
                return;
            }

            Shared.Models.Tractor tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(_view.IdTractorSeleccionado.Value);
            Semi semi = await _semiRepositorio.ObtenerSemiPorIdAsync(_view.IdSemiSeleccionado.Value);

            if (tractor == null || semi == null)
            {
                _view.MostrarMensaje("No se pudieron cargar correctamente los datos del tractor o del semirremolque.");
                return;
            }

            var unidad = new Unidad
            {
                IdEmpresa = _view.IdEmpresa,
                IdTractor = tractor.IdTractor,
                IdSemi = semi.IdSemi,
                TaraTotal = (int)(tractor.Tara + semi.Tara),
                IdTrafico = _view.IdTraficoSeleccionado,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _unidadRepositorio.AgregarUnidadAsync(unidad, _sesionService.IdUsuario);
                _view.MostrarMensaje("Unidad creada exitosamente.");
                _view.Cerrar();
            });
        }
    }
}