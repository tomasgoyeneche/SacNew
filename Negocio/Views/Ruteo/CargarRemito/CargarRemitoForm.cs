using DevExpress.XtraEditors.Controls;
using GestionFlota.Presenters.Ruteo;
using GestionFlota.Views.Ruteo;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class CargarRemitoForm : DevExpress.XtraEditors.XtraForm, ICargaRemitoView
    {
        public readonly CargaRemitoPresenter _presenter;

        public CargarRemitoForm(CargaRemitoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int? IdMedida => Convert.ToInt32(cmbMedida.EditValue) == 0 ? (int?)null : Convert.ToInt32(cmbMedida.EditValue);
        public int? IdOrigen => Convert.ToInt32(cmbCarga.EditValue) == 0 ? (int?)null : Convert.ToInt32(cmbCarga.EditValue);
        public int? IdProducto => Convert.ToInt32(cmbProducto.EditValue) == 0 ? (int?)null : Convert.ToInt32(cmbProducto.EditValue);
        public string RemitoNumero => txtRemito.Text.Trim();
        public int? Cantidad => Convert.ToInt32(txtTotal.Text.Trim());
        public DateTime? FechaRemito => dtpFechaRemito.EditValue as DateTime?;

        public void CargarLocaciones(List<Locacion> locaciones)
        {
            cmbEntrega.Properties.DataSource = locaciones;
            cmbEntrega.Properties.DisplayMember = "Nombre";
            cmbEntrega.Properties.ValueMember = "IdLocacion";

            cmbCarga.Properties.DataSource = locaciones;
            cmbCarga.Properties.DisplayMember = "Nombre";
            cmbCarga.Properties.ValueMember = "IdLocacion";

            cmbCarga.Properties.Columns.Clear();
            cmbCarga.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Origen"));

            cmbEntrega.Properties.Columns.Clear();
            cmbEntrega.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Destino"));
        }

        public void CargarDatosIniciales(Programa programa, Shared.Models.Ruteo ruteo, string tipoRemito)
        {
            lblChofer.Text = ruteo.Chofer ?? "No asignado";
            lblTractor.Text = $"{ruteo.Tractor}-{ruteo.Semi}";

            if (tipoRemito == "Carga")
            {
                txtRemito.Text = programa.CargaRemito?.ToString() ?? "0";
                txtTotal.Text = programa.CargaRemitoKg?.ToString() ?? "0";
                cmbCarga.EditValue = programa.IdOrigen;
                cmbEntrega.EditValue = ruteo.IdDestino;
                txtAlbaran.Text = programa.AlbaranDespacho.ToString() ?? string.Empty;
                cmbProducto.EditValue = programa.IdProducto;
                cmbMedida.EditValue = programa.CargaRemitoUnidad;
                dtpFechaRemito.EditValue = programa.CargaRemitoFecha ?? DateTime.Now; // Asignar fecha actual si no hay
            }
            else if (tipoRemito == "Entrega")
            {
                txtRemito.Text = programa.EntregaRemito?.ToString() ?? "0";
                txtTotal.Text = programa.EntregaRemitoKg?.ToString() ?? "0";
                cmbCarga.EditValue = programa.IdOrigen; // Origen es el destino del ruteo
                cmbEntrega.EditValue = ruteo.IdDestino;
                txtAlbaran.Text = programa.AlbaranDespacho.ToString() ?? string.Empty;
                cmbProducto.EditValue = programa.IdProducto;
                cmbMedida.EditValue = programa.EntregoRemitoUnidad;
                dtpFechaRemito.EditValue = programa.EntregaRemitoFecha ?? DateTime.Now; // Asignar fecha actual si no hay
            }
        }

        public void CargarMedidas(List<Medida> medidas)
        {
            cmbMedida.Properties.DataSource = medidas;
            cmbMedida.Properties.DisplayMember = "Descripcion";
            cmbMedida.Properties.ValueMember = "IdMedida";

            cmbMedida.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbMedida.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Medida"));
        }

        public void CargarProductos(List<Producto> productos)
        {
            cmbProducto.Properties.DataSource = productos;
            cmbProducto.Properties.DisplayMember = "Nombre";
            cmbProducto.Properties.ValueMember = "IdProducto";

            cmbProducto.Properties.Columns.Clear(); // 🔥 Borra todo
            cmbProducto.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "IdProducto"));
        }

        public void Close()
        {
            Dispose();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }
    }
}