using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class MotivoBajaSelectorControl : DevExpress.XtraEditors.XtraUserControl
    {
        public MotivoBajaSelectorControl(List<DisponibleEstado> motivos)
        {
            InitializeComponent();

            var combo = new LookUpEdit
            {
                Name = "cmbMotivo",
                Dock = DockStyle.Top
            };

            combo.Properties.DataSource = motivos;
            combo.Properties.DisplayMember = "Descripcion";
            combo.Properties.ValueMember = "IdDisponibleEstado";
            combo.Properties.NullText = "[Seleccione motivo]";
            combo.Properties.Columns.Clear();
            combo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Motivo de baja")); // Solo mostramos "Descripcion"

            this.Controls.Add(combo);
            this.Height = 50;
        }

        public int? MotivoSeleccionado
        {
            get
            {
                var combo = this.Controls["cmbMotivo"] as LookUpEdit;
                return combo?.EditValue as int?;
            }
        }
    }
}