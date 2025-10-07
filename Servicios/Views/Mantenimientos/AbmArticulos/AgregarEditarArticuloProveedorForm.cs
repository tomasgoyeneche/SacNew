using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
    public partial class AgregarEditarArticuloProveedorForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarArticuloProveedorView
    {
        public readonly AgregarEditarArticuloProveedorPresenter _presenter;

        public AgregarEditarArticuloProveedorForm(AgregarEditarArticuloProveedorPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int? IdArticuloProveedor { get; private set; }

        public string RazonSocial => txtRazonSocial.Text.Trim();
        public string CUIT => txtCUIT.Text.Trim();
        public string? Direccion => txtDireccion.Text.Trim();
        public string? Telefono => txtTelefono.Text.Trim();

        public string? Email => txtEmail.Text.Trim();


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        public void Cerrar()
        {
            Dispose();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        public void MostrarDatosProveedor(ArticuloProveedor proveedor)
        {
            IdArticuloProveedor = proveedor.IdProveedor;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtCUIT.Text = proveedor.CUIT;
            txtDireccion.Text = proveedor.Direccion;
            txtTelefono.Text = proveedor.Telefono;
            txtEmail.Text = proveedor.Email;

        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}