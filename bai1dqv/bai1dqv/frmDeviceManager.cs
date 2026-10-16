#nullable disable
using System.ComponentModel;
using System.Globalization;

namespace bai1dqv
{
    public class Device
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public partial class frmDeviceManager : Form
    {
        // ---------- Dữ liệu dùng chung ----------
        private readonly BindingList<Device> devices = new BindingList<Device>();
        private readonly BindingSource bs = new BindingSource();
        private readonly CultureInfo VN = new CultureInfo("vi-VN");
        private int nextId = 1;

        public frmDeviceManager()
        {
            InitializeComponent();

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            ConfigureGrid();                       // 1. tạo cột bằng code

            bs.DataSource = devices;               // 2. danh sách -> BindingSource
            dgvDevices.DataSource = bs;            // 3. BindingSource -> Grid

            cboCategory.Items.AddRange(new object[]
                { "Điện thoại", "Laptop", "Máy tính bảng", "Phụ kiện", "Linh kiện" });

            // Đăng ký sự kiện bằng code (đừng double-click nút trong Designer)
            dgvDevices.SelectionChanged += dgvDevices_SelectionChanged;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;

            ResetInputs();
        }

        // ---------- Cấu hình DataGridView hoàn toàn bằng code ----------
        private void ConfigureGrid()
        {
            dgvDevices.AutoGenerateColumns = false;
            dgvDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevices.MultiSelect = false;
            dgvDevices.ReadOnly = true;
            dgvDevices.AllowUserToAddRows = false;
            dgvDevices.AllowUserToDeleteRows = false;
            dgvDevices.RowHeadersVisible = false;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colId", DataPropertyName = "DeviceId", HeaderText = "Mã TB", FillWeight = 15 });

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colName", DataPropertyName = "DeviceName", HeaderText = "Tên thiết bị", FillWeight = 35 });

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colCategory", DataPropertyName = "Category", HeaderText = "Loại", FillWeight = 20 });

            var colPrice = new DataGridViewTextBoxColumn
            { Name = "colPrice", DataPropertyName = "Price", HeaderText = "Đơn giá (VNĐ)", FillWeight = 20 };
            colPrice.DefaultCellStyle.Format = "N0";
            colPrice.DefaultCellStyle.FormatProvider = VN;
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDevices.Columns.Add(colPrice);

            var colQty = new DataGridViewTextBoxColumn
            { Name = "colQty", DataPropertyName = "Quantity", HeaderText = "Tồn kho", FillWeight = 10 };
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDevices.Columns.Add(colQty);
        }

        // ---------- Lấy đối tượng của dòng đang được chọn ----------
        private Device GetSelectedDevice()
        {
            if (dgvDevices.SelectedRows.Count == 0) return null;
            return dgvDevices.SelectedRows[0].DataBoundItem as Device;
        }

        // ---------- Validation + ErrorProvider ----------
        private bool ValidateInputs(out decimal price)
        {
            bool ok = true;
            price = 0;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtDeviceName.Text))
            {
                errorProvider.SetError(txtDeviceName, "Tên thiết bị không được để trống.");
                ok = false;
            }

            if (cboCategory.SelectedIndex < 0)
            {
                errorProvider.SetError(cboCategory, "Vui lòng chọn loại thiết bị.");
                ok = false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Number, VN, out price) || price < 0)
            {
                errorProvider.SetError(txtPrice, "Đơn giá phải là số hợp lệ và không nhỏ hơn 0.");
                ok = false;
            }

            return ok;
        }

        // ---------- Grid -> các ô nhập ----------
        private void dgvDevices_SelectionChanged(object sender, EventArgs e)
        {
            var d = GetSelectedDevice();
            if (d == null) return;

            txtDeviceId.Text = d.DeviceId;
            txtDeviceName.Text = d.DeviceName;
            cboCategory.SelectedItem = d.Category;
            txtPrice.Text = d.Price.ToString("#,##0.##", VN);
            numQuantity.Value = d.Quantity;
            errorProvider.Clear();
        }

        // ---------- Đưa form về trạng thái nhập mới ----------
        private void ResetInputs()
        {
            txtDeviceId.Text = "TB" + nextId.ToString("D3");
            txtDeviceName.Clear();
            cboCategory.SelectedIndex = -1;
            txtPrice.Clear();
            numQuantity.Value = 0;
            errorProvider.Clear();
            dgvDevices.ClearSelection();
            txtDeviceName.Focus();
        }

        // ---------- Các nút chức năng ----------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out decimal price)) return;

            devices.Add(new Device
            {
                DeviceId = txtDeviceId.Text,
                DeviceName = txtDeviceName.Text.Trim(),
                Category = cboCategory.SelectedItem.ToString(),
                Price = price,
                Quantity = (int)numQuantity.Value
            });

            nextId++;
            ResetInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var d = GetSelectedDevice();
            if (d == null)
            {
                MessageBox.Show("Hãy chọn một dòng trên bảng để cập nhật.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInputs(out decimal price)) return;

            d.DeviceName = txtDeviceName.Text.Trim();
            d.Category = cboCategory.SelectedItem.ToString();
            d.Price = price;
            d.Quantity = (int)numQuantity.Value;

            bs.ResetCurrentItem();   // báo Grid vẽ lại dòng vừa sửa
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var d = GetSelectedDevice();
            if (d == null)
            {
                MessageBox.Show("Hãy chọn một dòng trên bảng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rs = MessageBox.Show($"Bạn có chắc muốn xóa thiết bị \"{d.DeviceName}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
            {
                devices.Remove(d);
                ResetInputs();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetInputs();
        }

        // ---------- Hàm trống để Designer không báo lỗi thiếu sự kiện ----------
        // (Designer đang gắn sự kiện TextChanged của textBox1 tới hàm này)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}