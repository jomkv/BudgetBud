using BudgetBud.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Pages.Menus;

namespace BudgetBud.Pages
{
    public partial class Main : Form
    {
        #region Round Edge Imports

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

        public Main()
        {
            InitializeComponent();

            // Round edge
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            // Initialize Username and Status
            this.usernameText.Text = UserContext.UserName;
            this.statusText.Text = UserContext.Status;

            #region Navbar Stuff

            PnlNav.Height = homeBtn.Height;
            PnlNav.Top = homeBtn.Top;
            PnlNav.Left = homeBtn.Left;
            homeBtn.BackColor = Color.FromArgb(46, 51, 73);

            // Subscribe custom function to Resize event
            this.Resize += Main_Resize;

            #endregion

            if (CheckSessionValidity())
            {
                Home homePage = new Home();
                ChangeMenu(homePage);
            }
        }

        #region Control Bar

        private void Main_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
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

        #region Navbar Actions

        #region Helper Functions
        private void UpdateNavLine(int height, int top, int left)
        {
            PnlNav.Height = height;
            PnlNav.Top = top;
            PnlNav.Left = left;
        }

        private void ResetButtonColors()
        {
            homeBtn.BackColor = Color.FromArgb(24, 30, 54);
            budgetBtn.BackColor = Color.FromArgb(24, 30, 54);
            categoriesBtn.BackColor = Color.FromArgb(24, 30, 54);
            expenseBtn.BackColor = Color.FromArgb(24, 30, 54);
            historyBtn.BackColor = Color.FromArgb(24, 30, 54);
            analysisBtn.BackColor = Color.FromArgb(24, 30, 54);
            logoutBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        public void ChangeMenu(UserControl userControl)
        {
            contentPanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(userControl);
        }

        private bool CheckSessionValidity()
        {
            return true;

            if (!UserContext.IsLoggedIn)
            {
                Debug.WriteLine("Unauthorized access attempt: User not logged in.");

                RedirectToLogin();

                return false;
            }

            return true;
        }

        private void RedirectToLogin()
        {
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
        }

        #endregion

        private void homeBtn_Click(object sender, EventArgs e)
        {
            if (!CheckSessionValidity())
            {
                return;
            }

            ResetButtonColors();

            UpdateNavLine(homeBtn.Height, homeBtn.Top, homeBtn.Left);
            homeBtn.BackColor = Color.FromArgb(46, 51, 73);

            Home homePage = new Home();
            ChangeMenu(homePage);
        }

        private void budgetBtn_Click(object sender, EventArgs e)
        {
            if (!CheckSessionValidity())
            {
                return;
            }

            ResetButtonColors();

            UpdateNavLine(budgetBtn.Height, budgetBtn.Top, budgetBtn.Left);
            budgetBtn.BackColor = Color.FromArgb(46, 51, 73);

            Budget budgetPage = new Budget();
            ChangeMenu(budgetPage);
        }

        private void categoriesBtn_Click(object sender, EventArgs e)
        {
            if (!CheckSessionValidity())
            {
                return;
            }

            ResetButtonColors();

            UpdateNavLine(categoriesBtn.Height, categoriesBtn.Top, categoriesBtn.Left);
            categoriesBtn.BackColor = Color.FromArgb(46, 51, 73);

            Categories categoriesPage = new Categories();
            ChangeMenu(categoriesPage);
        }

        private void expenseBtn_Click(object sender, EventArgs e)
        {
            if (!CheckSessionValidity())
            {
                return;
            }

            ResetButtonColors();

            UpdateNavLine(expenseBtn.Height, expenseBtn.Top, expenseBtn.Left);
            expenseBtn.BackColor = Color.FromArgb(46, 51, 73);

            Expense expensePage = new Expense();
            ChangeMenu(expensePage);
        }

        private void historyBtn_Click(object sender, EventArgs e)
        {
            if (!CheckSessionValidity())
            {
                return;
            }

            ResetButtonColors();

            UpdateNavLine(historyBtn.Height, historyBtn.Top, historyBtn.Left);
            historyBtn.BackColor = Color.FromArgb(46, 51, 73);

            History historyPage = new History(this);
            ChangeMenu(historyPage);
        }

        private void analysisBtn_Click(object sender, EventArgs e)
        {
            if (!CheckSessionValidity())
            {
                return;
            }

            ResetButtonColors();

            UpdateNavLine(analysisBtn.Height, analysisBtn.Top, analysisBtn.Left);
            analysisBtn.BackColor = Color.FromArgb(46, 51, 73);

            Analysis historyPage = new Analysis();
            ChangeMenu(historyPage);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            // Reset UserContext properties
            UserContext.IsLoggedIn = false;
            UserContext.SessionUserId = 0;

            ResetButtonColors();

            UpdateNavLine(logoutBtn.Height, logoutBtn.Top, logoutBtn.Left);
            logoutBtn.BackColor = Color.FromArgb(46, 51, 73);

            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
        }

        #endregion
    }
}
