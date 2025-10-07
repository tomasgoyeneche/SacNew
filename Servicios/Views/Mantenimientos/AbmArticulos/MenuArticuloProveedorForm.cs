using DevExpress.XtraEditors;
using Servicios.Presenters;
using Servicios.Presenters.Mantenimiento;
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

namespace Servicios.Views.Mantenimiento
{
    public partial class MenuArticuloProveedorForm : DevExpress.XtraEditors.XtraForm, IMenuArticuloProveedorView
    {
        public readonly MenuArticuloProveedorPresenter _presenter;

        public MenuArticuloProveedorForm(MenuArticuloProveedorPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarProveedores(List<ArticuloProveedor> proveedores)
        {
            gridControlProv.DataSource = proveedores;
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
            ArticuloProveedor row = gridViewProv.GetFocusedRow() as ArticuloProveedor;
            if (row != null)
            {
                await _presenter.EliminarArticulosAsync(row.IdProveedor);
            }
            else
            {
                MostrarMensaje("Seleccione una Articulo para eliminar.");
            }
        }

        private async void btnAgregarNovedad_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarArticulosAsync();
        }

        private async void btnEditarNovedad_Click(object sender, EventArgs e)
        {
            ArticuloProveedor row = gridViewProv.GetFocusedRow() as ArticuloProveedor;
            if (row != null)
            {
                await _presenter.EditarArticulosAsync(row.IdProveedor);
            }
            else
            {
                MostrarMensaje("Seleccione una Articulo para editar.");
            }
        }
    }
}