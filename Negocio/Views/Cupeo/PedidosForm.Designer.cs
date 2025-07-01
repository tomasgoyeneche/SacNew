namespace GestionFlota.Views
{
    partial class PedidosForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            gridControlProg = new DevExpress.XtraGrid.GridControl();
            gridViewProg = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlProg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProg).BeginInit();
            guna2Panel10.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel5
            // 
            guna2Panel5.BackColor = Color.Transparent;
            guna2Panel5.Controls.Add(gridControlProg);
            guna2Panel5.Controls.Add(guna2HtmlLabel3);
            guna2Panel5.CustomizableEdges = customizableEdges1;
            guna2Panel5.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel5.Location = new Point(12, 70);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel5.Size = new Size(774, 472);
            guna2Panel5.TabIndex = 52;
            // 
            // gridControlProg
            // 
            gridControlProg.Dock = DockStyle.Bottom;
            gridControlProg.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlProg.Location = new Point(0, 39);
            gridControlProg.MainView = gridViewProg;
            gridControlProg.Name = "gridControlProg";
            gridControlProg.Size = new Size(774, 433);
            gridControlProg.TabIndex = 51;
            gridControlProg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewProg });
            // 
            // gridViewProg
            // 
            gridViewProg.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewProg.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewProg.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewProg.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn6, gridColumn2, gridColumn3, gridColumn4, gridColumn5, gridColumn9 });
            gridViewProg.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewProg.GridControl = gridControlProg;
            gridViewProg.Name = "gridViewProg";
            gridViewProg.OptionsBehavior.Editable = false;
            gridViewProg.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewProg.OptionsView.EnableAppearanceEvenRow = true;
            gridViewProg.RowCellClick += gridViewProg_RowCellClick;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Carga";
            gridColumn1.FieldName = "FechaCarga";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "Destino";
            gridColumn6.FieldName = "Locacion";
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "AlbaranDespacho";
            gridColumn2.FieldName = "AlbaranDespacho";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "PedidoOr";
            gridColumn3.FieldName = "PedidoOr";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "Entrega";
            gridColumn4.FieldName = "FechaEntrega";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "Kg/Tn";
            gridColumn5.FieldName = "CantidadPedido";
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 5;
            // 
            // gridColumn9
            // 
            gridColumn9.Caption = "Producto";
            gridColumn9.FieldName = "Producto";
            gridColumn9.Name = "gridColumn9";
            gridColumn9.Visible = true;
            gridColumn9.VisibleIndex = 6;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.MediumSeaGreen;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(774, 20);
            guna2HtmlLabel3.TabIndex = 50;
            guna2HtmlLabel3.Text = "Programas Pendientes YPF";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel17);
            guna2Panel10.CustomizableEdges = customizableEdges3;
            guna2Panel10.FillColor = Color.MediumSeaGreen;
            guna2Panel10.Location = new Point(0, 12);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel10.Size = new Size(293, 38);
            guna2Panel10.TabIndex = 53;
            // 
            // guna2HtmlLabel17
            // 
            guna2HtmlLabel17.AutoSize = false;
            guna2HtmlLabel17.BackColor = Color.Transparent;
            guna2HtmlLabel17.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel17.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel17.Location = new Point(8, 5);
            guna2HtmlLabel17.Name = "guna2HtmlLabel17";
            guna2HtmlLabel17.Size = new Size(256, 28);
            guna2HtmlLabel17.TabIndex = 14;
            guna2HtmlLabel17.Text = "Programas Pendientes";
            // 
            // PedidosForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(guna2Panel10);
            Controls.Add(guna2Panel5);
            MaximizeBox = false;
            Name = "PedidosForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PedidosForm";
            guna2Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlProg).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProg).EndInit();
            guna2Panel10.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private DevExpress.XtraGrid.GridControl gridControlProg;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
    }
}