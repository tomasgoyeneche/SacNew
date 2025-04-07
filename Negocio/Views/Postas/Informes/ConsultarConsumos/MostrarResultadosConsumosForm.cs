using GestionFlota.Presenters.Informes;
using Shared.Models;

namespace GestionFlota.Views.Postas.Informes.ConsultarConsumos
{
    public partial class MostrarResultadosConsumosForm : Form, IResultadosConsumoView
    {
        private readonly ResultadosConsumoPresenter _presenter;
        private List<InformeConsumoPocDto> _resultados = new();

        public MostrarResultadosConsumosForm(ResultadosConsumoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarResultados(List<InformeConsumoPocDto> resultados)
        {
            _resultados = resultados;
            dataGridViewResultados.DataSource = resultados;

            ConfigurarColumnas();
            _presenter.CalcularYMostrarTotales(resultados);
        }

        public void MostrarTotales(List<TotalConsumoDto> totales)
        {
            dataGridViewTotales.DataSource = totales;

            // Configuración visual del grid
            dataGridViewTotales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public List<InformeConsumoPocDto> ObtenerResultados()
        {
            return _resultados;
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await _presenter.ExportarAExcelAsync();
        }

        private void ConfigurarColumnas()
        {
            var columnasVisibles = new List<string>
        {
            "NumeroPoc", "Chofer_Nombre", "Codigo_Posta", "Empresa_Nombre", "Tractor_Patente", "Semi_Patente",
            "Concepto_Codigo", "Odometro", "Comentario", "FechaCreacion", "FechaCierre", "Usuario", "NumeroVale", "LitrosAutorizados", "LitrosCargados", "Observaciones", "Dolar",
            "PrecioTotal", "FechaCarga", "Estado"
        };

            foreach (DataGridViewColumn col in dataGridViewResultados.Columns)
            {
                col.Visible = columnasVisibles.Contains(col.Name);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}