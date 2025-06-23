using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters;
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

namespace GestionFlota.Views
{
    public partial class AsignarCargaForm : DevExpress.XtraEditors.XtraForm, IAsignarCargaView
    {
        public readonly AsignarCargaPresenter _presenter;

        public AsignarCargaForm(AsignarCargaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void CargarOrigenes(List<Locacion> origenes, int? idOrigenSeleccionado)
        {
            cmbOrigen.Properties.DataSource = origenes;
            cmbOrigen.Properties.DisplayMember = "Nombre";
            cmbOrigen.Properties.ValueMember = "IdLocacion";

            cmbOrigen.Properties.Columns.Clear();
            cmbOrigen.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Origen"));

            cmbOrigen.EditValue = idOrigenSeleccionado;
        }

        public void CargarDestinos(List<Locacion> destinos, int? idDestinoSeleccionado)
        {
            cmbDestino.Properties.DataSource = destinos;
            cmbDestino.Properties.DisplayMember = "Nombre";
            cmbDestino.Properties.ValueMember = "IdLocacion";


            cmbDestino.Properties.Columns.Clear();
            cmbDestino.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Destino"));

            cmbDestino.EditValue = idDestinoSeleccionado;
        }

        public void CargarProductos(List<Producto> productos, int? idProductoSeleccionado)
        {
            cmbProducto.Properties.DataSource = productos;
            cmbProducto.Properties.DisplayMember = "Nombre";
            cmbProducto.Properties.ValueMember = "IdProducto";

            cmbProducto.Properties.Columns.Clear();
            cmbProducto.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Producto"));

            cmbProducto.EditValue = idProductoSeleccionado;
        }

        public int? IdOrigenSeleccionado => cmbOrigen.EditValue as int?;
        public int? IdDestinoSeleccionado => cmbDestino.EditValue as int?;
        public int? IdProductoSeleccionado => cmbProducto.EditValue as int?;

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            this.Close();
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            await _presenter.ConfirmarAsignacionAsync();
        }

        private async void bAgregarObservacion_Click(object sender, EventArgs e)
        {
            string comentario = XtraInputBox.Show(
            "Ingrese el comentario:",
            "Comentario",
            ""
        );

            // Si canceló o dejó vacío, no guardes nada
            if (string.IsNullOrWhiteSpace(comentario)) return;

            // Guardar el comentario en NominaRegistro
            await _presenter.RegistrarComentarioAsync(comentario);
        }

        private async void bOrdenCarga_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirOrdenCarga();
        }

        private async void bCancelarAsignado_Click(object sender, EventArgs e)
        {
            await _presenter.CancelarAsignacionAsync();
        }
    }
}