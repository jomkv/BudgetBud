using BudgetBud.Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Backend.Models;

namespace BudgetBud.Pages.Menus
{
    public partial class Expense : UserControl
    {
        private ExpenseModel model = new ExpenseModel();
        private List<int> categoryIDs = new List<int>();

        public Expense()
        {
            InitializeComponent();
            LoadData();

            // Initialize some form inputs
            categoryDropdown.SelectedIndex = 0;
            amountText.Text = "0";
        }

        #region Helper Functions

        private void LoadData()
        {
            model.FetchCategories();

            if (model.categories.Count > 0)
            {
                // add categories from db to combobox
                foreach (KeyValuePair<int, string> category in model.categories)
                {
                    // Add category to combo box
                    categoryDropdown.Items.Add(category.Value);
                    // Append category id to list
                    categoryIDs.Add(category.Key);
                }
            }
        }

        private void clearInputs()
        {
            categoryDropdown.SelectedIndex = 0;
            titleText.Text = "";
            descriptionText.Text = "";
            amountText.Text = "0";
            dateTimePicker.Value = DateTime.Now;
            errorText.Text = "";
        }

        #endregion

        private void addExpenseButton_Click(object sender, EventArgs e)
        {
            #region Expense Properties

            int selectedCategoryID = categoryIDs[categoryDropdown.SelectedIndex];
            string selectedCategory = categoryDropdown.Text;
            decimal price;
            string title = titleText.Text;
            string desc = descriptionText.Text;
            string date = dateTimePicker.Value.ToString("yyyy-MM-dd");

            #endregion

            #region Input Validations

            bool res = Decimal.TryParse(amountText.Text, out price);

            if (!res)
            {
                errorText.Text = "Invalid amount input";
                return;
            }

            if (price <= 0)
            {
                errorText.Text = "Price must be greater than 0";
                return;
            }

            if (
                String.IsNullOrEmpty(title) ||
                String.IsNullOrEmpty(desc) ||
                String.IsNullOrEmpty(date)
               )
            {
                errorText.Text = "Incomplete input";
                return;
            }

            #endregion

            addExpenseButton.Enabled = false;

            bool isSuccess = model.AddExpense(selectedCategory, selectedCategoryID, title, desc, price, date);

            if (!isSuccess)
            {
                errorText.Text = "Error adding new expense";
            }

            addExpenseButton.Enabled = true;

            clearInputs();

        }
    }
}
