using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionDocumental.Presenters.Novedades;
using GestionDocumental.Views.Novedades.NovedadesUnidades;
using Shared.Models;

namespace GestionDocumental.Views.Novedades
{
    public partial class AgregarEditarNovedadUnidadForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarNovedadUnidadView
    {
        public readonly AgregarEditarNovedadUnidadPresenter _presenter;

        public AgregarEditarNovedadUnidadForm(AgregarEditarNovedadUnidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int IdUnidad => Convert.ToInt32(cmbUnidad.EditValue);

        public int IdMantenimientoEstado => Convert.ToInt32(cmbEstado.EditValue);

        public DateTime FechaInicio => dtpFechaInicio.Value;

        public DateTime FechaFin => dtpFechaFinal.Value;

        public string Observaciones => txtObservaciones.Text.Trim();

        public int Odometro => Convert.ToInt32(txtOdometro.Text);

        public void MostrarAusenciasChofer(string texto)
        {
            lblAusenciasChofer.Text = texto; // Asegurate de tener un label llamado así, o poné el nombre que uses
        }

        public void CargarUnidades(List<UnidadDto> unidades)
        {
            cmbUnidad.Properties.DataSource = unidades;
            cmbUnidad.Properties.DisplayMember = "PatenteCompleta";
            cmbUnidad.Properties.ValueMember = "IdUnidad";

            cmbUnidad.Properties.Columns.Clear();
            cmbUnidad.Properties.Columns.Add(new LookUpColumnInfo("PatenteCompleta", "Unidad"));

            dtpFechaFinal.Value = DateTime.Now.AddDays(1);
            dtpFechaInicio.Value = DateTime.Now;
            cmbUnidad.EditValue = _presenter.NovedadActual?.idUnidad ?? -1;
        }

        public void CargarEstados(List<UnidadMantenimientoEstado> estados)
        {
            cmbEstado.Properties.DataSource = estados;
            cmbEstado.Properties.DisplayMember = "Descripcion";
            cmbEstado.Properties.ValueMember = "IdMantenimientoEstado";

            cmbEstado.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbEstado.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Estado"));

            cmbEstado.EditValue = _presenter.NovedadActual?.idMantenimientoEstado ?? -1;
        }

        public void MostrarDatosNovedad(UnidadMantenimientoDto novedadesUnidades)
        {
            txtObservaciones.Text = novedadesUnidades.Observaciones;
            cmbUnidad.EditValue = novedadesUnidades.idUnidad;
            cmbEstado.EditValue = novedadesUnidades.idMantenimientoEstado;

            txtOdometro.Text = novedadesUnidades.Odometro.ToString();

            dtpFechaInicio.Value = novedadesUnidades.FechaInicio;
            dtpFechaFinal.Value = novedadesUnidades.FechaFin;
            _presenter.CalcularAusencia();
        }

        public void Close()
        {
            Dispose();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarDiasAusente(int dias)
        {
            lblDiasAusente.Text = $"Días ausente: {dias}";
        }

        public void MostrarFechaReincorporacion(DateTime fecha)
        {
            lblReincorporacion.Text = $"Reincorporación: {fecha:dd/MM/yyyy}";
        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            _presenter.CalcularAusencia();
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            _presenter.CalcularAusencia();
        }

        private async void cmbUnidad_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbUnidad.EditValue?.ToString(), out int idUnidad) && idUnidad > 0)
            {
                await _presenter.MostrarAusenciasDelChoferAsync(idUnidad);
            }
            else
            {
                await _presenter.MostrarAusenciasDelChoferAsync(0); // Limpiar si no hay chofer
            }
        }
    }
}