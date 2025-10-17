using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimientos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class EditarOrdenTrabajoPresenter : BasePresenter<IEditarOrdenTrabajoView>
    {
        private readonly IOrdenTrabajoRepositorio _ordenRepositorio;
        private readonly ILugarReparacionRepositorio _lugarRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IOrdenTrabajoComprobanteRepositorio _ordenTrabajoComprobanteRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;

        private OrdenTrabajo? _ordenActual;

        public EditarOrdenTrabajoPresenter(
            IOrdenTrabajoRepositorio ordenRepositorio,
            ILugarReparacionRepositorio lugarRepositorio,
            IOrdenTrabajoComprobanteRepositorio ordenTrabajoComprobanteRepositorio,
            IUnidadRepositorio unidadRepositorio,
            INominaRepositorio nominaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _ordenRepositorio = ordenRepositorio;
            _lugarRepositorio = lugarRepositorio;
            _ordenTrabajoComprobanteRepositorio = ordenTrabajoComprobanteRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _nominaRepositorio = nominaRepositorio;
        }

        public async Task InicializarAsync(int idOrdenTrabajo)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                _view.CargarUnidades(unidades);

                List<LugarReparacion> lugares = await _lugarRepositorio.ObtenerTodosAsync();
                _view.CargarLugares(lugares);

                List<OrdenTrabajoComprobante> comprobantes = await _ordenTrabajoComprobanteRepositorio.ObtenerPorMovimientoAsync(idOrdenTrabajo);
                _view.CargarComprobantes(comprobantes);

                _ordenActual = await _ordenRepositorio.ObtenerPorIdAsync(idOrdenTrabajo);

                if (_ordenActual == null)
                    throw new Exception("No se encontró la orden de trabajo.");

                _view.IdOrdenTrabajo = _ordenActual.IdOrdenTrabajo;
                _view.FechaEmision = _ordenActual.FechaEmision;
                _view.IdLugarReparacion = _ordenActual.IdLugarReparacion;
                _view.Observaciones = _ordenActual.Observaciones ?? "";
                _view.FechaIngreso = _ordenActual.FechaInicio;
                _view.FechaFin = _ordenActual.FechaFin;
                _view.OdometroIngreso = _ordenActual.OdometroIngreso;
                _view.OdometroSalida = _ordenActual.OdometroSalida;

                // 🔹 Buscar unidad en base a la nómina actual
                if (_ordenActual.IdNomina != null)
                {
                    Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(_ordenActual.IdNomina.Value);
                    _view.IdNomina = nomina?.IdNomina;
                    _view.IdUnidad = nomina?.IdUnidad;
                }

                // 🔹 Habilitar/deshabilitar botones según estado
                if (_ordenActual.IdNomina != null)
                {
                    var nomina = await _nominaRepositorio.ObtenerPorIdAsync(_ordenActual.IdNomina.Value);
                    _view.IdNomina = nomina?.IdNomina;
                    _view.IdUnidad = nomina?.IdUnidad;
                }

                // 🔹 Actualizar estado general de la interfaz
                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }

        public async Task AutorizarAsync(int idUnidad)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idUnidad, DateTime.Now);
                if (nomina == null)
                {
                    _view.MostrarMensaje("No se encontró una nómina activa para la unidad seleccionada.");
                    return;
                }

                _ordenActual!.IdNomina = nomina.IdNomina;
                _ordenActual.Fase = 1; // Autorizada
                await _ordenRepositorio.ActualizarAsync(_ordenActual);

                _view.IdNomina = nomina.IdNomina;
                _view.MostrarMensaje("Orden de trabajo autorizada correctamente.");

                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }
        public async Task ConfirmarIngresoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                decimal? odometroIngreso = await _nominaRepositorio.ObtenerOdometerPorNomina(_ordenActual.IdNomina!.Value);
                _ordenActual.FechaInicio = DateTime.Now;
                _ordenActual.IdLugarReparacion = _view.IdLugarReparacion;
                _ordenActual.OdometroIngreso = odometroIngreso;
                _ordenActual.Fase = 2; // En Taller

                await _ordenRepositorio.ActualizarAsync(_ordenActual);

                _view.FechaIngreso = _ordenActual.FechaInicio;
                _view.OdometroIngreso = odometroIngreso;
                _view.MostrarMensaje("Ingreso confirmado correctamente.");

                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }

        public async Task ConfirmarSalidaAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                decimal? odometroSalida = await _nominaRepositorio.ObtenerOdometerPorNomina(_ordenActual.IdNomina!.Value);
                _ordenActual.FechaFin = DateTime.Now;
                _ordenActual.OdometroSalida = odometroSalida;
                _ordenActual.Fase = 3; // Finalizada

                await _ordenRepositorio.ActualizarAsync(_ordenActual);

                _view.FechaFin = _ordenActual.FechaFin;
                _view.OdometroSalida = odometroSalida;
                _view.MostrarMensaje("Orden de trabajo finalizada correctamente.");

                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }
    }
}
