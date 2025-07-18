using DevExpress.XtraEditors;
using Servicios.Presenters;
using Shared.Models;

namespace Servicios
{
    public partial class MenuVaporizados : DevExpress.XtraEditors.XtraForm, IMenuVaporizadosView
    {
        public readonly MenuVaporizadosPresenter _presenter;

        public MenuVaporizados(MenuVaporizadosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarVaporizados(List<VaporizadoDto> vaporizados)
        {
            gridControlVaporizados.DataSource = null;
            gridViewVaporizados.Columns.Clear();

            gridControlVaporizados.DataSource = vaporizados;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewVaporizados;

            foreach (var col in new[] { "IdVaporizado", "TipoIngreso", "IdPosta", "IdGuardiaIngreso" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            if (view.Columns["Inicio"] != null)
            {
                view.Columns["Inicio"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["Inicio"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            if (view.Columns["Fin"] != null)
            {
                view.Columns["Fin"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["Fin"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            VaporizadoDto row = gridViewVaporizados.GetFocusedRow() as VaporizadoDto;
            if (row != null)
            {
                await _presenter.EliminarVaporizadoAsync(row);
            }
            else
            {
                MostrarMensaje("Seleccione un vaporizado para eliminar.");
            }
        }

        //private async void btnAgregarNovedad_Click(object sender, EventArgs e)
        //{
        //    await _presenter.AgregarVaporizadoAsync();
        //}

        private async void btnEditarVaporizado_Click(object sender, EventArgs e)
        {
            VaporizadoDto? row = gridViewVaporizados.GetFocusedRow() as VaporizadoDto;
            if (row != null)
            {
                await _presenter.EditarVaporizadoAsync(row);
            }
            else
            {
                MostrarMensaje("Seleccione un vaporizado para editar.");
            }
        }

        private async void bAgregarExterno_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarVaporizadoExternoAsync();
        }
    }
}