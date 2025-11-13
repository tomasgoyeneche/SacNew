using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Shared.Models;

namespace Servicios.Views.Mantenimientos
{
    public partial class AgregarEditarTareaForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarTareaView
    {
        public readonly AgregarEditarTareaPresenter _presenter;

        public AgregarEditarTareaForm(AgregarEditarTareaPresenter presenter)
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

        private async void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarArticuloAsync();
        }

        private async void btnQuitarArticulo_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Eliminar este Articulo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                var dto = gridViewArticulos.GetFocusedRow() as TareaArticuloDto;
                if (dto != null)
                    await _presenter.EliminarArticuloAsync(dto.IdArticulo, dto.IdOrdenTrabajoArticulo);
            }
        }

        // =====================================
        // 🔹 Propiedades expuestas
        // =====================================
        public int IdTarea { get; set; }

        public string TipoVista { get; set; } = "MantenimientoPredefinido";

        public string Nombre
        {
            get => txtNombre.Text.Trim();
            set => txtNombre.Text = value;
        }

        public string Descripcion
        {
            get => txtDescripcion.Text.Trim();
            set => txtDescripcion.Text = value;
        }

        public decimal Horas
        {
            get => Convert.ToDecimal(txtHoras.EditValue ?? 0);
            set => txtHoras.EditValue = value;
        }

        public decimal ManoObra
        {
            get => Convert.ToDecimal(txtManoObra.EditValue ?? 0);
            set => txtManoObra.EditValue = value;
        }

        public decimal Repuestos
        {
            get => Convert.ToDecimal(txtRepuestos.EditValue ?? 0);
            set => txtRepuestos.EditValue = value;
        }

        public int IdArticuloSeleccionado => Convert.ToInt32(cmbArticulo.EditValue ?? 0);

        public decimal CantidadArticulo
        {
            get => Convert.ToDecimal(txtCantidad.EditValue ?? 0);
            set => txtCantidad.EditValue = value;
        }

        public void CargarArticulos(List<Articulo> articulos)
        {
            cmbArticulo.Properties.DataSource = articulos;
            cmbArticulo.Properties.DisplayMember = "Codigo";
            cmbArticulo.Properties.ValueMember = "IdArticulo";
            cmbArticulo.Properties.NullText = "Seleccione un artículo...";
            cmbArticulo.Properties.Columns.Clear();
            cmbArticulo.Properties.Columns.Add(new LookUpColumnInfo("Codigo", "Código"));
            cmbArticulo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Descripción"));
        }

        public void CargarArticulosAsociados(List<TareaArticuloDto> articulos)
        {
            gridControlArticulos.DataSource = articulos;
            gridViewArticulos.BestFitColumns();
        }

        public void Cerrar() => Dispose();

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void bEditarComprobante_Click(object sender, EventArgs e)
        {
            var articulo = gridViewArticulos.GetFocusedRow() as TareaArticuloDto;

            if (articulo.Estado == "Confirmado")
            {
                MostrarMensaje("No se puede editar un articulo confirmado");
                return;
            }

            if (articulo.IdOrdenTrabajoArticulo == null)
            {
                
                await _presenter.AgregarEditarArticulosAsync(articulo.IdArticulo);
            }
            else
            {
                await _presenter.AgregarEditarArticulosAsync(articulo.IdOrdenTrabajoArticulo);
            }
        }

        public void MostrarMovimientoStock(bool estado)
        {
            bMoverStock.Enabled = estado;
            bMoverStock.Visible = estado;
        }

        private async void bMoverStock_Click(object sender, EventArgs e)
        {
            var articulo = gridViewArticulos.GetFocusedRow() as TareaArticuloDto;
            if (articulo == null)
            {
                MostrarMensaje("Seleccione un articulo para mover el stock");
                return;
            }
            else
            {
                if (articulo.Estado == "Confirmado")
                {
                    MostrarMensaje("No se puede mover el stock de un articulo confirmado");
                    return;
                }
                else
                {
                    await _presenter.MoverStockArticulo(articulo.IdOrdenTrabajoArticulo.Value);
                }
            }
        }

        private async void bNuevoArticulo_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarEditarArticulosAsync();
        }

        private void gridViewArticulos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            var row = view.GetRow(e.RowHandle) as TareaArticuloDto;
            if (row == null)
                return;

            if (row.Estado == "Pendiente")
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.BackColor2 = Color.Khaki;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (row.Estado == "Confirmado")
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.BackColor2 = Color.MediumSeaGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
    }
}