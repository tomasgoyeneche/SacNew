using GestionOperativa.Presenters;
using Shared.Models;

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
            }
            else
            {
                bEliminarEvento.Visible = true;
                bEstacionamiento.Visible = true;
                bVaporizado.Visible = true;
                bReingreso.Visible = true;
                bReparacion.Visible = true;
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
            await _presenter.RegistrarCambio(false, 5, "ST Completa");
        }

        private async void bSalidaTractor_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(false, 2, "ST Tractor");
        }

        private async void bSalidaChofer_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(false, 3, "ST Chofer");
        }

        private async void bReingreso_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(false, 6, "Reingreso");
        }

        private void bVaporizado_Click(object sender, EventArgs e)
        {
            // En Desarrollo
        }

        private async void bReparacion_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(false, 8, "Reparacion");
        }

        private async void bEstacionamiento_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(false, 9, "Estacionamiento");
        }

        private async void bComentario_Click(object sender, EventArgs e)
        {
            string Comentario = Microsoft.VisualBasic.Interaction.InputBox("Por favor ingrese comentario:", "Ingrese comentario", "");

            await _presenter.RegistrarCambio(false, 10, Comentario);
        }

        private async void dtpSalidaCompleta_EditValueChanged(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(true, 5, "ST Completa");
        }

        private async void dtpSalidaTractor_EditValueChanged(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(true, 2, "ST Tractor");
        }

        private async void dtpSalidaChofer_EditValueChanged(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(true, 3, "ST Chofer");
        }

        private async void dtpReingreso_EditValueChanged(object sender, EventArgs e)
        {
            await _presenter.RegistrarCambio(true, 6, "Reingreso");
        }

        private async void bEliminarEvento_Click(object sender, EventArgs e)
        {
            await _presenter.EliminarEventoAsync();
        }
    }
}