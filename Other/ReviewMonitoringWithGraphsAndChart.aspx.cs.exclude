using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

public partial class ReviewMonitoring : System.Web.UI.Page
{
    // Add missing control field so code-behind references compile.
    // If the designer file is present and up-to-date this is usually generated automatically.
    // Ensure the corresponding .aspx contains: <asp:Chart ID="chartPerformance" runat="server" />
    //protected Chart PerformanceChart;

    // Note: Targeting C# 5 (.NET 4.5). Ensure an <asp:Chart ID="chartPerformance" runat="server" />
    // exists in the .aspx markup to render the performance trend chart.

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadReports();
                // Optionally call PopulateChartWithSampleData() here for testing rendering
                PopulateChartWithSampleData();

                
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    
    private void LoadDropdowns()
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            SqlCommand cmd1 = new SqlCommand("SELECT InterventionID, InterventionName FROM new_Interventions", conn);
            ddlIntervention.DataSource = cmd1.ExecuteReader();
            ddlIntervention.DataTextField = "InterventionName";
            ddlIntervention.DataValueField = "InterventionID";
            ddlIntervention.DataBind();
            ddlIntervention.Items.Insert(0, new ListItem("All Interventions", ""));

            conn.Close();
            conn.Open();

            // Load clusters dropdown
            SqlCommand cmdCluster = new SqlCommand("SELECT ClusterID, ClusterName FROM new_Clusters", conn);
            ddlCluster.DataSource = cmdCluster.ExecuteReader();
            ddlCluster.DataTextField = "ClusterName";
            ddlCluster.DataValueField = "ClusterID";
            ddlCluster.DataBind();
            ddlCluster.Items.Insert(0, new ListItem("All Clusters", ""));

            conn.Close();
            conn.Open();

            SqlCommand cmd2 = new SqlCommand("SELECT FY_ID, FY_Name FROM new_FinancialYears", conn);
            ddlFinancialYear.DataSource = cmd2.ExecuteReader();
            ddlFinancialYear.DataTextField = "FY_Name";
            ddlFinancialYear.DataValueField = "FY_ID";
            ddlFinancialYear.DataBind();
            ddlFinancialYear.Items.Insert(0, new ListItem("All Years", ""));
        }
    }

    protected void ddlIntervention_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReports();
    }

    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReports();
    }

    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReports();
    }

    protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReports();
    }


    private void LoadReports()
    {
        List<reviewMonitoring> reports = reviewMonitoring.GetReports(
            ddlIntervention.SelectedValue,
            ddlFinancialYear.SelectedValue,
            ddlQuarter.SelectedValue,
            ddlCluster.SelectedValue
        );

        gvReports.DataSource = reports;
        gvReports.DataBind();

        lblSummary.Text = "Total Reports: " + reports.Count;

        // Bind chart view for performance trends
        BindPerformanceChart(reports);
    }

    private void BindPerformanceChart(List<reviewMonitoring> reports)
    {
        // Guard: chartPerformance is declared in the .aspx markup as <asp:Chart ID="chartPerformance" runat="server" />
        if (PerformanceChart == null)
            return;

        PerformanceChart.Series.Clear();
        PerformanceChart.ChartAreas.Clear();
        PerformanceChart.Titles.Clear();
        PerformanceChart.Legends.Clear();

        var area = new ChartArea("MainArea");
        area.AxisX.Interval = 1;
        area.AxisX.Title = "Quarter";
        area.AxisY.Title = "Performance Value";
        PerformanceChart.ChartAreas.Add(area);

        var seriesPlanned = new Series("Planned")
        {
            ChartType = SeriesChartType.Line,
            BorderWidth = 2,
            XValueType = ChartValueType.String,
            MarkerStyle = MarkerStyle.Circle,
            MarkerSize = 7
        };

        var seriesActual = new Series("Actual")
        {
            ChartType = SeriesChartType.Line,
            BorderWidth = 2,
            XValueType = ChartValueType.String,
            MarkerStyle = MarkerStyle.Diamond,
            MarkerSize = 7
        };

        // Aggregate by Quarter (compute average planned/actual performance per quarter)
        var grouped = reports
            .GroupBy(r => r.Quarter ?? string.Empty)
            .Select(g => new
            {
                Quarter = g.Key,
                AvgPlanned = g.Any() ? g.Average(x => x.PerformancePlannedValue) : 0m,
                AvgActual = g.Any() ? g.Average(x => x.PerformanceActualValue) : 0m
            })
            // Try to order quarters logically if they follow "Q1","Q2"... else fallback to alphabetical
            .OrderBy(x =>
            {
                int q;
                if (!string.IsNullOrEmpty(x.Quarter) && x.Quarter.StartsWith("Q") && int.TryParse(x.Quarter.Substring(1), out q))
                    return q;
                return int.MaxValue; // push non-Q labels to end alphabetically by second key
            })
            .ThenBy(x => x.Quarter)
            .ToList();

        // If there's no grouping (no Quarter distinct values), optionally use an index
        if (!grouped.Any() && reports.Any())
        {
            int idx = 1;
            foreach (var r in reports)
            {
                string label = string.IsNullOrEmpty(r.Quarter) ? ("P" + idx++) : r.Quarter;
                seriesPlanned.Points.AddXY(label, r.PerformancePlannedValue);
                seriesActual.Points.AddXY(label, r.PerformanceActualValue);
            }
        }
        else
        {
            foreach (var g in grouped)
            {
                seriesPlanned.Points.AddXY(g.Quarter, Convert.ToDouble(g.AvgPlanned));
                seriesActual.Points.AddXY(g.Quarter, Convert.ToDouble(g.AvgActual));
            }
        }

        seriesPlanned.ToolTip = "Planned: #VAL";
        seriesActual.ToolTip = "Actual: #VAL";

        PerformanceChart.Series.Add(seriesPlanned);
        PerformanceChart.Series.Add(seriesActual);

        var legend = new Legend("Legend");
        PerformanceChart.Legends.Add(legend);

        PerformanceChart.Titles.Add("Performance Trend (Planned vs Actual)");
        PerformanceChart.Width = 700;
        PerformanceChart.Height = 350;
    }

    // PSEUDOCODE / PLAN:
    // 1. Add a method `PopulateChartWithSampleData()` that can be called to force-populate `chartPerformance`
    //    with visible sample data so the chart renders points and legend correctly in the page.
    // 2. Guard against null `chartPerformance` so calling in design-time or tests is safe.
    // 3. Clear existing Series/ChartAreas/Titles/Legends to ensure a clean state.
    // 4. Create a ChartArea and configure Axis titles, intervals and grid lines for visibility.
    // 5. Create two Series ("Planned", "Actual") with Line chart type, marker styles, visible labels, and tooltips.
    // 6. Populate both series with explicit X/Y points (e.g., Q1..Q4 with sample numeric values).
    // 7. Add a Legend and Title, set chart dimensions and call `DataBind()` to ensure rendering.
    // 8. This method can be used to debug rendering or called from `Page_Load` when no real data is present.

    private void PopulateChartWithSampleData()
    {


        // Guard clause: ensure control exists
        if (PerformanceChart == null)
            return;

        // Reset chart
        PerformanceChart.Series.Clear();
        PerformanceChart.ChartAreas.Clear();
        PerformanceChart.Titles.Clear();
        PerformanceChart.Legends.Clear();

        // Create and configure chart area
        var area = new ChartArea("MainArea");
        area.AxisX.Interval = 1;
        area.AxisX.Title = "Quarter";
        area.AxisX.MajorGrid.Enabled = true;
        area.AxisY.Title = "Performance Value";
        area.AxisY.MajorGrid.Enabled = true;
        PerformanceChart.ChartAreas.Add(area);

        // Create Planned series
        var seriesPlanned = new Series("Planned")
        {
            ChartType = SeriesChartType.Line,
            BorderWidth = 2,
            XValueType = ChartValueType.String,
            MarkerStyle = MarkerStyle.Circle,
            MarkerSize = 8,
            IsValueShownAsLabel = true
        };

        // Create Actual series
        var seriesActual = new Series("Actual")
        {
            ChartType = SeriesChartType.Line,
            BorderWidth = 2,
            XValueType = ChartValueType.String,
            MarkerStyle = MarkerStyle.Diamond,
            MarkerSize = 8,
            IsValueShownAsLabel = true
        };

        // Example data (visible deterministic values)
        string[] quarters = new[] { "Q1", "Q2", "Q3", "Q4" };
        double[] plannedValues = new[] { 75.0, 80.5, 85.0, 90.0 };
        double[] actualValues  = new[] { 72.0, 82.0, 88.5, 92.0 };

        for (int i = 0; i < quarters.Length; i++)
        {
            seriesPlanned.Points.AddXY(quarters[i], plannedValues[i]);
            seriesActual.Points.AddXY(quarters[i], actualValues[i]);
        }

        // Tooltips and legend text
        seriesPlanned.ToolTip = "Planned: #VAL";
        seriesActual.ToolTip = "Actual: #VAL";
        seriesPlanned.LegendText = "Planned";
        seriesActual.LegendText = "Actual";

        // Add series to chart
        PerformanceChart.Series.Add(seriesPlanned);
        PerformanceChart.Series.Add(seriesActual);

        // Add legend and title
        var legend = new Legend("MainLegend")
        {
            Docking = Docking.Top
        };
        PerformanceChart.Legends.Add(legend);

        PerformanceChart.Titles.Add("Sample Performance Trend (Planned vs Actual)");

        // Optional: set size for predictable rendering
        PerformanceChart.Width = 700;
        PerformanceChart.Height = 350;

        // Ensure the chart binds the updated points
        PerformanceChart.DataBind();
    }

    protected void gvReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Add a header cell for Chart when header is bound
        if (e.Row.RowType == DataControlRowType.Header)
        {
            bool hasChartHeader = false;
            foreach (TableCell cell in e.Row.Cells)
            {
                if (!string.IsNullOrEmpty(cell.Text) && cell.Text.Trim().Equals("Chart", StringComparison.OrdinalIgnoreCase))
                {
                    hasChartHeader = true;
                    break;
                }
            }

            if (!hasChartHeader)
            {
                TableCell chartHeaderCell = new TableCell();
                chartHeaderCell.Text = "Chart";
                // Optionally set CSS class to align with header styles:
                // chartHeaderCell.CssClass = "gv-header-chart";
                e.Row.Cells.Add(chartHeaderCell);
            }

            return; // header handling done
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Existing deviation-based row coloring
            object devObj = DataBinder.Eval(e.Row.DataItem, "DeviationPercent");
            decimal deviation = 0m;
            if (devObj != null && devObj != DBNull.Value)
            {
                decimal.TryParse(devObj.ToString(), out deviation);
            }

            if (Math.Abs(deviation) <= 5)
                e.Row.CssClass = "status-green";
            else if (Math.Abs(deviation) <= 15)
                e.Row.CssClass = "status-yellow";
            else
                e.Row.CssClass = "status-red";

            // Try to find an existing Chart control placed in the .aspx (TemplateField with <asp:Chart ID="rowChart" runat="server" />)
            Chart rowChart = e.Row.FindControl("rowChart") as Chart;

            bool addedDynamically = false;
            // If not found, create one dynamically and add to a NEW cell (ensures separate column)
            if (rowChart == null)
            {
                rowChart = new Chart();
                rowChart.ID = "rowChart";
                addedDynamically = true;

                // Create a new TableCell specifically for the chart so it does not go into the Document column
                TableCell chartCell = new TableCell();
                // Optionally set style/class for alignment
                chartCell.VerticalAlign = VerticalAlign.Middle;
                chartCell.Wrap = false;
                chartCell.Controls.Add(rowChart);

                // Append the new cell to the row (this creates a separate column for charts)
                e.Row.Cells.Add(chartCell);

                // Ensure the GridView header has a matching "Chart" cell: if header exists and lacks Chart, add it
                if (gvReports != null && gvReports.HeaderRow != null)
                {
                    bool headerHasChart = false;
                    foreach (TableCell hdrCell in gvReports.HeaderRow.Cells)
                    {
                        if (!string.IsNullOrEmpty(hdrCell.Text) && hdrCell.Text.Trim().Equals("Chart", StringComparison.OrdinalIgnoreCase))
                        {
                            headerHasChart = true;
                            break;
                        }
                    }

                    if (!headerHasChart)
                    {
                        TableCell hdrChart = new TableCell();
                        hdrChart.Text = "Chart";
                        gvReports.HeaderRow.Cells.Add(hdrChart);
                    }
                }
            }

            // Clear any previous configuration
            rowChart.Series.Clear();
            rowChart.ChartAreas.Clear();
            rowChart.Titles.Clear();
            rowChart.Legends.Clear();

            // Read data values safely
            decimal planned = 0m;
            decimal actual = 0m;
            string quarter = string.Empty;

            object plannedObj = DataBinder.Eval(e.Row.DataItem, "PerformancePlannedValue");
            object actualObj = DataBinder.Eval(e.Row.DataItem, "PerformanceActualValue");
            object quarterObj = DataBinder.Eval(e.Row.DataItem, "Quarter");

            if (plannedObj != null && plannedObj != DBNull.Value)
                decimal.TryParse(plannedObj.ToString(), out planned);

            if (actualObj != null && actualObj != DBNull.Value)
                decimal.TryParse(actualObj.ToString(), out actual);

            if (quarterObj != null && quarterObj != DBNull.Value)
                quarter = quarterObj.ToString();

            // Configure chart area (compact)
            var area = new ChartArea("RowArea");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.LabelStyle.Enabled = true;
            area.AxisX.Title = ""; // keep compact
            area.AxisY.Title = "";
            area.BackColor = System.Drawing.Color.Transparent;
            rowChart.ChartAreas.Add(area);

            // Create series for Planned and Actual as side-by-side columns
            var seriesPlanned = new Series("Planned")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = false
            };
            seriesPlanned["PointWidth"] = "0.6";
            seriesPlanned.Color = System.Drawing.Color.FromArgb(79, 129, 189);

            var seriesActual = new Series("Actual")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = false
            };
            seriesActual["PointWidth"] = "0.6";
            seriesActual.Color = System.Drawing.Color.FromArgb(192, 80, 77);

            // X label: use quarter or a short fallback
            string xLabel = string.IsNullOrEmpty(quarter) ? "#" + e.Row.RowIndex.ToString() : quarter;

            // Add points (as doubles)
            seriesPlanned.Points.AddXY(xLabel, Convert.ToDouble(planned));
            seriesActual.Points.AddXY(xLabel, Convert.ToDouble(actual));

            seriesPlanned.ToolTip = "Planned: #VAL";
            seriesActual.ToolTip = "Actual: #VAL";

            rowChart.Series.Add(seriesPlanned);
            rowChart.Series.Add(seriesActual);

            // Minimal legend
            var legend = new Legend("rLegend")
            {
                Docking = Docking.Bottom,
                IsDockedInsideChartArea = false,
                Enabled = true
            };
            rowChart.Legends.Add(legend);

            // Size for inline display
            rowChart.Width = Unit.Pixel(180);
            rowChart.Height = Unit.Pixel(80);
            rowChart.BackColor = System.Drawing.Color.Transparent;

            // Ensure data is bound
            rowChart.DataBind();
        }
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        List<reviewMonitoring> reports = reviewMonitoring.GetReports(
            ddlIntervention.SelectedValue,
            ddlFinancialYear.SelectedValue,
            ddlQuarter.SelectedValue,
            ddlCluster.SelectedValue
        );

        cls_ExportHelper.ExportToExcel(reports, "Quarterly_Report_Review.xlsx");
    }

    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        List<reviewMonitoring> reports = reviewMonitoring.GetReports(
            ddlIntervention.SelectedValue,
            ddlFinancialYear.SelectedValue,
            ddlQuarter.SelectedValue,
            ddlCluster.SelectedValue
        );

        cls_ExportHelper.ExportToPDF(reports, "Quarterly_Report_Review.pdf");
    }


}