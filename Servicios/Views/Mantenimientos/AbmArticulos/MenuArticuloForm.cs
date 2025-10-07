using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using GestionFlota.Views.Alertas;
using Servicios.Presenters;
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

namespace Servicios.Views.Mantenimiento
{
    public partial class MenuArticuloForm : DevExpress.XtraEditors.XtraForm, IMenuArticuloView
    {
        public readonly MenuArticuloPresenter _presenter;

        public MenuArticuloForm(MenuArticuloPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarArticulos(List<Articulo> alertas)
        {
            gridControlArt.DataSource = alertas;
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo row = gridViewArt.GetFocusedRow() as Articulo;
            if (row != null)
            {
                await _presenter.EliminarArticulosAsync(row.IdArticulo);
            }
            else
            {
                MostrarMensaje("Seleccione una Articulo para eliminar.");
            }
        }

        private async void btnAgregarNovedad_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarArticulosAsync();
        }

        private async void btnEditarNovedad_Click(object sender, EventArgs e)
        {
            Articulo row = gridViewArt.GetFocusedRow() as Articulo;
            if (row != null)
            {
                await _presenter.EditarArticulosAsync(row.IdArticulo);
            }
            else
            {
                MostrarMensaje("Seleccione una Articulo para editar.");
            }
        }
    }
}