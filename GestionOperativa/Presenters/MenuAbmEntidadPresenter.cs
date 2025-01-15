using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using GestionOperativa.Views.AdministracionDocumental.Altas.Empresas;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class MenuAbmEntidadPresenter : BasePresenter<IMenuAltasView>
    {
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;

        private string? _entidad;

        public MenuAbmEntidadPresenter(
            IChoferRepositorio choferRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _empresaRepositorio = empresaRepositorio;
            _choferRepositorio = choferRepositorio;
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
                        var choferes = await _choferRepositorio.ObtenerTodosLosChoferes();
                        _view.MostrarEntidades(choferes);
                        break;
                    case "empresa":
                        var tractores = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
                        _view.MostrarEntidades(tractores);
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
                        ("NombreApellido", 0), // Columna "Nombre" en la posición 0
                        ("Documento", 1), // Columna "Apellido" en la posición 1
                        ("Domicilio", 2)  // Columna "Licencia" en la posición 2
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
                        if (entidadSeleccionada is Chofer chofer)
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
                    default:
                        throw new ArgumentException("Entidad no soportada para eliminación.");
                }

                _view.MostrarMensaje($"Registro eliminado correctamente.");
                await CargarEntidadesAsync(); // Recargar la lista de entidades
            });
        }
    }
}