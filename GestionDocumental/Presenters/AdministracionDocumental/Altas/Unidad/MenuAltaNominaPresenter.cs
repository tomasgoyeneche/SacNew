using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Reports;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;
using System.Diagnostics;
using System.IO;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class MenuAltaNominaPresenter : BasePresenter<IMenuAltaNominaView>
    {
        private readonly IConfRepositorio _confRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IExcelService _excelService;
        private List<UnidadDto> _unidadesCargadas;

        public MenuAltaNominaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IConfRepositorio confRepositorio,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IExcelService excelService,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio
        ) : base(sesionService, navigationService)
        {
            _excelService = excelService;
            _confRepositorio = confRepositorio;
            _choferRepositorio = choferRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;

            _unidadesCargadas = new List<UnidadDto>();
        }

        public async Task CargarEmpresasAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                List<EmpresaDto> empresas = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();

                _view.CargarEmpresas(empresas);
                _unidadesCargadas = unidades.ToList();
                _view.MostrarUnidades(unidades);
            });
        }

        public void FiltrarPorEmpresa(string nombreEmpresa)
        {
            var unidadesFiltradas = string.IsNullOrEmpty(nombreEmpresa)
                ? _unidadesCargadas
                : _unidadesCargadas.Where(u => u.Cuit_Unidad.Equals(nombreEmpresa, StringComparison.OrdinalIgnoreCase)).ToList();

            _view.MostrarUnidades(unidadesFiltradas);
        }

        public async Task BuscarPorPatenteAsync(string patente)
        {
            if (string.IsNullOrEmpty(patente))
            {
                _view.MostrarMensaje("Ingrese una patente para buscar.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var unidad = _unidadesCargadas.FirstOrDefault(u =>
                    u.Tractor_Patente.Equals(patente, StringComparison.OrdinalIgnoreCase) ||
                    u.Semirremolque_Patente.Equals(patente, StringComparison.OrdinalIgnoreCase));

                if (unidad != null)
                {
                    string transportista = $"{unidad.Empresa_Tractor} (Cuit: {unidad.Cuit_Tractor})";
                    string subTransportista = $"{unidad.Empresa_Unidad} (Cuit: {unidad.Cuit_Unidad})";

                    _view.MostrarTransportista(transportista);
                    _view.MostrarSubTransportista(subTransportista);
                }
                else
                {
                    _view.MostrarMensaje("No se encontró una unidad con esa patente.");
                }
            });
        }

        public async Task EditarUnidadAsync()
        {
            var unidadSeleccionada = _view.ObtenerUnidadSeleccionada();
            if (unidadSeleccionada == null)
            {
                _view.MostrarMensaje("Seleccione una unidad para editar.");
                return;
            }

            await AbrirFormularioAsync<AgregarEditarUnidadForm>(async form =>
            {
                await form.CargarDatos(unidadSeleccionada.IdUnidad);
            });
        }

        public async Task EliminarUnidadAsync()
        {
            var unidadSeleccionada = _view.ObtenerUnidadSeleccionada();
            if (unidadSeleccionada == null)
            {
                _view.MostrarMensaje("Seleccione una unidad para eliminar.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta unidad?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult != DialogResult.Yes)
                return;

            await EjecutarConCargaAsync(async () =>
            {
                await _unidadRepositorio.EliminarUnidadAsync(unidadSeleccionada.IdUnidad); // 🔹 Cambia `Activo` a `0`
                _view.MostrarMensaje("Unidad eliminada correctamente.");
                await CargarEmpresasAsync(); // 🔹 Recargar lista después de eliminar
            });
        }

        public async Task AgregarUnidad(EmpresaDto empresa)
        {
            await AbrirFormularioAsync<AgregarUnidadForm>(async form =>
            {
                await form._presenter.InicializarAsync(empresa.IdEmpresa, empresa.NombreFantasia);
            });
            await CargarEmpresasAsync(); // Refrescar la vista después de agregar
        }

        public async Task GenerarReporteFlotaAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener los datos desde el repositorio
                List<NominaMetanolActivaDto> flotaUnidades = await _unidadRepositorio.ObtenerNominaMetanolActiva();

                // Crear una instancia del nuevo reporte DevExpress
                var reporte = new ReporteNominaMetanolActiva();
                reporte.DataSource = flotaUnidades;
                reporte.DataMember = "";

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }


        public async Task ExportarNominaControlCambios(DateTime desde)
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener los datos desde el repositorio
                List<NominaMetanolActivaDto> flotaUnidades = await _unidadRepositorio.ObtenerNominaMetanolActivaPorFecha(desde);

                // Crear una instancia del nuevo reporte DevExpress
                var reporte = new ReporteNominaMetanolCtrlCambios();
                reporte.DataSource = null;
                reporte.DataMember = "";

                reporte.DetailReport.DataSource = flotaUnidades;
                reporte.DetailReport.DataMember = "";

                reporte.DetailReportBajas.DataSource = flotaUnidades;
                reporte.DetailReportBajas.DataMember = "";


                reporte.DetailReportAltas.DataSource = flotaUnidades;
                reporte.DetailReportAltas.DataMember = "";
                reporte.Parameters["pFechaFiltro"].Value = desde;

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }


        public async Task GenerarReporteNominaComodatoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener los datos desde el repositorio
                List<UnidadDto> flotaUnidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();

                // Crear una instancia del nuevo reporte DevExpress
                var reporte = new ReporteNominaEquiposEnComodato();
                reporte.DataSource = flotaUnidades;
                reporte.DataMember = ""; // Dejar vacío si el datasource es una lista de objetos

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task ExportarHistorialUnidadesAsync(DateTime desde, DateTime hasta)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<Unidad> unidades = await _unidadRepositorio.ObtenerUnidadesAsync();
                List<Empresa> empresas = await _empresaRepositorio.ObtenerTodasLasEmpresas();
                List<Shared.Models.Tractor> tractores = await _tractorRepositorio.ObtenerTodosLosTractores();
                List<Semi> semis = await _semiRepositorio.ObtenerTodosLosSemis();

                var historial = new List<HistorialUnidadDto>();

                foreach (var unidad in unidades)
                {
                    var empresa = empresas.FirstOrDefault(e => e.IdEmpresa == unidad.IdEmpresa);
                    var tractor = tractores.FirstOrDefault(t => t.IdTractor == unidad.IdTractor);
                    var semi = semis.FirstOrDefault(s => s.IdSemi == unidad.IdSemi);

                    if (unidad.AltaUnidad >= desde && unidad.AltaUnidad <= hasta)
                    {
                        historial.Add(new HistorialUnidadDto
                        {
                            Empresa = empresa?.NombreFantasia ?? "N/A",
                            Tractor = tractor?.Patente ?? "N/A",
                            Semi = semi?.Patente ?? "N/A",
                            Estado = "Alta",
                            EstadoFecha = unidad.AltaUnidad,
                            EstadoActual = unidad.Activo ? "Activo" : "Inactivo"
                        });
                    }

                    if (unidad.BajaUnidad >= desde && unidad.BajaUnidad <= hasta)
                    {
                        historial.Add(new HistorialUnidadDto
                        {
                            Empresa = empresa?.NombreFantasia ?? "N/A",
                            Tractor = tractor?.Patente ?? "N/A",
                            Semi = semi?.Patente ?? "N/A",
                            Estado = "Baja",
                            EstadoFecha = unidad.BajaUnidad,
                            EstadoActual = unidad.Activo ? "Activo" : "Inactivo"
                        });
                    }
                }

                if (!historial.Any())
                {
                    _view.MostrarMensaje("No se encontraron unidades en el rango de fechas seleccionado.");
                    return;
                }

                // Crear carpeta si no existe
                string carpetaExport = @"C:\Compartida\Exportaciones";
                Directory.CreateDirectory(carpetaExport);

                string filePath = Path.Combine(carpetaExport, "Historial_Unidades.xlsx");

                await _excelService.ExportarAExcelAsync(historial, filePath, "Historial");

                // Abrir el archivo
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });

                _view.MostrarMensaje("Exportación completada.");
            });
        }

        //public async Task GenerarReporteFlotaAsync()
        //{
        //    await EjecutarConCargaAsync(async () =>
        //    {
        //        // Obtener los datos desde el repositorio
        //        List<NominaMetanolActivaDto> flotaUnidades = await _unidadRepositorio.ObtenerNominaMetanolActiva();

        //        // Agrupar por empresa para que el RDLC los muestre correctamente
        //        var dataSources = new Dictionary<string, object>
        //        {
        //            { "NominaMetanolActiva", flotaUnidades }
        //        };

        //        // Crear el reporte con el servicio
        //        var report = _reportService.CrearReporte("NominaMetanolActiva", dataSources);

        //        // Navegar al formulario que muestra el reporte
        //        await AbrirFormularioAsync<VisualizadorReportesForm>(async form =>
        //        {
        //            await form.MostrarReporte(report);
        //        });
        //    });
        //}
    }
}