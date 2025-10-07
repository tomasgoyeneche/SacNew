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

           
            gridViewChoferes.FocusedRowChanged += async (s, e) => await _presenter.ChoferSeleccionadoCambioAsync();
        }

        public void CargarChoferes(List<Chofer> choferes)
        {
            gridControlChoferes.DataSource = choferes;
            gridViewChoferes.BestFitColumns();
            dateEditFechaCambio.EditValue = DateTime.Now;
            bGuardar.Enabled = true;
            bBajarChofer.Enabled = true;
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
                if (gridViewChoferes.GetFocusedRow() is Chofer ch)
                    return ch.IdChofer;
                return null;
            }
        }

        public string NombreChoferSeleccionado
        {
            get
            {
                if (gridViewChoferes.GetFocusedRow() is Chofer ch)
                    return ch.NombreApellido;
                return null;
            }
        }
        public string Observacion => txtObservacion.Text.Trim();

        public DateTime FechaCambio => dateEditFechaCambio.EditValue as DateTime? ?? DateTime.Today;

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar() => Dispose();

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private async void bGuardar_Click(object sender, EventArgs e)
        {
            bGuardar.Enabled = false;
            await _presenter.ConfirmarCambioChoferAsync();
        }

        private async void bBajarChofer_Click(object sender, EventArgs e)
        {
            bBajarChofer.Enabled = false;
            await _presenter.BajarChoferAsync();
        }
    }
}