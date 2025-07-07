using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionDocumental.Presenters;
using Shared.Models;

namespace GestionDocumental.Views
{
    public partial class AgregarEditarNovedadChoferForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarNovedadChoferView
    {
        public readonly AgregarEditarNovedadChoferPresenter _presenter;

        public AgregarEditarNovedadChoferForm(AgregarEditarNovedadChoferPresenter presenter)
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

        public int IdChofer => Convert.ToInt32(cmbChofer.EditValue);

        public int IdEstado => Convert.ToInt32(cmbEstado.EditValue);

        public DateTime FechaInicio => dtpFechaInicio.Value;

        public DateTime FechaFin => dtpFechaFinal.Value;

        public string Observaciones => txtObservaciones.Text.Trim();

        public bool Disponible => dispoCheck.Checked;

        public void CargarChoferes(List<Chofer> choferes)
        {
            cmbChofer.Properties.DataSource = choferes;
            cmbChofer.Properties.DisplayMember = "NombreApellido";
            cmbChofer.Properties.ValueMember = "IdChofer";

            cmbChofer.Properties.Columns.Clear();
            cmbChofer.Properties.Columns.Add(new LookUpColumnInfo("NombreApellido", "Chofer"));

            dtpFechaFinal.Value = DateTime.Now.AddDays(1);
            dtpFechaInicio.Value = DateTime.Now;
            cmbChofer.EditValue = _presenter.NovedadActual?.idChofer ?? -1;
        }

        public void MostrarMantenimientosUnidad(string texto)
        {
            lblMantenimientosUnidad.Text = texto; // Asegurate de tener un label llamado así, o poné el nombre que uses
        }

        public void CargarEstados(List<ChoferTipoEstado> estados)
        {
            cmbEstado.Properties.DataSource = estados;
            cmbEstado.Properties.DisplayMember = "Descripcion";
            cmbEstado.Properties.ValueMember = "IdEstado";

            cmbEstado.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbEstado.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Estado"));

            cmbEstado.EditValue = _presenter.NovedadActual?.idEstado ?? -1;
        }

        public void MostrarDatosNovedad(NovedadesChoferesDto novedadesChoferes)
        {
            txtObservaciones.Text = novedadesChoferes.Observaciones;
            cmbChofer.EditValue = novedadesChoferes.idChofer;
            cmbEstado.EditValue = novedadesChoferes.idEstado;

            if (novedadesChoferes.Disponible == "SI")
            {
                dispoCheck.Checked = true;
            }
            else
            {
                dispoCheck.Checked = false;
            }
            dtpFechaInicio.Value = novedadesChoferes.FechaInicio;
            dtpFechaFinal.Value = novedadesChoferes.FechaFin;
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

        private async void cmbChofer_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbChofer.EditValue?.ToString(), out int idChofer) && idChofer > 0)
            {
                await _presenter.MostrarMantenimientosUnidadDelChoferAsync(idChofer);
            }
            else
            {
                await _presenter.MostrarMantenimientosUnidadDelChoferAsync(0); // Limpiar si no hay chofer
            }
        }
    }
}