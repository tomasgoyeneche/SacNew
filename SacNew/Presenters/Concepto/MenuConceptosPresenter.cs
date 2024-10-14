using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;

namespace SacNew.Presenters
{
    public class MenuConceptosPresenter
    {
        private readonly IMenuConceptosView _view;
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConceptoTipoRepositorio _conceptoTipoRepositorio;
        private readonly IServiceProvider _serviceProvider;

        public MenuConceptosPresenter(IMenuConceptosView view, IConceptoRepositorio conceptoRepositorio, IConceptoTipoRepositorio conceptoTipoRepositorio, IServiceProvider serviceProvider)
        {
            _view = view;
            _conceptoRepositorio = conceptoRepositorio;
            _conceptoTipoRepositorio = conceptoTipoRepositorio;

            _serviceProvider = serviceProvider;
        }

        public string ObtenerDescripcionTipoConsumo(int idTipoConsumo)
        {
            return _conceptoTipoRepositorio.ObtenerDescripcionPorId(idTipoConsumo);
        }

        public void CargarConceptos()
        {
            var conceptos = _conceptoRepositorio.ObtenerTodosLosConceptos();
            _view.MostrarConceptos(conceptos);
        }

        public void BuscarConceptos()
        {
            var textoBusqueda = _view.TextoBusqueda;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarConceptos(); // Si no hay texto de búsqueda, cargar todos los conceptos
            }
            else
            {
                var conceptosFiltrados = _conceptoRepositorio.BuscarConceptos(textoBusqueda);
                _view.MostrarConceptos(conceptosFiltrados);
            }
        }

        public Concepto ObtenerConceptoPorId(int idConsumo)
        {
            return _conceptoRepositorio.ObtenerPorId(idConsumo);
        }

        public void AgregarConcepto()
        {
            var agregarEditarConcepto = _serviceProvider.GetService<AgregarEditarConcepto>();
            agregarEditarConcepto.CargarTiposDeConsumo(_conceptoTipoRepositorio.ObtenerTodosLosTipos());
            agregarEditarConcepto.ShowDialog();
            CargarConceptos(); // Recargar después de agregar uno nuevo
        }

        public void EditarConcepto(Concepto conceptoSeleccionado)
        {
            var agregarEditarConcepto = _serviceProvider.GetService<AgregarEditarConcepto>();
            agregarEditarConcepto.CargarTiposDeConsumo(_conceptoTipoRepositorio.ObtenerTodosLosTipos());
            agregarEditarConcepto._presenter.CargarDatosParaEditar(conceptoSeleccionado);// Cargar los datos en el formulario
            agregarEditarConcepto.ShowDialog();
            CargarConceptos(); // Recargar la lista después de la edición
        }

        public void EliminarConceptoPorId(int idConsumo)
        {
            var confirmResult = MessageBox.Show("¿Estás seguro de que quieres eliminar este concepto?",
                                        "Confirmar Eliminación",
                                        MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                _conceptoRepositorio.EliminarConcepto(idConsumo);
                _view.MostrarMensaje("Concepto marcado como inactivo.");
                CargarConceptos();  // Recargar los conceptos después de eliminar
            }
        }
    }
}