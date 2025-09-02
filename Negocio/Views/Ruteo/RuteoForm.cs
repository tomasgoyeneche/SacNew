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

        public void SetEstadoCargaDisponibles(bool cargando)
        {
            if (cargando)
            {
                // Limpia datos y bloquea la grilla
                //gridControlCargados.DataSource = null;
                //gridViewCargados.OptionsBehavior.Editable = false;
                gridControlCargados.Enabled = false;

                // Mensajes opcionales
                //gridControlVacios.DataSource = null;
                //gridViewVacios.OptionsBehavior.Editable = false;
                gridControlVacios.Enabled = false;


                //gridControlAlertas.DataSource = null;
                //gridViewAlertas.OptionsBehavior.Editable = false;
                gridControlAlertas.Enabled = false;

                //gridControlHistorico.DataSource = null;
                //gridViewHistorico.OptionsBehavior.Editable = false;
                gridControlHistorico.Enabled = false;
            }
            else
            {
                gridControlCargados.Enabled = true;
                gridControlVacios.Enabled = true;
                gridControlAlertas.Enabled = true;
                gridControlHistorico.Enabled = true;
            }
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

            //view.BestFitColumns();
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

        private async void gridViewAlertas_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks != 2 || e.RowHandle < 0) return;

            if (gridViewAlertas.GetFocusedRow() is AlertaDto alerta)
            {
                var confirm = XtraMessageBox.Show(
                    $"¿Deseás eliminar la alerta #{alerta.IdAlerta}?\n\n{alerta.Descripcion}",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    await _presenter.EliminarAlertaAsync(alerta);
                }
            }
        }

        private async void gridViewCargados_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;

            // Deseleccionar en vacíos
            gridViewVacios.ClearSelection();
            gridViewVacios.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            gridControlAlertas.Enabled = false;
            gridControlHistorico.Enabled = false;
            gridControlVencimientos.Enabled = false;

            if (gridViewCargados.GetRow(e.RowHandle) is Shared.Models.Ruteo ruteo)
            {
                await _presenter.CargarVencimientosYAlertasAsync(ruteo);
                gridControlAlertas.Enabled = true;
                gridControlHistorico.Enabled = true;
                gridControlVencimientos.Enabled = true;
            }
        }

        private async void gridViewVacios_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;

            // Deseleccionar en cargados
            gridViewCargados.ClearSelection();
            gridViewCargados.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            gridControlAlertas.Enabled = false;
            gridControlHistorico.Enabled = false;
            gridControlVencimientos.Enabled = false;

            if (gridViewVacios.GetRow(e.RowHandle) is Shared.Models.Ruteo ruteo)
            {
                await _presenter.CargarVencimientosYAlertasAsync(ruteo);
                gridControlAlertas.Enabled = true;
                gridControlHistorico.Enabled = true;
                gridControlVencimientos.Enabled = true;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text;

            // Buscar en Cargados
            gridViewCargados.FindFilterText = filtro;

            // Buscar en Vacíos
            gridViewVacios.FindFilterText = filtro;
        }

        private async void bRecargar_Click(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }
    }
}