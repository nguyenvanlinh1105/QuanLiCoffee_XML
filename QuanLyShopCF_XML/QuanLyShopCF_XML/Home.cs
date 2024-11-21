using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;  // Đảm bảo rằng thư viện này đã được thêm vào

namespace QuanLyShopCF_XML
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormMain_Load);

            // Đăng ký sự kiện MouseEnter và MouseLeave cho các button
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;

            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;

            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;

            button4.MouseEnter += button4_MouseEnter;
            button4.MouseLeave += button4_MouseLeave;

            button5.MouseEnter += button5_MouseEnter;
            button5.MouseLeave += button5_MouseLeave;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        // Khi chuột di vào button1
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // Màu nền khi chuột vào
            button1.BackColor = Color.FromArgb(23, 162, 184);  // Màu xanh da trời đậm
            button1.ForeColor = Color.White;  // Màu chữ trắng
        }

        // Khi chuột ra khỏi button1
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            // Màu nền khi chuột rời đi
            button1.BackColor = Color.White;  // Màu xanh lá cây sáng
            button1.ForeColor = Color.Black;  // Màu chữ đen
        }

        // Khi chuột di vào button2
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            // Màu nền khi chuột vào
            button2.BackColor = Color.FromArgb(23, 162, 184);  // Màu xanh da trời đậm
            button2.ForeColor = Color.White;  // Màu chữ trắng
        }

        // Khi chuột ra khỏi button2
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            // Màu nền khi chuột rời đi
            button2.BackColor = Color.White;  // Màu xanh lá cây sáng
            button2.ForeColor = Color.Black;  // Màu chữ đen
        }

        // Khi chuột di vào button3
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            // Màu nền khi chuột vào
            button3.BackColor = Color.FromArgb(23, 162, 184);  // Màu xanh da trời đậm
            button3.ForeColor = Color.White;  // Màu chữ trắng
        }

        // Khi chuột ra khỏi button3
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            // Màu nền khi chuột rời đi
            button3.BackColor = Color.White;  // Màu xanh lá cây sáng
            button3.ForeColor = Color.Black;  // Màu chữ đen
        }

        // Khi chuột di vào button4
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            // Màu nền khi chuột vào
            button4.BackColor = Color.FromArgb(23, 162, 184);  // Màu xanh da trời đậm
            button4.ForeColor = Color.White;  // Màu chữ trắng
        }

        // Khi chuột ra khỏi button4
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            // Màu nền khi chuột rời đi
            button4.BackColor = Color.White;  // Màu xanh lá cây sáng
            button4.ForeColor = Color.Black;  // Màu chữ đen
        }

        // Khi chuột di vào button5
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            // Màu nền khi chuột vào
            button5.BackColor = Color.FromArgb(23, 162, 184);  // Màu xanh da trời đậm
            button5.ForeColor = Color.White;  // Màu chữ trắng
        }

        // Khi chuột ra khỏi button5
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            // Màu nền khi chuột rời đi
            button5.BackColor = Color.White;  // Màu xanh lá cây sáng
            button5.ForeColor = Color.Black;  // Màu chữ đen
        }

        // Các sự kiện click của các nút
        private void button1_Click(object sender, EventArgs e)
        {
            // Mở FormA vào Panel container
            // OpenFormInPanel(new QuanLyNhanVien());
            OpenFormInPanel(new QuanLyNhanVien_2());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Xử lý khi click vào button2
            OpenFormInPanel(new QuanLyDanhMuc());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new QuanlyThucUong());
            // Xử lý khi click vào button3
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Xử lý khi click vào button4
            OpenFormInPanel(new BanHang());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Xử lý khi click vào button5
            OpenFormInPanel(new SaoLuuDuLieu());
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Xử lý khi vẽ lên flowLayoutPanel1 nếu cần
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Xử lý khi vẽ lên panel1 nếu cần
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Xử lý khi click vào label1 nếu cần
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Xử lý khi click vào pictureBox1 nếu cần
        }

        private void containerpanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void OpenFormInPanel(Form form)
        {
            // Xóa form hiện tại nếu có
            foreach (Control control in containerpanel.Controls)
            {
                control.Dispose();
            }

            form.TopLevel = false;  // Đảm bảo rằng form không phải là top-level
            form.FormBorderStyle = FormBorderStyle.None;  // Không có border
            form.Dock = DockStyle.Fill;  // Chiếm toàn bộ panel
            containerpanel.Controls.Add(form);  // Thêm form vào panel container
            form.Show();  // Hiển thị form
        }
    }
}
