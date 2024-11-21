using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuanLyShopCF_XML
{
    public partial class QuanLyNhanVien : Form
    {
        private string xmlFilePath = "nhanvien.xml"; // Đường dẫn đến file XML
        private string connectionString = "Data Source=PETER;Initial Catalog=QUANLICAFE;User ID=sa;Password=dinh942004;TrustServerCertificate=True";

        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        // Lớp đại diện cho một nhân viên
        public class NhanVien
        {
            public string TenUser { get; set; }
            public string TenDN { get; set; }
            public string MatKhau { get; set; }
            public int RoleID { get; set; }
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string Email { get; set; }
            public string SDT { get; set; }
        }
        private void LoadDataFromSQL()
        {
            List<NhanVien> nhanviens = new List<NhanVien>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TenUser, TenDN, MatKhau, RoleID, NgaySinh, GioiTinh, DiaChi, Email, SDT FROM Users";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nhanviens.Add(new NhanVien
                        {
                            TenUser = reader["TenUser"].ToString(),
                            TenDN = reader["TenDN"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            RoleID = Convert.ToInt32(reader["RoleID"]),
                            NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            Email = reader["Email"].ToString(),
                            SDT = reader["SDT"].ToString()
                        });
                    }
                }
            }

            // Cập nhật DataGridView với danh sách nhân viên
            dataGridViewNhanVien.DataSource = nhanviens;
        }
       
        private void btnLoadData_Click_1(object sender, EventArgs e)
        {

            LoadDataFromSQL();
        }

        // Sự kiện LoadData khi bấm vào nút Load
        private void btnLoadData_Click(object sender, EventArgs e)
        {
        }

        // Sự kiện SaveData khi bấm vào nút Save
        private void btnSaveData_Click(object sender, EventArgs e)
        {
        }

        // Thiết kế form và trang trí các TextBox
        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            // Cài đặt các TextBox đẹp mắt
            DesignTextBox(txtTenUser);
            DesignTextBox(txtTenDN);
            DesignTextBox(txtMatKhau);
            DesignTextBox(txtRoleID);
            DesignTextBox(txtNgaySinh);
            DesignTextBox(txtGioiTinh);
            DesignTextBox(txtDiaChi);
            DesignTextBox(txtEmail);
            DesignTextBox(txtSDT);
        }
        private void SaveDataToXML()
        {
            // Chỉ định đường dẫn tuyệt đối đến thư mục và tên file XML
            string filePath = @"C:\Users\ACER\source\repos\QuanLiCoffee_XML\QuanLyShopCF_XML\QuanLyShopCF_XML\XML\NhanVien.xml";
            dataGridViewNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Kiểm tra xem thư mục có tồn tại không, nếu không thì tạo mới
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory); // Tạo thư mục nếu không tồn tại
            }

            // Tạo mới tài liệu XML hoặc mở file nếu nó đã tồn tại
            XDocument doc;
            if (File.Exists(filePath))
            {
                doc = XDocument.Load(filePath); // Tải dữ liệu XML từ file hiện tại
            }
            else
            {
                doc = new XDocument(new XElement("NhanViens")); // Nếu không có file, tạo mới
            }

            // Duyệt qua danh sách nhân viên trong DataGridView
            foreach (DataGridViewRow row in dataGridViewNhanVien.Rows)
            {
                if (row.Cells["TenUser"].Value != null)  // Kiểm tra nếu ô không trống
                {
                    NhanVien nv = new NhanVien
                    {
                        TenUser = row.Cells["TenUser"].Value.ToString(),
                        TenDN = row.Cells["TenDN"].Value.ToString(),
                        MatKhau = row.Cells["MatKhau"].Value.ToString(),
                        RoleID = Convert.ToInt32(row.Cells["RoleID"].Value),
                        NgaySinh = Convert.ToDateTime(row.Cells["NgaySinh"].Value),
                        GioiTinh = row.Cells["GioiTinh"].Value.ToString(),
                        DiaChi = row.Cells["DiaChi"].Value.ToString(),
                        Email = row.Cells["Email"].Value.ToString(),
                        SDT = row.Cells["SDT"].Value.ToString()
                    };

                    XElement newNhanVien = new XElement("NhanVien",
                        new XElement("TenUser", nv.TenUser),
                        new XElement("TenDN", nv.TenDN),
                        new XElement("MatKhau", nv.MatKhau),
                        new XElement("RoleID", nv.RoleID),
                        new XElement("NgaySinh", nv.NgaySinh.ToString("yyyy-MM-dd")),
                        new XElement("GioiTinh", nv.GioiTinh),
                        new XElement("DiaChi", nv.DiaChi),
                        new XElement("Email", nv.Email),
                        new XElement("SDT", nv.SDT)
                    );

                    // Thêm phần tử mới vào XML
                    doc.Element("NhanViens").Add(newNhanVien);
                }
            }

            // Lưu file XML lại sau khi thêm dữ liệu
            doc.Save(filePath);

            // Hiển thị thông báo thành công
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dataGridViewNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                // Đảm bảo rằng nút xóa được kích hoạt
                btnXoa.Enabled = true;
            }
            else
            {
                // Nếu không có dòng nào được chọn, vô hiệu hóa nút xóa
                btnXoa.Enabled = false;
            }
        }

        // Hàm thiết kế TextBox đẹp
        private void DesignTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Arial", 12, FontStyle.Regular);
            textBox.BackColor = Color.LightBlue;
            textBox.ForeColor = Color.DarkBlue;
            textBox.Padding = new Padding(10);
        }

        // Thiết lập giao diện cho DataGridView
        private void SetupDataGridView()
        {
            dataGridViewNhanVien.BackgroundColor = Color.WhiteSmoke;
            dataGridViewNhanVien.BorderStyle = BorderStyle.None;
            dataGridViewNhanVien.GridColor = Color.Gray;
            dataGridViewNhanVien.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewNhanVien.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
            dataGridViewNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        // Sự kiện click vào DataGridView
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { if (e.RowIndex >= 0) // Đảm bảo rằng bạn đang chọn một dòng hợp lệ
            {
                int rowIndex = e.RowIndex;

                // Cập nhật các TextBox với dữ liệu từ dòng được chọn
                txtTenUser.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["TenUser"].Value.ToString();
                txtTenDN.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["TenDN"].Value.ToString();
                txtMatKhau.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["MatKhau"].Value.ToString();
                txtRoleID.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["RoleID"].Value.ToString();
                txtNgaySinh.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["NgaySinh"].Value.ToString();
                txtGioiTinh.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["GioiTinh"].Value.ToString();
                txtDiaChi.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["Email"].Value.ToString();
                txtSDT.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["SDT"].Value.ToString();
                btnXoa.Enabled = true;

            }

        }
        private void btnHienThiThongTin_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong DataGridView không
            if (dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                // Lấy chỉ mục của dòng đã chọn
                int rowIndex = dataGridViewNhanVien.SelectedRows[0].Index;

                // Hiển thị thông tin chi tiết của nhân viên trong các ô textboxes
                txtTenUser.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["TenUser"].Value.ToString();
                txtTenDN.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["TenDN"].Value.ToString();
                txtMatKhau.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["MatKhau"].Value.ToString();
                txtRoleID.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["RoleID"].Value.ToString();
                txtNgaySinh.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["NgaySinh"].Value.ToString();
                txtGioiTinh.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["GioiTinh"].Value.ToString();
                txtDiaChi.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["Email"].Value.ToString();
                txtSDT.Text = dataGridViewNhanVien.Rows[rowIndex].Cells["SDT"].Value.ToString();
            }
            else
            {
                // Nếu không có dòng nào được chọn, hiển thị thông báo lỗi
                MessageBox.Show("Vui lòng chọn một nhân viên từ danh sách.");
            }
        }

        // Sự kiện khi thay đổi dữ liệu ở các TextBox (sẽ giữ lại các sự kiện ban đầu của bạn)
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void txtTenUser_TextChanged(object sender, EventArgs e) { }
        private void txtTenDN_TextChanged(object sender, EventArgs e) { }
        private void txtRoleID_TextChanged(object sender, EventArgs e) { }
        private void txtGioiTinh_TextChanged(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveDataToXML();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các TextBox có trống không
            if (string.IsNullOrEmpty(txtTenUser.Text) || string.IsNullOrEmpty(txtTenDN.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Tạo đối tượng nhân viên mới và thêm vào DataGridView
            NhanVien nv = new NhanVien
            {
                TenUser = txtTenUser.Text,
                TenDN = txtTenDN.Text,
                MatKhau = txtMatKhau.Text,
                RoleID = Convert.ToInt32(txtRoleID.Text),
                NgaySinh = Convert.ToDateTime(txtNgaySinh.Text),
                GioiTinh = txtGioiTinh.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                SDT = txtSDT.Text
            };

            // Thêm vào DataGridView
            List<NhanVien> nhanviens = (List<NhanVien>)dataGridViewNhanVien.DataSource;
            nhanviens.Add(nv);
            dataGridViewNhanVien.DataSource = null;
            dataGridViewNhanVien.DataSource = nhanviens;

            // Cập nhật vào XML
            SaveDataToXML();
            txtTenUser.Clear();
            txtTenDN.Clear();
            txtMatKhau.Clear();
            txtRoleID.Clear();
            txtNgaySinh.Clear();
            txtGioiTinh.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
        }
        private void dataGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewNhanVien.SelectedRows[0].Index;

                // Cập nhật thông tin nhân viên trong DataGridView
                dataGridViewNhanVien.Rows[rowIndex].Cells["TenUser"].Value = txtTenUser.Text;
                dataGridViewNhanVien.Rows[rowIndex].Cells["TenDN"].Value = txtTenDN.Text;
                dataGridViewNhanVien.Rows[rowIndex].Cells["MatKhau"].Value = txtMatKhau.Text;
                dataGridViewNhanVien.Rows[rowIndex].Cells["RoleID"].Value = Convert.ToInt32(txtRoleID.Text);
                dataGridViewNhanVien.Rows[rowIndex].Cells["NgaySinh"].Value = Convert.ToDateTime(txtNgaySinh.Text);
                dataGridViewNhanVien.Rows[rowIndex].Cells["GioiTinh"].Value = txtGioiTinh.Text;
                dataGridViewNhanVien.Rows[rowIndex].Cells["DiaChi"].Value = txtDiaChi.Text;
                dataGridViewNhanVien.Rows[rowIndex].Cells["Email"].Value = txtEmail.Text;
                dataGridViewNhanVien.Rows[rowIndex].Cells["SDT"].Value = txtSDT.Text;

                // Cập nhật lại dữ liệu XML
                SaveDataToXML();
                txtTenUser.Clear();
                txtTenDN.Clear();
                txtMatKhau.Clear();
                txtRoleID.Clear();
                txtNgaySinh.Clear();
                txtGioiTinh.Clear();
                txtDiaChi.Clear();
                txtEmail.Clear();
                txtSDT.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong DataGridView không
            if (dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                // Lấy chỉ mục của dòng được chọn
                int rowIndex = dataGridViewNhanVien.SelectedRows[0].Index;

                // Lấy thông tin nhân viên từ dòng được chọn
                string tenUser = dataGridViewNhanVien.Rows[rowIndex].Cells["TenUser"].Value.ToString();
                int userId = Convert.ToInt32(dataGridViewNhanVien.Rows[rowIndex].Cells["UserId"].Value); // Giả sử có cột UserId

                // Xác nhận xóa nhân viên
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {tenUser}?", "Xác nhận", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Xóa nhân viên khỏi cơ sở dữ liệu

                        // Xóa nhân viên khỏi DataGridView
                        dataGridViewNhanVien.Rows.RemoveAt(rowIndex);

                        // Hiển thị thông báo thành công
                        MessageBox.Show("Đã xóa nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị thông báo lỗi khi xóa
                        MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Nếu không có dòng nào được chọn, hiển thị thông báo
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
