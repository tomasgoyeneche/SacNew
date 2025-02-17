using GestionOperativa.Presenters.Empresas;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public partial class ModificarDatosSeguroForm : Form, IModificarDatosSeguroView
    {
        public readonly ModificarDatosSeguroPresenter _presenter;

        public ModificarDatosSeguroForm(ModificarDatosSeguroPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdSeguroEmpresa { get; private set; }
        public int IdEmpresa { get; private set; }
        public int IdCia => (int)cmbCia.SelectedValue;
        public int IdCobertura => (int)cmbCobertura.SelectedValue;
        public string NumeroPoliza => txtNumeroPoliza.Text;
        public DateTime VigenciaHasta => dtpVigenciaHasta.Value;
        public DateTime PagoDesde => dtpPagoDesde.Value;
        public DateTime PagoHasta => dtpPagoHasta.Value;

        public void CargarDatosSeguro(EmpresaSeguro seguro, List<Cia> cias, List<Cobertura> coberturas)
        {
            IdSeguroEmpresa = seguro.IdSeguroEmpresa;
            IdEmpresa = seguro.IdEmpresa;

            cmbCia.DataSource = cias;
            cmbCia.DisplayMember = "NombreFantasia";
            cmbCia.ValueMember = "IdCia";
            cmbCia.SelectedValue = seguro.IdCia;

            cmbCobertura.DataSource = coberturas;
            cmbCobertura.DisplayMember = "TipoCobertura";
            cmbCobertura.ValueMember = "IdCobertura";
            cmbCobertura.SelectedValue = seguro.IdCobertura;

            txtNumeroPoliza.Text = seguro.NumeroPoliza;
            dtpVigenciaHasta.Value = seguro.VigenciaHasta;
            dtpPagoDesde.Value = seguro.PagoDesde;
            dtpPagoHasta.Value = seguro.PagoHasta;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarCambios();
            Close();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}