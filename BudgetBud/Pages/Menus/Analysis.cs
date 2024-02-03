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
            this.avgDailyText.Text = $"₱ {model.avgDailySpent}";

            PopulateDoughnutChart();
            PopulateBarGraph();
        }

        #region Populate Charts

        #region Doughnut 
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

            #region Style
            doughnutSeries.LabelForeColor = Color.White;
            doughnutSeries.Font = new Font("Nirmala UI", 14.25f, FontStyle.Bold); 
            legend.Font = new Font("Nirmala UI", 11.25f, FontStyle.Bold);
            legend.ForeColor = Color.Gainsboro;
            doughnutChart.Series.Add(doughnutSeries);
            legend.Docking = Docking.Bottom;
            doughnutChart.Legends.Add(legend);
            doughnutChart.Legends[0].BackColor = Color.FromArgb(37, 42, 64);
            doughnutChart.Palette = ChartColorPalette.Berry;
            #endregion

            doughnutChart.Refresh();
        }
        #endregion

        #region Bar Graph
        public void PopulateBarGraph()
        {
            // Clear existing data in the chart
            bargraphChart.Series.Clear();

            // Create a new series
            Series bargraphSeries = new Series("AmountSpentPerCategory");
            bargraphSeries.ChartType = SeriesChartType.Column;

            for (int i = 0; i < model.categoryTotalSpent.Count; i++)
            {
                // Add bars accordingly
                bargraphSeries.Points.Add(new DataPoint(i + 1, model.categoryTotalSpent[i].Key) { AxisLabel = model.categoryTotalSpent[i].Value });
            }

            bargraphChart.Series.Add(bargraphSeries);

            #region Style

            var xAxis = bargraphChart.ChartAreas[0].AxisX;
            var yAxis = bargraphChart.ChartAreas[0].AxisY;

            // Change the color of the gridlines
            xAxis.MajorGrid.LineColor = Color.Transparent;
            yAxis.MajorGrid.LineColor = Color.Transparent;

            // Change the color of the labels
            yAxis.LabelStyle.ForeColor = Color.Gainsboro; 
            xAxis.LabelStyle.ForeColor = Color.Gainsboro;

            // Change font of labels
            yAxis.LabelStyle.Font = new Font("Nirmala UI", 11.25f, FontStyle.Bold);
            xAxis.LabelStyle.Font = new Font("Nirmala UI", 11.25f, FontStyle.Bold);

            //bargraphChart.BorderlineColor = Color.Gainsboro;

            #endregion

            bargraphChart.Refresh();
        }
        #endregion

        #endregion

        #region Helper Functions
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
            this.avgDailyText.Text = $"₱ {model.avgDailySpent}";
        }

        private void ResetButtons()
        {
            thisMonthBtn.Enabled = true;
            todayBtn.Enabled = true;
            ThirtyDaysBtn.Enabled = true;
            sevenDaysBtn.Enabled = true;
        }

        #endregion

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
