namespace GestionOperativa.Reports
{
    partial class ReporteNominaMetanolActiva
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteNominaMetanolActiva));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.NominaMetanolActivaDto = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.NombreEmpresa = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NominaMetanolActivaDto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 20F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 20F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 28.74998F;
            this.Detail.Name = "Detail";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(166.4167F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(454.1667F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell7});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Tractor_Patente]")});
            this.xrTableCell3.Font = new DevExpress.Drawing.DXFont("Century Gothic", 12F);
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Tractor_Patente";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 2.0833334589096433D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Semirremolque_Patente]")});
            this.xrTableCell7.Font = new DevExpress.Drawing.DXFont("Century Gothic", 12F);
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 1.9895836443514523D;
            // 
            // NominaMetanolActivaDto
            // 
            this.NominaMetanolActivaDto.DataSource = typeof(global::Shared.Models.NominaMetanolActivaDto);
            this.NominaMetanolActivaDto.Name = "NominaMetanolActivaDto";
            // 
            // NombreEmpresa
            // 
            this.NombreEmpresa.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
            this.NombreEmpresa.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Empresa_Nombre", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.NombreEmpresa.HeightF = 42.49999F;
            this.NombreEmpresa.Name = "NombreEmpresa";
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Gray;
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Empresa_Nombre]")});
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Century Gothic", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.ForeColor = System.Drawing.Color.Black;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(166.4167F, 9.166637F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(454.1667F, 33.33335F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "xrLabel1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.PageHeader.HeightF = 111.0417F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel3,
            this.xrLine1,
            this.xrLabel2,
            this.xrPictureBox1});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(26.86996F, 8.000009F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(750.125F, 87.5F);
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPageInfo1.Font = new DevExpress.Drawing.DXFont("Century Gothic", 9F);
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(584.2917F, 54.75002F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(148.9582F, 17.99998F);
            this.xrPageInfo1.StylePriority.UseBorders = false;
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.TextFormatString = "Página {0} de {1}";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "GetDate(LocalDateTimeNow())\n")});
            this.xrLabel12.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(637.4165F, 14.75F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(95.8335F, 20F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "Hora:";
            this.xrLabel12.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "LocalDateTimeNow()")});
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(637.4165F, 34.75002F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(95.8335F, 20F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Hora:";
            this.xrLabel11.TextFormatString = "{0:HH:mm}";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel9.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(584.2917F, 34.75002F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(53.12469F, 20F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "Hora:";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel8.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(584.2917F, 14.75001F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(53.12482F, 20F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "Fecha:";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Century Gothic", 16F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(234.9583F, 44.99999F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(280.2084F, 23.00001F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "NOMINA FLOTA METANOL";
            // 
            // xrLine1
            // 
            this.xrLine1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLine1.BorderWidth = 1F;
            this.xrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(561.6201F, 1.525879E-05F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(11.92169F, 87.5F);
            this.xrLine1.StylePriority.UseBorders = false;
            this.xrLine1.StylePriority.UseBorderWidth = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Century Gothic", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(207.875F, 10F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(334.375F, 23F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Registro del Sistema de Gestión Integral";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrPictureBox1.BorderWidth = 1F;
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox1.ImageSource"));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(3.814697E-06F, 1.525879E-05F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(191.2083F, 87.49998F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            this.xrPictureBox1.StylePriority.UseBorderWidth = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel6,
            this.xrLabel7});
            this.PageFooter.HeightF = 46.04174F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 23.04176F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(250F, 22.99997F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "Archivo: QF71008 Nómina flota metanol. doc";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(293.9583F, 23.04174F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(117.7084F, 22.99997F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "formulario: QF10.08";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(466.7867F, 23.04174F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(117.7084F, 22.99997F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Rev: 00";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new DevExpress.Drawing.DXFont("Century Gothic", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(642.4117F, 23.04177F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(117.7084F, 22.99997F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "fecha: 01/10/16";
            // 
            // ReporteNominaMetanolActiva
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.NombreEmpresa,
            this.PageHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.NominaMetanolActivaDto});
            this.DataSource = this.NominaMetanolActivaDto;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(20F, 20F, 20F, 20F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Version = "24.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NominaMetanolActivaDto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource NominaMetanolActivaDto;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.GroupHeaderBand NombreEmpresa;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
    }
}
