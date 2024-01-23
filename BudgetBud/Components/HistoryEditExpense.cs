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
using BudgetBud.Pages;
using BudgetBud.Pages.Menus;

namespace BudgetBud.Components
{
    public partial class HistoryEditExpense : Form
    {
        HistoryModel model = new HistoryModel();
        private Main main { get; set; }
        private List<int> categoryIDs = new List<int>();
        private int expenseId { get; set; }

        public HistoryEditExpense()
        {
            InitializeComponent();
        }

        public HistoryEditExpense(Main main, int id, string categoryName, string title, string desc, decimal amount, string dateS)
        {
            InitializeComponent();

            this.expenseId = id;
            this.main = main;

            model.FetchCategories();

            // Iterate through categories
            foreach(KeyValuePair<int, string> category in model.categories)
            {
                // Append Category ID
                categoryIDs.Add(category.Key);

                // Append Category Name to dropdown
                categoryDropdown.Items.Add(category.Value);
            }

            #region Set Default Values

            DateTime date;
            if (DateTime.TryParse(dateS, out date))
            {
                this.dateTimePicker.Value = date;
            }

            int index = categoryDropdown.FindString(categoryName);
            if (index != -1)
            {
                categoryDropdown.SelectedIndex = index;
            }

            this.titleText.Text = title;
            this.descriptionText.Text = desc;
            this.amountText.Text = amount.ToString();

            #endregion
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            int selectedCategoryID = categoryIDs[categoryDropdown.SelectedIndex];
            string selectedCategory = categoryDropdown.Text;
            decimal price;
            string title = titleText.Text;
            string desc = descriptionText.Text;
            string date = dateTimePicker.Value.ToString("yyyy-MM-dd");

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

            saveBtn.Enabled = false;

            bool isSuccess = model.EditExpense(this.expenseId, selectedCategoryID, selectedCategory, title, desc, price, date);

            saveBtn.Enabled = true;

            if (!isSuccess)
            {
                errorText.Text = "Error editting expense";
            }
            else
            {
                this.Close();
                main.ChangeMenu(new History(this.main));
            }

        }
    }
}
