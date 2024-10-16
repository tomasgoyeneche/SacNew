using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Presenters
{
    public class AgregarKilometrosPresenter : BasePresenter<IAgregarKilometrosView>
    {
        private readonly ILocacionKilometrosEntreRepositorio _kilometrosRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private int _idLocacionOrigen;

        public AgregarKilometrosPresenter(
            ILocacionKilometrosEntreRepositorio kilometrosRepositorio,
            ILocacionRepositorio locacionRepositorio,
            ISesionService sesionService,
            IServiceProvider serviceProvider
        ) : base(sesionService, serviceProvider)
        {
            _kilometrosRepositorio = kilometrosRepositorio;
            _locacionRepositorio = locacionRepositorio;
        }

        public async Task InicializarAsync(int idLocacionOrigen)
        {
            _idLocacionOrigen = idLocacionOrigen;

            await EjecutarConCargaAsync(async () =>
            {
                // Obtener todas las locaciones excepto la de origen
                var locaciones = await _locacionRepositorio.ObtenerTodasAsync();
                locaciones = locaciones.Where(l => l.IdLocacion != idLocacionOrigen).ToList();
                _view.CargarLocaciones(locaciones);
            });
        }

        public async Task GuardarKilometrosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (_view.Kilometros <= 0)
                {
                    _view.MostrarMensaje("La distancia en kilómetros debe ser mayor a 0.");
                    return;
                }

                var nuevoKilometro = new LocacionKilometrosEntre
                {
                    IdLocacionOrigen = _idLocacionOrigen,
                    IdLocacionDestino = _view.IdLocacionDestino,
                    Kilometros = _view.Kilometros
                };

                await _kilometrosRepositorio.AgregarAsync(nuevoKilometro);
                _view.MostrarMensaje("Kilómetros guardados correctamente.");
            });
        }
    }
}