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
    public partial class AgregarProgramaManual : DevExpress.XtraEditors.XtraForm, INuevoProgramaView
    {
        public readonly NuevoProgramaPresenter _presenter;

        public AgregarProgramaManual(NuevoProgramaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarAlertas(List<AlertaDto> alertas)
        {
            gridControlAlertas.DataSource = alertas;

            var view = gridViewAlertas;
            foreach (var col in new[] { "IdAlerta", "IdNomina", "Activo", "PatenteSemi" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewAlertas.BestFitColumns();
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

        public void CargarDestinos(List<Locacion> destinos)
        {
            cmbDestino.Properties.DataSource = destinos;
            cmbDestino.Properties.DisplayMember = "Nombre";
            cmbDestino.Properties.ValueMember = "IdLocacion";

            cmbDestino.Properties.Columns.Clear();
            cmbDestino.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Destino"));
        }

        public void CargarCupos(List<int> cupos)
        {
            cmbCupo.Properties.DataSource = cupos;
            cmbCupo.Properties.Columns.Clear();
        }
        public void CargarProductos(List<Producto> productos)
        {
            cmbProducto.Properties.DataSource = productos;
            cmbProducto.Properties.DisplayMember = "Nombre";
            cmbProducto.Properties.ValueMember = "IdProducto";

            cmbProducto.Properties.Columns.Clear();
            cmbProducto.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Producto"));
        }

        public int? IdOrigenSeleccionado => cmbOrigen.EditValue as int?;
        public int? IdDestinoSeleccionado => cmbDestino.EditValue as int?;
        public int? IdProductoSeleccionado => cmbProducto.EditValue as int?;
        public int? Cupo => cmbCupo.EditValue as int?;

        public DateTime FechaCarga => dtpFechaCarga.EditValue as DateTime? ?? DateTime.Today;
        public DateTime FechaEntrega => dtpFechaEntrega.EditValue as DateTime? ?? DateTime.Today;
        public string Albaran => txtAlbaranDespacho.Text.Trim();
        public string Pedido => txtPedidoOr.Text.Trim();
        public string Observaciones => txtObservaciones.Text.Trim();

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            this.Dispose();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private async void bObservaciones_Click(object sender, EventArgs e)
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

        private async void bCambioChofer_Click(object sender, EventArgs e)
        {
            await _presenter.CambiarChoferAsync();
        }

        private async void bCancelarDisponible_Click(object sender, EventArgs e)
        {
            await _presenter.CancelarDisponibleAsync();
        }


        private async void bFrancos_Click(object sender, EventArgs e)
        {
            // await _presenter.AbrirNovedades("Chofer", "0014-NovedadesChoferes");
        }

        private async void cmbOrigen_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbOrigen.EditValue is int idOrigen)
            {
                await _presenter.ActualizarCuposDisponiblesAsync(idOrigen);

            }
        }
    }
}