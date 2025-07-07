using DevExpress.XtraEditors;
using GestionOperativa.Presenters.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class CambiarTransportistaForm : Form, ICambiarTransportistaView
    {
        public readonly CambiarTransportistaPresenter _presenter;
        private int _idEntidad;
        private string _tipoEntidad;

        public CambiarTransportistaForm(CambiarTransportistaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdEmpresaSeleccionada => (int)cmbEmpresas.SelectedValue;

        public void CargarEmpresas(List<EmpresaDto> empresas)
        {
            cmbEmpresas.DataSource = empresas;
            cmbEmpresas.DisplayMember = "NombreFantasia";
            cmbEmpresas.ValueMember = "IdEmpresa";
        }

        public void SeleccionarEmpresaActual(int idEmpresa)
        {
            cmbEmpresas.SelectedValue = idEmpresa;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            Dispose();
        }

        public async Task CargarDatosAsync(int idEntidad, string tipoEntidad)
        {
            _idEntidad = idEntidad;
            _tipoEntidad = tipoEntidad;
            await _presenter.CargarDatosAsync(idEntidad, tipoEntidad);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync(_idEntidad, _tipoEntidad);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}