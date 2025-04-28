using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas
{
    public class AgregarUnidadPresenter : BasePresenter<IAgregarUnidadView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        public AgregarUnidadPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IUnidadRepositorio unidadRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio)
            : base(sesionService, navigationService)
        {
            _unidadRepositorio = unidadRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
        }

        public async Task InicializarAsync(int idEmpresa, string nombreEmpresa)
        {
            _view.MostrarEmpresa(nombreEmpresa, idEmpresa); 
            await EjecutarConCargaAsync(async () =>
            {
                var tractores = await _tractorRepositorio.ObtenerTractoresLibresAsync();
                var semis = await _semiRepositorio.ObtenerSemisLibresAsync();

                _view.CargarTractores(tractores);
                _view.CargarSemis(semis);
            });
        }

        public async Task GuardarUnidadAsync()
        {
            if (!_view.IdTractorSeleccionado.HasValue || !_view.IdSemiSeleccionado.HasValue)
            {
                _view.MostrarMensaje("Debe seleccionar un tractor y un semirremolque.");
                return;
            }

            Shared.Models.Tractor tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(_view.IdTractorSeleccionado.Value);
            Semi semi = await _semiRepositorio.ObtenerSemiPorIdAsync(_view.IdSemiSeleccionado.Value);

            if (tractor == null || semi == null)
            {
                _view.MostrarMensaje("No se pudieron cargar correctamente los datos del tractor o del semirremolque.");
                return;
            }

            var unidad = new Unidad
            {
                IdEmpresa = _view.IdEmpresa,
                IdTractor = tractor.IdTractor,
                IdSemi = semi.IdSemi,
                TaraTotal = (int)(tractor.Tara + semi.Tara),
                Metanol = _view.UsaMetanol,
                Gasoil = _view.UsaGasoil,
                LujanCuyo = _view.UsaLujanCuyo,
                AptoBo = _view.UsaAptoBo,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _unidadRepositorio.AgregarUnidadAsync(unidad, _sesionService.IdUsuario);
                _view.MostrarMensaje("Unidad creada exitosamente.");
                _view.Cerrar();
            });
        }
    }
}
