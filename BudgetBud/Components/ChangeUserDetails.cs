using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Pages.Menus;
using BudgetBud.Backend.Models;
using BudgetBud.Backend;

namespace BudgetBud.Components
{
    public partial class ChangeUserDetails : UserControl
    {
        private ProfileModel model = new ProfileModel();
        private Profile parent { get; set; }

        public ChangeUserDetails()
        {
            InitializeComponent();
        }

        public ChangeUserDetails(Profile parent, string fullName, string username)
        {
            InitializeComponent();
            this.parent = parent;
            this.fullnameText.Text = fullName;
            this.usernameText.Text = username;
        }

        private void changePasswordBtn_Click(object sender, EventArgs e)
        {
            parent.changePass();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string fullname = fullnameText.Text;
            string username = usernameText.Text;

            #region Input validations

            if (String.IsNullOrEmpty(fullname) || String.IsNullOrEmpty(username))
            {
                errorText.Text = "Incomplete inputs";
                return;
            }

            if(fullname.Length > 50)
            {
                errorText.Text = "Fullname must not exceed 50 characters";
                return;
            }

            if (!(username.Length >= 4) || !(username.Length <= 20))
            {
                errorText.Text = "Username must be 4-20 characters";
                return;
            }

            if (model.IsUsernameTaken(usernameText.Text) && !(usernameText.Text == UserContext.UserName))
            {
                errorText.Text = "Username already taken";
                return;
            }

            #endregion

            saveBtn.Enabled = false;
            model.SetDetails(fullname, username);
            parent.updateDetails();
            errorText.Text = "";

            MessageBox.Show("User info updated");

            saveBtn.Enabled = true;

        }
    }
}
