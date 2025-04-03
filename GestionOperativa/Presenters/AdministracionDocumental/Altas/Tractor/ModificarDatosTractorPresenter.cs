using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Tractores;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class ModificarDatosTractorPresenter : BasePresenter<IModificarDatosTractorView>
    {
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly IVehiculoMarcaRepositorio _marcaRepositorio;
        private readonly IVehiculoModeloRepositorio _modeloRepositorio;
        private readonly IEmpresaSatelitalRepositorio _empresaSatelitalRepositorio;
        private Shared.Models.Tractor Tractor { get; set; }
        public ModificarDatosTractorPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ITractorRepositorio tractorRepositorio,
            IVehiculoMarcaRepositorio marcaRepositorio,
            IVehiculoModeloRepositorio modeloRepositorio,
            IEmpresaSatelitalRepositorio empresaSatelitalRepositorio)
            : base(sesionService, navigationService)
        {
            _tractorRepositorio = tractorRepositorio;
            _marcaRepositorio = marcaRepositorio;
            _modeloRepositorio = modeloRepositorio;
            _empresaSatelitalRepositorio = empresaSatelitalRepositorio;
        }

        public async Task InicializarAsync(int idTractor, string SatelitalNombre)
        {
            await EjecutarConCargaAsync(async () =>
            {
                Tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(idTractor);
                if (Tractor == null)
                {
                    _view.MostrarMensaje("No se encontró el tractor.");
                    return;
                }

                var marcas = await _marcaRepositorio.ObtenerMarcasPorTipoAsync(1);
                var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(Tractor.IdMarca);

                _view.CargarDatosTractor(Tractor, marcas, modelos, SatelitalNombre);
            });
        }

        public async Task CargarModelos(int idMarca)
        {
            var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(idMarca);
            _view.CargarModelos(modelos);
        }

        public async Task GuardarCambios()
        {

            Tractor.IdTractor = _view.IdTractor;
            Tractor.Patente = _view.Patente;
            Tractor.Anio = _view.Anio;
            Tractor.IdMarca = _view.IdMarca;
            Tractor.IdModelo = _view.IdModelo;
            Tractor.Tara = _view.Tara;
            Tractor.Hp = _view.Hp;
            Tractor.Combustible = _view.Combustible;
            Tractor.Cmt = _view.Cmt;
            Tractor.FechaAlta = _view.FechaAlta;
            Tractor.IdEmpresa = _view.IdEmpresa; // 🔹 Se usa para buscar EmpresaSatelital
            

            // 🔹 Obtener IdEmpresaSatelital si existe
            int idSatelital = _view.SatelitalSeleccionado == "Megatrans" ? 2 : 1;
            Tractor.IdEmpresaSatelital = await _empresaSatelitalRepositorio.ObtenerIdEmpresaSatelitalAsync(Tractor.IdEmpresa, idSatelital);

            await EjecutarConCargaAsync(async () =>
            {
                await _tractorRepositorio.ActualizarTractorAsync(Tractor);
                _view.MostrarMensaje("Datos del tractor actualizados correctamente.");
            });
        }
    }
}