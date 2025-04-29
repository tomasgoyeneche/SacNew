using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.OpenXml;
using GestionFlota.Presenters.Informes;
using GestionFlota.Views.Postas.Informes;
using GestionOperativa.Presenters;
using GestionOperativa.Views.AdministracionDocumental;
using InformesYEstadisticas.Presenters;
using InformesYEstadisticas.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InformesYEstadisticas
{
    public partial class MenuInformesEst : DevExpress.XtraEditors.XtraForm, IMenuInformesEstView
    {

        private readonly MenuInformesEstPresenter _presenter;

        public MenuInformesEst(MenuInformesEstPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        public void MostrarMensaje(string mensaje)
        {
            throw new NotImplementedException();
        }

        private async void bRelevamientoInicial_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarFichaVacia();
        }

        private async void bVerifMensual_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarVerifMensual();
        }

        private async void bControlOpCons_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarReporteConsumos();  
        }
    }
}