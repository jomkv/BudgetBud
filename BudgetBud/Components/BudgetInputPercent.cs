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
    public partial class BudgetInputPercent : UserControl
    {
        public int categoryId { get; private set; }
        public string input
        {
            get
            {
                return this.percentText.Text;
            }
            private set { }
        }

        public BudgetInputPercent()
        {
            InitializeComponent();
        }

        public BudgetInputPercent(int categoryId, string categoryName, decimal percent)
        {
            InitializeComponent();
            this.categoryText.Text = categoryName;
            this.categoryId = categoryId;
            this.percentText.Text = percent.ToString();
        }
    }
}
