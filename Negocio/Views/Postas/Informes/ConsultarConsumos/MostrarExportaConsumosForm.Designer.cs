namespace GestionFlota.Views.Postas.Informes.ConsultarConsumos
{
    partial class MostrarExportaConsumosForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MostrarExportaConsumosForm));
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            gridControlConsumos = new DevExpress.XtraGrid.GridControl();
            gridViewConsumos = new DevExpress.XtraGrid.Views.Grid.GridView();
            tabPage2 = new TabPage();
            gridControlTotales = new DevExpress.XtraGrid.GridControl();
            gridViewTotales = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnExportar = new DevExpress.XtraEditors.SimpleButton();
            guna2Panel3.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlConsumos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewConsumos).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlTotales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTotales).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel3);
            guna2Panel3.CustomizableEdges = customizableEdges1;
            guna2Panel3.FillColor = Color.Gray;
            guna2Panel3.Location = new Point(1, 12);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 56;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel3.Location = new Point(17, 8);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(172, 25);
            guna2HtmlLabel3.TabIndex = 0;
            guna2HtmlLabel3.Text = "Informe Consumos";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(11, 65);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 489);
            tabControl1.TabIndex = 59;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(gridControlConsumos);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(768, 463);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Consumos";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridControlConsumos
            // 
            gridControlConsumos.Dock = DockStyle.Fill;
            gridControlConsumos.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlConsumos.Location = new Point(3, 3);
            gridControlConsumos.MainView = gridViewConsumos;
            gridControlConsumos.Name = "gridControlConsumos";
            gridControlConsumos.Size = new Size(762, 457);
            gridControlConsumos.TabIndex = 60;
            gridControlConsumos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewConsumos });
            // 
            // gridViewConsumos
            // 
            gridViewConsumos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewConsumos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewConsumos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewConsumos.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewConsumos.GridControl = gridControlConsumos;
            gridViewConsumos.Name = "gridViewConsumos";
            gridViewConsumos.OptionsBehavior.Editable = false;
            gridViewConsumos.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewConsumos.OptionsView.ColumnAutoWidth = false;
            gridViewConsumos.OptionsView.EnableAppearanceEvenRow = true;
            gridViewConsumos.OptionsView.EnableAppearanceOddRow = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(gridControlTotales);
            tabPage2.Location = new Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 463);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Totales";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridControlTotales
            // 
            gridControlTotales.Dock = DockStyle.Fill;
            gridControlTotales.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlTotales.Location = new Point(3, 3);
            gridControlTotales.MainView = gridViewTotales;
            gridControlTotales.Name = "gridControlTotales";
            gridControlTotales.Size = new Size(762, 457);
            gridControlTotales.TabIndex = 61;
            gridControlTotales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTotales });
            // 
            // gridViewTotales
            // 
            gridViewTotales.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewTotales.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewTotales.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewTotales.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewTotales.GridControl = gridControlTotales;
            gridViewTotales.Name = "gridViewTotales";
            gridViewTotales.OptionsBehavior.Editable = false;
            gridViewTotales.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewTotales.OptionsView.ColumnAutoWidth = false;
            gridViewTotales.OptionsView.EnableAppearanceEvenRow = true;
            gridViewTotales.OptionsView.EnableAppearanceOddRow = true;
            // 
            // btnExportar
            // 
            btnExportar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            btnExportar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Appearance.Options.UseBackColor = true;
            btnExportar.Appearance.Options.UseFont = true;
            btnExportar.ImageOptions.Image = (Image)resources.GetObject("btnExportar.ImageOptions.Image");
            btnExportar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            btnExportar.Location = new Point(655, 12);
            btnExportar.Name = "btnExportar";
            btnExportar.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            btnExportar.Size = new Size(132, 43);
            btnExportar.TabIndex = 60;
            btnExportar.Text = "Exportar";
            btnExportar.Click += btnExportar_Click;
            // 
            // MostrarExportaConsumosForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(btnExportar);
            Controls.Add(tabControl1);
            Controls.Add(guna2Panel3);
            MaximizeBox = false;
            Name = "MostrarExportaConsumosForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MostrarExportaConsumosForm";
            guna2Panel3.ResumeLayout(false);
            guna2Panel3.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlConsumos).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewConsumos).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlTotales).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTotales).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DevExpress.XtraGrid.GridControl gridControlConsumos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewConsumos;
        private DevExpress.XtraEditors.SimpleButton btnExportar;
        private DevExpress.XtraGrid.GridControl gridControlTotales;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTotales;
    }
}