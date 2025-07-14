using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class CambioChoferForm : DevExpress.XtraEditors.XtraForm, ICambioChoferView
    {
        public readonly CambioChoferPresenter _presenter;

        public CambioChoferForm(CambioChoferPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            dateEditFechaCambio.EditValue = DateTime.Now;
            gridViewChoferes.FocusedRowChanged += async (s, e) => await _presenter.ChoferSeleccionadoCambioAsync();
            bBajarChofer.Click += async (s, e) => await _presenter.BajarChoferAsync();
        }

        public void CargarChoferes(List<Chofer> choferes)
        {
            gridControlChoferes.DataSource = choferes;
            gridViewChoferes.BestFitColumns();
        }

        public void CargarFrancos(List<NovedadesChoferesDto> francos)
        {
            gridControlNovedades.DataSource = francos;
            gridViewNovedades.BestFitColumns();
        }

        public int? IdChoferSeleccionado
        {
            get
            {
                if (gridViewChoferes.GetFocusedRow() is ChoferDto ch)
                    return ch.IdChofer;
                return null;
            }
        }

        public string NombreChoferSeleccionado
        {
            get
            {
                if (gridViewChoferes.GetFocusedRow() is ChoferDto ch)
                    return ch.Nombre;
                return null;
            }
        }

        public string Observacion => txtObservacion.Text.Trim();

        public DateTime FechaCambio => dateEditFechaCambio.EditValue as DateTime? ?? DateTime.Today;

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar() => Close();

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private async void bGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.ConfirmarCambioChoferAsync();
        }
    }
}