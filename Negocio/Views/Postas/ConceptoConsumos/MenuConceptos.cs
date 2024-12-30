using GestionFlota.Presenters;
using Shared.Models;
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

            // Inicializamos la carga asíncrona
            _ = InicializarAsync();
        }

        // Implementación de la interfaz para mostrar los conceptos
        public async Task MostrarConceptosAsync(List<Concepto> conceptos)
        {
            // Configuramos el DataGridView para mostrar solo las columnas deseadas
            var conceptosAMostrar = await Task.WhenAll(conceptos.Select(async c => new
            {
                c.IdConsumo,
                c.Codigo,
                c.Descripcion,
                TipoConsumo = await _presenter.ObtenerDescripcionTipoConsumoAsync(c.IdConsumoTipo) // Obtener la descripción del tipo de consumo de forma asíncrona
            }));

            dataGridViewConceptos.DataSource = conceptosAMostrar.ToList();

            // Configurar para que el ID de consumo no se muestre
            dataGridViewConceptos.Columns["IdConsumo"].Visible = false;
        }

        public string TextoBusqueda => txtBuscar.Text.Trim();

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        // Evento del botón Buscar
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarConceptosAsync();
            txtBuscar.Text = "";
        }

        private async void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarConceptosAsync();
        }

        // Evento del botón Agregar
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarConceptoAsync();
        }

        // Evento del botón Editar
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewConceptos.SelectedRows.Count > 0)
            {
                int idConsumo = Convert.ToInt32(dataGridViewConceptos.SelectedRows[0].Cells["IdConsumo"].Value);
                var conceptoCompleto = await _presenter.ObtenerConceptoPorIdAsync(idConsumo);
                await _presenter.EditarConceptoAsync(conceptoCompleto);
            }
            else
            {
                MostrarMensaje("Seleccione un concepto para editar.");
            }
        }

        // Evento del botón Eliminar
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewConceptos.SelectedRows.Count > 0)
            {
                // Obtener el IdConsumo de la fila seleccionada
                int idConsumo = Convert.ToInt32(dataGridViewConceptos.SelectedRows[0].Cells["IdConsumo"].Value);

                // Ahora puedes usar el idConsumo para eliminar el concepto
                await _presenter.EliminarConceptoPorIdAsync(idConsumo);
            }
            else
            {
                MostrarMensaje("Seleccione un concepto para eliminar.");
            }
        }

        private async void MenuConceptos_Load(object sender, EventArgs e)
        {
            await _presenter.CargarConceptosAsync();
        }

        private async Task InicializarAsync()
        {
            await _presenter.CargarConceptosAsync();
        }
    }
}