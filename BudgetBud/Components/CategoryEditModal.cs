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
using BudgetBud.Backend.Models;

namespace BudgetBud.Components
{
    public partial class CategoryEditModal : Form
    {
        CategoriesModel model = new CategoriesModel();

        private int categoryId { get; set; }
        public CategoryEditModal()
        {
            InitializeComponent();
        }

        public CategoryEditModal(string name, int id)
        {
            InitializeComponent();
            this.nameText.Text = name;
            this.categoryId = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CategoryEditModal_Load(object sender, EventArgs e)
        {
            
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            model.EditCategory(this.categoryId, this.nameText.Text);
            this.Close();
        }
    }
}
