using DevExpress.Utils.DirectXPaint.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using GestionFlota.Presenters;
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

namespace GestionFlota.Views
{
    public partial class CupeoForm : DevExpress.XtraEditors.XtraForm, ICupeoView
    {
        public readonly CupeoPresenter _presenter;

        public CupeoForm(CupeoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarCupeoDisp(List<Shared.Models.Cupeo> cargados)
        {
            gridControlDisp.DataSource = cargados;

            var view = gridViewDisp;

            view.BestFitColumns();
        }


        public void MostrarCupeoAsignados(List<Shared.Models.Cupeo> vacios)
        {
            gridControlAsignados.DataSource = vacios;

            var view = gridViewAsignados;

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

        private async void gridViewAsignados_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                // Si hay algo seleccionado en cargados, deselecciona en vacios
                gridViewDisp.ClearSelection();
                gridViewDisp.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            }

            if (gridViewAsignados.GetFocusedRow() is Shared.Models.Cupeo cupeo)
            {
                //await _presenter.MostrarHistorialAsync(guardia.IdGuardiaIngreso);
                await _presenter.CargarVencimientosYAlertasAsync(cupeo);
            }

        }

        private async void gridViewDisp_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                // Si hay algo seleccionado en vacios, deselecciona en cargados
                gridViewAsignados.ClearSelection();
                gridViewAsignados.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            }
            if (gridViewDisp.GetFocusedRow() is Shared.Models.Cupeo cupeo)
            {
                //await _presenter.MostrarHistorialAsync(guardia.IdGuardiaIngreso);
                await _presenter.CargarVencimientosYAlertasAsync(cupeo);
            }
        }

        private async void bImportarPrograma_Click(object sender, EventArgs e)
        {
            await _presenter.ImportarProgramaAsync();
        }

        private async void gridViewAsignados_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewAsignados.GetFocusedRow() is Shared.Models.Cupeo cupeo)
                await _presenter.AbrirAsignarCargaAsync(cupeo);
        }

        private void gridViewHistorico_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewHistorico.GetFocusedRow() is HistorialGeneralDto historico)
            {
                MostrarMensaje($"Mensaje: {historico.Descripcion}");
            }
        }

        private async void gridViewDisp_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewDisp.GetFocusedRow() is Shared.Models.Cupeo cupeo)
                await _presenter.AbrirAsignarManual(cupeo);
        }

        //private async void gridViewVacios_DoubleClick(object sender, EventArgs e)
        //{
        //    if (gridViewAsignados.GetFocusedRow() is Cupeo cupeo)
        //        await _presenter.AbrirModificacionPrograma(cupeo);
        //}
    }
}