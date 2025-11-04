using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimientos;
using Shared.Models;

namespace Servicios.Presenters
{
    public class ListadoOrdenTrabajoPresenter : BasePresenter<IListadoOrdenTrabajoView>
    {
        private readonly IOrdenTrabajoRepositorio _orderTrabajoRepositorio;
        private readonly IOrdenTrabajoMantenimientoRepositorio _ordenTrabajoMantenimientoRepositorio;
        private readonly IOrdenTrabajoTareaRepositorio _ordenTrabajoTareaRepositorio;
        private readonly IOrdenTrabajoArticuloRepositorio _ordenTrabajoArticuloRepositorio;
        public string _Criterio;

        public ListadoOrdenTrabajoPresenter(
            IOrdenTrabajoRepositorio orderTrabajoRepositorio,
            IOrdenTrabajoMantenimientoRepositorio ordenTrabajoMantenimientoRepositorio,
            IOrdenTrabajoTareaRepositorio ordenTrabajoTareaRepositorio,
            IOrdenTrabajoArticuloRepositorio ordenTrabajoArticuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _orderTrabajoRepositorio = orderTrabajoRepositorio;
            _ordenTrabajoMantenimientoRepositorio = ordenTrabajoMantenimientoRepositorio;
            _ordenTrabajoTareaRepositorio = ordenTrabajoTareaRepositorio;
            _ordenTrabajoArticuloRepositorio = ordenTrabajoArticuloRepositorio;
        }

        public async Task<int> CrearOrdenAsync()
        {
            var ordenTrabajo = new OrdenTrabajo
            {
                FechaEmision = DateTime.Now,
                FechaFin = null,
                FechaInicio = null,
                IdNomina = null,
                OdometroIngreso = null,
                OdometroSalida = null,
                HorasEstimadas = null,
                CostoEstimado = null,
                Fase = 0, // Nueva
                IdLugarReparacion = null,
                Observaciones = null,
                Activo = true
            };

            return await _orderTrabajoRepositorio.AgregarAsync(ordenTrabajo);
        }

        public async Task AbrirEdicionMovimientoAsync(int idOrdenTrabajo)
        {
            await AbrirFormularioAsync<EditarOrdenTrabajoForm>(async form =>
            {
                await form._presenter.InicializarAsync(idOrdenTrabajo);
            });

            await InicializarAsync(_Criterio);
        }

        public async Task EliminarOrdenAsync(int idOrdenTrabajo)
        {
            await _orderTrabajoRepositorio.EliminarAsync(idOrdenTrabajo);
            foreach (var ordenMantenimiento in await _ordenTrabajoMantenimientoRepositorio.ObtenerPorOrdenTrabajoAsync(idOrdenTrabajo))
            {
                await _ordenTrabajoMantenimientoRepositorio.EliminarAsync(ordenMantenimiento.IdOrdenTrabajoMantenimiento);
                foreach (var tarea in await _ordenTrabajoTareaRepositorio.ObtenerPorMantenimientoAsync(ordenMantenimiento.IdOrdenTrabajoMantenimiento))
                {
                    await _ordenTrabajoTareaRepositorio.EliminarAsync(tarea.IdOrdenTrabajoTarea);
                    foreach (var articulo in await _ordenTrabajoArticuloRepositorio.ObtenerPorTareaAsync(tarea.IdOrdenTrabajoTarea))
                    {
                        await _ordenTrabajoArticuloRepositorio.EliminarAsync(articulo.IdOrdenTrabajoArticulo);
                        // Aquí podrías agregar lógica para reestablecer el stock si es necesario
                    }
                }
            }
            _view.MostrarMensaje("Orden de trabajo eliminada correctamente recuerda reestablecer el stock en caso de moverlo en el mantenimiento.");
            await InicializarAsync(_Criterio);
        }

        public async Task InicializarAsync(string criterio)
        {
            _Criterio = criterio;
            await EjecutarConCargaAsync(async () =>
            {
                List<OrdenTrabajoDto> ordenes;
                if (_Criterio == "Todos")
                {
                    ordenes = await _orderTrabajoRepositorio.ObtenerTodosDtoAsync();
                }
                else
                {
                    ordenes = await _orderTrabajoRepositorio.ObtenerPorFaseAsync("Finalizada");
                }
                _view.MostrarOrdenesDeTrabajo(ordenes);
            });
        }
    }
}