using Servicios.Presenters;
using Shared.Models;

namespace Servicios.Views.RequerimientosDeCompra
{
    public partial class AgregarImputacionDependenciaForm : DevExpress.XtraEditors.XtraForm, IAgregarImputacionDependenciaView
    {
        private readonly AgregarImputacionDependenciaPresenter _presenter;

        public AgregarImputacionDependenciaForm(AgregarImputacionDependenciaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async void Inicializar(int PorcentajeActual)
        {
            await _presenter.CargarDependenciasAsync();
            int numeroRestante = 100 - PorcentajeActual;
            numPorcentaje.Maximum = numeroRestante;
            numPorcentaje.Value = numeroRestante;
        }

        public void CargarDependencias(List<Dependencia> dependencias)
        {
            lstDependencias.DataSource = dependencias;
            lstDependencias.DisplayMember = "Descripcion";
            lstDependencias.ValueMember = "IdDependencia";
        }

        public void CargarImputaciones(List<Imputacion> imputaciones)
        {
            lstImputaciones.DataSource = imputaciones;
            lstImputaciones.DisplayMember = "Descripcion";
            lstImputaciones.ValueMember = "IdImputacion";
        }

        public int Porcentaje => (int)numPorcentaje.Value;

        private void lstDependencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDependencias.SelectedItem is Dependencia dependenciaSeleccionada)
            {
                // Llama al presenter pasando el IdDependencia
                _presenter.CargarImputacionesPorDependencia(dependenciaSeleccionada.IdDependencia);
            }
            else
            {
                MostrarMensaje("Por favor, seleccione una dependencia válida.");
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bConfirmar_Click(object sender, EventArgs e)
        {
            if (lstImputaciones.SelectedItem is Imputacion imputacionSeleccionada && lstDependencias.SelectedItem is Dependencia dependenciaSeleccionada)
            {
                this.Hide();
                // Combinar descripciones
                var descripcionCompleta = $"{dependenciaSeleccionada.Descripcion} - {imputacionSeleccionada.Descripcion}";

                // Validar porcentaje
                var porcentaje = Porcentaje;
                if (porcentaje <= 0 || porcentaje > 100)
                {
                    MostrarMensaje("El porcentaje debe estar entre 1 y 100.");
                    return;
                }

                // Llamar al formulario anterior
                _presenter.GuardarImputacionDependencia(descripcionCompleta, imputacionSeleccionada.IdImputacion, porcentaje);
            }
            else
            {
                MostrarMensaje("Debe seleccionar una dependencia y una imputación antes de guardar.");
            }
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}