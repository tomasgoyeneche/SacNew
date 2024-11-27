using SacNew.Presenters;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public partial class IngresaGasoil : Form, IIngresaGasoilView
    {
        public readonly IngresaGasoilPresenter _presenter;
        public IngresaGasoil(IngresaGasoilPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }
    }
}