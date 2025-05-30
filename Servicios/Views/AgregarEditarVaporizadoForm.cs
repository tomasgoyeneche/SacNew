using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Servicios.Views;
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

namespace Servicios
{
    public partial class AgregarEditarVaporizadoForm : DevExpress.XtraEditors.XtraForm, IAgregarEditarVaporizadoView
    {
        public readonly AgregarEditarVaporizadoPresenter _presenter;

        public AgregarEditarVaporizadoForm(AgregarEditarVaporizadoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // Eventos para calcular el tiempo
            dtpInicio.EditValueChanged += (s, e) => _presenter.CalcularTiempoVaporizado();
            dtpFin.EditValueChanged += (s, e) => _presenter.CalcularTiempoVaporizado();
        }

        // Interfaz
        public void MostrarDatosGuardia(string texto)
        {
            lblDatos.Text = texto;
        }

        public void CargarDatos(Vaporizado vaporizado)
        {
            txtCisterna.Text = vaporizado.CantidadCisternas.ToString();
            cmbMotivo.EditValue = vaporizado.IdVaporizadoMotivo;
            dtpInicio.EditValue = vaporizado.FechaInicio;
            dtpFin.EditValue = vaporizado.FechaFin;
            txtNroCertificado.Text = vaporizado.NroCertificado;
            cmbPlanta.EditValue = vaporizado.IdVaporizadoZona;
            txtNroDanes.Text = vaporizado.RemitoDanes;
            txtObservaciones.Text = vaporizado.Observaciones;
        }
        public void CargarPlantas(List<VaporizadoZona> plantas)
        {
            cmbPlanta.Properties.DataSource = plantas;
            cmbPlanta.Properties.DisplayMember = "Descripcion";
            cmbPlanta.Properties.ValueMember = "IdVaporizadoZona";
            cmbPlanta.Properties.Columns.Clear();
            cmbPlanta.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Planta"));
        }
        public void CargarMotivos(List<VaporizadoMotivo> motivos)
        {
            cmbMotivo.Properties.DataSource = motivos;
            cmbMotivo.Properties.DisplayMember = "Descripcion";
            cmbMotivo.Properties.ValueMember = "IdVaporizadoMotivo";
            cmbMotivo.Properties.Columns.Clear();
            cmbMotivo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Motivo"));
        }

        // Propiedades de entrada/salida
        public int CantidadCisternas => int.TryParse(txtCisterna.Text, out int n) ? n : 0;
        public int? IdMotivo => cmbMotivo.EditValue is int v ? v : (int?)null;
        public DateTime? FechaInicio => dtpInicio.EditValue as DateTime?;
        public DateTime? FechaFin => dtpFin.EditValue as DateTime?;
        public string TiempoVaporizado
        {
            get => txtTiempoVaporizado.Text;
            set => txtTiempoVaporizado.Text = value;
        }
        public string NroCertificado => txtNroCertificado.Text.Trim();
        public int? IdPlanta => cmbPlanta.EditValue is int v ? v : (int?)null;
        public string RemitoDanes => txtNroDanes.Text.Trim();
        public string Observaciones => txtObservaciones.Text.Trim();
        public string NroPresupuesto => txtNroPres.Text.Trim();
        public string NroImporte => txtImporte.Text.Trim();

        public void SetNroPresupuestoVisible(bool visible)
        {
            lblNroPres.Visible = txtNroPres.Visible = visible;
        }
        public void SetNroImporteVisible(bool visible)
        {
            lblImporte.Visible = txtImporte.Visible = visible; 
            panelPresupuesto.Visible = visible;
        }
        public void SetTiempoVaporizado(string tiempo)
        {
            txtTiempoVaporizado.Text = tiempo;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
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
            this.Close();
        }
    }
}