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
            percentRadio.Checked = true;
            UpdateFormState();
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
                percentContainer.Controls.Clear();
                exactContainer.Controls.Clear();

                // Reverse traverse the array of categories
                for (int i = model.categories.Count - 1; i >= 0; i--)
                {
                    // get category data
                    int id = model.categories[i].Id;
                    string name = model.categories[i].Name;
                    decimal budgetPercent = model.categories[i].BudgetPercent;
                    decimal budgetAmount = model.categories[i].BudgetValue;

                    BudgetInputPercent inputPercent = new BudgetInputPercent(id, name, budgetPercent);
                    BudgetInputValue inputValue = new BudgetInputValue(id, name, budgetAmount);

                    inputPercent.Dock = DockStyle.Top;
                    inputValue.Dock = DockStyle.Top;
                    percentContainer.Controls.Add(inputPercent);
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

        public void UpdateFormState()
        {
            foreach (Control control in percentContainer.Controls)
            {
                if (control is BudgetInputPercent inputPercent)
                {
                    inputPercent.enable = percentRadio.Checked;
                }
            }
            foreach (Control control in exactContainer.Controls)
            {
                if (control is BudgetInputValue inputValue)
                {
                    inputValue.enable = valueRadio.Checked;
                }
            }
        }

        #endregion

        private void BudgetPage_Load(object sender, EventArgs e)
        {

        }

        #region Save 
        private void SaveBudget()
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

                if(percentRadio.Checked)
                {
                    #region Percent 

                    foreach (Control control in percentContainer.Controls)
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
                    }

                    if(total > 100)
                    {
                        errorText.Text = "Total Percent must not exceed 0";
                        return;
                    }

                    #endregion
                }
                else if (valueRadio.Checked)
                {
                    #region Exact Value

                    foreach (Control control in exactContainer.Controls)
                    {
                        if(control is BudgetInputValue budgetInputValue)
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

                    if (total > budget)
                    {
                        errorText.Text = "Total Amount must not exceed Budget";
                        return;
                    }

                    #endregion
                }

                #endregion

                errorText.Text = "";
                model.SaveBudget(categoryBudgets, budget);
                MessageBox.Show("Budget Saved");
                GetData();
                UpdateFormState();
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
            SaveBudget();
        }
        #endregion

        private void HandleRadioChange(object sender, EventArgs e)
        {
            UpdateFormState();
        }
    }
}
