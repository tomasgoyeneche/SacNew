using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Choferes;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Presenters.Choferes
{
    public class ModificarDatosChoferPresenter : BasePresenter<IModificarDatosChoferView>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IProvinciaRepositorio _provinciaRepositorio;
        private readonly ILocalidadRepositorio _localidadRepositorio;
        private readonly IConfRepositorio _confRepositorio;

        public ModificarDatosChoferPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaRepositorio empresaRepositorio,
            IChoferRepositorio choferRepositorio,
            IProvinciaRepositorio provinciaRepositorio,
            IConfRepositorio confRepositorio,
            ILocalidadRepositorio localidadRepositorio)
            : base(sesionService, navigationService)
        {
            _empresaRepositorio = empresaRepositorio;
            _choferRepositorio = choferRepositorio;
            _confRepositorio = confRepositorio;
            _provinciaRepositorio = provinciaRepositorio;
            _localidadRepositorio = localidadRepositorio;
        }

        public async Task InicializarAsync(int idChofer)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var empresas = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
                var chofer = await _choferRepositorio.ObtenerPorIdAsync(idChofer);
                var provincias = await _provinciaRepositorio.ObtenerProvinciasAsync();
                int idProvincia = await _localidadRepositorio.ObtenerPorIdAsync(chofer.IdLocalidad);

                if (chofer == null)
                {
                    _view.MostrarMensaje("No se encontró la empresa.");
                    return;
                }

                await VerificarArchivosChoferAsync(chofer.Documento);
                _view.CargarDatosChofer(chofer, empresas, provincias, idProvincia);
            });
        }

        public async Task CargarLocalidades(int idProvincia)
        {
            var localidades = await _localidadRepositorio.ObtenerPorProvinciaAsync(idProvincia);
            _view.CargarLocalidades(localidades);
        }

        public async Task GuardarCambios()
        {
            var chofer = new Chofer
            {
                IdChofer = _view.IdChofer,
                Apellido = _view.Apellido,
                Nombre = _view.Nombre,
                Documento = _view.Documento,
                FechaNacimiento = (DateTime)_view.FechaNacimiento,
                IdLocalidad = _view.IdLocalidad,
                Domicilio = _view.Domicilio,
                Telefono = _view.Telefono,
                IdEmpresa = _view.idEmpresa,
                ZonaFria = _view.ZonaFria,
                FechaAlta = (DateTime)_view.FechaAlta,
                Celular = _view.Celular,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _choferRepositorio.ActualizarAsync(chofer);
                _view.MostrarMensaje("Datos del chofer actualizados correctamente.");
            });
        }


        private async Task VerificarArchivosChoferAsync(string dni)
        {
            var rutaFoto = await ObtenerRutaPorIdAsync(1, "", dni + ".jpg");
            _view.ConfigurarFotoChofer(rutaFoto);
        }

        private async Task<string?> ObtenerRutaPorIdAsync(int idConf, string subDirectorio, string archivo)
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(idConf);
            return conf == null || string.IsNullOrEmpty(conf.Ruta)
                ? null
                : Path.Combine(conf.Ruta, subDirectorio, archivo);
        }

    }
}