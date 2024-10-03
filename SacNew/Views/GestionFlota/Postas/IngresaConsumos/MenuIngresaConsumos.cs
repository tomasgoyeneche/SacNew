using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models.DTOs;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class MenuIngresaConsumos : Form, IMenuIngresaConsumosView
    {
        private readonly MenuIngresaConsumosPresenter _presenter;
        private readonly IServiceProvider _serviceProvider;

        public MenuIngresaConsumos(MenuIngresaConsumosPresenter presenter, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
            _serviceProvider = serviceProvider;
        }

        public string CriterioBusqueda => txtBuscar.Text.Trim();

        // Implementación de IMenuIngresaConsumosView
        public void MostrarPOC(List<POCDto> listaPOC)
        {
            dataGridViewPOC.DataSource = listaPOC;
            dataGridViewPOC.Columns["IdPoc"].Visible = false;  // Ocultar el ID
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void MostrarNombreUsuario(string nombre)
        {
            lNombreUsuario.Text = nombre;
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        // Eventos
        private void MenuIngresaConsumos_Load(object sender, EventArgs e)
        {
            _presenter.Inicializar();  // Cargar datos iniciales
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _presenter.BuscarPOC(CriterioBusqueda);  // Pasar el criterio de búsqueda al presenter
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _presenter.BuscarPOC(CriterioBusqueda);  // Búsqueda dinámica al escribir
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPOC.SelectedRows.Count > 0)
            {
                int idPoc = Convert.ToInt32(dataGridViewPOC.SelectedRows[0].Cells["IdPoc"].Value);
                _presenter.EliminarPOC(idPoc);  // Llamar al presenter para eliminar
            }
            else
            {
                MostrarMensaje("Seleccione una POC para eliminar.");
            }
        }

        private void btnAgregarPOC_Click(object sender, EventArgs e)
        {
            using (var agregarEditarPOC = _serviceProvider.GetService<AgregarEditarPoc >())
            {
                agregarEditarPOC.ShowDialog();
            }
        }

        private void btnEditarPOC_Click(object sender, EventArgs e)
        {
            if (dataGridViewPOC.SelectedRows.Count > 0)
            {
                // Obtener el IdPoc de la fila seleccionada
                int idPoc = Convert.ToInt32(dataGridViewPOC.SelectedRows[0].Cells["IdPoc"].Value);

                // Obtener la POC desde el Presenter
                var poc = _presenter.ObtenerPOCPorId(idPoc);

                // Abrir el formulario de edición y pasar los datos
                _presenter.EditarPOC(poc);  
            }
            else
            {
                MessageBox.Show("Seleccione una POC para editar.");
            }
        }
    }
}
