using SacNew.Models;
using SacNew.Models.DTOs;
using SacNew.Presenters;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;

namespace SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.IngresoManual
{
    public partial class IngresoManualYPF : Form, IIngresoManualYPFView
    {
        public readonly IngresoManualYPFPresenter _presenter;

        public IngresoManualYPF(IngresoManualYPFPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public DateTime FechaVale => dtpFechaVale.Value;
        public int TipoGasoilSeleccionado => (int)cmbTipoGasoil.SelectedValue;

        public List<TicketInfo> Tickets
        {
            get
            {
                return new List<TicketInfo>
            {
                new TicketInfo(txtTicket1.Text, txtLitros1.Text, txtAclaracion1.Text),
                new TicketInfo(txtTicket2.Text, txtLitros2.Text, txtAclaracion2.Text),
                new TicketInfo(txtTicket3.Text, txtLitros3.Text, txtAclaracion3.Text),
                new TicketInfo(txtTicket4.Text, txtLitros4.Text, txtAclaracion4.Text),
                new TicketInfo(txtTicket5.Text, txtLitros5.Text, txtAclaracion5.Text),
            };
            }
        }

        public TicketInfo TicketUrea =>
            new TicketInfo(txtTicketUrea.Text, txtLitrosUrea.Text, txtAclaracionUrea.Text);

        public void CargarTiposGasoil(List<Concepto> tiposGasoil)
        {
            cmbTipoGasoil.DataSource = tiposGasoil;
            cmbTipoGasoil.DisplayMember = "Descripcion";
            cmbTipoGasoil.ValueMember = "IdConsumo";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumoAsync();
        }
    }
}