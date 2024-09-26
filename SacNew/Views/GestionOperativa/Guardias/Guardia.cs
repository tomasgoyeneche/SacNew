using SacNew.Models;
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

namespace SacNew.Views.GestionOperativa.Guardias
{
    public partial class Guardia : Form
    {

        private int _idUsuario;
        private string? _nombreUsuario;
        public Guardia()
        {
            InitializeComponent();
            //_idUsuario = SesionActual.IdUsuario;
            //_nombreUsuario = SesionActual.NombreCompleto;

        }

        private void Guardia_Load(object sender, EventArgs e)
        {

        }

        private void loginCloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
