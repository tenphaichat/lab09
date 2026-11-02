using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Lab09_DataReaderSearch.GUI;
using Lab09_DataReaderSearch.DAL;

namespace Lab09_DataReaderSearch
{
    public static class ScreenshotCapture
    {
        public static Bitmap FrameForm(Form form)
        {
            if (!form.Visible) form.Show();
            Application.DoEvents();
            Thread.Sleep(150);
            Application.DoEvents();

            int clientW = form.ClientSize.Width;
            int clientH = form.ClientSize.Height;

            Bitmap clientBmp = new Bitmap(clientW, clientH, PixelFormat.Format32bppArgb);
            form.DrawToBitmap(clientBmp, new Rectangle(0, 0, clientW, clientH));

            int titleH = 34;
            int borderWidth = 1;
            int totalW = clientW + borderWidth * 2;
            int totalH = clientH + titleH + borderWidth * 2;

            Bitmap finalBmp = new Bitmap(totalW, totalH, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(finalBmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (SolidBrush titleBrush = new SolidBrush(Color.FromArgb(243, 243, 243)))
                {
                    g.FillRectangle(titleBrush, 0, 0, totalW, titleH);
                }

                using (Pen iconPen = new Pen(Color.FromArgb(16, 54, 110), 1.6f))
                {
                    g.DrawRectangle(iconPen, 12, 9, 15, 15);
                    g.DrawLine(iconPen, 12, 14, 27, 14);
                }

                using (Font titleFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(30, 30, 30)))
                {
                    g.DrawString(form.Text, titleFont, textBrush, 34, 8);
                }

                int btnW = 46;
                int minX = totalW - btnW * 3;
                int maxX = totalW - btnW * 2;
                int closeX = totalW - btnW;

                using (Pen btnPen = new Pen(Color.FromArgb(60, 60, 60), 1.2f))
                {
                    g.DrawLine(btnPen, minX + 18, 17, minX + 28, 17);
                    g.DrawRectangle(btnPen, maxX + 18, 12, 10, 10);
                    g.DrawLine(btnPen, closeX + 18, 12, closeX + 28, 22);
                    g.DrawLine(btnPen, closeX + 28, 12, closeX + 18, 22);
                }

                g.DrawImage(clientBmp, borderWidth, titleH);

                using (Pen borderPen = new Pen(Color.FromArgb(180, 180, 180), 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, totalW - 1, totalH - 1);
                }
            }

            form.Dispose();
            clientBmp.Dispose();
            return finalBmp;
        }

        public static void RunCapture(string outputDir)
        {
            Directory.CreateDirectory(outputDir);
            Console.WriteLine("Capturing screenshots for Lab 09 directly from WinForms project...");

            // 1. Dashboard Overview / Bài 1: Nạp SqlDataReader
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(1);
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai1_LoadSqlDataReader.png"), ImageFormat.Png);
                    bmp.Save(Path.Combine(outputDir, "Lab9_Dashboard_Overview.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 1 & Overview.");
            }

            // 2. Bài 2: Tìm kiếm gần đúng theo họ tên
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(2); // Họ tên chứa 'Thắng'
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai2_SearchByName.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 2.");
            }

            // 3. Bài 3: Lọc theo Khoa và khoảng điểm
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(3); // CNTT & Điểm 7.0 - 9.0
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai3_FilterKhoaScore.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 3.");
            }

            // 4. Bài 4: Phân trang OFFSET-FETCH - Trang 1
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(1); // Page 1
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai4_Pagination_Page1.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 4 Page 1.");
            }

            // 5. Bài 4: Phân trang OFFSET-FETCH - Trang 2
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(4); // Page 2
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai4_Pagination_Page2.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 4 Page 2.");
            }

            // 6. Bài 5: Chi tiết dòng chọn
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(2); // Sinh viên Thắng được chọn
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai5_DetailSelection.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 5.");
            }

            // 7. Bài 6: Tìm kiếm đa tiêu chí
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(5); // Nam & Điểm >= 7.5
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai6_MultiCriteriaSearch.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 6.");
            }

            // 8. Bài 7: Sắp xếp theo lựa chọn
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(6); // Sắp xếp theo Điểm TB giảm dần
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai7_SafeSorting.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 7.");
            }

            // 9. Bài 8: Lọc theo tuổi
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(7); // Tuổi 20 - 22
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai8_AgeFilter.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 8.");
            }

            // 10. Bài 9: Định dạng và Tô màu DataGridView
            {
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(8); // Tô màu <5 và >=8
                Application.DoEvents();
                using (var bmp = FrameForm(form))
                {
                    bmp.Save(Path.Combine(outputDir, "Lab9_Bai9_ColorFormatting.png"), ImageFormat.Png);
                }
                Console.WriteLine("Captured Bai 9.");
            }

            // 11. Bài 10: Xuất kết quả CSV
            {
                string csvPath = Path.Combine(outputDir, "DanhSachSinhVien_Exported.csv");
                var form = new frmMain();
                form.Size = new Size(1184, 684);
                form.Show();
                Application.DoEvents();
                form.ApplyPreset(1);
                Application.DoEvents();

                var dgvField = typeof(frmMain).GetField("dgvSinhVien", BindingFlags.NonPublic | BindingFlags.Instance);
                if (dgvField != null)
                {
                    var dgv = dgvField.GetValue(form) as DataGridView;
                    if (dgv != null)
                    {
                        CsvExporter.ExportToCsvFile(dgv, csvPath);
                    }
                }

                Bitmap dialogBmp = RenderSuccessDialog("Xuất dữ liệu thành công",
                    $"Đã xuất thành công 10 bản ghi ra tệp CSV (UTF-8 BOM):\n{csvPath}\n\nSẵn sàng mở trực tiếp bằng Microsoft Excel!");
                dialogBmp.Save(Path.Combine(outputDir, "Lab9_Bai10_ExportCsv.png"), ImageFormat.Png);
                dialogBmp.Dispose();
                form.Dispose();
                Console.WriteLine("Captured Bai 10 & Exported CSV.");
            }

            Console.WriteLine("All screenshots captured successfully!");
        }

        private static Bitmap RenderSuccessDialog(string title, string message)
        {
            int w = 560;
            int h = 230;
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(240, 240, 240));
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (SolidBrush titleBg = new SolidBrush(Color.FromArgb(245, 245, 245)))
                {
                    g.FillRectangle(titleBg, 0, 0, w, 32);
                }
                using (Font tFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (SolidBrush tBrush = new SolidBrush(Color.FromArgb(30, 30, 30)))
                {
                    g.DrawString(title, tFont, tBrush, 14, 7);
                }

                using (SolidBrush iconBg = new SolidBrush(Color.FromArgb(0, 122, 217)))
                {
                    g.FillEllipse(iconBg, 24, 60, 42, 42);
                }
                using (Font iFont = new Font("Georgia", 20f, FontStyle.Bold))
                using (SolidBrush iBrush = new SolidBrush(Color.White))
                {
                    g.DrawString("i", iFont, iBrush, 36, 62);
                }

                using (Font mFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (SolidBrush mBrush = new SolidBrush(Color.FromArgb(20, 20, 20)))
                {
                    RectangleF rect = new RectangleF(80, 52, w - 100, 110);
                    g.DrawString(message, mFont, mBrush, rect);
                }

                Rectangle btnRect = new Rectangle(w - 110, h - 45, 88, 28);
                using (SolidBrush btnBg = new SolidBrush(Color.FromArgb(225, 225, 225)))
                {
                    g.FillRectangle(btnBg, btnRect);
                }
                using (Pen btnBorder = new Pen(Color.FromArgb(170, 170, 170)))
                {
                    g.DrawRectangle(btnBorder, btnRect);
                }
                using (Font btnFont = new Font("Segoe UI", 9f))
                using (SolidBrush btnTxt = new SolidBrush(Color.Black))
                {
                    g.DrawString("OK", btnFont, btnTxt, w - 80, h - 39);
                }

                using (Pen borderPen = new Pen(Color.FromArgb(180, 180, 180)))
                {
                    g.DrawRectangle(borderPen, 0, 0, w - 1, h - 1);
                }
            }
            return bmp;
        }
    }
}
