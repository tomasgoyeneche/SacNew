using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas
{
    public class AgregarEntidadPresenter : BasePresenter<IAltaEntidadView>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;

        private string _entidad;

        public AgregarEntidadPresenter(
            IEmpresaRepositorio empresaRepositorio,
            IChoferRepositorio choferRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _empresaRepositorio = empresaRepositorio;
            _choferRepositorio = choferRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
        }

        public void SetEntidad(string entidad)
        {
            _entidad = entidad.ToLower();
            _view.ConfigurarCampos(_entidad);
        }

        public async Task GuardarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                switch (_entidad)
                {
                    case "empresa":
                        if(_view.Campo1 == null || _view.Campo2 == null)
                        {
                            _view.MostrarMensaje("Los campos Nombre Fantasía y CUIT son obligatorios.");
                            break;
                        }
                        var empresa = new Empresa
                        {
                            RazonSocial = _view.Campo1,
                            Cuit = _view.Campo2,
                            IdEmpresaTipo = 1, // Asignar un tipo de empresa por defecto
                            IdLocalidad = 1,
                            Domicilio = "Indefinido",
                        };
                        await _empresaRepositorio.AgregarEmpresaAsync(empresa);
                        break;

                    case "chofer":
                        if (_view.Campo1 == null || _view.Campo2 == null || _view.Campo3 == null)
                        {
                            _view.MostrarMensaje("Los campos Nombre, Apellido y Documento son obligatorios.");
                            break;
                        }
                        var chofer = new Chofer
                        {
                            Nombre = _view.Campo1,
                            Apellido = _view.Campo2,
                            Documento = _view.Campo3
                        };
                        await _choferRepositorio.AltaChoferAsync(chofer.Nombre, chofer.Apellido, chofer.Documento, _sesionService.IdUsuario);
                        break;

                    case "tractor":
                        if (_view.Campo1 == null)
                        {
                            _view.MostrarMensaje("La patente es obligatoria.");
                            break;
                        }
                        var tractor = new Shared.Models.Tractor
                        {
                            Patente = _view.Campo1.ToUpper(),
                            Activo = true
                        };
                        await _tractorRepositorio.AltaTractorAsync(tractor.Patente, _sesionService.IdUsuario);
                        break;

                    case "semi":
                        if (_view.Campo1 == null)
                        {
                            _view.MostrarMensaje("La patente es obligatoria.");
                            break;
                        }
                        var semi = new Semi
                        {
                            Patente = _view.Campo1.ToUpper(),
                            Activo = true
                        };
                        await _semiRepositorio.AltaSemiAsync(semi.Patente, _sesionService.IdUsuario);
                        break;

                    default:
                        throw new InvalidOperationException("Entidad no reconocida.");
                }

                _view.MostrarMensaje("Entidad agregada correctamente.");
                _view.Cerrar();
            });
        }
    }
}