using DevExpress.XtraEditors;
using Servicios.Presenters;
using Shared.Models;

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

        private async void btnEliminarArt_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Eliminar esta Orden de trabajo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                OrdenTrabajoDto row = gridViewMantenimientos.GetFocusedRow() as OrdenTrabajoDto;
                if (row.Fase != 0)
                {
                    MostrarMensaje("No se puede eliminar una Orden de trabajo que ya ha sido iniciada.");
                    return;
                }
                else
                {
                    if (row != null)
                    {
                        await _presenter.EliminarOrdenAsync(row.IdOrdenTrabajo);
                    }
                    else
                    {
                        MostrarMensaje("Seleccione una Orden para eliminar.");
                    }
                }
            }
        }
    }
}