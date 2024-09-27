using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;
using SacNew.Repositories;
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

namespace SacNew.Views.GestionFlota.Postas.ABMPostas
{
    public partial class MenuAbmPostas : Form, IMenuAbmPostasView
    {
        private readonly MenuAbmPostasPresenter _presenter;

        public MenuAbmPostas(IPostaRepositorio postaRepositorio, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _presenter = new MenuAbmPostasPresenter(this, postaRepositorio, serviceProvider);

            // Cargar las postas al iniciar el formulario
            _presenter.CargarPostas();
        }


        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public string TextoBusqueda => txtBuscar.Text.Trim();

        public void MostrarPostas(List<Posta> postas)
        {
            dataGridViewPostas.DataSource = postas;

            // Mostrar solo las columnas específicas
            dataGridViewPostas.Columns["Id"].Visible = false;
            dataGridViewPostas.Columns["ProvinciaId"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _presenter.BuscarPostas();
        }

        private void loginCloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            _presenter.AgregarPosta();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPostas.SelectedRows.Count > 0)
            {
                var postaSeleccionada = (Posta)dataGridViewPostas.SelectedRows[0].DataBoundItem;
                _presenter.EditarPosta(postaSeleccionada);
            }
            else
            {
                MostrarMensaje("Seleccione una posta para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPostas.SelectedRows.Count > 0)
            {
                var postaSeleccionada = (Posta)dataGridViewPostas.SelectedRows[0].DataBoundItem;
                _presenter.EliminarPosta(postaSeleccionada);
            }
            else
            {
                MostrarMensaje("Seleccione una posta para eliminar.");
            }
        }
    }
}
