using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionFlota.Views
{
    public partial class EditarProgramaForm : DevExpress.XtraEditors.XtraForm, IEditarProgramaView
    {
        public readonly EditarProgramaPresenter _presenter;

        public EditarProgramaForm(EditarProgramaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarDatos(Shared.Models.Ruteo ruteo, Programa? programa)
        {
            // Label de edición
            lblEditarPrograma.Text = programa != null
                ? $"Editar Programa n°: {programa.IdPrograma}"
                : "Programa no asignado";

            // Label Chofer
            lblChofer.Text = $"Chofer: {ruteo.Chofer}";

            // Label Tractor - Semi
            lblTractor.Text = $"{ruteo.Tractor} - {ruteo.Semi}";


            bEliminarLlegadaCarga.Enabled = programa?.CargaLlegada != null;
            bEliminarIngresoCarga.Enabled = programa?.CargaIngreso != null;
            bEliminarSalidaCarga.Enabled = programa?.CargaSalida != null;
            bEliminarLlegadaEntrega.Enabled = programa?.EntregaLlegada != null;
            bEliminarIngresoEntrega.Enabled = programa?.EntregaIngreso != null;
            bEliminarSalidaEntrega.Enabled = programa?.EntregaSalida != null;

            dateEditCarga.EditValueChanged -= dateEditCarga_EditValueChanged;
            dateEditLlegadaCarga.EditValueChanged -= dateEditLlegadaCarga_EditValueChanged;
            dateEditCargaIngreso.EditValueChanged -= dateEditCargaIngreso_EditValueChanged;
            dateEditSalidaCarga.EditValueChanged -= dateEditSalidaCarga_EditValueChanged;
            dateEditEntrega.EditValueChanged -= dateEditEntrega_EditValueChanged;
            dateEditLlegadaEntrega.EditValueChanged -= dateEditLlegadaEntrega_EditValueChanged;
            dateEditEntregaIngreso.EditValueChanged -= dateEditEntregaIngreso_EditValueChanged;
            dateEditEntregaSalida.EditValueChanged -= dateEditEntregaSalida_EditValueChanged;

            dateEditCarga.EditValue = programa?.FechaCarga ?? DateTime.Now;
            dateEditLlegadaCarga.EditValue = programa?.CargaLlegada ?? DateTime.Now;
            dateEditCargaIngreso.EditValue = programa?.CargaIngreso ?? DateTime.Now;
            dateEditSalidaCarga.EditValue = programa?.CargaSalida ?? DateTime.Now;

            dateEditEntrega.EditValue = programa?.FechaEntrega ?? DateTime.Now;
            dateEditLlegadaEntrega.EditValue = programa?.EntregaLlegada ?? DateTime.Now;
            dateEditEntregaIngreso.EditValue = programa?.EntregaIngreso ?? DateTime.Now;
            dateEditEntregaSalida.EditValue = programa?.EntregaSalida ?? DateTime.Now;

            dateEditCarga.EditValueChanged += dateEditCarga_EditValueChanged;
            dateEditLlegadaCarga.EditValueChanged += dateEditLlegadaCarga_EditValueChanged;
            dateEditCargaIngreso.EditValueChanged += dateEditCargaIngreso_EditValueChanged;
            dateEditSalidaCarga.EditValueChanged += dateEditSalidaCarga_EditValueChanged;
            dateEditEntrega.EditValueChanged += dateEditEntrega_EditValueChanged;
            dateEditLlegadaEntrega.EditValueChanged += dateEditLlegadaEntrega_EditValueChanged;
            dateEditEntregaIngreso.EditValueChanged += dateEditEntregaIngreso_EditValueChanged;
            dateEditEntregaSalida.EditValueChanged += dateEditEntregaSalida_EditValueChanged;


            // Paneles visibles según existencia de programa
            pCarga.Visible = pEntrega.Visible =
                pAduanaArg.Visible =
                pAduanaExt.Visible =
                bSubirRemitoCargaPdf.Visible =
                bSubirRemitoEntregaPdf.Visible =
                bSubirOtrosDocs.Visible =
                lblCargaControl.Visible =
                lblEntregaControl.Visible =
                bControlarEntrega.Visible =
                bControlarCarga.Visible =
                (programa != null);

            if (programa != null)
            {
                lblCargaControl.Text = !string.IsNullOrEmpty(programa?.CargaCheck)
                ? $"Controlo: {programa.CargaCheck}"
                : "Controlo: -";

                lblEntregaControl.Text = !string.IsNullOrEmpty(programa?.EntregaCheck)
                    ? $"Controlo: {programa.EntregaCheck}"
                    : "Controlo: -";

                lblCargaEdit.Text = $"CARGA - {ruteo.Origen}";
                lblEntregaEdit.Text = $"ENTREGA - {ruteo.Destino}";

                lblCarga.Text = $"Carga: {programa.FechaCarga?.ToString("dd/MM/yyyy") ?? "-"}";
                lblLlegadaCarga.Text = $"Llegada: {programa.CargaLlegada?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
                lblIngresoCarga.Text = $"Ingreso: {programa.CargaIngreso?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
                lblSalidaCarga.Text = $"Salida: {programa.CargaSalida?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
                lblRemitoCarga.Text = $"Remito: {programa.CargaRemito?.ToString() ?? "-"}";

                lblEntrega.Text = $"Entrega: {programa.FechaEntrega?.ToString("dd/MM/yyyy") ?? "-"}";
                lblLlegadaEntrega.Text = $"Llegada: {programa.EntregaLlegada?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
                lblIngresoEntrega.Text = $"Ingreso: {programa.EntregaIngreso?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
                lblSalidaEntrega.Text = $"Salida: {programa.EntregaSalida?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
                lblRemitoEntrega.Text = $"Remito: {programa.EntregaRemito?.ToString() ?? "-"}";
            }
            else
            {
                lblCargaEdit.Text = $"CARGA - N/A";
                lblEntregaEdit.Text = $"ENTREGA - N/A";

                lblCarga.Text = "Carga: -";
                lblLlegadaCarga.Text = "Llegada: -";
                lblIngresoCarga.Text = "Ingreso: -";
                lblSalidaCarga.Text = "Salida: -";
                lblRemitoCarga.Text = "Remito: -";

                lblEntrega.Text = "Entrega: -";
                lblLlegadaEntrega.Text = "Llegada: -";
                lblIngresoEntrega.Text = "Ingreso: -";
                lblSalidaEntrega.Text = "Salida: -";
                lblRemitoEntrega.Text = "Remito: -";
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void dateEditCarga_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditCarga.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("FechaCarga", nuevaFecha);
        }

        private async void dateEditLlegadaCarga_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditLlegadaCarga.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("CargaLlegada", nuevaFecha);
        }

        private async void dateEditCargaIngreso_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditCargaIngreso.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("CargaIngreso", nuevaFecha);
        }

        private async void dateEditSalidaCarga_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditSalidaCarga.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("CargaSalida", nuevaFecha);
        }

        private async void bEliminarLlegadaCarga_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que desea eliminar la Fecha de Llegada a Carga?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await _presenter.GuardarFechaProgramaAsync("CargaLlegada", null);

        }

        private async void bEliminarIngresoCarga_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que desea eliminar la Fecha de Ingreso a Carga?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await _presenter.GuardarFechaProgramaAsync("CargaIngreso", null);
        }

        private async void bEliminarSalidaCarga_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que desea eliminar la Fecha de Salida a Carga?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await _presenter.GuardarFechaProgramaAsync("CargaSalida", null);
        }

        private async void dateEditEntrega_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditEntrega.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("FechaEntrega", nuevaFecha);
        }

        private async void dateEditLlegadaEntrega_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditLlegadaEntrega.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("EntregaLlegada", nuevaFecha);
        }

        private async void dateEditEntregaIngreso_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditEntregaIngreso.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("EntregaIngreso", nuevaFecha);
        }

        private async void dateEditEntregaSalida_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? nuevaFecha = dateEditEntregaSalida.EditValue as DateTime?;
            await _presenter.GuardarFechaProgramaAsync("EntregaSalida", nuevaFecha);
        }

        private async void bEliminarLlegadaEntrega_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que desea eliminar la Fecha de Llegada a Entrega?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await _presenter.GuardarFechaProgramaAsync("EntregaLlegada", null);
        }

        private async void bEliminarIngresoEntrega_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que desea eliminar la Fecha de Ingreso a Entrega?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await _presenter.GuardarFechaProgramaAsync("EntregaIngreso", null);
        }

        private async void bEliminarSalidaEntrega_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que desea eliminar la Fecha de Salida a Entrega?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await _presenter.GuardarFechaProgramaAsync("EntregaSalida", null);
        }

        private async void bSubirRemitoCargaPdf_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Filter = "PDF Files|*.pdf",
                Title = "Seleccione el Remito de Carga"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Pasá el path del archivo origen al presenter
                    await _presenter.SubirArchivoRemitoAsync(
                        tipo: "RC",
                        archivoOrigen: ofd.FileName
                    );
                }
            }
        }

        private async void bSubirOtrosDocs_Click(object sender, EventArgs e)
        {

            using (var ofd = new OpenFileDialog()
            {
                Title = "Seleccione el documento a subir"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Pasá el path del archivo origen al presenter
                    await _presenter.SubirArchivoRemitoAsync(
                        tipo: "Otros",
                        archivoOrigen: ofd.FileName
                    );
                }
            }
        }

        private async void bSubirRemitoEntregaPdf_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Filter = "PDF Files|*.pdf",
                Title = "Seleccione el Remito de Entrega"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Pasá el path del archivo origen al presenter
                    await _presenter.SubirArchivoRemitoAsync(
                        tipo: "RE",
                        archivoOrigen: ofd.FileName
                    );
                }
            }
        }

        public void MostrarAlertas(List<AlertaDto> alertas)
        {
            gridControlAlertas.DataSource = alertas;

            var view = gridViewAlertas;
            foreach (var col in new[] { "IdAlerta", "IdNomina", "Activo", "PatenteSemi" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewAlertas.BestFitColumns();
        }

        public void MostrarArchivos(List<ArchivoDocRuteo> archivos)
        {
            gridControlDocumentos.DataSource = archivos;

            // Deja solo la columna NombreArchivo visible
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewDocumentos.Columns)
                col.Visible = col.FieldName == "NombreArchivo";

            gridViewDocumentos.BestFitColumns();
        }

        private void gridViewDocumentos_DoubleClick(object sender, EventArgs e)
        {

            var archivoGrid = gridViewDocumentos.GetFocusedRow() as ArchivoDocRuteo;
            if (archivoGrid == null)
                return;

            string archivo = archivoGrid.Ruta;
            var opcion = MessageBox.Show(
                "¿Desea ABRIR este archivo?\n(Si = abrir / No = eliminar / Cancelar = nada)",
                "Archivo Adjunto", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (opcion == DialogResult.Yes)
            {
                try { System.Diagnostics.Process.Start("explorer", archivo); }
                catch (Exception ex) { MostrarMensaje($"No se pudo abrir el archivo: {ex.Message}"); }
            }
            else if (opcion == DialogResult.No)
            {
                var confirmar = MessageBox.Show(
                    "¿Seguro que desea eliminar el archivo?\nEsta acción no se puede deshacer.",
                    "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmar == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(archivo);
                        MostrarMensaje("Archivo eliminado.");
                        _presenter.CargarArchivos(); // Refresca el grid
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje($"No se pudo eliminar: {ex.Message}");
                    }
                }
            }

        }

        private async void bControlarEntrega_Click(object sender, EventArgs e)
        {
            await _presenter.ControlarAsync("EntregaCheck");
        }

        private async void bControlarCarga_Click(object sender, EventArgs e)
        {
            await _presenter.ControlarAsync("CargaCheck");
        }

        private async void bRemitoCarga_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirCargaRemitoFormAsync("Carga");
        }

        private async void bRemitoEntrega_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirCargaRemitoFormAsync("Entrega");
        }

        private async void bObservacion_Click(object sender, EventArgs e)
        {
            string comentario = XtraInputBox.Show(
            "Ingrese el comentario:",
            "Comentario",
            ""
        );

            // Si canceló o dejó vacío, no guardes nada
            if (string.IsNullOrWhiteSpace(comentario)) return;

            // Guardar el comentario en NominaRegistro
            await _presenter.RegistrarComentarioAsync(comentario);
        }

        private async void bCambiarChofer_Click(object sender, EventArgs e)
        {
            await _presenter.CambiarChoferAsync();
        }
    }
}