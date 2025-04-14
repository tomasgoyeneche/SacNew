using GestionOperativa.Presenters.AdministracionDocumental.Altas;
using Guna.UI2.WinForms;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class ModificarVencimientosForm : DevExpress.XtraEditors.XtraForm, IModificarVencimientosView
    {
        public readonly EditarVencimientosPresenter _presenter;

        // Map IDVencimiento ↔ (Label, DateTimePicker)
        private readonly Dictionary<int, (Guna2HtmlLabel label, Guna2DateTimePicker picker)> _controles;

        public ModificarVencimientosForm(EditarVencimientosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            _controles = new Dictionary<int, (Guna2HtmlLabel, Guna2DateTimePicker)>
        {
            { 1, (lblVencimiento1, dtpVencimiento1) }, // Ej: Licencia / MasYPF
            { 2, (lblVencimiento2, dtpVencimiento2) }, // Ej: Psicofísico Apto / VTV / Calibrado
            { 3, (lblVencimiento3, dtpVencimiento3) }, // Ej: Psicofísico Curso / Checklist
            { 4, (lblVencimiento4, dtpVencimiento4) }, // Ej: Examen Anual
            { 5, (lblVencimiento5, dtpVencimiento5) },  // Ej: Espesor / Visual / Estanqueidad
             { 6, (lblVencimiento6, dtpVencimiento6) } , // Ej: Espesor / Visual / Estanqueidad
             { 7, (lblVencimiento7, dtpVencimiento7) } , // Ej: Espesor / Visual / Estanqueidad
             { 8, (lblVencimiento8, dtpVencimiento8) } , // Ej: Espesor / Visual / Estanqueidad
        };
        }

        public async Task InicializarAsync(string entidad, int idEntidad)
        {
            await _presenter.InicializarAsync(entidad, idEntidad);
        }

        public void MostrarVencimientos(Dictionary<int, (string etiqueta, DateTime? fecha)> vencimientos)
        {
            // Ocultar todos
            foreach (var (label, picker) in _controles.Values)
            {
                label.Visible = false;
                picker.Visible = false;
            }

            // Mostrar solo los que correspondan
            foreach (var (idVencimiento, (texto, fecha)) in vencimientos)
            {
                if (_controles.TryGetValue(idVencimiento, out var control))
                {
                    control.label.Text = texto + ":";
                    control.label.Visible = true;

                    control.picker.Value = fecha ?? DateTime.Today;
                    control.picker.Visible = true;
                }
            }
        }

        public Dictionary<int, DateTime?> ObtenerFechasActualizadas()
        {
            return _controles
                .Where(kvp => kvp.Value.picker.Visible)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => (DateTime?)kvp.Value.picker.Value.Date
                );
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarCambiosAsync();
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}