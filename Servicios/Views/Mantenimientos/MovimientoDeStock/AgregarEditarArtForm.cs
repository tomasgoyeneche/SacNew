using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Servicios.Views.Mantenimiento;
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

namespace Servicios.Views
{
    public partial class AgregarEditarArtForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarMovimientoStockDetalleView
    {
        public readonly AgregarEditarMovimientoStockDetallePresenter _presenter;

        public AgregarEditarArtForm(AgregarEditarMovimientoStockDetallePresenter presenter)
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

        public void Cerrar()
        {
            Dispose();
        }

        // 🔹 Properties expuestas al presenter
        public int IdMovimientoStock { get; set; }

        public int IdArticulo
        {
            get => Convert.ToInt32(lookupArticulo.EditValue ?? 0);
            set => lookupArticulo.EditValue = value;
        }

        public int IdPosta { get; set; }  // lo setea el presenter con SesionService

        public decimal Cantidad
        {
            get => Convert.ToDecimal(txtCantidad.EditValue ?? 0);
            set => txtCantidad.EditValue = value;
        }

        public decimal PrecioUnitario
        {
            get => Convert.ToDecimal(txtPrecioUnitario.EditValue ?? 0);
            set => txtPrecioUnitario.EditValue = value;
        }

        public decimal PrecioTotal
        {
            get => Convert.ToDecimal(txtPrecioTotal.EditValue ?? 0);
            set => txtPrecioTotal.EditValue = value;
        }

        public void CargarArticulos(List<Articulo> articulos)
        {
            lookupArticulo.Properties.DataSource = articulos;
            lookupArticulo.Properties.DisplayMember = "Codigo";
            lookupArticulo.Properties.ValueMember = "IdArticulo";

            lookupArticulo.Properties.Columns.Clear();
            lookupArticulo.Properties.Columns.Add(new LookUpColumnInfo("Codigo", "Código"));
        }

        public void MostrarArticuloSeleccionado(Articulo articulo)
        {
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            if (articulo.PrecioUnitario != 0) {
                PrecioUnitario = articulo.PrecioUnitario;
                CalcularTotal();
            }
          
      
        }

        public void CalcularTotal()
        {
            PrecioTotal = Cantidad * PrecioUnitario;
        }

        public void SuspenderEventoArticulo()
        {
            lookupArticulo.EditValueChanged -= lookupArticulo_EditValueChanged;
        }

        public void ReanudarEventoArticulo()
        {
            lookupArticulo.EditValueChanged += lookupArticulo_EditValueChanged;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Close()
        {
            Dispose();
        }

        // 🔹 Eventos locales de controles
        private async void lookupArticulo_EditValueChanged(object sender, EventArgs e)
        {
            if (lookupArticulo.EditValue is int idArticulo)
                await _presenter.ArticuloSeleccionadoAsync(idArticulo);
        }

        private void spinCantidad_EditValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }
    }
}