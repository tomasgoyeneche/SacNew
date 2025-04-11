using GestionFlota.Presenters;
using SacNew.Interfaces;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class MenuIngresaConsumos : DevExpress.XtraEditors.XtraForm, IMenuIngresaConsumosView
    {
        private readonly MenuIngresaConsumosPresenter _presenter;

        public MenuIngresaConsumos(MenuIngresaConsumosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string CriterioBusqueda => txtBuscar.Text.Trim();

        // Implementación de IMenuIngresaConsumosView
        public void MostrarPOC(List<POCDto> listaPOC)
        {
            gridControlPOC.DataSource = listaPOC;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewPOC;
            view.Columns["IdPoc"].Visible = false;
            view.Columns["IdPosta"].Visible = false;
            view.Columns["CapacidadTanque"].Visible = false;
            view.Columns["Estado"].Visible = false;

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido

            gridViewPOC.OptionsView.EnableAppearanceEvenRow = true;
            gridViewPOC.OptionsView.EnableAppearanceOddRow = true;
            gridViewPOC.Appearance.Row.Font = new Font("Segoe UI", 9);
            gridViewPOC.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75f);
            gridViewPOC.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewPOC.Appearance.Row.Options.UseFont = true;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        // Eventos
        private async void MenuIngresaConsumos_Load(object sender, EventArgs e)
        {
            await _presenter.InicializarAsync();  // Cargar datos iniciales de forma asíncrona
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarPOCAsync(CriterioBusqueda); // Pasar el criterio de búsqueda al presenter
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarPOCAsync(CriterioBusqueda);  // Búsqueda dinámica al escribir
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var row = gridViewPOC.GetFocusedRow() as POCDto;
            if (row != null)
            {
                await _presenter.EliminarPOCAsync(row.IdPoc);
            }
            else
            {
                MostrarMensaje("Seleccione una POC para eliminar.");
            }
        }

        private async void btnAgregarPOC_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarPOCAsync();
        }

        private async void btnEditarPOC_Click(object sender, EventArgs e)
        {
            var row = gridViewPOC.GetFocusedRow() as POCDto;
            if (row != null)
            {
                await _presenter.EditarPOCAsync(row.IdPoc);
            }
            else
            {
                MostrarMensaje("Seleccione una POC para editar.");
            }
        }

        private async void gridControlPOC_DoubleClick(object sender, EventArgs e)
        {
            var view = gridViewPOC;
            if (view.FocusedRowHandle >= 0)
            {
                var row = view.GetRow(view.FocusedRowHandle) as POCDto;
                if (row != null)
                {
                    await _presenter.AbrirMenuIngresaGasoilOtrosAsync(row.IdPoc);
                    await _presenter.InicializarAsync();
                }
            }
        }
    }
}