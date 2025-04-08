namespace GestionFlota.Views.Postas
{
    partial class PruebasDev
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
            components = new System.ComponentModel.Container();
            officeNavigationBar1 = new DevExpress.XtraBars.Navigation.OfficeNavigationBar();
            navigationBarItem1 = new DevExpress.XtraBars.Navigation.NavigationBarItem();
            navigationBarItem2 = new DevExpress.XtraBars.Navigation.NavigationBarItem();
            comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            fontEdit1 = new DevExpress.XtraEditors.FontEdit();
            calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            ((System.ComponentModel.ISupportInitialize)officeNavigationBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fontEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)calcEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProvider1).BeginInit();
            SuspendLayout();
            // 
            // officeNavigationBar1
            // 
            officeNavigationBar1.Dock = DockStyle.Bottom;
            officeNavigationBar1.Items.AddRange(new DevExpress.XtraBars.Navigation.NavigationBarItem[] { navigationBarItem1, navigationBarItem2 });
            officeNavigationBar1.Location = new Point(0, 522);
            officeNavigationBar1.Name = "officeNavigationBar1";
            officeNavigationBar1.Size = new Size(798, 46);
            officeNavigationBar1.TabIndex = 0;
            officeNavigationBar1.Text = "officeNavigationBar1";
            officeNavigationBar1.Click += officeNavigationBar1_Click;
            // 
            // navigationBarItem1
            // 
            navigationBarItem1.Name = "navigationBarItem1";
            navigationBarItem1.Text = "Item1";
            // 
            // navigationBarItem2
            // 
            navigationBarItem2.Name = "navigationBarItem2";
            navigationBarItem2.Text = "Item2";
            // 
            // comboBoxEdit1
            // 
            comboBoxEdit1.Location = new Point(250, 151);
            comboBoxEdit1.Name = "comboBoxEdit1";
            comboBoxEdit1.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxEdit1.Properties.Appearance.Options.UseFont = true;
            comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEdit1.Size = new Size(256, 26);
            comboBoxEdit1.TabIndex = 1;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(150, 301);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(63, 13);
            labelControl1.TabIndex = 2;
            labelControl1.Text = "labelControl1";
            // 
            // fontEdit1
            // 
            fontEdit1.Location = new Point(195, 359);
            fontEdit1.Name = "fontEdit1";
            fontEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            fontEdit1.Size = new Size(100, 20);
            fontEdit1.TabIndex = 3;
            // 
            // calcEdit1
            // 
            calcEdit1.Location = new Point(240, 443);
            calcEdit1.Name = "calcEdit1";
            calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            calcEdit1.Size = new Size(100, 20);
            calcEdit1.TabIndex = 4;
            // 
            // PruebasDev
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(calcEdit1);
            Controls.Add(fontEdit1);
            Controls.Add(labelControl1);
            Controls.Add(comboBoxEdit1);
            Controls.Add(officeNavigationBar1);
            MaximizeBox = false;
            Name = "PruebasDev";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)officeNavigationBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboBoxEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)fontEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)calcEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Navigation.OfficeNavigationBar officeNavigationBar1;
        private DevExpress.XtraBars.Navigation.NavigationBarItem navigationBarItem1;
        private DevExpress.XtraBars.Navigation.NavigationBarItem navigationBarItem2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.FontEdit fontEdit1;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}