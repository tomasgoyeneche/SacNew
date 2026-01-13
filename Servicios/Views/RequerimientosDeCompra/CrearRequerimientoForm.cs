using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Shared.Models.RequerimientoCompra;
using System.Data;

namespace Servicios.Views.RequerimientosDeCompra
{
    public partial class CrearRequerimientoForm
    : DevExpress.XtraEditors.XtraForm, ICrearRequerimientoView
    {
        public readonly CrearRequerimientoPresenter _presenter;

        public CrearRequerimientoForm(CrearRequerimientoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            ConfigurarColumnasDataGridView();
        }

        // ================= CABECERA =================
        public int NumeroRc { set => txtNumeroRc.Text = value.ToString(); }

        public DateTime Fecha { get => dateFecha.DateTime; set => dateFecha.DateTime = value; }

        public int? IdProveedor { get => cmbProveedor.EditValue as int?; set => cmbProveedor.EditValue = value; }
        public int? IdEmitido { get => cmbEmitido.EditValue as int?; set => cmbEmitido.EditValue = value; }
        public int? IdAprobado { get => cmbAprobado.EditValue as int?; set => cmbAprobado.EditValue = value; }

        public string FuncionEmitido { set => txtFuncionEmitido.Text = value; }
        public string FuncionAprobado { set => txtFuncionAprobado.Text = value; }

        public string EntregaLugar { get => txtEntregaLugar.Text; set => txtEntregaLugar.Text = value; }
        public string EntregaFecha { get => txtEntregaFecha.Text; set => txtEntregaFecha.Text = value; }
        public string Importe { get => txtImporte.Text; set => txtImporte.Text = value; }
        public string CondicionPago { get => txtCondicionPago.Text; set => txtCondicionPago.Text = value; }
        public string Observaciones { get => txtObservaciones.Text; set => txtObservaciones.Text = value; }

        // ================= DETALLE =================
        public string DetalleDescripcion => txtDetalleDescripcion.Text;

        public decimal DetalleCantidad => spinDetalleCantidad.Value;

        public void MostrarDetalles(List<RcDetalleRcc> detalles)
        {
            gridDetalles.DataSource = detalles;
            gridDetalles.RefreshDataSource();
        }

        public void LimpiarDetalle()
        {
            txtDetalleDescripcion.Text = string.Empty;
            spinDetalleCantidad.Value = 1;
        }

        // ================= CARGAS =================
        public void CargarProveedores(List<ProveedorRcc> proveedores)
        {
            cmbProveedor.Properties.DataSource = proveedores;
            cmbProveedor.Properties.ValueMember = nameof(ProveedorRcc.IdProveedor);
            cmbProveedor.Properties.DisplayMember = nameof(ProveedorRcc.RazonSocial);
            cmbProveedor.Properties.NullText = "Seleccione proveedor...";
            cmbProveedor.Properties.PopupWidth = 300;

            cmbProveedor.Properties.Columns.Clear();
            cmbProveedor.Properties.Columns.Add(new LookUpColumnInfo(nameof(ProveedorRcc.RazonSocial), "Razón Social", 150));
            cmbProveedor.Properties.Columns.Add(new LookUpColumnInfo(nameof(ProveedorRcc.Cuit), "CUIT", 100));

            cmbProveedor.Properties.CustomDisplayText -= CmbProveedor_CustomDisplayText;
            cmbProveedor.Properties.CustomDisplayText += CmbProveedor_CustomDisplayText;
        }

        private void CmbProveedor_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (e.Value == null) return;

            var lookUp = sender as LookUpEdit;
            var prov = lookUp?.GetSelectedDataRow() as ProveedorRcc;
            if (prov != null)
                e.DisplayText = $"{prov.RazonSocial} - {prov.Cuit}";
        }

        public void CargarUsuarios(List<UsuarioRcc> usuarios)
        {
            ConfigurarComboUsuario(cmbEmitido, usuarios);
            ConfigurarComboUsuario(cmbAprobado, usuarios);
        }

        private void ConfigurarComboUsuario(LookUpEdit combo, List<UsuarioRcc> usuarios)
        {
            combo.Properties.DataSource = usuarios;
            combo.Properties.ValueMember = nameof(UsuarioRcc.IdUsuario);
            combo.Properties.DisplayMember = nameof(UsuarioRcc.NombreApellido);
            combo.Properties.NullText = "Seleccione Usuario...";

            combo.Properties.Columns.Clear();
            combo.Properties.Columns.Add(new LookUpColumnInfo(nameof(UsuarioRcc.NombreApellido), "Nombre y Apellido", 150));
            combo.Properties.Columns.Add(new LookUpColumnInfo(nameof(UsuarioRcc.Funcion), "Función", 100));
        }

        // ================= IMPUTACIONES (DGV) =================
        private void ConfigurarColumnasDataGridView()
        {
            dgvImputaciones.Columns.Clear();
            dgvImputaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvImputaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Porcentaje",
                HeaderText = "Porcentaje",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvImputaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdImputacion",
                HeaderText = "ID Imputación",
                Visible = false
            });
        }

        public List<(int IdImputacion, int Porcentaje)> ObtenerImputaciones() =>
            dgvImputaciones.Rows.Cast<DataGridViewRow>()
                .Where(row => row.Cells["IdImputacion"].Value != null && row.Cells["Porcentaje"].Value != null)
                .Select(row => (
                    Convert.ToInt32(row.Cells["IdImputacion"].Value),
                    Convert.ToInt32(row.Cells["Porcentaje"].Value)))
                .ToList();

        // ================= EVENTOS =================
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            _presenter.AgregarDetalle();
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            var detalle = gvDetalles.GetFocusedRow() as RcDetalleRcc;
            if (detalle != null)
                _presenter.EliminarDetalle(detalle);
        }

        private async void cmbAprobado_EditValueChanged(object sender, EventArgs e)
        {
            if (IdAprobado.HasValue)
                await _presenter.UsuarioAprobadorSeleccionadoAsync(IdAprobado.Value);
        }

        private void bCancelar_Click(object sender, EventArgs e) => Cerrar();

        private async void bAgregarImputacion_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirAgregarImputacionAsync(CalcularPorcentajeActual());
        }

        private void btnEliminarImputacion_Click(object sender, EventArgs e)
        {
            if (dgvImputaciones.SelectedRows.Count > 0)
            {
                dgvImputaciones.Rows.RemoveAt(dgvImputaciones.SelectedRows[0].Index);
                txtTotalPorcentaje.Text = $"{CalcularPorcentajeActual()}%";
            }
            else
            {
                MostrarMensaje("Debe seleccionar una imputación para eliminar.");
            }
        }

        public void AgregarImputacionDependencia(string descripcion, int idImputacion, int porcentaje)
        {
            if (CalcularPorcentajeActual() + porcentaje > 100)
            {
                MostrarMensaje("El porcentaje total no puede superar el 100%.");
                return;
            }

            dgvImputaciones.Rows.Add(descripcion, porcentaje, idImputacion);
            txtTotalPorcentaje.Text = $"{CalcularPorcentajeActual()}%";
        }

        private int CalcularPorcentajeActual()
        {
            int total = 0;

            foreach (DataGridViewRow row in dgvImputaciones.Rows)
            {
                if (row.Cells["Porcentaje"].Value != null &&
                    int.TryParse(row.Cells["Porcentaje"].Value.ToString(), out int porcentaje))
                {
                    total += porcentaje;
                }
            }

            return total;
        }

        // ================= UTILES =================
        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(mensaje, "RC",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void BloquearEmitido()
        {
            cmbEmitido.Enabled = false;
            txtFuncionEmitido.Enabled = false;
        }

        public void Cerrar() => Dispose();
    }
}