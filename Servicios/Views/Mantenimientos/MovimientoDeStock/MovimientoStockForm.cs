using DevExpress.XtraEditors;
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

namespace Servicios.Views
{
    public partial class MovimientoStockForm : DevExpress.XtraEditors.XtraForm, IMovimientoStockView
    {
        public readonly MovimientoStockPresenter _presenter;

        public MovimientoStockForm(MovimientoStockPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }
        public void MostrarMovimientos(List<MovimientoStockDto> movimientos)
        {
            gridControlMovimientos.DataSource = movimientos;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnAgregarMov_Click(object sender, EventArgs e)
        {
            var opcion = XtraMessageBox.Show(
                "¿El movimiento es de ENTRADA?",
                "Nuevo Movimiento",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (opcion == DialogResult.Cancel) return;

            int tipoMovimiento = opcion == DialogResult.Yes ? 1 : 2; // 1=Entrada, 2=Salida

            // Crear el movimiento en la BD con datos iniciales
            int idMovimiento = await _presenter.CrearMovimientoAsync(tipoMovimiento);

            // Abrir el form de edición directamente
            await _presenter.AbrirEdicionMovimientoAsync(idMovimiento);
        }

        private async void btnEditarMov_Click(object sender, EventArgs e)
        {
            MovimientoStockDto row = gridViewMovimientos.GetFocusedRow() as MovimientoStockDto;
            if (row != null)
            {
                await _presenter.AbrirEdicionMovimientoAsync(row.IdMovimientoStock);
            }
            else
            {
                MostrarMensaje("Seleccione una Novedad para editar.");
            }
        }
    }

}