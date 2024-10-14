using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Views.GestionFlota.Postas.ABMPostas
{
    public partial class AgregarEditarPosta : Form, IAgregarEditarPostaView
    {
        private readonly ISesionService _sesionService;
        private readonly AgregarEditarPostaPresenter _presenter;

        public AgregarEditarPosta(ISesionService sesionService, IPostaRepositorio postaRepositorio, IProvinciaRepositorio provinciaRepositorio)
        {
            InitializeComponent();

            _sesionService = sesionService;
            _presenter = new AgregarEditarPostaPresenter(this, postaRepositorio, provinciaRepositorio);
            _presenter.CargarProvincias();
            _presenter.CargarProvincias();
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

        // Método para cargar datos de una posta existente
        public void CargarDatos(Posta posta)
        {
            _presenter.CargarDatosPosta(posta);
        }

        // Evento del botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarPosta();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}