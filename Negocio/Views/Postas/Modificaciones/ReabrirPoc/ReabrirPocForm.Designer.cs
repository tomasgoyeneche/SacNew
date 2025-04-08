namespace GestionFlota.Views.Postas.Modificaciones.ReabrirPoc
{
    partial class ReabrirPocForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReabrirPocForm));
            gridControlPOC = new DevExpress.XtraGrid.GridControl();
            gridViewPOC = new DevExpress.XtraGrid.Views.Grid.GridView();
            bReabrirPoc = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)gridControlPOC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPOC).BeginInit();
            SuspendLayout();
            // 
            // gridControlPOC
            // 
            gridControlPOC.Dock = DockStyle.Top;
            gridControlPOC.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlPOC.Location = new Point(0, 0);
            gridControlPOC.MainView = gridViewPOC;
            gridControlPOC.Name = "gridControlPOC";
            gridControlPOC.Size = new Size(798, 513);
            gridControlPOC.TabIndex = 29;
            gridControlPOC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPOC });
            // 
            // gridViewPOC
            // 
            gridViewPOC.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewPOC.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewPOC.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewPOC.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewPOC.GridControl = gridControlPOC;
            gridViewPOC.Name = "gridViewPOC";
            gridViewPOC.OptionsBehavior.Editable = false;
            gridViewPOC.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewPOC.OptionsView.EnableAppearanceEvenRow = true;
            gridViewPOC.OptionsView.EnableAppearanceOddRow = true;
            // 
            // bReabrirPoc
            // 
            bReabrirPoc.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            bReabrirPoc.Appearance.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bReabrirPoc.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            bReabrirPoc.Appearance.Options.UseBackColor = true;
            bReabrirPoc.Appearance.Options.UseFont = true;
            bReabrirPoc.Appearance.Options.UseForeColor = true;
            bReabrirPoc.Appearance.Options.UseTextOptions = true;
            bReabrirPoc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bReabrirPoc.AppearanceDisabled.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            bReabrirPoc.AppearanceDisabled.Options.UseForeColor = true;
            bReabrirPoc.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bReabrirPoc.ImageOptions.SvgImage");
            bReabrirPoc.Location = new Point(634, 522);
            bReabrirPoc.Name = "bReabrirPoc";
            bReabrirPoc.Size = new Size(153, 36);
            bReabrirPoc.TabIndex = 30;
            bReabrirPoc.Text = "Reabrir";
            bReabrirPoc.Click += bReabrirPoc_Click;
            // 
            // ReabrirPocForm
            // 
            Appearance.BackColor = Color.FromArgb(40, 40, 40);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(bReabrirPoc);
            Controls.Add(gridControlPOC);
            MaximizeBox = false;
            Name = "ReabrirPocForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reabrir Poc";
            Load += ReabrirPocForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridControlPOC).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPOC).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlPOC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPOC;
        private DevExpress.XtraEditors.SimpleButton bReabrirPoc;
    }
}