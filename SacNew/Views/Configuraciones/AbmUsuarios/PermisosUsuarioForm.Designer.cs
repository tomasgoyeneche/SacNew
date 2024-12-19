namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    partial class PermisosUsuarioForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Panel9 = new Guna.UI2.WinForms.Guna2Panel();
            bMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            bClose = new Guna.UI2.WinForms.Guna2ControlBox();
            lstPermisosDisponibles = new ListBox();
            lstPermisosAsignados = new ListBox();
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnEliminar = new Guna.UI2.WinForms.Guna2Button();
            btnAgregar = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel9.SuspendLayout();
            guna2Panel3.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel9
            // 
            guna2Panel9.BackColor = Color.Transparent;
            guna2Panel9.Controls.Add(bMinimize);
            guna2Panel9.Controls.Add(bClose);
            guna2Panel9.CustomizableEdges = customizableEdges11;
            guna2Panel9.Dock = DockStyle.Top;
            guna2Panel9.FillColor = Color.Transparent;
            guna2Panel9.Location = new Point(0, 0);
            guna2Panel9.Name = "guna2Panel9";
            guna2Panel9.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel9.Size = new Size(800, 31);
            guna2Panel9.TabIndex = 56;
            // 
            // bMinimize
            // 
            bMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            bMinimize.CustomizableEdges = customizableEdges7;
            bMinimize.FillColor = Color.Transparent;
            bMinimize.IconColor = Color.WhiteSmoke;
            bMinimize.Location = new Point(736, 0);
            bMinimize.Name = "bMinimize";
            bMinimize.ShadowDecoration.CustomizableEdges = customizableEdges8;
            bMinimize.Size = new Size(32, 31);
            bMinimize.TabIndex = 18;
            // 
            // bClose
            // 
            bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bClose.Animated = true;
            bClose.CustomizableEdges = customizableEdges9;
            bClose.FillColor = Color.Transparent;
            bClose.HoverState.FillColor = Color.IndianRed;
            bClose.HoverState.IconColor = Color.White;
            bClose.IconColor = Color.WhiteSmoke;
            bClose.Location = new Point(768, 0);
            bClose.Name = "bClose";
            bClose.ShadowDecoration.CustomizableEdges = customizableEdges10;
            bClose.Size = new Size(32, 31);
            bClose.TabIndex = 17;
            // 
            // lstPermisosDisponibles
            // 
            lstPermisosDisponibles.FormattingEnabled = true;
            lstPermisosDisponibles.ItemHeight = 15;
            lstPermisosDisponibles.Location = new Point(26, 174);
            lstPermisosDisponibles.Name = "lstPermisosDisponibles";
            lstPermisosDisponibles.Size = new Size(280, 364);
            lstPermisosDisponibles.TabIndex = 57;
            // 
            // lstPermisosAsignados
            // 
            lstPermisosAsignados.FormattingEnabled = true;
            lstPermisosAsignados.ItemHeight = 15;
            lstPermisosAsignados.Location = new Point(488, 174);
            lstPermisosAsignados.Name = "lstPermisosAsignados";
            lstPermisosAsignados.Size = new Size(280, 364);
            lstPermisosAsignados.TabIndex = 58;
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel1);
            guna2Panel3.CustomizableEdges = customizableEdges5;
            guna2Panel3.FillColor = Color.Sienna;
            guna2Panel3.Location = new Point(0, 37);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 59;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(17, 8);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(164, 25);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Permisos Usuarios";
            // 
            // btnEliminar
            // 
            btnEliminar.CustomizableEdges = customizableEdges1;
            btnEliminar.DisabledState.BorderColor = Color.DarkGray;
            btnEliminar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEliminar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEliminar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEliminar.FillColor = Color.Brown;
            btnEliminar.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Image = Resources.flechaIzquierda;
            btnEliminar.ImageAlign = HorizontalAlignment.Left;
            btnEliminar.Location = new Point(332, 389);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEliminar.Size = new Size(130, 40);
            btnEliminar.TabIndex = 61;
            btnEliminar.Text = "Eliminar";
            // 
            // btnAgregar
            // 
            btnAgregar.CustomizableEdges = customizableEdges3;
            btnAgregar.DisabledState.BorderColor = Color.DarkGray;
            btnAgregar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAgregar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAgregar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAgregar.FillColor = Color.ForestGreen;
            btnAgregar.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Image = Resources.flechaDerecha;
            btnAgregar.ImageAlign = HorizontalAlignment.Right;
            btnAgregar.Location = new Point(332, 305);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAgregar.Size = new Size(130, 40);
            btnAgregar.TabIndex = 60;
            btnAgregar.Text = "Agregar";
            // 
            // PermisosUsuarioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 44, 53);
            ClientSize = new Size(800, 600);
            Controls.Add(btnEliminar);
            Controls.Add(btnAgregar);
            Controls.Add(guna2Panel3);
            Controls.Add(lstPermisosAsignados);
            Controls.Add(lstPermisosDisponibles);
            Controls.Add(guna2Panel9);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PermisosUsuarioForm";
            Text = "PermisosUsuarioForm";
            guna2Panel9.ResumeLayout(false);
            guna2Panel3.ResumeLayout(false);
            guna2Panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel9;
        private Guna.UI2.WinForms.Guna2ControlBox bMinimize;
        private Guna.UI2.WinForms.Guna2ControlBox bClose;
        private ListBox lstPermisosDisponibles;
        private ListBox lstPermisosAsignados;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnEliminar;
        private Guna.UI2.WinForms.Guna2Button btnAgregar;
    }
}