namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    partial class AgregarEditarUsuarioForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Panel9 = new Guna.UI2.WinForms.Guna2Panel();
            bMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            bClose = new Guna.UI2.WinForms.Guna2ControlBox();
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel9.SuspendLayout();
            guna2Panel3.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 0;
            guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel9
            // 
            guna2Panel9.BackColor = Color.Transparent;
            guna2Panel9.Controls.Add(bMinimize);
            guna2Panel9.Controls.Add(bClose);
            guna2Panel9.CustomizableEdges = customizableEdges13;
            guna2Panel9.Dock = DockStyle.Top;
            guna2Panel9.FillColor = Color.Transparent;
            guna2Panel9.Location = new Point(0, 0);
            guna2Panel9.Name = "guna2Panel9";
            guna2Panel9.ShadowDecoration.CustomizableEdges = customizableEdges14;
            guna2Panel9.Size = new Size(800, 31);
            guna2Panel9.TabIndex = 56;
            // 
            // bMinimize
            // 
            bMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            bMinimize.CustomizableEdges = customizableEdges9;
            bMinimize.FillColor = Color.Transparent;
            bMinimize.IconColor = Color.WhiteSmoke;
            bMinimize.Location = new Point(737, -1);
            bMinimize.Name = "bMinimize";
            bMinimize.ShadowDecoration.CustomizableEdges = customizableEdges10;
            bMinimize.Size = new Size(32, 31);
            bMinimize.TabIndex = 18;
            // 
            // bClose
            // 
            bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bClose.Animated = true;
            bClose.CustomizableEdges = customizableEdges11;
            bClose.FillColor = Color.Transparent;
            bClose.HoverState.FillColor = Color.IndianRed;
            bClose.HoverState.IconColor = Color.White;
            bClose.IconColor = Color.WhiteSmoke;
            bClose.Location = new Point(768, -1);
            bClose.Name = "bClose";
            bClose.ShadowDecoration.CustomizableEdges = customizableEdges12;
            bClose.Size = new Size(32, 31);
            bClose.TabIndex = 17;
            // 
            // guna2Panel3
            // 
            guna2Panel3.Controls.Add(guna2HtmlLabel1);
            guna2Panel3.CustomizableEdges = customizableEdges7;
            guna2Panel3.FillColor = Color.Sienna;
            guna2Panel3.Location = new Point(0, 31);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel3.Size = new Size(253, 43);
            guna2Panel3.TabIndex = 57;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel1.Location = new Point(17, 8);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(215, 25);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Agregar/Editar Usuario";
            // 
            // AgregarEditarUsuarioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 44, 53);
            ClientSize = new Size(800, 600);
            Controls.Add(guna2Panel3);
            Controls.Add(guna2Panel9);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AgregarEditarUsuarioForm";
            Text = "AgregarEditarUsuarioForm";
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
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}