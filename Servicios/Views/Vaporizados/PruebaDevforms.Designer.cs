namespace Servicios.Views.Vaporizados
{
    partial class PruebaDevforms
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
            lciBirthdayDateEdit = new DevExpress.XtraLayout.LayoutControlItem();
            EmailTextEdit = new DevExpress.XtraEditors.TextEdit();
            PhoneTextEdit = new DevExpress.XtraEditors.TextEdit();
            ZipCodeTextEdit = new DevExpress.XtraEditors.TextEdit();
            CityTextEdit = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)lciBirthdayDateEdit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EmailTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PhoneTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZipCodeTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CityTextEdit.Properties).BeginInit();
            SuspendLayout();
            // 
            // lciBirthdayDateEdit
            // 
            lciBirthdayDateEdit.Location = new Point(100, 100);
            lciBirthdayDateEdit.Name = "lciBirthdayDateEdit";
            lciBirthdayDateEdit.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 8, 4);
            lciBirthdayDateEdit.Size = new Size(364, 62);
            lciBirthdayDateEdit.Text = "Birth Date";
            lciBirthdayDateEdit.TextLocation = DevExpress.Utils.Locations.Top;
            lciBirthdayDateEdit.TextSize = new Size(67, 17);
            // 
            // EmailTextEdit
            // 
            EmailTextEdit.Location = new Point(36, 33);
            EmailTextEdit.MaximumSize = new Size(0, 30);
            EmailTextEdit.MinimumSize = new Size(0, 30);
            EmailTextEdit.Name = "EmailTextEdit";
            EmailTextEdit.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            EmailTextEdit.Properties.MaskSettings.Set("mask", "(\\w|[\\.\\-])+@(\\w|[\\-]+\\.)*(\\w|[\\-]){2,63}\\.[a-zA-Z]{2,4}");
            EmailTextEdit.Properties.NullValuePrompt = "Enter Email";
            EmailTextEdit.Size = new Size(360, 30);
            EmailTextEdit.TabIndex = 4;
            // 
            // PhoneTextEdit
            // 
            PhoneTextEdit.Location = new Point(36, 84);
            PhoneTextEdit.MaximumSize = new Size(0, 30);
            PhoneTextEdit.MinimumSize = new Size(0, 30);
            PhoneTextEdit.Name = "PhoneTextEdit";
            PhoneTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            PhoneTextEdit.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.SimpleMaskManager));
            PhoneTextEdit.Properties.MaskSettings.Set("mask", "(999) 000-0000");
            PhoneTextEdit.Properties.NullValuePrompt = "(XXX) XXX-XXXX";
            PhoneTextEdit.Size = new Size(360, 30);
            PhoneTextEdit.TabIndex = 5;
            // 
            // ZipCodeTextEdit
            // 
            ZipCodeTextEdit.Location = new Point(36, 136);
            ZipCodeTextEdit.MaximumSize = new Size(0, 30);
            ZipCodeTextEdit.MinimumSize = new Size(0, 30);
            ZipCodeTextEdit.Name = "ZipCodeTextEdit";
            ZipCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            ZipCodeTextEdit.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.SimpleMaskManager));
            ZipCodeTextEdit.Properties.MaskSettings.Set("mask", "00000");
            ZipCodeTextEdit.Properties.NullValuePrompt = "Enter Zip Code";
            ZipCodeTextEdit.Size = new Size(174, 30);
            ZipCodeTextEdit.TabIndex = 9;
            // 
            // CityTextEdit
            // 
            CityTextEdit.Location = new Point(36, 192);
            CityTextEdit.MaximumSize = new Size(0, 30);
            CityTextEdit.MinimumSize = new Size(0, 30);
            CityTextEdit.Name = "CityTextEdit";
            CityTextEdit.Size = new Size(450, 30);
            CityTextEdit.TabIndex = 12;
            // 
            // PruebaDevforms
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 546);
            Controls.Add(CityTextEdit);
            Controls.Add(ZipCodeTextEdit);
            Controls.Add(PhoneTextEdit);
            Controls.Add(EmailTextEdit);
            Name = "PruebaDevforms";
            Text = "PruebaDevforms";
            ((System.ComponentModel.ISupportInitialize)lciBirthdayDateEdit).EndInit();
            ((System.ComponentModel.ISupportInitialize)EmailTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)PhoneTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZipCodeTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)CityTextEdit.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem lciBirthdayDateEdit;
        private DevExpress.XtraEditors.TextEdit EmailTextEdit;
        private DevExpress.XtraEditors.TextEdit PhoneTextEdit;
        private DevExpress.XtraEditors.TextEdit ZipCodeTextEdit;
        private DevExpress.XtraEditors.TextEdit CityTextEdit;
    }
}