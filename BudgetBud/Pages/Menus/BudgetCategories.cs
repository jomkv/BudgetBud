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
using BudgetBud.Backend.Models;
using System.Diagnostics;

namespace BudgetBud.Pages.Menus
{
    public partial class BudgetCategories : UserControl
    {
        private Main main { get; set; }
        BudgetModel model = new BudgetModel();
        CategoriesModel categoriesModel = new CategoriesModel();

        public BudgetCategories()
        {
            InitializeComponent();
            GetData();
        }

        public BudgetCategories(Main main)
        {
            InitializeComponent();
            RefreshData();
            this.main = main;
        }

        public void RefreshData()
        {
            GetData();
            GetData2();
        }

        public void GetData2()
        {
            categoriesModel.GetData();

            bool allowDelete = (categoriesModel.categoryCount <= 3) ? false : true;

            if (categoriesModel.categories != null)
            {
                categoriesPanel.Controls.Clear();

                for (int i = model.categories.Count - 1; i >= 0; i--)
                {
                    KeyValuePair<int, string> category = categoriesModel.categories[i];

                    int id = category.Key;
                    string name = category.Value;

                    CustomizeCategory customizeCategory = new CustomizeCategory(this, id, name, allowDelete);
                    customizeCategory.Dock = DockStyle.Top;

                    categoriesPanel.Controls.Add(customizeCategory);
                }
            }

            if (categoriesModel.categoryCount >= 8)
            {
                saveBtn.Enabled = false;
            }
            else
            {
                saveBtn.Enabled = true;
            }
        }

        public void GetData()
        {
            model.GetData();

            // update label and center it
            this.monthText.Text = model.Month;

            #region Render Categories

            if (model.categories.Count > 0)
            {
                exactContainer.Controls.Clear();

                // Reverse traverse the array of categories
                for (int i = model.categories.Count - 1; i >= 0; i--)
                {
                    // get category data
                    int id = model.categories[i].Id;
                    string name = model.categories[i].Name;
                    decimal budgetAmount = model.categories[i].BudgetValue;

                    BudgetInputValue inputValue = new BudgetInputValue(id, name, budgetAmount);

                    inputValue.Dock = DockStyle.Top;
                    exactContainer.Controls.Add(inputValue);
                }

            }
            else
            {
                // TODO
            }

            #endregion

            // render budget
            if (model.MonthlyBudget == null)
            {
                this.budgetText.Text = "0";
            }
            else
            {
                this.budgetText.Text = model.MonthlyBudget.ToString();
            }
        }

        #region Budget Actions

        private void SaveBudget(object sender, EventArgs e)
        {
            try
            {
                this.saveBtn.Enabled = false;

                #region Properties

                decimal budget;

                // CategoryId, BudgetPercent, BudgetAmount
                List<CategoryBudget<int, decimal, decimal>> categoryBudgets = new List<CategoryBudget<int, decimal, decimal>>();

                decimal total = 0;

                #endregion

                bool res = decimal.TryParse(budgetText.Text, out budget);

                #region Input Validation
                if (!res)
                {
                    errorText.Text = "Invalid budget input";
                    return;
                }

                if (budget <= 0)
                {
                    errorText.Text = "Budget must be greater than 0";
                    return;
                }
                #endregion

                #region Iterate through Input User Controls 

                    #region Exact Value

                    foreach (Control control in exactContainer.Controls)
                    {
                        if (control is BudgetInputValue budgetInputValue)
                        {
                            decimal budgetAmount;
                            bool response = decimal.TryParse(budgetInputValue.input, out budgetAmount);

                            #region Category Input Validation
                            if (!response)
                            {
                                errorText.Text = "Invalid category budget input";
                                return;
                            }
                            else if (budgetAmount < 0)
                            {
                                errorText.Text = "Category Budget cannot be negative";
                                return;
                            }
                            #endregion

                            // Convert amount literal to percent
                            decimal budgetPercent = Math.Round(budgetAmount / budget * 100, 2);

                            categoryBudgets.Add(new CategoryBudget<int, decimal, decimal>(budgetInputValue.categoryId, budgetPercent, budgetAmount));

                            total += budgetAmount;
                        }
                    }

                    if (total > budget)
                    {
                        errorText.Text = "Total Amount must not exceed Budget";
                        return;
                    }

                    #endregion
                

                #endregion

                errorText.Text = "";
                model.SaveBudget(categoryBudgets, budget);
                MessageBox.Show("Budget Saved");
                GetData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                this.saveBtn.Enabled = true;
            }
        }

        #endregion

        // Add category
        private void button1_Click(object sender, EventArgs e)
        {
            using (CreateCategoryModal modal = new CreateCategoryModal(this))
            {
                modal.ShowDialog();
            }
        }
    }
}
