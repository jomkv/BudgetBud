using BudgetBud.Backend.Models;
using BudgetBud.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetBud.Pages.Menus
{
    public partial class Budget : UserControl
    {
        BudgetModel model = new BudgetModel();

        public Budget()
        {
            InitializeComponent();
            GetData();
            valueTypeDropdown.SelectedIndex = 0;
        }

        #region Get Data from Database
        public void GetData()
        {
            model.GetData();

            // update label and center it
            this.monthText.Text = model.Month;

            #region Render Categories

            if (model.categories.Count > 0)
            {
                // Reverse traverse the array of categories
                for (int i = model.categories.Count - 1; i >= 0; i--)
                {
                    // get category data
                    int id = model.categories[i].Id;
                    string name = model.categories[i].Name;
                    decimal budget = model.categories[i].BudgetPercent;

                    BudgetInputPercent input = new BudgetInputPercent(id, name, budget);

                    input.Dock = DockStyle.Top;
                    budgetContainer.Controls.Add(input);
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

        #endregion

        private void BudgetPage_Load(object sender, EventArgs e)
        {

        }

        #region Save using Percent
        private void SaveBudgetPercent()
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
                    errorText.Text = "Budget must be greater than 100";
                    return;
                }
                #endregion

                #region Iterate through Input User Controls 

                foreach (Control control in budgetContainer.Controls)
                {
                    if (control is BudgetInputPercent budgetInputPercent)
                    {
                        decimal budgetPercent;
                        bool response = decimal.TryParse(budgetInputPercent.input, out budgetPercent);

                        #region Category Input Validation
                        if (!response)
                        {
                            errorText.Text = "Invalid category budget input";
                            return;
                        }
                        else if (budgetPercent < 0)
                        {
                            errorText.Text = "Category Budget cannot be negative";
                            return;
                        }
                        #endregion

                        // Convert percent to literal amount
                        decimal budgetAmount = budgetPercent * budget / 100;

                        categoryBudgets.Add(new CategoryBudget<int, decimal, decimal>(budgetInputPercent.categoryId, budgetPercent, budgetAmount));

                        total += budgetPercent;
                    }
                    else if (control is BudgetInputValue budgetInputValue)
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
                        decimal budgetPercent = budgetAmount / budget * 100;

                        categoryBudgets.Add(new CategoryBudget<int, decimal, decimal>(budgetInputValue.categoryId, budgetPercent, budgetAmount));

                        total += budgetAmount;
                    }
                }

                #endregion

                if (valueTypeDropdown.SelectedIndex == 0 && total > 100)
                {
                    errorText.Text = "Total Percent must not exceed 0";
                    return;
                }
                else if (valueTypeDropdown.SelectedIndex == 1 && total > budget)
                {
                    errorText.Text = "Total Amount must not exceed Budget";
                    return;
                }

                errorText.Text = "";
                MessageBox.Show("Budget Saved");
                model.SaveBudget(categoryBudgets, budget);
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
        
        #region Save 
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveBudgetPercent();
        }
        #endregion

        private void valueTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            budgetContainer.Controls.Clear();

            if(valueTypeDropdown.SelectedIndex == 0) // Percent
            {
                if (model.categories.Count > 0)
                {
                    // Reverse traverse the array of categories
                    for (int i = model.categories.Count - 1; i >= 0; i--)
                    {
                        // get category data
                        int id = model.categories[i].Id;
                        string name = model.categories[i].Name;
                        decimal budget = model.categories[i].BudgetPercent;

                        BudgetInputPercent input = new BudgetInputPercent(id, name, budget);

                        input.Dock = DockStyle.Top;
                        budgetContainer.Controls.Add(input);
                    }
                }
                else
                {
                    // TODO
                }
            }
            else // Value
            {
                if (model.categories.Count > 0)
                {
                    // Reverse traverse the array of categories
                    for (int i = model.categories.Count - 1; i >= 0; i--)
                    {
                        // get category data
                        int id = model.categories[i].Id;
                        string name = model.categories[i].Name;
                        decimal budget = model.categories[i].BudgetValue;

                        BudgetInputValue input = new BudgetInputValue(id, name, budget);

                        input.Dock = DockStyle.Top;
                        budgetContainer.Controls.Add(input);
                    }
                }
                else
                {
                    // TODO
                }
            }
            
        }
    }
}
