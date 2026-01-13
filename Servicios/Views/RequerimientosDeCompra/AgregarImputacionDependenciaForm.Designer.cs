namespace Servicios.Views.RequerimientosDeCompra
{
    partial class AgregarImputacionDependenciaForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lstDependencias = new ListBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lstImputaciones = new ListBox();
            bCancelar = new Guna.UI2.WinForms.Guna2Button();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bConfirmar = new Guna.UI2.WinForms.Guna2Button();
            numPorcentaje = new Guna.UI2.WinForms.Guna2NumericUpDown();
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel17 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)numPorcentaje).BeginInit();
            guna2Panel3.SuspendLayout();
            SuspendLayout();
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.ForestGreen;
            guna2HtmlLabel3.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel3.Location = new Point(41, 101);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(157, 28);
            guna2HtmlLabel3.TabIndex = 91;
            guna2HtmlLabel3.Text = "Dependencia";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lstDependencias
            // 
            lstDependencias.FormattingEnabled = true;
            lstDependencias.ItemHeight = 13;
            lstDependencias.Location = new Point(41, 129);
            lstDependencias.Name = "lstDependencias";
            lstDependencias.Size = new Size(157, 238);
            lstDependencias.TabIndex = 90;
            lstDependencias.SelectedIndexChanged += lstDependencias_SelectedIndexChanged;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.ForestGreen;
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(247, 101);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(157, 28);
            guna2HtmlLabel1.TabIndex = 93;
            guna2HtmlLabel1.Text = "Imputacion";
            guna2HtmlLabel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lstImputaciones
            // 
            lstImputaciones.FormattingEnabled = true;
            lstImputaciones.ItemHeight = 13;
            lstImputaciones.Location = new Point(247, 129);
            lstImputaciones.Name = "lstImputaciones";
            lstImputaciones.Size = new Size(157, 238);
            lstImputaciones.TabIndex = 92;
            // 
            // bCancelar
            // 
            bCancelar.CustomizableEdges = customizableEdges1;
            bCancelar.DisabledState.BorderColor = Color.DarkGray;
            bCancelar.DisabledState.CustomBorderColor = Color.DarkGray;
            bCancelar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bCancelar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bCancelar.FillColor = Color.Gray;
            bCancelar.Font = new Font("Segoe UI", 9F);
            bCancelar.ForeColor = Color.White;
            bCancelar.Location = new Point(41, 489);
            bCancelar.Name = "bCancelar";
            bCancelar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            bCancelar.Size = new Size(172, 40);
            bCancelar.TabIndex = 102;
            bCancelar.Text = "Cancelar";
            bCancelar.Click += bCancelar_Click;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.ForestGreen;
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(41, 397);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(363, 28);
            guna2HtmlLabel2.TabIndex = 101;
            guna2HtmlLabel2.Text = "Porcentaje %";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // bConfirmar
            // 
            bConfirmar.CustomizableEdges = customizableEdges3;
            bConfirmar.DisabledState.BorderColor = Color.DarkGray;
            bConfirmar.DisabledState.CustomBorderColor = Color.DarkGray;
            bConfirmar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bConfirmar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bConfirmar.FillColor = Color.ForestGreen;
            bConfirmar.Font = new Font("Segoe UI", 9F);
            bConfirmar.ForeColor = Color.White;
            bConfirmar.Location = new Point(232, 489);
            bConfirmar.Name = "bConfirmar";
            bConfirmar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            bConfirmar.Size = new Size(172, 40);
            bConfirmar.TabIndex = 100;
            bConfirmar.Text = "Confirmar";
            bConfirmar.Click += bConfirmar_Click;
            // 
            // numPorcentaje
            // 
            numPorcentaje.BackColor = Color.Transparent;
            numPorcentaje.CustomizableEdges = customizableEdges5;
            numPorcentaje.Font = new Font("Segoe UI", 9F);
            numPorcentaje.Location = new Point(41, 424);
            numPorcentaje.Name = "numPorcentaje";
            numPorcentaje.ShadowDecoration.CustomizableEdges = customizableEdges6;
            numPorcentaje.Size = new Size(363, 36);
            numPorcentaje.TabIndex = 99;
            numPorcentaje.UpDownButtonFillColor = Color.ForestGreen;
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel17);
            guna2Panel3.CustomizableEdges = customizableEdges7;
            guna2Panel3.FillColor = Color.ForestGreen;
            guna2Panel3.Location = new Point(1, 27);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 103;
            // 
            // guna2HtmlLabel17
            // 
            guna2HtmlLabel17.BackColor = Color.Transparent;
            guna2HtmlLabel17.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel17.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel17.Location = new Point(17, 10);
            guna2HtmlLabel17.Name = "guna2HtmlLabel17";
            guna2HtmlLabel17.Size = new Size(211, 21);
            guna2HtmlLabel17.TabIndex = 0;
            guna2HtmlLabel17.Text = "Imputacion Dependencias";
            // 
            // AgregarImputacionDependenciaForm
            // 
            Appearance.BackColor = Color.FromArgb(41, 44, 53);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 568);
            Controls.Add(guna2Panel3);
            Controls.Add(bCancelar);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(bConfirmar);
            Controls.Add(numPorcentaje);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(lstImputaciones);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(lstDependencias);
            MaximizeBox = false;
            Name = "AgregarImputacionDependenciaForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarImputacionDependenciaForm";
            ((System.ComponentModel.ISupportInitialize)numPorcentaje).EndInit();
            guna2Panel3.ResumeLayout(false);
            guna2Panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private ListBox lstDependencias;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private ListBox lstImputaciones;
        private Guna.UI2.WinForms.Guna2Button bCancelar;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Button bConfirmar;
        private Guna.UI2.WinForms.Guna2NumericUpDown numPorcentaje;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel17;
    }
}