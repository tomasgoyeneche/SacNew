using SacNew.Presenters.AbmUsuarios;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public partial class MenuUsuariosForm : Form, IMenuUsuariosView
    {
        private readonly MenuUsuariosPresenter _presenter;

        public MenuUsuariosForm(MenuUsuariosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void MenuUsuariosForm_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }

        public void CargarUsuarios(List<Usuario> usuarios)
        {
            dataGridViewUsuarios.DataSource = usuarios;
            ConfigurarColumnas();
        }

        public string CriterioBusqueda => txtBuscar.Text.Trim();

        private void ConfigurarColumnas()
        {
            var columns = dataGridViewUsuarios.Columns;
            columns["IdUsuario"].Visible = false;
            columns["contrasena"].Visible = false;
            columns["activo"].Visible = false;
            columns["idPosta"].Visible = false;
            columns["NombreCompleto"].HeaderText = "Nombre Completo";
            columns["NombreUsuario"].HeaderText = "Usuario";
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                int idUsuario = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["idUsuario"].Value);
                await _presenter.EliminarUsuarioAsync(idUsuario);
            }
            else
            {
                MostrarMensaje("Seleccione un usuario para eliminar.");
            }
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarUsuariosAsync(CriterioBusqueda);
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                int idUsuario = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["idUsuario"].Value);
                await _presenter.EditarUsuario(idUsuario);
            }
            else
            {
                MostrarMensaje("Seleccione un usuario para editar.");
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarUsuario();
        }

        private async void btnPermisos_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                int IdUsuario = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["IdUsuario"].Value);
                await _presenter.AbrirPermiso(IdUsuario);
            }
            else
            {
                MostrarMensaje("Seleccione un usuario para editar.");
            }
        }
    }
}