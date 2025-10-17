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
    public class AgregarEditarTareaPresenter : BasePresenter<IAgregarEditarTareaView>
    {
        private readonly ITareaRepositorio _tareaRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _tareaArticuloRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;

        private Tarea? _tarea;
        private string _tipoVista;

        public AgregarEditarTareaPresenter(
            ITareaRepositorio tareaRepositorio,
            IMantenimientoTareaArticuloRepositorio tareaArticuloRepositorio,
            IArticuloRepositorio articuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _tareaRepositorio = tareaRepositorio;
            _tareaArticuloRepositorio = tareaArticuloRepositorio;
            _articuloRepositorio = articuloRepositorio;
        }

        public async Task InicializarAsync(string tipoVista, int idTarea)
        {
            _tipoVista = tipoVista;
            _view.IdTarea = idTarea;

            await EjecutarConCargaAsync(async () =>
            {
                // 🔹 Obtener la tarea
                _tarea = await _tareaRepositorio.ObtenerPorIdAsync(idTarea);
                if (_tarea == null)
                {
                    _view.MostrarMensaje("No se encontró la tarea especificada.");
                    return;
                }

                _view.Nombre = _tarea.Nombre;
                _view.Descripcion = _tarea.Descripcion;
                _view.Horas = _tarea.Horas ?? 0;
                _view.ManoObra = _tarea.ManoObra ?? 0;

                // 🔹 Cargar artículos disponibles y asociados
                var articulos = await _articuloRepositorio.ObtenerArticulosActivosAsync();
                _view.CargarArticulos(articulos);

                var articulosAsociados = await _tareaArticuloRepositorio.ObtenerPorTareaAsync(idTarea);
                var listaDto = new List<TareaArticuloDto>();

                foreach (var ta in articulosAsociados)
                {
                    var art = await _articuloRepositorio.ObtenerPorIdAsync(ta.IdArticulo);
                    if (art != null)
                    {
                        listaDto.Add(new TareaArticuloDto
                        {
                            IdArticulo = art.IdArticulo,
                            Codigo = art.Codigo,
                            Descripcion = art.Descripcion,
                            Cantidad = ta.Cantidad,
                            PrecioUnitario = art.PrecioUnitario
                        });
                    }
                }

                _view.CargarArticulosAsociados(listaDto);

                // 🔹 Calcular el total de repuestos
                decimal totalRepuestos = listaDto.Sum(x => x.PrecioTotal);
                _view.Repuestos = totalRepuestos;
            });
        }

        public async Task AgregarArticuloAsync()
        {
            if (_view.IdArticuloSeleccionado == 0 || _view.CantidadArticulo <= 0)
            {
                _view.MostrarMensaje("Debe seleccionar un artículo y una cantidad válida.");
                return;
            }

            var existente = await _tareaArticuloRepositorio.ObtenerPorTareaYArticuloAsync(_view.IdTarea, _view.IdArticuloSeleccionado);
            if (existente != null)
            {
                _view.MostrarMensaje("El artículo ya está agregado a esta tarea.");
                return;
            }

            MantenimientoTareaArticulo entidad = new MantenimientoTareaArticulo
            {
                IdTarea = _view.IdTarea,
                IdArticulo = _view.IdArticuloSeleccionado,
                Cantidad = _view.CantidadArticulo,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _tareaArticuloRepositorio.AgregarAsync(entidad);
                await InicializarAsync(_tipoVista, _view.IdTarea);
            });
        }

        public async Task EliminarArticuloAsync(int idArticulo)
        {
            var existentes = await _tareaArticuloRepositorio.ObtenerPorTareaAsync(_view.IdTarea);
            var registro = existentes.FirstOrDefault(x => x.IdArticulo == idArticulo);

            if (registro != null)
            {
                await _tareaArticuloRepositorio.EliminarAsync(registro.IdMantenimientoTareaArticulo);
                await InicializarAsync(_tipoVista, _view.IdTarea);
            }
        }

        public async Task GuardarAsync()
        {
            if (_tarea == null)
            {
                _view.MostrarMensaje("No se pudo guardar: la tarea no está inicializada.");
                return;
            }

            _tarea.Horas = _view.Horas;
            _tarea.ManoObra = _view.ManoObra;

            await EjecutarConCargaAsync(async () =>
            {
                await _tareaRepositorio.ActualizarAsync(_tarea);
                _view.MostrarMensaje("Tarea actualizada correctamente.");
                _view.Cerrar();
            });
        }
    }
}
