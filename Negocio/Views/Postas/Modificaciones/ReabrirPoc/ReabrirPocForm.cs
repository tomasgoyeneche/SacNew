using DevExpress.XtraEditors;
using GestionFlota.Presenters;
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

namespace GestionFlota.Views.Postas.Modificaciones.ReabrirPoc
{
    public partial class ReabrirPocForm : DevExpress.XtraEditors.XtraForm, IReabrirPocView
    {
        private readonly ReabrirPocPresenter _presenter;

        public ReabrirPocForm(ReabrirPocPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void bReabrirPoc_Click(object sender, EventArgs e)
        {
            var row = gridViewPOC.GetFocusedRow() as POCDto;
            if (row != null)
            {
                await _presenter.ReabrirPOCAsync(row.IdPoc);
            }
            else
            {
                MostrarMensaje("Seleccione una POC para eliminar.");
            }
        }

        public void MostrarPOC(List<POCDto> listaPOC)
        {
            gridControlPOC.DataSource = listaPOC;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewPOC;
            view.Columns["IdPoc"].Visible = false;
            view.Columns["IdPosta"].Visible = false;
            view.Columns["CapacidadTanque"].Visible = false;
            view.Columns["Estado"].Visible = false;


            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido

            gridViewPOC.OptionsView.EnableAppearanceEvenRow = true;
            gridViewPOC.OptionsView.EnableAppearanceOddRow = true;
            gridViewPOC.Appearance.Row.Font = new Font("Segoe UI", 9);
            gridViewPOC.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75f);
            gridViewPOC.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewPOC.Appearance.Row.Options.UseFont = true;
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }


        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void ReabrirPocForm_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }
    }
}