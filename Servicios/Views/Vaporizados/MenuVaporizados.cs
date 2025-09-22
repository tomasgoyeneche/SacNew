using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
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

            gridViewVaporizados.OptionsView.ShowFooter = true;
            gridViewVaporizados.OptionsView.ShowGroupPanel = true;

            // Limpiar y agregar GroupSummary
            view.GroupSummary.Clear();
            view.GroupSummary.Add(
                DevExpress.Data.SummaryItemType.Count,
                null,
                null,
                "{0}"
            );

            view.CustomDrawGroupRow -= View_CustomDrawGroupRow; // evitar doble suscripción
            view.CustomDrawGroupRow += View_CustomDrawGroupRow;
            // 👇 Opcional: que el texto del grupo muestre la cantidad
            view.GroupFormat = "{1} [Cantidad: {2}]";

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido
        }

        private void View_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            // Obtener info del grupo
            var info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            if (info == null) return;

            // Cantidad de registros en el grupo
            int groupRowCount = view.GetChildRowCount(e.RowHandle);

            // Total de registros en el grid
            int totalCount = view.DataRowCount;

            // Valor de la columna agrupada (Descripcion)
            string groupValue = view.GetGroupRowValue(e.RowHandle)?.ToString() ?? "";

            // Calcular porcentaje
            decimal porcentaje = totalCount > 0
                ? Math.Round((decimal)groupRowCount * 100 / totalCount, 2)
                : 0;

            // 👇 Cambiar el texto que se pinta en la fila de grupo
            info.GroupText = $"{groupValue} [Cantidad: {groupRowCount} | {porcentaje}%]";
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string rutaArchivo = @$"C:\Compartida\Exportaciones\Vaporizados{DateTime.Now:yyyyMMdd}.xlsx";

            gridViewVaporizados.ExportToXlsx(rutaArchivo, new XlsxExportOptionsEx()
            {
                ExportType = DevExpress.Export.ExportType.WYSIWYG // o DataAware si querés solo datos
            });

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = rutaArchivo,
                UseShellExecute = true
            });
        }
    }
}