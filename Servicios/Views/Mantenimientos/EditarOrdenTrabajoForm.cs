using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios.Views.Mantenimientos
{
    public partial class EditarOrdenTrabajoForm : DevExpress.XtraEditors.XtraForm, IEditarOrdenTrabajoView
    {
        public readonly EditarOrdenTrabajoPresenter _presenter;

        public EditarOrdenTrabajoForm(EditarOrdenTrabajoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        // =====================================================
        // 🧩 Propiedades
        // =====================================================

        public int IdOrdenTrabajo
        {
            get => Convert.ToInt32(txtIdOrden.Text);
            set => txtIdOrden.Text = value.ToString();
        }

        public DateTime FechaEmision
        {
            get => (DateTime)dateFechaEmision.EditValue;
            set => dateFechaEmision.EditValue = value;
        }

        public int? IdUnidad
        {
            get => Convert.ToInt32(cmbUnidad.EditValue == null ? 0 : cmbUnidad.EditValue);
            set => cmbUnidad.EditValue = value;
        }

        public int? IdNomina { get; set; }

        public int? IdLugarReparacion
        {
            get => Convert.ToInt32(cmbLugarMantenimiento.EditValue == null ? 0 : cmbLugarMantenimiento.EditValue);
            set => cmbLugarMantenimiento.EditValue = value;
        }

        public DateTime? FechaIngreso
        {
            get => dtpFechaIngreso.EditValue as DateTime?;
            set => dtpFechaIngreso.EditValue = value;
        }

        public DateTime? FechaFin
        {
            get => dtpFechaFin.EditValue as DateTime?;
            set => dtpFechaFin.EditValue = value;
        }

        public decimal? OdometroIngreso
        {
            get => Convert.ToDecimal(txtOdometroIng.EditValue ?? 0);
            set => txtOdometroIng.EditValue = value;
        }

        public decimal? OdometroSalida
        {
            get => Convert.ToDecimal(txtOdometroFin.EditValue ?? 0);
            set => txtOdometroFin.EditValue = value;
        }

        public string Observaciones
        {
            get => txtDescripcion.Text.Trim();
            set => txtDescripcion.Text = value;
        }

        // =====================================================
        // 🧩 Carga de combos
        // =====================================================

        public void CargarUnidades(List<UnidadDto> unidades)
        {
            cmbUnidad.Properties.DataSource = unidades;
            cmbUnidad.Properties.DisplayMember = "PatenteCompleta";
            cmbUnidad.Properties.ValueMember = "IdUnidad";
            cmbUnidad.Properties.NullText = "Seleccione Unidad...";
            cmbUnidad.Properties.Columns.Clear();
            cmbUnidad.Properties.Columns.Add(new LookUpColumnInfo("PatenteCompleta", "Unidad"));
        }

        public void CargarLugares(List<LugarReparacion> lugares)
        {
            cmbLugarMantenimiento.Properties.DataSource = lugares;
            cmbLugarMantenimiento.Properties.DisplayMember = "Nombre";
            cmbLugarMantenimiento.Properties.ValueMember = "IdLugarReparacion";
            cmbLugarMantenimiento.Properties.NullText = "Seleccione...";
            cmbLugarMantenimiento.Properties.Columns.Clear();
            cmbLugarMantenimiento.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Lugar"));
        }

        public void CargarComprobantes(List<OrdenTrabajoComprobante> comprobantes)
        {
            gridControlComprobantes.DataSource = comprobantes;
            gridViewComprobantes.BestFitColumns();
        }
        // =====================================================
        // 🧩 Control de botones
        // =====================================================

        public void ActualizarEstadoUI(int fase)
        {
            // 🔹 Reiniciar todo
            cmbUnidad.Enabled = false;
            btnAutorizar.Enabled = false;
            cmbLugarMantenimiento.Enabled = false;
            btnIngreso.Enabled = false;
            dtpFechaIngreso.Enabled = false;
            txtOdometroIng.Enabled = false;
            bFinalizo.Enabled = false;
            dtpFechaFin.Enabled = false;
            txtOdometroFin.Enabled = false;

            switch (fase)
            {
                case 0: // Creada → solo se puede autorizar
                    cmbUnidad.Enabled = true;
                    btnAutorizar.Enabled = true;
                    break;

                case 1: // Autorizada → habilita lugar y botón de ingreso
                    cmbLugarMantenimiento.Enabled = true;
                    btnIngreso.Enabled = true;
                    break;

                case 2: // En Taller → habilita odómetro ingreso y botón de finalización
                    dtpFechaIngreso.Enabled = true;
                    txtOdometroIng.Enabled = true;
                    bFinalizo.Enabled = true;
                    break;

                case 3: // Finalizada → habilita campos finales
                    dtpFechaFin.Enabled = true;
                    txtOdometroFin.Enabled = true;
                    break;
            }
        }

        // =====================================================
        // 🧩 Eventos
        // =====================================================

        private async void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (IdUnidad.HasValue && IdUnidad.Value > 0)
                await _presenter.AutorizarAsync(IdUnidad.Value);
            else
                MostrarMensaje("Debe seleccionar una unidad antes de autorizar.");
        }

        private async void btnIngreso_Click(object sender, EventArgs e)
        {
            await _presenter.ConfirmarIngresoAsync();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar() => Dispose();

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void bFinalizo_Click(object sender, EventArgs e)
        {
            await _presenter.ConfirmarSalidaAsync();
        }
    }
}