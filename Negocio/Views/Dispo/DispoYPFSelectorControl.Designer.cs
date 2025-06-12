namespace GestionFlota.Views
{
    partial class DispoYPFSelectorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gridFechas = new DevExpress.XtraGrid.GridControl();
            gridViewFechas = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)gridFechas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewFechas).BeginInit();
            SuspendLayout();
            // 
            // gridFechas
            // 
            gridFechas.Location = new Point(24, 28);
            gridFechas.MainView = gridViewFechas;
            gridFechas.Name = "gridFechas";
            gridFechas.Size = new Size(343, 200);
            gridFechas.TabIndex = 1;
            gridFechas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewFechas });
            // 
            // gridViewFechas
            // 
            gridViewFechas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1 });
            gridViewFechas.GridControl = gridFechas;
            gridViewFechas.Name = "gridViewFechas";
            gridViewFechas.OptionsBehavior.Editable = false;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Fecha Disponibilidad";
            gridColumn1.FieldName = "DispoFecha";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            // 
            // DispoYPFSelectorControl
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridFechas);
            Name = "DispoYPFSelectorControl";
            Size = new Size(385, 244);
            ((System.ComponentModel.ISupportInitialize)gridFechas).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewFechas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridFechas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFechas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
