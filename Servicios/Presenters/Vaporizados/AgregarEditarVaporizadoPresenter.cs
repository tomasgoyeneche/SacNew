using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Shared.Models;

namespace Servicios.Presenters
{
    public class AgregarEditarVaporizadoPresenter : BasePresenter<IAgregarEditarVaporizadoView>
    {
        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;
        private readonly IVaporizadoZonaRepositorio _zonaRepo;
        private readonly IPOCRepositorio _pocRepo;
        private readonly IUnidadRepositorio _uniRepo;
        private readonly INominaRepositorio _nomRepo;
        private readonly ITeRepositorio _teRepo;
        private readonly IProductoRepositorio _productoRepo;

        private readonly IConsumoOtrosRepositorio _consumoOtrosRepo;
        private readonly IPostaRepositorio _postaRepo;

        private readonly IVaporizadoMotivoRepositorio _motivoRepo;
        public Vaporizado? VaporizadoActual { get; private set; }

        public GuardiaIngreso? GuardiaActual { get; private set; }

        public AgregarEditarVaporizadoPresenter(
            IVaporizadoRepositorio vaporizadoRepositorio,
            IVaporizadoZonaRepositorio zonaRepo,
            IVaporizadoMotivoRepositorio motivoRepo,
            IPOCRepositorio pocRepositorio,
            IConsumoOtrosRepositorio consumoOtrosRepositorio,
            IPostaRepositorio postaRepositorio,
            IUnidadRepositorio uniRepositorio,
            IProductoRepositorio productoRepositorio,
            INominaRepositorio nominaRepositorio,
            ITeRepositorio teRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _vaporizadoRepositorio = vaporizadoRepositorio;
            _zonaRepo = zonaRepo;
            _motivoRepo = motivoRepo;
            _pocRepo = pocRepositorio;
            _consumoOtrosRepo = consumoOtrosRepositorio;
            _postaRepo = postaRepositorio;
            _uniRepo = uniRepositorio;
            _teRepo = teRepositorio;
            _productoRepo = productoRepositorio;
            _nomRepo = nominaRepositorio;
        }

        public async Task CargarDatosAsync(Vaporizado? vaporizado, GuardiaIngreso guardia)
        {
            VaporizadoActual = vaporizado;
            GuardiaActual = guardia;

            string textoGuardia = string.Empty;

            if (guardia.TipoIngreso == 1)
            {
                Nomina nomina = await _nomRepo.ObtenerPorIdAsync(guardia.IdNomina.Value);
                UnidadDto unidad = await _uniRepo.ObtenerPorIdDtoAsync(nomina.IdUnidad);
                textoGuardia = $"{unidad.Tractor_Patente} - {unidad.Semirremolque_Patente} ({unidad.Empresa_Unidad})";
            }
            else
            {
                TransitoEspecial te = await _teRepo.ObtenerPorIdAsync(guardia.IdTe.Value);
                textoGuardia = $"{te.Tractor} - {te.Semi} ({te.RazonSocial})";
            }
            _view.MostrarDatosGuardia(textoGuardia);

            // Cargar combos
            var zonas = await _zonaRepo.ObtenerTodasAsync();
            var motivos = await _motivoRepo.ObtenerTodosAsync();
            _view.CargarPlantas(zonas.Where(z => z.Activo).ToList());
            _view.CargarMotivos(motivos.Where(m => m.Activo).ToList());

            // Set valores actuales si existen
            if (vaporizado != null)
            {
                _view.CargarDatos(vaporizado);
            }

            // Mostrar/ocultar txtNroPresupuesto y txtNroImporte
            bool mostrar = guardia.TipoIngreso == 1;
            _view.SetNroPresupuestoVisible(mostrar);
            _view.SetNroImporteVisible(mostrar);
        }

        public void CalcularTiempoVaporizado()
        {
            if (_view.FechaInicio.HasValue && _view.FechaFin.HasValue)
            {
                var inicio = _view.FechaInicio.Value;
                var fin = _view.FechaFin.Value;
                if (fin > inicio)
                {
                    var tiempo = fin - inicio;
                    string txt;
                    if (tiempo.TotalMinutes < 60)
                        txt = $"{tiempo.TotalMinutes:N0} min";
                    else
                        txt = $"{(int)tiempo.TotalHours} hs {tiempo.Minutes} min";
                    _view.SetTiempoVaporizado(txt);
                }
                else
                {
                    _view.SetTiempoVaporizado("0 min");
                }
            }
            else
            {
                _view.SetTiempoVaporizado(string.Empty);
            }
        }

        public async Task GuardarAsync()
        {
            // Validación mínima
            if (!_view.IdMotivo.HasValue || !_view.IdPlanta.HasValue || !_view.FechaInicio.HasValue || !_view.FechaFin.HasValue)
            {
                _view.MostrarMensaje("Debe completar motivo, planta y fechas de vaporizado.");
                return;
            }

            // Mapear View → Modelo
            Vaporizado vap = VaporizadoActual ?? new Vaporizado();
            vap.CantidadCisternas = _view.CantidadCisternas;
            vap.IdVaporizadoMotivo = _view.IdMotivo;                 // <- se usa para Producto
            vap.IdPosta = VaporizadoActual?.IdPosta ?? _sesionService.IdPosta; // Posta actual
            vap.FechaInicio = _view.FechaInicio;
            vap.FechaFin = _view.FechaFin;
            vap.IdVaporizadoZona = _view.IdPlanta;
            vap.NroCertificado = _view.NroCertificado;
            vap.RemitoDanes = _view.RemitoDanes;
            vap.Observaciones = _view.Observaciones;
            vap.Activo = true;

            // Guardar/actualizar
            bool esEdicion = vap.IdVaporizado > 0;
            if (esEdicion)
                await _vaporizadoRepositorio.EditarAsync(vap, _sesionService.IdUsuario);
            else
                await _vaporizadoRepositorio.AgregarAsync(vap, _sesionService.IdUsuario);

            // Registrar en Nómina (solo si TipoIngreso == 1)
            if (GuardiaActual?.TipoIngreso == 1)
            {
                // Obtener Producto (por IdMotivo) y Posta (por IdPosta)
                Producto producto = await _productoRepo.ObtenerPorIdAsync(vap.IdVaporizadoMotivo!.Value);
                Posta posta = await _postaRepo.ObtenerPorIdAsync(vap.IdPosta);

                string fechaSoloDia = vap.FechaInicio!.Value.ToString("dd/MM/yyyy"); // solo fecha
                string horaInicio = vap.FechaInicio!.Value.ToString("HH:mm");
                string horaFin = vap.FechaFin!.Value.ToString("HH:mm");
                string nombreProd = producto?.Nombre ?? "-";
                string nombrePosta = posta?.Descripcion ?? "-";
                string nroCert = string.IsNullOrWhiteSpace(vap.NroCertificado) ? "-" : vap.NroCertificado;

                // Descripción con el formato pedido
                string descripcionNomina =
                    $"{fechaSoloDia} Producto {nombreProd} " +
                    $"Inicio: {horaInicio} Fin: {horaFin} " +
                    $"Posta {nombrePosta} NroCertificado {nroCert} " +
                    $"Cisternas: {vap.CantidadCisternas}";

                await _nomRepo.RegistrarNominaAsync(
                    GuardiaActual?.IdNomina ?? 0,
                    esEdicion ? "Edita Vaporizado" : "Alta Vaporizado",
                    descripcionNomina,
                    _sesionService.IdUsuario
                );
            }

            _view.MostrarMensaje("Datos de vaporizado guardados correctamente.");
            _view.Cerrar();
        }

        public async Task CrearConsumoOtrosAsync(Vaporizado vap, GuardiaIngreso guardia, string nroPresupuesto, string nroImporte)
        {
            // 1. Obtener el objeto Posta
            var posta = await _postaRepo.ObtenerPorIdAsync(guardia.IdPosta);
            if (posta == null)
                throw new Exception("No se encontró la posta asociada.");

            // 2. Armar el número de POC
            string numeroPoc = $"{posta.Codigo}-{guardia.IdGuardiaIngreso}";

            // 3. Buscar el POC
            var poc = await _pocRepo.ObtenerPorNumeroAsync(numeroPoc);
            if (poc == null)
                throw new Exception($"No se encontró POC con número {numeroPoc}.");

            // 4. Armar el objeto ConsumoOtros
            var consumo = new ConsumoOtros
            {
                IdPOC = poc.IdPoc,
                IdConsumo = 5, // Según tu flujo
                NumeroVale = nroPresupuesto,
                Cantidad = 1,
                ImporteTotal = decimal.TryParse(nroImporte, out var importe) ? importe : 0,
                Aclaracion = "",
                FechaRemito = vap.FechaFin ?? DateTime.Now, // Si es null, usá ahora
                Activo = true,
                Dolar = false,
                PrecioDolar = 0
            };

            // 5. Insertar el registro usando el repo
            await _consumoOtrosRepo.AgregarConsumoAsync(consumo);
        }
    }
}