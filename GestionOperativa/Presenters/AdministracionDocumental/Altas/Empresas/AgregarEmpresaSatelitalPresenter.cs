using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Empresas;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class AgregarEmpresaSatelitalPresenter : BasePresenter<IAgregarEmpresaSatelitalView>
    {
        private readonly IEmpresaSatelitalRepositorio _empresaSatelitalRepositorio;
        private readonly ISatelitalRepositorio _satelitalRepositorio;

        public AgregarEmpresaSatelitalPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaSatelitalRepositorio empresaSatelitalRepositorio,
            ISatelitalRepositorio satelitalRepositorio)
            : base(sesionService, navigationService)
        {
            _empresaSatelitalRepositorio = empresaSatelitalRepositorio;
            _satelitalRepositorio = satelitalRepositorio;
        }

        public async Task InicializarAsync(int idEmpresa)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var satelitales = await _satelitalRepositorio.ObtenerTodosAsync();
                _view.CargarSatelitales(satelitales);
                _view.Inicializar(idEmpresa);
            });
        }

        public async Task GuardarEmpresaSatelital()
        {
            var empresaSatelital = new EmpresaSatelital
            {
                IdEmpresa = _view.IdEmpresa,
                IdSatelital = _view.IdSatelital,
                Usuario = _view.Usuario,
                Clave = _view.Clave,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _empresaSatelitalRepositorio.AgregarAsync(empresaSatelital);
                _view.MostrarMensaje("Empresa satelital agregada correctamente.");
            });
        }
    }
}