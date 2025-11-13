using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters.Mantenimiento
{
    public class MuestraDatosGenericoPresenter : BasePresenter<IMuestraDatosGenericoView>
    {
        private readonly IOrdenTrabajoRepositorio _ordenTrabajorepositorio;
        private readonly IMovimientoStockRepositorio _movimientoStockrepositorio;
        public string TipoVistaActual { get; private set; } = string.Empty;

        public MuestraDatosGenericoPresenter(
            IOrdenTrabajoRepositorio ordenTrabajorepositorio,
            IMovimientoStockRepositorio movimientoStockrepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _movimientoStockrepositorio = movimientoStockrepositorio;
            _ordenTrabajorepositorio = ordenTrabajorepositorio;
        }

        public async Task InicializarAsync(string tipoVista, int? idPosta = null)
        {
            TipoVistaActual = tipoVista;

            await EjecutarConCargaAsync(async () =>
            {
                switch (tipoVista)
                {
                    case "OrdenTrabajoProximo":
                        _view.MostrarTitulo("Próximos Mantenimientos");
                        var listaMantenimientos = await _ordenTrabajorepositorio.ObtenerOrdenTrabajoProximoAsync();
                        _view.CargarDatos(listaMantenimientos);
                        break;

                    case "ArticuloMovimientoHistorico":
                        if (!idPosta.HasValue)
                            throw new Exception("Debe especificar una posta para ver el historial de movimientos.");
                        _view.MostrarTitulo("Movimientos de Artículos por Posta");
                        var listaMovimientos = await _movimientoStockrepositorio.ObtenerMovimientosPorPostaAsync(idPosta.Value);
                        _view.CargarDatos(listaMovimientos);
                        break;

                    case "ArticuloStockDeposito":
                        if (!idPosta.HasValue)
                            throw new Exception("Debe especificar una posta para ver el stock.");
                        _view.MostrarTitulo("Stock de Artículos por Posta");
                        var listaStock = await _movimientoStockrepositorio.ObtenerStockPorPostaAsync(idPosta.Value);
                        _view.CargarDatos(listaStock);
                        break;

                    case "ArticuloStockCritico":
                        if (!idPosta.HasValue)
                            throw new Exception("Debe especificar una posta para ver el stock.");
                        _view.MostrarTitulo("Stock de Artículos Criticos por Posta");
                        var listaStockCritico = await _movimientoStockrepositorio.ObtenerStockPorPostaCriticoAsync(idPosta.Value);
                        _view.CargarDatos(listaStockCritico);
                        break;

                    default:
                        _view.MostrarTitulo("Vista desconocida");
                        _view.MostrarMensaje("Tipo de vista no reconocido.");
                        break;
                }
            });
        }
    }
}
