using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using System.Data;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public partial class AgregarEditarLocacion : Form, IAgregarEditarLocacionView
    {
        public AgregarEditarLocacionPresenter _presenter;

        public AgregarEditarLocacion(AgregarEditarLocacionPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string Nombre => txtNombre.Text.Trim();
        public string Direccion => txtDireccion.Text.Trim();

        public bool Carga => cbCarga.SelectedItem.ToString() == "Sí";
        public bool Descarga => cbDescarga.SelectedItem.ToString() == "Sí";

        public void MostrarDatosLocacion(Locacion locacion)
        {
            txtNombre.Text = locacion.Nombre;
            txtDireccion.Text = locacion.Direccion;
            cbCarga.SelectedItem = locacion.Carga ? "Sí" : "No";
            cbDescarga.SelectedItem = locacion.Descarga ? "Sí" : "No";
        }

        public void CargarProductos(List<LocacionProducto> productos)
        {
            dataGridViewProductos.DataSource = productos.Select(p => new
            {
                p.IdLocacionProducto,
                p.Producto.Nombre,
            }).ToList();

            dataGridViewProductos.Columns["IdLocacionProducto"].Visible = false;

        }

        public void CargarKilometros(List<LocacionKilometrosEntre> kilometrosEntre)
        {
            dataGridViewKilometros.DataSource = kilometrosEntre.Select(k => new
            {
                k.IdKilometros,
                LocacionDestino = k.LocacionDestino.Nombre,
                k.Kilometros
            }).ToList();


            dataGridViewKilometros.Columns["idKilometros"].Visible = false;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }
        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                await _presenter.GuardarLocacionAsync();
                Close();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar locación: {ex.Message}");
            }
        }

        private async void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dataGridViewProductos.SelectedRows.Count > 0)
            {
                int idLocacionProducto = Convert.ToInt32(dataGridViewProductos.SelectedRows[0].Cells["IdLocacionProducto"].Value);
                await _presenter.EliminarProductoAsync(idLocacionProducto);
            }
        }

        private async void btnEliminarKilometros_Click(object sender, EventArgs e)
        {
            if (dataGridViewKilometros.SelectedRows.Count > 0)
            {
                int idKilometros = Convert.ToInt32(dataGridViewKilometros.SelectedRows[0].Cells["IdKilometros"].Value);
                await _presenter.EliminarKilometrosAsync(idKilometros);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        public void EstablecerModoEdicion(bool habilitar)
        {
            // Habilitar o deshabilitar los DataGridViews y botones
            dataGridViewProductos.Enabled = habilitar;
            dataGridViewKilometros.Enabled = habilitar;
            btnEliminarProducto.Enabled = habilitar;
            btnEliminarKilometros.Enabled = habilitar;
            btnAgregarKilometro.Enabled = habilitar;
            btnAgregarProducto.Enabled = habilitar;
            // Si hay más controles, puedes agregarlos aquí
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            _presenter.AgregarProducto();

        }

        private void btnAgregarKilometro_Click(object sender, EventArgs e)
        {
            _presenter.AgregarKilometro();
        }
    }
}