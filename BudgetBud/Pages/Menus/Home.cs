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
using BudgetBud.Backend.Models;
using BudgetBud.Components;

namespace BudgetBud.Pages.Menus
{
    public partial class Home : UserControl
    {
        HomeModel model = new HomeModel();

        public Home()
        {
            InitializeComponent();

            GetData();
        }

        private void GetData()
        {
            model.GetData();

            foreach (KeyValuePair<string, int> meter in model.expenseMeters)
            {
                CategoryProgressBar progress = new CategoryProgressBar(meter.Key, meter.Value);

                progress.Dock = DockStyle.Top;
                expenseMeterContainer.Controls.Add(progress);
            }

            this.budgetText.Text = $"₱ {model.budget}";
            this.spentText.Text = $"₱ {model.spent}";
            this.availableText.Text = $"₱ {model.available}";
        }
    }
}
