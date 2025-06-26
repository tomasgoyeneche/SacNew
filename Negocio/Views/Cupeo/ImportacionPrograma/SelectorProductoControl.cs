using Shared.Models;

namespace GestionFlota
{
    public partial class SelectorProductoControl : DevExpress.XtraEditors.XtraUserControl
    {
        public Producto ProductoSeleccionado { get; private set; }

        public SelectorProductoControl(List<Producto> producto)
        {
            InitializeComponent();
            gridControl1.DataSource = producto;

            gridView1.DoubleClick += (s, e) =>
            {
                if (gridView1.GetFocusedRow() is Producto pro)
                {
                    ProductoSeleccionado = pro;
                    // El parent form debe cerrar y devolver DialogResult.OK
                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
            };
        }
    }
}