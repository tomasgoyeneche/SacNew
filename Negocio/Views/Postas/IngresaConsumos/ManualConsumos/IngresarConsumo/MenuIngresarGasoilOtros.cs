using GestionFlota.Presenters;
using Shared.Models.DTOs;
using System.Globalization;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public partial class MenuIngresarGasoilOtros : Form, IMenuIngresaGasoilOtrosView
    {
        public readonly MenuIngresaGasoilOtrosPresenter _presenter;

        public MenuIngresarGasoilOtros(MenuIngresaGasoilOtrosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string NumeroPoc
        {
            get => txtNumeroPoc.Text;
            set => txtNumeroPoc.Text = value;
        }

        public int IdPoc { get; set; }

        public string CreditoTotal
        {
            get => txtCreditoTotal.Text;
            set => txtCreditoTotal.Text = value;
        }

        public string CreditoEnPoc
        {
            get => txtConsumoEnPoc.Text;
            set => txtConsumoEnPoc.Text = value;
        }

        public string CreditoConsumido
        {
            get => txtCreditoConsumido.Text;
            set => txtCreditoConsumido.Text = value;
        }

        public string CreditoDisponible
        {
            get => txtCreditoDisponible.Text;
            set => txtCreditoDisponible.Text = value;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void MostrarNombreUsuario(string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        private async void bIngresaGasoil_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirGasoilAutorizadoAsync();
        }

        private async void bIngresaYpfRuta_Click(object sender, EventArgs e)
        {
            //await _presenter.AbrirConsumosenYpfEnRutaAsync(IdPoc);
        }

        private async void bIngresaOtrosConsumos_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirOtrosConsumos();
        }

        public void MostrarConsumos(List<ConsumosUnificadosDto> consumos, POCDto poc)
        {
            dtpCierrePoc.Value = DateTime.Now;
            txtTractor.Text = poc.PatenteTractor;
            txtNombreChofer.Text = poc.NombreCompletoChofer;    
            txtSemi.Text = poc.PatenteSemi; 
            txtTanque.Text = poc.CapacidadTanque.ToString();
            labelEstado.Text = poc.Estado.ToUpper();


            dataGridViewConsumos.DataSource = consumos.Select(c => new
            {
                c.IdConsumo,
                c.Descripcion,
                c.NumeroVale,
                c.LitrosAutorizados,
                c.Cantidad,
                ImporteTotal = c.ImporteTotal.HasValue
            ? $"${c.ImporteTotal.Value.ToString("N2", new CultureInfo("es-AR"))}" // Agregar símbolo $ y formatear
            : "",
                c.Aclaraciones,
                c.FechaRemito,
                c.tipoConsumo // Asegúrate de incluir este campo
            }).ToList();

            // Ocultar columna tipoConsumo
            if (dataGridViewConsumos.Columns["tipoConsumo"] != null)
            {
                dataGridViewConsumos.Columns["IdConsumo"].Visible = false;

                dataGridViewConsumos.Columns["tipoConsumo"].Visible = false;
            }
        }

        private void ConfigurarEstilos()
        {
            foreach (DataGridViewRow fila in dataGridViewConsumos.Rows)
            {
                var tipoConsumo = Convert.ToInt32(fila.Cells["tipoConsumo"].Value);

                // Aplicar colores según tipoConsumo
                switch (tipoConsumo)
                {
                    case 1:
                        fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 150); // Naranja pastel moderado
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    case 2:
                        fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 160, 180); // Rosa suave
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    case 3:
                        fila.DefaultCellStyle.BackColor = Color.FromArgb(135, 206, 250); // Azul cielo claro
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    default:
                        fila.DefaultCellStyle.BackColor = Color.White;
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            }

            if (decimal.TryParse(txtCreditoDisponible.Text.Replace("$", "").Trim(), out var creditoDisponible))
            {
                if (creditoDisponible < 0)
                {
                    panelFondo.FillColor = Color.Red;
                }
                else
                {
                    panelFondo.FillColor = Color.LightGreen;
                }
            }
            else
            {
                panelFondo.BackColor = Color.White; // Valor no válido
            }
        }

        private void dataGridViewConsumos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ConfigurarEstilos();
        }

        private async void btnCerrarPoc_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
        "¿Está seguro de que desea cerrar la POC? Esta acción no se puede deshacer.",
        "Confirmar cierre de POC",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    );

            if (confirmacion == DialogResult.Yes)
            {
                await _presenter.CerrarPocAsync(IdPoc, dtpCierrePoc.Value);
                this.Close();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewConsumos.SelectedRows.Count > 0)
            {
                int idConsumo = Convert.ToInt32(dataGridViewConsumos.SelectedRows[0].Cells["IdConsumo"].Value);
                int tipoConsumo = Convert.ToInt32(dataGridViewConsumos.SelectedRows[0].Cells["tipoConsumo"].Value);
                string importeTexto = dataGridViewConsumos.SelectedRows[0].Cells["ImporteTotal"].Value?.ToString() ?? "0";
                decimal importeTotal = 0;

                if (decimal.TryParse(importeTexto.Replace("$", "").Replace(".", "").Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal resultado))
                {
                    importeTotal = resultado;
                }

                var confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este consumo? Esta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion == DialogResult.Yes)
                {
                    await _presenter.EliminarConsumo(idConsumo, tipoConsumo, importeTotal);
                }
            }
            else
            {
                MostrarMensaje("Seleccione un consumo para eliminar.");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewConsumos.SelectedRows.Count > 0)
            {
                int idConsumo = Convert.ToInt32(dataGridViewConsumos.SelectedRows[0].Cells["IdConsumo"].Value);
                int tipoConsumo = Convert.ToInt32(dataGridViewConsumos.SelectedRows[0].Cells["tipoConsumo"].Value);

                await _presenter.EditarConsumoOtros(idConsumo, tipoConsumo);
            }
            else
            {
                MostrarMensaje("Seleccione un consumo para eliminar.");
            }
        }
    }
}