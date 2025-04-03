using GestionFlota.Presenters;
using SacNew.Interfaces;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class MenuIngresaConsumos : Form,  IMenuIngresaConsumosView
    {
        private readonly MenuIngresaConsumosPresenter _presenter;

        public MenuIngresaConsumos(MenuIngresaConsumosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string CriterioBusqueda => txtBuscar.Text.Trim();

        // Implementación de IMenuIngresaConsumosView
        public void MostrarPOC(List<POCDto> listaPOC)
        {
            dataGridViewPOC.DataSource = listaPOC;
            dataGridViewPOC.Columns["IdPoc"].Visible = false;
            dataGridViewPOC.Columns["IdPosta"].Visible = false;
            dataGridViewPOC.Columns["CapacidadTanque"].Visible = false;

            dataGridViewPOC.Columns["Estado"].Visible = false;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        // Eventos
        private async void MenuIngresaConsumos_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();  // Cargar datos iniciales de forma asíncrona
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarPOCAsync(CriterioBusqueda); // Pasar el criterio de búsqueda al presenter
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarPOCAsync(CriterioBusqueda);  // Búsqueda dinámica al escribir
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPOC.SelectedRows.Count > 0)
            {
                int idPoc = Convert.ToInt32(dataGridViewPOC.SelectedRows[0].Cells["IdPoc"].Value);

                await _presenter.EliminarPOCAsync(idPoc); // Llamar al presenter para eliminar
            }
            else
            {
                MostrarMensaje("Seleccione una POC para eliminar.");
            }
        }

        private async void btnAgregarPOC_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarPOCAsync();
        }

        private async void btnEditarPOC_Click(object sender, EventArgs e)
        {
            if (dataGridViewPOC.SelectedRows.Count > 0)
            {
                int idPoc = Convert.ToInt32(dataGridViewPOC.SelectedRows[0].Cells["IdPoc"].Value);

                await _presenter.EditarPOCAsync(idPoc);  // Editar usando el idPoc
            }
            else
            {
                MostrarMensaje("Seleccione una POC para editar.");
            }
        }

        private async void dataGridViewPOC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idPoc = Convert.ToInt32(dataGridViewPOC.Rows[e.RowIndex].Cells["IdPoc"].Value);

                await _presenter.AbrirMenuIngresaGasoilOtrosAsync(idPoc);
                _presenter.InicializarAsync();
            }
        }
    }
}