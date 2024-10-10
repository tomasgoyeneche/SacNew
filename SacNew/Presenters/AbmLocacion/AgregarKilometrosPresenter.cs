using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;

namespace SacNew.Presenters
{
    public class AgregarKilometrosPresenter
    {
        private IAgregarKilometrosView _view;
        private readonly ILocacionKilometrosEntreRepositorio _kilometrosRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private int _idLocacionOrigen;

        public AgregarKilometrosPresenter(
            ILocacionKilometrosEntreRepositorio kilometrosRepositorio,
            ILocacionRepositorio locacionRepositorio)
        {
            _kilometrosRepositorio = kilometrosRepositorio;
            _locacionRepositorio = locacionRepositorio;
        }

        public void SetView(IAgregarKilometrosView view)
        {
            _view = view;
        }

        public async Task InicializarAsync(int idLocacionOrigen)
        {
            _idLocacionOrigen = idLocacionOrigen;

            // Obtener todas las locaciones excepto la de origen
            var locaciones = await _locacionRepositorio.ObtenerTodasAsync();
            locaciones = locaciones.Where(l => l.IdLocacion != idLocacionOrigen).ToList();

            _view.CargarLocaciones(locaciones);
        }

        public async Task GuardarKilometrosAsync()
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
        }
    }
}