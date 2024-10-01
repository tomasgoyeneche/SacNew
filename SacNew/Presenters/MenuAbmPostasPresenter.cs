using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacNew.Presenters
{
    public class MenuAbmPostasPresenter
    {
        private readonly IMenuAbmPostasView _view;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISesionService _sesionService;

        public MenuAbmPostasPresenter(IMenuAbmPostasView view, IPostaRepositorio postaRepositorio, IServiceProvider serviceProvider, ISesionService sesionService)
        {
            _view = view;
            _postaRepositorio = postaRepositorio;
            _serviceProvider = serviceProvider;
            _sesionService = sesionService;
        }

        public async Task CargarPostasAsync()
        {
            try
            {
                var postas = await Task.Run(() => _postaRepositorio.ObtenerTodasLasPostasAsync());
                _view.MostrarPostas(postas);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar las postas: {ex.Message}");
            }
        }

        public async Task BuscarPostasAsync()
        {
            try
            {
                var textoBusqueda = _view.TextoBusqueda;

                if (string.IsNullOrEmpty(textoBusqueda))
                {
                    await CargarPostasAsync(); // Si no hay texto de búsqueda, cargar todas las postas
                }
                else
                {
                    var postasFiltradas = await Task.Run(() => _postaRepositorio.BuscarPostasAsync(textoBusqueda));
                    _view.MostrarPostas(postasFiltradas);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al buscar las postas: {ex.Message}");
            }
        }

        public async Task AgregarPostaAsync()
        {
            try
            {
                var agregarEditarPosta = _serviceProvider.GetService<AgregarEditarPosta>();
                agregarEditarPosta.ShowDialog();
                await CargarPostasAsync(); // Recargar las postas después de agregar una nueva
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al agregar la posta: {ex.Message}");
            }
        }

        public async Task EditarPostaAsync(Posta postaSeleccionada)
        {
            try
            {
                var agregarEditarPosta = _serviceProvider.GetService<AgregarEditarPosta>();
                agregarEditarPosta.CargarDatos(postaSeleccionada);
                agregarEditarPosta.ShowDialog();
                await CargarPostasAsync(); // Recargar las postas después de editar
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al editar la posta: {ex.Message}");
            }
        }

        public async Task EliminarPostaAsync(Posta postaSeleccionada)
        {
            try
            {
                var confirmResult = MessageBox.Show($"¿Estás seguro de que quieres eliminar esta posta?",
                                                    "Confirmar Eliminación",
                                                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    await Task.Run(() => _postaRepositorio.EliminarPostaAsync(postaSeleccionada.Id));
                    _view.MostrarMensaje("Posta eliminada exitosamente.");
                    await CargarPostasAsync();  // Recargar las postas después de eliminar
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al eliminar la posta: {ex.Message}");
            }
        }
    }
}
