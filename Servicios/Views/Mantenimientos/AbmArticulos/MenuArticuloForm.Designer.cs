namespace Servicios.Views.Mantenimiento
{
    partial class MenuArticuloForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            gridControlArt = new DevExpress.XtraGrid.GridControl();
            gridViewArt = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            Descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnAgregarArt = new Guna.UI2.WinForms.Guna2Button();
            btnEditarArt = new Guna.UI2.WinForms.Guna2Button();
            btnEliminarArt = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlArt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArt).BeginInit();
            guna2Panel10.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel5
            // 
            guna2Panel5.BackColor = Color.Transparent;
            guna2Panel5.Controls.Add(gridControlArt);
            guna2Panel5.Controls.Add(guna2HtmlLabel3);
            guna2Panel5.CustomizableEdges = customizableEdges1;
            guna2Panel5.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel5.Location = new Point(14, 59);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel5.Size = new Size(768, 454);
            guna2Panel5.TabIndex = 52;
            // 
            // gridControlArt
            // 
            gridControlArt.Dock = DockStyle.Bottom;
            gridControlArt.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlArt.Location = new Point(0, 31);
            gridControlArt.MainView = gridViewArt;
            gridControlArt.Name = "gridControlArt";
            gridControlArt.Size = new Size(768, 423);
            gridControlArt.TabIndex = 51;
            gridControlArt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewArt });
            // 
            // gridViewArt
            // 
            gridViewArt.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewArt.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewArt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewArt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn6, gridColumn7, Descripcion, gridColumn10, gridColumn29 });
            gridViewArt.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            gridViewArt.GridControl = gridControlArt;
            gridViewArt.Name = "gridViewArt";
            gridViewArt.OptionsBehavior.Editable = false;
            gridViewArt.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewArt.OptionsSelection.EnableAppearanceHotTrackedRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewArt.OptionsView.EnableAppearanceEvenRow = true;
            gridViewArt.RowHeight = 20;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "Codigo";
            gridColumn6.FieldName = "Codigo";
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 0;
            gridColumn6.Width = 118;
            // 
            // gridColumn7
            // 
            gridColumn7.Caption = "Nombre";
            gridColumn7.FieldName = "Nombre";
            gridColumn7.Name = "gridColumn7";
            gridColumn7.Visible = true;
            gridColumn7.VisibleIndex = 1;
            gridColumn7.Width = 169;
            // 
            // Descripcion
            // 
            Descripcion.Caption = "Descripcion";
            Descripcion.FieldName = "Descripcion";
            Descripcion.Name = "Descripcion";
            Descripcion.Visible = true;
            Descripcion.VisibleIndex = 2;
            Descripcion.Width = 253;
            // 
            // gridColumn10
            // 
            gridColumn10.Caption = "Precio";
            gridColumn10.FieldName = "PrecioUnitario";
            gridColumn10.Name = "gridColumn10";
            gridColumn10.Visible = true;
            gridColumn10.VisibleIndex = 3;
            gridColumn10.Width = 70;
            // 
            // gridColumn29
            // 
            gridColumn29.Caption = "Pedido Minimo";
            gridColumn29.FieldName = "PedidoMinimo";
            gridColumn29.Name = "gridColumn29";
            gridColumn29.Visible = true;
            gridColumn29.VisibleIndex = 4;
            gridColumn29.Width = 70;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.SeaGreen;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(768, 20);
            guna2HtmlLabel3.TabIndex = 50;
            guna2HtmlLabel3.Text = "Articulos";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnAgregarArt
            // 
            btnAgregarArt.CustomizableEdges = customizableEdges3;
            btnAgregarArt.DisabledState.BorderColor = Color.DarkGray;
            btnAgregarArt.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAgregarArt.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAgregarArt.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAgregarArt.FillColor = Color.ForestGreen;
            btnAgregarArt.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAgregarArt.ForeColor = Color.White;
            btnAgregarArt.Location = new Point(650, 521);
            btnAgregarArt.Name = "btnAgregarArt";
            btnAgregarArt.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAgregarArt.Size = new Size(132, 37);
            btnAgregarArt.TabIndex = 55;
            btnAgregarArt.Text = "Agregar";
            btnAgregarArt.Click += btnAgregarNovedad_Click;
            // 
            // btnEditarArt
            // 
            btnEditarArt.CustomizableEdges = customizableEdges5;
            btnEditarArt.DisabledState.BorderColor = Color.DarkGray;
            btnEditarArt.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEditarArt.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEditarArt.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEditarArt.FillColor = Color.Goldenrod;
            btnEditarArt.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEditarArt.ForeColor = Color.White;
            btnEditarArt.Location = new Point(156, 521);
            btnEditarArt.Name = "btnEditarArt";
            btnEditarArt.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnEditarArt.Size = new Size(132, 37);
            btnEditarArt.TabIndex = 54;
            btnEditarArt.Text = "Editar";
            btnEditarArt.Click += btnEditarNovedad_Click;
            // 
            // btnEliminarArt
            // 
            btnEliminarArt.CustomizableEdges = customizableEdges7;
            btnEliminarArt.DisabledState.BorderColor = Color.DarkGray;
            btnEliminarArt.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEliminarArt.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEliminarArt.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEliminarArt.FillColor = Color.Brown;
            btnEliminarArt.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminarArt.ForeColor = Color.White;
            btnEliminarArt.Location = new Point(14, 521);
            btnEliminarArt.Name = "btnEliminarArt";
            btnEliminarArt.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnEliminarArt.Size = new Size(132, 37);
            btnEliminarArt.TabIndex = 53;
            btnEliminarArt.Text = "Eliminar";
            btnEliminarArt.Click += btnEliminar_Click;
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel17);
            guna2Panel10.CustomizableEdges = customizableEdges9;
            guna2Panel10.FillColor = Color.SeaGreen;
            guna2Panel10.Location = new Point(0, 7);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel10.Size = new Size(253, 38);
            guna2Panel10.TabIndex = 56;
            // 
            // guna2HtmlLabel17
            // 
            guna2HtmlLabel17.AutoSize = false;
            guna2HtmlLabel17.BackColor = Color.Transparent;
            guna2HtmlLabel17.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel17.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel17.Location = new Point(8, 5);
            guna2HtmlLabel17.Name = "guna2HtmlLabel17";
            guna2HtmlLabel17.Size = new Size(242, 28);
            guna2HtmlLabel17.TabIndex = 14;
            guna2HtmlLabel17.Text = "Menu Articulos";
            // 
            // MenuArticuloForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(guna2Panel10);
            Controls.Add(btnAgregarArt);
            Controls.Add(btnEditarArt);
            Controls.Add(btnEliminarArt);
            Controls.Add(guna2Panel5);
            MaximizeBox = false;
            Name = "MenuArticuloForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuArticuloForm";
            guna2Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlArt).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArt).EndInit();
            guna2Panel10.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private DevExpress.XtraGrid.GridControl gridControlArt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewArt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn Descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button btnAgregarArt;
        private Guna.UI2.WinForms.Guna2Button btnEditarArt;
        private Guna.UI2.WinForms.Guna2Button btnEliminarArt;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
    }
}