namespace bai1dqv
{
    partial class frmDeviceManager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            txtDeviceId = new TextBox();
            lblDeviceId = new Label();
            txtDeviceName = new TextBox();
            lblDeviceName = new Label();
            lblCategory = new Label();
            lblPrice = new Label();
            lblQuantity = new Label();
            txtPrice = new TextBox();
            cboCategory = new ComboBox();
            numQuantity = new NumericUpDown();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            errorProvider = new ErrorProvider(components);
            dgvDevices = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // txtDeviceId
            // 
            txtDeviceId.Location = new Point(141, 32);
            txtDeviceId.Name = "txtDeviceId";
            txtDeviceId.ReadOnly = true;
            txtDeviceId.Size = new Size(151, 27);
            txtDeviceId.TabIndex = 1;
            txtDeviceId.TabStop = false;
            txtDeviceId.TextChanged += this.textBox1_TextChanged;
            // 
            // lblDeviceId
            // 
            lblDeviceId.AutoSize = true;
            lblDeviceId.Location = new Point(20, 32);
            lblDeviceId.Name = "lblDeviceId";
            lblDeviceId.Size = new Size(81, 20);
            lblDeviceId.TabIndex = 2;
            lblDeviceId.Text = "Mã thiết bị";
            // 
            // txtDeviceName
            // 
            txtDeviceName.Location = new Point(141, 70);
            txtDeviceName.Name = "txtDeviceName";
            txtDeviceName.Size = new Size(151, 27);
            txtDeviceName.TabIndex = 3;
            // 
            // lblDeviceName
            // 
            lblDeviceName.AutoSize = true;
            lblDeviceName.Location = new Point(20, 70);
            lblDeviceName.Name = "lblDeviceName";
            lblDeviceName.Size = new Size(83, 20);
            lblDeviceName.TabIndex = 4;
            lblDeviceName.Text = "Tên thiết bị";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(20, 108);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(88, 20);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Loại thiết bị";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(400, 32);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(106, 20);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Đơn giá(VNĐ):";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(400, 70);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(95, 20);
            lblQuantity.TabIndex = 7;
            lblQuantity.Text = "Số lượng tồn";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(534, 32);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(150, 27);
            txtPrice.TabIndex = 8;
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(141, 105);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(151, 28);
            cboCategory.TabIndex = 9;
            // 
            // numQuantity
            // 
            numQuantity.Location = new Point(534, 71);
            numQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(150, 27);
            numQuantity.TabIndex = 10;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 195);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(142, 195);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(105, 29);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(272, 195);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 29);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(402, 195);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(115, 29);
            btnClear.TabIndex = 14;
            btnClear.Text = "Làm mới / Huỷ";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // dgvDevices
            // 
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Location = new Point(12, 245);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.RowHeadersWidth = 51;
            dgvDevices.Size = new Size(776, 283);
            dgvDevices.TabIndex = 15;
            // 
            // frmDeviceManager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 535);
            Controls.Add(dgvDevices);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(numQuantity);
            Controls.Add(cboCategory);
            Controls.Add(txtPrice);
            Controls.Add(lblQuantity);
            Controls.Add(lblPrice);
            Controls.Add(lblCategory);
            Controls.Add(lblDeviceName);
            Controls.Add(txtDeviceName);
            Controls.Add(lblDeviceId);
            Controls.Add(txtDeviceId);
            Name = "frmDeviceManager";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtDeviceId;
        private Label lblDeviceId;
        private TextBox txtDeviceName;
        private Label lblDeviceName;
        private Label lblCategory;
        private Label lblPrice;
        private Label lblQuantity;
        private TextBox txtPrice;
        private ComboBox cboCategory;
        private NumericUpDown numQuantity;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private ErrorProvider errorProvider;
        private DataGridView dgvDevices;
    }
}
