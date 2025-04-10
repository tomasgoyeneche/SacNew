using DevExpress.XtraEditors;
using GestionOperativa.Presenters.AdministracionDocumental.Altas;
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
    public partial class AltaEntidadForm : DevExpress.XtraEditors.XtraForm, IAltaEntidadView
    {
        public readonly AgregarEntidadPresenter _presenter;

        public AltaEntidadForm(AgregarEntidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string Campo1 => txtCampo1.Text.Trim();
        public string Campo2 => txtCampo2.Text.Trim();
        public string Campo3 => txtCampo3.Text.Trim();

        public void ConfigurarCampos(string entidad)
        {
            switch (entidad)
            {
                case "empresa":
                    lblCampo1.Text = "Nombre Fantasía:";
                    lblCampo2.Text = "CUIT:";
                    lblCampo3.Visible = txtCampo3.Visible = false;
                    lblCampo2.Visible = txtCampo2.Visible = true;
                    break;

                case "chofer":
                    lblCampo1.Text = "Nombre:";
                    lblCampo2.Text = "Apellido:";
                    lblCampo3.Text = "Documento:";
                    lblCampo2.Visible = txtCampo2.Visible = true;
                    lblCampo3.Visible = txtCampo3.Visible = true;
                    break;

                case "tractor":
                case "semi":
                    lblCampo1.Text = "Patente:";
                    lblCampo2.Visible = txtCampo2.Visible = false;
                    lblCampo3.Visible = txtCampo3.Visible = false;
                    break;
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar() => Close();

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}