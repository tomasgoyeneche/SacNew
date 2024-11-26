using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.Configuraciones.AbmLocaciones;
using SacNew.Views.GestionFlota.Postas.ABMPostas;

namespace SacNew.Presenters
{
    public class MenuAbmPostasPresenter : BasePresenter<IMenuAbmPostasView>
    {
        private readonly IPostaRepositorio _postaRepositorio;

        public MenuAbmPostasPresenter(
            IPostaRepositorio postaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _postaRepositorio = postaRepositorio;
        }

        public async Task CargarPostasAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();
                _view.MostrarPostas(postas);
            });
        }

        public async Task BuscarPostasAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var textoBusqueda = _view.TextoBusqueda;

                if (string.IsNullOrEmpty(textoBusqueda))
                {
                    await CargarPostasAsync(); // Si no hay texto de búsqueda, cargar todas las postas
                }
                else
                {
                    var postasFiltradas = await _postaRepositorio.BuscarPostasAsync(textoBusqueda);
                    _view.MostrarPostas(postasFiltradas);
                }
            });
        }

        public async Task AgregarPostaAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarPosta>(async form =>
                {
                    await form.CargarDatosAsync(null);
                });
                await CargarPostasAsync(); // Recargar las postas después de agregar una nueva
            });
        }

        public async Task EditarPostaAsync(Posta postaSeleccionada)
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarPosta>(async form =>
                {
                    await form.CargarDatosAsync(postaSeleccionada);
                });
                await CargarPostasAsync(); // Recargar las postas después de editar
            });
        }

        public async Task EliminarPostaAsync(Posta postaSeleccionada)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmResult = MessageBox.Show(
                    $"¿Estás seguro de que quieres eliminar esta posta?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    await _postaRepositorio.EliminarPostaAsync(postaSeleccionada.IdPosta);
                    _view.MostrarMensaje("Posta eliminada exitosamente.");
                    await CargarPostasAsync(); // Recargar las postas después de eliminar
                }
            });
        }
    }
}