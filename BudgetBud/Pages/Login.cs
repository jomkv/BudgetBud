﻿using BudgetBud.Backend;
using BudgetBud.Pages;
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
    public partial class Login : Form
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

        public Login()
        {
            InitializeComponent();

            // Round edges
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        public Login(string errorMessage)
        {
            InitializeComponent();

            // Round edges
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            errorText.Text = errorMessage;
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

        private void signupText_Click(object sender, EventArgs e)
        {
            this.Hide();

            Register registerPage = new Register();
            registerPage.Show();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameText.Text;
            string password = passwordText.Text;

            #region Input Validations

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                errorText.Text = "Incomplete Input";
                return;
            }

            #endregion

            loginBtn.Enabled = false;

            if (userDataAccess.GetLoginAuthentication(username, password))
            {
                Main mainPage = new Main();

                this.Hide();
                mainPage.Show();
            }
            else
            {
                errorText.Text = "Invalid Username and/or Password";
            }

            loginBtn.Enabled = true;
        }
    }
}
