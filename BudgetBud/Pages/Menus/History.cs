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
    public partial class History : UserControl
    {
        #region Properties

        HistoryModel model = new HistoryModel();
        private List<int> categoryIds = new List<int>();
        private bool IsPlaceholder = true;
        private Main main { get; set; }

        #endregion

        public History()
        {
            InitializeComponent();
            GetData();
        }

        public History(Main main)
        {
            InitializeComponent();

            GetData();
            categoryDropdown.SelectedIndex = 0;

            this.main = main; // whats this??

            // Textbox Placeholders
            searchText.Text = "Search by title here...";
            searchText.GotFocus += RemoveText;
            searchText.LostFocus += AddText;
        }

        #region Text placeholder stuff

        public void RemoveText(object sender, EventArgs e)
        {
            if (searchText.Text == "Search by title here...")
            {
                searchText.Text = "";
                searchText.ForeColor = Color.Black;
                this.IsPlaceholder = false;
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchText.Text))
            {
                searchText.Text = "Search by title here...";
                searchText.ForeColor = Color.Gray;
                this.IsPlaceholder = true;
            }
        }

        #endregion

        public void GetData()
        {
            model.GetData();

            #region Initialize history table

            // Populate category filter dropdown
            if (model.categories.Count > 0)
            {
                categoryDropdown.Items.Add("None");
                categoryIds.Add(0);

                for (int i = 0; i < model.categories.Count; i++)
                {
                    categoryDropdown.Items.Add(model.categories[i].Value);
                    categoryIds.Add(model.categories[i].Key);
                }
            }


            // Determine if filters are enabled
            if (model.expenseTable.Rows.Count == 0)
            {
                applyFilterBtn.Enabled = false;
                searchBtn.Enabled = false;
                return;
            }

            dataGridView1.DataSource = model.expenseTable;

            // Set the height of each row
            dataGridView1.RowTemplate.Height = 50;

            #region Datagrid Edit and Delete Buttons

            // Customize column widths
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Edit button
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editButtonColumn);

            // Delete button
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButtonColumn);

            // Handle button clicks
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            #endregion

            #endregion
        }

        #region Helper

        private void UpdateDataGrid()
        {
            dataGridView1.DataSource = model.expenseTable;
        }

        #endregion

        #region Datagrid Button Onclicks
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                // Check which button is clicked
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if(senderGrid.Rows[e.RowIndex].Cells["ID"].Value == DBNull.Value)
                    {
                        return;
                    }

                    #region Category Properties
                    int expenseId = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["ID"].Value);
                    string title = senderGrid.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                    string desc = senderGrid.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                    decimal amount = Convert.ToDecimal(senderGrid.Rows[e.RowIndex].Cells["Amount"].Value);
                    string date = senderGrid.Rows[e.RowIndex].Cells["Date"].Value.ToString();
                    string category = senderGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString();
                    #endregion

                    // CHANGE index if possible
                    if (e.ColumnIndex == 0) // edit
                    {
                        using (HistoryEditExpense modal = new HistoryEditExpense(this.main, expenseId, category, title, desc, amount, date))
                        {
                            modal.ShowDialog();
                        }
                    }
                    else if (e.ColumnIndex == 1) // delete
                    {
                        DialogResult dr = MessageBox.Show("Are you sure you want to delete this expense?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if(dr == DialogResult.Yes)
                        {
                            model.DeleteExpense(expenseId);
                            main.ChangeMenu(new History(this.main));
                        }
                    }
                }
            }
        }

        #endregion

        #region Filter Button Onclicks
        private void applyFilterBtn_Click(object sender, EventArgs e)
        {
            if(categoryDropdown.SelectedIndex == 0)
            {
                model.CategoryId = null;
            }
            else
            {
                model.CategoryId = categoryIds[categoryDropdown.SelectedIndex];
            }

            model.FetchTable();
            UpdateDataGrid();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            model.ExpenseTitle = IsPlaceholder ? "" : searchText.Text;
            model.FetchTable();
            UpdateDataGrid();
        }

        #endregion
    }
}
