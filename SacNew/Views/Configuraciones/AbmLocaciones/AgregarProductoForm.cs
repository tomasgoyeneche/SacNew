using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarProducto();
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
