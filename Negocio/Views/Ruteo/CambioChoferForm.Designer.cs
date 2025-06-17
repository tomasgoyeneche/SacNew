namespace GestionFlota.Views
{
    partial class CambioChoferForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            gridControlChoferes = new DevExpress.XtraGrid.GridControl();
            gridViewChoferes = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            gridControlNovedades = new DevExpress.XtraGrid.GridControl();
            gridViewNovedades = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            lblOrigen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtDni = new Guna.UI2.WinForms.Guna2TextBox();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bGuardar = new DevExpress.XtraEditors.SimpleButton();
            bBajarChofer = new DevExpress.XtraEditors.SimpleButton();
            bCancelar = new DevExpress.XtraEditors.SimpleButton();
            dateEditFechaCambio = new DevExpress.XtraEditors.DateEdit();
            guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlChoferes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewChoferes).BeginInit();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlNovedades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNovedades).BeginInit();
            guna2Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dateEditFechaCambio.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEditFechaCambio.Properties.CalendarTimeProperties).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel5
            // 
            guna2Panel5.BackColor = Color.Transparent;
            guna2Panel5.Controls.Add(guna2HtmlLabel3);
            guna2Panel5.Controls.Add(gridControlChoferes);
            guna2Panel5.CustomizableEdges = customizableEdges9;
            guna2Panel5.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel5.Location = new Point(24, 69);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel5.Size = new Size(752, 230);
            guna2Panel5.TabIndex = 44;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.Goldenrod;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(752, 20);
            guna2HtmlLabel3.TabIndex = 50;
            guna2HtmlLabel3.Text = "Choferes Disponibles";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // gridControlChoferes
            // 
            gridControlChoferes.Dock = DockStyle.Bottom;
            gridControlChoferes.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlChoferes.Location = new Point(0, 36);
            gridControlChoferes.MainView = gridViewChoferes;
            gridControlChoferes.Name = "gridControlChoferes";
            gridControlChoferes.Size = new Size(752, 194);
            gridControlChoferes.TabIndex = 39;
            gridControlChoferes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewChoferes });
            // 
            // gridViewChoferes
            // 
            gridViewChoferes.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewChoferes.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewChoferes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewChoferes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn7, gridColumn1, gridColumn2 });
            gridViewChoferes.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewChoferes.GridControl = gridControlChoferes;
            gridViewChoferes.Name = "gridViewChoferes";
            gridViewChoferes.OptionsBehavior.Editable = false;
            gridViewChoferes.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewChoferes.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridColumn7
            // 
            gridColumn7.Caption = "Apellido";
            gridColumn7.FieldName = "Apellido";
            gridColumn7.Name = "gridColumn7";
            gridColumn7.Visible = true;
            gridColumn7.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Nombre";
            gridColumn1.FieldName = "Nombre";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "Empresa";
            gridColumn2.FieldName = "Empresa_Nombre";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.Controls.Add(guna2HtmlLabel1);
            guna2Panel1.Controls.Add(gridControlNovedades);
            guna2Panel1.CustomizableEdges = customizableEdges11;
            guna2Panel1.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel1.Location = new Point(24, 374);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel1.Size = new Size(752, 129);
            guna2Panel1.TabIndex = 51;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.Goldenrod;
            guna2HtmlLabel1.Dock = DockStyle.Top;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = Color.Black;
            guna2HtmlLabel1.Location = new Point(0, 0);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(752, 20);
            guna2HtmlLabel1.TabIndex = 50;
            guna2HtmlLabel1.Text = "Francos";
            guna2HtmlLabel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // gridControlNovedades
            // 
            gridControlNovedades.Dock = DockStyle.Bottom;
            gridControlNovedades.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlNovedades.Location = new Point(0, 39);
            gridControlNovedades.MainView = gridViewNovedades;
            gridControlNovedades.Name = "gridControlNovedades";
            gridControlNovedades.Size = new Size(752, 90);
            gridControlNovedades.TabIndex = 39;
            gridControlNovedades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNovedades });
            // 
            // gridViewNovedades
            // 
            gridViewNovedades.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewNovedades.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewNovedades.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewNovedades.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn3, gridColumn4, gridColumn5, gridColumn6 });
            gridViewNovedades.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewNovedades.GridControl = gridControlNovedades;
            gridViewNovedades.Name = "gridViewNovedades";
            gridViewNovedades.OptionsBehavior.Editable = false;
            gridViewNovedades.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewNovedades.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Ausencias";
            gridColumn3.FieldName = "Descripcion";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "Desde";
            gridColumn4.FieldName = "FechaInicio";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "Hasta";
            gridColumn5.FieldName = "FechaFin";
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "Observaciones";
            gridColumn6.FieldName = "Observaciones";
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 3;
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = false;
            lblOrigen.BackColor = Color.Goldenrod;
            lblOrigen.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrigen.ForeColor = SystemColors.ControlLight;
            lblOrigen.Location = new Point(458, 22);
            lblOrigen.Margin = new Padding(4, 3, 4, 3);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(141, 26);
            lblOrigen.TabIndex = 71;
            lblOrigen.Text = "Desde";
            lblOrigen.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.Goldenrod;
            guna2HtmlLabel2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ActiveCaptionText;
            guna2HtmlLabel2.Location = new Point(24, 313);
            guna2HtmlLabel2.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(752, 25);
            guna2HtmlLabel2.TabIndex = 72;
            guna2HtmlLabel2.Text = "Novedad";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtDni
            // 
            txtDni.BackColor = Color.FromArgb(26, 29, 35);
            txtDni.CustomizableEdges = customizableEdges13;
            txtDni.DefaultText = "";
            txtDni.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtDni.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtDni.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtDni.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtDni.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtDni.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDni.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtDni.Location = new Point(24, 338);
            txtDni.Margin = new Padding(4, 3, 4, 3);
            txtDni.MaxLength = 255;
            txtDni.Name = "txtDni";
            txtDni.PasswordChar = '\0';
            txtDni.PlaceholderText = "Ingrese Novedad (Opcional)";
            txtDni.RightToLeft = RightToLeft.No;
            txtDni.SelectedText = "";
            txtDni.ShadowDecoration.CustomizableEdges = customizableEdges14;
            txtDni.Size = new Size(752, 22);
            txtDni.TabIndex = 78;
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel17);
            guna2Panel10.CustomizableEdges = customizableEdges15;
            guna2Panel10.FillColor = Color.Goldenrod;
            guna2Panel10.Location = new Point(1, 15);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges16;
            guna2Panel10.Size = new Size(253, 38);
            guna2Panel10.TabIndex = 79;
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
            guna2HtmlLabel17.Text = "Cambio Chofer";
            // 
            // bGuardar
            // 
            bGuardar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            bGuardar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bGuardar.Appearance.Options.UseBackColor = true;
            bGuardar.Appearance.Options.UseFont = true;
            bGuardar.Location = new Point(615, 515);
            bGuardar.Name = "bGuardar";
            bGuardar.Size = new Size(161, 41);
            bGuardar.TabIndex = 119;
            bGuardar.Text = "Guardar";
            // 
            // bBajarChofer
            // 
            bBajarChofer.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            bBajarChofer.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bBajarChofer.Appearance.Options.UseBackColor = true;
            bBajarChofer.Appearance.Options.UseFont = true;
            bBajarChofer.Location = new Point(198, 516);
            bBajarChofer.Name = "bBajarChofer";
            bBajarChofer.Size = new Size(161, 40);
            bBajarChofer.TabIndex = 118;
            bBajarChofer.Text = "Bajar Chofer";
            // 
            // bCancelar
            // 
            bCancelar.Appearance.BackColor = Color.DimGray;
            bCancelar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bCancelar.Appearance.Options.UseBackColor = true;
            bCancelar.Appearance.Options.UseFont = true;
            bCancelar.Location = new Point(24, 516);
            bCancelar.Name = "bCancelar";
            bCancelar.Size = new Size(161, 40);
            bCancelar.TabIndex = 117;
            bCancelar.Text = "Cancelar";
            bCancelar.Click += bCancelar_Click;
            // 
            // dateEditFechaCambio
            // 
            dateEditFechaCambio.EditValue = null;
            dateEditFechaCambio.Location = new Point(599, 22);
            dateEditFechaCambio.Name = "dateEditFechaCambio";
            dateEditFechaCambio.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateEditFechaCambio.Properties.Appearance.Options.UseFont = true;
            dateEditFechaCambio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditFechaCambio.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            dateEditFechaCambio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditFechaCambio.Properties.DisplayFormat.FormatString = "g";
            dateEditFechaCambio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEditFechaCambio.Properties.EditFormat.FormatString = "g";
            dateEditFechaCambio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEditFechaCambio.Properties.MaskSettings.Set("mask", "g");
            dateEditFechaCambio.Size = new Size(177, 26);
            dateEditFechaCambio.TabIndex = 120;
            // 
            // CambioChoferForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(dateEditFechaCambio);
            Controls.Add(bGuardar);
            Controls.Add(bBajarChofer);
            Controls.Add(bCancelar);
            Controls.Add(guna2Panel10);
            Controls.Add(txtDni);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(lblOrigen);
            Controls.Add(guna2Panel1);
            Controls.Add(guna2Panel5);
            MaximizeBox = false;
            Name = "CambioChoferForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CambioChoferForm";
            guna2Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlChoferes).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewChoferes).EndInit();
            guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlNovedades).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNovedades).EndInit();
            guna2Panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dateEditFechaCambio.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEditFechaCambio.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private DevExpress.XtraGrid.GridControl gridControlChoferes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChoferes;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private DevExpress.XtraGrid.GridControl gridControlNovedades;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNovedades;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblOrigen;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2TextBox txtDni;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
        private DevExpress.XtraEditors.SimpleButton bGuardar;
        private DevExpress.XtraEditors.SimpleButton bBajarChofer;
        private DevExpress.XtraEditors.SimpleButton bCancelar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.DateEdit dateEditFechaCambio;
    }
}