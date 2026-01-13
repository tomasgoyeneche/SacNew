using Core.Base;
using Core.Repositories.RequerimientoCompra;
using Core.Services;
using Servicios.Views.RequerimientosDeCompra;

namespace Servicios.Presenters
{
    public class AgregarImputacionDependenciaPresenter : BasePresenter<IAgregarImputacionDependenciaView>
    {
        private readonly IRcRepositorio _repositorio;

        public AgregarImputacionDependenciaPresenter(
            IRcRepositorio repositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _repositorio = repositorio;
        }

        public async Task CargarDependenciasAsync()
        {
            var dependencias = await _repositorio.ObtenerDependenciasAsync();
            _view.CargarDependencias(dependencias);
        }

        public async void CargarImputacionesPorDependencia(int idDependencia)
        {
            var imputaciones = await _repositorio.ObtenerImputacionesPorDependenciaAsync(idDependencia);
            _view.CargarImputaciones(imputaciones);
        }

        public async void GuardarImputacionDependencia(string descripcion, int idImputacion, int porcentaje)
        {
            // Llamar al formulario anterior (NuevoRcForm)
            await AbrirFormularioAsync<CrearRequerimientoForm>(async form =>
            {
                form.AgregarImputacionDependencia(descripcion, idImputacion, porcentaje);
            });
        }
    }
}