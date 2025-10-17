namespace GestionFlota.Views
{
    partial class AgregarEditarDisponibleForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarEditarDisponibleForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbDestino = new DevExpress.XtraEditors.LookUpEdit();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbOrigen = new DevExpress.XtraEditors.LookUpEdit();
            lblOrigen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtObservaciones = new Guna.UI2.WinForms.Guna2TextBox();
            cmbCupo = new DevExpress.XtraEditors.LookUpEdit();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dataGridProductos = new Guna.UI2.WinForms.Guna2DataGridView();
            bCancelar = new DevExpress.XtraEditors.SimpleButton();
            bGuardar = new DevExpress.XtraEditors.SimpleButton();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dateEditFecha = new DevExpress.XtraEditors.DateEdit();
            lblMantenimientosUnidad = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblAusenciasChofer = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bAgregarObservacion = new DevExpress.XtraEditors.SimpleButton();
            bCambiarChofer = new DevExpress.XtraEditors.SimpleButton();
            guna2Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbDestino.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbOrigen.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbCupo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEditFecha.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEditFecha.Properties.CalendarTimeProperties).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel10
            // 
            guna2Panel10.Controls.Add(guna2HtmlLabel17);
            guna2Panel10.CustomizableEdges = customizableEdges1;
            guna2Panel10.FillColor = Color.SteelBlue;
            guna2Panel10.Location = new Point(1, 16);
            guna2Panel10.Name = "guna2Panel10";
            guna2Panel10.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel10.Size = new Size(253, 38);
            guna2Panel10.TabIndex = 51;
            // 
            // guna2HtmlLabel17
            // 
            guna2HtmlLabel17.AutoSize = false;
            guna2HtmlLabel17.BackColor = Color.Transparent;
            guna2HtmlLabel17.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel17.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel17.Location = new Point(8, 8);
            guna2HtmlLabel17.Name = "guna2HtmlLabel17";
            guna2HtmlLabel17.Size = new Size(229, 22);
            guna2HtmlLabel17.TabIndex = 14;
            guna2HtmlLabel17.Text = "Agregar/Editar Disponible";
            // 
            // cmbDestino
            // 
            cmbDestino.Location = new Point(34, 343);
            cmbDestino.Margin = new Padding(4, 3, 4, 3);
            cmbDestino.Name = "cmbDestino";
            cmbDestino.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbDestino.Properties.Appearance.Options.UseFont = true;
            cmbDestino.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbDestino.Size = new Size(354, 26);
            cmbDestino.TabIndex = 4;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.SteelBlue;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(34, 320);
            guna2HtmlLabel1.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(354, 23);
            guna2HtmlLabel1.TabIndex = 62;
            guna2HtmlLabel1.Text = "Destino";
            guna2HtmlLabel1.TextAlignment = ContentAlignment.TopCenter;
            // 
            // cmbOrigen
            // 
            cmbOrigen.Location = new Point(34, 173);
            cmbOrigen.Margin = new Padding(4, 3, 4, 3);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbOrigen.Properties.Appearance.Options.UseFont = true;
            cmbOrigen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbOrigen.Size = new Size(354, 26);
            cmbOrigen.TabIndex = 2;
            cmbOrigen.EditValueChanged += cmbOrigen_EditValueChanged;
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = false;
            lblOrigen.BackColor = Color.SteelBlue;
            lblOrigen.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrigen.ForeColor = SystemColors.ControlLight;
            lblOrigen.Location = new Point(34, 150);
            lblOrigen.Margin = new Padding(4, 3, 4, 3);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(354, 23);
            lblOrigen.TabIndex = 60;
            lblOrigen.Text = "Origen";
            lblOrigen.TextAlignment = ContentAlignment.TopCenter;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.SteelBlue;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel3.Location = new Point(34, 389);
            guna2HtmlLabel3.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(354, 23);
            guna2HtmlLabel3.TabIndex = 67;
            guna2HtmlLabel3.Text = "Observacion YPF";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.TopCenter;
            // 
            // txtObservaciones
            // 
            txtObservaciones.BackColor = Color.FromArgb(26, 29, 35);
            txtObservaciones.CustomizableEdges = customizableEdges3;
            txtObservaciones.DefaultText = "";
            txtObservaciones.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtObservaciones.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtObservaciones.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtObservaciones.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtObservaciones.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtObservaciones.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtObservaciones.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtObservaciones.Location = new Point(34, 411);
            txtObservaciones.Margin = new Padding(4, 3, 4, 3);
            txtObservaciones.MaxLength = 255;
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.PasswordChar = '\0';
            txtObservaciones.PlaceholderText = "";
            txtObservaciones.RightToLeft = RightToLeft.No;
            txtObservaciones.SelectedText = "";
            txtObservaciones.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtObservaciones.Size = new Size(354, 78);
            txtObservaciones.TabIndex = 5;
            // 
            // cmbCupo
            // 
            cmbCupo.Location = new Point(34, 238);
            cmbCupo.Margin = new Padding(4, 3, 4, 3);
            cmbCupo.Name = "cmbCupo";
            cmbCupo.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbCupo.Properties.Appearance.Options.UseFont = true;
            cmbCupo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbCupo.Size = new Size(175, 26);
            cmbCupo.TabIndex = 3;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.SteelBlue;
            guna2HtmlLabel2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(34, 215);
            guna2HtmlLabel2.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(175, 23);
            guna2HtmlLabel2.TabIndex = 68;
            guna2HtmlLabel2.Text = "Cupo";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.TopCenter;
            // 
            // dataGridProductos
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Tahoma", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridProductos.ColumnHeadersHeight = 25;
            dataGridProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Tahoma", 8.25F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridProductos.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridProductos.GridColor = Color.FromArgb(231, 229, 255);
            dataGridProductos.Location = new Point(216, 215);
            dataGridProductos.Name = "dataGridProductos";
            dataGridProductos.RowHeadersVisible = false;
            dataGridProductos.Size = new Size(172, 87);
            dataGridProductos.TabIndex = 70;
            dataGridProductos.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridProductos.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridProductos.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridProductos.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridProductos.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridProductos.ThemeStyle.BackColor = Color.White;
            dataGridProductos.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridProductos.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridProductos.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridProductos.ThemeStyle.HeaderStyle.Font = new Font("Tahoma", 8.25F);
            dataGridProductos.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridProductos.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridProductos.ThemeStyle.HeaderStyle.Height = 25;
            dataGridProductos.ThemeStyle.ReadOnly = false;
            dataGridProductos.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridProductos.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridProductos.ThemeStyle.RowsStyle.Font = new Font("Tahoma", 8.25F);
            dataGridProductos.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridProductos.ThemeStyle.RowsStyle.Height = 25;
            dataGridProductos.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridProductos.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // bCancelar
            // 
            bCancelar.Appearance.BackColor = Color.Gray;
            bCancelar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bCancelar.Appearance.Options.UseBackColor = true;
            bCancelar.Appearance.Options.UseFont = true;
            bCancelar.Location = new Point(34, 508);
            bCancelar.Name = "bCancelar";
            bCancelar.Size = new Size(161, 41);
            bCancelar.TabIndex = 7;
            bCancelar.Text = "Cancelar";
            bCancelar.Click += bCancelar_Click;
            // 
            // bGuardar
            // 
            bGuardar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            bGuardar.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bGuardar.Appearance.Options.UseBackColor = true;
            bGuardar.Appearance.Options.UseFont = true;
            bGuardar.Location = new Point(227, 508);
            bGuardar.Name = "bGuardar";
            bGuardar.Size = new Size(161, 41);
            bGuardar.TabIndex = 6;
            bGuardar.Text = "Guardar";
            bGuardar.Click += bGuardar_Click;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.AutoSize = false;
            guna2HtmlLabel4.BackColor = Color.SteelBlue;
            guna2HtmlLabel4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel4.Location = new Point(34, 73);
            guna2HtmlLabel4.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(354, 23);
            guna2HtmlLabel4.TabIndex = 73;
            guna2HtmlLabel4.Text = "Fecha";
            guna2HtmlLabel4.TextAlignment = ContentAlignment.TopCenter;
            // 
            // dateEditFecha
            // 
            dateEditFecha.EditValue = null;
            dateEditFecha.Enabled = false;
            dateEditFecha.Location = new Point(34, 96);
            dateEditFecha.Name = "dateEditFecha";
            dateEditFecha.Properties.Appearance.BackColor = Color.WhiteSmoke;
            dateEditFecha.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateEditFecha.Properties.Appearance.Options.UseBackColor = true;
            dateEditFecha.Properties.Appearance.Options.UseFont = true;
            editorButtonImageOptions1.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("editorButtonImageOptions1.SvgImage");
            dateEditFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            dateEditFecha.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            dateEditFecha.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditFecha.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            dateEditFecha.Properties.MaskSettings.Set("mask", "d");
            dateEditFecha.Properties.MinDate = new DateOnly(2025, 6, 10);
            dateEditFecha.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            dateEditFecha.Size = new Size(354, 40);
            dateEditFecha.TabIndex = 1;
            // 
            // lblMantenimientosUnidad
            // 
            lblMantenimientosUnidad.AutoSize = false;
            lblMantenimientosUnidad.BackColor = Color.FromArgb(42, 48, 56);
            lblMantenimientosUnidad.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMantenimientosUnidad.ForeColor = Color.WhiteSmoke;
            lblMantenimientosUnidad.Location = new Point(427, 98);
            lblMantenimientosUnidad.Margin = new Padding(4, 3, 4, 3);
            lblMantenimientosUnidad.Name = "lblMantenimientosUnidad";
            lblMantenimientosUnidad.Size = new Size(364, 140);
            lblMantenimientosUnidad.TabIndex = 92;
            lblMantenimientosUnidad.Text = "Mantenimiento";
            // 
            // guna2HtmlLabel5
            // 
            guna2HtmlLabel5.AutoSize = false;
            guna2HtmlLabel5.BackColor = Color.Gray;
            guna2HtmlLabel5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel5.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel5.Location = new Point(427, 73);
            guna2HtmlLabel5.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            guna2HtmlLabel5.Size = new Size(364, 24);
            guna2HtmlLabel5.TabIndex = 91;
            guna2HtmlLabel5.Text = "Mantenimientos Unidad";
            guna2HtmlLabel5.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblAusenciasChofer
            // 
            lblAusenciasChofer.AutoSize = false;
            lblAusenciasChofer.BackColor = Color.FromArgb(42, 48, 56);
            lblAusenciasChofer.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAusenciasChofer.ForeColor = Color.WhiteSmoke;
            lblAusenciasChofer.Location = new Point(427, 288);
            lblAusenciasChofer.Margin = new Padding(4, 3, 4, 3);
            lblAusenciasChofer.Name = "lblAusenciasChofer";
            lblAusenciasChofer.Size = new Size(364, 140);
            lblAusenciasChofer.TabIndex = 110;
            lblAusenciasChofer.Text = "Ausencia";
            // 
            // guna2HtmlLabel6
            // 
            guna2HtmlLabel6.AutoSize = false;
            guna2HtmlLabel6.BackColor = Color.Gray;
            guna2HtmlLabel6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel6.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel6.Location = new Point(427, 258);
            guna2HtmlLabel6.Margin = new Padding(4, 3, 4, 3);
            guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            guna2HtmlLabel6.Size = new Size(364, 28);
            guna2HtmlLabel6.TabIndex = 109;
            guna2HtmlLabel6.Text = "Ausencias Chofer";
            guna2HtmlLabel6.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // bAgregarObservacion
            // 
            bAgregarObservacion.Appearance.BackColor = Color.DimGray;
            bAgregarObservacion.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bAgregarObservacion.Appearance.Options.UseBackColor = true;
            bAgregarObservacion.Appearance.Options.UseFont = true;
            bAgregarObservacion.Location = new Point(427, 508);
            bAgregarObservacion.Name = "bAgregarObservacion";
            bAgregarObservacion.Size = new Size(364, 40);
            bAgregarObservacion.TabIndex = 112;
            bAgregarObservacion.Text = "Observaciones";
            bAgregarObservacion.Click += bAgregarObservacion_Click;
            // 
            // bCambiarChofer
            // 
            bCambiarChofer.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            bCambiarChofer.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bCambiarChofer.Appearance.Options.UseBackColor = true;
            bCambiarChofer.Appearance.Options.UseFont = true;
            bCambiarChofer.Location = new Point(427, 449);
            bCambiarChofer.Name = "bCambiarChofer";
            bCambiarChofer.Size = new Size(364, 40);
            bCambiarChofer.TabIndex = 111;
            bCambiarChofer.Text = "Cambiar Chofer";
            bCambiarChofer.Click += bCambiarChofer_Click;
            // 
            // AgregarEditarDisponibleForm
            // 
            AcceptButton = bGuardar;
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = bCancelar;
            ClientSize = new Size(827, 572);
            Controls.Add(bAgregarObservacion);
            Controls.Add(bCambiarChofer);
            Controls.Add(lblAusenciasChofer);
            Controls.Add(guna2HtmlLabel6);
            Controls.Add(lblMantenimientosUnidad);
            Controls.Add(guna2HtmlLabel5);
            Controls.Add(dateEditFecha);
            Controls.Add(guna2HtmlLabel4);
            Controls.Add(bCancelar);
            Controls.Add(bGuardar);
            Controls.Add(dataGridProductos);
            Controls.Add(cmbCupo);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(txtObservaciones);
            Controls.Add(cmbDestino);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(cmbOrigen);
            Controls.Add(lblOrigen);
            Controls.Add(guna2Panel10);
            MaximizeBox = false;
            Name = "AgregarEditarDisponibleForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarEditarDisponibleForm";
            guna2Panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cmbDestino.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbOrigen.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbCupo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEditFecha.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEditFecha.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
        private DevExpress.XtraEditors.LookUpEdit cmbDestino;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private DevExpress.XtraEditors.LookUpEdit cmbOrigen;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblOrigen;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2TextBox txtObservaciones;
        private DevExpress.XtraEditors.LookUpEdit cmbCupo;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridProductos;
        private DevExpress.XtraEditors.SimpleButton bCancelar;
        private DevExpress.XtraEditors.SimpleButton bGuardar;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private DevExpress.XtraEditors.DateEdit dateEditFecha;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMantenimientosUnidad;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAusenciasChofer;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private DevExpress.XtraEditors.SimpleButton bAgregarObservacion;
        private DevExpress.XtraEditors.SimpleButton bCambiarChofer;
    }
}