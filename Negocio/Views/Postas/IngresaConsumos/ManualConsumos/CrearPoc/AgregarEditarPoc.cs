using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters;
using Shared.Models;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc
{
    public partial class AgregarEditarPoc : Form, IAgregarEditarPOCView
    {
        public readonly AgregarEditarPOCPresenter _presenter;

        public AgregarEditarPoc(AgregarEditarPOCPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public int IdUnidad => Convert.ToInt32(cmbNomina.EditValue);
        public int IdChofer => Convert.ToInt32(cmbChofer.EditValue);
        public int IdPeriodo => Convert.ToInt32(cmbPeriodo.SelectedValue);
        public string NumeroPOC => txtNumeroPOC.Text.Trim();
        public double Odometro => string.IsNullOrEmpty(txtOdometro.Text) ? 0 : Convert.ToDouble(txtOdometro.Text.Trim());
        public string Comentario => txtComentario.Text.Trim();
        public DateTime FechaCreacion => dtpFechaCreacion.Value;
        public int IdUsuario => _presenter.IdUsuario;

        public void CargarNominas(List<UnidadDto> unidades)
        {
            cmbNomina.Properties.DataSource = unidades;
            cmbNomina.Properties.DisplayMember = "Tractor_Patente";
            cmbNomina.Properties.ValueMember = "IdUnidad";

            cmbNomina.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbNomina.Properties.Columns.Add(new LookUpColumnInfo("Tractor_Patente", "Unidad"));

            cmbNomina.EditValue = _presenter.PocActual?.IdUnidad ?? -1;
        }

        public void CargarChoferes(List<Chofer> choferes)
        {
            cmbChofer.Properties.DataSource = choferes;
            cmbChofer.Properties.DisplayMember = "NombreApellido";
            cmbChofer.Properties.ValueMember = "IdChofer";

            cmbChofer.Properties.Columns.Clear();
            cmbChofer.Properties.Columns.Add(new LookUpColumnInfo("NombreApellido", "Chofer"));

            cmbChofer.EditValue = _presenter.PocActual?.IdChofer ?? -1;
        }

        public void CargarPeriodo(List<Periodo> periodos)
        {
            cmbPeriodo.DataSource = periodos;
            cmbPeriodo.DisplayMember = "NombrePeriodo";
            cmbPeriodo.ValueMember = "idPeriodo";

            var fechaActual = DateTime.Today;
            int mesActual = fechaActual.Month;
            int anioActual = fechaActual.Year;
            int quincenaActual = fechaActual.Day <= 15 ? 1 : 2;

            var periodoActual = periodos.FirstOrDefault(p => p.Anio == anioActual && p.Mes == mesActual && p.Quincena == quincenaActual);

            if (periodoActual != null)
            {
                cmbPeriodo.SelectedValue = periodoActual.IdPeriodo;
            }

            if (_presenter.PocActual == null)
            {
                dtpFechaCreacion.Value = DateTime.Now;
            }
            else
            {
                cmbPeriodo.SelectedValue = _presenter.PocActual?.IdPeriodo ?? -1;
            }
        }

        public void MostrarDatosPOC(POC poc)
        {
            txtNumeroPOC.Text = poc.NumeroPoc;
            txtOdometro.Text = poc.Odometro.ToString();
            txtComentario.Text = poc.Comentario;
            cmbNomina.EditValue = poc.IdUnidad;
            cmbPeriodo.SelectedValue = poc.IdPeriodo;
            dtpFechaCreacion.Value = poc.FechaCreacion;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarPOCAsync();
        }

        private async void AgregarEditarPOC_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void MostrarMensaje(string mensaje)
        {
            DialogoMensaje.Show(mensaje);
        }

        public void Close()
        {
            Dispose();
        }

        private void txtOdometro_TextChanged(object sender, EventArgs e)
        {
            txtOdometro.TextChanged -= txtOdometro_TextChanged;
            if (txtOdometro.Text.Contains('.'))
            {
                int selectionStart = txtOdometro.SelectionStart;
                txtOdometro.Text = txtOdometro.Text.Replace('.', ',');
                txtOdometro.SelectionStart = selectionStart;
            }

            txtOdometro.TextChanged += txtOdometro_TextChanged;
        }
    }
}