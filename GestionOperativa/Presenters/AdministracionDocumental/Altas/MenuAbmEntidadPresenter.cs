using App.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using GestionOperativa.Views.AdministracionDocumental.Altas.Choferes;
using GestionOperativa.Views.AdministracionDocumental.Altas.Empresas;
using GestionOperativa.Views.AdministracionDocumental.Altas.Semis;
using GestionOperativa.Views.AdministracionDocumental.Altas.Tractores;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class MenuAbmEntidadPresenter : BasePresenter<IMenuAltasView>
    {
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        // reporte
        private readonly IUnidadRepositorio _unidadRepositorio;

        private readonly IReportService _reportService;

        private string? _entidad;

        public MenuAbmEntidadPresenter(
            IChoferRepositorio choferRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio,

            IUnidadRepositorio unidadRepositorio,
            IReportService reportService,

            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _tractorRepositorio = tractorRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _choferRepositorio = choferRepositorio;
            _semiRepositorio = semiRepositorio;

            _unidadRepositorio = unidadRepositorio;
            _reportService = reportService;
        }

        public void SetEntidad(string entidad)
        {
            _entidad = entidad;
        }

        public async Task CargarEntidadesAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                switch (_entidad.ToLower())
                {
                    case "chofer":
                        var choferes = await _choferRepositorio.ObtenerTodosLosChoferesDto();
                        _view.MostrarEntidades(choferes);
                        break;

                    case "empresa":
                        var empresa = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
                        _view.MostrarEntidades(empresa);
                        break;

                    case "tractor":
                        var tractores = await _tractorRepositorio.ObtenerTodosLosTractoresDto();
                        _view.MostrarEntidades(tractores);
                        break;

                    case "semi":
                        var semis = await _semiRepositorio.ObtenerTodosLosSemisDto();
                        _view.MostrarEntidades(semis);
                        break;

                    default:
                        throw new ArgumentException("Entidad no soportada");
                }
            });
        }

        public async Task BuscarEntidadesAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var textoBusqueda = _view.TextoBusqueda;

                if (string.IsNullOrEmpty(textoBusqueda))
                {
                    await CargarEntidadesAsync();
                }
                else
                {
                    switch (_entidad.ToLower())
                    {
                        case "chofer":
                            var choferes = await _choferRepositorio.BuscarAsync(textoBusqueda);
                            _view.MostrarEntidades(choferes);
                            break;

                        case "empresa":
                            var empresas = await _empresaRepositorio.BuscarEmpresasAsync(textoBusqueda);
                            _view.MostrarEntidades(empresas);
                            break;

                        case "tractor":
                            var tractores = await _tractorRepositorio.BuscarTractoresAsync(textoBusqueda);
                            _view.MostrarEntidades(tractores);
                            break;

                        case "semi":
                            var semis = await _semiRepositorio.BuscarSemisAsync(textoBusqueda);
                            _view.MostrarEntidades(semis);
                            break;

                        default:
                            throw new ArgumentException("Entidad no soportada");
                    }
                }
            });
        }

        public void ConfigurarColumnas(DataGridView gridView)
        {
            switch (_entidad)
            {
                case "chofer":
                    MostrarColumnasEspecificas(gridView, new List<(string columna, int orden)>
                    {
                        ("Apellido", 0), // Columna "Nombre" en la posición 0
                        ("Nombre", 1),
                        ("Documento", 2), // Columna "Apellido" en la posición 1
                        ("Domicilio", 3)  // Columna "Licencia" en la posición 2
                    });
                    break;

                case "empresa":
                    MostrarColumnasEspecificas(gridView, new List<(string columna, int orden)>
                    {
                        ("razonSocial", 0), // Columna "Nombre" en la posición 0
                        ("nombreFantasia", 1), // Columna "Apellido" en la posición 1
                        ("Cuit", 2)  // Columna "Licencia" en la posición 2
                    });
                    break;

                case "tractor":
                    MostrarColumnasEspecificas(gridView, new List<(string columna, int orden)>
                    {
                        ("patente", 0), // Columna "Nombre" en la posición 0
                        ("empresa_cuit", 2), // Columna "Apellido" en la posición 1
                        ("empresa_nombre", 1)  // Columna "Licencia" en la posición 2
                    });
                    break;

                case "semi":
                    MostrarColumnasEspecificas(gridView, new List<(string columna, int orden)>
                    {
                        ("patente", 0), // Columna "Nombre" en la posición 0
                        ("empresa_cuit", 2), // Columna "Apellido" en la posición 1
                        ("empresa_nombre", 1)  // Columna "Licencia" en la posición 2
                    });
                    break;

                default:
                    // Si no hay columnas configuradas, puedes decidir mostrar todas o ninguna
                    foreach (DataGridViewColumn column in gridView.Columns)
                    {
                        column.Visible = false;
                    }
                    break;
            }
        }

        public void MostrarColumnasEspecificas(DataGridView gridView, List<(string columna, int orden)> configuracionColumnas)
        {
            // Ocultar todas las columnas primero
            foreach (DataGridViewColumn column in gridView.Columns)
            {
                column.Visible = false;
            }

            // Configurar visibilidad y orden de las columnas especificadas
            foreach (var (columna, orden) in configuracionColumnas)
            {
                if (gridView.Columns.Contains(columna))
                {
                    gridView.Columns[columna].Visible = true;
                    gridView.Columns[columna].DisplayIndex = orden;
                }
            }
        }

        public async Task EditarEntidadAsync<T>(T entidadSeleccionada)
        {
            if (entidadSeleccionada == null)
            {
                _view.MostrarMensaje("Debe seleccionar un registro para editar.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                switch (_entidad.ToLower())
                {
                    case "chofer":
                        if (entidadSeleccionada is ChoferDto chofer)
                        {
                            await AbrirFormularioAsync<AgregarEditarChoferForm>(async form =>
                            {
                                await form._presenter.CargarDatosParaMostrarAsync(chofer.IdChofer);
                            });
                        }
                        break;

                    case "empresa":
                        if (entidadSeleccionada is EmpresaDto empresa)
                        {
                            await AbrirFormularioAsync<AgregarEditarEmpresaForm>(async form =>
                            {
                                await form._presenter.CargarDatosParaMostrarAsync(empresa.IdEmpresa);
                            });
                        }
                        break;

                    case "tractor":
                        if (entidadSeleccionada is TractorDto tractor)
                        {
                            await AbrirFormularioAsync<AgregarEditarTractorForm>(async form =>
                            {
                                await form._presenter.CargarDatosParaMostrarAsync(tractor.IdTractor);
                            });
                        }
                        break;

                    case "semi":
                        if (entidadSeleccionada is SemiDto semi)
                        {
                            await AbrirFormularioAsync<AgregarEditarSemiForm>(async form =>
                            {
                                await form._presenter.CargarDatosParaMostrarAsync(semi.IdSemi);
                            });
                        }
                        break;

                    default:
                        throw new ArgumentException("Entidad no soportada para edición.");
                }
            }, CargarEntidadesAsync);
        }

        public async Task EliminarEntidadAsync<T>(T entidadSeleccionada)
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (entidadSeleccionada == null)
                {
                    _view.MostrarMensaje("Seleccione un registro para eliminar.");
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"¿Estás seguro de que deseas eliminar esta {_entidad}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                    return;

                switch (_entidad.ToLower())
                {
                    case "chofer":
                        if (entidadSeleccionada is ChoferDto chofer)
                        {
                            await _choferRepositorio.EliminarChoferAsync(chofer.IdChofer);
                        }
                        break;

                    case "empresa":
                        if (entidadSeleccionada is EmpresaDto empresa)
                        {
                            await _empresaRepositorio.EliminarEmpresaAsync(empresa.IdEmpresa);
                        }
                        break;

                    case "tractor":
                        if (entidadSeleccionada is TractorDto tractor)
                        {
                            await _tractorRepositorio.EliminarTractorAsync(tractor.IdTractor);
                        }
                        break;

                    case "semi":
                        if (entidadSeleccionada is SemiDto semi)
                        {
                            await _semiRepositorio.EliminarSemiAsync(semi.IdSemi);
                        }
                        break;

                    default:
                        throw new ArgumentException("Entidad no soportada para eliminación.");
                }

                _view.MostrarMensaje($"Registro eliminado correctamente.");
                await CargarEntidadesAsync(); // Recargar la lista de entidades
            });
        }
    }
}