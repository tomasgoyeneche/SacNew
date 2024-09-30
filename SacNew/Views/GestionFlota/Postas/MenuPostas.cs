using Microsoft.Extensions.DependencyInjection;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacNew.Views.GestionFlota.Postas
{
    public partial class MenuPostas : Form
    {

        private readonly ISesionService _sesionService;
        private readonly IServiceProvider _serviceProvider;
        public MenuPostas(ISesionService sesionService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;
        }

        private void MenuPostas_Load(object sender, EventArgs e)
        {
            lMenuPostasNombre.Text = _sesionService.NombreCompleto;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            this.Dispose();
        }

        private void loginCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bAbmPostasMenu_Click(object sender, EventArgs e)
        {
            //var MenuAbmPostas = _serviceProvider.GetService<MenuAbmPostas>();
            using (var MenuAbmPostas = _serviceProvider.GetService<MenuAbmPostas>())
            {
                this.Hide();
                MenuAbmPostas.ShowDialog();
                this.Show();
            }

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bMenuConceptos_Click(object sender, EventArgs e)
        {
            using (var MenuConceptos = _serviceProvider.GetService<MenuConceptos>())
            {
                this.Hide();
                MenuConceptos.ShowDialog();
                this.Show();
            }
        }
    }
}
