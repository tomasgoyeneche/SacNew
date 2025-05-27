namespace GestionOperativa.Views
{
    partial class EliminarEventoForm
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
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            gridControlHistorico = new DevExpress.XtraGrid.GridControl();
            gridViewHistorico = new DevExpress.XtraGrid.Views.Grid.GridView();
            guna2Panel10.SuspendLayout();
            guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlHistorico).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHistorico).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel17);
            guna2Panel10.CustomizableEdges = customizableEdges1;
            guna2Panel10.FillColor = Color.Peru;
            guna2Panel10.Location = new Point(0, 12);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel10.Size = new Size(253, 29);
            guna2Panel10.TabIndex = 113;
            // 
            // guna2HtmlLabel17
            // 
            guna2HtmlLabel17.AutoSize = false;
            guna2HtmlLabel17.BackColor = Color.Transparent;
            guna2HtmlLabel17.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel17.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel17.Location = new Point(8, 1);
            guna2HtmlLabel17.Name = "guna2HtmlLabel17";
            guna2HtmlLabel17.Size = new Size(242, 24);
            guna2HtmlLabel17.TabIndex = 14;
            guna2HtmlLabel17.Text = "Eliminar Evento";
            // 
            // guna2Panel3
            // 
            guna2Panel3.BackColor = Color.Transparent;
            guna2Panel3.Controls.Add(guna2HtmlLabel4);
            guna2Panel3.Controls.Add(gridControlHistorico);
            guna2Panel3.CustomizableEdges = customizableEdges3;
            guna2Panel3.Dock = DockStyle.Bottom;
            guna2Panel3.FillColor = Color.FromArgb(42, 48, 56);
            guna2Panel3.Location = new Point(0, 54);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel3.Size = new Size(598, 314);
            guna2Panel3.TabIndex = 114;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.AutoSize = false;
            guna2HtmlLabel4.BackColor = Color.DarkGray;
            guna2HtmlLabel4.Dock = DockStyle.Top;
            guna2HtmlLabel4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = Color.Black;
            guna2HtmlLabel4.Location = new Point(0, 0);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(598, 20);
            guna2HtmlLabel4.TabIndex = 50;
            guna2HtmlLabel4.Text = "Historico";
            guna2HtmlLabel4.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // gridControlHistorico
            // 
            gridControlHistorico.Dock = DockStyle.Bottom;
            gridControlHistorico.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlHistorico.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridControlHistorico.Location = new Point(0, 30);
            gridControlHistorico.MainView = gridViewHistorico;
            gridControlHistorico.Name = "gridControlHistorico";
            gridControlHistorico.Size = new Size(598, 284);
            gridControlHistorico.TabIndex = 39;
            gridControlHistorico.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewHistorico });
            // 
            // gridViewHistorico
            // 
            gridViewHistorico.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewHistorico.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewHistorico.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewHistorico.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewHistorico.GridControl = gridControlHistorico;
            gridViewHistorico.Name = "gridViewHistorico";
            gridViewHistorico.OptionsBehavior.Editable = false;
            gridViewHistorico.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewHistorico.OptionsView.EnableAppearanceEvenRow = true;
            gridViewHistorico.OptionsView.EnableAppearanceOddRow = true;
            gridViewHistorico.OptionsView.ShowGroupPanel = false;
            gridViewHistorico.DoubleClick += gridViewHistorial_DoubleClick;
            // 
            // EliminarEventoForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 368);
            Controls.Add(guna2Panel3);
            Controls.Add(guna2Panel10);
            MaximizeBox = false;
            Name = "EliminarEventoForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EliminarEventoForm";
            guna2Panel10.ResumeLayout(false);
            guna2Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlHistorico).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHistorico).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private DevExpress.XtraGrid.GridControl gridControlHistorico;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHistorico;
    }
}