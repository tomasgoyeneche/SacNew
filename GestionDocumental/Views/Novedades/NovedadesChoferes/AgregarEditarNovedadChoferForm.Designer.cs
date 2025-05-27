namespace GestionDocumental.Views
{
    partial class AgregarEditarNovedadChoferForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel23 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbChofer = new DevExpress.XtraEditors.LookUpEdit();
            guna2HtmlLabel8 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            txtObservaciones = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbEstado = new DevExpress.XtraEditors.LookUpEdit();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dispoCheck = new Guna.UI2.WinForms.Guna2CheckBox();
            dtpFechaInicio = new Guna.UI2.WinForms.Guna2DateTimePicker();
            dtpFechaFinal = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblDiasAusente = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblReincorporacion = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblMantenimientosUnidad = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbChofer.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado.Properties).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel23);
            guna2Panel10.CustomizableEdges = customizableEdges13;
            guna2Panel10.FillColor = Color.Coral;
            guna2Panel10.Location = new Point(0, 24);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges14;
            guna2Panel10.Size = new Size(253, 31);
            guna2Panel10.TabIndex = 47;
            // 
            // guna2HtmlLabel23
            // 
            guna2HtmlLabel23.BackColor = Color.Transparent;
            guna2HtmlLabel23.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel23.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel23.Location = new Point(13, 4);
            guna2HtmlLabel23.Name = "guna2HtmlLabel23";
            guna2HtmlLabel23.Size = new Size(196, 21);
            guna2HtmlLabel23.TabIndex = 0;
            guna2HtmlLabel23.Text = "Agregar/Editar Novedad";
            // 
            // cmbChofer
            // 
            cmbChofer.Location = new Point(33, 111);
            cmbChofer.Margin = new Padding(4, 3, 4, 3);
            cmbChofer.Name = "cmbChofer";
            cmbChofer.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbChofer.Properties.Appearance.Options.UseFont = true;
            cmbChofer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbChofer.Size = new Size(354, 26);
            cmbChofer.TabIndex = 49;
            cmbChofer.EditValueChanged += cmbChofer_EditValueChanged;
            // 
            // guna2HtmlLabel8
            // 
            guna2HtmlLabel8.AutoSize = false;
            guna2HtmlLabel8.BackColor = Color.Gray;
            guna2HtmlLabel8.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel8.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel8.Location = new Point(33, 88);
            guna2HtmlLabel8.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel8.Name = "guna2HtmlLabel8";
            guna2HtmlLabel8.Size = new Size(354, 23);
            guna2HtmlLabel8.TabIndex = 48;
            guna2HtmlLabel8.Text = "Chofer:";
            guna2HtmlLabel8.TextAlignment = ContentAlignment.TopCenter;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.Gray;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(33, 154);
            guna2HtmlLabel1.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(167, 23);
            guna2HtmlLabel1.TabIndex = 51;
            guna2HtmlLabel1.Text = "Desde:";
            guna2HtmlLabel1.TextAlignment = ContentAlignment.TopCenter;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.Gray;
            guna2HtmlLabel2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(220, 154);
            guna2HtmlLabel2.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(167, 23);
            guna2HtmlLabel2.TabIndex = 53;
            guna2HtmlLabel2.Text = "Hasta:";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.TopCenter;
            // 
            // btnGuardar
            // 
            btnGuardar.AnimatedGIF = true;
            btnGuardar.CustomizableEdges = customizableEdges15;
            btnGuardar.DisabledState.BorderColor = Color.DarkGray;
            btnGuardar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGuardar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGuardar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGuardar.FillColor = Color.ForestGreen;
            btnGuardar.Font = new Font("Segoe UI", 9F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(220, 507);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnGuardar.Size = new Size(167, 35);
            btnGuardar.TabIndex = 56;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.CustomizableEdges = customizableEdges17;
            btnCancelar.DisabledState.BorderColor = Color.DarkGray;
            btnCancelar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancelar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancelar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancelar.FillColor = Color.Gray;
            btnCancelar.Font = new Font("Segoe UI", 9F);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(33, 507);
            btnCancelar.Margin = new Padding(4, 3, 4, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnCancelar.Size = new Size(167, 35);
            btnCancelar.TabIndex = 55;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtObservaciones
            // 
            txtObservaciones.BackColor = Color.FromArgb(26, 29, 35);
            txtObservaciones.CustomizableEdges = customizableEdges19;
            txtObservaciones.DefaultText = "";
            txtObservaciones.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtObservaciones.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtObservaciones.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtObservaciones.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtObservaciones.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtObservaciones.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtObservaciones.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtObservaciones.Location = new Point(33, 387);
            txtObservaciones.Margin = new Padding(4, 3, 4, 3);
            txtObservaciones.MaxLength = 255;
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.PasswordChar = '\0';
            txtObservaciones.PlaceholderText = "";
            txtObservaciones.RightToLeft = RightToLeft.No;
            txtObservaciones.SelectedText = "";
            txtObservaciones.ShadowDecoration.CustomizableEdges = customizableEdges20;
            txtObservaciones.Size = new Size(354, 98);
            txtObservaciones.TabIndex = 54;
            // 
            // guna2HtmlLabel7
            // 
            guna2HtmlLabel7.BackColor = Color.Transparent;
            guna2HtmlLabel7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel7.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel7.Location = new Point(33, 358);
            guna2HtmlLabel7.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            guna2HtmlLabel7.Size = new Size(123, 23);
            guna2HtmlLabel7.TabIndex = 57;
            guna2HtmlLabel7.Text = "Observaciones:";
            // 
            // cmbEstado
            // 
            cmbEstado.Location = new Point(33, 315);
            cmbEstado.Margin = new Padding(4, 3, 4, 3);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbEstado.Properties.Appearance.Options.UseFont = true;
            cmbEstado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbEstado.Size = new Size(354, 26);
            cmbEstado.TabIndex = 59;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.Gray;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel3.Location = new Point(33, 292);
            guna2HtmlLabel3.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(354, 23);
            guna2HtmlLabel3.TabIndex = 58;
            guna2HtmlLabel3.Text = "Motivo";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.TopCenter;
            // 
            // dispoCheck
            // 
            dispoCheck.AutoSize = true;
            dispoCheck.CheckedState.BorderColor = Color.Transparent;
            dispoCheck.CheckedState.BorderRadius = 0;
            dispoCheck.CheckedState.BorderThickness = 0;
            dispoCheck.CheckedState.FillColor = Color.Transparent;
            dispoCheck.CheckMarkColor = Color.LimeGreen;
            dispoCheck.Font = new Font("Century Gothic", 10F);
            dispoCheck.ForeColor = Color.LimeGreen;
            dispoCheck.Location = new Point(288, 221);
            dispoCheck.Name = "dispoCheck";
            dispoCheck.Size = new Size(99, 23);
            dispoCheck.TabIndex = 83;
            dispoCheck.Text = "Disponible";
            dispoCheck.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            dispoCheck.UncheckedState.BorderRadius = 0;
            dispoCheck.UncheckedState.BorderThickness = 0;
            dispoCheck.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Checked = true;
            dtpFechaInicio.CustomizableEdges = customizableEdges21;
            dtpFechaInicio.FillColor = Color.Gainsboro;
            dtpFechaInicio.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(33, 177);
            dtpFechaInicio.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFechaInicio.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.ShadowDecoration.CustomizableEdges = customizableEdges22;
            dtpFechaInicio.Size = new Size(167, 21);
            dtpFechaInicio.TabIndex = 85;
            dtpFechaInicio.Value = new DateTime(2025, 5, 6, 9, 41, 5, 367);
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            // 
            // dtpFechaFinal
            // 
            dtpFechaFinal.Checked = true;
            dtpFechaFinal.CustomizableEdges = customizableEdges23;
            dtpFechaFinal.FillColor = Color.Gainsboro;
            dtpFechaFinal.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaFinal.Format = DateTimePickerFormat.Short;
            dtpFechaFinal.Location = new Point(220, 177);
            dtpFechaFinal.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFechaFinal.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFechaFinal.Name = "dtpFechaFinal";
            dtpFechaFinal.ShadowDecoration.CustomizableEdges = customizableEdges24;
            dtpFechaFinal.Size = new Size(167, 21);
            dtpFechaFinal.TabIndex = 86;
            dtpFechaFinal.Value = new DateTime(2025, 5, 6, 9, 41, 5, 367);
            dtpFechaFinal.ValueChanged += dtpFechaFinal_ValueChanged;
            // 
            // lblDiasAusente
            // 
            lblDiasAusente.BackColor = Color.Transparent;
            lblDiasAusente.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDiasAusente.ForeColor = SystemColors.ControlLight;
            lblDiasAusente.Location = new Point(33, 221);
            lblDiasAusente.Margin = new Padding(4, 3, 4, 3);
            lblDiasAusente.Name = "lblDiasAusente";
            lblDiasAusente.Size = new Size(97, 18);
            lblDiasAusente.TabIndex = 87;
            lblDiasAusente.Text = "Dias Ausentes:";
            // 
            // lblReincorporacion
            // 
            lblReincorporacion.BackColor = Color.Transparent;
            lblReincorporacion.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblReincorporacion.ForeColor = SystemColors.ControlLight;
            lblReincorporacion.Location = new Point(33, 254);
            lblReincorporacion.Margin = new Padding(4, 3, 4, 3);
            lblReincorporacion.Name = "lblReincorporacion";
            lblReincorporacion.Size = new Size(87, 18);
            lblReincorporacion.TabIndex = 88;
            lblReincorporacion.Text = "Reincorpora:";
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.AutoSize = false;
            guna2HtmlLabel4.BackColor = Color.Gray;
            guna2HtmlLabel4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel4.Location = new Point(410, 87);
            guna2HtmlLabel4.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(354, 24);
            guna2HtmlLabel4.TabIndex = 89;
            guna2HtmlLabel4.Text = "Mantenimientos Unidad";
            guna2HtmlLabel4.TextAlignment = ContentAlignment.TopCenter;
            // 
            // lblMantenimientosUnidad
            // 
            lblMantenimientosUnidad.AutoSize = false;
            lblMantenimientosUnidad.BackColor = Color.FromArgb(42, 48, 56);
            lblMantenimientosUnidad.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMantenimientosUnidad.ForeColor = Color.Red;
            lblMantenimientosUnidad.Location = new Point(410, 110);
            lblMantenimientosUnidad.Margin = new Padding(4, 3, 4, 3);
            lblMantenimientosUnidad.Name = "lblMantenimientosUnidad";
            lblMantenimientosUnidad.Size = new Size(354, 432);
            lblMantenimientosUnidad.TabIndex = 90;
            lblMantenimientosUnidad.Text = "Mantenimiento";
            // 
            // AgregarEditarNovedadChoferForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(798, 568);
            Controls.Add(lblMantenimientosUnidad);
            Controls.Add(guna2HtmlLabel4);
            Controls.Add(lblReincorporacion);
            Controls.Add(lblDiasAusente);
            Controls.Add(dtpFechaFinal);
            Controls.Add(dtpFechaInicio);
            Controls.Add(dispoCheck);
            Controls.Add(cmbEstado);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Controls.Add(txtObservaciones);
            Controls.Add(guna2HtmlLabel7);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(cmbChofer);
            Controls.Add(guna2HtmlLabel8);
            Controls.Add(guna2Panel10);
            MaximizeBox = false;
            Name = "AgregarEditarNovedadChoferForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarEditarNovedadChoferForm";
            guna2Panel10.ResumeLayout(false);
            guna2Panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbChofer.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel23;
        private DevExpress.XtraEditors.LookUpEdit cmbChofer;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel8;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaFin;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaInicio;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
        private Guna.UI2.WinForms.Guna2TextBox txtObservaciones;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private DevExpress.XtraEditors.LookUpEdit cmbEstado;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2CheckBox dispoCheck;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaFinal;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiasAusente;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblReincorporacion;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMantenimientosUnidad;
    }
}