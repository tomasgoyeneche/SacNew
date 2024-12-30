using SacNew.Presenters;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public partial class AgregarEditarPool : Form, IAgregarEditarLocacionPoolView
    {
        public readonly AgregarEditarLocacionPoolPresenter _presenter;

        public AgregarEditarPool(AgregarEditarLocacionPoolPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CargarLocacionesDisponibles(IEnumerable<Locacion> locaciones)
        {
            lstLocacionesDisponibles.DataSource = locaciones.ToList();
            lstLocacionesDisponibles.DisplayMember = "Nombre";
            lstLocacionesDisponibles.ValueMember = "IdLocacion";
        }

        public void CargarLocacionesAsignadas(IEnumerable<Locacion> locaciones)
        {
            lstLocacionesAsignadas.DataSource = locaciones.ToList();
            lstLocacionesAsignadas.DisplayMember = "Nombre";
            lstLocacionesAsignadas.ValueMember = "IdLocacion";
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarLocacionAlPoolAsync();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await _presenter.EliminarLocacionDelPoolAsync();
        }

        public Locacion? LocacionSeleccionadaDisponible =>
            lstLocacionesDisponibles.SelectedItem as Locacion;

        public Locacion? LocacionSeleccionadaAsignada =>
            lstLocacionesAsignadas.SelectedItem as Locacion;
    }
}