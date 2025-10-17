using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Servicios.Views.Mantenimientos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class ListadoOrdenTrabajoPresenter : BasePresenter<IListadoOrdenTrabajoView>
    {
        private readonly IOrdenTrabajoRepositorio _orderTrabajoRepositorio;
        public string _Criterio; 

        public ListadoOrdenTrabajoPresenter(
            IOrdenTrabajoRepositorio orderTrabajoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _orderTrabajoRepositorio = orderTrabajoRepositorio;
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


        public async Task InicializarAsync(string criterio)
        {
            _Criterio = criterio;
            await EjecutarConCargaAsync(async () =>
            {
                List<OrdenTrabajoDto> ordenes;
                if(_Criterio == "Todos")
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
