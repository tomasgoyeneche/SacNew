namespace GestionFlota.Views.Alertas
{
    partial class AlertasForm
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
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            gridControlAlertas = new DevExpress.XtraGrid.GridControl();
            gridViewAlertas = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnAgregarNovedad = new Guna.UI2.WinForms.Guna2Button();
            btnEditarNovedad = new Guna.UI2.WinForms.Guna2Button();
            btnEliminar = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlAlertas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewAlertas).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel17);
            guna2Panel10.CustomizableEdges = customizableEdges1;
            guna2Panel10.FillColor = Color.Goldenrod;
            guna2Panel10.Location = new Point(1, 12);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel10.Size = new Size(253, 38);
            guna2Panel10.TabIndex = 39;
            // 
            // guna2HtmlLabel17
            // 
            guna2HtmlLabel17.AutoSize = false;
            guna2HtmlLabel17.BackColor = Color.Transparent;
            guna2HtmlLabel17.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel17.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel17.Location = new Point(8, 5);
            guna2HtmlLabel17.Name = "guna2HtmlLabel17";
            guna2HtmlLabel17.Size = new Size(190, 28);
            guna2HtmlLabel17.TabIndex = 14;
            guna2HtmlLabel17.Text = "Alertas";
            // 
            // gridControlAlertas
            // 
            gridControlAlertas.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlAlertas.Location = new Point(12, 66);
            gridControlAlertas.MainView = gridViewAlertas;
            gridControlAlertas.Name = "gridControlAlertas";
            gridControlAlertas.Size = new Size(776, 442);
            gridControlAlertas.TabIndex = 40;
            gridControlAlertas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewAlertas });
            // 
            // gridViewAlertas
            // 
            gridViewAlertas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewAlertas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewAlertas.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewAlertas.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewAlertas.GridControl = gridControlAlertas;
            gridViewAlertas.Name = "gridViewAlertas";
            gridViewAlertas.OptionsBehavior.Editable = false;
            gridViewAlertas.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewAlertas.OptionsView.EnableAppearanceEvenRow = true;
            gridViewAlertas.OptionsView.EnableAppearanceOddRow = true;
            // 
            // btnAgregarNovedad
            // 
            btnAgregarNovedad.CustomizableEdges = customizableEdges3;
            btnAgregarNovedad.DisabledState.BorderColor = Color.DarkGray;
            btnAgregarNovedad.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAgregarNovedad.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAgregarNovedad.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAgregarNovedad.FillColor = Color.ForestGreen;
            btnAgregarNovedad.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAgregarNovedad.ForeColor = Color.White;
            btnAgregarNovedad.Location = new Point(656, 521);
            btnAgregarNovedad.Name = "btnAgregarNovedad";
            btnAgregarNovedad.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAgregarNovedad.Size = new Size(132, 37);
            btnAgregarNovedad.TabIndex = 43;
            btnAgregarNovedad.Text = "Agregar";
            btnAgregarNovedad.Click += btnAgregarNovedad_Click;
            // 
            // btnEditarNovedad
            // 
            btnEditarNovedad.CustomizableEdges = customizableEdges5;
            btnEditarNovedad.DisabledState.BorderColor = Color.DarkGray;
            btnEditarNovedad.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEditarNovedad.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEditarNovedad.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEditarNovedad.FillColor = Color.Goldenrod;
            btnEditarNovedad.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEditarNovedad.ForeColor = Color.White;
            btnEditarNovedad.Location = new Point(150, 521);
            btnEditarNovedad.Name = "btnEditarNovedad";
            btnEditarNovedad.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnEditarNovedad.Size = new Size(132, 37);
            btnEditarNovedad.TabIndex = 42;
            btnEditarNovedad.Text = "Editar";
            btnEditarNovedad.Click += btnEditarNovedad_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.CustomizableEdges = customizableEdges7;
            btnEliminar.DisabledState.BorderColor = Color.DarkGray;
            btnEliminar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEliminar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEliminar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEliminar.FillColor = Color.Brown;
            btnEliminar.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(12, 521);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnEliminar.Size = new Size(132, 37);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // AlertasForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(803, 573);
            Controls.Add(btnAgregarNovedad);
            Controls.Add(btnEditarNovedad);
            Controls.Add(btnEliminar);
            Controls.Add(gridControlAlertas);
            Controls.Add(guna2Panel10);
            Name = "AlertasForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AlertasForm";
            guna2Panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlAlertas).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewAlertas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
        private DevExpress.XtraGrid.GridControl gridControlAlertas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAlertas;
        private Guna.UI2.WinForms.Guna2Button btnAgregarNovedad;
        private Guna.UI2.WinForms.Guna2Button btnEditarNovedad;
        private Guna.UI2.WinForms.Guna2Button btnEliminar;
    }
}