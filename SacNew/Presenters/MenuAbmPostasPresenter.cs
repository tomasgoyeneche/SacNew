using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Presenters
{
    public class MenuAbmPostasPresenter
    {
        private readonly IMenuAbmPostasView _view;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IServiceProvider _serviceProvider;

        public MenuAbmPostasPresenter(IMenuAbmPostasView view, IPostaRepositorio postaRepositorio, IServiceProvider serviceProvider)
        {
            _view = view;
            _postaRepositorio = postaRepositorio;
            _serviceProvider = serviceProvider;
        }

        public void CargarPostas()
        {
            var postas = _postaRepositorio.ObtenerTodasLasPostas();
            _view.MostrarPostas(postas);
        }

        public void BuscarPostas()
        {
            var textoBusqueda = _view.TextoBusqueda;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarPostas(); // Si no hay texto de búsqueda, cargar todas las postas
            }
            else
            {
                var postasFiltradas = _postaRepositorio.BuscarPostas(textoBusqueda);
                _view.MostrarPostas(postasFiltradas);
            }
        }

        public void AgregarPosta()
        {
            var agregarEditarPosta = _serviceProvider.GetService<AgregarEditarPosta>();
            agregarEditarPosta.ShowDialog();
            CargarPostas(); // Recargar las postas después de agregar una nueva
        }

        public void EditarPosta(Posta postaSeleccionada)
        {
            var agregarEditarPosta = _serviceProvider.GetService<AgregarEditarPosta>();
            agregarEditarPosta.CargarDatos(postaSeleccionada);
            agregarEditarPosta.ShowDialog();
            CargarPostas(); // Recargar las postas después de editar
        }

        public void EliminarPosta(Posta postaSeleccionada)
        {
            var confirmResult = MessageBox.Show("¿Estás seguro de que quieres eliminar esta posta?",
                                                "Confirmar Eliminación",
                                                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                _postaRepositorio.EliminarPosta(postaSeleccionada.Id);
                _view.MostrarMensaje("Posta eliminada exitosamente.");
                CargarPostas();  // Recargar las postas después de eliminar
            }
        }
    }
}
