using HKDashboard.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WinformLeGroup.Models.CommonModel;
using WinformLeGroup.Models.Dashboard;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HKDashboard.Forms
{
    public partial class frmDashboardSLSX : Form
    {
        private System.Windows.Forms.Timer tRefreshData;
        public frmDashboardSLSX()
        {
            InitializeComponent();
        }

        private void frmDashboardSLSX_Load(object sender, EventArgs e)
        {
            int y = Screen.PrimaryScreen.Bounds.Bottom -100;
            this.Location = new Point(0, y);
            this.TopMost = true;
            string apppath = Application.StartupPath;
            //if (comboBox2.SelectedIndex < 0)
            //    return;
            List<Data_BoPhan> dsDivision = frmDashboardSLSXServices.getAllDivision();
            Data_BoPhan _DMBoPhan = new Data_BoPhan();
            _DMBoPhan.name = "-----";
            _DMBoPhan.id = -1;
            dsDivision.Insert(0, _DMBoPhan);
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "name";
            comboBox2.DataSource = dsDivision;
            comboBox2.SelectedIndex = 0;

            //Left group
            Color cl = Color.FromArgb(146, 205, 220);
            txtSoLuongPOKeHoach.BackColor = cl;
            txtSoLuongPOKeHoach.Text = "";
            cl = Color.FromArgb(249, 196, 153);
            txtSoLuongPOHoanThanh.BackColor = cl;
            txtSoLuongPOHoanThanh.Text = "";
            cl = Color.FromArgb(255, 217, 101);
            txtTiLeHoanThanhPO.BackColor = cl;
            txtTiLeHoanThanhPO.Text = "";
            cl = Color.FromArgb(255, 0, 0);
            txtTongLoiNG.BackColor = cl;
            txtTongLoiNG.Text = "";
            picSoLuongPOKeHoach.Load(apppath + @"\img\DashboardSLSX\SoLuongPOKH.jpg");
            picSoLuongPOHoanThanh.Load(apppath + @"\img\DashboardSLSX\SoLuongPOHoanThanh.jpg");
            picTiLeHoanThanhPO.Load(apppath + @"\img\DashboardSLSX\TiLeHoanThanhPO.jpg");
            picTongLoiNG.Load(apppath + @"\img\DashboardSLSX\TongLoiNG.jpg");
            gr01.Paint += PaintBorderlessGroupBox;
            gr02.Paint += PaintBorderlessGroupBox;
            gr03.Paint += PaintBorderlessGroupBox;
            gr04.Paint += PaintBorderlessGroupBox;

            //Righ group
            cl = Color.FromArgb(146, 205, 220);
            txtSanLuongKeHoach.BackColor = cl;
            txtSanLuongKeHoach.Text = "";
            cl = Color.FromArgb(249, 196, 153);
            txtSanLuongThucHien.BackColor = cl;
            txtSanLuongThucHien.Text = "";
            cl = Color.FromArgb(255, 217, 101);
            txtTiLeDatSanLuong.BackColor = cl;
            txtTiLeDatSanLuong.Text = "";
            cl = Color.FromArgb(255, 0, 0);
            txtTiLeLoiNG.BackColor = cl;
            txtTiLeLoiNG.Text = "";
            picSanLuongKeHoach.Load(apppath + @"\img\DashboardSLSX\SoLuongPOKH.jpg");
            picSanLuongThucHien.Load(apppath + @"\img\DashboardSLSX\SoLuongPOHoanThanh.jpg");
            picTiLeDatSanLuong.Load(apppath + @"\img\DashboardSLSX\TiLeHoanThanhPO.jpg");
            picTiLeLoiNG.Load(apppath + @"\img\DashboardSLSX\TongLoiNG.jpg");
            gr05.Paint += PaintBorderlessGroupBox;
            gr06.Paint += PaintBorderlessGroupBox;
            gr07.Paint += PaintBorderlessGroupBox;
            gr08.Paint += PaintBorderlessGroupBox;

            for (int i = 0; i < lvPOData.Columns.Count; i++)
                lvPOData.Columns[i].TextAlign = HorizontalAlignment.Center;

            for (int i = 0; i < lvDetailOperation.Columns.Count; i++)
                lvDetailOperation.Columns[i].TextAlign = HorizontalAlignment.Center;

            tRefreshData = new System.Windows.Forms.Timer();
            tRefreshData.Tick += new EventHandler(tRefreshData_Tick);
            tRefreshData.Interval = 15000;
            tRefreshData.Enabled = true;

            //load dữ liệu lần đầu tiên
            tRefreshData_Tick(null, null);
            this.CenterToScreen();
        }

        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);
            Color borderColor = Color.Black;
            Brush borderBrush = new SolidBrush(borderColor);
            Pen borderPen = new Pen(borderBrush);
            //string - if have
            p.Graphics.DrawString("", box.Font, Brushes.Black, 0, 0);

            Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                           box.ClientRectangle.Y,
                                           box.ClientRectangle.Width - 1,
                                           box.ClientRectangle.Height - 1);
            ////Left
            //p.Graphics.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
            ////Right
            //p.Graphics.DrawLine(borderPen, new Point(rect.X + rect.Width + box.Padding.Right + 2, rect.Y), new Point(rect.X + rect.Width + box.Padding.Right + 2, rect.Y + rect.Height + 1000));
            ////Bottom
            //p.Graphics.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            ////Top
            //p.Graphics.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y));
            p.Graphics.DrawRectangle(borderPen, rect);
        }

        public void tRefreshData_Tick(object sender, EventArgs e)
        {
            double soPOKH = 0, soPOHoanThanh = 0, sanLuongKH = 0, sanluongTT = 0, tongSoNG = 0, tyle = 0;
            Hashtable dbCongDoan = new Hashtable();
            Hashtable ttThanhPham = new Hashtable();
            DashboardCongDoan congDoan;
            TTThanhPham thanhPham;
            //string division_id = null;

            List<Data_LineSX> dsChuyen = (List<Data_LineSX>)comboBox1.DataSource;
            Data_LineSX chuyen = dsChuyen[comboBox1.SelectedIndex];
            List<KHNgayModel> dataKHNgay = frmDashboardSLSXServices.getKHNgayData(DateTime.Now, chuyen.id);
            ListViewItem it;


            //Thống kê phân loại dữ liệu
            for (int i = 0; i < dataKHNgay.Count; i++)
            {
                if (!dbCongDoan.Contains(dataKHNgay[i].maCongDoan))
                {
                    congDoan = new DashboardCongDoan();
                    congDoan.maCongDoan = dataKHNgay[i].maCongDoan;
                    congDoan.tenCongDoan = dataKHNgay[i].tenCongDoan;
                    congDoan.sanLuongKH = dataKHNgay[i].SoLuongKHNgay;
                    dbCongDoan.Add(congDoan.maCongDoan, congDoan);
                }
                else
                    congDoan = (DashboardCongDoan)dbCongDoan[dataKHNgay[i].maCongDoan];

                congDoan.sanLuongSX += dataKHNgay[i].SoThucTe;

                //Thống kê nếu công đoạn là công đoạn cuối - xuất ra thành phẩm
                if (dataKHNgay[i].MaThanhPham.Equals(dataKHNgay[i].MaHangHoa))
                {
                    if (!ttThanhPham.Contains(dataKHNgay[i].SoLSX + "_" + dataKHNgay[i].MaThanhPham))
                    {
                        thanhPham = new TTThanhPham();
                        thanhPham.MaHangHoa = dataKHNgay[i].MaThanhPham;
                        thanhPham.TenHangHoa = dataKHNgay[i].TenThanhPham;
                        thanhPham.SoPO = dataKHNgay[i].SoLSX;
                        thanhPham.SoLuongKH = dataKHNgay[i].SoLuongKHNgay;
                        ttThanhPham.Add(thanhPham.SoPO + "_" + thanhPham.MaHangHoa, thanhPham);
                    }
                    else
                        thanhPham = (TTThanhPham)ttThanhPham[dataKHNgay[i].SoLSX + "_" + dataKHNgay[i].MaThanhPham];

                    thanhPham.SoLuongTT += dataKHNgay[i].SoThucTe;
                }
            }

            List<NGData> ngData = frmDashboardSLSXServices.getAllNG(DateTime.Now, "0");
            for (int i = 0; i < ngData.Count; i++)
            {
                if (dbCongDoan.Contains(ngData[i].maCongDoan))
                    congDoan = (DashboardCongDoan)dbCongDoan[ngData[i].maCongDoan];
                else
                {
                    congDoan = new DashboardCongDoan();
                    congDoan.maCongDoan = ngData[i].maCongDoan;
                    congDoan.tenCongDoan = ngData[i].tenCongDoan;
                    congDoan.sanLuongKH = 0;
                    congDoan.sanLuongSX = 0;
                    dbCongDoan.Add(congDoan.maCongDoan, congDoan);
                }
                congDoan.soLoiNG = Convert.ToInt32(Math.Floor(ngData[i].soLoi));

                if (ttThanhPham.Contains(ngData[i].soLSX + "_" + ngData[i].maHangHoa))
                    thanhPham = (TTThanhPham)ttThanhPham[ngData[i].soLSX + "_" + ngData[i].maHangHoa];
                else
                {
                    thanhPham = new TTThanhPham();
                    thanhPham.MaHangHoa = ngData[i].maHangHoa;
                    thanhPham.TenHangHoa = ngData[i].tenHangHoa;
                    thanhPham.SoPO = ngData[i].soLSX;
                    thanhPham.SoLuongKH = 0;
                    thanhPham.SoLuongTT = 0;
                    ttThanhPham.Add(thanhPham.SoPO + "_" + thanhPham.MaHangHoa, thanhPham);
                }
                thanhPham.SoLuongNG = Convert.ToInt32(Math.Floor(ngData[i].soLoi));
            }

            //tải dữ liệu lên các lưới công đoạn
            lvDetailOperation.Items.Clear();
            List<DashboardCongDoan> dsCongDoan = dbCongDoan.Values.OfType<DashboardCongDoan>().ToList();
            for (int i = 0; i < dsCongDoan.Count; i++)
            {
                it = new ListViewItem(dsCongDoan[i].tenCongDoan);
                it.SubItems.Add(dsCongDoan[i].soLoiNG.ToString());
                it.SubItems.Add(dsCongDoan[i].sanLuongSX.ToString());
                it.SubItems.Add(dsCongDoan[i].tyLe.ToString() + "%");
                lvDetailOperation.Items.Add(it);
            }

            //tải dữ liệu lên các lưới lệnh sản xuất
            lvPOData.Items.Clear();
            List<TTThanhPham> dsTTThanhPham = ttThanhPham.Values.OfType<TTThanhPham>().ToList();
            for (int i = 0; i < dsTTThanhPham.Count; i++)
            {
                it = new ListViewItem(dsTTThanhPham[i].SoPO);
                it.SubItems.Add(dsTTThanhPham[i].MaHangHoa);
                //it.SubItems.Add(dsTTThanhPham[i].TenHangHoa);
                it.SubItems.Add(dsTTThanhPham[i].SoLuongKH.ToString());
                it.SubItems.Add(dsTTThanhPham[i].SoLuongTT.ToString());
                it.SubItems.Add(dsTTThanhPham[i].SoLuongNG.ToString());
                it.SubItems.Add(dsTTThanhPham[i].TinhTrang);
                lvPOData.Items.Add(it);
                soPOKH += 1;
                sanLuongKH += dsTTThanhPham[i].SoLuongKH;
                sanluongTT += dsTTThanhPham[i].SoLuongTT;
                switch (dsTTThanhPham[i].TinhTrang)
                {
                    case "Hoàn thành":
                        soPOHoanThanh += 1;
                        break;
                }
                tongSoNG += dsTTThanhPham[i].SoLuongNG;
            }

            //Tải dữ liệu lên các text box
            txtSoLuongPOKeHoach.Text = soPOKH.ToString();
            txtSoLuongPOHoanThanh.Text = soPOHoanThanh.ToString();
            tyle = soPOKH == 0 ? 0 : soPOHoanThanh * 100 / soPOKH;
            txtTiLeHoanThanhPO.Text = string.Format("{0:N2}%", tyle);
            txtTongLoiNG.Text = tongSoNG.ToString();

            txtSanLuongKeHoach.Text = sanLuongKH.ToString();
            txtSanLuongThucHien.Text = sanluongTT.ToString();
            tyle = sanLuongKH == 0 ? 0 : sanluongTT * 100 / sanLuongKH;
            txtTiLeDatSanLuong.Text = string.Format("{0:N2}%", tyle);
            tyle = sanluongTT == 0 ? 0 : tongSoNG * 100 / sanluongTT;
            txtTiLeLoiNG.Text = string.Format("{0:N2}%", tyle);

            lvDetailOperation.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvDetailOperation.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvPOData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvPOData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void frmDashboardSLSX_FormClosed(object sender, FormClosedEventArgs e)
        {
            List<Data_LineSX> dsChuyen1 = (List<Data_LineSX>)comboBox1.DataSource;
            Data_LineSX chuyen = dsChuyen1[comboBox1.SelectedIndex];
            tRefreshData.Tick -= tRefreshData_Tick;
            tRefreshData.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
                return;
            List<Data_LineSX> dsChuyen = (List<Data_LineSX>)comboBox1.DataSource;
            Data_LineSX chuyen = dsChuyen[comboBox1.SelectedIndex];


            double soPOKH = 0, soPOHoanThanh = 0, sanLuongKH = 0, sanluongTT = 0, tongSoNG = 0, tyle = 0;
            Hashtable dbCongDoan = new Hashtable();
            Hashtable ttThanhPham = new Hashtable();
            DashboardCongDoan congDoan;
            TTThanhPham thanhPham;
            //string division_id = null;

            var xx = comboBox1.SelectedValue;
            List<KHNgayModel> dataKHNgay = frmDashboardSLSXServices.getKHNgayData(DateTime.Now, chuyen.division_id);
            ListViewItem it;


            //Thống kê phân loại dữ liệu
            for (int i = 0; i < dataKHNgay.Count; i++)
            {
                if (!dbCongDoan.Contains(dataKHNgay[i].maCongDoan))
                {
                    congDoan = new DashboardCongDoan();
                    congDoan.maCongDoan = dataKHNgay[i].maCongDoan;
                    congDoan.tenCongDoan = dataKHNgay[i].tenCongDoan;
                    congDoan.sanLuongKH = dataKHNgay[i].SoLuongKHNgay;
                    dbCongDoan.Add(congDoan.maCongDoan, congDoan);
                }
                else
                    congDoan = (DashboardCongDoan)dbCongDoan[dataKHNgay[i].maCongDoan];

                congDoan.sanLuongSX += dataKHNgay[i].SoThucTe;

                //Thống kê nếu công đoạn là công đoạn cuối - xuất ra thành phẩm
                if (dataKHNgay[i].MaThanhPham.Equals(dataKHNgay[i].MaHangHoa))
                {
                    if (!ttThanhPham.Contains(dataKHNgay[i].SoLSX + "_" + dataKHNgay[i].MaThanhPham))
                    {
                        thanhPham = new TTThanhPham();
                        thanhPham.MaHangHoa = dataKHNgay[i].MaThanhPham;
                        thanhPham.TenHangHoa = dataKHNgay[i].TenThanhPham;
                        thanhPham.SoPO = dataKHNgay[i].SoLSX;
                        thanhPham.SoLuongKH = dataKHNgay[i].SoLuongKHNgay;
                        ttThanhPham.Add(thanhPham.SoPO + "_" + thanhPham.MaHangHoa, thanhPham);
                    }
                    else
                        thanhPham = (TTThanhPham)ttThanhPham[dataKHNgay[i].SoLSX + "_" + dataKHNgay[i].MaThanhPham];

                    thanhPham.SoLuongTT += dataKHNgay[i].SoThucTe;
                }
            }

            List<NGData> ngData = frmDashboardSLSXServices.getAllNG(DateTime.Now, chuyen.division_id.ToString());
            for (int i = 0; i < ngData.Count; i++)
            {
                if (dbCongDoan.Contains(ngData[i].maCongDoan))
                    congDoan = (DashboardCongDoan)dbCongDoan[ngData[i].maCongDoan];
                else
                {
                    congDoan = new DashboardCongDoan();
                    congDoan.maCongDoan = ngData[i].maCongDoan;
                    congDoan.tenCongDoan = ngData[i].tenCongDoan;
                    congDoan.sanLuongKH = 0;
                    congDoan.sanLuongSX = 0;
                    dbCongDoan.Add(congDoan.maCongDoan, congDoan);
                }
                congDoan.soLoiNG = Convert.ToInt32(Math.Floor(ngData[i].soLoi));

                if (ttThanhPham.Contains(ngData[i].soLSX + "_" + ngData[i].maHangHoa))
                    thanhPham = (TTThanhPham)ttThanhPham[ngData[i].soLSX + "_" + ngData[i].maHangHoa];
                else
                {
                    thanhPham = new TTThanhPham();
                    thanhPham.MaHangHoa = ngData[i].maHangHoa;
                    thanhPham.TenHangHoa = ngData[i].tenHangHoa;
                    thanhPham.SoPO = ngData[i].soLSX;
                    thanhPham.SoLuongKH = 0;
                    thanhPham.SoLuongTT = 0;
                    ttThanhPham.Add(thanhPham.SoPO + "_" + thanhPham.MaHangHoa, thanhPham);
                }
                thanhPham.SoLuongNG = Convert.ToInt32(Math.Floor(ngData[i].soLoi));
            }

            //tải dữ liệu lên các lưới công đoạn
            lvDetailOperation.Items.Clear();
            List<DashboardCongDoan> dsCongDoan = dbCongDoan.Values.OfType<DashboardCongDoan>().ToList();
            for (int i = 0; i < dsCongDoan.Count; i++)
            {
                it = new ListViewItem(dsCongDoan[i].tenCongDoan);
                it.SubItems.Add(dsCongDoan[i].soLoiNG.ToString());
                it.SubItems.Add(dsCongDoan[i].sanLuongSX.ToString());
                it.SubItems.Add(dsCongDoan[i].tyLe.ToString() + "%");
                lvDetailOperation.Items.Add(it);
            }

            //tải dữ liệu lên các lưới lệnh sản xuất
            lvPOData.Items.Clear();
            List<TTThanhPham> dsTTThanhPham = ttThanhPham.Values.OfType<TTThanhPham>().ToList();
            for (int i = 0; i < dsTTThanhPham.Count; i++)
            {
                it = new ListViewItem(dsTTThanhPham[i].SoPO);
                it.SubItems.Add(dsTTThanhPham[i].MaHangHoa);
                //it.SubItems.Add(dsTTThanhPham[i].TenHangHoa);
                it.SubItems.Add(dsTTThanhPham[i].SoLuongKH.ToString());
                it.SubItems.Add(dsTTThanhPham[i].SoLuongTT.ToString());
                it.SubItems.Add(dsTTThanhPham[i].SoLuongNG.ToString());
                it.SubItems.Add(dsTTThanhPham[i].TinhTrang);
                lvPOData.Items.Add(it);
                soPOKH += 1;
                sanLuongKH += dsTTThanhPham[i].SoLuongKH;
                sanluongTT += dsTTThanhPham[i].SoLuongTT;
                switch (dsTTThanhPham[i].TinhTrang)
                {
                    case "Hoàn thành":
                        soPOHoanThanh += 1;
                        break;
                }
                tongSoNG += dsTTThanhPham[i].SoLuongNG;
            }

            //Tải dữ liệu lên các text box
            txtSoLuongPOKeHoach.Text = soPOKH.ToString();
            txtSoLuongPOHoanThanh.Text = soPOHoanThanh.ToString();
            tyle = soPOKH == 0 ? 0 : soPOHoanThanh * 100 / soPOKH;
            txtTiLeHoanThanhPO.Text = string.Format("{0:N2}%", tyle);
            txtTongLoiNG.Text = tongSoNG.ToString();

            txtSanLuongKeHoach.Text = sanLuongKH.ToString();
            txtSanLuongThucHien.Text = sanluongTT.ToString();
            tyle = sanLuongKH == 0 ? 0 : sanluongTT * 100 / sanLuongKH;
            txtTiLeDatSanLuong.Text = string.Format("{0:N2}%", tyle);
            tyle = sanluongTT == 0 ? 0 : tongSoNG * 100 / sanluongTT;
            txtTiLeLoiNG.Text = string.Format("{0:N2}%", tyle);

            lvDetailOperation.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvDetailOperation.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvPOData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvPOData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0)
                return;
            List<Data_BoPhan> dsDivision = (List<Data_BoPhan>)comboBox2.DataSource;
            Data_BoPhan boPhan = dsDivision[comboBox2.SelectedIndex];

            comboBox1.Text = "";
            comboBox1.SelectedItem = null;

            List<Data_LineSX> dsChuyen = frmDashboardSLSXServices.getAllProductionLines(boPhan.id.ToString());
            Data_LineSX _DMDayChuyenSX = new Data_LineSX();

            _DMDayChuyenSX.name = "-----";
            _DMDayChuyenSX.id = -1;
            dsChuyen.Insert(0, _DMDayChuyenSX);
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = dsChuyen;
            if (dsChuyen.Count > 0)
                comboBox1.SelectedIndex = 0;

            


        }
        //private void SetText(string text)
        //{
        //    try
        //    {
        //        // InvokeRequired required compares the thread ID of the
        //        // calling thread to the thread ID of the creating thread.
        //        // If these threads are different, it returns true.
        //        if (this.txtTSKProductionTime.InvokeRequired)
        //        {
        //            SetTextCallback d = new SetTextCallback(SetText);
        //            this.Invoke(d, new object[] { text });
        //        }
        //        else
        //        {
        //            this.txtTSKProductionTime.Text = text;
        //        }
        //    }
        //    catch (Exception ex)
        //    { }

        //}
    }
}
