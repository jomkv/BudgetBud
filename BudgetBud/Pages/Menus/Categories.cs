using BudgetBud.Backend.Models;
using BudgetBud.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetBud.Pages.Menus
{
    public partial class Categories : UserControl
    {
        CategoriesModel model = new CategoriesModel();
        private Main main {  get; set; }

        public Categories()
        {
            InitializeComponent();
            GetData();
        }

        public Categories(Main main)
        {
            InitializeComponent();
            GetData();
            this.main = main;
        }

        public void GetData()
        {
            model.GetData();

            bool allowDelete = (model.categoryCount <= 3) ? false : true;

            if(model.categories != null) 
            {
                categoriesPanel.Controls.Clear();

                for(int i = model.categories.Count - 1; i >= 0; i--)
                {
                    KeyValuePair<int, string> category = model.categories[i];

                    int id = category.Key;
                    string name = category.Value;

                    CustomizeCategory customizeCategory = new CustomizeCategory(this, id, name, allowDelete);
                    customizeCategory.Dock = DockStyle.Top;

                    categoriesPanel.Controls.Add(customizeCategory);
                }
            }

            if(model.categoryCount >= 8)
            {
                saveBtn.Enabled = false;
            }
            else
            {
                saveBtn.Enabled = true;
            }
        }

        private void createCategoryBtn_Click(object sender, EventArgs e)
        {
            using (CreateCategoryModal modal = new CreateCategoryModal(this.main))
            {
                modal.ShowDialog();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            nameText.Text = "";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string name = this.nameText.Text;

            #region Input Validation

            if (String.IsNullOrEmpty(name))
            {
                // ERROR: NO INPUT
                errorText.Text = "Category name cannot be empty";
                return;
            }
            else if (name.Length < 5 || name.Length > 20)
            {
                // ERROR: NAME TOO LONG
                errorText.Text = "Category name must be 5-20 characters";
                return;
            }

            #endregion

            model.CreateCategory(name);
            errorText.Text = "";
            nameText.Text = "";
            GetData();
        }
    }
}
