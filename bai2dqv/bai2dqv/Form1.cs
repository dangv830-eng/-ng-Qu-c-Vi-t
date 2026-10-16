using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bai2dqv
{
    public class Product
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = "";          // chỉ lưu đường dẫn ảnh
        public decimal Total => Quantity * Price;            // thành tiền
    }

    public partial class Form1 : Form
    {
        BindingList<Product> ds = new BindingList<Product>();   // danh sách sản phẩm
        string anhHienTai = "";                                 // đường dẫn ảnh đang chọn
        const char SEP = ';';                                   // dấu phân cách CSV

        // ---- các control ----
        TextBox txtId = new TextBox(), txtName = new TextBox(), txtPrice = new TextBox();
        ComboBox cboCategory = new ComboBox();
        NumericUpDown nudQuantity = new NumericUpDown();
        PictureBox picProductImage = new PictureBox();
        DataGridView dgvProducts = new DataGridView();
        ToolStripStatusLabel lblCount = new ToolStripStatusLabel(),
                             lblTotalValue = new ToolStripStatusLabel(),
                             lblTime = new ToolStripStatusLabel();

        public Form1()
        {
            InitializeComponent();
            Text = "Quản lý kho hàng";
            ClientSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            // ----- nhập liệu -----
            Nhan("Mã SP:", 20, 80); Them(txtId, 100, 78);
            Nhan("Tên SP:", 20, 115); Them(txtName, 100, 113);
            Nhan("Danh mục:", 20, 150); Them(cboCategory, 100, 148);
            Nhan("Số lượng:", 20, 185); Them(nudQuantity, 100, 183);
            Nhan("Đơn giá:", 20, 220); Them(txtPrice, 100, 218);
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Items.AddRange(new object[] { "Điện tử", "Gia dụng", "Thực phẩm", "Khác" });
            cboCategory.SelectedIndex = 0;
            nudQuantity.Maximum = 1000000;

            // ----- nút -----
            Nut("Thêm", 280, 78, btnAdd_Click);
            Nut("Sửa", 280, 113, btnUpdate_Click);
            Nut("Xóa", 280, 148, btnDelete_Click);
            Nut("Làm mới", 280, 183, (s, e) => XoaForm());

            // ----- ảnh -----
            Them(picProductImage, 400, 78, 160, 160);
            picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            Nut("Chọn ảnh", 400, 245, btnChooseImage_Click);

            // ----- bảng dữ liệu -----
            Them(dgvProducts, 10, 285, 980, 335);
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.MultiSelect = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.DataSource = ds;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;

            // ----- StatusStrip -----
            var status = new StatusStrip();
            lblTime.Spring = true;
            lblTime.TextAlign = ContentAlignment.MiddleRight;
            status.Items.AddRange(new ToolStripItem[] { lblCount, lblTotalValue, lblTime });
            Controls.Add(status);

            // ----- ToolStrip (nút có icon) -----
            var tool = new ToolStrip();
            tool.Items.Add(new ToolStripButton("Open", Icon("📂"), ImportCsv));
            tool.Items.Add(new ToolStripButton("Save", Icon("💾"), ExportCsv));
            tool.Items.Add(new ToolStripButton("Import", Icon("📥"), ImportCsv));
            tool.Items.Add(new ToolStripButton("Export", Icon("📤"), ExportCsv));
            Controls.Add(tool);

            // ----- MenuStrip -----
            var menu = new MenuStrip();
            var mSystem = new ToolStripMenuItem("System");
            mSystem.DropDownItems.Add(new ToolStripMenuItem("Open File", null, ImportCsv));
            mSystem.DropDownItems.Add(new ToolStripMenuItem("Save Report", null, ExportCsv));
            mSystem.DropDownItems.Add(new ToolStripMenuItem("Exit", null, (s, e) => Close()));
            var mTools = new ToolStripMenuItem("Tools");
            mTools.DropDownItems.Add(new ToolStripMenuItem("Import CSV", null, ImportCsv));
            mTools.DropDownItems.Add(new ToolStripMenuItem("Export CSV", null, ExportCsv));
            menu.Items.Add(mSystem);
            menu.Items.Add(mTools);
            Controls.Add(menu);

            // ----- đồng hồ + cập nhật StatusStrip -----
            var timerClock = new System.Windows.Forms.Timer { Interval = 1000 };
            timerClock.Tick += (s, e) => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timerClock.Start();

            ds.ListChanged += (s, e) => CapNhatStatus();   // Thêm/Sửa/Xóa/Import đều chạy vào đây
            CapNhatStatus();

            FormClosing += Form1_FormClosing;
        }

        // ================= HÀM HỖ TRỢ DỰNG GIAO DIỆN =================
        void Them(Control c, int x, int y, int w = 150, int h = 25) { c.SetBounds(x, y, w, h); Controls.Add(c); }
        void Nhan(string text, int x, int y) { Them(new Label { Text = text }, x, y, 75, 20); }
        void Nut(string text, int x, int y, EventHandler click)
        {
            var b = new Button { Text = text };
            b.Click += click;
            Them(b, x, y, 100, 28);
        }
        static Bitmap Icon(string emoji)   // vẽ icon nhỏ từ emoji
        {
            var bmp = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(bmp))
            using (var f = new Font("Segoe UI Emoji", 12))
                g.DrawString(emoji, f, Brushes.Black, -2, 2);
            return bmp;
        }

        // ================= STATUS STRIP =================
        void CapNhatStatus()
        {
            lblCount.Text = "Tổng SP: " + ds.Count + "  |  ";
            lblTotalValue.Text = "Tổng giá trị kho: " + ds.Sum(p => p.Total).ToString("N0") + " đ";
        }

        // ================= CHỌN ẢNH (OpenFileDialog) =================
        void btnChooseImage_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp";        // chỉ lọc file ảnh
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                anhHienTai = ofd.FileName;
                HienAnh(anhHienTai);
            }
        }

        void HienAnh(string path)
        {
            picProductImage.Image?.Dispose();
            picProductImage.Image = null;
            if (!File.Exists(path)) return;
            using (var img = Image.FromFile(path))
                picProductImage.Image = new Bitmap(img);       // copy để không khóa file
        }

        // ================= THÊM / SỬA / XÓA =================
        bool DocForm(out decimal price)
        {
            if (txtId.Text.Trim() == "" || txtName.Text.Trim() == "")
            {
                MessageBox.Show("Mã và tên không được để trống!");
                price = 0; return false;
            }
            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Đơn giá phải là số không âm!");
                return false;
            }
            return true;
        }

        void GanForm(Product p, decimal price)
        {
            p.Id = txtId.Text.Trim();
            p.Name = txtName.Text.Trim();
            p.Category = cboCategory.Text;
            p.Quantity = (int)nudQuantity.Value;
            p.Price = price;
            p.ImagePath = anhHienTai;
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (!DocForm(out decimal price)) return;
            if (ds.Any(p => p.Id == txtId.Text.Trim()))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }
            var sp = new Product();
            GanForm(sp, price);
            ds.Add(sp);
            XoaForm();
        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null || !DocForm(out decimal price)) return;
            int i = dgvProducts.CurrentRow.Index;
            GanForm(ds[i], price);
            ds.ResetItem(i);                                   // báo bảng vẽ lại dòng
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ds.RemoveAt(dgvProducts.CurrentRow.Index);
                XoaForm();
            }
        }

        void XoaForm()
        {
            txtId.Clear(); txtName.Clear(); txtPrice.Clear();
            nudQuantity.Value = 0;
            cboCategory.SelectedIndex = 0;
            anhHienTai = "";
            picProductImage.Image?.Dispose();
            picProductImage.Image = null;
        }

        // bấm vào dòng nào thì đổ dữ liệu dòng đó lên form
        void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
            {
                txtId.Text = p.Id;
                txtName.Text = p.Name;
                cboCategory.Text = p.Category;
                nudQuantity.Value = Math.Min(p.Quantity, nudQuantity.Maximum);
                txtPrice.Text = p.Price.ToString();
                anhHienTai = p.ImagePath;
                HienAnh(p.ImagePath);
            }
        }

        // ================= XUẤT CSV (SaveFileDialog) =================
        void ExportCsv(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "CSV|*.csv";
            sfd.FileName = "BaoCaoKho.csv";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Id;Name;Category;Quantity;Price;ImagePath");   // dòng tiêu đề
                foreach (var p in ds)
                    sb.AppendLine(string.Join(SEP.ToString(), p.Id, p.Name.Replace(SEP, ','), p.Category,
                        p.Quantity, p.Price.ToString(CultureInfo.InvariantCulture), p.ImagePath));
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));   // UTF-8 để giữ tiếng Việt
                MessageBox.Show("Đã xuất " + ds.Count + " sản phẩm.");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // ================= NHẬP CSV (OpenFileDialog) =================
        void ImportCsv(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                ds.Clear();
                foreach (var line in lines.Skip(1))            // bỏ dòng tiêu đề
                {
                    var f = line.Split(SEP);
                    if (f.Length < 6) continue;
                    ds.Add(new Product
                    {
                        Id = f[0],
                        Name = f[1],
                        Category = f[2],
                        Quantity = int.Parse(f[3]),
                        Price = decimal.Parse(f[4], CultureInfo.InvariantCulture),
                        ImagePath = f[5]
                    });
                }
                MessageBox.Show("Đã nhập " + ds.Count + " sản phẩm.");
            }
            catch (Exception ex) { MessageBox.Show("File CSV không hợp lệ: " + ex.Message); }
        }

        // ================= THOÁT (Yes/No) =================
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }
    }
}