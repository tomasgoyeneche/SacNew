namespace Configuraciones.Views.AbmUsuarios
{
    partial class MenuUsuarioForm
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
            gridControlUsuarios = new DevExpress.XtraGrid.GridControl();
            gridViewUsuarios = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bAgregar = new DevExpress.XtraEditors.SimpleButton();
            bPermisos = new DevExpress.XtraEditors.SimpleButton();
            bEliminar = new DevExpress.XtraEditors.SimpleButton();
            bEditar = new DevExpress.XtraEditors.SimpleButton();
            guna2Panel3.SuspendLayout();
            guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlUsuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsuarios).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel1);
            guna2Panel3.CustomizableEdges = customizableEdges1;
            guna2Panel3.FillColor = Color.Sienna;
            guna2Panel3.Location = new Point(0, 12);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 40;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(17, 8);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(185, 25);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Menu Abm Usuarios";
            // 
            // guna2Panel5
            // 
            guna2Panel5.BackColor = Color.Transparent;
            guna2Panel5.Controls.Add(gridControlUsuarios);
            guna2Panel5.Controls.Add(guna2HtmlLabel3);
            guna2Panel5.CustomizableEdges = customizableEdges3;
            guna2Panel5.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel5.Location = new Point(16, 70);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel5.Size = new Size(769, 437);
            guna2Panel5.TabIndex = 53;
            // 
            // gridControlUsuarios
            // 
            gridControlUsuarios.Dock = DockStyle.Bottom;
            gridControlUsuarios.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlUsuarios.Location = new Point(0, 44);
            gridControlUsuarios.MainView = gridViewUsuarios;
            gridControlUsuarios.Name = "gridControlUsuarios";
            gridControlUsuarios.Size = new Size(769, 393);
            gridControlUsuarios.TabIndex = 51;
            gridControlUsuarios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUsuarios });
            // 
            // gridViewUsuarios
            // 
            gridViewUsuarios.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewUsuarios.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewUsuarios.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewUsuarios.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn13, gridColumn14 });
            gridViewUsuarios.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewUsuarios.GridControl = gridControlUsuarios;
            gridViewUsuarios.Name = "gridViewUsuarios";
            gridViewUsuarios.OptionsBehavior.Editable = false;
            gridViewUsuarios.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewUsuarios.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridColumn13
            // 
            gridColumn13.Caption = "Usuario";
            gridColumn13.FieldName = "NombreUsuario";
            gridColumn13.Name = "gridColumn13";
            gridColumn13.Visible = true;
            gridColumn13.VisibleIndex = 0;
            // 
            // gridColumn14
            // 
            gridColumn14.Caption = "Empresa";
            gridColumn14.FieldName = "NombreCompleto";
            gridColumn14.Name = "gridColumn14";
            gridColumn14.Visible = true;
            gridColumn14.VisibleIndex = 1;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.Sienna;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(769, 20);
            guna2HtmlLabel3.TabIndex = 50;
            guna2HtmlLabel3.Text = "Usuarios Actuales";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // bAgregar
            // 
            bAgregar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            bAgregar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bAgregar.Appearance.Options.UseBackColor = true;
            bAgregar.Appearance.Options.UseFont = true;
            bAgregar.Location = new Point(624, 519);
            bAgregar.Name = "bAgregar";
            bAgregar.Size = new Size(161, 41);
            bAgregar.TabIndex = 54;
            bAgregar.Text = "Agregar";
            bAgregar.Click += btnAgregar_Click;
            // 
            // bPermisos
            // 
            bPermisos.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            bPermisos.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bPermisos.Appearance.Options.UseBackColor = true;
            bPermisos.Appearance.Options.UseFont = true;
            bPermisos.Location = new Point(455, 519);
            bPermisos.Name = "bPermisos";
            bPermisos.Size = new Size(161, 41);
            bPermisos.TabIndex = 112;
            bPermisos.Text = "Permisos";
            bPermisos.Click += btnPermisos_Click;
            // 
            // bEliminar
            // 
            bEliminar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            bEliminar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bEliminar.Appearance.Options.UseBackColor = true;
            bEliminar.Appearance.Options.UseFont = true;
            bEliminar.Location = new Point(16, 519);
            bEliminar.Name = "bEliminar";
            bEliminar.Size = new Size(161, 41);
            bEliminar.TabIndex = 113;
            bEliminar.Text = "Eliminar";
            bEliminar.Click += btnEliminar_Click;
            // 
            // bEditar
            // 
            bEditar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            bEditar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bEditar.Appearance.Options.UseBackColor = true;
            bEditar.Appearance.Options.UseFont = true;
            bEditar.Location = new Point(185, 519);
            bEditar.Name = "bEditar";
            bEditar.Size = new Size(161, 41);
            bEditar.TabIndex = 114;
            bEditar.Text = "Editar";
            bEditar.Click += btnEditar_Click;
            // 
            // MenuUsuarioForm
            // 
            Appearance.BackColor = Color.FromArgb(41, 44, 53);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 572);
            Controls.Add(bEditar);
            Controls.Add(bEliminar);
            Controls.Add(bPermisos);
            Controls.Add(bAgregar);
            Controls.Add(guna2Panel5);
            Controls.Add(guna2Panel3);
            MaximizeBox = false;
            Name = "MenuUsuarioForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuUsuarioForm";
            Load += MenuUsuariosForm_Load;
            guna2Panel3.ResumeLayout(false);
            guna2Panel3.PerformLayout();
            guna2Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlUsuarios).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private DevExpress.XtraEditors.SimpleButton bAgregar;
        private DevExpress.XtraEditors.SimpleButton bPermisos;
        private DevExpress.XtraEditors.SimpleButton bEliminar;
        private DevExpress.XtraEditors.SimpleButton bEditar;
        private DevExpress.XtraGrid.GridControl gridControlUsuarios;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsuarios;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
    }
}