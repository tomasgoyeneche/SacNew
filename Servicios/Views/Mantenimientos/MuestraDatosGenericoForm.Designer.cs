namespace Servicios.Views.Mantenimientos
{
    partial class MuestraDatosGenericoForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            gridControlGenerico = new DevExpress.XtraGrid.GridControl();
            gridViewGenerico = new DevExpress.XtraGrid.Views.Grid.GridView();
            lblDescripcion = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel10.SuspendLayout();
            guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlGenerico).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewGenerico).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(lblTitulo);
            guna2Panel10.CustomizableEdges = customizableEdges5;
            guna2Panel10.FillColor = Color.SeaGreen;
            guna2Panel10.Location = new Point(0, 12);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel10.Size = new Size(330, 38);
            guna2Panel10.TabIndex = 73;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = false;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = SystemColors.ControlLight;
            lblTitulo.Location = new Point(8, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(319, 28);
            lblTitulo.TabIndex = 14;
            lblTitulo.Text = "Mantenimientos";
            // 
            // guna2Panel5
            // 
            guna2Panel5.BackColor = Color.Transparent;
            guna2Panel5.Controls.Add(gridControlGenerico);
            guna2Panel5.Controls.Add(lblDescripcion);
            guna2Panel5.CustomizableEdges = customizableEdges7;
            guna2Panel5.Dock = DockStyle.Bottom;
            guna2Panel5.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel5.Location = new Point(0, 76);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel5.Size = new Size(798, 492);
            guna2Panel5.TabIndex = 72;
            // 
            // gridControlGenerico
            // 
            gridControlGenerico.Dock = DockStyle.Bottom;
            gridControlGenerico.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlGenerico.Location = new Point(0, 37);
            gridControlGenerico.MainView = gridViewGenerico;
            gridControlGenerico.Name = "gridControlGenerico";
            gridControlGenerico.Size = new Size(798, 455);
            gridControlGenerico.TabIndex = 51;
            gridControlGenerico.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewGenerico });
            // 
            // gridViewGenerico
            // 
            gridViewGenerico.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewGenerico.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewGenerico.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewGenerico.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            gridViewGenerico.GridControl = gridControlGenerico;
            gridViewGenerico.Name = "gridViewGenerico";
            gridViewGenerico.OptionsBehavior.Editable = false;
            gridViewGenerico.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewGenerico.OptionsSelection.EnableAppearanceHotTrackedRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewGenerico.OptionsView.EnableAppearanceEvenRow = true;
            gridViewGenerico.RowHeight = 20;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = false;
            lblDescripcion.BackColor = Color.SeaGreen;
            lblDescripcion.Dock = DockStyle.Top;
            lblDescripcion.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.ForeColor = Color.Black;
            lblDescripcion.Location = new Point(0, 0);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(798, 20);
            lblDescripcion.TabIndex = 50;
            lblDescripcion.Text = "Listado de Mantenimientos Activos";
            lblDescripcion.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // MuestraDatosGenericoForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(guna2Panel10);
            Controls.Add(guna2Panel5);
            Name = "MuestraDatosGenericoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MuestraDatosGenericoForm";
            guna2Panel10.ResumeLayout(false);
            guna2Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlGenerico).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewGenerico).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private DevExpress.XtraGrid.GridControl gridControlGenerico;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGenerico;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDescripcion;
    }
}