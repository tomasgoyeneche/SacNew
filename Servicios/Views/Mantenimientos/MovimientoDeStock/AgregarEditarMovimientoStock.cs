using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios.Views
{
    public partial class AgregarEditarMovimientoStock : DevExpress.XtraEditors.XtraForm, IAgregarEditarMovimientoStockView
    {
        public readonly AgregarEditarMovimientoStockPresenter _presenter;

        public AgregarEditarMovimientoStock(AgregarEditarMovimientoStockPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        // Properties de la View
        public int IdMovimientoStock { get; private set; }

        public int IdTipoMovimiento { get => Convert.ToInt32(cmbTipoMovimiento.EditValue); set => cmbTipoMovimiento.EditValue = value; }
        public bool Autorizado { get => btnAutorizar.Enabled == false; set => btnAutorizar.Enabled = !value; }
        public DateTime FechaEmision { get => (DateTime)dateFechaEmision.EditValue; set => dateFechaEmision.EditValue = value; }
        public DateTime? FechaIngreso { get => dateFechaIngreso.EditValue as DateTime?; set => dateFechaIngreso.EditValue = value; }
        public string Observaciones { get => txtObservaciones.Text; set => txtObservaciones.Text = value; }

        public void CargarTiposMovimiento(List<TipoMovimientoStock> tipos)
        {
            cmbTipoMovimiento.Properties.DataSource = tipos;
            cmbTipoMovimiento.Properties.DisplayMember = "Nombre";
            cmbTipoMovimiento.Properties.ValueMember = "IdTipoMovimiento";
            cmbTipoMovimiento.Properties.NullText = "Seleccione...";

            cmbTipoMovimiento.Properties.Columns.Clear();
            cmbTipoMovimiento.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Tipo Movimiento"));


        }

        public List<MovimientoStockDetalleDto> ObtenerDetalles()
        {
            return (gridControlDetalles.DataSource as List<MovimientoStockDetalleDto>) ?? new List<MovimientoStockDetalleDto>();
        }

        public void CargarDetalles(List<MovimientoStockDetalleDto> detalles)
        {
            gridControlDetalles.DataSource = detalles;


            var view = gridViewDetalles;

            view.BestFitColumns();
        }

        public void CargarComprobantes(List<MovimientoComprobanteDto> comprobantes)
        {
            gridControlComprobantes.DataSource = comprobantes;

            var view = gridViewComprobantes;

            view.BestFitColumns();
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            _presenter.Autorizar();
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            _presenter.ConfirmarIngresoAsync();
            await _presenter.GuardarAsync();
        }


        public void HabilitarFechaIngreso(bool habilitar)
        {
            dateFechaIngreso.Enabled = habilitar;
        }
        public void HabilitarConfirmar(bool habilitar)
        {
            cmbTipoMovimiento.Enabled = habilitar;
            bEliminarArticulo.Enabled = habilitar;
            bConfirmar.Enabled = habilitar;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        public void Cerrar()
        {
            Dispose();
        }
        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void bAgregarArt_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarArtAsync();
        }

        private async void bEditarArt_Click(object sender, EventArgs e)
        {
            MovimientoStockDetalleDto row = gridViewDetalles.GetFocusedRow() as MovimientoStockDetalleDto;
            if (row != null)
            {
                await _presenter.EditarArtAsync(row.IdMovimientoDetalle);
            }
            else
            {
                MostrarMensaje("Seleccione un Articulo para editar.");
            }
        }

        private async void bEliminarArticulo_Click(object sender, EventArgs e)
        {
            MovimientoStockDetalleDto row = gridViewDetalles.GetFocusedRow() as MovimientoStockDetalleDto;
            if (row != null)
            {
                await _presenter.EliminarArtAsync(row.IdMovimientoDetalle);
            }
            else
            {
                MostrarMensaje("Seleccione un Articulo para eliminar.");
            }
        }

        private async void simpleButton4_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarComprobanteAsync();
        }

        private async void bEditarComprobante_Click(object sender, EventArgs e)
        {
            MovimientoComprobanteDto row = gridViewComprobantes.GetFocusedRow() as MovimientoComprobanteDto;
            if (row != null)
            {
                await _presenter.EditarComprobanteAsync(row.IdMovimientoComprobante);
            }
            else
            {
                MostrarMensaje("Seleccione un Comprobante para editar.");
            }
        }

        private async void bEliminarComprobante_Click(object sender, EventArgs e)
        {
            MovimientoComprobanteDto row = gridViewDetalles.GetFocusedRow() as MovimientoComprobanteDto;
            if (row != null)
            {
                await _presenter.EliminarComprobanteAsync(row.IdMovimientoComprobante);
            }
            else
            {
                MostrarMensaje("Seleccione un Comprobante para eliminar.");
            }
        }

        private void gridViewComprobantes_DoubleClick(object sender, EventArgs e)
        {

            MovimientoComprobanteDto comprobante = gridViewComprobantes.GetFocusedRow() as MovimientoComprobanteDto;
            if (comprobante == null) return;

            if (!string.IsNullOrWhiteSpace(comprobante.RutaComprobante) && File.Exists(comprobante.RutaComprobante))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = comprobante.RutaComprobante,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"No se pudo abrir el archivo.\n\n{ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("El archivo no existe en la ruta especificada.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}