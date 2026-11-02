using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Lab09_DataReaderSearch.GUI
{
    partial class frmMain
    {
        private IContainer components = null;

        // Top Panel
        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSub;

        // Filter GroupBox
        private GroupBox grpFilter;
        private Label lblSearchMaSV;
        private TextBox txtSearchMaSV;
        private Label lblSearchHoTen;
        private TextBox txtSearchHoTen;
        private Label lblSearchKhoa;
        private ComboBox cboKhoa;
        private Label lblSearchGioiTinh;
        private ComboBox cboGioiTinh;

        private Label lblDiem;
        private NumericUpDown numDiemMin;
        private Label lblDiemTo;
        private NumericUpDown numDiemMax;

        private Label lblTuoi;
        private NumericUpDown numTuoiMin;
        private Label lblTuoiTo;
        private NumericUpDown numTuoiMax;

        private Label lblSort;
        private ComboBox cboSortCol;
        private ComboBox cboSortDir;

        private Button btnSearch;
        private Button btnReset;
        private Button btnExportCsv;

        private Label lblDemoMode;
        private ComboBox cboDemoMode;

        // Content Area Panels
        private Panel pnlContent;

        // DataGridView Area
        private GroupBox grpData;
        private DataGridView dgvSinhVien;
        private Panel pnlPaging;
        private Button btnFirst;
        private Button btnPrev;
        private Label lblPageInfo;
        private Button btnNext;
        private Button btnLast;
        private Label lblPageSize;
        private ComboBox cboPageSize;
        private Label lblTotalRecords;

        // Detail Area
        private GroupBox grpDetail;
        private PictureBox picAvatar;
        private Label lblDetailMaSV;
        private TextBox txtDetailMaSV;
        private Label lblDetailHoTen;
        private TextBox txtDetailHoTen;
        private Label lblDetailNgaySinh;
        private DateTimePicker dtpDetailNgaySinh;
        private Label lblDetailTuoi;
        private TextBox txtDetailTuoi;
        private Label lblDetailGioiTinh;
        private RadioButton rdoDetailNam;
        private RadioButton rdoDetailNu;
        private Label lblDetailKhoa;
        private TextBox txtDetailKhoa;
        private Label lblDetailDiemTB;
        private TextBox txtDetailDiemTB;
        private Label lblDetailEmail;
        private TextBox txtDetailEmail;
        private Label lblLegend;
        private Panel pnlLegendLow;
        private Label lblLegendLow;
        private Panel pnlLegendHigh;
        private Label lblLegendHigh;

        // Status Strip
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblAuthor;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblHeaderTitle = new Label();
            this.lblHeaderSub = new Label();

            this.grpFilter = new GroupBox();
            this.lblSearchMaSV = new Label();
            this.txtSearchMaSV = new TextBox();
            this.lblSearchHoTen = new Label();
            this.txtSearchHoTen = new TextBox();
            this.lblSearchKhoa = new Label();
            this.cboKhoa = new ComboBox();
            this.lblSearchGioiTinh = new Label();
            this.cboGioiTinh = new ComboBox();

            this.lblDiem = new Label();
            this.numDiemMin = new NumericUpDown();
            this.lblDiemTo = new Label();
            this.numDiemMax = new NumericUpDown();

            this.lblTuoi = new Label();
            this.numTuoiMin = new NumericUpDown();
            this.lblTuoiTo = new Label();
            this.numTuoiMax = new NumericUpDown();

            this.lblSort = new Label();
            this.cboSortCol = new ComboBox();
            this.cboSortDir = new ComboBox();

            this.btnSearch = new Button();
            this.btnReset = new Button();
            this.btnExportCsv = new Button();

            this.lblDemoMode = new Label();
            this.cboDemoMode = new ComboBox();

            this.pnlContent = new Panel();

            this.grpData = new GroupBox();
            this.dgvSinhVien = new DataGridView();
            this.pnlPaging = new Panel();
            this.btnFirst = new Button();
            this.btnPrev = new Button();
            this.lblPageInfo = new Label();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.lblPageSize = new Label();
            this.cboPageSize = new ComboBox();
            this.lblTotalRecords = new Label();

            this.grpDetail = new GroupBox();
            this.picAvatar = new PictureBox();
            this.lblDetailMaSV = new Label();
            this.txtDetailMaSV = new TextBox();
            this.lblDetailHoTen = new Label();
            this.txtDetailHoTen = new TextBox();
            this.lblDetailNgaySinh = new Label();
            this.dtpDetailNgaySinh = new DateTimePicker();
            this.lblDetailTuoi = new Label();
            this.txtDetailTuoi = new TextBox();
            this.lblDetailGioiTinh = new Label();
            this.rdoDetailNam = new RadioButton();
            this.rdoDetailNu = new RadioButton();
            this.lblDetailKhoa = new Label();
            this.txtDetailKhoa = new TextBox();
            this.lblDetailDiemTB = new Label();
            this.txtDetailDiemTB = new TextBox();
            this.lblDetailEmail = new Label();
            this.txtDetailEmail = new TextBox();

            this.lblLegend = new Label();
            this.pnlLegendLow = new Panel();
            this.lblLegendLow = new Label();
            this.pnlLegendHigh = new Panel();
            this.lblLegendHigh = new Label();

            this.statusStrip1 = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();
            this.lblAuthor = new ToolStripStatusLabel();

            ((ISupportInitialize)(this.numDiemMin)).BeginInit();
            ((ISupportInitialize)(this.numDiemMax)).BeginInit();
            ((ISupportInitialize)(this.numTuoiMin)).BeginInit();
            ((ISupportInitialize)(this.numTuoiMax)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.grpData.SuspendLayout();
            ((ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.pnlPaging.SuspendLayout();
            this.grpDetail.SuspendLayout();
            ((ISupportInitialize)(this.picAvatar)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = Color.FromArgb(235, 243, 252);
            this.pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.lblHeaderSub);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(1184, 62);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = Color.FromArgb(16, 54, 110);
            this.lblHeaderTitle.Location = new Point(16, 8);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new Size(740, 23);
            this.lblHeaderTitle.Text = "LAB 9: ĐỌC, TÌM KIẾM VÀ LỌC DỮ LIỆU BẰNG SQLDATAREADER";

            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new Font("Segoe UI", 9F);
            this.lblHeaderSub.ForeColor = Color.FromArgb(60, 60, 60);
            this.lblHeaderSub.Location = new Point(18, 34);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new Size(780, 15);
            this.lblHeaderSub.Text = "Học phần: Lập trình .NET (IT3212) | Sinh viên: Phan Văn Thắng - MSSV: 24002545 - Lớp: IT3212 - EAUT";

            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.lblDemoMode);
            this.grpFilter.Controls.Add(this.cboDemoMode);
            this.grpFilter.Controls.Add(this.btnExportCsv);
            this.grpFilter.Controls.Add(this.btnReset);
            this.grpFilter.Controls.Add(this.btnSearch);
            this.grpFilter.Controls.Add(this.cboSortDir);
            this.grpFilter.Controls.Add(this.cboSortCol);
            this.grpFilter.Controls.Add(this.lblSort);
            this.grpFilter.Controls.Add(this.numTuoiMax);
            this.grpFilter.Controls.Add(this.lblTuoiTo);
            this.grpFilter.Controls.Add(this.numTuoiMin);
            this.grpFilter.Controls.Add(this.lblTuoi);
            this.grpFilter.Controls.Add(this.numDiemMax);
            this.grpFilter.Controls.Add(this.lblDiemTo);
            this.grpFilter.Controls.Add(this.numDiemMin);
            this.grpFilter.Controls.Add(this.lblDiem);
            this.grpFilter.Controls.Add(this.cboGioiTinh);
            this.grpFilter.Controls.Add(this.lblSearchGioiTinh);
            this.grpFilter.Controls.Add(this.cboKhoa);
            this.grpFilter.Controls.Add(this.lblSearchKhoa);
            this.grpFilter.Controls.Add(this.txtSearchHoTen);
            this.grpFilter.Controls.Add(this.lblSearchHoTen);
            this.grpFilter.Controls.Add(this.txtSearchMaSV);
            this.grpFilter.Controls.Add(this.lblSearchMaSV);
            this.grpFilter.Dock = DockStyle.Top;
            this.grpFilter.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.grpFilter.Location = new Point(0, 62);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new Size(1184, 138);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Bộ lọc tra cứu dữ liệu (Bài 2, 3, 6, 7, 8)";

            // Row 1: MaSV, HoTen, Khoa, GioiTinh
            this.lblSearchMaSV.AutoSize = true;
            this.lblSearchMaSV.Location = new Point(16, 26);
            this.lblSearchMaSV.Text = "Mã SV:";
            this.txtSearchMaSV.Location = new Point(68, 23);
            this.txtSearchMaSV.Size = new Size(100, 23);

            this.lblSearchHoTen.AutoSize = true;
            this.lblSearchHoTen.Location = new Point(180, 26);
            this.lblSearchHoTen.Text = "Họ tên (Bài 2):";
            this.txtSearchHoTen.Location = new Point(275, 23);
            this.txtSearchHoTen.Size = new Size(170, 23);

            this.lblSearchKhoa.AutoSize = true;
            this.lblSearchKhoa.Location = new Point(460, 26);
            this.lblSearchKhoa.Text = "Khoa (Bài 3):";
            this.cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboKhoa.Location = new Point(540, 23);
            this.cboKhoa.Size = new Size(185, 23);

            this.lblSearchGioiTinh.AutoSize = true;
            this.lblSearchGioiTinh.Location = new Point(740, 26);
            this.lblSearchGioiTinh.Text = "Giới tính:";
            this.cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Items.AddRange(new object[] { "-- Tất cả --", "Nam", "Nữ" });
            this.cboGioiTinh.SelectedIndex = 0;
            this.cboGioiTinh.Location = new Point(805, 23);
            this.cboGioiTinh.Size = new Size(110, 23);

            // Row 2: DiemTB, Tuoi, SapXep
            this.lblDiem.AutoSize = true;
            this.lblDiem.Location = new Point(16, 62);
            this.lblDiem.Text = "Điểm TB:";
            this.numDiemMin.DecimalPlaces = 1;
            this.numDiemMin.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            this.numDiemMin.Location = new Point(68, 60);
            this.numDiemMin.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numDiemMin.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numDiemMin.Size = new Size(52, 23);
            this.numDiemMin.Value = new decimal(new int[] { 0, 0, 0, 0 });

            this.lblDiemTo.AutoSize = true;
            this.lblDiemTo.Location = new Point(124, 62);
            this.lblDiemTo.Text = "đến";
            this.numDiemMax.DecimalPlaces = 1;
            this.numDiemMax.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            this.numDiemMax.Location = new Point(152, 60);
            this.numDiemMax.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numDiemMax.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numDiemMax.Size = new Size(52, 23);
            this.numDiemMax.Value = new decimal(new int[] { 10, 0, 0, 0 });

            this.lblTuoi.AutoSize = true;
            this.lblTuoi.Location = new Point(220, 62);
            this.lblTuoi.Text = "Tuổi (Bài 8):";
            this.numTuoiMin.Location = new Point(295, 60);
            this.numTuoiMin.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.numTuoiMin.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            this.numTuoiMin.Size = new Size(48, 23);
            this.numTuoiMin.Value = new decimal(new int[] { 17, 0, 0, 0 });

            this.lblTuoiTo.AutoSize = true;
            this.lblTuoiTo.Location = new Point(348, 62);
            this.lblTuoiTo.Text = "đến";
            this.numTuoiMax.Location = new Point(376, 60);
            this.numTuoiMax.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.numTuoiMax.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            this.numTuoiMax.Size = new Size(48, 23);
            this.numTuoiMax.Value = new decimal(new int[] { 30, 0, 0, 0 });

            this.lblSort.AutoSize = true;
            this.lblSort.Location = new Point(440, 62);
            this.lblSort.Text = "Sắp xếp (Bài 7):";
            this.cboSortCol.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSortCol.Items.AddRange(new object[] { 
                "Mã SV (MaSV)", 
                "Họ tên (HoTen)", 
                "Điểm TB (DiemTB)", 
                "Ngày sinh (NgaySinh)", 
                "Khoa (TenKhoa)",
                "Tuổi (Tuoi)"
            });
            this.cboSortCol.SelectedIndex = 0;
            this.cboSortCol.Location = new Point(540, 60);
            this.cboSortCol.Size = new Size(135, 23);

            this.cboSortDir.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSortDir.Items.AddRange(new object[] { "Tăng dần (ASC)", "Giảm dần (DESC)" });
            this.cboSortDir.SelectedIndex = 0;
            this.cboSortDir.Location = new Point(682, 60);
            this.cboSortDir.Size = new Size(125, 23);

            // Row 3: Action Buttons & Quick Test Scenario
            this.btnSearch.Location = new Point(16, 96);
            this.btnSearch.Size = new Size(115, 28);
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;

            this.btnReset.Location = new Point(138, 96);
            this.btnReset.Size = new Size(140, 28);
            this.btnReset.Text = "🔄 Nạp lại tất cả (Bài 1)";
            this.btnReset.UseVisualStyleBackColor = true;

            this.btnExportCsv.Location = new Point(285, 96);
            this.btnExportCsv.Size = new Size(145, 28);
            this.btnExportCsv.Text = "📥 Xuất CSV (Bài 10)";
            this.btnExportCsv.UseVisualStyleBackColor = true;

            this.lblDemoMode.AutoSize = true;
            this.lblDemoMode.Location = new Point(445, 102);
            this.lblDemoMode.Text = "🎯 Kịch bản mẫu từng bài:";
            this.cboDemoMode.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDemoMode.Items.AddRange(new object[] {
                "-- Chọn kịch bản kiểm thử --",
                "Bài 1: Nạp toàn bộ bằng SqlDataReader",
                "Bài 2: Tìm kiếm họ tên chứa 'Thắng'",
                "Bài 3: Lọc Khoa CNTT và Điểm 7.0 - 9.0",
                "Bài 4: Phân trang OFFSET-FETCH (10 dòng/trang)",
                "Bài 6: Tìm đa tiêu chí (Nam + Điểm >= 7.5)",
                "Bài 7: Sắp xếp theo Điểm TB giảm dần",
                "Bài 8: Lọc sinh viên theo tuổi (20 - 22 tuổi)",
                "Bài 9: Định dạng và Tô màu điểm số (<5 đỏ, >=8 xanh)"
            });
            this.cboDemoMode.SelectedIndex = 0;
            this.cboDemoMode.Location = new Point(600, 99);
            this.cboDemoMode.Size = new Size(320, 23);

            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.grpData);
            this.pnlContent.Controls.Add(this.grpDetail);
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Location = new Point(0, 200);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new Size(1184, 460);
            this.pnlContent.TabIndex = 2;

            // 
            // grpDetail (Bài 5) - Docked Right
            // 
            this.grpDetail.Controls.Add(this.lblLegendHigh);
            this.grpDetail.Controls.Add(this.pnlLegendHigh);
            this.grpDetail.Controls.Add(this.lblLegendLow);
            this.grpDetail.Controls.Add(this.pnlLegendLow);
            this.grpDetail.Controls.Add(this.lblLegend);
            this.grpDetail.Controls.Add(this.txtDetailEmail);
            this.grpDetail.Controls.Add(this.lblDetailEmail);
            this.grpDetail.Controls.Add(this.txtDetailDiemTB);
            this.grpDetail.Controls.Add(this.lblDetailDiemTB);
            this.grpDetail.Controls.Add(this.txtDetailKhoa);
            this.grpDetail.Controls.Add(this.lblDetailKhoa);
            this.grpDetail.Controls.Add(this.rdoDetailNu);
            this.grpDetail.Controls.Add(this.rdoDetailNam);
            this.grpDetail.Controls.Add(this.lblDetailGioiTinh);
            this.grpDetail.Controls.Add(this.txtDetailTuoi);
            this.grpDetail.Controls.Add(this.lblDetailTuoi);
            this.grpDetail.Controls.Add(this.dtpDetailNgaySinh);
            this.grpDetail.Controls.Add(this.lblDetailNgaySinh);
            this.grpDetail.Controls.Add(this.txtDetailHoTen);
            this.grpDetail.Controls.Add(this.lblDetailHoTen);
            this.grpDetail.Controls.Add(this.txtDetailMaSV);
            this.grpDetail.Controls.Add(this.lblDetailMaSV);
            this.grpDetail.Controls.Add(this.picAvatar);
            this.grpDetail.Dock = DockStyle.Right;
            this.grpDetail.Font = new Font("Segoe UI", 9F);
            this.grpDetail.Location = new Point(834, 0);
            this.grpDetail.Name = "grpDetail";
            this.grpDetail.Padding = new Padding(8);
            this.grpDetail.Size = new Size(350, 460);
            this.grpDetail.TabIndex = 1;
            this.grpDetail.TabStop = false;
            this.grpDetail.Text = "Thông tin chi tiết sinh viên (Bài 5)";

            // Avatar
            this.picAvatar.BorderStyle = BorderStyle.FixedSingle;
            this.picAvatar.Location = new Point(125, 20);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new Size(100, 100);
            this.picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            // MaSV
            this.lblDetailMaSV.AutoSize = true;
            this.lblDetailMaSV.Location = new Point(16, 133);
            this.lblDetailMaSV.Text = "Mã SV:";
            this.txtDetailMaSV.Location = new Point(90, 130);
            this.txtDetailMaSV.ReadOnly = true;
            this.txtDetailMaSV.Size = new Size(240, 23);

            // HoTen
            this.lblDetailHoTen.AutoSize = true;
            this.lblDetailHoTen.Location = new Point(16, 163);
            this.lblDetailHoTen.Text = "Họ và tên:";
            this.txtDetailHoTen.Location = new Point(90, 160);
            this.txtDetailHoTen.ReadOnly = true;
            this.txtDetailHoTen.Size = new Size(240, 23);

            // NgaySinh
            this.lblDetailNgaySinh.AutoSize = true;
            this.lblDetailNgaySinh.Location = new Point(16, 193);
            this.lblDetailNgaySinh.Text = "Ngày sinh:";
            this.dtpDetailNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpDetailNgaySinh.Enabled = false;
            this.dtpDetailNgaySinh.Format = DateTimePickerFormat.Custom;
            this.dtpDetailNgaySinh.Location = new Point(90, 190);
            this.dtpDetailNgaySinh.Size = new Size(130, 23);

            // Tuoi
            this.lblDetailTuoi.AutoSize = true;
            this.lblDetailTuoi.Location = new Point(230, 193);
            this.lblDetailTuoi.Text = "Tuổi:";
            this.txtDetailTuoi.Location = new Point(268, 190);
            this.txtDetailTuoi.ReadOnly = true;
            this.txtDetailTuoi.Size = new Size(62, 23);

            // GioiTinh
            this.lblDetailGioiTinh.AutoSize = true;
            this.lblDetailGioiTinh.Location = new Point(16, 223);
            this.lblDetailGioiTinh.Text = "Giới tính:";
            this.rdoDetailNam.AutoSize = true;
            this.rdoDetailNam.Enabled = false;
            this.rdoDetailNam.Location = new Point(92, 221);
            this.rdoDetailNam.Text = "Nam";
            this.rdoDetailNu.AutoSize = true;
            this.rdoDetailNu.Enabled = false;
            this.rdoDetailNu.Location = new Point(160, 221);
            this.rdoDetailNu.Text = "Nữ";

            // Khoa
            this.lblDetailKhoa.AutoSize = true;
            this.lblDetailKhoa.Location = new Point(16, 253);
            this.lblDetailKhoa.Text = "Khoa:";
            this.txtDetailKhoa.Location = new Point(90, 250);
            this.txtDetailKhoa.ReadOnly = true;
            this.txtDetailKhoa.Size = new Size(240, 23);

            // DiemTB
            this.lblDetailDiemTB.AutoSize = true;
            this.lblDetailDiemTB.Location = new Point(16, 283);
            this.lblDetailDiemTB.Text = "Điểm TB:";
            this.txtDetailDiemTB.Location = new Point(90, 280);
            this.txtDetailDiemTB.ReadOnly = true;
            this.txtDetailDiemTB.Size = new Size(240, 23);

            // Email
            this.lblDetailEmail.AutoSize = true;
            this.lblDetailEmail.Location = new Point(16, 313);
            this.lblDetailEmail.Text = "Email:";
            this.txtDetailEmail.Location = new Point(90, 310);
            this.txtDetailEmail.ReadOnly = true;
            this.txtDetailEmail.Size = new Size(240, 23);

            // Legend for Exercise 9
            this.lblLegend.AutoSize = true;
            this.lblLegend.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblLegend.Location = new Point(16, 355);
            this.lblLegend.Text = "Quy tắc tô màu điểm số (Bài 9):";

            this.pnlLegendLow.BorderStyle = BorderStyle.FixedSingle;
            this.pnlLegendLow.BackColor = Color.FromArgb(255, 230, 230);
            this.pnlLegendLow.Location = new Point(20, 380);
            this.pnlLegendLow.Size = new Size(20, 16);

            this.lblLegendLow.AutoSize = true;
            this.lblLegendLow.Font = new Font("Segoe UI", 8.5F);
            this.lblLegendLow.ForeColor = Color.DarkRed;
            this.lblLegendLow.Location = new Point(46, 381);
            this.lblLegendLow.Text = "Điểm < 5.0 (Cảnh báo học tập)";

            this.pnlLegendHigh.BorderStyle = BorderStyle.FixedSingle;
            this.pnlLegendHigh.BackColor = Color.FromArgb(230, 255, 230);
            this.pnlLegendHigh.Location = new Point(20, 405);
            this.pnlLegendHigh.Size = new Size(20, 16);

            this.lblLegendHigh.AutoSize = true;
            this.lblLegendHigh.Font = new Font("Segoe UI", 8.5F);
            this.lblLegendHigh.ForeColor = Color.DarkGreen;
            this.lblLegendHigh.Location = new Point(46, 406);
            this.lblLegendHigh.Text = "Điểm ≥ 8.0 (Học lực Giỏi - Xuất sắc)";

            // 
            // grpData - Docked Fill
            // 
            this.grpData.Controls.Add(this.dgvSinhVien);
            this.grpData.Controls.Add(this.pnlPaging);
            this.grpData.Dock = DockStyle.Fill;
            this.grpData.Font = new Font("Segoe UI", 9F);
            this.grpData.Location = new Point(0, 0);
            this.grpData.Name = "grpData";
            this.grpData.Padding = new Padding(8);
            this.grpData.Size = new Size(834, 460);
            this.grpData.TabIndex = 0;
            this.grpData.TabStop = false;
            this.grpData.Text = "Danh sách sinh viên (SqlDataReader -> DataTable)";

            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.AllowUserToAddRows = false;
            this.dgvSinhVien.AllowUserToDeleteRows = false;
            this.dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSinhVien.BackgroundColor = SystemColors.Window;
            this.dgvSinhVien.BorderStyle = BorderStyle.Fixed3D;
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Dock = DockStyle.Fill;
            this.dgvSinhVien.Location = new Point(8, 24);
            this.dgvSinhVien.MultiSelect = false;
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.ReadOnly = true;
            this.dgvSinhVien.RowHeadersWidth = 32;
            this.dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSinhVien.Size = new Size(818, 388);
            this.dgvSinhVien.TabIndex = 0;

            // 
            // pnlPaging (Bài 4)
            // 
            this.pnlPaging.Controls.Add(this.lblTotalRecords);
            this.pnlPaging.Controls.Add(this.cboPageSize);
            this.pnlPaging.Controls.Add(this.lblPageSize);
            this.pnlPaging.Controls.Add(this.btnLast);
            this.pnlPaging.Controls.Add(this.btnNext);
            this.pnlPaging.Controls.Add(this.lblPageInfo);
            this.pnlPaging.Controls.Add(this.btnPrev);
            this.pnlPaging.Controls.Add(this.btnFirst);
            this.pnlPaging.Dock = DockStyle.Bottom;
            this.pnlPaging.Location = new Point(8, 412);
            this.pnlPaging.Name = "pnlPaging";
            this.pnlPaging.Size = new Size(818, 40);
            this.pnlPaging.TabIndex = 1;

            this.btnFirst.Location = new Point(8, 6);
            this.btnFirst.Size = new Size(40, 28);
            this.btnFirst.Text = "|<";

            this.btnPrev.Location = new Point(52, 6);
            this.btnPrev.Size = new Size(65, 28);
            this.btnPrev.Text = "< Trước";

            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPageInfo.Location = new Point(125, 12);
            this.lblPageInfo.Text = "Trang 1 / 4";

            this.btnNext.Location = new Point(205, 6);
            this.btnNext.Size = new Size(65, 28);
            this.btnNext.Text = "Sau >";

            this.btnLast.Location = new Point(274, 6);
            this.btnLast.Size = new Size(40, 28);
            this.btnLast.Text = ">|";

            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Location = new Point(335, 12);
            this.lblPageSize.Text = "Số dòng/trang:";

            this.cboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPageSize.Items.AddRange(new object[] { "5", "10", "15", "20" });
            this.cboPageSize.SelectedIndex = 1; // 10
            this.cboPageSize.Location = new Point(425, 8);
            this.cboPageSize.Size = new Size(55, 23);

            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblTotalRecords.ForeColor = Color.DarkSlateBlue;
            this.lblTotalRecords.Location = new Point(500, 12);
            this.lblTotalRecords.Text = "Tổng số sinh viên: 31";

            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new ToolStripItem[] {
                this.lblStatus,
                this.lblAuthor
            });
            this.statusStrip1.Location = new Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(1184, 24);
            this.statusStrip1.TabIndex = 3;

            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(800, 19);
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.lblStatus.Text = "Đang chọn: Chưa chọn dòng nào";

            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new Size(350, 19);
            this.lblAuthor.Text = "Phan Văn Thắng - MSSV: 24002545 - IT3212 | Lab 9 EAUT";

            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1184, 684);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new Size(1000, 650);
            this.Name = "frmMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Lab 09 - Quản lý, Tra cứu và Lọc sinh viên bằng SqlDataReader";

            ((ISupportInitialize)(this.numDiemMin)).EndInit();
            ((ISupportInitialize)(this.numDiemMax)).EndInit();
            ((ISupportInitialize)(this.numTuoiMin)).EndInit();
            ((ISupportInitialize)(this.numTuoiMax)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.grpData.ResumeLayout(false);
            ((ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.pnlPaging.ResumeLayout(false);
            this.pnlPaging.PerformLayout();
            this.grpDetail.ResumeLayout(false);
            this.grpDetail.PerformLayout();
            ((ISupportInitialize)(this.picAvatar)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
