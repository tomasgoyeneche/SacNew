using DevExpress.XtraGrid.Views.Grid;
using GestionOperativa.Presenters;
using GestionOperativa.Properties;
using Shared.Models;
using System.Data;

namespace GestionOperativa.Views
{
    public partial class AdministracionForm : DevExpress.XtraEditors.XtraForm, IAdministracionView
    {
        public readonly AdministracionPresenter _presenter;

        public AdministracionForm(AdministracionPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarMensaje(string mensaje)
        {
            MensajeBox.Show(mensaje);
        }

        public void MostrarGuardia(List<GuardiaDto> guardias)
        {
            gridControlGuardia.DataSource = guardias;

            var view = gridViewGuardia;
            foreach (var col in new[] { "IdEntidad", "IdGuardiaIngreso", "TipoIngreso", "IdPosta", "IdEstadoEvento" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            view.BestFitColumns();
        }

        public void MostrarResumen(List<(string Descripcion, int Cantidad)> resumen)
        {
            gridControlResumen.DataSource = resumen
                .Select(r => new { r.Descripcion, r.Cantidad })
                .ToList();

            gridViewResumen.BestFitColumns();
        }

        public void MostrarHistorial(List<GuardiaHistorialDto> historial)
        {
            gridControlHistorico.DataSource = historial;

            var view = gridViewHistorico;
            foreach (var col in new[] { "IdGuardiaIngreso", "IdGuardiaEstado", "IdGuardiaRegistro" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            if (view.Columns["FechaGuardia"] != null)
            {
                view.Columns["FechaGuardia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["FechaGuardia"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }

            view.BestFitColumns();
        }

        public void MostrarVencimientos(List<VencimientosDto> vencimientos)
        {
            gridControlVencimientos.DataSource = vencimientos;

            var view = gridViewVencimientos;
            foreach (var col in new[] { "Entidad" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewVencimientos.BestFitColumns();
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

        private void gridViewVencimientos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            GridView? view = sender as GridView;
            VencimientosDto? vencimiento = view.GetRow(e.RowHandle) as VencimientosDto;

            if (vencimiento == null || e.Column.FieldName != "FechaVencimiento")
                return;

            var fecha = vencimiento.FechaVencimiento.Date;
            var hoy = DateTime.Now.Date;

            if (fecha < hoy)
            {
                e.Appearance.BackColor = Color.LightCoral; // rojo
            }
            else if ((fecha - hoy).TotalDays <= 7)
            {
                e.Appearance.BackColor = Color.Khaki; // amarillo
            }
            else
            {
                e.Appearance.BackColor = Color.LightGreen; // verde
            }
        }

        private async void gridViewGuardia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.MostrarHistorialAsync(guardia.IdGuardiaIngreso);
                await _presenter.CargarVencimientosYAlertasAsync(guardia);
                await _presenter.MostrarDatosYFotoAsync(guardia);
            }
        }

        public void MostrarDatosTe(TransitoEspecial te)
        {
            txtApellidoNombre.Text = te.Nombre + " " + te.Apellido;
            txtNombreFantasia.Text = te.RazonSocial;
            txtCuit.Text = te.Cuit;
            txtDni.Text = te.Documento;
            txtLicencia.Text = te.Licencia.Value.ToShortDateString();
            txtTractor.Text = te.Tractor;
            txtSemi.Text = te.Semi;

            txtPsiApto.Text = "N/A";
            txtPsiCurso.Text = "N/A";
            txtVtv.Text = "N/A";
            txtVtvSemi.Text = "N/A";
            txtEstanq.Text = "N/A";
            txtVisualInt.Text = "N/A";
            txtMasYpf.Text = "N/A";
            txtVerifMensual.Text = "N/A";
            txtVisualExt.Text = "N/A";
            txtChecklist.Text = "N/A";
            txtEspesor.Text = "N/A";

            picBoxFotoChofer.BackgroundImage = Resources.fotoChoferIncognito;
            picBoxFotoUnidad.BackgroundImage = Resources.fotoTractorIncognito__1_;
        }

        public void MostrarDatosOtros(GuardiaIngresoOtros otros)
        {
            txtApellidoNombre.Text = otros.Nombre + " " + otros.Apellido;
            txtNombreFantasia.Text = otros.Empresa;
            txtCuit.Text = "N/A";
            txtDni.Text = otros.Documento;
            txtLicencia.Text = otros.Licencia.Value.ToShortDateString();
            txtTractor.Text = otros.Patente;
            txtSemi.Text = "N/A";

            txtPsiApto.Text = "N/A";
            txtPsiCurso.Text = "N/A";
            txtVtv.Text = "N/A";
            txtVtvSemi.Text = "N/A";
            txtEstanq.Text = "N/A";
            txtVisualInt.Text = "N/A";
            txtMasYpf.Text = "N/A";
            txtVerifMensual.Text = "N/A";
            txtVisualExt.Text = "N/A";
            txtChecklist.Text = "N/A";
            txtEspesor.Text = "N/A";

            picBoxFotoChofer.BackgroundImage = Resources.fotoChoferIncognito;
            picBoxFotoUnidad.BackgroundImage = Resources.fotoTractorIncognito__1_;
        }

        public void MostrarDatosNomina(ChoferDto chofer, TractorDto tractor, SemiDto semi, UnidadDto unidad, string rutaFotoChofer, string RutaFotoUnidad)
        {
            txtApellidoNombre.Text = chofer.Nombre + " " + chofer.Apellido;
            txtNombreFantasia.Text = chofer.Empresa_Nombre;
            txtCuit.Text = chofer.Empresa_Cuit;
            txtDni.Text = chofer.Documento;
            txtLicencia.Text = chofer.Licencia.ToShortDateString();
            txtTractor.Text = tractor.Patente;
            txtSemi.Text = semi.Patente;

            txtPsiApto.Text = chofer.PsicofisicoApto.ToShortDateString();
            txtPsiCurso.Text = chofer.PsicofisicoCurso.ToShortDateString();
            txtVtv.Text = tractor.Vtv.Value.ToShortDateString();
            txtVtvSemi.Text = semi.Vtv.Value.ToShortDateString();
            txtEstanq.Text = semi.Estanqueidad.Value.ToShortDateString();
            txtVisualInt.Text = semi.VisualInterna.Value.ToShortDateString();
            txtMasYpf.Text = unidad.MasYPF.Value.ToShortDateString();
            txtVerifMensual.Text = unidad.Calibrado.Value.ToShortDateString();
            txtVisualExt.Text = semi.VisualExterna.Value.ToShortDateString();
            txtChecklist.Text = unidad.Checklist.Value.ToShortDateString();
            txtEspesor.Text = semi.CisternaEspesor.Value.ToShortDateString();

            if (rutaFotoChofer != null)
            {
                picBoxFotoChofer.BackgroundImage = Image.FromFile(rutaFotoChofer);
            }
            else
            {
                picBoxFotoChofer.BackgroundImage = Resources.fotoChoferIncognito;
            }

            if (RutaFotoUnidad != null)
            {
                picBoxFotoUnidad.BackgroundImage = Image.FromFile(RutaFotoUnidad);
            }
            else
            {
                picBoxFotoUnidad.BackgroundImage = Resources.fotoTractorIncognito__1_;
            }
        }

        private async void gridViewGuardia_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.AbrirCambioEstadoAsync(guardia);
            }
        }

        private async void bNominaMetanol_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarReporteFlotaAsync();
        }

        private async void bImprimirPoc_Click(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.ReimprimirPoc(guardia);
            }
        }

        private async void bVerifMensual_Click(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia && guardia.TipoIngreso == 1)
            {
                await _presenter.VerifMensual(guardia);
            }
            else
            {
                MensajeBox.Show("La Unidad no se encuentra en una Nomina activa");
            }
        }

        private async void bNominaActual_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarNominaActual();
        }

        private void bNroPoc_Click(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                MostrarMensaje("El Numero de POC es: " + guardia.IdGuardiaIngreso);
            }
        }

        private async void bTelefonos_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarReporteTelefonosNomina();
        }

        private async void bVaporizados_Click(object sender, EventArgs e)
        {
            await _presenter.AbrirVaporizados("0011-Vaporizados");
        }
    }
}