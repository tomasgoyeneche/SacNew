using DevExpress.XtraEditors;
using Servicios.Presenters;
using Servicios.Views.Mantenimientos;
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

namespace Servicios.Views.Mantenimiento
{
    public partial class ListadoMantenimientosForm : DevExpress.XtraEditors.XtraForm, IListadoMantenimientosView
    {
        public readonly ListadoMantenimientosPresenter _presenter;

        public ListadoMantenimientosForm(ListadoMantenimientosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }
        public void MostrarMantenimientos(List<Shared.Models.Mantenimiento> movimientos)
        {
            gridControlMantenimientos.DataSource = movimientos;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnAgregarMov_Click(object sender, EventArgs e)
        {
            var opcion = XtraMessageBox.Show(
                "¿El Mantenimiento es Preventivo?",
                "Nuevo Mantenimiento",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (opcion == DialogResult.Cancel) return;

            int tipoMovimiento = opcion == DialogResult.Yes ? 1 : 2; // 1=Entrada, 2=Salida

            // Crear el movimiento en la BD con datos iniciales
            int idMovimiento = await _presenter.CrearMantenimientoAsync(tipoMovimiento);

            // Abrir el form de edición directamente
            await _presenter.AbrirEdicionMantenimientoAsync(idMovimiento);
        }

        private async void btnEditarMov_Click(object sender, EventArgs e)
        {
            Shared.Models.Mantenimiento row = gridViewMantenimientos.GetFocusedRow() as Shared.Models.Mantenimiento;
            if (row != null)
            {
                await _presenter.AbrirEdicionMantenimientoAsync(row.IdMantenimiento);
            }
            else
            {
                MostrarMensaje("Seleccione un Mantenimiento para editar.");
            }
        }
    }
}