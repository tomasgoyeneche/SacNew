using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.LimiteDeCredito;

namespace GestionFlota.Presenters
{
    public class CrearEditarCreditoPresenter : BasePresenter<ICrearEditarCreditoView>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IEmpresaCreditoRepositorio _creditoRepositorio;

        public CrearEditarCreditoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaRepositorio empresaRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IEmpresaCreditoRepositorio creditoRepositorio
        ) : base(sesionService, navigationService)
        {
            _empresaRepositorio = empresaRepositorio;
            _periodoRepositorio = periodoRepositorio;
            _creditoRepositorio = creditoRepositorio;
        }

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var empresas = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
                _view.CargarEmpresas(empresas);
            });
        }

        public async Task VerificarCreditoExistente()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var idPeriodo = await ObtenerIdPeriodoAsync();
                if (idPeriodo == null)
                {
                    _view.MostrarMensaje("El período seleccionado no existe.");
                    return;
                }

                var credito = await _creditoRepositorio.ObtenerCreditoPorEmpresaYPeriodoAsync(_view.IdEmpresa, idPeriodo.Value);
                _view.MostrarCreditoActual(credito);
            });
        }

        public async Task GuardarCreditoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var idPeriodo = await ObtenerIdPeriodoAsync();
                if (idPeriodo == null)
                {
                    _view.MostrarMensaje("No se puede asignar crédito porque el período no existe.");
                    return;
                }

                var creditoActual = await _creditoRepositorio.ObtenerCreditoPorEmpresaYPeriodoAsync(_view.IdEmpresa, idPeriodo.Value);

                if (creditoActual == null)
                {
                    // 🔹 Insertar nuevo crédito
                    await _creditoRepositorio.InsertarCreditoAsync(_view.IdEmpresa, idPeriodo.Value, _view.CreditoAsignado);
                    _view.MostrarMensaje("Crédito asignado correctamente.");
                }
                else
                {
                    // 🔹 Actualizar crédito existente
                    await _creditoRepositorio.ActualizarCreditoPeriodoAsync(_view.IdEmpresa, idPeriodo.Value, _view.CreditoAsignado);
                    _view.MostrarMensaje("Crédito actualizado correctamente.");
                }

                _view.Close();
            });
        }

        private async Task<int?> ObtenerIdPeriodoAsync()
        {
            var fecha = _view.PeriodoSeleccionado;
            return await _periodoRepositorio.ObtenerIdPeriodoPorMesAnioAsync(fecha.Month, fecha.Year);
        }
    }
}