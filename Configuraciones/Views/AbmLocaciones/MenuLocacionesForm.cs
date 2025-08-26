using DevExpress.XtraEditors;
using SacNew.Presenters;
using SacNew.Views.Configuraciones.AbmLocaciones;
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

namespace Configuraciones.Views.AbmLocaciones
{
    public partial class MenuLocacionesForm : DevExpress.XtraEditors.XtraForm, IMenuLocacionesView
    {
        private readonly MenuLocacionesPresenter _presenter;

        public MenuLocacionesForm(MenuLocacionesPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void MenuLocaciones_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();
        }

        public void CargarLocaciones(List<Locacion> locaciones)
        {
            gridControlLocaciones.DataSource = locaciones;

            var view = gridViewLocaciones;

            view.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (gridViewLocaciones.GetFocusedRow() is Locacion locacion)
            {
                await _presenter.EliminarLocacionAsync(locacion.IdLocacion);
            }
            else
            {
                MostrarMensaje("Seleccione una locación para eliminar.");
            }
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private async void btnPool_Click(object sender, EventArgs e)
        {
            if (gridViewLocaciones.GetFocusedRow() is Locacion locacion)
            {
                if (locacion.Carga == true)
                {
                    await _presenter.AbrirLocacionPool(locacion.IdLocacion);
                }
                else
                {
                    MostrarMensaje("La locación no es de carga.");
                }
            }
            else
            {
                MostrarMensaje("Seleccione una locación para editar.");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridViewLocaciones.GetFocusedRow() is Locacion locacion)
            {
                await _presenter.EditarLocacion(locacion.IdLocacion);
            }
            else
            {
                MostrarMensaje("Seleccione una locación para editar.");
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarLocacion();
        }
    }
}