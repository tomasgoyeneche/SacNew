using DevExpress.XtraEditors;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionFlota.Views
{
    public partial class SelectorLocacionControl : DevExpress.XtraEditors.XtraUserControl
    {
        public Locacion LocacionSeleccionada { get; private set; }

        public SelectorLocacionControl(List<Locacion> locaciones)
        {
            InitializeComponent();
            gridControl1.DataSource = locaciones;

            gridView1.DoubleClick += (s, e) =>
            {
                if (gridView1.GetFocusedRow() is Locacion loc)
                {
                    LocacionSeleccionada = loc;
                    // El parent form debe cerrar y devolver DialogResult.OK
                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
            };
        }
    }
}
