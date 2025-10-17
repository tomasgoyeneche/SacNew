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
    public class MenuCrearMantenimientoPresenter : BasePresenter<IMenuCrearMantenimientoView>
    {
        private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
        private readonly ITipoMantenimientoRepositorio _tipoRepositorio;
        private readonly ITareaRepositorio _tareaRepositorio;
        private readonly IMantenimientoTareaRepositorio _mantenimientoTareaRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _mantenimientoTareaArticuloRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;

        private Shared.Models.Mantenimiento? _mantenimiento;
        private string? _tipoVista;

        public MenuCrearMantenimientoPresenter(
            IMantenimientoRepositorio mantenimientoRepositorio,
            ITipoMantenimientoRepositorio tipoRepositorio,
            ITareaRepositorio tareaRepositorio,
            IMantenimientoTareaRepositorio mantenimientoTareaRepositorio,
            IMantenimientoTareaArticuloRepositorio mantenimientoTareaArticuloRepositorio,
            IArticuloRepositorio articuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _mantenimientoRepositorio = mantenimientoRepositorio;
            _tipoRepositorio = tipoRepositorio;
            _tareaRepositorio = tareaRepositorio;
            _mantenimientoTareaRepositorio = mantenimientoTareaRepositorio;
            _mantenimientoTareaArticuloRepositorio = mantenimientoTareaArticuloRepositorio;
            _articuloRepositorio = articuloRepositorio;
        }

        public async Task InicializarAsync(string tipoVista, int idMantenimiento)
        {
            _tipoVista = tipoVista;

            await EjecutarConCargaAsync(async () =>
            {
                List<TipoMantenimiento> tipos = await _tipoRepositorio.ObtenerTodosAsync();
                _view.CargarTiposMantenimiento(tipos);
                _view.CargarAplicaA();

                if (_tipoVista == "MantenimientoPredefinido")
                    _view.CargarFrecuencias();
                else
                    _view.OcultarFrecuencias();

                // 🔹 1. Obtener todas las tareas activas
                List<Tarea> todasLasTareas = await _tareaRepositorio.ObtenerTodosAsync();

                // 🔹 2. Si estamos editando un mantenimiento, filtramos las ya agregadas
                if (idMantenimiento != 0)
                {
                    List<MantenimientoTarea> tareasAsociadas = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
                    var idsTareasAsociadas = tareasAsociadas.Select(t => t.IdTarea).ToHashSet();

                    // 🔹 3. Filtrar las tareas no asociadas
                    todasLasTareas = todasLasTareas
                        .Where(t => !idsTareasAsociadas.Contains(t.IdTarea))
                        .OrderBy(t => t.Nombre)
                        .ToList();
                }

                // 🔹 4. Cargar el combo con las tareas filtradas
                _view.CargarTareasPredefinidas(todasLasTareas);

                if (idMantenimiento != 0)
                {
                    _mantenimiento = await _mantenimientoRepositorio.ObtenerPorIdAsync(idMantenimiento);
                    if (_mantenimiento != null)
                    {
                        _view.IdMantenimiento = _mantenimiento.IdMantenimiento;
                        _view.Nombre = _mantenimiento.Nombre;
                        _view.IdTipoMantenimiento = _mantenimiento.IdTipoMantenimiento;
                        _view.AplicaA = _mantenimiento.AplicaA;
                        _view.Descripcion = _mantenimiento.Descripcion;
                        _view.KilometrosIntervalo = _mantenimiento.KilometrosIntervalo;
                        _view.DiasIntervalo = _mantenimiento.DiasIntervalo;

                        string frecuenciaInicial;
                        int? valorIntervalo;
                        if (_mantenimiento.DiasIntervalo.HasValue && _mantenimiento.DiasIntervalo.Value > 0)
                        {
                            frecuenciaInicial = "Días";
                            valorIntervalo = _mantenimiento.DiasIntervalo;
                        } 
                        else if (_mantenimiento.KilometrosIntervalo.HasValue && _mantenimiento.KilometrosIntervalo.Value > 0)
                        {
                            frecuenciaInicial = "Kilómetros";
                            valorIntervalo = _mantenimiento.KilometrosIntervalo;
                        }
                        else
                        {
                            frecuenciaInicial = "Indefinido";
                            valorIntervalo = null;
                        }

                        _view.SeleccionarFrecuencia(frecuenciaInicial, valorIntervalo);

                        // cargar tareas asociadas
                        var tareasAsociadas = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
                        var listaTareas = new List<Tarea>();

                        foreach (var t in tareasAsociadas)
                        {
                            var tarea = await _tareaRepositorio.ObtenerPorIdAsync(t.IdTarea);
                            if (tarea != null) listaTareas.Add(tarea);
                        }

                        _view.CargarTareasAsignadas(listaTareas);

                        // Calcular totales de Horas, Mano de Obra y Repuestos
                        await CalcularTotalesAsync(listaTareas);
                    }
                }
            });
        }

        private async Task CalcularTotalesAsync(List<Tarea> tareas)
        {
            decimal totalHoras = tareas.Sum(t => t.Horas ?? 0);
            decimal totalManoObra = tareas.Sum(t => t.ManoObra ?? 0);
            decimal totalRepuestos = 0;

            foreach (var tarea in tareas)
            {
                var articulosAsociados = await _mantenimientoTareaArticuloRepositorio.ObtenerPorTareaAsync(tarea.IdTarea);
                foreach (var item in articulosAsociados)
                {
                    var articulo = await _articuloRepositorio.ObtenerPorIdAsync(item.IdArticulo);
                    if (articulo != null)
                    {
                        totalRepuestos += (articulo.PrecioUnitario * item.Cantidad);
                    }
                }
            }

            _view.HorasTotales = totalHoras;
            _view.ManoObraTotal = totalManoObra;
            _view.RepuestosTotales = totalRepuestos;
        }

        public async Task GuardarAsync()
        {
            // según el tipo de mantenimiento, actuamos distinto
            if (_tipoVista == "MantenimientoProceso")
            {
                // el comportamiento de proceso se define más adelante
                _view.MostrarMensaje("Guardado para MantenimientoProceso pendiente de implementar.");
                return;
            }

            var mantenimiento = new Shared.Models.Mantenimiento
            {
                IdMantenimiento = _mantenimiento?.IdMantenimiento ?? 0,
                Nombre = _view.Nombre,
                IdTipoMantenimiento = _view.IdTipoMantenimiento,
                AplicaA = _view.AplicaA,
                Descripcion = _view.Descripcion,
                Activo = true,
                KilometrosIntervalo = _view.KilometrosIntervalo,
                DiasIntervalo = _view.DiasIntervalo
            };

            await EjecutarConCargaAsync(async () =>
            {
                if (_mantenimiento == null)
                {
                    var id = await _mantenimientoRepositorio.AgregarAsync(mantenimiento);
                    mantenimiento.IdMantenimiento = id;
                    _view.MostrarMensaje("Mantenimiento agregado correctamente.");
                }
                else
                {
                    await _mantenimientoRepositorio.ActualizarAsync(mantenimiento);
                    _view.MostrarMensaje("Mantenimiento actualizado correctamente.");
                }

                _view.Cerrar();
            });
        }

        public async Task AgregarTareaAsync(int idTarea)
        {
            if (_view.IdMantenimiento == 0)
            {
                _view.MostrarMensaje("Debe guardar el mantenimiento antes de agregar tareas.");
                return;
            }

            var mt = new MantenimientoTarea
            {
                IdMantenimiento = _view.IdMantenimiento,
                IdTarea = idTarea,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _mantenimientoTareaRepositorio.AgregarAsync(mt);
                await InicializarAsync(_tipoVista, _view.IdMantenimiento);
            });
        }

        public async Task EliminarTareaAsync(int idTarea)
        {
            if (_view.IdMantenimiento == 0) return;

            var tareasAsociadas = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(_view.IdMantenimiento);
            var registro = tareasAsociadas.FirstOrDefault(t => t.IdTarea == idTarea);
            if (registro != null)
            {
                await _mantenimientoTareaRepositorio.EliminarAsync(registro.IdMantenimientoTarea);
                await InicializarAsync(_tipoVista, _view.IdMantenimiento);
            }
        }

        public async Task<int> CrearTareaAsync(string nombre)
        {
            var tarea = new Tarea
            {
                Nombre = nombre.Trim(),
                Descripcion = "",
                Horas = 0,
                ManoObra = 0,
                Activo = true
            };

            return await _tareaRepositorio.AgregarAsync(tarea);
        }

        public async Task AbrirEdicionTareaAsync(int idTarea)
        {
            await AbrirFormularioAsync<AgregarEditarTareaForm>(async form =>
            {
                await form._presenter.InicializarAsync("MantenimientoPredefinido", idTarea);
            });

            // 🔹 Refrescar lista de tareas al cerrar
            await InicializarAsync(_tipoVista, _view.IdMantenimiento);
        }
    }
}
