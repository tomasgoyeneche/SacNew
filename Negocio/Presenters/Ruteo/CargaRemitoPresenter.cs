using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Ruteo;
using Shared.Models;

namespace GestionFlota.Presenters.Ruteo
{
    public class CargaRemitoPresenter : BasePresenter<ICargaRemitoView>
    {
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IMedidaRepositorio _medidaRepositorio;
        private readonly IProgramaRepositorio _programaRepositorio;

        private Programa _programa;
        private Shared.Models.Ruteo _ruteo;
        private string _tipoRemito; // "Carga" o "Entrega"

        public CargaRemitoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ILocacionRepositorio locacionRepositorio,
            IProductoRepositorio productoRepositorio,
            IMedidaRepositorio medidaRepositorio,
            IProgramaRepositorio programaRepositorio)
            : base(sesionService, navigationService)
        {
            _locacionRepositorio = locacionRepositorio;
            _productoRepositorio = productoRepositorio;
            _medidaRepositorio = medidaRepositorio;
            _programaRepositorio = programaRepositorio;
        }

        public async Task InicializarAsync(Programa programa, Shared.Models.Ruteo ruteo, string tipoRemito)
        {
            _programa = programa;
            _ruteo = ruteo;
            _tipoRemito = tipoRemito; // "Carga" o "Entrega"

            // Locaciones (Origen y Destino)
            List<Locacion> locaciones = await _locacionRepositorio.ObtenerTodasAsync();
            List<Producto> productos = await _productoRepositorio.ObtenerTodosAsync();
            List<Medida> medidas = await _medidaRepositorio.ObtenerTodosAsync();

            // Origen para cmbCarga, Destino para cmbDestino (solo mostrar, no editar)
            _view.CargarLocaciones(locaciones);
            _view.CargarProductos(productos);
            _view.CargarMedidas(medidas);

            // Albarán y demás datos según tipo de remito
            _view.CargarDatosIniciales(_programa, _ruteo, _tipoRemito);
        }

        public async Task GuardarAsync()
        {
            // Validaciones
            // ...

            string motivo;
            string descripcion;

            // Actualizar los campos según tipo de remito
            if (_tipoRemito == "Carga")
            {
                _programa.IdOrigen = _view.IdOrigen ?? _programa.IdOrigen;
                _programa.CargaRemito = int.TryParse(_view.RemitoNumero, out int nroRemito) ? nroRemito : (int?)null;
                _programa.CargaRemitoFecha = _view.FechaRemito;
                _programa.CargaRemitoUnidad = _view.IdMedida ?? _programa.CargaRemitoUnidad;
                _programa.CargaRemitoKg = _view.Cantidad ?? _programa.CargaRemitoKg;
                _programa.IdProducto = _view.IdProducto ?? _programa.IdProducto;
                motivo = "Remito/Recibo de Carga";
                descripcion = $"Nro: {_view.RemitoNumero}, Cantidad: {_view.Cantidad}, Fecha: {_view.FechaRemito:dd/MM/yyyy}";
            }
            else
            {
                _programa.EntregaRemito = int.TryParse(_view.RemitoNumero, out int nroRemito) ? nroRemito : (int?)null;
                _programa.EntregaRemitoFecha = _view.FechaRemito;
                _programa.EntregoRemitoUnidad = _view.IdMedida ?? _programa.EntregoRemitoUnidad;
                _programa.EntregaRemitoKg = _view.Cantidad ?? _programa.EntregaRemitoKg;
                _programa.IdProducto = _view.IdProducto ?? _programa.IdProducto;
                motivo = "Remito/Recibo de Entrega";
                descripcion = $"Nro: {_view.RemitoNumero}, Cantidad: {_view.Cantidad}, Fecha: {_view.FechaRemito:dd/MM/yyyy}";
            }

            if (_ruteo.IdDestino != _view.IdDestino)
            {
                await _programaRepositorio.CerrarTramosActivosPorProgramaAsync(
                    _programa.IdPrograma);

                ProgramaTramo tramo = new ProgramaTramo
                {
                    IdPrograma = _programa.IdPrograma,
                    IdNomina = _ruteo.IdNomina,
                    IdDestino = _view.IdDestino ?? 0,
                    FechaInicio = DateTime.Now,
                    FechaFin = null // vacío
                };

                await _programaRepositorio.InsertarProgramaTramoAsync(tramo);
            }

            await _programaRepositorio.ActualizarProgramaAsync(_programa);

            await _programaRepositorio.RegistrarProgramaAsync(
              _programa.IdPrograma,
              motivo,
              descripcion,
              _sesionService.IdUsuario);

            _view.MostrarMensaje("Remito actualizado correctamente.");
            _view.Close();
        }
    }
}