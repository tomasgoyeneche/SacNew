using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;
using Shared.Models;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public partial class IngresaGasoilForm : DevExpress.XtraEditors.XtraForm, IIngresaGasoilView
    {
        public readonly IngresaGasoilPresenter _presenter;

        public IngresaGasoilForm(IngresaGasoilPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }


        public Concepto TipoGasoilSeleccionado => cmbTipoGasoil.GetSelectedDataRow() as Concepto;
        public decimal? Litros => decimal.TryParse(txtLitros.EditValue?.ToString(), out var result)
        ? result
        : null;
        public string NumeroVale =>
            txtNumeroVale.Text.Trim();

        public string Observaciones =>
            txtObservaciones.Text.Trim();
        public DateTime FechaCarga =>
            dtpFechaCarga.EditValue is DateTime fecha
        ? fecha
        : DateTime.Now;

        public bool Dolar => dolarCheck.Checked;

        public void CargarTiposGasoil(List<Concepto> tiposGasoil, string poc, POCDto pocdto)
        {
            cmbTipoGasoil.Properties.DataSource = tiposGasoil;
            cmbTipoGasoil.Properties.DisplayMember = "Descripcion";
            cmbTipoGasoil.Properties.ValueMember = "IdConsumo";
            cmbTipoGasoil.Properties.NullText = "[Seleccione tipo]";
            cmbTipoGasoil.Properties.Columns.Clear();
            cmbTipoGasoil.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Descripcion"));

            lblChofer.Text = pocdto.NombreCompletoChofer;
            lblTractor.Text = pocdto.PatenteTractor;
            lblSemi.Text = pocdto.PatenteSemi;
            labelPoc.Text = poc;

            dtpFechaCarga.EditValue = DateTime.Now;
        }

        public void InicializarParaEdicion(ConsumoGasoil consumo)
        {
            cmbTipoGasoil.EditValue = consumo.IdConsumo;
            txtNumeroVale.EditValue = consumo.NumeroVale;
            dtpFechaCarga.EditValue = consumo.FechaCarga;
            txtLitros.EditValueChanged -= txtLitros_TextChanged;
            txtLitros.EditValue = consumo.LitrosCargados;
            txtLitros.EditValueChanged += txtLitros_TextChanged;
            txtTotal.EditValue = consumo.PrecioTotal;
            txtObservaciones.EditValue = consumo.Observaciones;
            dolarCheck.Checked = consumo.Dolar;
        }

        public void MostrarLitrosAutorizados(decimal litrosAutorizados, decimal kilometros, string origen, string destino, string albaran)
        {
            txtLitrosAutorizados.Text = $"Litros autorizados restantes : {litrosAutorizados.ToString("N2")}Lts | Viaje TD: {albaran} | Orig/Dest: {origen} - {destino} | Recorrido: {kilometros.ToString("N2")}Km";
        }

        public void MostrarTotalCalculado(decimal total)
        {
            txtTotal.EditValue = total;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ConfirmarGuardado(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void Cerrar()
        {
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (Litros.HasValue && cmbTipoGasoil.EditValue != null)
            {
                _presenter.CalcularTotal(Litros.Value);
            }
            else
            {
                MostrarMensaje("Debe ingresar un valor válido para los litros.");
            }
        }

        private void txtLitros_TextChanged(object sender, EventArgs e)
        {
            if (Litros.HasValue && cmbTipoGasoil.EditValue != null)
            {
                _presenter.CalcularTotal(Litros.Value);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumoAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        public void MostrarConsumosAnteriores(List<ConsumoGasoilAutorizadoDto> consumos)
        {
            dataGridViewAnteriores.DataSource = consumos;
            gridViewAnteriores.BestFitColumns();
        }

        public void MostrarConsumosTotales(List<ConsumoGasoilAutorizadoDto> consumos)
        {
            dataGridViewTotales.DataSource = consumos;
            gridViewTotales.BestFitColumns();
        }

        public void ActualizarLabelAnterior(decimal restante)
        {
            lblAnterior.Text = $"Restante Anterior: {restante:N2} L";

            if (restante < 0)
            {
                lblAnterior.ForeColor = Color.Brown;
            }
            else if (restante < 1000)
            {
                lblAnterior.ForeColor = Color.Orange;
            }
            else
            {
                lblAnterior.ForeColor = Color.LightGreen;
            }
        }

        public void ActualizarLabelTotal(decimal restante)
        {
            lblTotal.Text = $"Restante Total: {restante:N2} L";
            if (restante < 0)
            {
                lblTotal.ForeColor = Color.Brown;
            }
            else if (restante < 1000)
            {
                lblTotal.ForeColor = Color.Orange;
            }
            else
            {
                lblTotal.ForeColor = Color.LightGreen;
            }
        }
    }
}