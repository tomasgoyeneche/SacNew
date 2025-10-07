using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class MovimientoStockPresenter : BasePresenter<IMovimientoStockView>
    {
        private readonly IMovimientoStockRepositorio _movimientoRepositorio;

        public MovimientoStockPresenter(
            IMovimientoStockRepositorio movimientoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _movimientoRepositorio = movimientoRepositorio;
        }

        public async Task<int> CrearMovimientoAsync(int idTipoMovimiento)
        {
            var movimiento = new MovimientoStock
            {
                IdTipoMovimiento = idTipoMovimiento,
                FechaEmision = DateTime.Now,
                Autorizado = false,
                Activo = true,
                Observaciones = ""
            };

            return await _movimientoRepositorio.AgregarAsync(movimiento);
        }

        public async Task AbrirEdicionMovimientoAsync(int idMovimientoStock)
        {
            await AbrirFormularioAsync<AgregarEditarMovimientoStock>(async form =>
            {
                await form._presenter.InicializarAsync(idMovimientoStock);
            });

            // refrescar lista después de cerrar
            await InicializarAsync();
        }


        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var movimientos = await _movimientoRepositorio.ObtenerMovimientosAsync();
                _view.MostrarMovimientos(movimientos);
            });
        }
    }
}
