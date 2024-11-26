using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;

namespace SacNew.Presenters
{
    public class MenuConceptosPresenter : BasePresenter<IMenuConceptosView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConceptoTipoRepositorio _conceptoTipoRepositorio;
        private readonly IServiceProvider _serviceProvider;

        public MenuConceptosPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConceptoTipoRepositorio conceptoTipoRepositorio,
            IServiceProvider serviceProvider,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _conceptoTipoRepositorio = conceptoTipoRepositorio;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> ObtenerDescripcionTipoConsumoAsync(int idTipoConsumo)
        {
            return await _conceptoTipoRepositorio.ObtenerDescripcionPorIdAsync(idTipoConsumo);
        }

        public async Task CargarConceptosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var conceptos = await _conceptoRepositorio.ObtenerTodosLosConceptosAsync();
                await _view.MostrarConceptosAsync(conceptos);
            });
        }

        public async Task BuscarConceptosAsync()
        {
            var textoBusqueda = _view.TextoBusqueda;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                await CargarConceptosAsync(); // Si no hay texto de búsqueda, cargar todos los conceptos
            }
            else
            {
                await EjecutarConCargaAsync(async () =>
                {
                    var conceptosFiltrados = await _conceptoRepositorio.BuscarConceptosAsync(textoBusqueda);
                    await _view.MostrarConceptosAsync(conceptosFiltrados);
                });
            }
        }

        public async Task<Concepto> ObtenerConceptoPorIdAsync(int idConsumo)
        {
            return await _conceptoRepositorio.ObtenerPorIdAsync(idConsumo);
        }

        public async Task AgregarConceptoAsync()
        {
            await AbrirFormularioAsync<AgregarEditarConcepto>(async form =>
            {
                await form._presenter.InicializarAsync(default);
            });
            await CargarConceptosAsync();
        }

        public async Task EditarConceptoAsync(Concepto conceptoSeleccionado)
        {
            await AbrirFormularioAsync<AgregarEditarConcepto>(async form =>
            {
                await form._presenter.InicializarAsync(conceptoSeleccionado);
            });
            await CargarConceptosAsync();
        }

        public async Task EliminarConceptoPorIdAsync(int idConsumo)
        {
            var confirmResult = MessageBox.Show(
                "¿Estás seguro de que quieres eliminar este concepto?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                await EjecutarConCargaAsync(async () =>
                {
                    await _conceptoRepositorio.EliminarConceptoAsync(idConsumo);
                    _view.MostrarMensaje("Concepto marcado como inactivo.");
                }, CargarConceptosAsync);
            }
        }
    }
}