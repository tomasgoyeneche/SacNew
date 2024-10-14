using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;

namespace SacNew.Presenters
{
    public class AgregarEditarPostaPresenter
    {
        private readonly IAgregarEditarPostaView _view;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IProvinciaRepositorio _provinciaRepositorio;
        private Posta _postaActual;

        public AgregarEditarPostaPresenter(IAgregarEditarPostaView view, IPostaRepositorio postaRepositorio, IProvinciaRepositorio provinciaRepositorio)
        {
            _view = view;
            _postaRepositorio = postaRepositorio;
            _provinciaRepositorio = provinciaRepositorio;

            CargarProvincias();
        }

        public void CargarProvincias()
        {
            var provincias = _provinciaRepositorio.ObtenerProvincias();
            _view.CargarProvincias(provincias);
        }

        public void CargarDatosPosta(Posta posta)
        {
            _postaActual = posta;
            _view.Codigo = posta.Codigo;
            _view.Descripcion = posta.Descripcion;
            _view.Direccion = posta.Direccion;
            _view.ProvinciaId = posta.IdProvincia;
            _view.Id = posta.IdPosta;
        }

        public void GuardarPosta()
        {
            if (!ValidarDatos())
            {
                return;
            }

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
                _postaRepositorio.AgregarPostaAsync(nuevaPosta);
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
                _postaRepositorio.ActualizarPostaAsync(postaExistente);
                _view.MostrarMensaje("Posta actualizada exitosamente.");
            }
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