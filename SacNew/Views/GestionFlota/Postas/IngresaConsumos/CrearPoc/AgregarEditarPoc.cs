using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc
{
    public partial class AgregarEditarPoc : Form, IAgregarEditarPOCView
    {
        public readonly AgregarEditarPOCPresenter _presenter;

        public AgregarEditarPoc(AgregarEditarPOCPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);  // Relacionamos la vista con el Presenter
        }

        // Implementación de IAgregarEditarPOCView
        public int IdNomina => Convert.ToInt32(cmbNomina.SelectedValue);
        public int IdPosta => Convert.ToInt32(cmbPosta.SelectedValue);
        public string NumeroPOC => txtNumeroPOC.Text.Trim();
        public int Odometro => string.IsNullOrEmpty(txtOdometro.Text) ? 0 : Convert.ToInt32(txtOdometro.Text.Trim());
        public string Comentario => txtComentario.Text.Trim();
        public DateTime FechaCreacion => dtpFechaCreacion.Value;
        public int IdUsuario => _presenter.IdUsuario;



        public void CargarNominas(List<Nomina> nominas)
        {
            cmbNomina.DataSource = nominas;
            cmbNomina.DisplayMember = "DescripcionNomina";  // Mostrar la descripción personalizada
            cmbNomina.ValueMember = "IdNomina";

            if (_presenter._pocActual != null)
            {
                cmbNomina.SelectedValue = _presenter._pocActual.IdNomina;
            }
        }

        public void CargarPostas(List<Posta> postas)
        {

            cmbPosta.DataSource = postas;
            cmbPosta.DisplayMember = "Descripcion";
            cmbPosta.ValueMember = "Id";

            if (_presenter._pocActual != null)
            {
                cmbPosta.SelectedValue = _presenter._pocActual.IdPosta;
            }
        }

        public void MostrarDatosPOC(POC poc)
        {
            txtNumeroPOC.Text = poc.NumeroPOC;
            txtOdometro.Text = Convert.ToString(poc.Odometro);
            txtComentario.Text = poc.Comentario;
            cmbNomina.SelectedValue = poc.IdNomina;
            cmbPosta.SelectedValue = poc.IdPosta;
            dtpFechaCreacion.Value = poc.FechaCreacion;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarPOC();
            this.Close();
        }

        private void AgregarEditarPOC_Load(object sender, EventArgs e)
        {
            _presenter.Inicializar();  // Cargar datos iniciales
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

      
    }
}
