using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Tractores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas.Tractor
{
    public class CambiarConfiguracionPresenter : BasePresenter<ICambiarConfiguracionView>
    {
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private int _idEntidad;
        private string _tipoEntidad;

        private readonly List<string> _configTractor = new() { "DS", "SDS", "DDS", "SDS-MANUAL", "SDS-AUTOMATICO", "DDS-MANUAL", "DDS-AUTOMATICO" };
        private readonly List<string> _configSemi = new() { "DD", "SSS", "DD_D", "SSS-MANUAL", "SSS-AUTOMATICO", "DD_D-MANUAL", "DD_D-AUTOMATICO", "DDD" };

        public CambiarConfiguracionPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio
        ) : base(sesionService, navigationService)
        {
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
        }

        public async Task CargarDatosAsync(int idEntidad, string tipoEntidad)
        {
            _idEntidad = idEntidad;
            _tipoEntidad = tipoEntidad.ToLower();

            _view.ConfigurarVistaPorEntidad(_tipoEntidad);

            if (_tipoEntidad == "tractor")
            {
                var tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(_idEntidad);
                if (tractor == null)
                {
                    _view.MostrarMensaje("No se encontró el tractor.");
                    return;
                }
                _view.CargarOpcionesConfiguracion(_configTractor);
                _view.SeleccionarConfiguracionActual(tractor.Configuracion);
            }
            else if (_tipoEntidad == "semi")
            {
                var semi = await _semiRepositorio.ObtenerSemiPorIdAsync(_idEntidad);
                if (semi == null)
                {
                    _view.MostrarMensaje("No se encontró el semirremolque.");
                    return;
                }
                _view.CargarOpcionesConfiguracion(_configSemi);
                _view.SeleccionarConfiguracionActual(semi.Configuracion);
            }
        }

        public async Task GuardarConfiguracionAsync()
        {
            if (_tipoEntidad == "tractor")
            {
                var tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(_idEntidad);
                if (tractor == null)
                {
                    _view.MostrarMensaje("No se encontró el tractor para actualizar.");
                    return;
                }
                tractor.Configuracion = _view.ConfiguracionSeleccionada;
                await _tractorRepositorio.ActualizarTractorAsync(tractor);
                _view.MostrarMensaje("Configuración actualizada correctamente.");
            }
            else if (_tipoEntidad == "semi")
            {
                var semi = await _semiRepositorio.ObtenerSemiPorIdAsync(_idEntidad);
                if (semi == null)
                {
                    _view.MostrarMensaje("No se encontró el semirremolque para actualizar.");
                    return;
                }
                semi.Configuracion = _view.ConfiguracionSeleccionada;
                await _semiRepositorio.ActualizarSemiAsync(semi);
                _view.MostrarMensaje("Configuración actualizada correctamente.");
            }
        }
    }
}
