using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Presenters;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo
{
    public partial class MenuIngresarGasoilOtros : Form, IMenuIngresaGasoilOtrosView
    {
        public readonly MenuIngresaGasoilOtrosPresenter _presenter;

        public MenuIngresarGasoilOtros(MenuIngresaGasoilOtrosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string NumeroPoc
        {
            get => txtNumeroPoc.Text;
            set => txtNumeroPoc.Text = value;
        }

        public string CreditoTotal
        {
            get => txtCreditoTotal.Text;
            set => txtCreditoTotal.Text = value;
        }

        public string CreditoDisponible
        {
            get => txtCreditoDisponible.Text;
            set => txtCreditoDisponible.Text = value;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private void bIngresaGasoil_Click(object sender, EventArgs e)
        {
            _presenter.IngresaGasoil();
        }
    }
}