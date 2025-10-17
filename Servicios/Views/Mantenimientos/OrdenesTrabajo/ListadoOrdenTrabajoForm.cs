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

namespace Servicios.Views.Mantenimientos
{
    public partial class ListadoOrdenTrabajoForm : DevExpress.XtraEditors.XtraForm, IListadoOrdenTrabajoView
    {
        public readonly ListadoOrdenTrabajoPresenter _presenter;

        public ListadoOrdenTrabajoForm(ListadoOrdenTrabajoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }
        public void MostrarOrdenesDeTrabajo(List<OrdenTrabajoDto> ordenes)
        {
            gridControlMantenimientos.DataSource = ordenes;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnAgregarMov_Click(object sender, EventArgs e)
        {
            int idMovimiento = await _presenter.CrearOrdenAsync();

            // Abrir el form de edición directamente
            await _presenter.AbrirEdicionMovimientoAsync(idMovimiento);
        }

        private async void btnEditarMov_Click(object sender, EventArgs e)
        {
            OrdenTrabajoDto row = gridViewMantenimientos.GetFocusedRow() as OrdenTrabajoDto;
            if (row != null)
            {
                await _presenter.AbrirEdicionMovimientoAsync(row.IdOrdenTrabajo);
            }
            else
            {
                MostrarMensaje("Seleccione una Orden para editar.");
            }
        }
    }
}