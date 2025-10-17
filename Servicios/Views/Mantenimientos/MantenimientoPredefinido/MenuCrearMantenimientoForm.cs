using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using Servicios.Presenters;
using Servicios.Views.Mantenimientos;
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
    public partial class MenuCrearMantenimientoForm : DevExpress.XtraEditors.XtraForm, IMenuCrearMantenimientoView
    {
        public readonly MenuCrearMantenimientoPresenter _presenter;

        public MenuCrearMantenimientoForm(MenuCrearMantenimientoPresenter presenter)
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
            Cerrar();
        }

        private async void btnAgregarTarea_Click(object sender, EventArgs e)
        {
            if (cmbTarea.EditValue is int idTarea && idTarea > 0)
                await _presenter.AgregarTareaAsync(idTarea);
            else
                MostrarMensaje("Debe seleccionar una tarea válida.");
        }

        private async void btnQuitarTarea_Click(object sender, EventArgs e)
        {
            var tarea = gridViewTareas.GetFocusedRow() as Tarea;
            if (tarea != null)
                await _presenter.EliminarTareaAsync(tarea.IdTarea);
        }

        private void cmbFrecuencia_EditValueChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadIntervalo();
        }

        private void ActualizarVisibilidadIntervalo()
        {
            string seleccion = cmbFrecuencia.EditValue.ToString();

            if (seleccion.Equals("Días", StringComparison.OrdinalIgnoreCase))
            {
                txtIntervalo.Visible = true;
                lblIntervalo.Visible = true;
                lblIntervalo.Text = "Dias:";
            }
            else if (seleccion.Equals("Kilómetros", StringComparison.OrdinalIgnoreCase))
            {
                txtIntervalo.Visible = true;
                lblIntervalo.Visible = true;
                lblIntervalo.Text = "Km:";
            }
            else // Indefinido
            {
                txtIntervalo.Visible = false;
                lblIntervalo.Visible = false;
            }
        }

        public void Cerrar() => Dispose();

        public void SeleccionarFrecuencia(string frecuencia, int? valorIntervalo)
        {
            if (cmbFrecuencia.Properties.DataSource == null)
                return;

            cmbFrecuencia.EditValue = frecuencia;
            txtIntervalo.EditValue = valorIntervalo ?? 0;
            ActualizarVisibilidadIntervalo();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =====================================================
        // 🔹 Propiedades expuestas al Presenter
        // =====================================================

        public int IdMantenimiento { get; set; }
        public string TipoVista { get; set; } = "MantenimientoPredefinido";

        public string Nombre
        {
            get => txtNombre.Text.Trim();
            set => txtNombre.Text = value;
        }

        public int IdTipoMantenimiento
        {
            get => Convert.ToInt32(cmbTipoMantenimiento.EditValue ?? 0);
            set => cmbTipoMantenimiento.EditValue = value;
        }

        public string AplicaA
        {
            get => cmbAplicaA.EditValue?.ToString() ?? "";
            set => cmbAplicaA.EditValue = value;
        }

        public string Descripcion
        {
            get => txtDescripcion.Text.Trim();
            set => txtDescripcion.Text = value;
        }

        public int? KilometrosIntervalo
        {
            get => txtIntervalo.Visible && cmbFrecuencia.EditValue?.ToString() == "Kilómetros"
                ? Convert.ToInt32(txtIntervalo.EditValue ?? 0)
                : null;
            set => txtIntervalo.EditValue = value ?? 0;
        }

        public int? DiasIntervalo
        {
            get => txtIntervalo.Visible && cmbFrecuencia.EditValue?.ToString() == "Días"
                ? Convert.ToInt32(txtIntervalo.EditValue ?? 0)
                : null;
            set => txtIntervalo.EditValue = value ?? 0;
        }

        public decimal HorasTotales
        {
            get => Convert.ToDecimal(txtHorasTotales.EditValue ?? 0);
            set => txtHorasTotales.EditValue = value;
        }

        public decimal ManoObraTotal
        {
            get => Convert.ToDecimal(txtManoObraTotales.EditValue ?? 0);
            set => txtManoObraTotales.EditValue = value;
        }

        public decimal RepuestosTotales
        {
            get => Convert.ToDecimal(txtRepuestosTotales.EditValue ?? 0);
            set => txtRepuestosTotales.EditValue = value;
        }

        // =====================================================
        // 🔹 Carga de combos y grids
        // =====================================================

        public void CargarTiposMantenimiento(List<TipoMantenimiento> tipos)
        {
            cmbTipoMantenimiento.Properties.DataSource = tipos;
            cmbTipoMantenimiento.Properties.DisplayMember = "Nombre";
            cmbTipoMantenimiento.Properties.ValueMember = "IdTipoMantenimiento";
            cmbTipoMantenimiento.Properties.NullText = "Seleccione...";
            cmbTipoMantenimiento.Properties.Columns.Clear();
            cmbTipoMantenimiento.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Tipo"));
        }

        public void CargarAplicaA()
        {
            var lista = new List<string> { "Unidad", "Tractor", "Semi" };
            cmbAplicaA.Properties.DataSource = lista;
            cmbAplicaA.Properties.NullText = "Seleccione...";
        }

        public void CargarFrecuencias()
        {
            cmbFrecuencia.Visible = true;
            lblFrecuencia.Visible = true;
            txtIntervalo.Visible = true;
            lblIntervalo.Visible = true;

            var lista = new List<string> { "Indefinido", "Días", "Kilómetros" };
            cmbFrecuencia.Properties.DataSource = lista;
            cmbFrecuencia.Properties.NullText = "Seleccione...";
        }

        public void OcultarFrecuencias()
        {
            cmbFrecuencia.Visible = false;
            lblFrecuencia.Visible = false;
            txtIntervalo.Visible = false;
            lblIntervalo.Visible = false;
        }

        public void CargarTareasPredefinidas(List<Tarea> tareas)
        {
            cmbTarea.Properties.DataSource = tareas;
            cmbTarea.Properties.DisplayMember = "Nombre";
            cmbTarea.Properties.ValueMember = "IdTarea";
            cmbTarea.Properties.NullText = "Seleccione una tarea...";
            cmbTarea.Properties.Columns.Clear();
            cmbTarea.Properties.Columns.Add(new LookUpColumnInfo("Nombre", "Tarea"));
        }

        public void CargarTareasAsignadas(List<Tarea> tareas)
        {
            gridControlTareas.DataSource = tareas;
            gridViewTareas.BestFitColumns();
        }

        private async void bAgregarTarea_Click(object sender, EventArgs e)
        {
            string nombreTarea = XtraInputBox.Show(
      "Ingrese el nombre de la nueva tarea:",
      "Nueva Tarea",
      ""
  );

            if (string.IsNullOrWhiteSpace(nombreTarea))
                return; // canceló o dejó vacío

            // Crear la tarea en la BD con datos iniciales
            int idTarea = await _presenter.CrearTareaAsync(nombreTarea);

            // Abrir el form de edición directamente
            await _presenter.AbrirEdicionTareaAsync(idTarea);

            // Refrescar lista (si corresponde)
        }

        private async void bEditarTarea_Click(object sender, EventArgs e)
        {
            var tarea = gridViewTareas.GetFocusedRow() as Tarea;
            if (tarea != null)
                await _presenter.AbrirEdicionTareaAsync(tarea.IdTarea);
        }
    }
}