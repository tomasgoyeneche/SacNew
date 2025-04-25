using DevExpress.XtraEditors;
using GestionOperativa.Presenters.AdministracionDocumental;
using Shared.Models;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Views.AdministracionDocumental.Relevamientos
{
    public partial class FichaTecnicaUnidades : DevExpress.XtraEditors.XtraForm, IFichaTecnicaUnidadesView
    {
        public FichaTecnicaUnidadesPresenter _presenter;

        public FichaTecnicaUnidades(FichaTecnicaUnidadesPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void CargarTransportistas(List<EmpresaDto> transportistas)
        {
            cmbTransportistas.DataSource = transportistas;
            cmbTransportistas.DisplayMember = "NombreFantasia";
            cmbTransportistas.ValueMember = "IdEmpresa";
            cmbTransportistas.SelectedIndex = -1;
        }

        public void MostrarUnidades(List<UnidadDto> unidades)
        {
            gridControlUnidades.DataSource = unidades;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewUnidades;
            view.Columns["Tractor_Configuracion"].Visible = false;
            view.Columns["IdUnidad"].Visible = false;
            view.Columns["Cuit_Tractor"].Visible = false;
            view.Columns["Semirremolque_Configuracion"].Visible = false;
            view.Columns["Empresa_Semi"].Visible = false;
            view.Columns["Cuit_Semi"].Visible = false;
            view.Columns["Empresa_Tractor"].Visible = false;
            view.Columns["Cuit_Unidad"].Visible = false;
            view.Columns["Tipo_Empresa"].Visible = false;
            view.Columns["TaraTotal"].Visible = false;
            view.Columns["Metanol"].Visible = false;
            view.Columns["Gasoil"].Visible = false;
            view.Columns["LujanCuyo"].Visible = false;
            view.Columns["AptoBo"].Visible = false;
            view.Columns["Calibrado"].Visible = false;
            view.Columns["Checklist"].Visible = false;
            view.Columns["MasYPF"].Visible = false;

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido

            gridViewUnidades.OptionsView.EnableAppearanceEvenRow = true;
            gridViewUnidades.OptionsView.EnableAppearanceOddRow = true;
            gridViewUnidades.Appearance.Row.Font = new Font("Segoe UI", 9);
            gridViewUnidades.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75f);
            gridViewUnidades.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewUnidades.Appearance.Row.Options.UseFont = true;
        }

        public void MostrarTotales(int cantidadTractores, int cantidadSemis, int cantidadUnidades)
        {
            txtTotalTractores.Text = cantidadTractores.ToString();
            txtTotalSemis.Text = cantidadSemis.ToString();
            txtTotalUnidades.Text = cantidadUnidades.ToString();
        }

        public void MostrarPromedios(double promedioTractor, double promedioSemi, double promedioUnidad)
        {
            txtPromedioTractores.Text = $"{promedioTractor:F1}";
            txtPromedioSemis.Text = $"{promedioSemi:F1}";
            txtPromedioUnidades.Text = $"{promedioUnidad:F1}";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async void cmbTransportistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTransportistas.SelectedIndex != -1)
            {
                EmpresaDto empresa = cmbTransportistas.SelectedItem as EmpresaDto;
                _presenter.BuscarAsync(empresa.Cuit);
            }

        }

        private async void bFichaTecnica_Click(object sender, EventArgs e)
        {
            if(gridViewUnidades.GetFocusedRow() != null)
            {
                var row = gridViewUnidades.GetFocusedRow() as UnidadDto;
                _presenter.GenerarFichaTecnica(row.IdUnidad);

            }
            else
            {
                MostrarMensaje("Seleccione una unidad");
            }


        }
    }
}