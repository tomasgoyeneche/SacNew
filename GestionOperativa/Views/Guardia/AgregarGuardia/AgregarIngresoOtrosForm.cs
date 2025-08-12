using DevExpress.XtraEditors;
using GestionOperativa.Presenters.AgregarGuardia;

namespace GestionOperativa.Views.AgregarGuardia
{
    public partial class AgregarIngresoOtrosForm : DevExpress.XtraEditors.XtraForm, IAgregarIngresoOtrosView
    {
        public readonly AgregarIngresoOtrosPresenter _presenter;

        public AgregarIngresoOtrosForm(AgregarIngresoOtrosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
            CargarZonas();
        }

        private void CargarZonas()
        {
            cmbTipoIngreso.Items.AddRange(new[]
            {
            "Proveedor", "Cliente", "Visita" //, Ingreso
            });
        }

        public DateTime Fecha { set => lblFechaIngreso.Text = value.ToString("dd/MM/yyyy"); }
        public string Observaciones => txtObservaciones.Text;
        public string Nombre => txtNombre.Text;
        public string Apellido => txtApellido.Text;
        public string Documento => txtDni.Text;
        public DateTime? Licencia => dtpLicencia.EditValue as DateTime?;
        public DateTime? Art => dtpArt.EditValue as DateTime?;
        public string Patente { get => txtPatente.Text; set => txtPatente.Text = value; }
        public string Empresa => txtEmpresa.Text;
        public string TipoIngreso => cmbTipoIngreso.Text;

        public void Close() => Dispose();

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}