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

            view.BestFitColumns();
        }


        public void MostrarRuteoVacios(List<Shared.Models.Ruteo> vacios)
        {
            gridControlVacios.DataSource = vacios;

            var view = gridViewVacios;

            view.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void MostrarHistorial(List<GuardiaHistorialDto> historial)
        {
            gridControlHistorico.DataSource = historial;

            var view = gridViewHistorico;
            foreach (var col in new[] { "IdGuardiaIngreso", "IdGuardiaEstado", "IdGuardiaRegistro" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            if (view.Columns["FechaGuardia"] != null)
            {
                view.Columns["FechaGuardia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["FechaGuardia"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }

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

        private async void gridViewCargados_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewCargados.GetFocusedRow() is Shared.Models.Ruteo ruteo)
                await _presenter.AbrirEdicionDePrograma(ruteo);
        }

        private async void gridViewVacios_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewVacios.GetFocusedRow() is Shared.Models.Ruteo ruteo)
                await _presenter.AbrirEdicionDePrograma(ruteo);
        }
    }
}