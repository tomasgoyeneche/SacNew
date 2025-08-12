using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Shared.Models;

namespace Servicios.Views
{
    public partial class AgregarEditarVaporizadosExternosForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarVaporizadoExternoView
    {
        public readonly AgregarEditarVaporizadoExternoPresenter _presenter;

        public AgregarEditarVaporizadosExternosForm(AgregarEditarVaporizadoExternoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // Eventos para calcular el tiempo
            dtpInicio.EditValueChanged += (s, e) => _presenter.CalcularTiempoVaporizado();
            dtpFin.EditValueChanged += (s, e) => _presenter.CalcularTiempoVaporizado();
        }

        // Interfaz
        public void MostrarDatos(string texto)
        {
            lblDatos.Text = texto;
        }

        public void CargarDatos(Vaporizado vaporizado, int idUnidad)
        {
            txtCisterna.Text = vaporizado.CantidadCisternas.ToString();
            cmbMotivo.EditValue = vaporizado.IdVaporizadoMotivo;
            cmbUnidad.EditValue = idUnidad;
            dtpInicio.EditValue = vaporizado.FechaInicio;
            dtpFin.EditValue = vaporizado.FechaFin;
            txtNroCertificado.Text = vaporizado.NroCertificado;
            txtNroDanes.Text = vaporizado.RemitoDanes;
            txtObservaciones.Text = vaporizado.Observaciones;
        }

        public void CargarMotivos(List<VaporizadoMotivo> motivos)
        {
            cmbMotivo.Properties.DataSource = motivos;
            cmbMotivo.Properties.DisplayMember = "Descripcion";
            cmbMotivo.Properties.ValueMember = "IdVaporizadoMotivo";
            cmbMotivo.Properties.Columns.Clear();
            cmbMotivo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Motivo"));
        }

        public void CargarUnidades(List<UnidadDto> unidades)
        {
            cmbUnidad.Properties.DataSource = unidades;
            cmbUnidad.Properties.DisplayMember = "PatenteCompleta";
            cmbUnidad.Properties.ValueMember = "IdUnidad";
            cmbUnidad.Properties.Columns.Clear();
            cmbUnidad.Properties.Columns.Add(new LookUpColumnInfo("PatenteCompleta", "Unidad"));
        }


        // Propiedades de entrada/salida
        public int CantidadCisternas => int.TryParse(txtCisterna.Text, out int n) ? n : 0;

        public int? IdMotivo => cmbMotivo.EditValue is int v ? v : (int?)null;
        public int? IdUnidad => cmbUnidad.EditValue is int v ? v : (int?)null;

        public DateTime? FechaInicio => dtpInicio.EditValue as DateTime?;
        public DateTime? FechaFin => dtpFin.EditValue as DateTime?;

        public string TiempoVaporizado
        {
            get => txtTiempoVaporizado.Text;
            set => txtTiempoVaporizado.Text = value;
        }

        public string NroCertificado => txtNroCertificado.Text.Trim();
        public string RemitoDanes => txtNroDanes.Text.Trim();
        public string Observaciones => txtObservaciones.Text.Trim();

        public void SetTiempoVaporizado(string tiempo)
        {
            txtTiempoVaporizado.Text = tiempo;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar()
        {
            this.Close();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }
    }
}