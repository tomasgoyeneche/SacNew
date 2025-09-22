namespace SacNew
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessFormLogin = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            pInicioLogin = new Guna.UI2.WinForms.Guna2Panel();
            tLogUsu = new Guna.UI2.WinForms.Guna2TextBox();
            tLogPass = new Guna.UI2.WinForms.Guna2TextBox();
            lblError = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bIniciarSesion = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bClose = new Guna.UI2.WinForms.Guna2ControlBox();
            bMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessFormLogin
            // 
            guna2BorderlessFormLogin.ContainerControl = this;
            guna2BorderlessFormLogin.DockIndicatorTransparencyValue = 0.8D;
            guna2BorderlessFormLogin.DragForm = false;
            guna2BorderlessFormLogin.ResizeForm = false;
            guna2BorderlessFormLogin.TransparentWhileDrag = true;
            // 
            // pInicioLogin
            // 
            pInicioLogin.BackColor = Color.Transparent;
            pInicioLogin.BackgroundImage = App.Properties.Resources.loginVolvoCamionChico;
            pInicioLogin.BackgroundImageLayout = ImageLayout.Stretch;
            pInicioLogin.CustomizableEdges = customizableEdges7;
            pInicioLogin.Dock = DockStyle.Left;
            pInicioLogin.FillColor = Color.FromArgb(120, 0, 0, 0);
            pInicioLogin.Location = new Point(0, 0);
            pInicioLogin.Name = "pInicioLogin";
            pInicioLogin.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pInicioLogin.Size = new Size(407, 450);
            pInicioLogin.TabIndex = 0;
            // 
            // tLogUsu
            // 
            tLogUsu.Animated = true;
            tLogUsu.BorderRadius = 20;
            tLogUsu.CustomizableEdges = customizableEdges11;
            tLogUsu.DefaultText = "";
            tLogUsu.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tLogUsu.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tLogUsu.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tLogUsu.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tLogUsu.FocusedState.BorderColor = Color.Chocolate;
            tLogUsu.Font = new Font("Segoe UI", 10F);
            tLogUsu.ForeColor = Color.Black;
            tLogUsu.HoverState.BorderColor = Color.Coral;
            tLogUsu.IconLeft = App.Properties.Resources.loginUsuarioGris;
            tLogUsu.Location = new Point(47, 161);
            tLogUsu.Margin = new Padding(4, 5, 4, 5);
            tLogUsu.MaxLength = 25;
            tLogUsu.Name = "tLogUsu";
            tLogUsu.PasswordChar = '\0';
            tLogUsu.PlaceholderText = "Ingrese su usuario";
            tLogUsu.SelectedText = "";
            tLogUsu.ShadowDecoration.CustomizableEdges = customizableEdges12;
            tLogUsu.Size = new Size(220, 34);
            tLogUsu.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            tLogUsu.TabIndex = 1;
            // 
            // tLogPass
            // 
            tLogPass.Animated = true;
            tLogPass.BackColor = Color.Transparent;
            tLogPass.CustomizableEdges = customizableEdges9;
            tLogPass.DefaultText = "";
            tLogPass.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tLogPass.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tLogPass.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tLogPass.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tLogPass.FocusedState.BorderColor = Color.Chocolate;
            tLogPass.Font = new Font("Segoe UI", 10F);
            tLogPass.ForeColor = Color.Black;
            tLogPass.HoverState.BorderColor = Color.Coral;
            tLogPass.IconLeft = App.Properties.Resources.loginContrasenaGris;
            tLogPass.Location = new Point(47, 231);
            tLogPass.Margin = new Padding(4, 5, 4, 5);
            tLogPass.MaxLength = 25;
            tLogPass.Name = "tLogPass";
            tLogPass.PasswordChar = '●';
            tLogPass.PlaceholderText = "Ingrese su contraseña";
            tLogPass.SelectedText = "";
            tLogPass.ShadowDecoration.CustomizableEdges = customizableEdges10;
            tLogPass.Size = new Size(220, 34);
            tLogPass.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            tLogPass.TabIndex = 2;
            tLogPass.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            lblError.BackColor = Color.FromArgb(63, 73, 75);
            lblError.Font = new Font("Century Gothic", 8F);
            lblError.ForeColor = Color.IndianRed;
            lblError.Location = new Point(16, 355);
            lblError.Name = "lblError";
            lblError.Size = new Size(25, 18);
            lblError.TabIndex = 0;
            lblError.Text = "Error";
            lblError.Visible = false;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 8F);
            guna2HtmlLabel1.ForeColor = SystemColors.ButtonShadow;
            guna2HtmlLabel1.Location = new Point(738, 432);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(60, 15);
            guna2HtmlLabel1.TabIndex = 7;
            guna2HtmlLabel1.Text = "Version 0.8";
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.BorderRadius = 20;
            guna2Panel1.Controls.Add(guna2Separator1);
            guna2Panel1.Controls.Add(guna2HtmlLabel4);
            guna2Panel1.Controls.Add(guna2HtmlLabel3);
            guna2Panel1.Controls.Add(bIniciarSesion);
            guna2Panel1.Controls.Add(guna2HtmlLabel2);
            guna2Panel1.Controls.Add(tLogPass);
            guna2Panel1.Controls.Add(lblError);
            guna2Panel1.Controls.Add(tLogUsu);
            guna2Panel1.CustomBorderColor = Color.Transparent;
            guna2Panel1.CustomizableEdges = customizableEdges13;
            guna2Panel1.FillColor = Color.FromArgb(63, 73, 75);
            guna2Panel1.Location = new Point(447, 39);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges14;
            guna2Panel1.Size = new Size(315, 375);
            guna2Panel1.TabIndex = 9;
            // 
            // guna2Separator1
            // 
            guna2Separator1.Location = new Point(76, 87);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(163, 10);
            guna2Separator1.TabIndex = 10;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.AutoSize = false;
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Font = new Font("Century Gothic", 9F);
            guna2HtmlLabel4.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel4.Location = new Point(47, 209);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(220, 19);
            guna2HtmlLabel4.TabIndex = 9;
            guna2HtmlLabel4.Text = "Contraseña*";
            guna2HtmlLabel4.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Century Gothic", 9F);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel3.Location = new Point(47, 139);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(220, 19);
            guna2HtmlLabel3.TabIndex = 8;
            guna2HtmlLabel3.Text = "Usuario*";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // bIniciarSesion
            // 
            bIniciarSesion.BorderRadius = 5;
            bIniciarSesion.CustomizableEdges = customizableEdges1;
            bIniciarSesion.DisabledState.BorderColor = Color.DarkGray;
            bIniciarSesion.DisabledState.CustomBorderColor = Color.DarkGray;
            bIniciarSesion.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bIniciarSesion.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            bIniciarSesion.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bIniciarSesion.FillColor = Color.Coral;
            bIniciarSesion.FillColor2 = Color.Coral;
            bIniciarSesion.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            bIniciarSesion.ForeColor = Color.White;
            bIniciarSesion.ImageAlign = HorizontalAlignment.Left;
            bIniciarSesion.Location = new Point(47, 299);
            bIniciarSesion.Name = "bIniciarSesion";
            bIniciarSesion.ShadowDecoration.CustomizableEdges = customizableEdges2;
            bIniciarSesion.Size = new Size(220, 40);
            bIniciarSesion.TabIndex = 3;
            bIniciarSesion.Text = "Ingresar";
            bIniciarSesion.Click += bIniciarSesion_Click;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Century Gothic", 24F);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(47, 22);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(220, 75);
            guna2HtmlLabel2.TabIndex = 6;
            guna2HtmlLabel2.Text = "Iniciar Sesion";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // bClose
            // 
            bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bClose.Animated = true;
            bClose.CustomizableEdges = customizableEdges5;
            bClose.FillColor = Color.Transparent;
            bClose.HoverState.FillColor = Color.IndianRed;
            bClose.HoverState.IconColor = Color.White;
            bClose.IconColor = Color.WhiteSmoke;
            bClose.Location = new Point(769, 0);
            bClose.Name = "bClose";
            bClose.ShadowDecoration.CustomizableEdges = customizableEdges6;
            bClose.Size = new Size(32, 24);
            bClose.TabIndex = 18;
            // 
            // bMinimize
            // 
            bMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            bMinimize.CustomizableEdges = customizableEdges3;
            bMinimize.FillColor = Color.Transparent;
            bMinimize.IconColor = Color.WhiteSmoke;
            bMinimize.Location = new Point(739, 0);
            bMinimize.Name = "bMinimize";
            bMinimize.ShadowDecoration.CustomizableEdges = customizableEdges4;
            bMinimize.Size = new Size(32, 24);
            bMinimize.TabIndex = 19;
            // 
            // Login
            // 
            AcceptButton = bIniciarSesion;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 44, 53);
            ClientSize = new Size(800, 450);
            Controls.Add(bMinimize);
            Controls.Add(bClose);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(pInicioLogin);
            Controls.Add(guna2Panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessFormLogin;
        private Guna.UI2.WinForms.Guna2Panel pInicioLogin;
        private Guna.UI2.WinForms.Guna2TextBox tLogUsu;
        private Guna.UI2.WinForms.Guna2TextBox tLogPass;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblError;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2ControlBox bClose;
        private Guna.UI2.WinForms.Guna2ControlBox bMinimize;
        private Guna.UI2.WinForms.Guna2GradientButton bIniciarSesion;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
    }
}
