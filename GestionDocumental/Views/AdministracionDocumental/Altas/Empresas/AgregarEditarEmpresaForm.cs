using GestionOperativa.Presenters;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public partial class AgregarEditarEmpresaForm : Form, IAgregarEditarEmpresaView
    {
        public readonly AgregarEditarEmpresaPresenter _presenter;
        private string? _rutaArchivoArt;
        private string? _rutaArchivoCuit;

        public AgregarEditarEmpresaForm(AgregarEditarEmpresaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        // Implementación de la vista
        public void MostrarDatosEmpresa(EmpresaDto empresa)
        {
            lTipo.Text = empresa.EmpresaTipo ?? "N/A";
            lRazonSoc.Text = empresa.RazonSocial ?? "N/A";
            lNombreFantasia.Text = empresa.NombreFantasia ?? "N/A";
            lCuit.Text = empresa.Cuit ?? "N/A";
            lProvinciaLocalidad.Text = empresa.Ubicacion ?? "N/A";
            lDireccion.Text = empresa.Domicilio ?? "N/A";
            lTelefonos.Text = empresa.Telefono ?? "N/A";
            lMail.Text = empresa.Email ?? "N/A";

            IdEmpresa = empresa.IdEmpresa;
        }

        public int IdEmpresa { get; private set; } // Propiedad para manejar el ID de la empresa

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void HabilitarBotonLegajoArt(bool habilitar)
        {
            btnLegajoArt.Enabled = habilitar;
        }

        public void ConfigurarRutaArchivoArt(string rutaArchivo)
        {
            _rutaArchivoArt = rutaArchivo;
        }

        public void HabilitarBotonLegajoCuit(bool habilitar)
        {
            btnLegajoCuit.Enabled = habilitar;
        }

        public void ConfigurarRutaArchivoCuit(string rutaArchivo)
        {
            _rutaArchivoCuit = rutaArchivo;
        }

        private void btnLegajoArt_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaArchivoArt);
        }

        private void btnLegajoCuit_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaArchivoCuit);
        }

        private void AbrirArchivo(string? rutaArchivo)
        {
            if (!string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo))
            {
                System.Diagnostics.Process.Start("explorer", rutaArchivo);
            }
            else
            {
                MostrarMensaje("El archivo no existe o la ruta no es válida.");
            }
        }

        public void MostrarSatelitales(List<EmpresaSatelitalDto> satelitales)
        {
            dgvSatelitales.DataSource = satelitales;
            dgvSatelitales.Columns["idEmpresaSatelital"].Visible = false;
        }

        public void MostrarPaises(List<EmpresaPaisDto> paises)
        {
            dgvPaises.DataSource = paises;
            dgvPaises.Columns["idEmpresaPais"].Visible = false;
        }

        public void MostrarSeguros(List<EmpresaSeguroDto> seguros)
        {
            dvgSeguroEmpresa.DataSource = seguros;

            // Ocultar columnas técnicas
            dvgSeguroEmpresa.Columns["idEmpresaSeguro"].Visible = false;
            dvgSeguroEmpresa.Columns["idEmpresa"].Visible = false;
            dvgSeguroEmpresa.Columns["idEmpresaSeguroEntidad"].Visible = false;

            dvgSeguroEmpresa.Columns["entidad"].HeaderText = "Tipo";
            dvgSeguroEmpresa.Columns["cia"].HeaderText = "CIA";
            dvgSeguroEmpresa.Columns["TipoCobertura"].HeaderText = "Cobertura";
            dvgSeguroEmpresa.Columns["numeroPoliza"].HeaderText = "Poliza";
            dvgSeguroEmpresa.Columns["certificadoMensual"].HeaderText = "Certificado Mensual";
            dvgSeguroEmpresa.Columns["vigenciaAnual"].HeaderText = "Vigencia Anual";

            // Reordenar columnas (DisplayIndex)
            dvgSeguroEmpresa.Columns["TipoCobertura"].DisplayIndex = 0;
            dvgSeguroEmpresa.Columns["certificadoMensual"].DisplayIndex = 1;
            dvgSeguroEmpresa.Columns["vigenciaAnual"].DisplayIndex = 2;
            dvgSeguroEmpresa.Columns["numeroPoliza"].DisplayIndex = 3;
            dvgSeguroEmpresa.Columns["cia"].DisplayIndex = 4;
            dvgSeguroEmpresa.Columns["entidad"].DisplayIndex = 5;
        }

        private void btnEditarDatos_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatosEmpresa(IdEmpresa);
        }



        private void bAgregarSatelital_Click(object sender, EventArgs e)
        {
            _presenter.AgregarEmpresaSatelital(IdEmpresa);
        }

        private async void bEliminarSatelital_Click(object sender, EventArgs e)
        {
            if (dgvSatelitales.SelectedRows.Count > 0)
            {
                int idEmpresaSatelital = Convert.ToInt32(dgvSatelitales.SelectedRows[0].Cells["idEmpresaSatelital"].Value);
                await _presenter.EliminarEmpresaSatelital(idEmpresaSatelital, IdEmpresa);
            }
            else
            {
                MostrarMensaje("Seleccione una empresa satelital para eliminar.");
            }
        }

        private void bAgregarPaisAuto_Click(object sender, EventArgs e)
        {
        }

        private void bAgregarSeguro_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatosSeguro(IdEmpresa, null);
        }

        private void btnEditarArt_Click(object sender, EventArgs e)
        {
            EmpresaSeguroDto empresaseguro = dvgSeguroEmpresa.SelectedRows[0].DataBoundItem as EmpresaSeguroDto;
            if (empresaseguro == null)
            {
                MostrarMensaje("No se pudo obtener el seguro seleccionado.");
                return;
            }
            _presenter.EditarDatosSeguro(IdEmpresa, empresaseguro);
        }

        private async void bEliminarSeguro_Click(object sender, EventArgs e)
        {
            if (dvgSeguroEmpresa.SelectedRows.Count == 0)
            {
                MostrarMensaje("Seleccione un seguro para eliminar.");
                return;
            }

            var row = dvgSeguroEmpresa.SelectedRows[0].DataBoundItem as EmpresaSeguroDto;
            if (row == null)
            {
                MostrarMensaje("No se pudo obtener el seguro seleccionado.");
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea eliminar el seguro seleccionado?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                await _presenter.EliminarSeguroAsync(row.idEmpresaSeguro, IdEmpresa);
            }
        }
    }
}