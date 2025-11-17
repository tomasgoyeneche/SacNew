using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Servicios.Presenters;
using Shared.Models;
using System.IO;

namespace Servicios.Views.Mantenimientos.OrdenesTrabajo
{
    public partial class EdicionOrdenTrabajoForm : DevExpress.XtraEditors.XtraForm, IEditarOrdenTrabajoView
    {
        public readonly EditarOrdenTrabajoPresenter _presenter;

        public EdicionOrdenTrabajoForm(EditarOrdenTrabajoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        // =====================================================
        // 🧩 Propiedades
        // =====================================================

        public int IdOrdenTrabajo
        {
            get => Convert.ToInt32(txtIdOrden.Text);
            set => txtIdOrden.Text = value.ToString();
        }

        public DateTime FechaEmision
        {
            get => (DateTime)dateFechaEmision.EditValue;
            set => dateFechaEmision.EditValue = value;
        }

        public int? IdUnidad
        {
            get => Convert.ToInt32(cmbUnidad.EditValue == null ? 0 : cmbUnidad.EditValue);
            set => cmbUnidad.EditValue = value;
        }

        public int? IdNomina { get; set; }

        public int? IdLugarReparacion
        {
            get => Convert.ToInt32(cmbLugarMantenimiento.EditValue == null ? 0 : cmbLugarMantenimiento.EditValue);
            set => cmbLugarMantenimiento.EditValue = value;
        }

        public DateTime? FechaIngreso
        {
            get => dtpFechaIngreso.EditValue as DateTime?;
            set => dtpFechaIngreso.EditValue = value;
        }

        public DateTime? FechaFin
        {
            get => dtpFechaFin.EditValue as DateTime?;
            set => dtpFechaFin.EditValue = value;
        }

        public decimal? OdometroIngreso
        {
            get => Convert.ToDecimal(txtOdometroIng.EditValue ?? 0);
            set => txtOdometroIng.EditValue = value;
        }

        public decimal? OdometroSalida
        {
            get => Convert.ToDecimal(txtOdometroFin.EditValue ?? 0);
            set => txtOdometroFin.EditValue = value;
        }

        public decimal? Horas
        {
            get => Convert.ToDecimal(txtHoras.EditValue ?? 0);
            set => txtHoras.EditValue = value;
        }

        public decimal? Costo
        {
            get => Convert.ToDecimal(txtManoObra.EditValue ?? 0);
            set => txtManoObra.EditValue = value;
        }

        public string Observaciones
        {
            get => txtDescripcion.Text.Trim();
            set => txtDescripcion.Text = value;
        }

        // =====================================================
        // 🧩 Carga de combos
        // =====================================================

        public void CargarUnidades(List<UnidadDto> unidades)
        {
            cmbUnidad.Properties.DataSource = unidades;
            cmbUnidad.Properties.DisplayMember = "PatenteCompleta";
            cmbUnidad.Properties.ValueMember = "IdUnidad";
            cmbUnidad.Properties.NullText = "Seleccione Unidad...";
            cmbUnidad.Properties.Columns.Clear();
            cmbUnidad.Properties.Columns.Add(new LookUpColumnInfo("PatenteCompleta", "Unidad"));
        }

        public void CargarLugares(List<LugarReparacion> lugares)
        {
            cmbLugarMantenimiento.Properties.DataSource = lugares;
            cmbLugarMantenimiento.Properties.DisplayMember = "Nombre";
            cmbLugarMantenimiento.Properties.ValueMember = "IdLugarReparacion";
            cmbLugarMantenimiento.Properties.NullText = "Seleccione...";
            cmbLugarMantenimiento.Properties.Columns.Clear();
            cmbLugarMantenimiento.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Lugar"));
        }

        public void CargarComprobantes(List<OrdenTrabajoComprobante> comprobantes)
        {
            gridControlComprobantes.DataSource = comprobantes;
            gridViewComprobantes.BestFitColumns();
        }

        // =====================================================
        // 🧩 Control de botones
        // =====================================================

        public void CargarMantenimientosPredefinidos(List<Shared.Models.Mantenimiento> mantenimientos)
        {
            cmbMantenimiento.Properties.DataSource = mantenimientos;
            cmbMantenimiento.Properties.DisplayMember = "Nombre";
            cmbMantenimiento.Properties.ValueMember = "IdMantenimiento";
            cmbMantenimiento.Properties.NullText = "Seleccione un mantenimiento...";
            cmbMantenimiento.Properties.Columns.Clear();
            cmbMantenimiento.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Mantenimiento"));
            cmbMantenimiento.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Descripción"));
        }

        public void CargarMantenimientosOrden(List<OrdenTrabajoMantenimiento> mantenimientos)
        {
            gridControlMantenimientos.DataSource = mantenimientos;
            gridViewMantenimientos.BestFitColumns();
        }

        // ======================================
        // 🟨 Banner visual de Fase
        // ======================================

        public void ActualizarBannerFase(int fase)
        {
            string texto = "";
            Color color = Color.LightGray;

            switch (fase)
            {
                case 0:
                    texto = "FASE: CREADA";
                    color = Color.LightSteelBlue;
                    break;

                case 1:
                    texto = "FASE: AUTORIZADA";
                    color = Color.Goldenrod;
                    break;

                case 2:
                    texto = "FASE: EN TALLER";
                    color = Color.CornflowerBlue;
                    break;

                case 3:
                    texto = "FASE: FINALIZADA";
                    color = Color.SeaGreen;
                    break;
            }

            lblFase.Text = texto;
        }

        public void ActualizarEstadoUI(int fase)
        {
            ActualizarBannerFase(fase);
            // 🔹 Reiniciar todo
            cmbUnidad.Enabled = false;
            btnAutorizar.Enabled = false;
            cmbLugarMantenimiento.Enabled = false;
            btnIngreso.Enabled = false;
            dtpFechaIngreso.Enabled = false;
            txtOdometroIng.Enabled = false;
            bFinalizo.Enabled = false;
            dtpFechaFin.Enabled = false;
            txtOdometroFin.Enabled = false;
            cmbMantenimiento.Enabled = false;
            btnAgregarMantenimiento.Enabled = false;

            switch (fase)
            {
                case 0: // Creada → solo se puede autorizar
                    cmbUnidad.Enabled = true;
                    btnAutorizar.Enabled = true;
                    break;

                case 1: // Autorizada → habilita lugar y botón de ingreso
                    cmbLugarMantenimiento.Enabled = true;
                    btnIngreso.Enabled = true;
                    break;

                case 2: // En Taller → habilita odómetro ingreso y botón de finalización
                    dtpFechaIngreso.Enabled = true;
                    txtOdometroIng.Enabled = true;
                    bFinalizo.Enabled = true;
                    cmbMantenimiento.Enabled = true;
                    btnAgregarMantenimiento.Enabled = true;
                    break;

                case 3: // Finalizada → habilita campos finales
                    dtpFechaFin.Enabled = true;
                    txtOdometroFin.Enabled = true;
                    break;
            }
        }

        // =====================================================
        // 🧩 Eventos
        // =====================================================

        private async void btnAutorizar_Click(object sender, EventArgs e)
        {
            btnAutorizar.Enabled = false;
            if (IdUnidad.HasValue && IdUnidad.Value > 0)
                await _presenter.AutorizarAsync(IdUnidad.Value);
            else
                MostrarMensaje("Debe seleccionar una unidad antes de autorizar.");
            btnAutorizar.Enabled = true;
        }

        public void LimpiarFormulario()
        {
            txtIdOrden.Text = "0";
            dateFechaEmision.EditValue = DateTime.Now;
            cmbUnidad.EditValue = null;
            cmbLugarMantenimiento.EditValue = null;
            dtpFechaIngreso.EditValue = null;
            dtpFechaFin.EditValue = null;
            txtOdometroIng.EditValue = null;
            txtOdometroFin.EditValue = null;
            txtHoras.EditValue = null;
            txtManoObra.EditValue = null;
            txtDescripcion.Text = string.Empty;
            gridViewComprobantes.ClearColumnsFilter();
            gridViewMantenimientos.ClearColumnsFilter();
        }

        private async void btnIngreso_Click(object sender, EventArgs e)
        {
            if (IdLugarReparacion == 0)
            {
                MostrarMensaje("Seleccione un lugar de reparacion primero");
                return;
            }
            btnIngreso.Enabled = false;
            await _presenter.ConfirmarIngresoAsync();
            btnIngreso.Enabled = true;
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Cerrar() => Dispose();

        private void bCancelar_Click(object sender, EventArgs e)
        {
            bCancelar.Enabled = false;
            Dispose();
            bCancelar.Enabled = true;
        }

        private async void bFinalizo_Click(object sender, EventArgs e)
        {
            bFinalizo.Enabled = false;
            await _presenter.ConfirmarSalidaAsync();
            bFinalizo.Enabled = true;
        }

        private async void bAgregarCom_Click(object sender, EventArgs e)
        {
            bAgregarCom.Enabled = false;
            await _presenter.AgregarComprobanteAsync();
            bAgregarCom.Enabled = true;
        }

        private async void bEditarCom_Click(object sender, EventArgs e)
        {
            OrdenTrabajoComprobante row = gridViewComprobantes.GetFocusedRow() as OrdenTrabajoComprobante;
            if (row != null)
            {
                await _presenter.EditarComprobanteAsync(row.IdOrdenTrabajoComprobante);
            }
            else
            {
                MostrarMensaje("Seleccione un Articulo para editar.");
            }
        }

        private void gridViewComprobantes_DoubleClick(object sender, EventArgs e)
        {
            OrdenTrabajoComprobante comprobante = gridViewComprobantes.GetFocusedRow() as OrdenTrabajoComprobante;
            if (comprobante == null) return;

            if (!string.IsNullOrWhiteSpace(comprobante.RutaComprobante) && File.Exists(comprobante.RutaComprobante))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = comprobante.RutaComprobante,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"No se pudo abrir el archivo.\n\n{ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("El archivo no existe en la ruta especificada.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void bGuardar_Click(object sender, EventArgs e)
        {
            bGuardar.Enabled = false;
            await _presenter.GuardarAsync();
            bGuardar.Enabled = true;
        }

        private async void btnAgregarMantenimiento_Click(object sender, EventArgs e)
        {
            btnAgregarMantenimiento.Enabled = false;
            if (cmbMantenimiento.EditValue == null)
            {
                MostrarMensaje("Seleccione un mantenimiento primero.");
                return;
            }

            int idMantenimiento = Convert.ToInt32(cmbMantenimiento.EditValue);
            await _presenter.AgregarMantenimientoAsync(idMantenimiento);
            btnAgregarMantenimiento.Enabled = true;
        }

        private async void bAgregarMan_Click(object sender, EventArgs e)
        {
            var opcion = XtraMessageBox.Show(
               "¿El Mantenimiento es Preventivo?",
               "Nuevo Mantenimiento",
               MessageBoxButtons.YesNoCancel,
               MessageBoxIcon.Question
           );

            if (opcion == DialogResult.Cancel) return;

            int tipoMovimiento = opcion == DialogResult.Yes ? 1 : 2; // 1=Entrada, 2=Salida

            // Crear el movimiento en la BD con datos iniciales
            int idMovimiento = await _presenter.CrearMantenimientoAsync(tipoMovimiento);

            // Abrir el form de edición directamente
            await _presenter.AbrirEdicionMantenimientoAsync(idMovimiento);
        }

        private async void bEditarComprobante_Click(object sender, EventArgs e)
        {
            OrdenTrabajoMantenimiento row = gridViewMantenimientos.GetFocusedRow() as OrdenTrabajoMantenimiento;
            if (row != null)
            {
                await _presenter.AbrirEdicionMantenimientoAsync(row.IdOrdenTrabajoMantenimiento);
            }
            else
            {
                MostrarMensaje("Seleccione un Mantenimiento para editar.");
            }
        }

        private async void bEliminarCom_Click(object sender, EventArgs e)
        {
            OrdenTrabajoComprobante row = gridViewComprobantes.GetFocusedRow() as OrdenTrabajoComprobante;
            if (row != null)
            {
                var confirm = MessageBox.Show("¿Eliminar este Comprobante?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    await _presenter.EliminarComprobanteAsync(row.IdOrdenTrabajoComprobante, row.RutaComprobante);
                }
            }
            else
            {
                MostrarMensaje("Seleccione un Articulo para editar.");
            }
        }

        private async void bEliminarMantenimiento_Click(object sender, EventArgs e)
        {
            OrdenTrabajoMantenimiento row = gridViewMantenimientos.GetFocusedRow() as OrdenTrabajoMantenimiento;
            if (row != null)
            {
                var confirm = MessageBox.Show("¿Eliminar este Mantenimiento?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    await _presenter.EliminarMantenimiento(row.IdOrdenTrabajoMantenimiento);
                }
            }
            else
            {
                MostrarMensaje("Seleccione un Mantenimiento para eliminar.");
            }
        }
    }
}