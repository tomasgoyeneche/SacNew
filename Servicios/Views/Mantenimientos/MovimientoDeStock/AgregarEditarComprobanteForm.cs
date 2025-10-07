using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
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
    public partial class AgregarEditarComprobanteForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarComprobanteView
    {
        public readonly AgregarEditarComprobantePresenter _presenter;

        public AgregarEditarComprobanteForm(AgregarEditarComprobantePresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void btnCargarPdf_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = "Seleccionar comprobante PDF"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _presenter.CargarPdf(dialog.FileName);
            }
        }

        // 🔹 Props para exponer al presenter
        public int IdMovimientoStock { get; set; }

        public int IdTipoComprobante
        {
            get => Convert.ToInt32(cmbTipoComprobante.EditValue ?? 0);
            set => cmbTipoComprobante.EditValue = value;
        }

        public string NroComprobante
        {
            get => txtNroComprobante.Text.Trim();
            set => txtNroComprobante.Text = value;
        }

        public int IdProveedor
        {
            get => Convert.ToInt32(cmbProveedor.EditValue ?? 0);
            set => cmbProveedor.EditValue = value;
        }

        public bool Activo { get; set; } = true;

        public string RutaComprobante { get; set; } = string.Empty;

        // 🔹 Métodos para cargar combos
        public void CargarTiposComprobante(List<TipoComprobante> tipos)
        {
            cmbTipoComprobante.Properties.DataSource = tipos;
            cmbTipoComprobante.Properties.DisplayMember = "Nombre";
            cmbTipoComprobante.Properties.ValueMember = "IdTipoComprobante";

            cmbTipoComprobante.Properties.Columns.Clear();
            cmbTipoComprobante.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Tipo Comprobante"));
        }

        public void CargarProveedores(List<ArticuloProveedor> proveedores)
        {
            cmbProveedor.Properties.DataSource = proveedores;
            cmbProveedor.Properties.DisplayMember = "RazonSocial";
            cmbProveedor.Properties.ValueMember = "IdProveedor";

            cmbProveedor.Properties.Columns.Clear();
            cmbProveedor.Properties.Columns.Add(new LookUpColumnInfo("RazonSocial", "Proveedor"));
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            Dispose();
        }

        private async void bAgregarProveedor_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarProveedorAsync();
        }
    }
}