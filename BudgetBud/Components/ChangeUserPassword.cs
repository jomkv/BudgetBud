using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Backend;
using BudgetBud.Pages.Menus;
using BudgetBud.Backend.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BudgetBud.Components
{
    public partial class ChangeUserPassword : UserControl
    {
        private Profile parent { get; set; }
        private ProfileModel model = new ProfileModel();

        public ChangeUserPassword()
        {
            InitializeComponent();
        }

        public ChangeUserPassword(Profile parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void changeDetailsBtn_Click(object sender, EventArgs e)
        {
            parent.changeDetails();
        }

        private void clearInputs()
        {
            currentPassText.Clear();
            newPassText.Clear();
            confirmPassText.Clear();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string currPass = currentPassText.Text;
            string newPass = newPassText.Text;
            string confirmPass = confirmPassText.Text;

            #region Input validations

            if (String.IsNullOrEmpty(currPass) || String.IsNullOrEmpty(newPass) || String.IsNullOrEmpty(confirmPass))
            {
                errorText.Text = "Incomplete inputs";
                return;
            }

            if (newPass != confirmPass)
            {
                errorText.Text = "Passwords do not match";
                return;
            }

            if (!(newPass.Length >= 8) || !(newPass.Length <= 20))
            {
                errorText.Text = "New Password must be 8-20 characters";
                return;
            }

            // Check if current password is correct
            if(!model.IsCurrentPassCorrect(currPass))
            {
                errorText.Text = "Current password is not correct";
                return;
            }

            #endregion

            saveBtn.Enabled = false;

            errorText.Text = "";
            model.SetPassword(newPass);
            MessageBox.Show("Password updated");
            clearInputs();

            saveBtn.Enabled = true;


        }
    }
}
