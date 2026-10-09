
namespace NhaThuocTayHongKhanh;

public class MainForm : Form
{
    private readonly string userRole;

    public MainForm(string role)
    {
        userRole = role;

        Text = "Nhà thuốc Tây Hồng Khánh - Trang chủ";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1000, 650);
        BackColor = Color.FromArgb(242, 247, 246);

        Label lblWelcome = new()
        {
            Text = $"NHÀ THUỐC TÂY HỒNG KHÁNH\nXin chào: {userRole}",
            Font = new Font("Segoe UI", 22, FontStyle.Bold),
            ForeColor = Color.FromArgb(20, 110, 90),
            AutoSize = true,
            Location = new Point(40, 35)
        };

        Button btnLogout = new()
        {
            Text = "ĐĂNG XUẤT",
            Size = new Size(150, 42),
            Location = new Point(40, 150)
        };

        btnLogout.Click += (sender, e) =>
        {
            Close();
        };

        Controls.Add(lblWelcome);
        Controls.Add(btnLogout);
    }
}
