using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Servicios.Presenters.Mantenimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Servicios.Views.Mantenimiento
{
    public partial class MenuMantenimiento : DevExpress.XtraEditors.XtraForm, IMenuMantenimientoView
    {
        private readonly MenuMantenimientoPresenter _presenter;
        private BarManager barManager;

        // Popups
        private PopupMenu popupMenuInformes;
        private PopupMenu popupMenuMovimientos;
        private PopupMenu popupMenuDeposito;
        private PopupMenu popupMenuObjetos;


        public MenuMantenimiento(MenuMantenimientoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            barManager = new BarManager();
            barManager.Form = this;

            InicializarMenus();
        }

        private void InicializarMenus()
        {
            // 🔹 Menu Informes
            popupMenuInformes = new PopupMenu(barManager);

            var informeMantenimiento = new BarButtonItem(barManager, "Informe Mantenimiento");
            informeMantenimiento.ItemClick += (s, e) => _presenter.AbrirInformeMantenimiento();

            var informeStock = new BarButtonItem(barManager, "Informe Stock");
            informeStock.ItemClick += (s, e) => _presenter.AbrirInformeStock();

            var informeTrabajos = new BarButtonItem(barManager, "Informe Trabajos");
            informeTrabajos.ItemClick += (s, e) => _presenter.AbrirInformeTrabajos();

            popupMenuInformes.AddItem(informeMantenimiento);
            popupMenuInformes.AddItem(informeStock);
            popupMenuInformes.AddItem(informeTrabajos);

            // 🔹 Menu Movimientos Stock
            popupMenuMovimientos = new PopupMenu(barManager);

            var historialArticulo = new BarButtonItem(barManager, "Historial de Artículo");
            historialArticulo.ItemClick += (s, e) => _presenter.AbrirHistorialArticulo();

            var movimientosArticulo = new BarButtonItem(barManager, "Movimientos de Artículos");
            movimientosArticulo.ItemClick += (s, e) => _presenter.AbrirMovimientosArticulo();

            var movimientoDeposito = new BarButtonItem(barManager, "Movimiento Depósito");
            movimientoDeposito.ItemClick += (s, e) => _presenter.AbrirMovimientosStock();

            popupMenuMovimientos.AddItem(historialArticulo);
            popupMenuMovimientos.AddItem(movimientosArticulo);
            popupMenuMovimientos.AddItem(movimientoDeposito);

            // 🔹 Menu Depósito
            popupMenuDeposito = new PopupMenu(barManager);

            var articulosExistencia = new BarButtonItem(barManager, "Artículos en Existencia");
            articulosExistencia.ItemClick += (s, e) => _presenter.AbrirArticulosExistencia();

            var articulosStockCritico = new BarButtonItem(barManager, "Artículos con Stock Crítico");
            articulosStockCritico.ItemClick += (s, e) => _presenter.AbrirArticulosStockCritico();

            popupMenuDeposito.AddItem(articulosExistencia);
            popupMenuDeposito.AddItem(articulosStockCritico);

            popupMenuObjetos = new PopupMenu(barManager);

            var articulos = new BarButtonItem(barManager, "Artículos");
            articulos.ItemClick += (s, e) => _presenter.AbrirArticulos();

            var proveedores = new BarButtonItem(barManager, "Proveedores");
            proveedores.ItemClick += (s, e) => _presenter.AbrirProveedores();

            popupMenuObjetos.AddItem(articulos);
            popupMenuObjetos.AddItem(proveedores);
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bAbmArticulos_Click(object sender, EventArgs e)
        {
            popupMenuObjetos.ShowPopup(Cursor.Position);
        }

        private void bMovimientoStock_Click(object sender, EventArgs e)
        {
            popupMenuMovimientos.ShowPopup(Cursor.Position);
        }

        private void bMenuInformes_Click(object sender, EventArgs e)
        {
            popupMenuInformes.ShowPopup(Cursor.Position);
        }

        private void bDeposito_Click(object sender, EventArgs e)
        {
            popupMenuDeposito.ShowPopup(Cursor.Position);
        }

        private async void bMenuMantenimientos_Click(object sender, EventArgs e)
        {
            _presenter.AbrirMenuMantenimientos();
        }
    }
}