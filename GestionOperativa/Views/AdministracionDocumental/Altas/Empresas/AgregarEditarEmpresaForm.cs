using GestionOperativa.Presenters;
using Shared.Models;

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
            lCompania.Text = empresa.NombreCia ?? "N/A";
            lCobertura.Text = empresa.TipoCobertura ?? "N/A";
            lNumPoliza.Text = empresa.NumeroPoliza ?? "N/A";
            lVigenciaHasta.Text = empresa.VigenciaHasta != DateTime.MinValue
                ? empresa.VigenciaHasta.ToShortDateString()
                : "Sin Fecha";
            lPagoDesde.Text = empresa.PagoDesde != DateTime.MinValue
                ? empresa.PagoDesde.ToShortDateString()
                : "Sin Fecha";
            lPagoHasta.Text = empresa.PagoHasta != DateTime.MinValue
                ? empresa.PagoHasta.ToShortDateString()
                : "Sin Fecha";

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

        private void btnEditarDatos_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatosEmpresa(IdEmpresa);

        }

        private void btnEditarArt_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatosSeguro(IdEmpresa);

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
    }
}