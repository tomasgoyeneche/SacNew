namespace Servicios
{
    partial class MenuVaporizados
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            gridControlVaporizados = new DevExpress.XtraGrid.GridControl();
            gridViewVaporizados = new DevExpress.XtraGrid.Views.Grid.GridView();
            bAgregarExterno = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            labelNovedades = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)gridControlVaporizados).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewVaporizados).BeginInit();
            guna2Panel10.SuspendLayout();
            SuspendLayout();
            // 
            // gridControlVaporizados
            // 
            gridControlVaporizados.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            gridControlVaporizados.Location = new Point(13, 65);
            gridControlVaporizados.MainView = gridViewVaporizados;
            gridControlVaporizados.Name = "gridControlVaporizados";
            gridControlVaporizados.Size = new Size(1068, 442);
            gridControlVaporizados.TabIndex = 30;
            gridControlVaporizados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewVaporizados });
            // 
            // gridViewVaporizados
            // 
            gridViewVaporizados.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewVaporizados.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewVaporizados.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewVaporizados.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewVaporizados.GridControl = gridControlVaporizados;
            gridViewVaporizados.Name = "gridViewVaporizados";
            gridViewVaporizados.OptionsBehavior.Editable = false;
            gridViewVaporizados.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewVaporizados.OptionsView.ColumnAutoWidth = false;
            gridViewVaporizados.OptionsView.EnableAppearanceEvenRow = true;
            gridViewVaporizados.OptionsView.EnableAppearanceOddRow = true;
            // 
            // bAgregarExterno
            // 
            bAgregarExterno.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            bAgregarExterno.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bAgregarExterno.Appearance.Options.UseBackColor = true;
            bAgregarExterno.Appearance.Options.UseFont = true;
            bAgregarExterno.Location = new Point(949, 520);
            bAgregarExterno.Name = "bAgregarExterno";
            bAgregarExterno.Size = new Size(132, 37);
            bAgregarExterno.TabIndex = 31;
            bAgregarExterno.Text = "Agregar Externo";
            // 
            // simpleButton2
            // 
            simpleButton2.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            simpleButton2.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            simpleButton2.Appearance.Options.UseBackColor = true;
            simpleButton2.Appearance.Options.UseFont = true;
            simpleButton2.Location = new Point(13, 520);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(132, 37);
            simpleButton2.TabIndex = 32;
            simpleButton2.Text = "Eliminar";
            simpleButton2.Click += btnEliminar_Click;
            // 
            // simpleButton3
            // 
            simpleButton3.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            simpleButton3.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            simpleButton3.Appearance.Options.UseBackColor = true;
            simpleButton3.Appearance.Options.UseFont = true;
            simpleButton3.Location = new Point(155, 520);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new Size(132, 37);
            simpleButton3.TabIndex = 33;
            simpleButton3.Text = "Editar";
            simpleButton3.Click += btnEditarVaporizado_Click;
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(labelNovedades);
            guna2Panel10.CustomizableEdges = customizableEdges3;
            guna2Panel10.FillColor = Color.LightCoral;
            guna2Panel10.Location = new Point(0, 15);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel10.Size = new Size(253, 31);
            guna2Panel10.TabIndex = 47;
            // 
            // labelNovedades
            // 
            labelNovedades.BackColor = Color.Transparent;
            labelNovedades.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNovedades.ForeColor = SystemColors.ControlLight;
            labelNovedades.Location = new Point(13, 4);
            labelNovedades.Name = "labelNovedades";
            labelNovedades.Size = new Size(100, 21);
            labelNovedades.TabIndex = 0;
            labelNovedades.Text = "Vaporizados";
            // 
            // MenuVaporizados
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1093, 573);
            Controls.Add(guna2Panel10);
            Controls.Add(simpleButton3);
            Controls.Add(simpleButton2);
            Controls.Add(bAgregarExterno);
            Controls.Add(gridControlVaporizados);
            MaximizeBox = false;
            Name = "MenuVaporizados";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuVaporizados";
            ((System.ComponentModel.ISupportInitialize)gridControlVaporizados).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewVaporizados).EndInit();
            guna2Panel10.ResumeLayout(false);
            guna2Panel10.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlVaporizados;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVaporizados;
        private DevExpress.XtraEditors.SimpleButton bAgregarExterno;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelNovedades;
    }
}