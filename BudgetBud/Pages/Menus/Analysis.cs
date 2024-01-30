using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BudgetBud.Backend.Models;
using static BudgetBud.Backend.Models.BudgetModel;

namespace BudgetBud.Pages.Menus
{
    public partial class Analysis : UserControl
    {
        AnalysisModel model = new AnalysisModel();

        public Analysis()
        {
            InitializeComponent();

            GetData();
        }

        public void GetData()
        {
            model.GetData();

            this.totalExpensesLoggedText.Text = model.totalExpenseLogged.ToString();
            this.overallSpentText.Text = $"₱ {model.userTotalSpent}";
            this.favoriteText.Text = model.favoriteCategory;

            PopulateDoughnutChart();
            PopulateBarGraph();
        }

        #region Populate Charts

        public void PopulateDoughnutChart ()
        {
            // Clear existing data in the chart
            doughnutChart.Series.Clear();
            doughnutChart.Legends.Clear();

            // Create a new series
            Series doughnutSeries = new Series("ExpenseCount");
            doughnutSeries.ChartType = SeriesChartType.Doughnut;

            Legend legend = new Legend("Legend");

            foreach (KeyValuePair<int, string> expenseCount in model.expenseCount)
            {
                doughnutSeries.Points.AddXY(expenseCount.Value, expenseCount.Key);
            }

            doughnutChart.Series.Add(doughnutSeries);
            legend.Docking = Docking.Bottom;
            doughnutChart.Legends.Add(legend);
            doughnutChart.Legends[0].BackColor = Color.FromArgb(37, 42, 64);
            doughnutChart.Palette = ChartColorPalette.Berry;
        }

        public void PopulateBarGraph()
        {

            for (int i = 0; i < model.categoryTotalSpent.Count; i++)
            {
                // Use the Y value for the horizontal bar
                bargraphChart.Series[0].Points.Add(new DataPoint(i + 1, model.categoryTotalSpent[i].Key) { AxisLabel = model.categoryTotalSpent[i].Value });
            }

            bargraphChart.Refresh();
        }

        #endregion

        private void ChangeChartDate(DateTime from, DateTime to)
        {
            model.fromDate = from;
            model.toDate = to;

            GetData();

            bargraphChart.Refresh();
            doughnutChart.Refresh();

            this.totalExpensesLoggedText.Text = model.totalExpenseLogged.ToString();
            this.overallSpentText.Text = $"₱ {model.userTotalSpent}";
            this.favoriteText.Text = model.favoriteCategory;
        }

        private void ResetButtons()
        {
            thisMonthBtn.Enabled = true;
            todayBtn.Enabled = true;
            ThirtyDaysBtn.Enabled = true;
            sevenDaysBtn.Enabled = true;
        }

        private void thisMonthBtn_Click(object sender, EventArgs e)
        {
            ResetButtons();
            thisMonthBtn.Enabled = false;

            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime toDate = fromDate.AddMonths(1).AddDays(-1);

            ChangeChartDate(fromDate, toDate);
        }

        private void todayBtn_Click(object sender, EventArgs e)
        {
            ResetButtons();
            todayBtn.Enabled = false;

            DateTime fromDate = DateTime.Now.Date;
            DateTime toDate = fromDate.AddDays(1).AddTicks(-1);

            ChangeChartDate(fromDate, toDate);
        }

        private void ThirtyDaysBtn_Click(object sender, EventArgs e)
        {
            ResetButtons();
            ThirtyDaysBtn.Enabled = false;

            DateTime fromDate = DateTime.Now.AddDays(-30);
            DateTime toDate = DateTime.Now;

            ChangeChartDate(fromDate, toDate);
        }

        private void sevenDaysBtn_Click(object sender, EventArgs e)
        {
            ResetButtons();
            sevenDaysBtn.Enabled = false;

            DateTime fromDate = DateTime.Now.AddDays(-7);
            DateTime toDate = DateTime.Now;

            ChangeChartDate(fromDate, toDate);
        }

        private void customDateBtn_Click(object sender, EventArgs e)
        {
            ResetButtons();
            DateTime from = fromDate.Value;
            DateTime to = toDate.Value;

            ChangeChartDate(from, to);
        }
    }
}
