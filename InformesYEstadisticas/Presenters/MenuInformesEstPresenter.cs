using Configuraciones.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Processor;
using GestionOperativa.Reports;
using GestionOperativa.Views.AdministracionDocumental;
using InformesYEstadisticas.Views;
using Shared.Models;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformesYEstadisticas.Presenters
{
    public class MenuInformesEstPresenter : BasePresenter<IMenuInformesEstView>
    {
        private readonly IDocumentacionProcessor _documentacionService;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IPlanillaRepositorio _planillaRepositorio;

        public MenuInformesEstPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IDocumentacionProcessor documentacionService,
            IPlanillaRepositorio planillaRepositorio,
            IConfRepositorio confRepositorio
        ) : base(sesionService, navigationService)
        {
            _documentacionService = documentacionService;
            _confRepositorio = confRepositorio;
            _planillaRepositorio = planillaRepositorio;
        }


        public async Task GenerarFichaVacia()
        {
            await EjecutarConCargaAsync(async () =>
            {

                Planilla planilla = await _planillaRepositorio.ObtenerPorIdAsync(1);
                // Obtener los datos desde el repositorio
                List<PlanillaPreguntaDto> preguntas = await _planillaRepositorio.ObtenerPreguntasPorPlanilla(1);

                List<PlanillaPreguntaDto> listaOrdenada = preguntas
                .OrderBy(p => p.Orden, new AlfanumericStringComparer())
                .ToList();
                // Crear una instancia del nuevo reporte DevExpress
                ReporteFichaTecnicaUnidadVacia reporte = new ReporteFichaTecnicaUnidadVacia();
                reporte.DataSource = new List<Planilla> { planilla };
                reporte.DataMember = "";

                reporte.DetailReportPreg.DataSource = listaOrdenada;
                reporte.DetailReportPreg.DataMember = ""; // si es lista directa


                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }
    }
}
