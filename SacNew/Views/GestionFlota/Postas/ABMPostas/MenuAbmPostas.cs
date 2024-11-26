using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Views.GestionFlota.Postas.ABMPostas
{
    public partial class MenuAbmPostas : Form, IMenuAbmPostasView
    {
        private readonly MenuAbmPostasPresenter _presenter;

        public MenuAbmPostas(MenuAbmPostasPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
            CargarPostas();
        }

        private async void CargarPostas()
        {
            try
            {
                await _presenter.CargarPostasAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar las postas: {ex.Message}");
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public string TextoBusqueda => txtBuscar.Text.Trim();

        public void MostrarPostas(List<Posta> postas)
        {
            dataGridViewPostas.DataSource = postas;

            // Mostrar solo las columnas específicas
            dataGridViewPostas.Columns["IdPosta"].Visible = false;
            dataGridViewPostas.Columns["idProvincia"].Visible = false;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                await _presenter.BuscarPostasAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar las postas: {ex.Message}");
            }
        }

        private async void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await _presenter.BuscarPostasAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar las postas: {ex.Message}");
            }
        }

        private void loginCloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                await _presenter.AgregarPostaAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al agregar la posta: {ex.Message}");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPostas.SelectedRows.Count > 0)
            {
                var postaSeleccionada = (Posta)dataGridViewPostas.SelectedRows[0].DataBoundItem;
                try
                {
                    await _presenter.EditarPostaAsync(postaSeleccionada);
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al editar la posta: {ex.Message}");
                }
            }
            else
            {
                MostrarMensaje("Seleccione una posta para editar.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPostas.SelectedRows.Count > 0)
            {
                var postaSeleccionada = (Posta)dataGridViewPostas.SelectedRows[0].DataBoundItem;
                try
                {
                    await _presenter.EliminarPostaAsync(postaSeleccionada);
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar la posta: {ex.Message}");
                }
            }
            else
            {
                MostrarMensaje("Seleccione una posta para eliminar.");
            }
        }
    }
}