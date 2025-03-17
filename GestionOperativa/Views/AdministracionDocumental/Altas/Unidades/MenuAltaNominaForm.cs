using GestionOperativa.Presenters.AdministracionDocumental;
using GestionOperativa.Presenters.Tractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class MenuAltaNominaForm : Form, IMenuAltaNominaView
    {
        public MenuAltaNominaPresenter _presenter;

        public MenuAltaNominaForm(MenuAltaNominaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void bNominaMetanol_Click(object sender, EventArgs e)
        {
            _presenter.GenerarReporteFlotaAsync();
        }
    }
}
