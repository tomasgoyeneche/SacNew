using DevExpress.XtraEditors;
using Servicios.Presenters.Mantenimiento;
using Shared.Models;

namespace Servicios.Views.Mantenimientos
{
    public partial class MuestraDatosGenericoForm : DevExpress.XtraEditors.XtraForm, IMuestraDatosGenericoView
    {
        public readonly MuestraDatosGenericoPresenter _presenter;

        public MuestraDatosGenericoForm(MuestraDatosGenericoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // 🔹 Evento para aplicar estilos dinámicos (por ejemplo, para “Saldo”)
            gridViewGenerico.RowCellStyle += GridView1_RowCellStyle;
        }

        // ===========================================================
        // 🧩 Implementación de la interfaz
        // ===========================================================
        public void CargarDatos<T>(List<T> datos)
        {
            gridControlGenerico.DataSource = datos;
            gridViewGenerico.PopulateColumns();

            // 🔹 Ocultar columnas de ID automáticamente
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewGenerico.Columns)
            {
                if (col.FieldName.StartsWith("Id", StringComparison.OrdinalIgnoreCase))
                    col.Visible = false;
            }

            string tipo = typeof(T).Name;

            if (tipo == nameof(OrdenTrabajoProximoDto))
            {
                string[] columnasOcultar =
                {
            "TipoIntervalo",
            "UltimoOdometro",
            "DiasIntervalo",
            "KilometrosIntervalo"
        };

                foreach (var nombre in columnasOcultar)
                {
                    var col = gridViewGenerico.Columns[nombre];
                    if (col != null)
                        col.Visible = false;
                }
            }

            if (tipo == nameof(ArticuloStockDepositoDto))
            {
                string[] columnasOcultar =
                {
            "ArticuloDescripcion",
            "PostaDescripcion"
            //"DiasIntervalo",
            //"KilometrosIntervalo"
        };

                foreach (var nombre in columnasOcultar)
                {
                    var col = gridViewGenerico.Columns[nombre];
                    if (col != null)
                        col.Visible = false;
                }
            }

            gridViewGenerico.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar() => Dispose();

        // ===========================================================
        // 🧩 Nueva funcionalidad visual
        // ===========================================================
        public void MostrarTitulo(string titulo)
        {
            lblTitulo.Text = titulo;
            lblDescripcion.Text = "Listado de " + titulo.ToLower();
        }

        private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 🔹 Solo para vista de movimientos
            if (_presenter.TipoVistaActual == "ArticuloMovimientoHistorico" && e.Column.FieldName == "Saldo")
            {
                var valor = e.CellValue as decimal? ?? 0;
                if (valor > 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(198, 239, 206); // Verde suave
                    e.Appearance.ForeColor = Color.DarkGreen;
                }
                else if (valor < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 199, 206); // Rojo suave
                    e.Appearance.ForeColor = Color.DarkRed;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.ForeColor = Color.Black;
                }
            }

            if ((_presenter.TipoVistaActual == "ArticuloStockDeposito" || _presenter.TipoVistaActual == "ArticuloStockCritico") && e.Column.FieldName == "CantidadActual")
            {
                var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null) return;

                // Obtenemos valores de la fila actual
                decimal cantidad = e.CellValue as decimal? ?? 0;
                decimal stockCritico = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "StockCritico") ?? 0);

                // 🔹 Lógica de color
                if (cantidad > (stockCritico + 10))
                {
                    e.Appearance.BackColor = Color.FromArgb(198, 239, 206); // Verde suave
                    e.Appearance.ForeColor = Color.DarkGreen;
                }
                else if (cantidad > stockCritico)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 153); // Amarillo suave
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 199, 206); // Rojo suave
                    e.Appearance.ForeColor = Color.DarkRed;
                }
            }
        }
    }
}