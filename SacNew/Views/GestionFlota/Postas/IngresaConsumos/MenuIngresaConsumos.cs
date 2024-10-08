using SacNew.Interfaces;
using SacNew.Models.DTOs;
using SacNew.Presenters;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class MenuIngresaConsumos : Form, IMenuIngresaConsumosView
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
            EjecutarEnHiloUI(() =>
            {
                dataGridViewPOC.DataSource = listaPOC;
                dataGridViewPOC.Columns["IdPoc"].Visible = false;
            });// Ocultar el ID
        }

        public void MostrarMensaje(string mensaje)
        {
            EjecutarEnHiloUI(() =>
            {
                MessageBox.Show(mensaje);
            });
        }

        public void MostrarNombreUsuario(string nombre)
        {
            EjecutarEnHiloUI(() =>
            {
                lNombreUsuario.Text = nombre;
            });
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        // Eventos
        private async void MenuIngresaConsumos_Load(object sender, EventArgs e)
        {
            await ManejarErroresAsync(async () =>
            {
                await _presenter.InicializarAsync().ConfigureAwait(false);  // Cargar datos iniciales de forma asíncrona
            });
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await ManejarErroresAsync(async () =>
            {
                await _presenter.BuscarPOCAsync(CriterioBusqueda).ConfigureAwait(false);  // Pasar el criterio de búsqueda al presenter
            });
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            await ManejarErroresAsync(async () =>
            {
                await _presenter.BuscarPOCAsync(CriterioBusqueda).ConfigureAwait(false);  // Búsqueda dinámica al escribir
            });
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPOC.SelectedRows.Count > 0)
            {
                int idPoc = Convert.ToInt32(dataGridViewPOC.SelectedRows[0].Cells["IdPoc"].Value);
                await ManejarErroresAsync(async () =>
                {
                    await _presenter.EliminarPOCAsync(idPoc).ConfigureAwait(false);  // Llamar al presenter para eliminar
                });
            }
            else
            {
                MostrarMensaje("Seleccione una POC para eliminar.");
            }
        }

        private async void btnAgregarPOC_Click(object sender, EventArgs e)
        {
            await ManejarErroresAsync(async () =>
            {
                await _presenter.AgregarPOCAsync().ConfigureAwait(false);
            });
        }

        private async void btnEditarPOC_Click(object sender, EventArgs e)
        {
            if (dataGridViewPOC.SelectedRows.Count > 0)
            {
                int idPoc = Convert.ToInt32(dataGridViewPOC.SelectedRows[0].Cells["IdPoc"].Value);
                await ManejarErroresAsync(async () =>
                {
                    await _presenter.EditarPOCAsync(idPoc).ConfigureAwait(false);  // Editar usando el idPoc
                });
            }
            else
            {
                MostrarMensaje("Seleccione una POC para editar.");
            }
        }


        private async Task ManejarErroresAsync(Func<Task> accion)
        {
            try
            {
                await accion();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Ocurrió un error: {ex.Message}");
            }
        }

        private void EjecutarEnHiloUI(Action accion)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(accion));
            }
            else
            {
                accion();
            }
        }
    }
}