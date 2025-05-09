using Configuraciones.Presenters;
using Configuraciones.Views.AbmUsuarios;
using SacNew.Presenters;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public partial class PermisosUsuarioForm : Form, IPermisosUsuarioView
    {
        public readonly PermisosUsuarioPresenter _presenter;

        public PermisosUsuarioForm(PermisosUsuarioPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }


        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CargarPermisosDisponibles(IEnumerable<Permiso> permisos)
        {
            lstPermisosDisponibles.DataSource = permisos.ToList();
            lstPermisosDisponibles.DisplayMember = "NombrePermiso";
            lstPermisosDisponibles.ValueMember = "IdPermiso";
        }

        public void CargarPermisosAsignados(IEnumerable<Permiso> permisos)
        {
            lstPermisosAsignados.DataSource = permisos.ToList();
            lstPermisosAsignados.DisplayMember = "NombrePermiso";
            lstPermisosAsignados.ValueMember = "IdPermiso";
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarPermisoAsync();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await _presenter.EliminarPermisoAsync();
        }

        public Permiso? PermisoSeleccionadoDisponible =>
            lstPermisosDisponibles.SelectedItem as Permiso;

        public Permiso? PermisoSeleccionadoAsignado =>
            lstPermisosAsignados.SelectedItem as Permiso;
    }
}