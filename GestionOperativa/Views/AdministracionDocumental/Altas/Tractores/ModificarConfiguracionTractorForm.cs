using GestionOperativa.Presenters.AdministracionDocumental.Altas.Tractor;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public partial class ModificarConfiguracionSemiForm : Form, ICambiarConfiguracionView
    {
        public readonly CambiarConfiguracionPresenter _presenter;
        private string _tipoEntidad;

        public ModificarConfiguracionSemiForm(CambiarConfiguracionPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string ConfiguracionSeleccionada => cmbConfiguracion.SelectedItem?.ToString() ?? "";

        public void CargarOpcionesConfiguracion(List<string> configuraciones)
        {
            cmbConfiguracion.DataSource = configuraciones;
        }

        public void SeleccionarConfiguracionActual(string configuracionActual)
        {
            cmbConfiguracion.SelectedItem = configuracionActual;
        }

        public void ConfigurarVistaPorEntidad(string tipoEntidad)
        {
            _tipoEntidad = tipoEntidad.ToLower();

            if (_tipoEntidad == "tractor")
            {
                lDsDd.Text = "DS";
                lDdsSss.Text = "DDS";
                lSdsDd_d.Text = "SDS";

                picBoxAutomatico.BackgroundImage = Properties.Resources.AUTOMATICO;
                picBoxManual.BackgroundImage = Properties.Resources.MANUAL;
                picBoxDsDd.BackgroundImage = Properties.Resources.DS;
                picBoxDdsSss.BackgroundImage = Properties.Resources.DDS;
                picBoxSdsDd_d.BackgroundImage = Properties.Resources.SDS;
            }
            else if (_tipoEntidad == "semi")
            {
                lDsDd.Text = "DD";
                lDdsSss.Text = "SSS";
                lSdsDd_d.Text = "DD_D";

                picBoxAutomatico.BackgroundImage = Properties.Resources.AUTOMATICO;
                picBoxManual.BackgroundImage = Properties.Resources.MANUAL;
                picBoxDsDd.BackgroundImage = Properties.Resources.DD;
                picBoxDdsSss.BackgroundImage = Properties.Resources.SSS;
                picBoxSdsDd_d.BackgroundImage = Properties.Resources.DD_D;
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConfiguracionAsync();
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}