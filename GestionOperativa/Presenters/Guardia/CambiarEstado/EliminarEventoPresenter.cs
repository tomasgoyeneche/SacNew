using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class EliminarEventoPresenter : BasePresenter<IEliminarEventoView>
    {
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private GuardiaDto _guardia;

        public EliminarEventoPresenter(
            ISesionService sesionService,
            INavigationService nav,
            IGuardiaRepositorio guardiaRepositorio)
            : base(sesionService, nav)
        {
            _guardiaRepositorio = guardiaRepositorio;
        }

        public async Task InicializarAsync(GuardiaDto guardia)
        {
            _guardia = guardia;
            var historial = await _guardiaRepositorio.ObtenerHistorialPorIngresoAsync(guardia.IdGuardiaIngreso);
            _view.MostrarHistorial(historial);
        }

        public async Task EliminarRegistroAsync(GuardiaHistorialDto registro)
        {
            if (registro.IdGuardiaEstado == 1)
            {
                // Eliminar todo si es el ingreso
                await _guardiaRepositorio.EliminarIngresoCompletoAsync(_guardia.IdGuardiaIngreso);
                _view.MostrarMensaje("Ingreso eliminado completamente.");
            }
            else
            {
                await _guardiaRepositorio.EliminarRegistroAsync(registro.IdGuardiaRegistro);

                // Verificar si el estado actual coincide con el eliminado
                if (_guardia.IdEstadoEvento == registro.IdGuardiaEstado)
                {
                    var historial = await _guardiaRepositorio.ObtenerHistorialPorIngresoAsync(_guardia.IdGuardiaIngreso);
                    var ultimo = historial
                        .Where(h => h.IdGuardiaRegistro != registro.IdGuardiaRegistro)
                        .OrderByDescending(h => h.FechaGuardia)
                        .FirstOrDefault();

                    if (ultimo != null)
                    {
                        await _guardiaRepositorio.ActualizarEstadoIngresoAsync(_guardia.IdGuardiaIngreso, ultimo.IdGuardiaEstado);
                    }
                }

                _view.MostrarMensaje("Registro eliminado correctamente.");
            }

            _view.Close();
        }
    }
}