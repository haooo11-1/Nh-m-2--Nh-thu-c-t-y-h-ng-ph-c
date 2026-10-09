
namespace NhaThuocTayHongKhanh;

public partial class Form1 : Form
{
    private TextBox txtUsername = new();
    private TextBox txtPassword = new();
    private Button btnLogin = new();
    private Button btnExit = new();

    public Form1()
    {
        InitializeComponent();

        // Cấu hình cửa sổ
        Text = "Nhà thuốc Tây Hồng Khánh - Đăng nhập";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(900, 560);
        MinimumSize = new Size(760, 500);
        BackColor = Color.FromArgb(242, 247, 246);
        Font = new Font("Segoe UI", 10);

        // Tiêu đề
        Label lblTitle = new()
        {
            Text = "NHÀ THUỐC TÂY HỒNG KHÁNH",
            Font = new Font("Segoe UI", 22, FontStyle.Bold),
            ForeColor = Color.FromArgb(20, 110, 90),
            AutoSize = true,
            Location = new Point(170, 65)
        };

        Label lblSubtitle = new()
        {
            Text = "HỆ THỐNG QUẢN LÝ NHÀ THUỐC",
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.DimGray,
            AutoSize = true,
            Location = new Point(310, 115)
        };

        // Nhãn và ô nhập tài khoản
        Label lblUsername = new()
        {
            Text = "Tên đăng nhập",
            AutoSize = true,
            Location = new Point(250, 190)
        };

        txtUsername.Location = new Point(250, 220);
        txtUsername.Size = new Size(380, 35);
        txtUsername.PlaceholderText = "Nhập tên đăng nhập";

        // Nhãn và ô nhập mật khẩu
        Label lblPassword = new()
        {
            Text = "Mật khẩu",
            AutoSize = true,
            Location = new Point(250, 275)
        };

        txtPassword.Location = new Point(250, 305);
        txtPassword.Size = new Size(380, 35);
        txtPassword.PlaceholderText = "Nhập mật khẩu";
        txtPassword.UseSystemPasswordChar = true;

        // Nút đăng nhập
        btnLogin.Text = "ĐĂNG NHẬP";
        btnLogin.Location = new Point(250, 375);
        btnLogin.Size = new Size(180, 45);
        btnLogin.BackColor = Color.FromArgb(20, 130, 100);
        btnLogin.ForeColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.Click += BtnLogin_Click;

        // Nút thoát
        btnExit.Text = "THOÁT";
        btnExit.Location = new Point(450, 375);
        btnExit.Size = new Size(180, 45);
        btnExit.Cursor = Cursors.Hand;
        btnExit.Click += (sender, e) => Close();

        // Thêm các thành phần vào Form
        Controls.AddRange(new Control[]
        {
            lblTitle,
            lblSubtitle,
            lblUsername,
            txtUsername,
            lblPassword,
            txtPassword,
            btnLogin,
            btnExit
        });

        AcceptButton = btnLogin;
        CancelButton = btnExit;
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show(
                "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            return;
        }

        // Tài khoản giả lập để kiểm tra giao diện.
        if (txtUsername.Text == "admin" &&
            txtPassword.Text == "123456")
        {
            MessageBox.Show(
                "Đăng nhập thử nghiệm thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        else
        {
            MessageBox.Show(
                "Tên đăng nhập hoặc mật khẩu không đúng.",
                "Đăng nhập thất bại",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
