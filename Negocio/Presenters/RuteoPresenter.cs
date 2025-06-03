using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionFlota.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Presenters
{
    public class RuteoPresenter : BasePresenter<IRuteoView>
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;

        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;

        public RuteoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IProgramaRepositorio programaRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            IPostaRepositorio postaRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IVaporizadoRepositorio vaporizadoRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _programaRepositorio = programaRepositorio;
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
                    List<Ruteo> ruteos = await _programaRepositorio.ObtenerRuteoAsync();
                   // List<RuteoResumen> resumen = await _programaRepositorio.ObtenerResumenAsync();

                    var cargados = ruteos
                        .Where(r => r.Estado == "En Viaje" || r.Estado == "Descargando")
                        .ToList();

                    var vacios = ruteos
                        .Where(r => r.Estado != "En Viaje" && r.Estado != "Descargando")
                        .ToList();

                    _view.MostrarRuteoCargados(cargados);
                    _view.MostrarRuteoVacios(vacios);
           });

        }

        //public async Task MostrarHistorialAsync(int idGuardiaIngreso)
        //{
        //    var historial = await _nominaRepositorio.ObtenerHistorial(idGuardiaIngreso);
        //    _view.MostrarHistorial(historial);
        //}

        public async Task CargarVencimientosYAlertasAsync(Ruteo ruteo)
        {
            List<VencimientosDto> vencimientos = new();
            List<AlertaDto> alertas = new();
            DateTime fechaLimite = DateTime.Now.AddDays(20);

            Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(ruteo.IdNomina);
            List<VencimientosDto> vencNomina = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(nomina);
            vencimientos.AddRange(vencNomina.Where(v => v.FechaVencimiento <= fechaLimite));

            List<AlertaDto> alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(ruteo.IdNomina);
            alertas.AddRange(alertasNomina);
             

            _view.MostrarVencimientos(vencimientos.OrderBy(v => v.FechaVencimiento).ToList());
            _view.MostrarAlertas(alertas);
        }
        

       
        //public async Task AbrirEdicionDePrograma(Ruteo ruteo)
        //{
        //    await AbrirFormularioAsync<ModificarProgramaForm>(async form =>
        //    {
        //        await form._presenter.InicializarAsync(ruteo, false);
        //    });
        //    await InicializarAsync();
        //}
    }
}

