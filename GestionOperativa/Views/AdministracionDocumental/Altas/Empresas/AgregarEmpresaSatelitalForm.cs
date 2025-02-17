using GestionOperativa.Presenters;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public partial class AgregarEmpresaSatelitalForm : Form, IAgregarEmpresaSatelitalView
    {
        public readonly AgregarEmpresaSatelitalPresenter _presenter;

        public AgregarEmpresaSatelitalForm(AgregarEmpresaSatelitalPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdEmpresa { get; private set; }
        public int IdSatelital => (int)cmbSatelital.SelectedValue;
        public string Usuario => txtUsuario.Text;
        public string Clave => txtClave.Text;

        public void CargarSatelitales(List<Satelital> satelitales)
        {
            cmbSatelital.DataSource = satelitales;
            cmbSatelital.DisplayMember = "Descripcion";
            cmbSatelital.ValueMember = "IdSatelital";
        }

        public void Inicializar(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarEmpresaSatelital();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}