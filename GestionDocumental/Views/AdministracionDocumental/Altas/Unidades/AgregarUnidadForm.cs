using DevExpress.XtraEditors;
using GestionOperativa.Presenters.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class AgregarUnidadForm : DevExpress.XtraEditors.XtraForm, IAgregarUnidadView
    {
        public readonly AgregarUnidadPresenter _presenter;

        public AgregarUnidadForm(AgregarUnidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdEmpresa { get; private set; }

        public int? IdTractorSeleccionado => dgvTractores.SelectedRows.Count > 0
            ? dgvTractores.SelectedRows[0].Cells["IdTractor"].Value as int?
            : null;

        public int? IdSemiSeleccionado => dgvSemis.SelectedRows.Count > 0
            ? dgvSemis.SelectedRows[0].Cells["IdSemi"].Value as int?
            : null;

        public bool UsaMetanol => chkMetanol.Checked;
        public bool UsaGasoil => chkGasoil.Checked;
        public bool UsaLujanCuyo => chkLujanCuyo.Checked;
        public bool UsaAptoBo => chkAptoBo.Checked;

        public void MostrarEmpresa(string nombreEmpresa, int idEmpresa)
        {
            lblEmpresa.Text = $"Empresa: {nombreEmpresa}";
            IdEmpresa = idEmpresa;
        }

        public void CargarTractores(List<Tractor> tractores)
        {
            dgvTractores.DataSource = tractores;

            foreach (DataGridViewColumn col in dgvTractores.Columns)
                col.Visible = col.DataPropertyName == "Patente";
        }

        public void CargarSemis(List<Semi> semis)
        {
            dgvSemis.DataSource = semis;

            foreach (DataGridViewColumn col in dgvSemis.Columns)
                col.Visible = col.DataPropertyName == "Patente";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarUnidadAsync();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }
    }
}