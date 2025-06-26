using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class AgregarEditarDisponibleForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarDisponibleView
    {
        public readonly AgregarEditarDisponiblePresenter _presenter;

        public AgregarEditarDisponibleForm(AgregarEditarDisponiblePresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        // --------- Métodos de la interfaz ---------
        public void CargarOrigenes(List<Locacion> origenes)
        {
            cmbOrigen.Properties.DataSource = origenes;
            cmbOrigen.Properties.DisplayMember = "Nombre";
            cmbOrigen.Properties.ValueMember = "IdLocacion";
            cmbOrigen.Properties.Columns.Clear();
            cmbOrigen.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Origen"));
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
            // Si tenés un combo para productos, lo cargás así:
            dataGridProductos.DataSource = productos;

            dataGridProductos.Columns["IdProducto"].Visible = false;
            dataGridProductos.Columns["Activo"].Visible = false;
        }

        public void MostrarDisponible(Disponible disponible)
        {
            // Setea los valores en los combos/campos del form
            dateEditFecha.EditValue = disponible.FechaDisponible;
            cmbOrigen.EditValue = disponible.IdOrigen;
            cmbDestino.EditValue = disponible.IdDestino;
            cmbCupo.EditValue = disponible.Cupo;
            txtObservaciones.Text = disponible.Observaciones ?? "";
            // Si tenés un combo de estado:
            // cmbEstado.EditValue = disponible.IdDisponibleEstado;
        }

        public void MostrarMantenimientosUnidad(string texto)
        {
            lblMantenimientosUnidad.Text = texto; // Asegurate de tener un label llamado así, o poné el nombre que uses
        }

        public void MostrarAusenciasChofer(string texto)
        {
            lblAusenciasChofer.Text = texto; // Asegurate de tener un label llamado así, o poné el nombre que uses
        }

        public Disponible ObtenerDisponible()
        {
            return new Disponible
            {
                IdOrigen = (int)cmbOrigen.EditValue,
                IdDestino = cmbDestino.EditValue is int id ? id : (int?)null,
                Cupo = (int)cmbCupo.EditValue,
                Observaciones = txtObservaciones.Text.Trim(),
                // IdDisponibleEstado = (int)cmbEstado.EditValue (si aplica)
            };
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // --------- Eventos ---------
        private async void cmbOrigen_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbOrigen.EditValue is int idOrigen)
            {
                await _presenter.ActualizarCuposDisponiblesAsync(idOrigen);
            }
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void bGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
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

        private async void bCambiarChofer_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirCambiarChoferAsync();
        }
    }
}