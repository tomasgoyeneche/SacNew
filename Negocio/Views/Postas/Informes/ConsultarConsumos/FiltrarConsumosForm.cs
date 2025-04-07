using GestionFlota.Presenters.Informes;
using GestionFlota.Views.Postas.Informes.ConsultarConsumos;
using Shared.Models;
using Shared.Models.DTOs;

namespace GestionFlota.Views.Postas.Modificaciones.ConsultarConsumos
{
    public partial class FiltrarConsumosForm : Form, IBuscarConsumosView
    {
        private readonly BuscarConsumosPresenter _presenter;

        public FiltrarConsumosForm(BuscarConsumosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void BuscarConsumosForm_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarConsumosAsync();
        }

        public int? IdConcepto => cmbConcepto.SelectedValue as int?;
        public int? IdPosta => cmbPosta.SelectedValue as int?;
        public int? IdEmpresa => cmbEmpresa.SelectedValue as int?;
        public int? IdUnidad => cmbUnidad.SelectedValue as int?;
        public int? IdChofer => cmbChofer.SelectedValue as int?;
        public string NumeroPoc => txtNumeroPoc.Text.Trim();
        public string Estado => rbTodas.Checked ? "Todas" : rbAbiertas.Checked ? "Abierta" : "Cerrada";
        public DateTime? FechaCreacionDesde => dtpCreacionDesde.Value.Date;
        public DateTime? FechaCreacionHasta => dtpCreacionHasta.Value.Date;
        public DateTime? FechaCierreDesde => checkBoxSinCerrar.Checked ? null : dtpCierreDesde.Value.Date;
        public DateTime? FechaCierreHasta => checkBoxSinCerrar.Checked ? null : dtpCierreHasta.Value.Date;

        public void CargarConceptos(List<Concepto> conceptos)
        {
            cmbConcepto.DataSource = conceptos;
            cmbConcepto.DisplayMember = "Codigo";
            cmbConcepto.ValueMember = "IdConsumo";
            cmbConcepto.SelectedIndex = -1;

            dtpCreacionHasta.Value = DateTime.Today.AddDays(1);
            dtpCierreHasta.Value = DateTime.Today.AddDays(1);
        }

        public void CargarPostas(List<Posta> postas)
        {
            cmbPosta.DataSource = postas;
            cmbPosta.DisplayMember = "Codigo";
            cmbPosta.ValueMember = "IdPosta";
            cmbPosta.SelectedIndex = -1;
        }

        public void CargarEmpresas(List<EmpresaDto> empresas)
        {
            cmbEmpresa.DataSource = empresas;
            cmbEmpresa.DisplayMember = "NombreFantasia";
            cmbEmpresa.ValueMember = "IdEmpresa";
            cmbEmpresa.SelectedIndex = -1;
        }

        public void CargarUnidades(List<UnidadPatenteDto> unidades)
        {
            cmbUnidad.DataSource = unidades;
            cmbUnidad.DisplayMember = "DescripcionUnidad";
            cmbUnidad.ValueMember = "IdUnidad";
            cmbUnidad.SelectedIndex = -1;
        }

        public void CargarChoferes(List<Chofer> choferes)
        {
            cmbChofer.DataSource = choferes;
            cmbChofer.DisplayMember = "NombreApellido";
            cmbChofer.ValueMember = "IdChofer";
            cmbChofer.SelectedIndex = -1;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}