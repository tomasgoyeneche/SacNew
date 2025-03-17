using App.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Processor;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class MenuAltaNominaPresenter : BasePresenter<IMenuAltaNominaView>
    {
        private readonly IConfRepositorio _confRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IReportService _reportService;

        public MenuAltaNominaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IConfRepositorio confRepositorio,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IReportService reportService
        ) : base(sesionService, navigationService)
        {
            _reportService = reportService;
            _confRepositorio = confRepositorio;
            _choferRepositorio = choferRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _unidadRepositorio = unidadRepositorio;
        }



        public async Task GenerarReporteFlotaAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener los datos desde el repositorio
                var flotaUnidades = await _unidadRepositorio.ObtenerNominaMetanolActiva();

                // Agrupar por empresa para que el RDLC los muestre correctamente
                var dataSources = new Dictionary<string, object>
                {
                    { "NominaMetanolActiva", flotaUnidades }
                };

                // Crear el reporte con el servicio
                var report = _reportService.CrearReporte("NominaMetanolActiva", dataSources);

                // Navegar al formulario que muestra el reporte
                await AbrirFormularioAsync<VisualizadorReportesForm>(async form =>
                {
                    await form.MostrarReporte(report);
                });
            });
        }
    }
}
