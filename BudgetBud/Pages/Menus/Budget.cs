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

                // CategoryId, CategoryBudget
                List<KeyValuePair<int, decimal>> categoryBudgets = new List<KeyValuePair<int, decimal>>();

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

                foreach (Control control in budgetContainer.Controls)
                {
                    if (control is BudgetInputPercent budgetInput)
                    {
                        decimal categoryBudget;
                        bool response = decimal.TryParse(budgetInput.input, out categoryBudget);

                        #region Category Input Validation
                        if (!response)
                        {
                            errorText.Text = "Invalid category budget input";
                            return;
                        }
                        else if (categoryBudget < 0)
                        {
                            errorText.Text = "Category Budget cannot be negative";
                            return;
                        }
                        #endregion

                        categoryBudgets.Add(new KeyValuePair<int, decimal>(budgetInput.categoryId, categoryBudget));

                        total += categoryBudget;
                    }
                }

                #endregion

                if (total > 100)
                {
                    errorText.Text = "Total Percent must not exceed 100";
                }
                else
                {
                    errorText.Text = "";
                    MessageBox.Show("Budget Saved");
                    model.SaveBudget(categoryBudgets, budget, true);
                }
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

        #region Save using Exact Value
        private void SaveBudgetValue()
        {
            try
            {
                this.saveBtn.Enabled = false;

                #region Properties

                decimal budget;

                // CategoryId, CategoryBudget
                List<KeyValuePair<int, decimal>> categoryBudgets = new List<KeyValuePair<int, decimal>>();

                decimal total = 0;

                #endregion

                bool res = Decimal.TryParse(budgetText.Text, out budget);

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

                foreach (Control control in budgetContainer.Controls)
                {
                    if (control is BudgetInputValue budgetInput)
                    {
                        decimal categoryBudget;
                        bool response = decimal.TryParse(budgetInput.input, out categoryBudget);

                        #region Category Input Validation
                        if (!response)
                        {
                            errorText.Text = "Invalid category budget input";
                            return;
                        }
                        else if (categoryBudget < 0)
                        {
                            errorText.Text = "Category Budget cannot be negative";
                            return;
                        }
                        #endregion

                        categoryBudgets.Add(new KeyValuePair<int, decimal>(budgetInput.categoryId, categoryBudget));

                        total += categoryBudget;
                    }
                }

                #endregion

                if (total > budget)
                {
                    errorText.Text = "Total Budget per Category must not exceed Budget";
                }
                else
                {
                    errorText.Text = "";
                    MessageBox.Show("Budget Saved");
                    model.SaveBudget(categoryBudgets, budget, false);
                }
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
            if(valueTypeDropdown.SelectedIndex == 0)
            {
                SaveBudgetPercent();
            }
            else
            {
                SaveBudgetValue();
            }
           
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
