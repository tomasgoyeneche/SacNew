using Configuraciones.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Processor;
using GestionOperativa.Reports;
using GestionOperativa.Views.AdministracionDocumental.Relevamientos;
using Shared.Models;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class FichaTecnicaUnidadesPresenter : BasePresenter<IFichaTecnicaUnidadesView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IFichaTecnicaUnidadProcessor _fichaTecnicaProcessor;

        public FichaTecnicaUnidadesPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IUnidadRepositorio unidadRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio,
            IFichaTecnicaUnidadProcessor fichaTecnicaProcessor
        ) : base(sesionService, navigationService)
        {
            _unidadRepositorio = unidadRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _semiRepositorio = semiRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _fichaTecnicaProcessor = fichaTecnicaProcessor;
        }

        public async Task InicializarAsync()
        {
            List<EmpresaDto> transportistas = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
            _view.CargarTransportistas(transportistas);

            List<TractorDto> tractores = await _tractorRepositorio.ObtenerTodosLosTractoresDto();
            List<SemiDto> semis = await _semiRepositorio.ObtenerTodosLosSemisDto();
            List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();


            int totalTractores = tractores.Count;
            int totalSemis = semis.Count;
            int totalUnidades = unidades.Count;

            _view.MostrarTotales(totalTractores, totalSemis, totalUnidades);

            double promedioTractores = tractores.Where(u => u.Anio.HasValue)
                .Select(u => DateTime.Now.Year - u.Anio.Value.Year).DefaultIfEmpty(0).Average();

            double promedioSemis = semis.Where(u => u.Anio.HasValue)
                .Select(u => DateTime.Now.Year - u.Anio.Value.Year).DefaultIfEmpty(0).Average();

            double promedioUnidad = (promedioTractores + promedioSemis) / 2;

            _view.MostrarPromedios(promedioTractores, promedioSemis, promedioUnidad);
        }

        public async Task BuscarAsync(string Cuit)
        {
            List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();

            // Filtro por transportista

            unidades = unidades.Where(u => u.Cuit_Unidad == Cuit).ToList();
               
  
            _view.MostrarUnidades(unidades);
            // Filtro por texto
        }

        public async Task GenerarFichaTecnica(int idUnidad)
        {
            await EjecutarConCargaAsync(async () =>
            {

                FichaTecnicaUnidadDto fichaTecnicaDto = await _fichaTecnicaProcessor.ObtenerFichaTecnicaAsync(idUnidad);    
                // Obtener los datos desde el repositorio

                // Crear una instancia del nuevo reporte DevExpress
                var reporte = new ReporteFichaTecnicaUnidad();
                reporte.DataSource = new List<FichaTecnicaUnidadDto> { fichaTecnicaDto };
                reporte.DataMember = "";

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

    }
}
