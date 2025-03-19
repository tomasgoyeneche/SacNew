using GestionOperativa.Presenters.AdministracionDocumental;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class MenuAltaNominaForm : Form, IMenuAltaNominaView
    {
        public MenuAltaNominaPresenter _presenter;

        public MenuAltaNominaForm(MenuAltaNominaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void UnidadListadoForm_Load(object sender, EventArgs e)
        {
            await _presenter.CargarEmpresasAsync();
        }

        public void CargarEmpresas(List<object> empresas)
        {
            cmbEmpresas.DisplayMember = "Nombre";
            cmbEmpresas.ValueMember = "Cuit";
            cmbEmpresas.DataSource = empresas;
            cmbEmpresas.SelectedIndex = -1; // Sin selección al inicio
        }

        public void MostrarUnidades(List<UnidadDto> unidades)
        {
            dataGridViewUnidades.DataSource = unidades;
            ConfigurarColumnasDataGrid();
        }

        public void MostrarTransportista(string transportista)
        {
            lTransportista.Text = transportista;
        }

        public void MostrarSubTransportista(string subTransportista)
        {
            lSubTransportista.Text = subTransportista;
        }

        public UnidadDto? ObtenerUnidadSeleccionada()
        {
            return dataGridViewUnidades.SelectedRows.Count > 0
                ? dataGridViewUnidades.SelectedRows[0].DataBoundItem as UnidadDto
                : null;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConfigurarColumnasDataGrid()
        {
            dataGridViewUnidades.AutoGenerateColumns = false;

            // Definir las columnas manualmente
            dataGridViewUnidades.Columns.Clear();

            dataGridViewUnidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tractor_Patente",
                HeaderText = "Tractor",
                DataPropertyName = "Tractor_Patente",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewUnidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Semirremolque_Patente",
                HeaderText = "Semi",
                DataPropertyName = "Semirremolque_Patente",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewUnidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Metanol",
                HeaderText = "Metanol",
                DataPropertyName = "Metanol",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewUnidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gasoil",
                HeaderText = "Gasoil",
                DataPropertyName = "Gasoil",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewUnidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LujanCuyo",
                HeaderText = "Luján Cuyo",
                DataPropertyName = "LujanCuyo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewUnidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AptoBo",
                HeaderText = "Apto BO",
                DataPropertyName = "AptoBo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Evento para formatear las columnas booleanas
            dataGridViewUnidades.CellFormatting += (sender, e) =>
            {
                if (e.Value is bool booleanValue)
                {
                    e.Value = booleanValue ? "Sí" : "No";
                    e.FormattingApplied = true;
                }
            };
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpresas.SelectedIndex != -1)
            {
                var nombreEmpresa = cmbEmpresas.Text;
                _presenter.FiltrarPorEmpresa(nombreEmpresa);
            }
        }

        private async void btnBuscarPatente_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarPorPatenteAsync(txtBuscarPatente.Text);
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            await _presenter.EditarUnidadAsync();
        }

        private async void bNominaMetanol_Click(object sender, EventArgs e)
        {
            _presenter.GenerarReporteFlotaAsync();
        }

        private async void btnEliminarUnidad_Click(object sender, EventArgs e)
        {
            await _presenter.EliminarUnidadAsync();
        }
    }
}