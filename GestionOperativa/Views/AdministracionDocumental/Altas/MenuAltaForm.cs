using GestionOperativa.Presenters;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class MenuAltaForm : Form, IMenuAltasView
    {
        public readonly MenuAbmEntidadPresenter _presenter;

        public MenuAltaForm(MenuAbmEntidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async Task CargarEntidades(string entidad)
        {
            _presenter.SetEntidad(entidad);

            try
            {
                await _presenter.CargarEntidadesAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar {ex.Message}");
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public string TextoBusqueda => txtBuscar.Text.Trim();

        public void MostrarEntidades<T>(List<T> entidades)
        {
            dataGridViewEntidades.DataSource = entidades;
            _presenter.ConfigurarColumnas(dataGridViewEntidades);
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            //CODIGO PARA PASAR PARAMETRO DE FECHA, IMPORTANTE

            //string desdeFechaString = Microsoft.VisualBasic.Interaction.InputBox("Por favor ingrese la fecha de inicio (formato: dd/mm/yyyy):", "Ingrese Fecha Desde", "");

            //if (string.IsNullOrEmpty(desdeFechaString) || !DateTime.TryParse(desdeFechaString, out DateTime desdeFecha))
            //{
            //    MessageBox.Show("Por favor ingrese una fecha válida para 'Desde Fecha'.");
            //    return;
            //}

            try
            {
                await _presenter.BuscarEntidadesAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar {ex.Message}");
            }
        }

        private async void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await _presenter.BuscarEntidadesAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar {ex.Message}");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewEntidades.SelectedRows.Count > 0)
            {
                var entidadSeleccionada = dataGridViewEntidades.SelectedRows[0].DataBoundItem;
                await _presenter.EliminarEntidadAsync(entidadSeleccionada);
            }
            else
            {
                MostrarMensaje("Debe seleccionar un registro para eliminar.");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewEntidades.SelectedRows.Count > 0)
            {
                var entidadSeleccionada = dataGridViewEntidades.SelectedRows[0].DataBoundItem;
                await _presenter.EditarEntidadAsync(entidadSeleccionada);
            }
            else
            {
                MostrarMensaje("Debe seleccionar un registro para editar.");
            }
        }
    }
}