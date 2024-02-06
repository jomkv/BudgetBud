using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Pages;
using BudgetBud.Pages.Menus;
using BudgetBud.Backend.Models;

namespace BudgetBud.Components
{
    public partial class CategoryEditModal : Form
    {
        CategoriesModel model = new CategoriesModel();
        private Categories parent { get; set; }
        private BudgetCategories parent2;

        private int categoryId { get; set; }
        public CategoryEditModal()
        {
            InitializeComponent();
        }

        public CategoryEditModal(Categories parent, string name, int id)
        {
            InitializeComponent();
            this.nameText.Text = name;
            this.categoryId = id;
            this.parent = parent;
        }

        public CategoryEditModal(BudgetCategories parent, string name, int id)
        {
            InitializeComponent();
            this.nameText.Text = name;
            this.categoryId = id;
            this.parent2 = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CategoryEditModal_Load(object sender, EventArgs e)
        {
            
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string name = nameText.Text;

            #region Input Validation

            if (String.IsNullOrEmpty(name)) 
            {
                // ERROR: NO INPUT
                errorText.Text = "Category name cannot be empty";
                return;
            }

            if (name.Length < 4 || name.Length > 20)
            {
                // ERROR: NAME TOO LONG
                errorText.Text = "Category name must be 4-20 characters";
                return;
            }

            if (model.IsCategoryNameTaken(name))
            {
                // ERROR: NAME TAKEN
                errorText.Text = "Category already exists";
                return;
            }

            #endregion

            model.EditCategory(this.categoryId, this.nameText.Text);
            this.Close();
            parent2.RefreshData();
        }

        private void closeBtn_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
