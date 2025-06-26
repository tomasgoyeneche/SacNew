using Core.Base;
using Core.Repositories;
using Core.Repositories.Semi;
using Core.Services;
using DevExpress.XtraEditors;
using GestionOperativa.Views.AdministracionDocumental.Altas.Semis;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class ModificarDatosSemiPresenter : BasePresenter<IModificarDatosSemiView>
    {
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IVehiculoMarcaRepositorio _marcaRepositorio;
        private readonly IVehiculoModeloRepositorio _modeloRepositorio;
        private readonly ISemiCisternaTipoCargaRepositorio _tipoCargaRepositorio;
        private readonly ISemiCisternaMaterialRepositorio _materialRepositorio;
        private readonly ISemiCisternaCompartimientoRepositorio _semiCompartimientoRepositorio;

        public Semi Semi { get; private set; }

        public ModificarDatosSemiPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ISemiRepositorio semiRepositorio,
            IVehiculoMarcaRepositorio marcaRepositorio,
            IVehiculoModeloRepositorio modeloRepositorio,
            ISemiCisternaTipoCargaRepositorio tipoCargaRepositorio,
            ISemiCisternaCompartimientoRepositorio semiCompartimientoRepositorio,
            ISemiCisternaMaterialRepositorio materialRepositorio)
            : base(sesionService, navigationService)
        {
            _semiRepositorio = semiRepositorio;
            _marcaRepositorio = marcaRepositorio;
            _modeloRepositorio = modeloRepositorio;
            _tipoCargaRepositorio = tipoCargaRepositorio;
            _materialRepositorio = materialRepositorio;
            _semiCompartimientoRepositorio = semiCompartimientoRepositorio;
        }

        public async Task InicializarAsync(int idSemi, string litros)
        {
            await EjecutarConCargaAsync(async () =>
            {
                Semi = await _semiRepositorio.ObtenerSemiPorIdAsync(idSemi);
                if (Semi == null)
                {
                    _view.MostrarMensaje("No se encontró el semirremolque.");
                    return;
                }

                var marcas = await _marcaRepositorio.ObtenerMarcasPorTipoAsync(2);
                var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(Semi.IdMarca);
                var tiposCarga = await _tipoCargaRepositorio.ObtenerTiposCargaAsync();
                var materiales = await _materialRepositorio.ObtenerMaterialesAsync();

                _view.CargarDatosSemi(Semi, marcas, modelos, tiposCarga, materiales, litros);
            });
        }

        public async Task CargarModelos(int idMarca)
        {
            var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(idMarca);
            _view.CargarModelos(modelos);
        }

        public async Task GuardarCambios()
        {
            Semi.IdSemi = _view.IdSemi;
            Semi.Patente = _view.Patente;
            Semi.Anio = _view.Anio;
            Semi.IdMarca = _view.IdMarca;
            Semi.IdModelo = _view.IdModelo;
            Semi.Tara = _view.Tara;
            Semi.FechaAlta = _view.FechaAlta;
            Semi.IdTipoCarga = _view.IdTipoCarga;
            Semi.Compartimientos = _view.Compartimientos;
            Semi.IdMaterial = _view.IdMaterial;
            Semi.Inv = _view.Inv;
            Semi.LitroNominal = _view.LitroNominal;
            Semi.Cubicacion = _view.Cubicacion;

            await EjecutarConCargaAsync(async () =>
            {
                await _semiRepositorio.ActualizarSemiAsync(Semi);
                _view.MostrarMensaje("Datos del semirremolque actualizados correctamente.");
            });
        }

        public async Task AgregarCompartimientoAsync()
        {
            // Validar que no supere la cantidad máxima de compartimientos
            var compartimientosActivos = await _semiCompartimientoRepositorio.ObtenerCompartimientosActivosAsync(Semi.IdSemi);

            if (compartimientosActivos.Count >= Semi.Compartimientos)
            {
                _view.MostrarMensaje($"No se pueden agregar más compartimientos. Máximo: {Semi.Compartimientos}");
                return;
            }

            // Pedir al usuario la capacidad
            string litrosStr = XtraInputBox.Show("Ingrese la capacidad en litros para el nuevo compartimiento:",
                "Agregar Compartimiento", "0");
            if (!int.TryParse(litrosStr, out int litros) || litros <= 0)
            {
                _view.MostrarMensaje("Ingrese un valor válido y mayor a cero.");
                return;
            }

            // Número de compartimiento nuevo (el siguiente disponible)
            int nuevoNumero = (compartimientosActivos.Any() ? compartimientosActivos.Max(c => c.NumeroCompartimiento) : 0) + 1;

            var nuevoCompartimiento = new SemiCisternaCompartimiento
            {
                IdSemi = Semi.IdSemi,
                NumeroCompartimiento = nuevoNumero,
                CapacidadLitros = litros,
                Activo = true
            };

            await _semiCompartimientoRepositorio.AgregarCompartimientoAsync(nuevoCompartimiento);

            // Refrescar la confección
            await ActualizarConfeccionAsync();
            _view.MostrarMensaje("Compartimiento agregado correctamente.");
        }

        public async Task EliminarCompartimientoAsync()
        {
            // Obtener compartimientos activos
            var compartimientosActivos = await _semiCompartimientoRepositorio.ObtenerCompartimientosActivosAsync(Semi.IdSemi);
            if (!compartimientosActivos.Any())
            {
                _view.MostrarMensaje("No hay compartimientos para eliminar.");
                return;
            }

            // Eliminar el de mayor número
            var paraEliminar = compartimientosActivos.OrderByDescending(c => c.NumeroCompartimiento).First();

            await _semiCompartimientoRepositorio.EliminarCompartimientoAsync(paraEliminar.IdCompartimiento);

            // Refrescar la confección
            await ActualizarConfeccionAsync();
            _view.MostrarMensaje($"Compartimiento número {paraEliminar.NumeroCompartimiento} eliminado.");
        }

        private async Task ActualizarConfeccionAsync()
        {
            // Refresca la confección y la muestra en el txtConfeccion
            var compartimientos = await _semiCompartimientoRepositorio.ObtenerCompartimientosActivosAsync(Semi.IdSemi);
            string confeccion = string.Join(" + ", compartimientos.OrderBy(c => c.NumeroCompartimiento).Select(c => c.CapacidadLitros));
            _view.ActualizarConfeccion(confeccion);
        }
    }
}