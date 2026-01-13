using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionOperativa.Presenters;
using Shared.Models;

namespace GestionOperativa
{
    public partial class AgregarEditarTransitoEspecialForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarTransitoEspecialView
    {
        public readonly AgregarEditarTransitoEspecialPresenter _presenter;

        public AgregarEditarTransitoEspecialForm(AgregarEditarTransitoEspecialPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
            CargarZonas();
        }

        private void CargarZonas()
        {
            cmbZona.Items.AddRange(new[]
            {
            "Administración", "Danés", "Servipetro", "Mercado Victoria", "Area de descanso", "Estacionamiento"
        });
        }

        public DateTime Fecha { set => lblFechaIngreso.Text = value.ToString("dd/MM/yyyy"); }
        public string Cuit => txtCuit.Text;
        public string RazonSocial => txtTransportista.Text;
        public string Nombre => txtNombre.Text;
        public string Apellido => txtApellido.Text;
        public string Documento => txtDni.Text;
        public DateTime? Licencia => dtpLicencia.EditValue as DateTime?;
        public DateTime? Art => dtpArt.EditValue as DateTime?;
        public DateTime? Seguro => dtpSeguro.EditValue as DateTime?;
        public string Tractor { get => txtTractor.Text; set => txtTractor.Text = value; }
        public string Semi => txtSemi.Text;
        public string Zona => cmbZona.Text;

        public void Close() => Dispose();

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CargarEmpresasTransitoEspecial(List<TransitoEspecialEmpresaDto> empresas)
        {
            cmbEmpresaTe.Properties.DataSource = empresas;
            cmbEmpresaTe.Properties.DisplayMember = nameof(TransitoEspecialEmpresaDto.Display);
            cmbEmpresaTe.Properties.ValueMember = nameof(TransitoEspecialEmpresaDto.Cuit);
            cmbEmpresaTe.Properties.NullText = "Seleccione empresa...";

            cmbEmpresaTe.Properties.Columns.Clear();
            cmbEmpresaTe.Properties.Columns.Add(
                new LookUpColumnInfo(nameof(TransitoEspecialEmpresaDto.RazonSocial), "Razón Social", 150));
            cmbEmpresaTe.Properties.Columns.Add(
                new LookUpColumnInfo(nameof(TransitoEspecialEmpresaDto.Cuit), "CUIT", 100));

            cmbEmpresaTe.EditValueChanged -= CmbEmpresaTe_EditValueChanged;
            cmbEmpresaTe.EditValueChanged += CmbEmpresaTe_EditValueChanged;
        }

        private void CmbEmpresaTe_EditValueChanged(object sender, EventArgs e)
        {
            var empresa = cmbEmpresaTe.GetSelectedDataRow() as TransitoEspecialEmpresaDto;
            if (empresa != null)
            {
                CompletarEmpresa(empresa.RazonSocial, empresa.Cuit);
            }
        }

        public void CompletarEmpresa(string razonSocial, string cuit)
        {
            txtTransportista.Text = razonSocial;
            txtCuit.Text = cuit;
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