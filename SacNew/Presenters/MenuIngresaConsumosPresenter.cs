using SacNew.Interfaces;
using SacNew.Repositories;
using SacNew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Presenters
{
    public class MenuIngresaConsumosPresenter
    {
        private IMenuIngresaConsumosView _view;
        private readonly IRepositorioPOC _repositorioPOC;
        private readonly ISesionService _sesionService;

        public MenuIngresaConsumosPresenter(IRepositorioPOC repositorioPOC, ISesionService sesionService)
        {
            _repositorioPOC = repositorioPOC;
            _sesionService = sesionService;
        }

        public void SetView(IMenuIngresaConsumosView view)
        {
            _view = view;
        }

        public void Inicializar()
        {
            MostrarNombreUsuario();
            CargarPOC();  // Cargar las POCs al inicializar
        }

        public void MostrarNombreUsuario()
        {
            string nombreUsuario = _sesionService.NombreCompleto;
            _view.MostrarNombreUsuario(nombreUsuario);
        }

        public void CargarPOC()
        {
            var listaPOC = _repositorioPOC.ObtenerTodos();
            _view.MostrarPOC(listaPOC);
        }

        public void BuscarPOC(string criterio)
        {
            var listaFiltrada = _repositorioPOC.BuscarPOC(criterio);
            _view.MostrarPOC(listaFiltrada);
        }

        public void EliminarPOC(int id)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta POC?");
            if (confirmacion == DialogResult.Yes)
            {
                _repositorioPOC.EliminarPOC(id);
                _view.MostrarMensaje("POC eliminada correctamente.");
                CargarPOC();  // Recargar la lista después de eliminar
            }
        }
    }
}
