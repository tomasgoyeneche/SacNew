using GestionOperativa.Presenters.AdministracionDocumental;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Semis
{
    public partial class ModificarDatosSemiForm : Form, IModificarDatosSemiView
    {
        public readonly ModificarDatosSemiPresenter _presenter;

        public ModificarDatosSemiForm(ModificarDatosSemiPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdSemi { get; private set; }
        public string Patente => txtPatente.Text.Trim();
        public DateTime Anio => dtpFechaAlta.Value;
        public int IdMarca => (int)cmbMarca.SelectedValue;
        public int IdModelo => (int)cmbModelo.SelectedValue;
        public decimal Tara => Convert.ToDecimal(txtTara.Text);
        public DateTime FechaAlta => dtpFechaAlta.Value;
        public int IdTipoCarga => (int)txtTipoCarga.SelectedValue;
        public int Compartimientos => Convert.ToInt32(txtCisternas.Text);
        public int IdMaterial => (int)cmbMaterial.SelectedValue;

        public void CargarDatosSemi(Semi semi, List<VehiculoMarca> marcas, List<VehiculoModelo> modelos,
                                    List<SemiCisternaTipoCarga> tiposCarga, List<SemiCisternaMaterial> materiales)
        {
            IdSemi = semi.IdSemi;
            txtPatente.Text = semi.Patente;
            dtpAnio.Value = semi.Anio;
            txtTara.Text = semi.Tara.ToString();
            dtpFechaAlta.Value = semi.FechaAlta;
            txtCisternas.Text = semi.Compartimientos.ToString();

            cmbMarca.DataSource = marcas;
            cmbMarca.DisplayMember = "NombreMarca";
            cmbMarca.ValueMember = "IdMarca";
            cmbMarca.SelectedValue = semi.IdMarca;

            cmbModelo.DataSource = modelos;
            cmbModelo.DisplayMember = "NombreModelo";
            cmbModelo.ValueMember = "IdModelo";
            cmbModelo.SelectedValue = semi.IdModelo;

            txtTipoCarga.DataSource = tiposCarga;
            txtTipoCarga.DisplayMember = "Descripcion";
            txtTipoCarga.ValueMember = "IdTipoCarga";
            txtTipoCarga.SelectedValue = semi.IdTipoCarga;

            cmbMaterial.DataSource = materiales;
            cmbMaterial.DisplayMember = "Descripcion";
            cmbMaterial.ValueMember = "IdMaterial";
            cmbMaterial.SelectedValue = semi.IdMaterial;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarCambios();
            Close();
        }

        private async void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedValue is int idMarca)
            {
                await _presenter.CargarModelos(idMarca);
            }
        }

        public void CargarModelos(List<VehiculoModelo> modelos)
        {
            cmbModelo.DataSource = modelos;
            cmbModelo.DisplayMember = "NombreModelo";
            cmbModelo.ValueMember = "IdModelo";
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