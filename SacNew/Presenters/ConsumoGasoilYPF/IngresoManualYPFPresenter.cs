using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;

namespace SacNew.Presenters
{
    public class IngresoManualYPFPresenter : BasePresenter<IIngresoManualYPFView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConsumoGasoilRepositorio _consumoGasoilRepositorio;
        private int _idPoc;

        public IngresoManualYPFPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            ISesionService sesionService,
         INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _consumoGasoilRepositorio = consumoGasoilRepositorio;
        }

        public async Task CargarDatosAsync(int idPoc)
        {
            _idPoc = idPoc;

            await EjecutarConCargaAsync(async () =>
            {
                var tiposGasoil = (await _conceptoRepositorio.ObtenerPorTipoAsync(6)).ToList();
                _view.CargarTiposGasoil(tiposGasoil);
            });
        }

        public async Task GuardarConsumoAsync()
        {
            if (!_view.HayConsumosValidos())
            {
                _view.MostrarMensaje("Debe ingresar al menos un ticket válido.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var ticketsValidos = _view.Tickets.Where(t => t.EsValido()).ToList();
                foreach (var ticket in ticketsValidos)
                {
                    var consumo = new ConsumoGasoil
                    {
                        IdPOC = _idPoc,
                        IdConsumo = _view.TipoGasoilSeleccionado,
                        FechaCarga = _view.FechaVale,
                        LitrosCargados = ticket.Litros,
                        Observaciones = ticket.Aclaracion,
                        NumeroVale = ticket.Numero,
                        Activo = true
                    };
                    await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);
                }

                var ticketUrea = _view.TicketUrea;
                if (ticketUrea.EsValido())
                {
                    var consumoUrea = new ConsumoGasoil
                    {
                        IdPOC = _idPoc,
                        IdConsumo = 23, // ID para Urea
                        FechaCarga = _view.FechaVale,
                        LitrosCargados = ticketUrea.Litros,
                        Observaciones = ticketUrea.Aclaracion,
                        NumeroVale = ticketUrea.Numero,
                        Activo = true
                    };
                    await _consumoGasoilRepositorio.AgregarConsumoAsync(consumoUrea);
                }

                _view.MostrarMensaje("Consumo(s) registrado(s) exitosamente.");
            });
        }
    }
}