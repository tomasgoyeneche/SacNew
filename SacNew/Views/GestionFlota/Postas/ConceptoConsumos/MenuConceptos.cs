using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using System.Data;

namespace SacNew.Views.GestionFlota.Postas.ConceptoConsumos
{
    public partial class MenuConceptos : Form, IMenuConceptosView
    {
        private readonly MenuConceptosPresenter _presenter;

        public MenuConceptos(MenuConceptosPresenter presenter)
        {
            InitializeComponent();

            // Inicializamos el Presenter y cargamos los conceptos
            _presenter = presenter;
            _presenter.SetView(this);
        }

        // Implementación de la interfaz para mostrar los conceptos
        public void MostrarConceptos(List<Concepto> conceptos)
        {
            // Se configura el DataGridView para mostrar solo las columnas deseadas
            dataGridViewConceptos.DataSource = conceptos.Select(c => new
            {
                c.IdConsumo,
                c.Codigo,
                c.Descripcion,
                TipoConsumo = _presenter.ObtenerDescripcionTipoConsumo(c.IdConsumoTipo)  // Obtener la descripción del tipo de consumo
            }).ToList();

            // Configurar para que el ID de consumo no se muestre
            dataGridViewConceptos.Columns["IdConsumo"].Visible = false;
        }

        public string TextoBusqueda => txtBuscar.Text.Trim();

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        // Evento del botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _presenter.BuscarConceptos();
            txtBuscar.Text = "";
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            _presenter.BuscarConceptos();
        }

        // Evento del botón Agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            _presenter.AgregarConcepto();
        }

        // Evento del botón Editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewConceptos.SelectedRows.Count > 0)
            {
                int idConsumo = Convert.ToInt32(dataGridViewConceptos.SelectedRows[0].Cells["IdConsumo"].Value);
                var conceptoCompleto = _presenter.ObtenerConceptoPorId(idConsumo);
                _presenter.EditarConcepto(conceptoCompleto);
            }
            else
            {
                MostrarMensaje("Seleccione un concepto para editar.");
            }
        }

        // Evento del botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewConceptos.SelectedRows.Count > 0)
            {
                // Obtener el IdConsumo de la fila seleccionada
                int idConsumo = Convert.ToInt32(dataGridViewConceptos.SelectedRows[0].Cells["IdConsumo"].Value);

                // Ahora puedes usar el idConsumo para eliminar el concepto
                _presenter.EliminarConceptoPorId(idConsumo);
            }
            else
            {
                MostrarMensaje("Seleccione un concepto para eliminar.");
            }
        }

        private void MenuConceptos_Load(object sender, EventArgs e)
        {
            _presenter.CargarConceptos();
        }
    }
}