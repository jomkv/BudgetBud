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
    public partial class BudgetInputValue : UserControl
    {
        public int categoryId { get; private set; }
        public string input
        {
            get
            {
                return this.valueText.Text;
            }
            private set { }
        }

        public bool enable
        {
            get
            {
                return this.enable;
            }
            set
            {
                valueText.Enabled = value;

                if (value == true) // If enable
                {
                    categoryText.ForeColor = Color.Gainsboro;
                    label1.ForeColor = Color.Gainsboro;
                }
                else // If disable
                {
                    categoryText.ForeColor = Color.DarkGray;
                    label1.ForeColor = Color.DarkGray;
                }
            }
        }

        public BudgetInputValue()
        {
            InitializeComponent();
        }

        public BudgetInputValue(int categoryId, string categoryName, decimal value)
        {
            InitializeComponent();
            this.categoryText.Text = categoryName;
            this.categoryId = categoryId;
            this.valueText.Text = value.ToString();
        }
    }
}
