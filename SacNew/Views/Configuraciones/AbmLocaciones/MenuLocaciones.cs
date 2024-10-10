using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using SacNew.Services;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public partial class MenuLocaciones : Form, IMenuLocacionesView
    {
        private readonly MenuLocacionesPresenter _presenter;
        private readonly ISesionService _sesionService;

        public MenuLocaciones(MenuLocacionesPresenter presenter, ISesionService sesionService)
        {
            InitializeComponent();
            _presenter = presenter;
            _sesionService = sesionService;
            _presenter.SetView(this);
        }

        private async void MenuLocaciones_Load(object sender, EventArgs e)
        {
            lMenuPostasNombre.Text = _sesionService.NombreCompleto;
            try
            {
                await _presenter.InicializarAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos iniciales: {ex.Message}");
            }
        }

        public string CriterioBusqueda => txtBuscar.Text.Trim();

        public void CargarLocaciones(List<Locacion> locaciones)
        {
            dataGridViewLocaciones.DataSource = locaciones;

            // Configurar las columnas
            ConfigurarColumnas();

            // Llenar las nuevas columnas con "Sí" o "No"
            foreach (DataGridViewRow row in dataGridViewLocaciones.Rows)
            {
                var locacion = (Locacion)row.DataBoundItem;
                row.Cells["CargaTexto"].Value = locacion.Carga ? "Sí" : "No";
                row.Cells["DescargaTexto"].Value = locacion.Descarga ? "Sí" : "No";
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridViewLocaciones.Columns["IdLocacion"].Visible = false;
            dataGridViewLocaciones.Columns["Activo"].Visible = false;
            dataGridViewLocaciones.Columns["Carga"].Visible = false;
            dataGridViewLocaciones.Columns["Descarga"].Visible = false;
            dataGridViewLocaciones.Columns["Nombre"].HeaderText = "Nombre";
            dataGridViewLocaciones.Columns["Direccion"].HeaderText = "Dirección";

            if (!dataGridViewLocaciones.Columns.Contains("CargaTexto"))
            {
                dataGridViewLocaciones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "CargaTexto",
                    HeaderText = "Carga",
                    ReadOnly = true
                });
            }

            if (!dataGridViewLocaciones.Columns.Contains("DescargaTexto"))
            {
                dataGridViewLocaciones.Columns.Add(new DataGridViewTextBoxColumn
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewLocaciones.SelectedRows.Count > 0)
            {
                int idLocacion = Convert.ToInt32(dataGridViewLocaciones.SelectedRows[0].Cells["IdLocacion"].Value);
              
                _presenter.EditarLocacion(idLocacion);
                
            }
            else
            {
                MostrarMensaje("Seleccione una locación para editar.");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            _presenter.AgregarLocacion();
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarLocacionesAsync(CriterioBusqueda);
        }
    }
}