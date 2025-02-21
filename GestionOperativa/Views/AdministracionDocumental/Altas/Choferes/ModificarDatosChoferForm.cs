using GestionOperativa.Presenters.Choferes;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Choferes
{
    public partial class ModificarDatosChoferForm : Form, IModificarDatosChoferView
    {
        public ModificarDatosChoferPresenter _presenter;

        public ModificarDatosChoferForm(ModificarDatosChoferPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdChofer { get; private set; }

        public string Nombre => txtNombres.Text.Trim();
        public string Apellido => txtApellido.Text.Trim();
        public string Documento => txtDoc.Text.Trim();

        public DateTime FechaNacimiento => dtpFechaNac.Value;

        public int idEmpresa => (int)cmdEmpresaId.SelectedValue;
        public bool ZonaFria => chkZonaFria.Checked;
        public DateTime FechaAlta => dtpFecha.Value;
        public int IdProvincia => (int)cmbProvincia.SelectedValue;
        public int IdLocalidad => (int)cmbLocalidad.SelectedValue;
        public string Domicilio => txtDomicilio.Text;
        public string Telefono => txtTelefono.Text;

        public void CargarDatosChofer(Chofer chofer, List<EmpresaDto> empresa, List<Provincia> provincias)
        {
            IdChofer = chofer.IdChofer;
            txtNombres.Text = chofer.Nombre;
            txtApellido.Text = chofer.Apellido;

            txtDoc.Text = chofer.Documento;
            txtDomicilio.Text = chofer.Domicilio;
            txtTelefono.Text = chofer.Telefono;

            dtpFecha.Value = chofer.FechaAlta;
            dtpFechaNac.Value = chofer.FechaNacimiento;
            chkZonaFria.Checked = chofer.ZonaFria;

            cmdEmpresaId.DataSource = empresa;
            cmdEmpresaId.DisplayMember = "razonSocial";
            cmdEmpresaId.ValueMember = "IdEmpresa";
            cmdEmpresaId.SelectedValue = chofer.IdEmpresa;

            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "NombreProvincia";
            cmbProvincia.ValueMember = "IdProvincia";
            cmbLocalidad.SelectedValue = chofer.IdLocalidad; // Se ajustará luego con las localidades.
        }

        public void CargarLocalidades(List<Localidad> localidades)
        {
            cmbLocalidad.DataSource = localidades;
            cmbLocalidad.DisplayMember = "NombreLocalidad";
            cmbLocalidad.ValueMember = "IdLocalidad";
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia)
            {
                await _presenter.CargarLocalidades(idProvincia);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarCambios();
            Hide();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}