using App.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class MenuAltaNominaPresenter : BasePresenter<IMenuAltaNominaView>
    {
        private readonly IConfRepositorio _confRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IReportService _reportService;
        private List<UnidadDto> _unidadesCargadas;

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
            _unidadesCargadas = new List<UnidadDto>();
        }

        public async Task CargarEmpresasAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                var empresas = unidades
                    .Select(u => new { Nombre = u.Empresa_Unidad, Cuit = u.Cuit_Unidad })
                    .Distinct()
                    .Select(e => (object)e)
                    .ToList();

                _view.CargarEmpresas(empresas);
                _unidadesCargadas = unidades.ToList();
                _view.MostrarUnidades(unidades);
            });
        }

        public void FiltrarPorEmpresa(string nombreEmpresa)
        {
            var unidadesFiltradas = string.IsNullOrEmpty(nombreEmpresa)
                ? _unidadesCargadas
                : _unidadesCargadas.Where(u => u.Empresa_Unidad == nombreEmpresa).ToList();

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