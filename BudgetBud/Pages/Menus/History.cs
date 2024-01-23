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

namespace BudgetBud.Pages.Menus
{
    public partial class History : UserControl
    {
        HistoryModel model = new HistoryModel();

        public History()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            model.GetData();

            #region Initialize history table

            dataGridView1.DataSource = model.expenseTable;

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
        }

        // Table button onClick
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

                    int expenseId = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["ID"].Value);

                    // CHANGE index if possible
                    if (e.ColumnIndex == 0) // edit
                    {
                        Debug.WriteLine($"Edit, ExpenseId: {expenseId}");
                    }
                    else if (e.ColumnIndex == 1) // delete
                    {
                        DialogResult dr = MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if(dr == DialogResult.Yes)
                        {
                            model.DeleteExpense(expenseId);
                        }
                    }
                }
            }
        }
    }
}
