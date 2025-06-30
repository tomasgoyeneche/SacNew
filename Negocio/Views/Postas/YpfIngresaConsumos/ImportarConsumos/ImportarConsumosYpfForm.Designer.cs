namespace GestionFlota.Views.Postas.YpfIngresaConsumos.ImportarConsumos
{
    partial class ImportarConsumosYpfForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            gridControlDatos = new DevExpress.XtraGrid.GridControl();
            dgvDatos = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            btnExportarExcel = new Guna.UI2.WinForms.Guna2Button();
            btnImportar = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbPeriodos = new Guna.UI2.WinForms.Guna2ComboBox();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlDatos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).BeginInit();
            guna2Panel3.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.Controls.Add(guna2HtmlLabel4);
            guna2Panel1.Controls.Add(gridControlDatos);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel1.Location = new Point(17, 75);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(1164, 430);
            guna2Panel1.TabIndex = 44;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.AutoSize = false;
            guna2HtmlLabel4.BackColor = Color.SteelBlue;
            guna2HtmlLabel4.Dock = DockStyle.Top;
            guna2HtmlLabel4.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = Color.Black;
            guna2HtmlLabel4.Location = new Point(0, 0);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(1164, 20);
            guna2HtmlLabel4.TabIndex = 50;
            guna2HtmlLabel4.Text = "Datos Importados";
            guna2HtmlLabel4.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // gridControlDatos
            // 
            gridControlDatos.Dock = DockStyle.Bottom;
            gridControlDatos.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlDatos.Location = new Point(0, 40);
            gridControlDatos.MainView = dgvDatos;
            gridControlDatos.Name = "gridControlDatos";
            gridControlDatos.Size = new Size(1164, 390);
            gridControlDatos.TabIndex = 39;
            gridControlDatos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { dgvDatos });
            // 
            // dgvDatos
            // 
            dgvDatos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            dgvDatos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgvDatos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            dgvDatos.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            dgvDatos.GridControl = gridControlDatos;
            dgvDatos.Name = "dgvDatos";
            dgvDatos.OptionsBehavior.Editable = false;
            dgvDatos.OptionsSelection.EnableAppearanceFocusedCell = false;
            dgvDatos.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // btnGuardar
            // 
            btnGuardar.CustomizableEdges = customizableEdges3;
            btnGuardar.DisabledState.BorderColor = Color.DarkGray;
            btnGuardar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGuardar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGuardar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGuardar.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(1061, 517);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnGuardar.Size = new Size(120, 37);
            btnGuardar.TabIndex = 48;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.CustomizableEdges = customizableEdges5;
            btnExportarExcel.DisabledState.BorderColor = Color.DarkGray;
            btnExportarExcel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExportarExcel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExportarExcel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExportarExcel.FillColor = Color.ForestGreen;
            btnExportarExcel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExportarExcel.ForeColor = Color.White;
            btnExportarExcel.Location = new Point(152, 517);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnExportarExcel.Size = new Size(120, 37);
            btnExportarExcel.TabIndex = 47;
            btnExportarExcel.Text = "Exportar Excel";
            btnExportarExcel.Click += btnExportarExcel_Click;
            // 
            // btnImportar
            // 
            btnImportar.CustomizableEdges = customizableEdges7;
            btnImportar.DisabledState.BorderColor = Color.DarkGray;
            btnImportar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnImportar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnImportar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnImportar.FillColor = Color.Goldenrod;
            btnImportar.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnImportar.ForeColor = Color.White;
            btnImportar.Location = new Point(17, 517);
            btnImportar.Name = "btnImportar";
            btnImportar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnImportar.Size = new Size(120, 37);
            btnImportar.TabIndex = 46;
            btnImportar.Text = "Importar";
            btnImportar.Click += btnImportar_Click;
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel1);
            guna2Panel3.CustomizableEdges = customizableEdges9;
            guna2Panel3.FillColor = Color.SteelBlue;
            guna2Panel3.Location = new Point(0, 12);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 49;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(17, 8);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(213, 25);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Importa Consumos YPF";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(841, 27);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(65, 23);
            guna2HtmlLabel2.TabIndex = 51;
            guna2HtmlLabel2.Text = "Periodo:";
            // 
            // cmbPeriodos
            // 
            cmbPeriodos.BackColor = Color.Transparent;
            cmbPeriodos.CustomizableEdges = customizableEdges11;
            cmbPeriodos.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPeriodos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodos.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbPeriodos.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbPeriodos.Font = new Font("Segoe UI", 10F);
            cmbPeriodos.ForeColor = Color.FromArgb(68, 88, 112);
            cmbPeriodos.ItemHeight = 30;
            cmbPeriodos.Location = new Point(912, 21);
            cmbPeriodos.Name = "cmbPeriodos";
            cmbPeriodos.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cmbPeriodos.Size = new Size(269, 36);
            cmbPeriodos.TabIndex = 50;
            // 
            // ImportarConsumosYpfForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1198, 568);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(cmbPeriodos);
            Controls.Add(guna2Panel3);
            Controls.Add(btnGuardar);
            Controls.Add(btnExportarExcel);
            Controls.Add(btnImportar);
            Controls.Add(guna2Panel1);
            Name = "ImportarConsumosYpfForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ImportarConsumosYpfForm";
            Load += ImportarConsumosYPF_Load;
            guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlDatos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).EndInit();
            guna2Panel3.ResumeLayout(false);
            guna2Panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private DevExpress.XtraGrid.GridControl gridControlDatos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDatos;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnExportarExcel;
        private Guna.UI2.WinForms.Guna2Button btnImportar;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPeriodos;
    }
}