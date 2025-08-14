using DevExpress.XtraEditors.Controls;
using GestionOperativa.Presenters.Choferes;
using GestionOperativa.Views.AdministracionDocumental.Altas.Choferes;
using Shared.Models;
using System.IO;

namespace GestionDocumental.Views.AdministracionDocumental.Altas.Choferes
{
    public partial class ModificarChoferDatosForm : DevExpress.XtraEditors.XtraForm, IModificarDatosChoferView
    {
        public ModificarDatosChoferPresenter _presenter;

        public ModificarChoferDatosForm(ModificarDatosChoferPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdChofer { get; private set; }

        public string Nombre => txtNombres.Text.Trim();
        public string Apellido => txtApellido.Text.Trim();
        public string Documento => txtDoc.Text.Trim();

        public DateTime? FechaNacimiento => dtpFechaNac.EditValue as DateTime?;

        public int idEmpresa => (int)cmdEmpresaId.EditValue;
        public bool ZonaFria => chkZonaFria.Checked;
        public DateTime? FechaAlta => dtpFecha.EditValue as DateTime?;
        public int IdProvincia => (int)cmbProvincia.EditValue;
        public int IdLocalidad => (int)cmbLocalidad.EditValue;
        public string Domicilio => txtDomicilio.Text.Trim();
        public string Telefono => txtTelefono.Text.Trim();
        public string Celular => txtCelular.Text.Trim();

        public async void CargarDatosChofer(Chofer chofer, List<EmpresaDto> empresa, List<Provincia> provincias, int idProvincia)
        {
            IdChofer = chofer.IdChofer;
            txtNombres.Text = chofer.Nombre;
            txtApellido.Text = chofer.Apellido;

            txtDoc.Text = chofer.Documento;
            txtDomicilio.Text = chofer.Domicilio;
            txtTelefono.Text = chofer.Telefono;
            txtCelular.Text = chofer.Celular;

            dtpFecha.EditValue = chofer.FechaAlta;
            dtpFechaNac.EditValue = chofer.FechaNacimiento;
            chkZonaFria.Checked = chofer.ZonaFria;

            cmdEmpresaId.Properties.DataSource = empresa;
            cmdEmpresaId.Properties.DisplayMember = "RazonSocial";
            cmdEmpresaId.Properties.ValueMember = "IdEmpresa";
            cmdEmpresaId.Properties.Columns.Clear();
            cmdEmpresaId.Properties.Columns.Add(new LookUpColumnInfo("RazonSocial", "Empresa"));
            cmdEmpresaId.EditValue = chofer.IdEmpresa;

            cmbProvincia.Properties.DataSource = provincias;
            cmbProvincia.Properties.DisplayMember = "NombreProvincia";
            cmbProvincia.Properties.ValueMember = "IdProvincia";
            cmbProvincia.Properties.Columns.Clear();
            cmbProvincia.Properties.Columns.Add(new LookUpColumnInfo("NombreProvincia", "Provincias"));
            cmbProvincia.EditValue = idProvincia;

            await _presenter.CargarLocalidades(idProvincia);
            cmbLocalidad.EditValue = chofer.IdLocalidad;
        }

        public void ConfigurarFotoChofer(string? rutaArchivo)
        {
            if (!string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo))
            {
                PhotoPictureEdit.Image = Image.FromFile(rutaArchivo);
            }
            else
            {
                PhotoPictureEdit.Image = null;
            }
        }

        public void CargarLocalidades(List<Localidad> localidades)
        {
            cmbLocalidad.Properties.DataSource = localidades;
            cmbLocalidad.Properties.DisplayMember = "NombreLocalidad";
            cmbLocalidad.Properties.ValueMember = "idLocalidad";
            cmbLocalidad.Properties.Columns.Clear();
            cmbLocalidad.Properties.Columns.Add(new LookUpColumnInfo("NombreLocalidad", "Localidades"));
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.EditValue is int idProvincia)
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

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}