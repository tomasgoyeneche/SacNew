using Core.Base;
using Core.Repositories;
using Core.Services;
using Shared.Models;

namespace Servicios.Presenters
{
    public class MenuVaporizadosPresenter : BasePresenter<IMenuVaporizadosView>
    {
        private readonly IVaporizadoRepositorio _repositorioVaporizado;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private readonly ICsvService _csvService;

        private int _IdPosta;

        public MenuVaporizadosPresenter(
            IVaporizadoRepositorio repositorioVaporizado,
            ICsvService csvService,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            INavigationService navigationService,
            IGuardiaRepositorio guardiaRepositorio)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _repositorioVaporizado = repositorioVaporizado;
            _unidadRepositorio = unidadRepositorio;
            _csvService = csvService;
            _guardiaRepositorio = guardiaRepositorio;
        }

        public async Task CargarVaporizadosAsync(int idPosta)
        {
            _IdPosta = idPosta;
            if (idPosta == 1)
            {
                await EjecutarConCargaAsync(
                () => _repositorioVaporizado.ObtenerTodosLosVaporizadosDto(),
                _view.MostrarVaporizados
                );
            }
            else
            {
                await EjecutarConCargaAsync(
                () => _repositorioVaporizado.ObtenerVaporizadosDtoPorPosta(idPosta),
                _view.MostrarVaporizados
                );
            }
        }

        public async Task EliminarVaporizadoAsync(VaporizadoDto vaporizado)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar el vaporizado?");
                if (confirmacion != DialogResult.Yes) return;

                await _repositorioVaporizado.EliminarAsync(vaporizado.IdVaporizado);

                _view.MostrarMensaje("Vaporizado eliminado correctamente.");
            }, async () => await CargarVaporizadosAsync(_IdPosta));
        }

        public async Task EditarVaporizadoAsync(VaporizadoDto vapoDto)
        {
            Vaporizado? vapo = await _repositorioVaporizado.ObtenerPorIdAsync(vapoDto.IdVaporizado);
            GuardiaDto? guardia = await _guardiaRepositorio.ObtenerGuardiaDtoPorId(vapo.IdGuardiaIngreso);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarVaporizadoForm>(async form =>
                {
                    await form._presenter.CargarDatosAsync(vapo, guardia);
                });
            }, async () => await CargarVaporizadosAsync(_IdPosta));
        }

        //public async Task AgregarVaporizadoExternoAsync()
        //{
        //    await EjecutarConCargaAsync(async () =>
        //    {
        //            await AbrirFormularioAsync<AgregarEditarVaporizadoExterno>(async form =>
        //            {
        //                await form._presenter.InicializarAsync(null);
        //            });
        //    }, async () => await CargarVaporizadosAsync(_IdPosta));
        //}
    }
}