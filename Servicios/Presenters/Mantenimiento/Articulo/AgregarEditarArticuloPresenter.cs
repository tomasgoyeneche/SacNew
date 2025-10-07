using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimiento;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters.Mantenimiento
{
    public class AgregarEditarArticuloPresenter : BasePresenter<IAgregarEditarArticuloView>
    {
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly IMedidaRepositorio _medidaRepositorio;
        private readonly IArticuloFamiliaRepositorio _familiaRepositorio;
        private readonly IArticuloMarcaRepositorio _marcaRepositorio;
        private readonly IArticuloStockRepositorio _articuloStockRepositorio;
        private readonly IArticuloModeloRepositorio _modeloRepositorio;

        public Articulo? ArticuloActual { get; private set; }

        public AgregarEditarArticuloPresenter(
            IArticuloRepositorio articuloRepositorio,
            IMedidaRepositorio medidaRepositorio,
            IArticuloFamiliaRepositorio familiaRepositorio,
            IArticuloMarcaRepositorio marcaRepositorio,
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
            _articuloStockRepositorio = articuloStockRepositorio;
            _modeloRepositorio = modeloRepositorio;
        }

        public async Task<List<ArticuloModelo>> ObtenerModelosPorMarcaAsync(int idMarca)
        {
            return await _modeloRepositorio.ObtenerPorMarcaAsync(idMarca);
        }

        public async Task InicializarAsync(int? idArticulo = null)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var medidas = await _medidaRepositorio.ObtenerTodosAsync();
                var familias = await _familiaRepositorio.ObtenerTodasAsync();
                var marcas = await _marcaRepositorio.ObtenerTodasAsync();

                _view.CargarMedidas(medidas);
                _view.CargarFamilias(familias);
                _view.CargarMarcas(marcas);

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
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                if (ArticuloActual == null)
                {
                    var id = await _articuloRepositorio.AgregarArticuloAsync(articulo);
                    await _articuloStockRepositorio.CrearStockAsync(id, 2, 0);
                    await _articuloStockRepositorio.CrearStockAsync(id, 3, 0);
                    _view.MostrarMensaje($"Artículo agregado correctamente. ID: {id}");
                }
                else
                {
                    await _articuloRepositorio.ActualizarArticuloAsync(articulo);
                    _view.MostrarMensaje("Artículo actualizado correctamente.");
                }

                _view.Cerrar();
            });
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
