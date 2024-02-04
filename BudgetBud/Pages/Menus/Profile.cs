using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Backend.Models;
using BudgetBud.Components;
using BudgetBud.Pages;
using BudgetBud.Backend;

namespace BudgetBud.Pages.Menus
{
    public partial class Profile : UserControl
    {
        private Main parent { get; set; }
        ProfileModel model = new ProfileModel();

        public Profile()
        {
            InitializeComponent();
            GetData();

            changeDetails();
        }

        public Profile(Main parent)
        {
            InitializeComponent();
            GetData();

            changeDetails();
            this.parent = parent;
        }

        private void GetData()
        {
            model.GetData();

            if(model.profilePic != null)
            {
                // Convert byte array to Image
                MemoryStream ms = new MemoryStream(model.profilePic);
                Image image = Image.FromStream(ms);

                pictureBox1.BackgroundImage = image;
            }
        }

        public void changePass()
        {
            ChangeUserPassword changeUserPassword = new ChangeUserPassword(this);
            changeUserPassword.Dock = DockStyle.Fill;

            formContainer.Controls.Clear();
            formContainer.Controls.Add(changeUserPassword);
        }

        public void changeDetails()
        {
            ChangeUserDetails changeUserDetails = new ChangeUserDetails(this, UserContext.FullName, UserContext.UserName);
            changeUserDetails.Dock = DockStyle.Fill;

            formContainer.Controls.Clear();
            formContainer.Controls.Add(changeUserDetails);
        }

        public void updateDetails()
        {
            parent.GetUpdatedInfo();
        }

        private bool IsFileWithinSizeLimit(string filePath)
        {
            long maxSizeInBytes = 4000000000;
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length <= maxSizeInBytes;
        }

        private void changeProfileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the file path and pass it to SaveImageToDatabase function
                    string filePath = ofd.FileName;
                    if(IsFileWithinSizeLimit(filePath) && model.SetPicture(filePath))
                    {
                        pictureBox1.BackgroundImage = new Bitmap(ofd.FileName);
                        errorText.Text = "";
                        parent.ShowProfilePic();
                    }
                    else
                    {
                        errorText.Text = "Image size too big";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    errorText.Text = "Invalid image file";
                }
            }
        }
    }
}
