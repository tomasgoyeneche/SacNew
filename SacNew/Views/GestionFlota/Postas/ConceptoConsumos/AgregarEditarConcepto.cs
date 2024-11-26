using SacNew.Models;
using SacNew.Presenters;

namespace SacNew.Views.GestionFlota.Postas.ConceptoConsumos
{
    public partial class AgregarEditarConcepto : Form, IAgregarEditarConceptoView
    {
        public readonly AgregarEditarConceptoPresenter _presenter;

        public AgregarEditarConcepto(AgregarEditarConceptoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // Inicialización asíncrona al cargar el formulario
        }

        public int Id { get; set; }

        public string Codigo => txtCodigo.Text.Trim();
        public string Descripcion => txtDescripcion.Text.Trim();
        public int IdTipoConsumo => Convert.ToInt32(cmbTipoConsumo.SelectedValue);
        public decimal PrecioActual => Convert.ToDecimal(txtPrecioActual.Text);
        public decimal PrecioAnterior => Convert.ToDecimal(txtPrecioAnterior.Text);
        public DateTime Vigencia => dtpVigencia.Value;

        public int IdProveedorBahiaBlanca => Convert.ToInt32(cmbProveedorBahiaBlanca.SelectedValue);
        public int IdProveedorPlazaHuincul => Convert.ToInt32(cmbProveedorPlazaHuincul.SelectedValue);

        public async Task CargarTiposDeConsumoAsync(List<ConceptoTipo> tiposDeConsumo)
        {
            cmbTipoConsumo.DataSource = tiposDeConsumo;
            cmbTipoConsumo.DisplayMember = "Descripcion";
            cmbTipoConsumo.ValueMember = "IdConsumoTipo";
        }

        public async Task CargarProveedoresBahiaBlancaAsync(List<Proveedor> proveedores)
        {
            cmbProveedorBahiaBlanca.DataSource = proveedores;
            cmbProveedorBahiaBlanca.DisplayMember = "Codigo";  // Mostramos el código del proveedor
            cmbProveedorBahiaBlanca.ValueMember = "IdProveedor";
        }

        public async Task CargarProveedoresPlazaHuinculAsync(List<Proveedor> proveedores)
        {
            cmbProveedorPlazaHuincul.DataSource = proveedores;
            cmbProveedorPlazaHuincul.DisplayMember = "Codigo";
            cmbProveedorPlazaHuincul.ValueMember = "IdProveedor";
        }

        public void MostrarDatosConcepto(Concepto concepto)
        {
            Id = concepto.IdConsumo;
            txtCodigo.Text = concepto.Codigo;
            txtCodigo.Enabled = false;  // El código no se puede modificar al editar
            txtDescripcion.Text = concepto.Descripcion;
            cmbTipoConsumo.SelectedValue = concepto.IdConsumoTipo;
            txtPrecioActual.Text = concepto.PrecioActual.ToString("F2");
            txtPrecioAnterior.Text = concepto.PrecioAnterior.ToString("F2");
            dtpVigencia.Value = concepto.Vigencia;
            cmbProveedorBahiaBlanca.Enabled = true;
            cmbProveedorPlazaHuincul.Enabled = true;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConceptoAsync();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}