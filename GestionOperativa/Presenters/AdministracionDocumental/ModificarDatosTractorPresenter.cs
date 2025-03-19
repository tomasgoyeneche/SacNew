using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Tractores;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Presenters.AdministracionDocumental
{
    public class ModificarDatosTractorPresenter : BasePresenter<IModificarDatosTractorView>
    {
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly IVehiculoMarcaRepositorio _marcaRepositorio;
        private readonly IVehiculoModeloRepositorio _modeloRepositorio;
        private readonly IEmpresaSatelitalRepositorio _empresaSatelitalRepositorio;

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
                var tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(idTractor);
                if (tractor == null)
                {
                    _view.MostrarMensaje("No se encontró el tractor.");
                    return;
                }

                var marcas = await _marcaRepositorio.ObtenerMarcasPorTipoAsync(1);
                var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(tractor.IdMarca);

                _view.CargarDatosTractor(tractor, marcas, modelos, SatelitalNombre);
            });
        }

        public async Task CargarModelos(int idMarca)
        {
            var modelos = await _modeloRepositorio.ObtenerModelosPorMarcaAsync(idMarca);
            _view.CargarModelos(modelos);
        }

        public async Task GuardarCambios()
        {
            var tractore = new Shared.Models.Tractor
            {
                IdTractor = _view.IdTractor,
                Patente = _view.Patente,
                Anio = _view.Anio,
                IdMarca = _view.IdMarca,
                IdModelo = _view.IdModelo,
                Tara = _view.Tara,
                Hp = _view.Hp,
                Combustible = _view.Combustible,
                Cmt = _view.Cmt,
                FechaAlta = _view.FechaAlta,
                IdEmpresa = _view.IdEmpresa // 🔹 Se usa para buscar EmpresaSatelital
            };

            // 🔹 Obtener IdEmpresaSatelital si existe
            int idSatelital = _view.SatelitalSeleccionado == "Megatrans" ? 2 : 1;
            tractore.IdEmpresaSatelital = await _empresaSatelitalRepositorio.ObtenerIdEmpresaSatelitalAsync(tractore.IdEmpresa, idSatelital);

            await EjecutarConCargaAsync(async () =>
            {
                await _tractorRepositorio.ActualizarTractorAsync(tractore);
                _view.MostrarMensaje("Datos del tractor actualizados correctamente.");
            });
        }
    }
}
