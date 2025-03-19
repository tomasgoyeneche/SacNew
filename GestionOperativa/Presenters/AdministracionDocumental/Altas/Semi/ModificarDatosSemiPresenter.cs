using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Semis;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class ModificarDatosSemiPresenter : BasePresenter<IModificarDatosSemiView>
    {
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IVehiculoMarcaRepositorio _marcaRepositorio;
        private readonly IVehiculoModeloRepositorio _modeloRepositorio;
        private readonly ISemiCisternaTipoCargaRepositorio _tipoCargaRepositorio;
        private readonly ISemiCisternaMaterialRepositorio _materialRepositorio;

        public ModificarDatosSemiPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ISemiRepositorio semiRepositorio,
            IVehiculoMarcaRepositorio marcaRepositorio,
            IVehiculoModeloRepositorio modeloRepositorio,
            ISemiCisternaTipoCargaRepositorio tipoCargaRepositorio,
            ISemiCisternaMaterialRepositorio materialRepositorio)
            : base(sesionService, navigationService)
        {
            _semiRepositorio = semiRepositorio;
            _marcaRepositorio = marcaRepositorio;
            _modeloRepositorio = modeloRepositorio;
            _tipoCargaRepositorio = tipoCargaRepositorio;
            _materialRepositorio = materialRepositorio;
        }

        public async Task InicializarAsync(int idSemi)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var semi = await _semiRepositorio.ObtenerSemiPorIdAsync(idSemi);
                if (semi == null)
                {
                    _view.MostrarMensaje("No se encontró el semirremolque.");
                    return;
                }

                var marcas = await _marcaRepositorio.ObtenerMarcasPorTipoAsync(2);
                var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(semi.IdMarca);
                var tiposCarga = await _tipoCargaRepositorio.ObtenerTiposCargaAsync();
                var materiales = await _materialRepositorio.ObtenerMaterialesAsync();

                _view.CargarDatosSemi(semi, marcas, modelos, tiposCarga, materiales);
            });
        }

        public async Task CargarModelos(int idMarca)
        {
            var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(idMarca);
            _view.CargarModelos(modelos);
        }

        public async Task GuardarCambios()
        {
            var semi = new ModificarSemiDto
            {
                IdSemi = _view.IdSemi,
                Patente = _view.Patente,
                Anio = _view.Anio,
                IdMarca = _view.IdMarca,
                IdModelo = _view.IdModelo,
                Tara = _view.Tara,
                FechaAlta = _view.FechaAlta,
                IdTipoCarga = _view.IdTipoCarga,
                Compartimientos = _view.Compartimientos,
                IdMaterial = _view.IdMaterial
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _semiRepositorio.ActualizarSemiAsync(semi);
                _view.MostrarMensaje("Datos del semirremolque actualizados correctamente.");
            });
        }
    }
}
