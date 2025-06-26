using DevExpress.XtraEditors;
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
    public partial class ViajesConsolidadosForm : DevExpress.XtraEditors.XtraForm, IViajesConsolidadosView
    {
        public readonly ViajesConsolidadosPresenter _presenter;

        public ViajesConsolidadosForm(ViajesConsolidadosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void CargarProgramas(List<VistaProgramaGridDto> programas)
        {
            gridControlProgramas.DataSource = programas;
            gridViewProgramas.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarRemitosFaltantes(int faltanCarga, int faltanEntrega)
        {
            txtCarga.Text = $"Carga: {faltanCarga}";
            txtEntrega.Text = $"Entrega: {faltanEntrega}";
        }

        private async void gridViewProgramas_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewProgramas.FocusedRowHandle >= 0)
            {
                var row = gridViewProgramas.GetRow(gridViewProgramas.FocusedRowHandle) as VistaProgramaGridDto;
                if (row != null)
                {
                    var ruteo = await _presenter.MapearARuteoAsync(row.Id);
                    if (ruteo != null)
                    {
                        await _presenter.AbrirEditarProgramaAsync(ruteo);
                    }
                }
            }
        }
    }
}