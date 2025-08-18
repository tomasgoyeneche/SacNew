using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class MotivoBajaSelectorControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly LookUpEdit _combo;
        private readonly MemoEdit _txtObs;

        public MotivoBajaSelectorControl(List<DisponibleEstado> motivos)
        {
            InitializeComponent();

            _combo = new LookUpEdit
            {
                Name = "cmbMotivo",
                Dock = DockStyle.Top
            };
            _combo.Properties.DataSource = motivos;
            _combo.Properties.DisplayMember = "Descripcion";
            _combo.Properties.ValueMember = "IdDisponibleEstado";
            _combo.Properties.NullText = "[Seleccione motivo]";
            _combo.Properties.Columns.Clear();
            _combo.Properties.Columns.Add(new LookUpColumnInfo("Descripcion", "Motivo de baja"));

            _txtObs = new MemoEdit
            {
                Name = "txtObs",
                Dock = DockStyle.Fill,
                Properties = { NullText = "Sin Observacion" }
            };

            this.Controls.Add(_txtObs);
            this.Controls.Add(_combo);

            this.Height = 120;
        }

        public int? MotivoSeleccionado => _combo.EditValue as int?;
        public string Observacion => _txtObs.Text?.Trim();
    }
}