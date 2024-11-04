using SacNew.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class MenuFormaIngresarConsumos : Form
    {

        private readonly INavigationService _navigationService;
        public MenuFormaIngresarConsumos(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private void bIngresaDatosMv_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuIngresaConsumos>();
        }

        private void bImportaDatosMv_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<ImportaDatosMv>();

        }
    }
}
