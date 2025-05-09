using InformesYEstadisticas.Presenters;
using InformesYEstadisticas.Views;

namespace InformesYEstadisticas
{
    public partial class MenuInformesEst : DevExpress.XtraEditors.XtraForm, IMenuInformesEstView
    {
        private readonly MenuInformesEstPresenter _presenter;

        public MenuInformesEst(MenuInformesEstPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        public void MostrarMensaje(string mensaje)
        {
            throw new NotImplementedException();
        }

        private async void bRelevamientoInicial_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarFichaVacia();
        }

        private async void bVerifMensual_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarVerifMensual();
        }

        private async void bControlOpCons_Click(object sender, EventArgs e)
        {
            await _presenter.GenerarReporteConsumos();
        }
    }
}