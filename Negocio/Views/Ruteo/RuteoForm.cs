using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class RuteoForm : DevExpress.XtraEditors.XtraForm, IRuteoView
    {
        public readonly RuteoPresenter _presenter;

        public RuteoForm(RuteoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarRuteoCargados(List<Shared.Models.Ruteo> cargados)
        {
            gridControlCargados.DataSource = cargados;

            var view = gridViewCargados;

            //view.BestFitColumns();
        }

        public void MostrarRuteoVacios(List<Shared.Models.Ruteo> vacios)
        {
            gridControlVacios.DataSource = vacios;

            var view = gridViewVacios;

            // view.BestFitColumns();
        }

        public void MostrarChoferesLibres(List<ChoferesLibresDto> choferes)
        {
            gridControlChoferesLibres.DataSource = choferes;

            var view = gridViewChoferesLibres;

            view.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarHistorial(List<HistorialGeneralDto> historial)
        {
            gridControlHistorico.DataSource = historial;

            var view = gridViewHistorico;

            //if (view.Columns["Fecha"] != null)
            //{
            //    view.Columns["Fecha"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //    view.Columns["Fecha"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            //}

            view.BestFitColumns();
        }

        public void MostrarVencimientos(List<VencimientosDto> vencimientos)
        {
            gridControlVencimientos.DataSource = vencimientos;

            var view = gridViewVencimientos;
            foreach (var col in new[] { "Entidad" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewVencimientos.BestFitColumns();
        }

        public void MostrarAlertas(List<AlertaDto> alertas)
        {
            gridControlAlertas.DataSource = alertas;

            var view = gridViewAlertas;
            foreach (var col in new[] { "IdAlerta", "IdNomina", "Activo", "PatenteSemi" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewAlertas.BestFitColumns();
        }

        private void gridViewVencimientos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var view = sender as GridView;
            var vencimiento = view.GetRow(e.RowHandle) as VencimientosDto;

            if (vencimiento == null || e.Column.FieldName != "FechaVencimiento")
                return;

            var fecha = vencimiento.FechaVencimiento.Date;
            var hoy = DateTime.Now.Date;

            if (fecha < hoy)
            {
                e.Appearance.BackColor = Color.LightCoral; // rojo
            }
            else if ((fecha - hoy).TotalDays <= 7)
            {
                e.Appearance.BackColor = Color.Khaki; // amarillo
            }
            else
            {
                e.Appearance.BackColor = Color.LightGreen; // verde
            }
        }

        private async void gridViewCargados_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                // Si hay algo seleccionado en cargados, deselecciona en vacios
                gridViewVacios.ClearSelection();
                gridViewVacios.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            }

            if (gridViewCargados.GetFocusedRow() is Shared.Models.Ruteo ruteo)
            {
                //await _presenter.MostrarHistorialAsync(guardia.IdGuardiaIngreso);
                await _presenter.CargarVencimientosYAlertasAsync(ruteo);
            }
        }

        private async void gridViewVacios_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                // Si hay algo seleccionado en vacios, deselecciona en cargados
                gridViewCargados.ClearSelection();
                gridViewCargados.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            }
            if (gridViewVacios.GetFocusedRow() is Shared.Models.Ruteo ruteo)
            {
                //await _presenter.MostrarHistorialAsync(guardia.IdGuardiaIngreso);
                await _presenter.CargarVencimientosYAlertasAsync(ruteo);
            }
        }

        public void MostrarResumen(List<RuteoResumen> resumen)
        {
            if (resumen == null || resumen.Count == 0)
            {
                gridControlResumen.DataSource = null;
                return;
            }

            int total = resumen.Sum(r => r.Cantidad);

            var lista = resumen
                .OrderBy(r => r.Orden)
                .Select(r => new RuteoResumen
                {
                    Estado = r.Estado,
                    Cantidad = r.Cantidad,
                    Orden = r.Orden,
                    Porcentaje = total > 0 ? Math.Round((decimal)r.Cantidad * 100 / total, 2) : 0
                })
                .ToList();

            // Agregar la fila de total al final
            lista.Add(new RuteoResumen
            {
                Estado = "Total",
                Cantidad = total,
                Orden = int.MaxValue,
                Porcentaje = 100
            });

            gridControlResumen.DataSource = lista;
        }

        private void gridViewHistorico_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewHistorico.GetFocusedRow() is HistorialGeneralDto historico)
            {
                MostrarMensaje($"Mensaje: {historico.Descripcion}");
            }
        }

        private async void bControlDemorados_Click(object sender, EventArgs e)
        {
            await _presenter.ExportarDemoradosAsync();
        }

        private async void gridViewCargados_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0 && gridViewCargados.GetFocusedRow() is Shared.Models.Ruteo ruteo) // Es doble click sobre una celda válida
            {
                var idPrograma = ruteo.IdPrograma; // Guardás antes de abrir
                await _presenter.AbrirEdicionDePrograma(ruteo, idPrograma);
            }
        }

        private async void gridViewVacios_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0 && gridViewVacios.GetFocusedRow() is Shared.Models.Ruteo ruteo) // Es doble click sobre una celda válida
            {
                var idNomina = ruteo.IdNomina; // Guardás antes de abrir
                await _presenter.AbrirEdicionDePrograma(ruteo, null, idNomina);
            }
        }

        public void SeleccionarRuteoCargadoPorId(int idPrograma)
        {
            var view = gridViewCargados;
            for (int i = 0; i < view.RowCount; i++)
            {
                var row = view.GetRow(i) as Shared.Models.Ruteo;
                if (row != null && row.IdPrograma == idPrograma)
                {
                    view.FocusedRowHandle = i;
                    view.SelectRow(i); // Opcional: resalta la fila
                    break;
                }
            }
        }

        public void SeleccionarRuteoPorNomina(int idNomina)
        {
            var view = gridViewVacios;
            for (int i = 0; i < view.RowCount; i++)
            {
                var row = view.GetRow(i) as Shared.Models.Ruteo;
                if (row != null && row.IdNomina == idNomina)
                {
                    view.FocusedRowHandle = i;
                    view.SelectRow(i); // Opcional: resalta la fila
                    break;
                }
            }
        }
    }
}