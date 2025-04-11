using GestionOperativa.Presenters;
using Guna.UI2.WinForms;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Semis
{
    public partial class AgregarEditarSemiForm : Form, IAgregarEditarSemiView
    {
        public AgregarEditarSemiPresenter _presenter;
        private string? _rutaFotoSemi;
        private readonly Dictionary<string, string?> _rutasDocumentos = new();

        public int IdSemi { get; private set; }

        public AgregarEditarSemiForm(AgregarEditarSemiPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async Task CargarDatos(int idSemi)
        {
            await _presenter.CargarDatosParaMostrarAsync(idSemi);
        }

        public void MostrarDatosSemi(SemiDto semi)
        {
            IdSemi = semi.IdSemi;
            IPatente.Text = semi.Patente;
            IAnio.Text = semi.Anio?.ToShortDateString() ?? "N/A";
            IMarca.Text = semi.Marca;
            IModelo.Text = semi.Modelo;
            ITara.Text = semi.Tara.ToString();
            ICisternas.Text = semi.Compartimientos.ToString();
            IMaterial.Text = semi.Material;
            ITipoCarga.Text = semi.Tipo_Carga;
            IConfiguracion.Text = semi.Configuracion;
            IEmpresa.Text = semi.Empresa_Nombre;
            IEmpresaCuit.Text = semi.Empresa_Cuit;
            ITipo.Text = semi.Empresa_Tipo;
            IConfeccion.Text = semi.LitrosDetalle + " = " + semi.LitrosTotal;
            IFechaAlta.Text = semi.FechaAlta?.ToShortDateString() ?? "N/A";
            IVtv.Text = semi.Vtv?.ToShortDateString() ?? "N/A";
            IEstanqueidad.Text = semi.Estanqueidad?.ToShortDateString();
            IltrosNominales.Text = semi.LitroNominal.ToString();
            ICubicacion.Text = semi.Cubicacion.ToString();
            IInv.Text = semi.Inv.ToString();
            IVisualInterna.Text = semi.VisualInterna?.ToShortDateString();
            IFechaEspesor.Text = semi.CisternaEspesor?.ToShortDateString();
            IVisualExt.Text = semi.VisualExterna?.ToShortDateString();
        }

        public void ConfigurarFotoSemi(bool habilitar, string? rutaArchivo)
        {
            bFotoSemi.Enabled = habilitar;
            _rutaFotoSemi = rutaArchivo;
            picBoxFotoSemi.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
        }

        public void ConfigurarFotoConfiguracion(bool habilitar, string? rutaArchivo)
        {
            picBoxConfiguracion.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
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

        public void ConfigurarBotonInv(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bInv, habilitar, rutaArchivo);

        public void ConfigurarBotonEstanqueidad(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bEstanqueidad, habilitar, rutaArchivo);

        public void ConfigurarBotonLitrosNominales(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bLitrosNom, habilitar, rutaArchivo);

        public void ConfigurarBotonCubicacion(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bCubicacion, habilitar, rutaArchivo);

        public void ConfigurarBotonEspesor(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bEspesor, habilitar, rutaArchivo);

        public void ConfigurarBotonVisualInt(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bVisualInt, habilitar, rutaArchivo);

        public void ConfigurarBotonVisualExt(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bVisualExt, habilitar, rutaArchivo);

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

        private void bFotoSemi_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaFotoSemi);
        }

        private void bEstanqueidad_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bEstanqueidad"]);
        }

        private void bLitrosNom_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bLitrosNom"]);
        }

        private void bCubicacion_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bCubicacion"]);
        }

        private void bVisualInt_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bVisualInt"]);
        }

        private void bInv_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bInv"]);
        }

        private void bEspesor_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bEspesor"]);
        }

        private void bVisualExt_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bVisualExt"]);
        }

        private void btnEditarDatos_Click(object sender, EventArgs e)
        {
            _presenter.EditarDatos(IdSemi);
        }

        private void bEditarTransportista_Click(object sender, EventArgs e)
        {
            _presenter.CambiarTransportista(IdSemi, "semi");
        }

        private void picBoxConfiguracion_Click(object sender, EventArgs e)
        {
            _presenter.CambiarConfiguracion(IdSemi, "semi");
        }

        private void bEditarVencimientos_Click(object sender, EventArgs e)
        {
            _presenter.EditarVencimientos(IdSemi);
        }
    }
}