using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Lab09_DataReaderSearch.DAL;

namespace Lab09_DataReaderSearch.GUI
{
    public partial class frmMain : Form
    {
        private readonly SinhVienDAL _sinhVienDAL = new SinhVienDAL();
        private readonly KhoaDAL _khoaDAL = new KhoaDAL();

        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private int _totalPages = 1;

        public frmMain()
        {
            InitializeComponent();
            SetupDataGridView();
            RegisterEvents();
        }

        private void SetupDataGridView()
        {
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.Columns.Clear();

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaSV",
                HeaderText = "Mã SV",
                Name = "colMaSV",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "Họ và tên",
                Name = "colHoTen",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày sinh",
                Name = "colNgaySinh",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tuoi",
                HeaderText = "Tuổi",
                Name = "colTuoi",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioiTinh",
                HeaderText = "Giới tính",
                Name = "colGioiTinh",
                Width = 75,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenKhoa",
                HeaderText = "Khoa",
                Name = "colTenKhoa",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DiemTB",
                HeaderText = "Điểm TB",
                Name = "colDiemTB",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "0.00" }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "colEmail",
                Width = 170,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKhoa",
                HeaderText = "MaKhoa",
                Name = "colMaKhoa",
                Visible = false
            });
        }

        private void RegisterEvents()
        {
            this.Load += FrmMain_Load;
            this.dgvSinhVien.SelectionChanged += DgvSinhVien_SelectionChanged;
            this.dgvSinhVien.CellFormatting += DgvSinhVien_CellFormatting;

            this.btnSearch.Click += BtnSearch_Click;
            this.btnReset.Click += BtnReset_Click;
            this.btnExportCsv.Click += BtnExportCsv_Click;
            this.cboDemoMode.SelectedIndexChanged += CboDemoMode_SelectedIndexChanged;

            this.btnFirst.Click += (s, e) => NavigatePage(1);
            this.btnPrev.Click += (s, e) => NavigatePage(_currentPage - 1);
            this.btnNext.Click += (s, e) => NavigatePage(_currentPage + 1);
            this.btnLast.Click += (s, e) => NavigatePage(_totalPages);
            this.cboPageSize.SelectedIndexChanged += CboPageSize_SelectedIndexChanged;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadKhoaComboBox();
            ResetFilterControls();
            LoadDataCurrentPage();
        }

        private void LoadKhoaComboBox()
        {
            try
            {
                DataTable dtKhoa = _khoaDAL.GetAllKhoa(includeAllOption: true);
                cboKhoa.DataSource = dtKhoa;
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi nạp danh sách Khoa: " + ex.Message;
            }
        }

        public void ResetFilterControls()
        {
            txtSearchMaSV.Text = string.Empty;
            txtSearchHoTen.Text = string.Empty;
            if (cboKhoa.Items.Count > 0) cboKhoa.SelectedIndex = 0;
            cboGioiTinh.SelectedIndex = 0;
            numDiemMin.Value = 0m;
            numDiemMax.Value = 10m;
            numTuoiMin.Value = 17m;
            numTuoiMax.Value = 35m;
            cboSortCol.SelectedIndex = 0; // MaSV
            cboSortDir.SelectedIndex = 0; // ASC
            _currentPage = 1;
        }

        public void LoadDataCurrentPage()
        {
            try
            {
                string maSV = txtSearchMaSV.Text.Trim();
                string hoTen = txtSearchHoTen.Text.Trim();
                string maKhoa = cboKhoa.SelectedValue?.ToString() ?? "";
                
                int? gioiTinh = null;
                if (cboGioiTinh.SelectedIndex == 1) gioiTinh = 1;      // Nam
                else if (cboGioiTinh.SelectedIndex == 2) gioiTinh = 0; // Nữ

                decimal minDiem = numDiemMin.Value;
                decimal maxDiem = numDiemMax.Value;
                int minTuoi = (int)numTuoiMin.Value;
                int maxTuoi = (int)numTuoiMax.Value;

                // Sort column whitelist mapping
                string sortCol = "MaSV";
                switch (cboSortCol.SelectedIndex)
                {
                    case 0: sortCol = "MaSV"; break;
                    case 1: sortCol = "HoTen"; break;
                    case 2: sortCol = "DiemTB"; break;
                    case 3: sortCol = "NgaySinh"; break;
                    case 4: sortCol = "TenKhoa"; break;
                    case 5: sortCol = "Tuoi"; break;
                }
                string sortDir = cboSortDir.SelectedIndex == 1 ? "DESC" : "ASC";

                DataTable dt = _sinhVienDAL.SearchAdvanced(
                    maSV, hoTen, maKhoa, gioiTinh, minDiem, maxDiem, minTuoi, maxTuoi,
                    sortCol, sortDir, _currentPage, _pageSize, out _totalRecords);

                dgvSinhVien.DataSource = dt;
                UpdatePagingUI();

                if (dgvSinhVien.Rows.Count > 0)
                {
                    dgvSinhVien.Rows[0].Selected = true;
                    DisplaySelectedRow(dgvSinhVien.Rows[0]);
                }
                else
                {
                    ClearDetailFields();
                    lblStatus.Text = "Không tìm thấy sinh viên nào phù hợp tiêu chí.";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi truy vấn: " + ex.Message;
            }
        }

        private void UpdatePagingUI()
        {
            _totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_totalPages < 1) _totalPages = 1;
            if (_currentPage > _totalPages) _currentPage = _totalPages;

            lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}";
            lblTotalRecords.Text = $"Tổng số sinh viên: {_totalRecords}";

            btnFirst.Enabled = (_currentPage > 1);
            btnPrev.Enabled = (_currentPage > 1);
            btnNext.Enabled = (_currentPage < _totalPages);
            btnLast.Enabled = (_currentPage < _totalPages);
        }

        private void NavigatePage(int targetPage)
        {
            if (targetPage < 1) targetPage = 1;
            if (targetPage > _totalPages) targetPage = _totalPages;
            _currentPage = targetPage;
            LoadDataCurrentPage();
        }

        private void CboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboPageSize.SelectedItem?.ToString(), out int newSize))
            {
                _pageSize = newSize;
                _currentPage = 1;
                LoadDataCurrentPage();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            LoadDataCurrentPage();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetFilterControls();
            cboDemoMode.SelectedIndex = 0;
            LoadDataCurrentPage();
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                string savedFile = CsvExporter.ExportToCsv(dgvSinhVien, $"DanhSachSinhVien_Trang{_currentPage}.csv");
                if (!string.IsNullOrEmpty(savedFile))
                {
                    MessageBox.Show($"Đã xuất thành công {dgvSinhVien.Rows.Count} bản ghi ra tệp:\n{savedFile}\n(Định dạng UTF-8 kèm BOM hỗ trợ tiếng Việt)",
                        "Xuất CSV Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------------
        // BÀI 5: Hiển thị chi tiết dòng được chọn
        // -------------------------------------------------------------
        private void DgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null || dgvSinhVien.CurrentRow.Index < 0)
            {
                return;
            }

            DisplaySelectedRow(dgvSinhVien.CurrentRow);
        }

        private void DisplaySelectedRow(DataGridViewRow row)
        {
            if (row == null || row.DataBoundItem == null) return;

            string maSV = Convert.ToString(row.Cells["colMaSV"].Value);
            string hoTen = Convert.ToString(row.Cells["colHoTen"].Value);
            string gioiTinhStr = Convert.ToString(row.Cells["colGioiTinh"].Value);
            string tenKhoa = Convert.ToString(row.Cells["colTenKhoa"].Value);
            string email = Convert.ToString(row.Cells["colEmail"].Value);
            object rawDiem = row.Cells["colDiemTB"].Value;
            object rawNgaySinh = row.Cells["colNgaySinh"].Value;
            object rawTuoi = row.Cells["colTuoi"].Value;

            txtDetailMaSV.Text = maSV;
            txtDetailHoTen.Text = hoTen;
            txtDetailKhoa.Text = tenKhoa;
            txtDetailEmail.Text = email;

            if (rawNgaySinh != null && rawNgaySinh != DBNull.Value && rawNgaySinh is DateTime dt)
            {
                dtpDetailNgaySinh.Value = dt;
            }

            if (rawTuoi != null && rawTuoi != DBNull.Value)
            {
                txtDetailTuoi.Text = rawTuoi.ToString();
            }
            else
            {
                txtDetailTuoi.Text = "--";
            }

            bool isNam = (gioiTinhStr == "Nam");
            rdoDetailNam.Checked = isNam;
            rdoDetailNu.Checked = !isNam;

            if (rawDiem != null && rawDiem != DBNull.Value && decimal.TryParse(rawDiem.ToString(), out decimal diem))
            {
                string xepLoai = "Trung bình";
                if (diem < 5.0m) xepLoai = "Yếu / Kém";
                else if (diem < 6.5m) xepLoai = "Trung bình";
                else if (diem < 8.0m) xepLoai = "Khá";
                else if (diem < 9.0m) xepLoai = "Giỏi";
                else xepLoai = "Xuất sắc";

                txtDetailDiemTB.Text = $"{diem:0.00} - Xếp loại: {xepLoai}";
            }
            else
            {
                txtDetailDiemTB.Text = "--";
            }

            LoadAvatar(maSV, isNam);

            // Cập nhật thanh trạng thái theo đúng yêu cầu Bài 5
            lblStatus.Text = $"Đang chọn: {maSV} - {hoTen} | Trang {_currentPage}/{_totalPages} | Tổng số: {_totalRecords} sinh viên";
        }

        private void LoadAvatar(string maSV, bool isNam)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string imgDir = Path.Combine(baseDir, "Images");

                string targetImg = null;
                if (maSV == "24002545")
                {
                    targetImg = Path.Combine(imgDir, "avatar_thang.png");
                }
                else if (!isNam)
                {
                    targetImg = Path.Combine(imgDir, "avatar_binh.png");
                }
                else
                {
                    targetImg = Path.Combine(imgDir, "avatar_thang.png");
                }

                if (File.Exists(targetImg))
                {
                    using (var bmpTemp = new Bitmap(targetImg))
                    {
                        picAvatar.Image = new Bitmap(bmpTemp);
                    }
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
            catch
            {
                picAvatar.Image = null;
            }
        }

        private void ClearDetailFields()
        {
            txtDetailMaSV.Clear();
            txtDetailHoTen.Clear();
            txtDetailKhoa.Clear();
            txtDetailDiemTB.Clear();
            txtDetailEmail.Clear();
            txtDetailTuoi.Clear();
            rdoDetailNam.Checked = false;
            rdoDetailNu.Checked = false;
            picAvatar.Image = null;
        }

        // -------------------------------------------------------------
        // BÀI 9: Định dạng DataGridView (Tô màu điểm < 5 và >= 8)
        // -------------------------------------------------------------
        private void DgvSinhVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSinhVien.Rows.Count) return;

            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "colDiemTB" && e.Value != null && e.Value != DBNull.Value)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal diem))
                {
                    e.Value = diem.ToString("0.00");
                    e.FormattingApplied = true;
                }
            }

            var row = dgvSinhVien.Rows[e.RowIndex];
            object cellVal = row.Cells["colDiemTB"].Value;

            if (cellVal != null && cellVal != DBNull.Value && decimal.TryParse(cellVal.ToString(), out decimal score))
            {
                if (score < 5.0m)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 232, 232);
                    if (dgvSinhVien.Columns[e.ColumnIndex].Name == "colDiemTB")
                    {
                        e.CellStyle.ForeColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(dgvSinhVien.Font, FontStyle.Bold);
                    }
                }
                else if (score >= 8.0m)
                {
                    e.CellStyle.BackColor = Color.FromArgb(232, 250, 232);
                    if (dgvSinhVien.Columns[e.ColumnIndex].Name == "colDiemTB")
                    {
                        e.CellStyle.ForeColor = Color.DarkGreen;
                        e.CellStyle.Font = new Font(dgvSinhVien.Font, FontStyle.Bold);
                    }
                }
            }
        }

        // -------------------------------------------------------------
        // Kịch bản demo kiểm thử các bài tập
        // -------------------------------------------------------------
        private void CboDemoMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cboDemoMode.SelectedIndex;
            if (idx <= 0) return;

            switch (idx)
            {
                case 1: // Bài 1: Nạp toàn bộ
                    ResetFilterControls();
                    _currentPage = 1;
                    _pageSize = 10;
                    cboPageSize.SelectedIndex = 1;
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 1: Nạp SqlDataReader vào DataTable thành công.";
                    break;

                case 2: // Bài 2: Tìm họ tên chứa "Thắng"
                    ResetFilterControls();
                    txtSearchHoTen.Text = "Thắng";
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 2: Tìm kiếm gần đúng họ tên chứa 'Thắng' (SqlParameter @Keyword).";
                    break;

                case 3: // Bài 3: Lọc Khoa CNTT & Điểm 7.0 - 9.0
                    ResetFilterControls();
                    for (int i = 0; i < cboKhoa.Items.Count; i++)
                    {
                        DataRowView drv = cboKhoa.Items[i] as DataRowView;
                        if (drv != null && drv["MaKhoa"].ToString() == "CNTT")
                        {
                            cboKhoa.SelectedIndex = i;
                            break;
                        }
                    }
                    numDiemMin.Value = 7.0m;
                    numDiemMax.Value = 9.0m;
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 3: Lọc theo Khoa CNTT và Điểm TB trong khoảng [7.0, 9.0].";
                    break;

                case 4: // Bài 4: Phân trang OFFSET-FETCH
                    ResetFilterControls();
                    _pageSize = 10;
                    cboPageSize.SelectedIndex = 1;
                    _currentPage = 2;
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 4: Phân trang OFFSET-FETCH trang 2/4 (mỗi trang 10 sinh viên).";
                    break;

                case 5: // Bài 6: Đa tiêu chí (Nam + Điểm >= 7.5)
                    ResetFilterControls();
                    cboGioiTinh.SelectedIndex = 1;
                    numDiemMin.Value = 7.5m;
                    numDiemMax.Value = 10.0m;
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 6: Tìm kiếm đa tiêu chí (Giới tính: Nam, Điểm TB >= 7.5).";
                    break;

                case 6: // Bài 7: Sắp xếp theo Điểm TB giảm dần
                    ResetFilterControls();
                    cboSortCol.SelectedIndex = 2; // DiemTB
                    cboSortDir.SelectedIndex = 1; // DESC
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 7: Sắp xếp an toàn theo Điểm TB giảm dần (Whitelist check).";
                    break;

                case 7: // Bài 8: Lọc tuổi 20 - 22
                    ResetFilterControls();
                    numTuoiMin.Value = 20m;
                    numTuoiMax.Value = 22m;
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 8: Lọc sinh viên theo độ tuổi chính xác từ 20 đến 22 tuổi.";
                    break;

                case 8: // Bài 9: Định dạng và tô màu
                    ResetFilterControls();
                    _currentPage = 1;
                    _pageSize = 10;
                    cboPageSize.SelectedIndex = 1;
                    LoadDataCurrentPage();
                    lblStatus.Text = "Bài 9: Tô màu sinh viên Điểm < 5.0 (hồng/đỏ) và >= 8.0 (xanh).";
                    break;
            }
        }

        public void ApplyPreset(int scenarioIndex)
        {
            if (scenarioIndex >= 1 && scenarioIndex <= 8)
            {
                cboDemoMode.SelectedIndex = scenarioIndex;
            }
        }
    }
}
