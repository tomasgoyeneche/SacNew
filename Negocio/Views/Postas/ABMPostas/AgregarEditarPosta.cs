using GestionFlota.Presenters;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.ABMPostas
{
    public partial class AgregarEditarPosta : Form, IAgregarEditarPostaView
    {
        public AgregarEditarPostaPresenter _presenter;

        public AgregarEditarPosta(AgregarEditarPostaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int Id { get; set; }

        public string Codigo
        {
            get => txtCodigo.Text.Trim();
            set => txtCodigo.Text = value;
        }

        public string Descripcion
        {
            get => txtDescripcion.Text.Trim();
            set => txtDescripcion.Text = value;
        }

        public string Direccion
        {
            get => txtDireccion.Text.Trim();
            set => txtDireccion.Text = value;
        }

        public int ProvinciaId
        {
            get => Convert.ToInt32(cmbProvincia.SelectedValue);
            set => cmbProvincia.SelectedValue = value;
        }

        public void CargarProvincias(List<Provincia> provincias)
        {
            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "NombreProvincia";
            cmbProvincia.ValueMember = "IdProvincia";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public async Task CargarDatosAsync(Posta posta)
        {
            await _presenter.CargarProvinciasAsync();
            _presenter.CargarDatosPosta(posta);
        }

        // Evento del botón Guardar
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarPostaAsync();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}