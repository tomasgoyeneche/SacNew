using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Empresas;
using Shared.Models;

namespace GestionOperativa.Presenters.Empresas
{
    public class ModificarDatosEmpresaPresenter : BasePresenter<IModificarDatosEmpresaView>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IEmpresaTipoRepositorio _empresaTipoRepositorio;
        private readonly IProvinciaRepositorio _provinciaRepositorio;
        private readonly ILocalidadRepositorio _localidadRepositorio;

        public ModificarDatosEmpresaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaRepositorio empresaRepositorio,
            IEmpresaTipoRepositorio empresaTipoRepositorio,
            IProvinciaRepositorio provinciaRepositorio,
            ILocalidadRepositorio localidadRepositorio)
            : base(sesionService, navigationService)
        {
            _empresaRepositorio = empresaRepositorio;
            _empresaTipoRepositorio = empresaTipoRepositorio;
            _provinciaRepositorio = provinciaRepositorio;
            _localidadRepositorio = localidadRepositorio;
        }

        public async Task InicializarAsync(int idEmpresa)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var empresa = await _empresaRepositorio.ObtenerPorIdAsync(idEmpresa);
                var tipos = await _empresaTipoRepositorio.ObtenerTodosAsync();
                var provincias = await _provinciaRepositorio.ObtenerProvinciasAsync();

                if (empresa == null)
                {
                    _view.MostrarMensaje("No se encontró la empresa.");
                    return;
                }

                _view.CargarDatosEmpresa(empresa, tipos, provincias);
                await CargarLocalidades(empresa.IdLocalidad);
            });
        }

        public async Task CargarLocalidades(int idProvincia)
        {
            var localidades = await _localidadRepositorio.ObtenerPorProvinciaAsync(idProvincia);
            _view.CargarLocalidades(localidades);
        }

        public async Task GuardarCambios()
        {
            var empresa = new Empresa
            {
                IdEmpresa = _view.IdEmpresa,
                Cuit = _view.Cuit,
                IdEmpresaTipo = _view.IdEmpresaTipo,
                RazonSocial = _view.RazonSocial,
                NombreFantasia = _view.NombreFantasia,
                IdLocalidad = _view.IdLocalidad,
                Domicilio = _view.Domicilio,
                Telefono = _view.Telefono,
                Email = _view.Email,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                await _empresaRepositorio.ActualizarAsync(empresa);
                _view.MostrarMensaje("Datos de la empresa actualizados correctamente.");
            });
        }
    }
}