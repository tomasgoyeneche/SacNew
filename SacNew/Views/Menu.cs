using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas;
using SacNew.Views.GestionOperativa.Guardias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacNew.Views
{
    public partial class Menu : Form
    {



        private readonly ISesionService _sesionService;
        private readonly IServiceProvider _serviceProvider;

        public Menu(ISesionService sesionService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            txtUserName.Text = $"{_sesionService.NombreCompleto}";
            lDiaDeHoy.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

        }


        private void loginCloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bGestGuardiaBB_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains(2) && !_sesionService.Permisos.Contains(3))
            {
                var postasMenu = _serviceProvider.GetService<MenuPostas>();

                this.Hide();
                postasMenu.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show(@"No tienes permisos para acceder a las Guardias de BB.", @"Permiso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void picBoxFlotaPostas_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains(2) && !_sesionService.Permisos.Contains(3))
            {
                var postasMenu = _serviceProvider.GetService<MenuPostas>();

                this.Hide();
                postasMenu.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show(@"No tienes permisos para acceder a las Guardias de BB.", @"Permiso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
