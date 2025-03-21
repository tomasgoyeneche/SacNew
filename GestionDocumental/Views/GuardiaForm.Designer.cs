namespace GestionDocumental.Views
{
    partial class GuardiaForm
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
            guna2Panel9.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 0;
            guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel9
            // 
            guna2Panel9.Controls.Add(bMinimize);
            guna2Panel9.Controls.Add(bClose);
            guna2Panel9.CustomizableEdges = customizableEdges5;
            guna2Panel9.Dock = DockStyle.Top;
            guna2Panel9.FillColor = Color.Transparent;
            guna2Panel9.Location = new Point(0, 0);
            guna2Panel9.Name = "guna2Panel9";
            guna2Panel9.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel9.Size = new Size(1280, 31);
            guna2Panel9.TabIndex = 69;
            // 
            // bMinimize
            // 
            bMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            bMinimize.CustomizableEdges = customizableEdges1;
            bMinimize.FillColor = Color.Transparent;
            bMinimize.IconColor = Color.WhiteSmoke;
            bMinimize.Location = new Point(1216, 0);
            bMinimize.Name = "bMinimize";
            bMinimize.ShadowDecoration.CustomizableEdges = customizableEdges2;
            bMinimize.Size = new Size(32, 31);
            bMinimize.TabIndex = 18;
            // 
            // bClose
            // 
            bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bClose.Animated = true;
            bClose.CustomizableEdges = customizableEdges3;
            bClose.FillColor = Color.Transparent;
            bClose.HoverState.FillColor = Color.IndianRed;
            bClose.HoverState.IconColor = Color.White;
            bClose.IconColor = Color.WhiteSmoke;
            bClose.Location = new Point(1248, 0);
            bClose.Name = "bClose";
            bClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
            bClose.Size = new Size(32, 31);
            bClose.TabIndex = 17;
            // 
            // GuardiaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 29, 35);
            ClientSize = new Size(1280, 720);
            Controls.Add(guna2Panel9);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GuardiaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GuardiaForm";
            guna2Panel9.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel9;
        private Guna.UI2.WinForms.Guna2ControlBox bMinimize;
        private Guna.UI2.WinForms.Guna2ControlBox bClose;
    }
}