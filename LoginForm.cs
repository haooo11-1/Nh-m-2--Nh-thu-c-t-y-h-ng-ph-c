
namespace NhaThuocTayHongKhanh;

public class LoginForm : Form
{
    private readonly TextBox txtUsername = new();
    private readonly TextBox txtPassword = new();
    private readonly Button btnLogin = new();
    private readonly Button btnExit = new();

    public string UserRole { get; private set; } = "";

    public LoginForm()
    {
        InitializeLoginForm();
    }

    private void InitializeLoginForm()
    {
        Text = "Nhà thuốc Tây Hồng Khánh - Đăng nhập";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(700, 480);
        MinimumSize = new Size(700, 480);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        BackColor = Color.FromArgb(242, 247, 246);
        Font = new Font("Segoe UI", 10);

        Label lblTitle = new()
        {
            Text = "NHÀ THUỐC TÂY HỒNG KHÁNH",
            Font = new Font("Segoe UI", 21, FontStyle.Bold),
            ForeColor = Color.FromArgb(20, 110, 90),
            AutoSize = true,
            Location = new Point(105, 55)
        };

        Label lblSubtitle = new()
        {
            Text = "HỆ THỐNG QUẢN LÝ NHÀ THUỐC",
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.DimGray,
            AutoSize = true,
            Location = new Point(218, 105)
        };

        Label lblUsername = new()
        {
            Text = "Tên đăng nhập",
            AutoSize = true,
            Location = new Point(160, 160)
        };

        txtUsername.Location = new Point(160, 188);
        txtUsername.Size = new Size(380, 32);
        txtUsername.PlaceholderText = "Nhập tên đăng nhập";
        txtUsername.MaxLength = 50;

        Label lblPassword = new()
        {
            Text = "Mật khẩu",
            AutoSize = true,
            Location = new Point(160, 240)
        };

        txtPassword.Location = new Point(160, 268);
        txtPassword.Size = new Size(380, 32);
        txtPassword.PlaceholderText = "Nhập mật khẩu";
        txtPassword.UseSystemPasswordChar = true;
        txtPassword.MaxLength = 100;

        btnLogin.Text = "ĐĂNG NHẬP";
        btnLogin.Location = new Point(160, 330);
        btnLogin.Size = new Size(180, 42);
        btnLogin.BackColor = Color.FromArgb(20, 130, 100);
        btnLogin.ForeColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.Click += BtnLogin_Click;

        btnExit.Text = "THOÁT";
        btnExit.Location = new Point(360, 330);
        btnExit.Size = new Size(180, 42);
        btnExit.Cursor = Cursors.Hand;
        btnExit.Click += (sender, e) => Close();

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

        Shown += (sender, e) => txtUsername.Focus();
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show(
                "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        // Tài khoản thử nghiệm, chưa kết nối Database.
        string role = "";

        if (string.Equals(
            username,
            "admin",
            StringComparison.OrdinalIgnoreCase)
            && password == "123456")
        {
            role = "Admin";
        }
        else if (string.Equals(
            username,
            "duocsi",
            StringComparison.OrdinalIgnoreCase)
            && password == "123456")
        {
            role = "DuocSi";
        }

        if (role == "")
        {
            MessageBox.Show(
                "Tên đăng nhập hoặc mật khẩu không đúng.",
                "Đăng nhập thất bại",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            txtPassword.Clear();
            txtPassword.Focus();
            return;
        }

        UserRole = role;

        MessageBox.Show(
            $"Đăng nhập thành công!\nVai trò: {UserRole}",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );

        DialogResult = DialogResult.OK;
        Close();
    }
}
