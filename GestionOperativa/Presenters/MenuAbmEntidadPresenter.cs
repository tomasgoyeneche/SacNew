using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using SacNew.Views.Configuraciones.AbmLocaciones;

namespace GestionOperativa.Presenters
{
    public class MenuAbmEntidadPresenter : BasePresenter<IMenuAltasView>
    {
        private readonly IChoferRepositorio _choferRepositorio;

        private string _entidad;

        public MenuAbmEntidadPresenter(
            IChoferRepositorio choferRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
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
                    //case "tractor":
                    //    var tractores = await _tractorGestor.ObtenerTodosAsync();
                    //    _view.MostrarEntidades(tractores);
                    //    break;
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
                        //case "tractor":
                        //    var tractores = await _tractorGestor.BuscarAsync(textoBusqueda);
                        //    _view.MostrarEntidades(tractores);
                        //    break;
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
                //case "tractor":
                //    MostrarColumnasEspecificas(gridView, new List<string> { "Placa", "Modelo", "Año" });
                //    break;

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
    }

}
