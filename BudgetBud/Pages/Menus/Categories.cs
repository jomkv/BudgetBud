using BudgetBud.Backend.Models;
using BudgetBud.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetBud.Pages.Menus
{
    public partial class Categories : UserControl
    {
        CategoriesModel model = new CategoriesModel();

        public Categories()
        {
            InitializeComponent();
            getData();
        }

        private void getData()
        {
            model.GetData();

            bool allowDelete = (model.categoryCount <= 3) ? false : true;

            if(model.categories != null) 
            {
                foreach(KeyValuePair<int, string> category in model.categories)
                {
                    int id = category.Key;
                    string name = category.Value;

                    CustomizeCategory customizeCategory = new CustomizeCategory(id, name, allowDelete);
                    customizeCategory.Dock = DockStyle.Top;

                    categoriesPanel.Controls.Add(customizeCategory);
                }
            }

            if(model.categoryCount >= 8)
            {
                createCategoryBtn.Visible = false;
            }
        }

        private void createCategoryBtn_Click(object sender, EventArgs e)
        {
            using (CreateCategoryModal modal = new CreateCategoryModal())
            {
                modal.ShowDialog(this);
            }
        }
    }
}
