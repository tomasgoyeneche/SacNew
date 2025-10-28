using DevExpress.XtraEditors;
using GestionOperativa.Presenters;
using Shared.Models;
using System.Threading.Tasks;

namespace GestionOperativa.Views
{
    public partial class CambiarEstadoForm : DevExpress.XtraEditors.XtraForm, ICambiarEstadoView
    {
        public readonly CambiarEstadoPresenter _presenter;

        public DateTime? FechaCompleta =>
            dtpSalidaCompleta.EditValue is DateTime fecha ? fecha : null;

        public DateTime? FechaChofer =>
            dtpSalidaChofer.EditValue is DateTime fecha ? fecha : null;

        public DateTime? FechaTractor =>
           dtpSalidaTractor.EditValue is DateTime fecha ? fecha : null;

        public DateTime? FechaReingreso =>
           dtpReingreso.EditValue is DateTime fecha ? fecha : null;

        public DateTime? FechaCarga =>
          dtpSalidaCarga.EditValue is DateTime fecha ? fecha : null;

        public CambiarEstadoForm(CambiarEstadoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void Close()
        {
            Dispose();
        }

        public void MostrarMensaje(string Mensaje)
        {
            MensajeShow.Show(Mensaje);
        }

        public void InicializarBotones(GuardiaDto guardia, bool Admin)
        {
            if (Admin == true)
            {
                bEliminarEvento.Visible = false;
                bEstacionamiento.Visible = false;
                bVaporizado.Visible = false;
                bReparacion.Visible = false;
                bReingreso.Visible = false;
                dtpReingreso.Visible = false;
            }
            else
            {
                bEliminarEvento.Visible = true;
                bEstacionamiento.Visible = true;
                bVaporizado.Visible = true;
                bReingreso.Visible = true;
                bReparacion.Visible = true;
                dtpReingreso.Visible = true;
            }
            bSalidaCompleta.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bSalidaTractor.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bSalidaChofer.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bReingreso.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bVaporizado.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bReparacion.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bEstacionamiento.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bComentario.Enabled = guardia.TipoIngreso == 3 ? false : true;
            dtpReingreso.Enabled = guardia.TipoIngreso == 3 ? false : true;
            dtpSalidaChofer.Enabled = guardia.TipoIngreso == 3 ? false : true;
            dtpSalidaTractor.Enabled = guardia.TipoIngreso == 3 ? false : true;
            dtpSalidaCompleta.Enabled = guardia.TipoIngreso == 3 ? false : true;
            bCambiarChofer.Enabled = guardia.TipoIngreso == 1 ? true : false;
        }

        private async void bSalidaCompleta_Click(object sender, EventArgs e)
        {
            bSalidaCompleta.Enabled = false;
            await _presenter.RegistrarCambio(false, 5, "ST Completa");
            bSalidaCompleta.Enabled = true;
        }

        private async void bSalidaTractor_Click(object sender, EventArgs e)
        {
            bSalidaTractor.Enabled = false;
            await _presenter.RegistrarCambio(false, 2, "ST Tractor");
            bSalidaTractor.Enabled = true;
        }

        private async void bSalidaChofer_Click(object sender, EventArgs e)
        {
            bSalidaChofer.Enabled = false;
            await _presenter.RegistrarCambio(false, 3, "ST Chofer");
            bSalidaChofer.Enabled = true;
        }

        private async void bReingreso_Click(object sender, EventArgs e)
        {
            bReingreso.Enabled = false;
            await _presenter.RegistrarCambio(false, 6, "Reingreso");
            bReingreso.Enabled = true;
        }

        private async void bVaporizado_Click(object sender, EventArgs e)
        {
            bVaporizado.Enabled = false;
            await _presenter.RegistrarCambio(false, 7, "Vaporizado");
            bVaporizado.Enabled = true;
        }

        private async void bReparacion_Click(object sender, EventArgs e)
        {
            bReparacion.Enabled = false;
            await _presenter.RegistrarCambio(false, 8, "Reparacion");
            bReparacion.Enabled = true;
        }

        private async void bEstacionamiento_Click(object sender, EventArgs e)
        {
            bEstacionamiento.Enabled = false;
            await _presenter.RegistrarCambio(false, 9, "Estacionamiento");
            bEstacionamiento.Enabled = true;
        }

        private async void bComentario_Click(object sender, EventArgs e)
        {
            string comentario = XtraInputBox.Show(
            "Ingrese el comentario:",
            "Comentario",
            ""
        );

            // Si canceló o dejó vacío, no guardes nada
            if (string.IsNullOrWhiteSpace(comentario)) return;

            // Guardar el comentario en NominaRegistro
            await _presenter.RegistrarCambio(false, 11, comentario);
        }

        private async void dtpSalidaCompleta_EditValueChanged(object sender, EventArgs e)
        {
            dtpSalidaCompleta.Enabled = false;
            await _presenter.RegistrarCambio(true, 5, "ST Completa");
            dtpSalidaCompleta.Enabled = true;
        }

        private async void dtpSalidaTractor_EditValueChanged(object sender, EventArgs e)
        {
            dtpSalidaTractor.Enabled = false;   
            await _presenter.RegistrarCambio(true, 2, "ST Tractor");
            dtpSalidaTractor.Enabled = true;
        }

        private async void dtpSalidaChofer_EditValueChanged(object sender, EventArgs e)
        {
            dtpSalidaChofer.Enabled = false;
            await _presenter.RegistrarCambio(true, 3, "ST Chofer");
            dtpSalidaChofer.Enabled = true;
        }

        private async void dtpReingreso_EditValueChanged(object sender, EventArgs e)
        {
            dtpReingreso.Enabled = false;
            await _presenter.RegistrarCambio(true, 6, "Reingreso");
            dtpReingreso.Enabled = true;
        }

        private async void bEliminarEvento_Click(object sender, EventArgs e)
        {
            await _presenter.EliminarEventoAsync();
        }

        private async void bSalidaCarga_Click(object sender, EventArgs e)
        {
            bSalidaCarga.Enabled = false;
            await _presenter.RegistrarCambio(false, 4, "Salida Carga");
            bSalidaCarga.Enabled = true;
        }

        private async void dtpSalidaCarga_EditValueChanged(object sender, EventArgs e)
        {
            dtpSalidaCarga.Enabled = false;
            await _presenter.RegistrarCambio(true, 4, "Salida Carga");
            dtpSalidaCarga.Enabled = true;
        }

        private async void bCambiarChofer_Click(object sender, EventArgs e)
        {

            await _presenter.CambiarChoferAsync();
        }
    }
}