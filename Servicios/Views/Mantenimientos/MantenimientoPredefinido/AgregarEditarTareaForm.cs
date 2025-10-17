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
            var dto = gridViewArticulos.GetFocusedRow() as TareaArticuloDto;
            if (dto != null)
                await _presenter.EliminarArticuloAsync(dto.IdArticulo);
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
    }
}