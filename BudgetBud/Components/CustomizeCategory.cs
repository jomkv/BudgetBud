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
    public partial class CustomizeCategory : UserControl
    {
        public int id {  get; private set; }

        public CustomizeCategory()
        {
            InitializeComponent();
        }

        public CustomizeCategory(int id, string name)
        {
            InitializeComponent();
            this.nameText.Text = name;
            this.id = id;
        }
    }
}
