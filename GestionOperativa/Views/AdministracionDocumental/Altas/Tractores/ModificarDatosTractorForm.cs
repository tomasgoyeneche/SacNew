using GestionOperativa.Presenters.AdministracionDocumental;
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

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public partial class ModificarDatosTractorForm : Form, IModificarDatosTractorView
    {
        public readonly ModificarDatosTractorPresenter _presenter;

        public ModificarDatosTractorForm(ModificarDatosTractorPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // 🔹 Evento para actualizar modelos cuando cambia la marca

            // 🔹 Cargar opciones del ComboBox Satelital
           
        }

        public int IdTractor { get; private set; }
        public string Patente => txtPatente.Text.Trim();
        public DateTime Anio => dtpAnio.Value;
        public int IdMarca => (int)cmbMarca.SelectedValue;
        public int IdModelo => (int)cmbModelo.SelectedValue;
        public decimal Tara => Convert.ToDecimal(txtTara.Text.Trim());
        public int Hp => Convert.ToInt32(txtHp.Text.Trim());
        public int Combustible => Convert.ToInt32(txtCapCombustible.Text.Trim());
        public int Cmt => Convert.ToInt32(txtCmt.Text.Trim());
        public int IdEmpresa { get; private set; } // Se obtiene del DTO al cargar datos
        public string SatelitalSeleccionado => cmbSatelital.SelectedItem?.ToString() ?? string.Empty;
        public DateTime FechaAlta => dtpFechaAlta.Value;

        public void CargarDatosTractor(Tractor tractor, List<VehiculoMarca> marcas, List<VehiculoModelo> modelos, string SatelitalNombre)
        {

            if (cmbSatelital.Items.Count == 0)
            {
                cmbSatelital.Items.Add("Megatrans");
                cmbSatelital.Items.Add("Sitrack");
            }


            IdTractor = tractor.IdTractor;
            IdEmpresa = tractor.IdEmpresa; // 🔹 Se guarda para buscar EmpresaSatelital

            txtPatente.Text = tractor.Patente;
            dtpAnio.Value = tractor.Anio;
            txtTara.Text = tractor.Tara.ToString();
            txtHp.Text = tractor.Hp.ToString();
            txtCapCombustible.Text = tractor.Combustible.ToString();
            txtCmt.Text = tractor.Cmt.ToString();
            dtpFechaAlta.Value = tractor.FechaAlta;

            cmbMarca.DataSource = marcas;
            cmbMarca.DisplayMember = "NombreMarca";
            cmbMarca.ValueMember = "IdMarca";
            cmbMarca.SelectedValue = tractor.IdMarca;

            cmbModelo.DataSource = modelos;
            cmbModelo.DisplayMember = "NombreModelo";
            cmbModelo.ValueMember = "IdModelo";
            cmbModelo.SelectedValue = tractor.IdModelo;

            // 🔹 Determinar Satelital Actual
            if (!string.IsNullOrEmpty(SatelitalNombre))
            {
                int index = cmbSatelital.FindStringExact(SatelitalNombre);
                cmbSatelital.SelectedIndex = index >= 0 ? index : -1;
            }
            else
            {
                cmbSatelital.SelectedIndex = -1;
            }
        }

        public void CargarModelos(List<VehiculoModelo> modelos)
        {
            cmbModelo.DataSource = modelos;
            cmbModelo.DisplayMember = "NombreModelo";
            cmbModelo.ValueMember = "IdModelo";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarCambios();
            Close();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedValue is int idMarca)
            {
                await _presenter.CargarModelos(idMarca);
            }
        }
    }
}
