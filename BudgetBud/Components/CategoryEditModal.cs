using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Pages;

namespace BudgetBud.Components
{
    public partial class CategoryEditModal : Form
    {
        public CategoryEditModal()
        {
            InitializeComponent();
        }

        public CategoryEditModal(string name)
        {
            InitializeComponent();
            this.nameText.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CategoryEditModal_Load(object sender, EventArgs e)
        {
            
        }
    }
}
