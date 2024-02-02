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

            this.spentText.Text = model.spent.ToString();
            this.availableText.Text = model.available.ToString();
            this.expenseCountText.Text = model.expenseCountToday.ToString();
            this.totalSpentText.Text = $"₱ {model.totalSpentToday}";
            this.fullNameText.Text = $"Hello, {UserContext.FullName}";

            // Populate chart
            foreach(Category categoryBudget in model.categoryBudgets)
            {
                doughnutChart.Series[0].Points.AddXY(categoryBudget.Name, categoryBudget.BudgetPercent);
            }

            doughnutChart.Refresh();
        }
    }
}
