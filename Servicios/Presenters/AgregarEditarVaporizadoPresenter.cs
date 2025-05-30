using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class AgregarEditarVaporizadoPresenter : BasePresenter<IAgregarEditarVaporizadoView>
    {
        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;
        private readonly IVaporizadoZonaRepositorio _zonaRepo;
        private readonly IPOCRepositorio _pocRepo;
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepo;
        private readonly IPostaRepositorio _postaRepo;

        private readonly IVaporizadoMotivoRepositorio _motivoRepo;
        public Vaporizado? VaporizadoActual { get; private set; }

        public GuardiaDto? GuardiaActual { get; private set; }

        public AgregarEditarVaporizadoPresenter(
            IVaporizadoRepositorio vaporizadoRepositorio,
            IVaporizadoZonaRepositorio zonaRepo,
            IVaporizadoMotivoRepositorio motivoRepo,
            IPOCRepositorio pocRepositorio,
            IConsumoOtrosRepositorio consumoOtrosRepositorio,
            IPostaRepositorio postaRepositorio,
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
        }

        public async Task CargarDatosAsync(Vaporizado? vaporizado, GuardiaDto guardia)
        {
            VaporizadoActual = vaporizado;
            GuardiaActual = guardia;

            // Label principal: Tractor-patente (Empresa)
            string textoGuardia = $"{guardia.Tractor} - {guardia.Semi} ({guardia.Empresa})";
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

            // Guardar
            Vaporizado vap = VaporizadoActual ?? new Vaporizado();
            vap.CantidadCisternas = _view.CantidadCisternas;
            vap.IdVaporizadoMotivo = _view.IdMotivo;
            vap.IdPosta = GuardiaActual?.IdPosta ?? 0; // Asignar posta de la guardia actual
            vap.FechaInicio = _view.FechaInicio;
            vap.FechaFin = _view.FechaFin;
            vap.IdVaporizadoZona = _view.IdPlanta;
            vap.NroCertificado = _view.NroCertificado;
            vap.RemitoDanes = _view.RemitoDanes;
            vap.Observaciones = _view.Observaciones;
            vap.Activo = true;

            // Extras para tipo ingreso 1
            if (GuardiaActual?.TipoIngreso == 1)
            {
                string NroPresupuesto = _view.NroPresupuesto;
                string NroImporte = _view.NroImporte;

                if (!string.IsNullOrWhiteSpace(_view.NroPresupuesto) && !string.IsNullOrWhiteSpace(_view.NroImporte))
                {
                    await CrearConsumoOtrosAsync(vap, GuardiaActual, _view.NroPresupuesto, _view.NroImporte);
                }
            }

            // Guardar/actualizar
            if (vap.IdVaporizado > 0)
                await _vaporizadoRepositorio.EditarAsync(vap, _sesionService.IdUsuario);
            else
                await _vaporizadoRepositorio.AgregarAsync(vap, _sesionService.IdUsuario);

            _view.MostrarMensaje("Datos de vaporizado guardados correctamente.");
            _view.Cerrar();
        }

        public async Task CrearConsumoOtrosAsync(Vaporizado vap, GuardiaDto guardia, string nroPresupuesto, string nroImporte)
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
