using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Presenters
{
    public class CupeoPresenter : BasePresenter<ICupeoView>
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;

        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;

        public CupeoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IPedidoRepositorio pedidoRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            IPostaRepositorio postaRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IVaporizadoRepositorio vaporizadoRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _postaRepositorio = postaRepositorio;
            _periodoRepositorio = periodoRepositorio;
            _vaporizadoRepositorio = vaporizadoRepositorio;
        }

        public async Task InicializarAsync()
        {

            await EjecutarConCargaAsync(async () =>
            {
                List<Cupeo> cupeos = await _pedidoRepositorio.ObtenerCupeoAsync();
                //List<Cupeo> resumen = await _cupeoRepositorio.ObtenerResumenAsync();

                var disp = cupeos
                    .Where(r => r.IdPedido == 0)
                    .ToList();

                var asignados = cupeos
                    .Where(r => r.IdPedido != 0)
                    .ToList();

                _view.MostrarCupeoDisp(disp);
                _view.MostrarCupeoAsignados(asignados);
                //_view.MostrarResumen(resumen);

            });

        }

        //public async Task MostrarHistorialAsync(int idGuardiaIngreso)
        //{
        //    var historial = await _nominaRepositorio.ObtenerHistorial(idGuardiaIngreso);
        //    _view.MostrarHistorial(historial);
        //}

        public async Task CargarVencimientosYAlertasAsync(Cupeo cupeo)
        {
            List<VencimientosDto> vencimientos = new();
            List<AlertaDto> alertas = new();
            DateTime fechaLimite = DateTime.Now.AddDays(20);

            Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(cupeo.IdNomina);
            List<VencimientosDto> vencNomina = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(nomina);
            vencimientos.AddRange(vencNomina.Where(v => v.FechaVencimiento <= fechaLimite));

            List<AlertaDto> alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(cupeo.IdNomina);
            alertas.AddRange(alertasNomina);


            _view.MostrarVencimientos(vencimientos.OrderBy(v => v.FechaVencimiento).ToList());
            _view.MostrarAlertas(alertas);
        }


        public async Task ImportarProgramaAsync()
        {
            await AbrirFormularioAsync<ImportarPrograma>(async form =>
            {
               // await form._presenter.InicializarAsync();
            });
            await InicializarAsync();
        }

        public async Task AbrirAsignarCargaAsync(Cupeo cupeo)
        {
            await AbrirFormularioAsync<AsignarCargaForm>(async f => await f._presenter.InicializarAsync(cupeo));
            await InicializarAsync();
        }
    }
}
