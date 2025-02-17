using GestionOperativa.Presenters.Empresas;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public partial class ModificarDatosEmpresaForm : Form, IModificarDatosEmpresaView
    {
        public ModificarDatosEmpresaPresenter _presenter;

        public ModificarDatosEmpresaForm(ModificarDatosEmpresaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdEmpresa { get; private set; }

        public string Cuit => txtCuit.Text;
        public int IdEmpresaTipo => (int)cmbEmpresaTipo.SelectedValue;
        public string RazonSocial => txtRazonSocial.Text;
        public string NombreFantasia => txtNombreFantasia.Text;
        public int IdProvincia => (int)cmbProvincia.SelectedValue;
        public int IdLocalidad => (int)cmbLocalidad.SelectedValue;
        public string Domicilio => txtDomicilio.Text;
        public string Telefono => txtTelefono.Text;
        public string Email => txtEmail.Text;

        public void CargarDatosEmpresa(Empresa empresa, List<EmpresaTipo> tipos, List<Provincia> provincias)
        {
            IdEmpresa = empresa.IdEmpresa;
            txtCuit.Text = empresa.Cuit;
            cmbEmpresaTipo.DataSource = tipos;
            cmbEmpresaTipo.DisplayMember = "Descripcion";
            cmbEmpresaTipo.ValueMember = "IdEmpresaTipo";
            cmbEmpresaTipo.SelectedValue = empresa.IdEmpresaTipo;

            txtRazonSocial.Text = empresa.RazonSocial;
            txtNombreFantasia.Text = empresa.NombreFantasia;
            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "NombreProvincia";
            cmbProvincia.ValueMember = "IdProvincia";
            cmbProvincia.SelectedValue = empresa.IdLocalidad; // Se ajustará luego con las localidades.

            txtDomicilio.Text = empresa.Domicilio;
            txtTelefono.Text = empresa.Telefono;
            txtEmail.Text = empresa.Email;
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