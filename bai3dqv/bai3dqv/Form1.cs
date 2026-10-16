using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bai3dqv
{
    // ================= FORM CHA MDI =================
    public partial class Form1 : Form
    {
        public ToolStripStatusLabel lblSync = new ToolStripStatusLabel("Đồng bộ: chưa chạy");
        public ToolStripProgressBar bar = new ToolStripProgressBar { Style = ProgressBarStyle.Marquee, Visible = false };

        public Form1()
        {
            InitializeComponent();
            Text = "QUẢN LÝ SIÊU THỊ (MDI)";
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;

            // Menu
            var menu = new MenuStrip();
            var heThong = new ToolStripMenuItem("Hệ thống");
            heThong.DropDownItems.Add("Đăng xuất", null, (s, e) => Close());
            heThong.DropDownItems.Add("Thoát", null, (s, e) => Application.Exit());

            var danhMuc = new ToolStripMenuItem("Danh mục");
            danhMuc.DropDownItems.Add("Quản lý Sản phẩm", null, (s, e) => OpenChild<frmProductCatalog>());
            danhMuc.DropDownItems.Add("Quản lý Nhập xuất", null, (s, e) => MessageBox.Show("Sẽ làm ở bài sau"));

            var baoCao = new ToolStripMenuItem("Báo cáo");
            baoCao.DropDownItems.Add("Thống kê Doanh thu", null, (s, e) => OpenChild<frmAsyncReport>());

            var cuaSo = new ToolStripMenuItem("Cửa sổ");
            cuaSo.DropDownItems.Add("Cascade", null, (s, e) => LayoutMdi(MdiLayout.Cascade));
            cuaSo.DropDownItems.Add("Tile Horizontal", null, (s, e) => LayoutMdi(MdiLayout.TileHorizontal));
            cuaSo.DropDownItems.Add("Tile Vertical", null, (s, e) => LayoutMdi(MdiLayout.TileVertical));

            menu.Items.AddRange(new ToolStripItem[] { heThong, danhMuc, baoCao, cuaSo });
            MainMenuStrip = menu;

            // StatusStrip: tên user + đồng bộ + ProgressBar
            var status = new StatusStrip();
            status.Items.Add(new ToolStripStatusLabel("Người dùng: admin") { Spring = true, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            status.Items.Add(lblSync);
            status.Items.Add(bar);

            Controls.Add(status);
            Controls.Add(menu);
        }

        // Scenario A: mở form con, nếu đã mở thì Activate
        void OpenChild<T>() where T : Form, new()
        {
            var f = MdiChildren.OfType<T>().FirstOrDefault();
            if (f != null) { f.Activate(); return; }
            new T { MdiParent = this }.Show();
        }
    }

    // ================= FORM CON 1: SẢN PHẨM =================
    public class Product
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public decimal Gia { get; set; }
    }

    public class frmProductCatalog : Form
    {
        BindingList<Product> ds = new BindingList<Product>();
        TextBox txtMa = new TextBox { Left = 10, Top = 10, Width = 80, PlaceholderText = "Mã" };
        TextBox txtTen = new TextBox { Left = 100, Top = 10, Width = 200, PlaceholderText = "Tên" };
        TextBox txtGia = new TextBox { Left = 310, Top = 10, Width = 80, PlaceholderText = "Giá" };
        Button btnThem = new Button { Text = "Thêm", Left = 400, Top = 9 };
        Button btnXoa = new Button { Text = "Xóa", Left = 485, Top = 9 };
        DataGridView grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };

        public frmProductCatalog()
        {
            Text = "Quản lý Sản phẩm";
            Size = new System.Drawing.Size(620, 400);
            ds.Add(new Product { Ma = "SP001", Ten = "Sữa tươi 1L", Gia = 32000 });
            ds.Add(new Product { Ma = "SP002", Ten = "Gạo ST25 5kg", Gia = 140000 });
            grid.DataSource = ds;

            var pnl = new Panel { Dock = DockStyle.Top, Height = 45 };
            pnl.Controls.AddRange(new Control[] { txtMa, txtTen, txtGia, btnThem, btnXoa });
            Controls.Add(grid);
            Controls.Add(pnl);

            btnThem.Click += (s, e) =>
            {
                if (!decimal.TryParse(txtGia.Text, out var gia)) { MessageBox.Show("Giá không hợp lệ"); return; }
                ds.Add(new Product { Ma = txtMa.Text, Ten = txtTen.Text, Gia = gia });
            };
            btnXoa.Click += (s, e) =>
            {
                if (grid.CurrentRow?.DataBoundItem is Product p) ds.Remove(p);
            };
        }
    }

    // ================= FORM CON 2: BÁO CÁO + ĐỒNG BỘ =================
    public class frmAsyncReport : Form
    {
        Button btnLoad = new Button { Text = "Tải Báo Cáo Doanh Thu", Left = 10, Top = 8, Width = 170 };
        Button btnStart = new Button { Text = "Bắt đầu đồng bộ", Left = 190, Top = 8, Width = 120 };
        Button btnStop = new Button { Text = "Dừng", Left = 320, Top = 8, Width = 60, Enabled = false };
        Label lblInfo = new Label { Left = 390, Top = 13, Width = 350, Text = "Sẵn sàng" };
        DataGridView gridReport = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
        DataGridView gridSync = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
        CancellationTokenSource cts;

        public frmAsyncReport()
        {
            Text = "Báo cáo Thống kê & Đồng bộ";
            Size = new System.Drawing.Size(780, 520);

            gridReport.Columns.Add("c1", "Tháng");
            gridReport.Columns.Add("c2", "Doanh thu");
            gridSync.Columns.Add("c1", "Giờ");
            gridSync.Columns.Add("c2", "Tỷ giá USD");
            gridSync.Columns.Add("c3", "Đơn mới");
            gridReport.AutoSizeColumnsMode = gridSync.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };
            split.Panel1.Controls.Add(gridReport);
            split.Panel2.Controls.Add(gridSync);

            var top = new Panel { Dock = DockStyle.Top, Height = 40 };
            top.Controls.AddRange(new Control[] { btnLoad, btnStart, btnStop, lblInfo });
            Controls.Add(split);
            Controls.Add(top);

            btnLoad.Click += BtnLoad_Click;
            btnStart.Click += (s, e) => StartSync();
            btnStop.Click += (s, e) => StopSync();
            FormClosing += (s, e) => cts?.Cancel(); // đóng form thì dừng thread ngầm
        }

        // ---------- Scenario B: async/await + Task.Run ----------
        async void BtnLoad_Click(object sender, EventArgs e)
        {
            var main = MdiParent as Form1;
            btnLoad.Enabled = false;
            gridReport.Rows.Clear();
            lblInfo.Text = "Đang tải báo cáo...";
            if (main != null) main.bar.Visible = true;          // bật ProgressBar

            // Việc nặng chạy ở thread phụ, UI vẫn mượt
            var data = await Task.Run(() =>
            {
                var rnd = new Random();
                var list = new (string, decimal)[12];
                for (int i = 0; i < 12; i++)
                {
                    Thread.Sleep(350);                            // giả lập truy xuất CSDL ~4 giây
                    list[i] = ("Tháng " + (i + 1), rnd.Next(300, 900) * 1_000_000m);
                }
                return list;
            });

            // Sau await tự quay lại UI Thread nên gán trực tiếp được
            foreach (var r in data) gridReport.Rows.Add(r.Item1, r.Item2.ToString("N0"));
            lblInfo.Text = "Tải xong lúc " + DateTime.Now.ToString("HH:mm:ss");
            if (main != null) main.bar.Visible = false;          // tắt ProgressBar
            btnLoad.Enabled = true;
        }

        // ---------- Scenario C: thread phụ + Invoke ----------
        void StartSync()
        {
            cts = new CancellationTokenSource();
            var token = cts.Token;
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            Task.Run(async () =>
            {
                var rnd = new Random();
                int rate = 25000;
                while (!token.IsCancellationRequested)
                {
                    try { await Task.Delay(2000, token); } catch { break; }
                    rate += rnd.Next(-30, 31);
                    UpdateUi(rate, rnd.Next(0, 8));              // thread phụ KHÔNG đụng Control trực tiếp
                }
            });
        }

        void StopSync()
        {
            cts?.Cancel();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            (MdiParent as Form1).lblSync.Text = "Đồng bộ: đã dừng";
        }

        void UpdateUi(int rate, int orders)
        {
            if (IsDisposed) return;
            if (InvokeRequired)                                   // đang ở thread phụ -> chuyển về UI Thread
            {
                try { Invoke(new Action(() => UpdateUi(rate, orders))); } catch { }
                return;
            }
            string time = DateTime.Now.ToString("HH:mm:ss");
            gridSync.Rows.Add(time, rate.ToString("N0"), orders);
            gridSync.FirstDisplayedScrollingRowIndex = gridSync.Rows.Count - 1;
            lblInfo.Text = $"USD/VND: {rate:N0} | +{orders} đơn";
            (MdiParent as Form1).lblSync.Text = $"Đồng bộ {time} | USD {rate:N0}";
        }
    }
}