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
            filterDropdown.SelectedIndex = 0;
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
            doughnutSeries.IsValueShownAsLabel = true;
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
        private void ReloadDatas()
        {
            GetData();

            bargraphChart.Refresh();
            doughnutChart.Refresh();

            this.totalExpensesLoggedText.Text = model.totalExpenseLogged.ToString();
            this.overallSpentText.Text = $"₱ {model.userTotalSpent}";
            this.favoriteText.Text = model.favoriteCategory;
            this.avgDailyText.Text = $"₱ {model.avgDailySpent}";
        }

        #endregion

        private void customDateBtn_Click(object sender, EventArgs e)
        {
            model.fromDate = fromDate.Value;
            model.toDate = toDate.Value;

            ReloadDatas();
        }

        private void handleFilterChange(object sender, EventArgs e)
        {
            int index = filterDropdown.SelectedIndex;

            switch(index)
            {
                case 0: // All time
                    model.fromDate = DateTime.MinValue; 
                    model.toDate = DateTime.MaxValue;
                    break;
                case 1: // This Month
                    model.fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    model.toDate = model.fromDate.AddMonths(1).AddDays(-1);
                    break;
                case 2: // Last 30 Days
                    model.fromDate = DateTime.Now.AddDays(-30);
                    model.toDate = DateTime.Now;
                    break;
                case 3: // Last 7 Days
                    model.fromDate = DateTime.Now.AddDays(-7);
                    model.toDate = DateTime.Now;
                    break;
                case 4: // Today
                    model.fromDate = DateTime.Now.Date;
                    model.toDate = model.fromDate.AddDays(1).AddTicks(-1);
                    break;
                default: // Default to all time
                    model.fromDate = DateTime.MinValue;
                    model.toDate = DateTime.MaxValue;
                    break;
            }

            ReloadDatas();
        }
    }
}
