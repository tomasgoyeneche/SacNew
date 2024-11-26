using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ABMPostas;

namespace SacNew.Presenters
{
    public class AgregarEditarPostaPresenter : BasePresenter<IAgregarEditarPostaView>
    {
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IProvinciaRepositorio _provinciaRepositorio;
        private Posta? _postaActual;

        public AgregarEditarPostaPresenter(
            IPostaRepositorio postaRepositorio,
            IProvinciaRepositorio provinciaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _postaRepositorio = postaRepositorio;
            _provinciaRepositorio = provinciaRepositorio;
        }

        public async Task CargarProvinciasAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var provincias = await _provinciaRepositorio.ObtenerProvinciasAsync();
                _view.CargarProvincias(provincias);
            });
        }

        public void CargarDatosPosta(Posta posta)
        {
            if (posta != null)
            {
                _postaActual = posta;
                _view.Codigo = posta.Codigo;
                _view.Descripcion = posta.Descripcion;
                _view.Direccion = posta.Direccion;
                _view.ProvinciaId = posta.IdProvincia;
                _view.Id = posta.IdPosta;
            }
        }

        public async Task GuardarPostaAsync()
        {
            if (!ValidarDatos())
            {
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                if (_postaActual == null)
                {
                    // Crear una nueva posta
                    var nuevaPosta = new Posta
                    {
                        Codigo = _view.Codigo,
                        Descripcion = _view.Descripcion,
                        Direccion = _view.Direccion,
                        IdProvincia = _view.ProvinciaId
                    };
                    await _postaRepositorio.AgregarPostaAsync(nuevaPosta);
                    _view.MostrarMensaje("Posta agregada exitosamente.");
                }
                else
                {
                    // Actualizar la posta existente
                    var postaExistente = new Posta
                    {
                        IdPosta = _view.Id,  // Mantener el Id de la Posta existente
                        Codigo = _view.Codigo,
                        Descripcion = _view.Descripcion,
                        Direccion = _view.Direccion,
                        IdProvincia = _view.ProvinciaId
                    };
                    await _postaRepositorio.ActualizarPostaAsync(postaExistente);
                    _view.MostrarMensaje("Posta actualizada exitosamente.");
                }
            });
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(_view.Codigo))
            {
                _view.MostrarMensaje("El código no puede estar vacío.");
                return false;
            }
            if (_view.Codigo.Length > 2)
            {
                _view.MostrarMensaje("El código es de maximo 2 letras.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_view.Descripcion))
            {
                _view.MostrarMensaje("La descripción no puede estar vacía.");
                return false;
            }

            if (_view.ProvinciaId <= 0)
            {
                _view.MostrarMensaje("Debe seleccionar una provincia válida.");
                return false;
            }

            return true;
        }
    }
}