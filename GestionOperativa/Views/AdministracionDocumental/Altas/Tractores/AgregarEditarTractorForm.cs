using GestionOperativa.Presenters.Tractor;
using Guna.UI2.WinForms;
using Shared.Models;
using System.Diagnostics;
using System.IO;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public partial class AgregarEditarTractorForm : Form, IAgregarEditarTractorView
    {
        public AgregarEditarTractorPresenter _presenter;
        private string? _rutaFotoTractor;
        private string? _rutaManual;
        private readonly Dictionary<string, string?> _rutasDocumentos = new();
        public int IdTractor { get; private set; }
        public string SatelitalNombre { get; private set; }

        public AgregarEditarTractorForm(AgregarEditarTractorPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async Task CargarDatos(int idTractor)
        {
            await _presenter.CargarDatosParaMostrarAsync(idTractor);
        }

        public void MostrarDatosTractor(TractorDto tractor)
        {
            IdTractor = tractor.IdTractor;
            IPatente.Text = tractor.Patente;
            IAnio.Text = tractor.Anio?.ToShortDateString() ?? "N/A";
            IMarca.Text = tractor.Marca;
            IModelo.Text = tractor.Modelo;
            ITara.Text = tractor.Tara.ToString();
            IHp.Text = tractor.Hp.ToString();
            ICombustible.Text = tractor.Combustible.ToString();
            ICmt.Text = tractor.Cmt.ToString();
            IConfiguracion.Text = tractor.Configuracion;
            ISatelitalNombre.Text = tractor.Satelital_Descripcion;
            IUsuarioSatelital.Text = tractor.Satelital_usuario;
            IClaveSatelital.Text = tractor.Satelital_clave;
            IEmpresa.Text = tractor.Empresa_Nombre;
            IEmpresaCuit.Text = tractor.Empresa_Cuit;
            ITipo.Text = tractor.Empresa_Tipo;
            IFechaAlta.Text = tractor.FechaAlta?.ToShortDateString() ?? "N/A";
            IVtv.Text = tractor.Vtv?.ToShortDateString() ?? "N/A";

            SatelitalNombre = tractor.Satelital_Descripcion;
        }

        public void ConfigurarFotoTractor(bool habilitar, string? rutaArchivo)
        {
            bFotoTractor.Enabled = habilitar;
            _rutaFotoTractor = rutaArchivo;
            picBoxFotoTractor.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
        }

        public void ConfigurarFotoConfiguracion(bool habilitar, string? rutaArchivo)
        {
            picBoxConfiguracion.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
        }

        public void ConfigurarFotoManual(bool habilitar, string? rutaArchivo)
        {
            bFotoManual.Enabled = habilitar;
            _rutaManual = rutaArchivo;
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

        public void ConfigurarBotonCedula(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bCedula, habilitar, rutaArchivo);

        public void ConfigurarBotonTitulo(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bTitulo, habilitar, rutaArchivo);

        public void ConfigurarBotonRuta(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bRuta, habilitar, rutaArchivo);

        public void ConfigurarBotonVTV(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bVtv, habilitar, rutaArchivo);

        private void ConfigurarBotonDocumento(Guna2ImageButton boton, bool habilitar, string? rutaArchivo)
        {
            boton.Enabled = habilitar;
            if (habilitar && rutaArchivo != null)
            {
                _rutasDocumentos[boton.Name] = rutaArchivo;
            }
        }

        public void MostrarVencimiento(string anioVencimiento)
        {
            IVencimiento.Text = anioVencimiento;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bTitulo_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bTitulo"]);
        }

        private void bCedula_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bCedula"]);
        }

        private void bRuta_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bRuta"]);
        }

        private void bVtv_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bVtv"]);
        }

        private void bFotoTractor_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaFotoTractor);
        }

        private void ISatelitalNombre_Click(object sender, EventArgs e)
        {
            if (ISatelitalNombre.Text == "SITRACK")
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.sitrack.com.ar/portal/",
                    UseShellExecute = true
                });
            }
            else
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://avl.megatrans.com.ar/",
                    UseShellExecute = true
                });
            }
        }

        private void bFotoManual_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaManual);
        }

        private void btnEditarDatos_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatos(IdTractor, SatelitalNombre);
        }

        private void bEditarTransportista_Click(object sender, EventArgs e)
        {
            _presenter.CambiarTransportista(IdTractor, "tractor");
        }

        private void picBoxConfiguracion_Click(object sender, EventArgs e)
        {
            _presenter.CambiarConfiguracion(IdTractor, "tractor");
        }

        private void bEditarVencimientos_Click(object sender, EventArgs e)
        {
            _presenter.EditarVencimientos(IdTractor);
        }
    }
}