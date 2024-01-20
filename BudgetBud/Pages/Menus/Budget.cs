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
        }

        #region Get Data from Database
        public void GetData()
        {
            model.GetData();

            // update label and center it
            this.monthText.Text = model.Month;
            this.panel2.Location = new Point((this.Width - this.label1.Width) / 2, this.panel2.Location.Y);

            #region Render Categories

            if (model.categories.Count > 0)
            {
                // Reverse traverse the array of categories
                for (int i = model.categories.Count - 1; i >= 0; i--)
                {
                    // get category data
                    int id = model.categories[i].Id;
                    string name = model.categories[i].Name;
                    int budget = model.categories[i].BudgetPercent;

                    BudgetInput input = new BudgetInput(id, name, budget);

                    input.Dock = DockStyle.Top;
                    budgetContainer.Controls.Add(input);
                }
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

        #region Save 
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveBtn.Enabled = false;

                #region Properties

                decimal budget;

                // CategoryId, CategoryBudget
                List<KeyValuePair<int, int>> categoryBudgets = new List<KeyValuePair<int, int>>();

                int total = 0;

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
                    if (control is BudgetInput budgetInput)
                    {
                        int categoryBudget;
                        bool response = int.TryParse(budgetInput.input, out categoryBudget);

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

                        categoryBudgets.Add(new KeyValuePair<int, int>(budgetInput.categoryId, categoryBudget));

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
                    model.SaveBudget(categoryBudgets, budget);
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
    }
}
