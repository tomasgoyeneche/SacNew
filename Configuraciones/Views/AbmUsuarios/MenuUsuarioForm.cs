using DevExpress.XtraEditors;
using SacNew.Presenters.AbmUsuarios;
using SacNew.Views.Configuraciones.AbmUsuarios;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configuraciones.Views.AbmUsuarios
{
    public partial class MenuUsuarioForm : DevExpress.XtraEditors.XtraForm, IMenuUsuariosView
    {
        private readonly MenuUsuariosPresenter _presenter;

        public MenuUsuarioForm(MenuUsuariosPresenter presenter)
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
            gridControlUsuarios.DataSource = usuarios;

            var view = gridViewUsuarios;

            view.BestFitColumns();
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (gridViewUsuarios.GetFocusedRow() is Usuario usuario)
            {
                await _presenter.EliminarUsuarioAsync(usuario.IdUsuario);
            }
            else
            {
                MostrarMensaje("Seleccione un usuario para eliminar.");
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridViewUsuarios.GetFocusedRow() is Usuario usuario)
            {
                await _presenter.EditarUsuario(usuario.IdUsuario);
            }
            else
            {
                MostrarMensaje("Seleccione un usuario para editar.");
            }
        }

        private async void btnPermisos_Click(object sender, EventArgs e)
        {
            if (gridViewUsuarios.GetFocusedRow() is Usuario usuario)
            {
                await _presenter.AbrirPermiso(usuario.IdUsuario);
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
    }
}
