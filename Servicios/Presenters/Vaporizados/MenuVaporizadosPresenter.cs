using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Shared.Models;

namespace Servicios.Presenters
{
    public class MenuVaporizadosPresenter : BasePresenter<IMenuVaporizadosView>
    {
        private readonly IVaporizadoRepositorio _repositorioVaporizado;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private readonly ICsvService _csvService;
        private readonly INominaRepositorio _nominaRepositorio;

        private int _IdPosta;

        public MenuVaporizadosPresenter(
            IVaporizadoRepositorio repositorioVaporizado,
            ICsvService csvService,
            IUnidadRepositorio unidadRepositorio,
            INominaRepositorio nominaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService,
            IGuardiaRepositorio guardiaRepositorio)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _repositorioVaporizado = repositorioVaporizado;
            _unidadRepositorio = unidadRepositorio;
            _csvService = csvService;
            _guardiaRepositorio = guardiaRepositorio;
            _nominaRepositorio = nominaRepositorio;
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
            GuardiaIngreso? guardia = await _guardiaRepositorio.ObtenerGuardiaPorId(vapo.IdGuardiaIngreso);

           
            if (vapoDto.Externo != "No")
            {
                Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(vapo.IdNomina.Value);
                UnidadDto? unidad = await _unidadRepositorio.ObtenerPorIdDtoAsync(nomina.IdUnidad);

                await EjecutarConCargaAsync(async () =>
                {
                    await AbrirFormularioAsync<AgregarEditarVaporizadosExternosForm>(async form =>
                    {
                        await form._presenter.CargarDatosAsync(vapo, unidad);
                    });
                }, async () => await CargarVaporizadosAsync(_IdPosta));
            }
            else
            {
                await EjecutarConCargaAsync(async () =>
                {
                    await AbrirFormularioAsync<AgregarEditarVaporizadoForm>(async form =>
                    {
                        await form._presenter.CargarDatosAsync(vapo, guardia);
                    });
                }, async () => await CargarVaporizadosAsync(_IdPosta));
            }          
        }

        public async Task AgregarVaporizadoExternoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarVaporizadosExternosForm>(async form =>
                {
                    await form._presenter.CargarDatosAsync(null, null);
                });
            }, async () => await CargarVaporizadosAsync(_IdPosta));
        }
    }
}