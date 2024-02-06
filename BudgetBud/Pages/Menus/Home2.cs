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
using BudgetBud.Backend;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace BudgetBud.Pages.Menus
{
    public partial class Home2 : UserControl
    {
        HomeModel model = new HomeModel();

        public Home2()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            model.GetData();

            this.spentText.Text = $"₱ {model.spent}";
            this.availableText.Text = $"₱ {model.available}";
            this.expenseCountText.Text = model.expenseCountToday.ToString();
            this.totalSpentText.Text = $"₱ {model.totalSpentToday}";
            this.fullNameText.Text = $"Hello, {UserContext.FullName}";
            this.monthBudgetText.Text = $"₱ {model.budget}";

            // Populate chart
            foreach(Category categoryBudget in model.categoryBudgets)
            {
                if(categoryBudget.BudgetPercent > 0)
                {
                    int index = doughnutChart.Series[0].Points.AddXY(categoryBudget.Name, categoryBudget.BudgetPercent);

                    // Access the DataPoint using the index
                    DataPoint dataPoint = doughnutChart.Series[0].Points[index];
                    dataPoint.Label = dataPoint.YValues[0].ToString("#,##0.##") + "%";
                    dataPoint.LegendText = categoryBudget.Name;

                    // Check if the category is "Unallocated"
                    if (categoryBudget.Name.Equals("Unallocated", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set a specific color for the "Unallocated" category
                        dataPoint.Color = Color.Gray;
                    }
                }
            }

            doughnutChart.Refresh();
        }
    }
}
