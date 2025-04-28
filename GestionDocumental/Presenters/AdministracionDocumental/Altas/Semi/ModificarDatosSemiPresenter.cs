using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Semis;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class ModificarDatosSemiPresenter : BasePresenter<IModificarDatosSemiView>
    {
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IVehiculoMarcaRepositorio _marcaRepositorio;
        private readonly IVehiculoModeloRepositorio _modeloRepositorio;
        private readonly ISemiCisternaTipoCargaRepositorio _tipoCargaRepositorio;
        private readonly ISemiCisternaMaterialRepositorio _materialRepositorio;
        public Semi Semi { get; private set; }

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
                Semi = await _semiRepositorio.ObtenerSemiPorIdAsync(idSemi);
                if (Semi == null)
                {
                    _view.MostrarMensaje("No se encontró el semirremolque.");
                    return;
                }

                var marcas = await _marcaRepositorio.ObtenerMarcasPorTipoAsync(2);
                var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(Semi.IdMarca);
                var tiposCarga = await _tipoCargaRepositorio.ObtenerTiposCargaAsync();
                var materiales = await _materialRepositorio.ObtenerMaterialesAsync();

                _view.CargarDatosSemi(Semi, marcas, modelos, tiposCarga, materiales);
            });
        }

        public async Task CargarModelos(int idMarca)
        {
            var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(idMarca);
            _view.CargarModelos(modelos);
        }

        public async Task GuardarCambios()
        {
            Semi.IdSemi = _view.IdSemi;
            Semi.Patente = _view.Patente;
            Semi.Anio = _view.Anio;
            Semi.IdMarca = _view.IdMarca;
            Semi.IdModelo = _view.IdModelo;
            Semi.Tara = _view.Tara;
            Semi.FechaAlta = _view.FechaAlta;
            Semi.IdTipoCarga = _view.IdTipoCarga;
            Semi.Compartimientos = _view.Compartimientos;
            Semi.IdMaterial = _view.IdMaterial;
            Semi.Inv = _view.Inv;   
            Semi.LitroNominal = _view.LitroNominal;
            Semi.Cubicacion = _view.Cubicacion; 

            await EjecutarConCargaAsync(async () =>
            {
                await _semiRepositorio.ActualizarSemiAsync(Semi);
                _view.MostrarMensaje("Datos del semirremolque actualizados correctamente.");
            });
        }
    }
}