using DevExpress.XtraEditors;
using SacNew.Interfaces;
using SacNew.Presenters;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public partial class AgregarProductoForm : Form, IAgregarProductoView
    {
        public AgregarProductoPresenter _presenter;

        public AgregarProductoForm(AgregarProductoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int ProductoSeleccionado => (int)cmbProductos.SelectedValue;

        public void CargarProductos(List<Producto> productos)
        {
            cmbProductos.DataSource = productos;
            cmbProductos.DisplayMember = "Nombre";  // Mostrar el nombre del producto en el ComboBox
            cmbProductos.ValueMember = "IdProducto";  // El valor seleccionado será el ID del producto
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarProductoAsync();
            Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}