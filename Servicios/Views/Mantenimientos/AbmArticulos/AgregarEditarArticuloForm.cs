using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters.Mantenimiento;
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
    public partial class AgregarEditarArticuloForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarArticuloView
    {
        public readonly AgregarEditarArticuloPresenter _presenter;

        public AgregarEditarArticuloForm(AgregarEditarArticuloPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int? IdArticulo { get; private set; }

        public string Codigo => txtCodigo.Text.Trim();
        public string Nombre => txtNombre.Text.Trim();
        public string? Descripcion => txtDescripcion.Text.Trim();
        public int IdMedida => Convert.ToInt32(cmbMedida.EditValue);
        public int IdArticuloFamilia => Convert.ToInt32(cmbFamilia.EditValue);
        public int? IdArticuloMarca => string.IsNullOrEmpty(cmbMarca.EditValue?.ToString()) ? null : Convert.ToInt32(cmbMarca.EditValue);
        public int? IdArticuloModelo => string.IsNullOrEmpty(cmbModelo.EditValue?.ToString()) ? null : Convert.ToInt32(cmbModelo.EditValue);
        public decimal PrecioUnitario => Convert.ToDecimal(txtPrecioUnitario.EditValue);
        public decimal? PedidoMinimo => string.IsNullOrEmpty(txtPedidoMinimo.Text) ? null : Convert.ToDecimal(txtPedidoMinimo.EditValue);
        public decimal? PedidoMaximo => string.IsNullOrEmpty(txtPedidoMaximo.Text) ? null : Convert.ToDecimal(txtPedidoMaximo.EditValue);

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        public void Cerrar()
        {
            Dispose();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        public void CargarMedidas(List<Medida> medidas)
        {
            cmbMedida.Properties.DataSource = medidas;
            cmbMedida.Properties.DisplayMember = "Descripcion";
            cmbMedida.Properties.ValueMember = "IdMedida";
            cmbMedida.Properties.NullText = "Seleccione Medida...";

            cmbMedida.Properties.Columns.Clear();
            cmbMedida.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Medida"));
        }

        public void CargarFamilias(List<ArticuloFamilia> familias)
        {
            cmbFamilia.Properties.DataSource = familias;
            cmbFamilia.Properties.DisplayMember = "Nombre";     // lo que ve el usuario
            cmbFamilia.Properties.ValueMember = "IdArticuloFamilia"; // el valor interno
            cmbFamilia.Properties.NullText = "Seleccione familia...";

            cmbFamilia.Properties.Columns.Clear();
            cmbFamilia.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Familia")); // solo muestra el Nombre
        }
        public void CargarMarcas(List<ArticuloMarca> marcas)
        {
            cmbMarca.Properties.DataSource = marcas;
            cmbMarca.Properties.DisplayMember = "Nombre";
            cmbMarca.Properties.ValueMember = "IdArticuloMarca";
            cmbMarca.Properties.NullText = "Seleccione Marca...";

            cmbMarca.Properties.Columns.Clear();
            cmbMarca.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Marca")); // solo muestra el Nombre
        }

        public void CargarModelos(List<ArticuloModelo> modelos)
        {
            cmbModelo.Properties.DataSource = modelos;
            cmbModelo.Properties.DisplayMember = "Nombre";
            cmbModelo.Properties.ValueMember = "IdArticuloModelo";
            cmbModelo.Properties.NullText = "Seleccione Modelo...";

            cmbModelo.Properties.Columns.Clear();
            cmbModelo.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Modelo")); // solo muestra el Nombre
        }

        public void MostrarDatosArticulo(Articulo articulo)
        {
            IdArticulo = articulo.IdArticulo;
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            cmbMedida.EditValue = articulo.IdMedida;
            cmbFamilia.EditValue = articulo.IdArticuloFamilia;
            cmbMarca.EditValue = articulo.IdArticuloMarca;
            cmbModelo.EditValue = articulo.IdArticuloModelo;
            txtPrecioUnitario.EditValue = articulo.PrecioUnitario;
            txtPedidoMinimo.EditValue = articulo.PedidoMinimo;
            txtPedidoMaximo.EditValue = articulo.PedidoMaximo;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void cmbMarca_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbMarca.EditValue?.ToString(), out int idMarca))
            {
                var modelos = await _presenter.ObtenerModelosPorMarcaAsync(idMarca);
                CargarModelos(modelos);
            }
            else
            {
                CargarModelos(new List<ArticuloModelo>());
            }
        }

        private async void bAgregarFamilia_Click(object sender, EventArgs e)
        {
            string nombre = XtraInputBox.Show("Ingrese el nombre de la nueva familia:", "Nueva Familia", "");
            if (string.IsNullOrWhiteSpace(nombre)) return;

            var nuevaFamilia = new ArticuloFamilia
            {
                Nombre = nombre,
                Activo = true
            };

            int id = await _presenter.AgregarFamiliaAsync(nuevaFamilia);

            // recargar familias
            var familias = await _presenter.ObtenerFamiliasAsync();
            CargarFamilias(familias);

            // seleccionar la recién creada
            cmbFamilia.EditValue = id;
        }

        private async void bAgregarMarca_Click(object sender, EventArgs e)
        {
            string nombre = XtraInputBox.Show("Ingrese el nombre de la nueva marca:", "Nueva Marca", "");
            if (string.IsNullOrWhiteSpace(nombre)) return;

            var nuevaMarca = new ArticuloMarca
            {
                Nombre = nombre,
                Activo = true
            };

            int id = await _presenter.AgregarMarcaAsync(nuevaMarca);

            var marcas = await _presenter.ObtenerMarcasAsync();
            CargarMarcas(marcas);

            cmbMarca.EditValue = id;
        }

        private async void bAgregarModelo_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(cmbMarca.EditValue?.ToString(), out int idMarca))
            {
                XtraMessageBox.Show("Debe seleccionar una marca antes de agregar un modelo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = XtraInputBox.Show("Ingrese el nombre del nuevo modelo:", "Nuevo Modelo", "");
            if (string.IsNullOrWhiteSpace(nombre)) return;

            var nuevoModelo = new ArticuloModelo
            {
                Nombre = nombre,
                IdArticuloMarca = idMarca,
                Activo = true
            };

            int id = await _presenter.AgregarModeloAsync(nuevoModelo);

            var modelos = await _presenter.ObtenerModelosPorMarcaAsync(idMarca);
            CargarModelos(modelos);

            cmbModelo.EditValue = id;
        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}