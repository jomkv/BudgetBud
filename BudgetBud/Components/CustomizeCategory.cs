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

namespace BudgetBud.Components
{
    public partial class CustomizeCategory : UserControl
    {
        CategoriesModel model = new CategoriesModel();

        public int id {  get; private set; }
        public string name { get; private set; }

        public CustomizeCategory()
        {
            InitializeComponent();
        }

        public CustomizeCategory(int id, string name)
        {
            InitializeComponent();
            this.nameText.Text = name;
            this.name = name;
            this.id = id;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this category? This action will permanently remove the category and all associated expenses. Once deleted, this data cannot be recovered", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dr == DialogResult.Yes)
            {
                model.DeleteCategory(this.id);
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            using (CategoryEditModal modal = new CategoryEditModal(this.name, this.id))
            {
                modal.ShowDialog(this);
            }
        }
    }
}
