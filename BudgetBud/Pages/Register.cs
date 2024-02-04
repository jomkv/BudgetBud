using BudgetBud.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetBud
{
    public partial class Register : Form
    {
        UserDataAccess userDataAccess = new UserDataAccess();

        #region Round Edges

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
              int nHeightEllipse
         );

        #endregion

        public Register()
        {
            InitializeComponent();

            // Round edges
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        #region Control Bar

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Drag

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        #endregion

        #endregion

        private void loginText_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login loginPage = new Login();
            loginPage.Show();
        }

        private void redirectToLogin()
        {
            Login loginPage = new Login();

            this.Hide();
            loginPage.Show();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string fullName = fullnameText.Text;
            string username = usernameText.Text;
            string password = passwordText.Text;
            string confirmPass = confirmText.Text;

            #region Input Validations

            if (
                String.IsNullOrEmpty(fullName) ||
                String.IsNullOrEmpty(username) ||
                String.IsNullOrEmpty(password) ||
                String.IsNullOrEmpty(confirmPass)
               )
            {
                errorText.Text = "Incomplete Input";
                return;
            }

            if (password != confirmPass)
            {
                errorText.Text = "Passwords do not match";
                return;
            }

            if (!(password.Length >= 8) || !(password.Length <= 20))
            {
                errorText.Text = "Password must be 8-20 characters";
                return;
            }

            if (!(username.Length >= 4) || !(username.Length <= 20))
            {
                errorText.Text = "Username must be 4-20 characters";
                return;
            }

            if(fullName.Length > 50)
            {
                errorText.Text = "Fullname must not exceed 50 characters";
                return;
            }

            #endregion

            registerBtn.Enabled = false;

            if(userDataAccess.IsUserTaken(username))
            {
                errorText.Text = "Username already taken";
                registerBtn.Enabled = true;
                return;
            }

            bool isSuccess = userDataAccess.Register(fullName, username, password);

            if (isSuccess)
            {
                redirectToLogin();
            }
            else
            {
                errorText.Text = "Error signing up";
            }

            registerBtn.Enabled = true;
        }

        // Redirect user to login form
        private void loginButton_Click(object sender, EventArgs e)
        {
            redirectToLogin();
        }
    }
}
