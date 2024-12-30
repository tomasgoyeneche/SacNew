using SacNew.Presenters;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public partial class MenuLocaciones : Form, IMenuLocacionesView
    {
        private readonly MenuLocacionesPresenter _presenter;

        public MenuLocaciones(MenuLocacionesPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void MenuLocaciones_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }

        public string CriterioBusqueda => txtBuscar.Text.Trim();

        public void CargarLocaciones(List<Locacion> locaciones)
        {
            dataGridViewLocaciones.DataSource = locaciones;
            ConfigurarColumnas();

            foreach (DataGridViewRow row in dataGridViewLocaciones.Rows)
            {
                var locacion = (Locacion)row.DataBoundItem;
                row.Cells["CargaTexto"].Value = locacion.Carga ? "Sí" : "No";
                row.Cells["DescargaTexto"].Value = locacion.Descarga ? "Sí" : "No";
            }
        }

        private void ConfigurarColumnas()
        {
            var columns = dataGridViewLocaciones.Columns;
            columns["IdLocacion"].Visible = false;
            columns["Activo"].Visible = false;
            columns["Carga"].Visible = false;
            columns["Descarga"].Visible = false;
            columns["Nombre"].HeaderText = "Nombre";
            columns["Direccion"].HeaderText = "Dirección";

            if (!columns.Contains("CargaTexto"))
            {
                columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "CargaTexto",
                    HeaderText = "Carga",
                    ReadOnly = true
                });
            }

            if (!columns.Contains("DescargaTexto"))
            {
                columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DescargaTexto",
                    HeaderText = "Descarga",
                    ReadOnly = true
                });
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarLocacionesAsync(CriterioBusqueda);
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewLocaciones.SelectedRows.Count > 0)
            {
                int idLocacion = Convert.ToInt32(dataGridViewLocaciones.SelectedRows[0].Cells["IdLocacion"].Value);
                await _presenter.EliminarLocacionAsync(idLocacion);
            }
            else
            {
                MostrarMensaje("Seleccione una locación para eliminar.");
            }
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private async void btnPool_Click(object sender, EventArgs e)
        {
            if (dataGridViewLocaciones.SelectedRows.Count > 0)
            {
                int idLocacion = Convert.ToInt32(dataGridViewLocaciones.SelectedRows[0].Cells["IdLocacion"].Value);
                string? carga = dataGridViewLocaciones.SelectedRows[0].Cells["CargaTexto"].Value.ToString();
                if (carga == "Sí")
                {
                    await _presenter.AbrirLocacionPool(idLocacion);
                }
                else
                {
                    MostrarMensaje("La locación no es de carga.");
                }
            }
            else
            {
                MostrarMensaje("Seleccione una locación para editar.");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewLocaciones.SelectedRows.Count > 0)
            {
                int idLocacion = Convert.ToInt32(dataGridViewLocaciones.SelectedRows[0].Cells["IdLocacion"].Value);
                await _presenter.EditarLocacion(idLocacion);
            }
            else
            {
                MostrarMensaje("Seleccione una locación para editar.");
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarLocacion();
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarLocacionesAsync(CriterioBusqueda);
        }

        public void MostrarNombreUsuario(string nombreUsuario)
        {
            lMenuPostasNombre.Text = nombreUsuario;
        }
    }
}