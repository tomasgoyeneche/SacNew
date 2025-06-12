using DevExpress.XtraEditors;
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
    public partial class DispoYPFSelectorControl : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler<DateTime>? FechaSeleccionada;

        public DispoYPFSelectorControl()
        {
            InitializeComponent();

            gridViewFechas.Appearance.Row.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            gridViewFechas.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridViewFechas.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
            gridViewFechas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewFechas.Appearance.HeaderPanel.BackColor = Color.LightSlateGray;
            gridViewFechas.Appearance.HeaderPanel.ForeColor = Color.Black;

            gridViewFechas.RowHeight = 36;

            // Mostrar solo la columna de fecha, con encabezado descriptivo

            //var fechaCol = gridViewFechas.Columns["dispoFecha"];
            //if (fechaCol != null)
            //{
            //    fechaCol.Visible = true;
            //    fechaCol.Caption = "Fecha Disponible";
            //    fechaCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //    fechaCol.DisplayFormat.FormatString = "dddd dd/MM/yyyy";
            //    fechaCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //}

            // Sin agrupación, sin filtros
            gridViewFechas.OptionsView.ShowGroupPanel = false;
            gridViewFechas.OptionsCustomization.AllowGroup = false;
            gridViewFechas.OptionsView.ShowIndicator = false;

            // Centrar el grid en el control
            gridFechas.Dock = DockStyle.Fill;

            // Selección solo de una fila
            gridViewFechas.OptionsSelection.MultiSelect = false;
            gridViewFechas.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridViewFechas.DoubleClick += GridViewFechas_DoubleClick;
        }

        public void CargarFechas(List<DisponibleFecha> fechas)
        {
            gridFechas.DataSource = fechas;
        }

        private void GridViewFechas_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewFechas.GetFocusedRow() is DisponibleFecha fecha)
            {
                FechaSeleccionada?.Invoke(this, fecha.DispoFecha);
            }
        }
    }
}
