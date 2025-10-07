using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class AgregarEditarDisponiblePresenter : BasePresenter<IAgregarEditarDisponibleView>
    {
        private readonly IDisponibilidadRepositorio _disponibilidadRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IUnidadMantenimientoRepositorio _unidadMantenimientoRepositorio;
        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;

        public Disponible? DisponibleActual { get; private set; }
        public int IdNomina { get; private set; }
        public DateTime FechaDisponible { get; private set; }

        public AgregarEditarDisponiblePresenter(
            IDisponibilidadRepositorio disponibilidadRepositorio,
            ILocacionProductoRepositorio locacionProductoRepositorio,
            ILocacionRepositorio locacionRepositorio,
            IProductoRepositorio productoRepositorio,
            ISesionService sesionService,
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            INominaRepositorio nominaRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _disponibilidadRepositorio = disponibilidadRepositorio;
            _locacionRepositorio = locacionRepositorio;
            _unidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        public async Task InicializarAsync(Disponibilidad dispo, DateTime fechaSeleccionada)
        {
            IdNomina = dispo.IdNomina;
            FechaDisponible = fechaSeleccionada;

            List<Locacion> origenes = await _locacionRepositorio.ObtenerTodasAsync();
            _view.CargarOrigenes(origenes);

            List<Locacion> destinos = await _locacionRepositorio.ObtenerTodasAsync();
            _view.CargarDestinos(destinos);

            DisponibleActual = await _disponibilidadRepositorio.ObtenerDisponiblePorNominaYFechaAsync(IdNomina, FechaDisponible);
            await MostrarMantenimientosyFrancosDelChoferAsync(IdNomina);

            if (DisponibleActual != null)
            {
                // Editar existente
                _view.MostrarDisponible(DisponibleActual);
            }
            else
            {
                // Agregar nuevo
                _view.MostrarDisponible(new Disponible
                {
                    IdNomina = IdNomina,
                    FechaDisponible = FechaDisponible
                });
            }
        }

        public async Task ActualizarCuposDisponiblesAsync(int idOrigen)
        {
            var cuposUsados = await _disponibilidadRepositorio.ObtenerCuposUsadosAsync(idOrigen, FechaDisponible);
            var locacionProductos = await _locacionProductoRepositorio.ObtenerPorLocacionIdAsync(idOrigen);
            List<int> productosId = locacionProductos.Select(p => p.IdProducto).ToList();
            List<Producto> productos = new List<Producto>();

            foreach (int idProducto in productosId)
            {
                Producto? producto = await _productoRepositorio.ObtenerPorIdAsync(idProducto);
                if (producto != null)
                {
                    productos.Add(producto);
                }
            }
            // Rango: 1..7, quitando los ya usados (y el del actual si se está editando)
            var cuposDisponibles = CalcularCuposDisponibles(cuposUsados);

            // Si edita y su propio cupo está en la lista usada, debe poder verlo
            if (DisponibleActual != null && !cuposDisponibles.Contains(DisponibleActual.Cupo) && DisponibleActual.IdOrigen == idOrigen)
                cuposDisponibles.Add(DisponibleActual.Cupo);

            _view.CargarCupos(cuposDisponibles.OrderBy(x => x).ToList());
            _view.CargarProductos(productos);
        }

        public List<int> CalcularCuposDisponibles(List<int> cuposOcupados)
        {
            var disponibles = new List<int>();
            int cupo = 1;

            // Hacemos esto hasta tener 5 cupos disponibles
            while (disponibles.Count < 5)
            {
                if (!cuposOcupados.Contains(cupo))
                {
                    disponibles.Add(cupo);
                }
                cupo++;
            }
            return disponibles;
        }

        public async Task RegistrarComentarioAsync(string comentario)
        {
            await _nominaRepositorio.RegistrarNominaAsync(
                IdNomina, // o el disponible.IdNomina según contexto
                "Comentario",
                comentario,
                _sesionService.IdUsuario
            );
            _view.MostrarMensaje("Comentario registrado correctamente.");
        }

        public async Task GuardarAsync()
        {
           
            var disponible = _view.ObtenerDisponible();
            disponible.IdNomina = IdNomina;
            disponible.FechaDisponible = FechaDisponible;
            disponible.IdUsuario = _sesionService.IdUsuario;
            disponible.IdDisponibleEstado = 1; // Asumiendo que 1 es el estado "Activo"

            if(disponible.IdOrigen == null)
            {
                _view.MostrarMensaje("Debe seleccionar un origen.");
                return;
            }

            if (await ValidarAsync(disponible))
            {
                // Origen y destino (consultas siempre, porque descripción la necesitás para ambos casos)
                var origen = await _locacionRepositorio.ObtenerPorIdAsync(disponible.IdOrigen);
                var destino = disponible.IdDestino.HasValue
                    ? await _locacionRepositorio.ObtenerPorIdAsync(disponible.IdDestino.Value)
                    : null;

                string descripcion = $"{disponible.FechaDisponible:dd/MM/yyyy} - {origen.Nombre} - {(destino?.Nombre ?? "Sin Destino")}";

                if (DisponibleActual != null)
                {
                    disponible.IdDisponible = DisponibleActual.IdDisponible;
                    await _disponibilidadRepositorio.ActualizarDisponibleAsync(disponible);

                    await _nominaRepositorio.RegistrarNominaAsync(
                        disponible.IdNomina,
                        "Edita Disponibilidad",
                        descripcion,
                        _sesionService.IdUsuario
                    );

                    _view.MostrarMensaje("Disponible actualizado correctamente.");
                }
                else
                {
                    await _disponibilidadRepositorio.AgregarDisponibleAsync(disponible);

                    await _nominaRepositorio.RegistrarNominaAsync(
                        disponible.IdNomina,
                        "Alta Disponibilidad",
                        descripcion,
                        _sesionService.IdUsuario
                    );

                    _view.MostrarMensaje("Disponible agregado correctamente.");
                }
                _view.Cerrar();
            }
        }

        public async Task MostrarMantenimientosyFrancosDelChoferAsync(int idNomina)
        {
            Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(idNomina);

            // Unidades
            List<UnidadMantenimientoDto> mantenimientos = await _unidadMantenimientoRepositorio.ObtenerPorUnidadAsync(nomina.IdUnidad);
            string textoMantenimientos = (mantenimientos != null && mantenimientos.Any())
                ? string.Join(Environment.NewLine,
                    mantenimientos.Select(m =>
                        $"{m.Descripcion} - fecha inicio: {m.FechaInicio:dd/MM/yyyy} - fecha fin: {m.FechaFin:dd/MM/yyyy}")
                )
                : "Sin mantenimientos asignados";
            _view.MostrarMantenimientosUnidad(textoMantenimientos);

            // Chofer
            List<NovedadesChoferesDto> ausencias = await _choferEstadoRepositorio.ObtenerPorChoferAsync(nomina.IdChofer);
            string textoAusencias = (ausencias != null && ausencias.Any())
                ? string.Join(Environment.NewLine,
                    ausencias.Select(m =>
                        $"{m.Descripcion} - fecha inicio: {m.FechaInicio:dd/MM/yyyy} - fecha fin: {m.FechaFin:dd/MM/yyyy}")
                )
                : "Sin francos asignados";
            _view.MostrarAusenciasChofer(textoAusencias);
        }

        public async Task AbrirCambiarChoferAsync()
        {
            await AbrirFormularioAsync<CambioChoferForm>(async form =>
            {
                await form._presenter.InicializarAsync(IdNomina);
            });
            _view.Cerrar();
        }
    }
}