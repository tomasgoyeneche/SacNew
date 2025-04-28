using DevExpress.XtraReports.UI;
using Shared.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace GestionOperativa.Reports
{
    public partial class ReporteNominaEquiposEnComodato : DevExpress.XtraReports.UI.XtraReport
    {
        public ReporteNominaEquiposEnComodato()
        {
            InitializeComponent();
        }

        private void xrTableCell1_BeforePrint(object sender, CancelEventArgs e)
        {
            var cell = (XRTableCell)sender;
            var data = GetCurrentRow() as UnidadDto;

            if (data.Empresa_Tractor != data.Empresa_Unidad)
            {
                cell.ForeColor = Color.Black;
                cell.Font = new Font(cell.Font, FontStyle.Bold);
            }
            else
            {
                cell.ForeColor = Color.Gray;
                cell.Font = new Font(cell.Font, FontStyle.Regular);
            }
        }

        private void xrTableCell2_BeforePrint(object sender, CancelEventArgs e)
        {
            var cell = (XRTableCell)sender;
            var data = GetCurrentRow() as UnidadDto;

            if (data.Empresa_Semi != data.Empresa_Unidad)
            {
                cell.ForeColor = Color.Black;
                cell.Font = new Font(cell.Font, FontStyle.Bold);
            }
            else
            {
                cell.ForeColor = Color.Gray;
                cell.Font = new Font(cell.Font, FontStyle.Regular);
            }
        }
    }
}
