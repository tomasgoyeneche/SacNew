using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class AgregarEditarAlertaForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarAlertaView
    {
        public readonly AgregarEditarAlertaPresenter _presenter;

        public AgregarEditarAlertaForm(AgregarEditarAlertaPresenter presenter)
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
            Close();
        }

        public int? IdChofer => Convert.ToInt32(cmbChofer.EditValue) == 0 ? (int?)null : Convert.ToInt32(cmbChofer.EditValue);

        public int? IdTractor => Convert.ToInt32(cmbTractor.EditValue) == 0 ? (int?)null : Convert.ToInt32(cmbTractor.EditValue);

        public int? IdSemi => Convert.ToInt32(cmbSemi.EditValue) == 0 ? (int?)null : Convert.ToInt32(cmbSemi.EditValue);

        public string Descripcion => txtObservaciones.Text.Trim();

        public void CargarChoferes(List<Chofer> choferes)
        {
            cmbChofer.Properties.DataSource = choferes;
            cmbChofer.Properties.DisplayMember = "NombreApellido";
            cmbChofer.Properties.ValueMember = "IdChofer";

            cmbChofer.Properties.Columns.Clear();
            cmbChofer.Properties.Columns.Add(new LookUpColumnInfo("NombreApellido", "Chofer"));

            cmbChofer.EditValue = _presenter.AlertaActual?.IdChofer ?? 0;
        }

        public void MostrarDatosAlerta(Alerta alerta)
        {
            txtObservaciones.Text = alerta.Descripcion;
            cmbChofer.EditValue = alerta.IdChofer;
            cmbTractor.EditValue = alerta.IdTractor;
            cmbSemi.EditValue = alerta.IdSemi;
        }

        public void CargarTractores(List<Tractor> tractores)
        {
            cmbTractor.Properties.DataSource = tractores;
            cmbTractor.Properties.DisplayMember = "Patente";
            cmbTractor.Properties.ValueMember = "IdTractor";

            cmbTractor.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbTractor.Properties.Columns.Add(new LookUpColumnInfo("Patente", "Tractor"));

            cmbTractor.EditValue = _presenter.AlertaActual?.IdTractor ?? 0;
        }

        public void CargarSemis(List<Semi> semis)
        {
            cmbSemi.Properties.DataSource = semis;
            cmbSemi.Properties.DisplayMember = "Patente";
            cmbSemi.Properties.ValueMember = "IdSemi";

            cmbSemi.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbSemi.Properties.Columns.Add(new LookUpColumnInfo("Patente", "IdSemi"));

            cmbSemi.EditValue = _presenter.AlertaActual?.IdSemi ?? 0;
        }

        public void Close()
        {
            Dispose();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}