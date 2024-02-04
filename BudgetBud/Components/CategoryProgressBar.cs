using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetBud.Components
{
    public partial class CategoryProgressBar : UserControl
    {
        public CategoryProgressBar()
        {
            InitializeComponent();
        }

        public CategoryProgressBar(string name, int percent)
        {
            InitializeComponent();

            this.nameText.Text = name;

            if(percent > 100)
            {
                this.progressBar.Value = 100;
                this.label1.Visible = true;
            }
            else
            {
                this.progressBar.Value = percent;
            }
        }

        public CategoryProgressBar(string name, int percent, decimal spent, decimal remaining)
        {
            InitializeComponent();

            this.nameText.Text = name;
            this.budgetText.Text = $"₱ {spent} / ₱ {remaining}";

            if (percent > 100 || spent > remaining)
            {
                this.progressBar.Value = 100;

                // Show over budget
                this.label1.Visible = true;
            }
            else
            {
                this.progressBar.Value = percent;
            }
        }

    }
}
