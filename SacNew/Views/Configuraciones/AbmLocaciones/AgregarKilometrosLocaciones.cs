using SacNew.Models;
using SacNew.Presenters;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public partial class AgregarKilometrosLocaciones : Form, IAgregarKilometrosView
    {
        public AgregarKilometrosPresenter _presenter;

        public AgregarKilometrosLocaciones(AgregarKilometrosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdLocacionDestino => (int)cmbLocacionDestino.SelectedValue;

        public decimal Kilometros => decimal.TryParse(txtKilometros.Text, out var value) ? value : 0;

        public void CargarLocaciones(List<Locacion> locaciones)
        {
            cmbLocacionDestino.DataSource = locaciones;
            cmbLocacionDestino.DisplayMember = "Nombre";
            cmbLocacionDestino.ValueMember = "IdLocacion";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                await _presenter.GuardarKilometrosAsync();
                Close();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar kilómetros: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}