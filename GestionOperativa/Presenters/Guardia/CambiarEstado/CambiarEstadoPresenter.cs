using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class CambiarEstadoPresenter : BasePresenter<ICambiarEstadoView>
    {
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;

        private GuardiaDto _guardia;

        public CambiarEstadoPresenter(ISesionService sesion, INavigationService nav, IGuardiaRepositorio guardiaRepositorio, IVaporizadoRepositorio vaporizadoRepositorio)
            : base(sesion, nav)
        {
            _guardiaRepositorio = guardiaRepositorio;
            _vaporizadoRepositorio = vaporizadoRepositorio;
        }

        public Task InicializarAsync(GuardiaDto guardia, bool admin)
        {
            _guardia = guardia;
            _view.InicializarBotones(_guardia, admin);
            return Task.CompletedTask;
        }

        public async Task EliminarEventoAsync()
        {
            await AbrirFormularioAsync<EliminarEventoForm>(async form =>
            {
                await form._presenter.InicializarAsync(_guardia);
            });
        }

        private async Task CambiarEstadoAsync(int idEstado, DateTime fecha, string observacion)
        {
            await _guardiaRepositorio.RegistrarCambioEstadoAsync(_guardia.IdGuardiaIngreso, _sesionService.IdUsuario, fecha, idEstado, observacion);
            _view.MostrarMensaje("Estado actualizado.");
            _view.Close();
        }

        public async Task RegistrarCambio(bool manual, int Estado, string observacion)
        {
            DateTime fecha;
            switch (Estado)
            {
                case 5:
                    fecha = manual ? _view.FechaCompleta.GetValueOrDefault() : DateTime.Now;
                    break;

                case 2:
                    fecha = manual ? _view.FechaTractor.GetValueOrDefault() : DateTime.Now;
                    break;

                case 3:
                    fecha = manual ? _view.FechaChofer.GetValueOrDefault() : DateTime.Now;
                    break;

                case 6:
                    fecha = manual ? _view.FechaReingreso.GetValueOrDefault() : DateTime.Now;
                    break;
                case 7:
                    RegistrarVaporizadoDesdeGuardiaAsync(_guardia);
                    fecha = DateTime.Now;
                    break;
                default:
                    fecha = DateTime.Now;
                    break;
            }
            await CambiarEstadoAsync(Estado, fecha, observacion + (manual ? " - Manual" : ""));
        }

        public async Task RegistrarVaporizadoDesdeGuardiaAsync(GuardiaDto guardia)
        {
            var vaporizado = new Vaporizado
            {
                NroCertificado = null, // Si lo tenés en otro lado, asignalo
                RemitoDanes = null,
                IdPosta = guardia.IdPosta,
                CantidadCisternas = null, // Cambialo si tenés otro origen
                IdVaporizadoMotivo = null, // O lo que corresponda
                FechaInicio = null,
                FechaFin = null,
                IdVaporizadoZona = null, // O lo que corresponda
                TipoIngreso = guardia.TipoIngreso,
                EsExterno = false,
                IdNomina = guardia.TipoIngreso == 1 ? guardia.IdEntidad : null,
                IdTe = guardia.TipoIngreso == 2 ? guardia.IdEntidad : null,
                Observaciones = null,
                Activo = true
            };

            await _vaporizadoRepositorio.AgregarAsync(vaporizado, _sesionService.IdUsuario);
        }
    }
}