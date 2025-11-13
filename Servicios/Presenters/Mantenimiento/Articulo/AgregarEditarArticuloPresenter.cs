using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimiento;
using Shared.Models;

namespace Servicios.Presenters.Mantenimiento
{
    public class AgregarEditarArticuloPresenter : BasePresenter<IAgregarEditarArticuloView>
    {
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly IMedidaRepositorio _medidaRepositorio;
        private readonly IArticuloFamiliaRepositorio _familiaRepositorio;
        private readonly IOrdenTrabajoArticuloRepositorio _ordenTrabajoArticuloRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _mantenimientoTareaArticuloRepositorio;

        private readonly IArticuloMarcaRepositorio _marcaRepositorio;
        private readonly IArticuloStockRepositorio _articuloStockRepositorio;
        private readonly IArticuloModeloRepositorio _modeloRepositorio;

        public Articulo? ArticuloActual { get; private set; }

        private int _idArticuloPrueba;
        private OrdenTrabajoArticulo _ordenArticulo;
        private string? _tipoVista;
        private int? _idTarea;

        public AgregarEditarArticuloPresenter(
            IArticuloRepositorio articuloRepositorio,
            IMedidaRepositorio medidaRepositorio,
            IArticuloFamiliaRepositorio familiaRepositorio,
            IMantenimientoTareaArticuloRepositorio mantenimientoTareaArticuloRepositorio,
            IArticuloMarcaRepositorio marcaRepositorio,
            IOrdenTrabajoArticuloRepositorio ordenTrabajoArticuloRepositorio,
            IArticuloModeloRepositorio modeloRepositorio,
            IArticuloStockRepositorio articuloStockRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _articuloRepositorio = articuloRepositorio;
            _medidaRepositorio = medidaRepositorio;
            _familiaRepositorio = familiaRepositorio;
            _marcaRepositorio = marcaRepositorio;
            _ordenTrabajoArticuloRepositorio = ordenTrabajoArticuloRepositorio;
            _mantenimientoTareaArticuloRepositorio = mantenimientoTareaArticuloRepositorio;
            _articuloStockRepositorio = articuloStockRepositorio;
            _modeloRepositorio = modeloRepositorio;
        }

        public async Task<List<ArticuloModelo>> ObtenerModelosPorMarcaAsync(int idMarca)
        {
            return await _modeloRepositorio.ObtenerPorMarcaAsync(idMarca);
        }

        public async Task InicializarAsync(int? idArticulo = null, string? tipoVista = null, int? idTarea = null)
        {
            _tipoVista = null;
            _idTarea = null;
            _view.LimpiarFormulario();

            if (tipoVista != null)
            {
                _tipoVista = tipoVista;
                _idTarea = idTarea ?? 0;
            }
            await EjecutarConCargaAsync(async () =>
            {
                var medidas = await _medidaRepositorio.ObtenerTodosAsync();
                var familias = await _familiaRepositorio.ObtenerTodasAsync();
                var marcas = await _marcaRepositorio.ObtenerTodasAsync();

                _view.CargarMedidas(medidas);
                _view.CargarFamilias(familias);
                _view.CargarMarcas(marcas);

                if (_tipoVista == null || _tipoVista == "MantenimientoPredefinido")
                {
                    if (idArticulo.HasValue)
                    {
                        ArticuloActual = await _articuloRepositorio.ObtenerPorIdAsync(idArticulo.Value);
                        if (ArticuloActual != null)
                        {
                            _view.MostrarDatosArticulo(ArticuloActual);

                            if (ArticuloActual.IdArticuloMarca.HasValue)
                            {
                                var modelos = await _modeloRepositorio.ObtenerPorMarcaAsync(ArticuloActual.IdArticuloMarca.Value);
                                _view.CargarModelos(modelos);
                            }
                        }
                    }
                }
                if (_tipoVista == "MantenimientoManual")
                {
                    if (idArticulo.HasValue)
                    {
                        _ordenArticulo = await _ordenTrabajoArticuloRepositorio.ObtenerPorIdAsync(idArticulo.Value);
                        ArticuloActual = await _articuloRepositorio.ObtenerPorIdAsync(_ordenArticulo.IdArticulo.Value);
                        if (_ordenArticulo != null)
                        {
                            _view.MostrarDatosArticulo(ArticuloActual, _ordenArticulo);

                            if (ArticuloActual.IdArticuloMarca.HasValue)
                            {
                                var modelos = await _modeloRepositorio.ObtenerPorMarcaAsync(ArticuloActual.IdArticuloMarca.Value);
                                _view.CargarModelos(modelos);
                            }
                        }
                    }
                }
            });
        }

        public async Task GuardarAsync()
        {
            var articulo = new Articulo
            {
                IdArticulo = ArticuloActual?.IdArticulo ?? 0,
                Codigo = _view.Codigo,
                Nombre = _view.Nombre,
                Descripcion = _view.Descripcion,
                IdMedida = _view.IdMedida,
                IdPosta = _sesionService.IdPosta,
                IdArticuloFamilia = _view.IdArticuloFamilia,
                IdArticuloMarca = _view.IdArticuloMarca,
                IdArticuloModelo = _view.IdArticuloModelo,
                PrecioUnitario = _view.PrecioUnitario,
                PedidoMinimo = _view.PedidoMinimo,
                PedidoMaximo = _view.PedidoMaximo,
                StockCritico = _view.StockCritico,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                if (ArticuloActual == null)
                {
                    var id = await _articuloRepositorio.AgregarArticuloAsync(articulo);
                    await _articuloStockRepositorio.CrearStockAsync(id, 2, 0);
                    await _articuloStockRepositorio.CrearStockAsync(id, 3, 0);

                    if (_tipoVista != null && _tipoVista == "MantenimientoPredefinido")
                    {
                        int cantidad = _view.ObtenerCantidad();

                        MantenimientoTareaArticulo mantenimientoTareaArticulo = new MantenimientoTareaArticulo
                        {
                            IdTarea = _idTarea ?? 0,
                            IdArticulo = id,
                            Cantidad = cantidad,
                            Activo = true
                        };
                        await _mantenimientoTareaArticuloRepositorio.AgregarAsync(mantenimientoTareaArticulo);
                    }
                    _view.MostrarMensaje($"Artículo agregado correctamente. ID: {id}");
                    _idArticuloPrueba = id;
                }
                else
                {
                    if (_tipoVista == null || _tipoVista == "MantenimientoPredefinido")
                    {
                        await _articuloRepositorio.ActualizarArticuloAsync(articulo);
                        _view.MostrarMensaje("Artículo actualizado correctamente.");
                    }
                }
            });

            if (_tipoVista == "MantenimientoManual")
            {
                await EjecutarConCargaAsync(async () =>
                {
                    int cantidad = _view.ObtenerCantidad();

                    var articulo = new OrdenTrabajoArticulo
                    {
                        IdOrdenTrabajoArticulo = _ordenArticulo?.IdOrdenTrabajoArticulo ?? 0,
                        IdOrdenTrabajoTarea = _idTarea ?? 0,
                        IdArticulo = _ordenArticulo?.IdArticulo ?? _idArticuloPrueba,
                        Codigo = _view.Codigo,
                        Nombre = _view.Nombre,
                        PrecioUnitario = _view.PrecioUnitario,
                        Cantidad = cantidad, //falta
                        Estimado = cantidad * _view.PrecioUnitario, //falta
                        Estado = _ordenArticulo?.Estado ?? "Pendiente",
                        Activo = true
                    };

                    if (ArticuloActual == null)
                    {
                        var id = await _ordenTrabajoArticuloRepositorio.AgregarAsync(articulo);
                        _view.MostrarMensaje($"Artículo agregado correctamente. ID: {id}");
                    }
                    else
                    {
                        await _ordenTrabajoArticuloRepositorio.ActualizarAsync(articulo);
                        _view.MostrarMensaje("Artículo actualizado correctamente.");
                    }
                });
            }

            _view.Cerrar();
        }

        public async Task<List<ArticuloFamilia>> ObtenerFamiliasAsync() =>
        await _familiaRepositorio.ObtenerTodasAsync();

        public async Task<int> AgregarFamiliaAsync(ArticuloFamilia familia) =>
            await _familiaRepositorio.AgregarAsync(familia);

        public async Task<List<ArticuloMarca>> ObtenerMarcasAsync() =>
            await _marcaRepositorio.ObtenerTodasAsync();

        public async Task<int> AgregarMarcaAsync(ArticuloMarca marca) =>
            await _marcaRepositorio.AgregarAsync(marca);

        public async Task<int> AgregarModeloAsync(ArticuloModelo modelo) =>
            await _modeloRepositorio.AgregarAsync(modelo);
    }
}