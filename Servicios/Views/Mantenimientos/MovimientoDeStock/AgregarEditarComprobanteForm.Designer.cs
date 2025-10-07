namespace Servicios.Views.Mantenimiento
{
    partial class AgregarEditarComprobanteForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarEditarComprobanteForm));
            simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            pComprobantes = new Guna.UI2.WinForms.Guna2Panel();
            bAgregarProveedor = new Guna.UI2.WinForms.Guna2Button();
            bSubirRemitoCargaPdf = new DevExpress.XtraEditors.SimpleButton();
            cmbProveedor = new DevExpress.XtraEditors.LookUpEdit();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNroComprobante = new DevExpress.XtraEditors.TextEdit();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbTipoComprobante = new DevExpress.XtraEditors.LookUpEdit();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pComprobantes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbProveedor.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtNroComprobante.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbTipoComprobante.Properties).BeginInit();
            SuspendLayout();
            // 
            // simpleButton8
            // 
            simpleButton8.Appearance.BackColor = Color.DimGray;
            simpleButton8.Appearance.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            simpleButton8.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            simpleButton8.Appearance.Options.UseBackColor = true;
            simpleButton8.Appearance.Options.UseFont = true;
            simpleButton8.Appearance.Options.UseForeColor = true;
            simpleButton8.Appearance.Options.UseTextOptions = true;
            simpleButton8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            simpleButton8.AppearanceDisabled.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            simpleButton8.AppearanceDisabled.Options.UseForeColor = true;
            simpleButton8.Location = new Point(13, 401);
            simpleButton8.Name = "simpleButton8";
            simpleButton8.Size = new Size(164, 47);
            simpleButton8.TabIndex = 108;
            simpleButton8.Text = "Cancelar";
            simpleButton8.Click += btnCancelar_Click;
            // 
            // simpleButton7
            // 
            simpleButton7.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            simpleButton7.Appearance.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            simpleButton7.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            simpleButton7.Appearance.Options.UseBackColor = true;
            simpleButton7.Appearance.Options.UseFont = true;
            simpleButton7.Appearance.Options.UseForeColor = true;
            simpleButton7.Appearance.Options.UseTextOptions = true;
            simpleButton7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            simpleButton7.AppearanceDisabled.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            simpleButton7.AppearanceDisabled.Options.UseForeColor = true;
            simpleButton7.Location = new Point(218, 401);
            simpleButton7.Name = "simpleButton7";
            simpleButton7.Size = new Size(164, 47);
            simpleButton7.TabIndex = 107;
            simpleButton7.Text = "Guardar";
            simpleButton7.Click += btnGuardar_Click;
            // 
            // pComprobantes
            // 
            pComprobantes.BackColor = Color.Transparent;
            pComprobantes.Controls.Add(bAgregarProveedor);
            pComprobantes.Controls.Add(bSubirRemitoCargaPdf);
            pComprobantes.Controls.Add(cmbProveedor);
            pComprobantes.Controls.Add(guna2HtmlLabel2);
            pComprobantes.Controls.Add(txtNroComprobante);
            pComprobantes.Controls.Add(guna2HtmlLabel1);
            pComprobantes.Controls.Add(cmbTipoComprobante);
            pComprobantes.Controls.Add(guna2HtmlLabel4);
            pComprobantes.Controls.Add(guna2HtmlLabel3);
            pComprobantes.CustomizableEdges = customizableEdges3;
            pComprobantes.FillColor = Color.FromArgb(42, 48, 56);
            pComprobantes.Location = new Point(13, 10);
            pComprobantes.Name = "pComprobantes";
            pComprobantes.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pComprobantes.Size = new Size(369, 381);
            pComprobantes.TabIndex = 106;
            // 
            // bAgregarProveedor
            // 
            bAgregarProveedor.AnimatedGIF = true;
            bAgregarProveedor.CustomizableEdges = customizableEdges1;
            bAgregarProveedor.DisabledState.BorderColor = Color.DarkGray;
            bAgregarProveedor.DisabledState.CustomBorderColor = Color.DarkGray;
            bAgregarProveedor.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bAgregarProveedor.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bAgregarProveedor.FillColor = Color.ForestGreen;
            bAgregarProveedor.Font = new Font("Segoe UI", 9F);
            bAgregarProveedor.ForeColor = Color.White;
            bAgregarProveedor.Location = new Point(308, 281);
            bAgregarProveedor.Margin = new Padding(4, 3, 4, 3);
            bAgregarProveedor.Name = "bAgregarProveedor";
            bAgregarProveedor.ShadowDecoration.CustomizableEdges = customizableEdges2;
            bAgregarProveedor.Size = new Size(43, 49);
            bAgregarProveedor.TabIndex = 106;
            bAgregarProveedor.Text = "+";
            bAgregarProveedor.Click += bAgregarProveedor_Click;
            // 
            // bSubirRemitoCargaPdf
            // 
            bSubirRemitoCargaPdf.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            bSubirRemitoCargaPdf.Appearance.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bSubirRemitoCargaPdf.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            bSubirRemitoCargaPdf.Appearance.Options.UseBackColor = true;
            bSubirRemitoCargaPdf.Appearance.Options.UseFont = true;
            bSubirRemitoCargaPdf.Appearance.Options.UseForeColor = true;
            bSubirRemitoCargaPdf.Appearance.Options.UseTextOptions = true;
            bSubirRemitoCargaPdf.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bSubirRemitoCargaPdf.AppearanceDisabled.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            bSubirRemitoCargaPdf.AppearanceDisabled.Options.UseForeColor = true;
            bSubirRemitoCargaPdf.ImageOptions.Image = (Image)resources.GetObject("bSubirRemitoCargaPdf.ImageOptions.Image");
            bSubirRemitoCargaPdf.Location = new Point(17, 47);
            bSubirRemitoCargaPdf.Name = "bSubirRemitoCargaPdf";
            bSubirRemitoCargaPdf.Size = new Size(334, 44);
            bSubirRemitoCargaPdf.TabIndex = 105;
            bSubirRemitoCargaPdf.Text = "Subir Comprobante";
            bSubirRemitoCargaPdf.Click += btnCargarPdf_Click;
            // 
            // cmbProveedor
            // 
            cmbProveedor.Location = new Point(17, 304);
            cmbProveedor.Margin = new Padding(4, 3, 4, 3);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProveedor.Properties.Appearance.Options.UseFont = true;
            cmbProveedor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbProveedor.Size = new Size(283, 26);
            cmbProveedor.TabIndex = 103;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.Gray;
            guna2HtmlLabel2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(16, 281);
            guna2HtmlLabel2.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(284, 23);
            guna2HtmlLabel2.TabIndex = 104;
            guna2HtmlLabel2.Text = "Proveedor";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.TopCenter;
            // 
            // txtNroComprobante
            // 
            txtNroComprobante.Location = new Point(16, 227);
            txtNroComprobante.Name = "txtNroComprobante";
            txtNroComprobante.Properties.Appearance.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNroComprobante.Properties.Appearance.Options.UseFont = true;
            txtNroComprobante.Size = new Size(335, 22);
            txtNroComprobante.TabIndex = 94;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.Gray;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(16, 204);
            guna2HtmlLabel1.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(335, 23);
            guna2HtmlLabel1.TabIndex = 93;
            guna2HtmlLabel1.Text = "Factura";
            guna2HtmlLabel1.TextAlignment = ContentAlignment.TopCenter;
            // 
            // cmbTipoComprobante
            // 
            cmbTipoComprobante.Location = new Point(17, 147);
            cmbTipoComprobante.Margin = new Padding(4, 3, 4, 3);
            cmbTipoComprobante.Name = "cmbTipoComprobante";
            cmbTipoComprobante.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbTipoComprobante.Properties.Appearance.Options.UseFont = true;
            cmbTipoComprobante.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbTipoComprobante.Size = new Size(334, 26);
            cmbTipoComprobante.TabIndex = 91;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.AutoSize = false;
            guna2HtmlLabel4.BackColor = Color.Gray;
            guna2HtmlLabel4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel4.Location = new Point(16, 124);
            guna2HtmlLabel4.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(335, 23);
            guna2HtmlLabel4.TabIndex = 92;
            guna2HtmlLabel4.Text = "Tipo Factura";
            guna2HtmlLabel4.TextAlignment = ContentAlignment.TopCenter;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.SeaGreen;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(369, 20);
            guna2HtmlLabel3.TabIndex = 50;
            guna2HtmlLabel3.Text = "Agregar/Editar Comprobante";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // AgregarEditarComprobanteForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 458);
            Controls.Add(simpleButton8);
            Controls.Add(simpleButton7);
            Controls.Add(pComprobantes);
            Name = "AgregarEditarComprobanteForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarEditarComprobanteForm";
            pComprobantes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cmbProveedor.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtNroComprobante.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbTipoComprobante.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private Guna.UI2.WinForms.Guna2Panel pComprobantes;
        private DevExpress.XtraEditors.TextEdit txtNroComprobante;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private DevExpress.XtraEditors.LookUpEdit cmbTipoComprobante;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private DevExpress.XtraEditors.LookUpEdit cmbProveedor;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private DevExpress.XtraEditors.SimpleButton bSubirRemitoCargaPdf;
        private Guna.UI2.WinForms.Guna2Button bAgregarProveedor;
    }
}