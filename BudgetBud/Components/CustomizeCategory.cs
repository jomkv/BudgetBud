﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Backend.Models;
using BudgetBud.Pages.Menus;
using BudgetBud.Pages;

namespace BudgetBud.Components
{
    public partial class CustomizeCategory : UserControl
    {
        CategoriesModel model = new CategoriesModel();
        public Categories parent { get; set; }
        public BudgetCategories parent2 { get; set; }

        #region Properties

        public int id {  get; private set; }
        public string name { get; private set; }
        public bool allowDelete { get; private set; }

        #endregion

        public CustomizeCategory()
        {
            InitializeComponent();
        }

        #region Custom Constructor
        public CustomizeCategory(Categories parent, int id, string name, bool allowDelete)
        {
            InitializeComponent();
            this.parent = parent;
            this.nameText.Text = name;
            this.name = name;
            this.id = id;
            this.allowDelete = allowDelete;
        }

        public CustomizeCategory(BudgetCategories parent, int id, string name, bool allowDelete)
        {
            InitializeComponent();
            this.parent2 = parent;
            this.nameText.Text = name;
            this.name = name;
            this.id = id;
            this.allowDelete = allowDelete;
        }

        #endregion

        #region Actions
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if(!allowDelete)
            {
                MessageBox.Show("Deletion Not Allowed\nYou must have at least 3 categories. Add more categories before deleting this one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this category? This action will permanently remove the category and all associated expenses. Once deleted, this data cannot be recovered", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dr == DialogResult.Yes)
            {
                model.DeleteCategory(this.id);
                parent2.RefreshData();
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            using (CategoryEditModal modal = new CategoryEditModal(this.parent2, this.name, this.id))
            {
                modal.ShowDialog();
            }
        }
        #endregion
    }
}
