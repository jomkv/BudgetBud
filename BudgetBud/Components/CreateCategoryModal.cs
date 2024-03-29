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
using BudgetBud.Pages;
using BudgetBud.Pages.Menus;

namespace BudgetBud.Components
{
    public partial class CreateCategoryModal : Form
    {
        CategoriesModel model = new CategoriesModel();
        BudgetCategories parent;
        private Main main { get; set; }

        public CreateCategoryModal()
        {
            InitializeComponent();
        }

        public CreateCategoryModal(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        public CreateCategoryModal(BudgetCategories parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            string name = this.nameText.Text;

            #region Input Validation

            if (String.IsNullOrEmpty(name))
            {
                // ERROR: NO INPUT
                errorText.Text = "Category name cannot be empty";
                return;
            }
            else if (name.Length < 4 || name.Length > 20)
            {
                // ERROR: NAME TOO LONG
                errorText.Text = "Category name must be 4-20 characters";
                return;
            }

            #endregion

            model.CreateCategory(this.nameText.Text);
            this.Close();
            parent.RefreshData();
        }
    }
}
