using Core;
using Core.Services;
using DevExpress.XtraEditors;
using GestionFlota.Views;
using GestionFlota.Views.Postas;
using GestionFlota.Views.Postas.IngresaConsumos;
using GestionFlota.Views.Postas.LimiteDeCredito;
using GestionFlota.Views.Postas.Modificaciones;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.DatosVolvo;
using SacNew.Views.GestionFlota.Postas.Informes;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos;

namespace SacNew.Views.GestionFlota.Postas
{
    public partial class MenuPostas : Form
    {
        private readonly ISesionService _sesionService;
        private readonly INavigationService _navigationService;

        public MenuPostas(ISesionService sesionService, INavigationService navigationService)
        {
            InitializeComponent();
            _sesionService = sesionService;
            _navigationService = navigationService;
        }

        private void MenuPostas_Load(object sender, EventArgs e)
        {
            lMenuPostasNombre.Text = _sesionService.NombreCompleto;
        }

        private void bAbmPostasMenu_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0026-AbmPostas") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                this.Hide();
                _navigationService.ShowDialog<MenuAbmPostas>();
                this.Show();
            }
            else
            {
                mensajeError.Show("No tienes permisos para acceder al abm de postas.");
            }
        }

        private void bMenuConceptos_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0022-Conceptos") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                this.Hide();
                _navigationService.ShowDialog<MenuConceptos>();
                this.Show();
            }
            else
            {
                mensajeError.Show("No tienes permisos para acceder a ingresar conceptos.");
            }
        }

        private void bIngresaConsumos_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0023-Consumos") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                _navigationService.ShowDialog<MenuIngresaConsumos>();
            }
            else
            {
                mensajeError.Show("No tienes permisos para acceder a ingresar consumos.");
            }
        }

        private void bIngresaConsumosYPF_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0024-ImportacionesConsumos") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                _navigationService.ShowDialog<PruebasDev>();

                //_navigationService.ShowDialog<ImportDatosVolvo>();
            }
            else
            {
                mensajeError.Show("No tienes permisos para acceder a ingresar consumos.");
            }
        }

        private void bDatosVolvo_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0024-ImportacionesConsumos") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
               _navigationService.ShowDialog<ImportDatosVolvo>();
            }
            else
            {
                mensajeError.Show("No tienes permisos para acceder a ingresar datos de volvo.");
            }
        }

        private void bLimiteCredito_Click(object sender, EventArgs e)
        {
            if (_sesionService.Permisos.Contains("0025-LimiteCredito") || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                _navigationService.ShowDialog<LimiteCreditoForm>();
            }
            else
            {
                mensajeError.Show("No tienes permisos para acceder a ingresar limite de credito.");
            }
        }

        private void bModificaTablas_Click(object sender, EventArgs e)
        {
            _navigationService.ShowDialog<MenuModificacionesTablasForm>();
        }

        private void bMenuInformesPostas_Click(object sender, EventArgs e)
        {
            this.Hide();
            _navigationService.ShowDialog<MenuInformesPostas>();
            this.Show();
        }
    }
}