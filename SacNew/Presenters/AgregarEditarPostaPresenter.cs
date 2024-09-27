using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _view.ProvinciaId = posta.ProvinciaId;
            _view.Id = posta.Id;
        }

        public void GuardarPosta()
        {
            if (_postaActual == null)
            {
                // Crear una nueva posta
                var nuevaPosta = new Posta
                {
                    Codigo = _view.Codigo,
                    Descripcion = _view.Descripcion,
                    Direccion = _view.Direccion,
                    ProvinciaId = _view.ProvinciaId
                };
                _postaRepositorio.AgregarPosta(nuevaPosta);
                _view.MostrarMensaje("Posta agregada exitosamente.");
            }
            else
            {
                // Actualizar la posta existente
                var postaExistente = new Posta
                {
                    Id = _view.Id,  // Mantener el Id de la Posta existente
                    Codigo = _view.Codigo,
                    Descripcion = _view.Descripcion,
                    Direccion = _view.Direccion,
                    ProvinciaId = _view.ProvinciaId
                };
                _postaRepositorio.ActualizarPosta(postaExistente);
                _view.MostrarMensaje("Posta actualizada exitosamente.");
            }
        }
    }
}
