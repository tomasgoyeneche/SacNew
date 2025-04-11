using GestionOperativa.Presenters.Choferes;
using Guna.UI2.WinForms;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Choferes
{
    public partial class AgregarEditarChoferForm : Form, IAgregarEditarChoferView
    {
        public readonly AgregarEditarChoferPresenter _presenter;
        private string? _rutaFotoChofer;
        private readonly Dictionary<string, string?> _rutasDocumentos = new();
        public int IdChofer { get; private set; } // Propiedad para manejar el ID de la empresa

        public AgregarEditarChoferForm(AgregarEditarChoferPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async Task CargarDatos(int idChofer)
        {
            await _presenter.CargarDatosParaMostrarAsync(idChofer);
        }

        public void MostrarDatosChofer(ChoferDto chofer)
        {
            IdChofer = chofer.IdChofer;
            IApellido.Text = chofer.Apellido;
            INombre.Text = chofer.Nombre;
            IDoc.Text = chofer.Documento;
            IFechaNacimiento.Text = chofer.FechaNacimiento.ToShortDateString();
            lProvinciaLocalidad.Text = chofer.Ubicacion;
            lDireccion.Text = chofer.Domicilio;
            ITelefonos.Text = chofer.Telefono + ' ' + chofer.Celular;
            IEmpresa.Text = chofer.Empresa_Nombre;
            IEmpresaCuit.Text = chofer.Empresa_Cuit;
            ITipo.Text = chofer.Empresa_Tipo;
            chkZonaFria.Checked = chofer.ZonaFria;
            IAltaTemprana.Text = chofer.FechaAlta.ToShortDateString();
            chkCoberturaCentralizada.Checked = chofer.CoberturaCentralizada;
            ICompania.Text = chofer.Cia_Seguro;
            ICobertura.Text = chofer.Tipo_Cobertura;
            INumeroPoliza.Text = chofer.NumeroPoliza;
            IPagoHasta.Text = chofer.PagoHasta.ToShortDateString();
            IVigenciaHasta.Text = chofer.VigenciaHasta.ToShortDateString();
            ILicencia.Text = chofer.Licencia.ToShortDateString();
            IExamenAnual.Text = chofer.ExamenAnual.ToShortDateString();
            IPsicofisicoApto.Text = chofer.PsicofisicoApto.ToShortDateString();
            IPsicofisicoCurso.Text = chofer.PsicofisicoCurso.ToShortDateString();
            ISeguroVida.Text = chofer.SvoSeguroVida.ToShortDateString();
        }

        public void ConfigurarFotoChofer(bool habilitar, string? rutaArchivo)
        {
            bFotoChofer.Enabled = habilitar;
            _rutaFotoChofer = rutaArchivo;
            if (habilitar && rutaArchivo != null)
            {
                picBoxFotoChofer.BackgroundImage = Image.FromFile(rutaArchivo);
            }
            else
            {
                picBoxFotoChofer.BackgroundImage = null;
            }
        }

        public void ConfigurarBotonAltaTemprana(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bAltaTemprana, habilitar, rutaArchivo);

        public void ConfigurarBotonApto(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bPsicofisicoApto, habilitar, rutaArchivo);

        public void ConfigurarBotonCurso(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bPsicofisicoCurso, habilitar, rutaArchivo);

        public void ConfigurarBotonDNI(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bDocumento, habilitar, rutaArchivo);

        public void ConfigurarBotonLicencia(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bLicencia, habilitar, rutaArchivo);

        public void ConfigurarBotonSeguro(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bCentralizado, habilitar, rutaArchivo);

        public void ConfigurarBotonExamenAnual(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bExamenAnual, habilitar, rutaArchivo);

        private void ConfigurarBotonDocumento(Guna2ImageButton boton, bool habilitar, string? rutaArchivo)
        {
            boton.Enabled = habilitar;
            if (habilitar && rutaArchivo != null)
            {
                _rutasDocumentos[boton.Name] = rutaArchivo;
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bDocumento_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bDocumento"]);
        }

        private void bFotoChofer_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaFotoChofer);
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

        private void bAltaTemprana_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bAltaTemprana"]);
        }

        private void bCentralizado_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bCentralizado"]);
        }

        private void bLicencia_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bLicencia"]);
        }

        private void bPsicofisicoApto_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bPsicofisicoApto"]);
        }

        private void bPsicofisicoCurso_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bPsicofisicoCurso"]);
        }

        private void btnEditarDatos_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatosChofer(IdChofer);
        }

        private void bEditarTransportista_Click(object sender, EventArgs e)
        {
            _presenter.CambiarTransportista(IdChofer, "chofer");
        }

        private void bEditarVencimientos_Click(object sender, EventArgs e)
        {
            _presenter.EditarVencimientos(IdChofer);  
        }
    }
}