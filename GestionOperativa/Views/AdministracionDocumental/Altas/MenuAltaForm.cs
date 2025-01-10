using GestionOperativa.Presenters;
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
    public partial class MenuAltaForm : Form, IMenuAltasView
    {
        public readonly MenuAbmEntidadPresenter _presenter;

        public MenuAltaForm(MenuAbmEntidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async Task CargarEntidades(string entidad)
        {
            _presenter.SetEntidad(entidad);

            try
            {
                await _presenter.CargarEntidadesAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar {ex.Message}");
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public string TextoBusqueda => txtBuscar.Text.Trim();

        public void MostrarEntidades<T>(List<T> entidades)
        {
            dataGridViewEntidades.DataSource = entidades;
            _presenter.ConfigurarColumnas(dataGridViewEntidades);
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                await _presenter.BuscarEntidadesAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar {ex.Message}");
            }
        }

        private async void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await _presenter.BuscarEntidadesAsync();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar {ex.Message}");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
