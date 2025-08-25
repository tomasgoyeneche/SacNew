using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionDocumental.Presenters.Novedades;
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

namespace GestionDocumental.Views.Novedades.NovedadesUnidades
{
    public partial class AgregarEditarUnidadNovedadForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarNovedadUnidadView
    {
        public readonly AgregarEditarNovedadUnidadPresenter _presenter;

        public AgregarEditarUnidadNovedadForm(AgregarEditarNovedadUnidadPresenter presenter)
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

        public DateTime FechaInicio => (DateTime)dtpFechaInicio.EditValue;

        public DateTime FechaFin => (DateTime)dtpFechaFinal.EditValue;

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

            dtpFechaFinal.EditValue = DateTime.Now.AddDays(1);
            dtpFechaInicio.EditValue = DateTime.Now;
            cmbUnidad.EditValue = _presenter.NovedadActual?.idUnidad ?? -1;
        }

        public void CargarEstados(List<UnidadMantenimientoEstado> estados)
        {
            cmbEstado.Properties.DataSource = estados;
            cmbEstado.Properties.DisplayMember = "Descripcion";
            cmbEstado.Properties.ValueMember = "IdMantenimientoEstado";

            cmbEstado.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbEstado.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Estado"));

            txtOdometro.Text = "0";
            cmbEstado.EditValue = _presenter.NovedadActual?.idMantenimientoEstado ?? -1;
        }

        public void MostrarDatosNovedad(UnidadMantenimientoDto novedadesUnidades)
        {
            txtObservaciones.Text = novedadesUnidades.Observaciones;
            cmbUnidad.EditValue = novedadesUnidades.idUnidad;
            cmbEstado.EditValue = novedadesUnidades.idMantenimientoEstado;

            txtOdometro.Text = novedadesUnidades.Odometro.ToString();

            dtpFechaInicio.EditValue = novedadesUnidades.FechaInicio;
            dtpFechaFinal.EditValue = novedadesUnidades.FechaFin;
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

        private void bAyuda_Click(object sender, EventArgs e)
        {
            if (cmbEstado.GetSelectedDataRow() is UnidadMantenimientoEstado estado)
            {
                // Mostrá el mensaje con la descripción
                XtraMessageBox.Show(this, $"Estado Detalle: {estado.Detalle}", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(this, "No hay ningún estado seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}