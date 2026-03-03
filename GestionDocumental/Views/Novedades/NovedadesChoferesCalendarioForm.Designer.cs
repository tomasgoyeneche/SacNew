namespace GestionDocumental.Views.Novedades
{
    partial class NovedadesChoferesCalendarioForm
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
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
            schedulerControlAusencias = new DevExpress.XtraScheduler.SchedulerControl();
            schedulerStorageAusencias = new DevExpress.XtraScheduler.SchedulerDataStorage(components);
            dateMes = new DevExpress.XtraEditors.DateEdit();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            btnMesAnterior = new DevExpress.XtraEditors.SimpleButton();
            btnRefrescar = new DevExpress.XtraEditors.SimpleButton();
            btnMesSiguiente = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)schedulerControlAusencias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)schedulerStorageAusencias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateMes.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateMes.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            SuspendLayout();
            // 
            // schedulerControlAusencias
            // 
            schedulerControlAusencias.DataStorage = schedulerStorageAusencias;
            schedulerControlAusencias.Location = new Point(4, 44);
            schedulerControlAusencias.Name = "schedulerControlAusencias";
            schedulerControlAusencias.OptionsView.ResourceHeaders.RotateCaption = false;
            schedulerControlAusencias.OptionsView.ToolTipVisibility = DevExpress.XtraScheduler.ToolTipVisibility.Always;
            schedulerControlAusencias.Size = new Size(1910, 1000);
            schedulerControlAusencias.Start = new DateTime(2026, 2, 12, 0, 0, 0, 0);
            schedulerControlAusencias.TabIndex = 5;
            schedulerControlAusencias.Text = "Control Ausencias";
            schedulerControlAusencias.Views.DayView.TimeRulers.Add(timeRuler1);
            schedulerControlAusencias.Views.FullWeekView.Enabled = true;
            schedulerControlAusencias.Views.FullWeekView.TimeRulers.Add(timeRuler2);
            schedulerControlAusencias.Views.TimelineView.Appearance.AdditionalHeaderCaption.Options.UseTextOptions = true;
            schedulerControlAusencias.Views.TimelineView.Appearance.AdditionalHeaderCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            schedulerControlAusencias.Views.TimelineView.Appearance.AdditionalHeaderCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            schedulerControlAusencias.Views.TimelineView.Appearance.ResourceHeaderCaption.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            schedulerControlAusencias.Views.TimelineView.Appearance.ResourceHeaderCaption.Options.UseFont = true;
            schedulerControlAusencias.Views.TimelineView.Appearance.ResourceHeaderCaptionLine.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            schedulerControlAusencias.Views.TimelineView.Appearance.ResourceHeaderCaptionLine.Options.UseFont = true;
            schedulerControlAusencias.Views.TimelineView.AppointmentDisplayOptions.AppointmentInterspacing = 0;
            schedulerControlAusencias.Views.TimelineView.ResourceHeight = 20;
            schedulerControlAusencias.Views.WeekView.Enabled = false;
            schedulerControlAusencias.Views.WorkWeekView.TimeRulers.Add(timeRuler3);
            schedulerControlAusencias.Views.YearView.UseOptimizedScrolling = false;
            // 
            // schedulerStorageAusencias
            // 
            // 
            // 
            // 
            schedulerStorageAusencias.AppointmentDependencies.AutoReload = false;
            // 
            // 
            // 
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(0, "Ninguno", "&Ninguno", SystemColors.Window);
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(1, "Importante", "&Importante", Color.FromArgb(255, 194, 190));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(2, "Trabajo", "&Trabajo", Color.FromArgb(168, 213, 255));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(3, "Particular", "Partic&ular", Color.FromArgb(193, 244, 156));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(4, "Vacaciones", "&Vacaciones", Color.FromArgb(243, 228, 199));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(5, "Requiere asistencia", "&Requiere asistencia", Color.FromArgb(244, 206, 147));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(6, "Requiere viajar", "Requiere via&jar", Color.FromArgb(199, 244, 255));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(7, "Requiere preparación", "Requiere &preparación", Color.FromArgb(207, 219, 152));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(8, "Cumpleaños", "&Cumpleaños", Color.FromArgb(224, 207, 233));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(9, "Aniversario", "&Aniversario", Color.FromArgb(141, 233, 223));
            schedulerStorageAusencias.Appointments.Labels.CreateNewLabel(10, "Llamada telefónica", "Llamada telefón&ica", Color.FromArgb(255, 247, 165));
            // 
            // dateMes
            // 
            dateMes.EditValue = null;
            dateMes.Location = new Point(4, 20);
            dateMes.Name = "dateMes";
            dateMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateMes.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateMes.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dateMes.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            dateMes.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            dateMes.Size = new Size(153, 20);
            dateMes.StyleController = layoutControl1;
            dateMes.TabIndex = 0;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(btnMesAnterior);
            layoutControl1.Controls.Add(btnRefrescar);
            layoutControl1.Controls.Add(btnMesSiguiente);
            layoutControl1.Controls.Add(dateMes);
            layoutControl1.Controls.Add(schedulerControlAusencias);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(1918, 1048);
            layoutControl1.TabIndex = 5;
            layoutControl1.Text = "layoutControl1";
            // 
            // btnMesAnterior
            // 
            btnMesAnterior.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMesAnterior.Appearance.Options.UseFont = true;
            btnMesAnterior.Location = new Point(1699, 4);
            btnMesAnterior.Name = "btnMesAnterior";
            btnMesAnterior.Size = new Size(106, 36);
            btnMesAnterior.StyleController = layoutControl1;
            btnMesAnterior.TabIndex = 3;
            btnMesAnterior.Text = "<";
            // 
            // btnRefrescar
            // 
            btnRefrescar.Appearance.BackColor = Color.DimGray;
            btnRefrescar.Appearance.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefrescar.Appearance.Options.UseBackColor = true;
            btnRefrescar.Appearance.Options.UseFont = true;
            btnRefrescar.ImageOptions.SvgImage = Properties.Resources.actions_refresh;
            btnRefrescar.Location = new Point(189, 4);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(120, 36);
            btnRefrescar.StyleController = layoutControl1;
            btnRefrescar.TabIndex = 2;
            btnRefrescar.Text = "Refrescar";
            // 
            // btnMesSiguiente
            // 
            btnMesSiguiente.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMesSiguiente.Appearance.Options.UseFont = true;
            btnMesSiguiente.Location = new Point(1809, 4);
            btnMesSiguiente.Name = "btnMesSiguiente";
            btnMesSiguiente.Size = new Size(105, 36);
            btnMesSiguiente.StyleController = layoutControl1;
            btnMesSiguiente.TabIndex = 4;
            btnMesSiguiente.Text = ">";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem3, emptySpaceItem1, layoutControlItem4, layoutControlItem2, layoutControlItem5, emptySpaceItem2 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            Root.Size = new Size(1918, 1048);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = schedulerControlAusencias;
            layoutControlItem1.Location = new Point(0, 40);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(1914, 1004);
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem1.TextSize = new Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnMesSiguiente;
            layoutControlItem3.Location = new Point(1805, 0);
            layoutControlItem3.MinSize = new Size(24, 28);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(109, 40);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new Point(309, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(1386, 40);
            emptySpaceItem1.TextSize = new Size(0, 0);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnRefrescar;
            layoutControlItem4.Location = new Point(185, 0);
            layoutControlItem4.MinSize = new Size(101, 40);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(124, 40);
            layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem4.TextSize = new Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.ForeColor = Color.White;
            layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            layoutControlItem2.Control = dateMes;
            layoutControlItem2.Location = new Point(0, 0);
            layoutControlItem2.MinSize = new Size(54, 24);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(157, 40);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.Text = "Seleccione Fecha";
            layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem2.TextSize = new Size(82, 13);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = btnMesAnterior;
            layoutControlItem5.Location = new Point(1695, 0);
            layoutControlItem5.MinSize = new Size(24, 28);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(110, 40);
            layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem5.TextSize = new Size(0, 0);
            layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new Point(157, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(28, 40);
            emptySpaceItem2.TextSize = new Size(0, 0);
            // 
            // NovedadesChoferesCalendarioForm
            // 
            Appearance.BackColor = Color.FromArgb(26, 29, 35);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1918, 1048);
            Controls.Add(layoutControl1);
            Name = "NovedadesChoferesCalendarioForm";
            Text = "NovedadesChoferesCalendarioForm";
            ((System.ComponentModel.ISupportInitialize)schedulerControlAusencias).EndInit();
            ((System.ComponentModel.ISupportInitialize)schedulerStorageAusencias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateMes.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateMes.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraScheduler.SchedulerControl schedulerControlAusencias;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerStorageAusencias;
        private DevExpress.XtraEditors.DateEdit dateMes;
        private DevExpress.XtraEditors.SimpleButton btnMesAnterior;
        private DevExpress.XtraEditors.SimpleButton btnMesSiguiente;
        private DevExpress.XtraEditors.SimpleButton btnRefrescar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}