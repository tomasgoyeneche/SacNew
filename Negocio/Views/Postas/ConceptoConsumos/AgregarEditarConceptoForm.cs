using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
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

namespace GestionFlota.Views.Postas.ConceptoConsumos
{
    public partial class AgregarEditarConceptoForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarConceptoView
    {
        public readonly AgregarEditarConceptoPresenter _presenter;

        public AgregarEditarConceptoForm(AgregarEditarConceptoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // Inicialización asíncrona al cargar el formulario
        }

        public int Id { get; set; }

        public string Codigo => txtCodigo.Text.Trim();
        public string Descripcion => txtDescripcion.Text.Trim();
        public int IdTipoConsumo => Convert.ToInt32(cmbTipoConsumo.EditValue);
        public decimal PrecioActual => Convert.ToDecimal(txtPrecioActual.EditValue ?? 0);
        public decimal PrecioAnterior => Convert.ToDecimal(txtPrecioAnterior.EditValue ?? 0);
        public DateTime Vigencia => (DateTime)dtpVigencia.EditValue;

        public int IdProveedorBahiaBlanca => Convert.ToInt32(cmbProveedorBahiaBlanca.EditValue);
        public int IdProveedorPlazaHuincul => Convert.ToInt32(cmbProveedorPlazaHuincul.EditValue);

        public void CierrePost()
        {
            Dispose();
        }

        public async Task CargarTiposDeConsumoAsync(List<ConceptoTipo> tiposDeConsumo)
        {
            cmbTipoConsumo.Properties.DataSource = tiposDeConsumo;
            cmbTipoConsumo.Properties.DisplayMember = "Descripcion";
            cmbTipoConsumo.Properties.ValueMember = "IdConsumoTipo";
            cmbTipoConsumo.Properties.NullText = "Seleccione Tipo Consumo...";
            cmbTipoConsumo.Properties.Columns.Clear();
            cmbTipoConsumo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Descripcion"));
        }

        public async Task CargarProveedoresBahiaBlancaAsync(List<Proveedor> proveedores)
        {
            cmbProveedorBahiaBlanca.Properties.DataSource = proveedores;
            cmbProveedorBahiaBlanca.Properties.DisplayMember = "Codigo";
            cmbProveedorBahiaBlanca.Properties.ValueMember = "IdProveedor";
            cmbProveedorBahiaBlanca.Properties.NullText = "Seleccione Proveedor...";
            cmbProveedorBahiaBlanca.Properties.Columns.Clear();
            cmbProveedorBahiaBlanca.Properties.Columns.Add(new LookUpColumnInfo("Codigo", "Codigo"));
        }

        public async Task CargarProveedoresPlazaHuinculAsync(List<Proveedor> proveedores)
        {
            cmbProveedorPlazaHuincul.Properties.DataSource = proveedores;
            cmbProveedorPlazaHuincul.Properties.DisplayMember = "Codigo";
            cmbProveedorPlazaHuincul.Properties.ValueMember = "IdProveedor";
            cmbProveedorPlazaHuincul.Properties.NullText = "Seleccione Proveedor...";
            cmbProveedorPlazaHuincul.Properties.Columns.Clear();
            cmbProveedorPlazaHuincul.Properties.Columns.Add(new LookUpColumnInfo("Codigo", "Codigo"));
        }

        public void MostrarDatosConcepto(Concepto concepto)
        {
            Id = concepto.IdConsumo;
            txtCodigo.Text = concepto.Codigo;
            txtCodigo.Enabled = false;  // El código no se puede modificar al editar
            txtDescripcion.Text = concepto.Descripcion;
            cmbTipoConsumo.EditValue = concepto.IdConsumoTipo;
            txtPrecioActual.EditValue = concepto.PrecioActual;
            txtPrecioAnterior.EditValue = concepto.PrecioAnterior;
            dtpVigencia.EditValue = concepto.Vigencia;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConceptoAsync();
            CierrePost();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}