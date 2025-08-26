namespace Configuraciones.Views.AbmLocaciones
{
    partial class MenuLocacionesForm
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
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            gridControlLocaciones = new DevExpress.XtraGrid.GridControl();
            gridViewLocaciones = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bEditar = new DevExpress.XtraEditors.SimpleButton();
            bEliminar = new DevExpress.XtraEditors.SimpleButton();
            bPool = new DevExpress.XtraEditors.SimpleButton();
            bAgregar = new DevExpress.XtraEditors.SimpleButton();
            guna2Panel3.SuspendLayout();
            guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlLocaciones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLocaciones).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel1);
            guna2Panel3.CustomizableEdges = customizableEdges1;
            guna2Panel3.FillColor = Color.Teal;
            guna2Panel3.Location = new Point(1, 12);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 41;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(17, 8);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(214, 25);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Menu Abm Locaciones";
            // 
            // guna2Panel5
            // 
            guna2Panel5.BackColor = Color.Transparent;
            guna2Panel5.Controls.Add(gridControlLocaciones);
            guna2Panel5.Controls.Add(guna2HtmlLabel3);
            guna2Panel5.CustomizableEdges = customizableEdges3;
            guna2Panel5.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel5.Location = new Point(15, 72);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel5.Size = new Size(769, 437);
            guna2Panel5.TabIndex = 54;
            // 
            // gridControlLocaciones
            // 
            gridControlLocaciones.Dock = DockStyle.Bottom;
            gridControlLocaciones.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlLocaciones.Location = new Point(0, 44);
            gridControlLocaciones.MainView = gridViewLocaciones;
            gridControlLocaciones.Name = "gridControlLocaciones";
            gridControlLocaciones.Size = new Size(769, 393);
            gridControlLocaciones.TabIndex = 51;
            gridControlLocaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewLocaciones });
            // 
            // gridViewLocaciones
            // 
            gridViewLocaciones.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewLocaciones.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewLocaciones.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewLocaciones.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn13, gridColumn14, gridColumn1, gridColumn2 });
            gridViewLocaciones.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewLocaciones.GridControl = gridControlLocaciones;
            gridViewLocaciones.Name = "gridViewLocaciones";
            gridViewLocaciones.OptionsBehavior.Editable = false;
            gridViewLocaciones.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewLocaciones.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridColumn13
            // 
            gridColumn13.Caption = "Nombre";
            gridColumn13.FieldName = "Nombre";
            gridColumn13.Name = "gridColumn13";
            gridColumn13.Visible = true;
            gridColumn13.VisibleIndex = 0;
            // 
            // gridColumn14
            // 
            gridColumn14.Caption = "Direccion";
            gridColumn14.FieldName = "Direccion";
            gridColumn14.Name = "gridColumn14";
            gridColumn14.Visible = true;
            gridColumn14.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Carga";
            gridColumn1.FieldName = "CargaTexto";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "Descarga";
            gridColumn2.FieldName = "DescargaTexto";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 3;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.Teal;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(769, 20);
            guna2HtmlLabel3.TabIndex = 50;
            guna2HtmlLabel3.Text = "Locaciones Actuales";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // bEditar
            // 
            bEditar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            bEditar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bEditar.Appearance.Options.UseBackColor = true;
            bEditar.Appearance.Options.UseFont = true;
            bEditar.Location = new Point(183, 517);
            bEditar.Name = "bEditar";
            bEditar.Size = new Size(161, 41);
            bEditar.TabIndex = 118;
            bEditar.Text = "Editar";
            bEditar.Click += btnEditar_Click;
            // 
            // bEliminar
            // 
            bEliminar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            bEliminar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bEliminar.Appearance.Options.UseBackColor = true;
            bEliminar.Appearance.Options.UseFont = true;
            bEliminar.Location = new Point(16, 517);
            bEliminar.Name = "bEliminar";
            bEliminar.Size = new Size(161, 41);
            bEliminar.TabIndex = 117;
            bEliminar.Text = "Eliminar";
            bEliminar.Click += btnEliminar_Click;
            // 
            // bPool
            // 
            bPool.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            bPool.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bPool.Appearance.Options.UseBackColor = true;
            bPool.Appearance.Options.UseFont = true;
            bPool.Location = new Point(456, 517);
            bPool.Name = "bPool";
            bPool.Size = new Size(161, 41);
            bPool.TabIndex = 116;
            bPool.Text = "Pool";
            bPool.Click += btnPool_Click;
            // 
            // bAgregar
            // 
            bAgregar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            bAgregar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bAgregar.Appearance.Options.UseBackColor = true;
            bAgregar.Appearance.Options.UseFont = true;
            bAgregar.Location = new Point(623, 517);
            bAgregar.Name = "bAgregar";
            bAgregar.Size = new Size(161, 41);
            bAgregar.TabIndex = 115;
            bAgregar.Text = "Agregar";
            bAgregar.Click += btnAgregar_Click;
            // 
            // MenuLocacionesForm
            // 
            Appearance.BackColor = Color.FromArgb(41, 44, 53);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(bEditar);
            Controls.Add(bEliminar);
            Controls.Add(bPool);
            Controls.Add(bAgregar);
            Controls.Add(guna2Panel5);
            Controls.Add(guna2Panel3);
            Name = "MenuLocacionesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuLocacionesForm";
            Load += MenuLocaciones_Load;
            guna2Panel3.ResumeLayout(false);
            guna2Panel3.PerformLayout();
            guna2Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlLocaciones).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLocaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private DevExpress.XtraGrid.GridControl gridControlLocaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLocaciones;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private DevExpress.XtraEditors.SimpleButton bEditar;
        private DevExpress.XtraEditors.SimpleButton bEliminar;
        private DevExpress.XtraEditors.SimpleButton bPool;
        private DevExpress.XtraEditors.SimpleButton bAgregar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}