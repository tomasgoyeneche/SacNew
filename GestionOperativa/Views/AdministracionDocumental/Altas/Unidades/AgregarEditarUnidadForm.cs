using GestionOperativa.Presenters.AdministracionDocumental;
using GestionOperativa.Views.AdministracionDocumental.Altas.Unidades;
using Guna.UI2.WinForms;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class AgregarEditarUnidadForm : Form, IAgregarEditarUnidadView
    {
        private readonly AgregarEditarUnidadPresenter _presenter;
        private readonly Dictionary<string, string?> _rutasDocumentos = new();
        private string? _rutaFotoUnidad;

        public AgregarEditarUnidadForm(AgregarEditarUnidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public async Task CargarDatos(int idUnidad)
        {
            await _presenter.CargarDatosUnidadAsync(idUnidad);
        }

        public void MostrarDatosUnidad(UnidadDto unidad)
        {
            IPatenteTractor.Text = unidad.Tractor_Patente;
            lEmpresaTractor.Text = unidad.Empresa_Tractor;
            lEmpresaCuitTractor.Text = unidad.Cuit_Tractor;
            lPatenteSemi.Text = unidad.Semirremolque_Patente;
            lEmpresaSemi.Text = unidad.Empresa_Semi;
            lEmpresaCuitSemi.Text = unidad.Cuit_Semi;
            IEmpresa.Text = unidad.Empresa_Unidad;
            IEmpresaCuit.Text = unidad.Cuit_Unidad;
            ITipo.Text = unidad.Tipo_Empresa;
            lTara.Text = unidad.TaraTotal.ToString();
            lMasYpf.Text = unidad.MasYPF?.ToShortDateString();
            lChecklist.Text = unidad.Checklist?.ToShortDateString();
            lVerifMensual.Text = unidad.Calibrado?.ToShortDateString();

            lMetanol.Text = unidad.Metanol ? "Sí" : "No";
            lGasoil.Text = unidad.Gasoil ? "Sí" : "No";
            lLujanCuyo.Text = unidad.LujanCuyo ? "Sí" : "No";
            lTrafigura.Text = unidad.AptoBo ? "Sí" : "No";
        }

        public void ConfigurarFotoUnidad(bool habilitar, string? rutaArchivo)
        {
            bFotoUnidad.Enabled = habilitar;
            _rutaFotoUnidad = rutaArchivo;
            picBoxFotoUnidad.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
        }

        public void ConfigurarFotoConfiguracionTractor(bool habilitar, string? rutaArchivo)
        {
            picBoxTractor.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
        }

        public void ConfigurarFotoConfiguracionSemi(bool habilitar, string? rutaArchivo)
        {
            picBoxConfiguracionSemi.BackgroundImage = habilitar && rutaArchivo != null ? Image.FromFile(rutaArchivo) : null;
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

        private void ConfigurarBotonDocumento(Guna2ImageButton boton, bool habilitar, string? rutaArchivo)
        {
            boton.Enabled = habilitar;
            if (habilitar && rutaArchivo != null)
            {
                _rutasDocumentos[boton.Name] = rutaArchivo;
            }
        }

        public void ConfigurarBotonTaraTotal(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bTara, habilitar, rutaArchivo);

        public void ConfigurarBotonMasYPF(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bMasYpf, habilitar, rutaArchivo);

        public void ConfigurarBotonChecklist(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bChecklist, habilitar, rutaArchivo);

        public void ConfigurarBotonCalibrado(bool habilitar, string? rutaArchivo) => ConfigurarBotonDocumento(bVerifMensual, habilitar, rutaArchivo);

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bTara_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bTara"]);
        }

        private void bMasYpf_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bMasYpf"]);
        }

        private void bChecklist_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bChecklist"]);
        }

        private void bVerifMensual_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutasDocumentos["bVerifMensual"]);
        }

        private void bFotoUnidad_Click(object sender, EventArgs e)
        {
            AbrirArchivo(_rutaFotoUnidad);
        }
    }
}