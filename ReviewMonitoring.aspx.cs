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
using System.Drawing;

public partial class ReviewMonitoring : System.Web.UI.Page
{
    // Add missing control field so code-behind references compile.
    // If the designer file is present and up-to-date this is usually generated automatically.
    // Ensure the corresponding .aspx contains: <asp:Chart ID="chartPerformance" runat="server" />
    //protected Chart PerformanceChart;

    // Color constants (hex) used for icons/badges
    private const string PlannedHex = "#5B9BD5"; // RGB(91,155,213)
    private const string ActualHex = "#ED7D31";  // RGB(237,125,49)

    private int _interventionColIndex = -1;
    private GridViewRow _lastInterventionRow = null;
    private string _lastInterventionName = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadReports();
                // Legend rendering moved into LoadReports so it follows gvReports content on postbacks.
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

            SqlCommand cmd1 = new SqlCommand("SELECT InterventionID, InterventionName FROM new_Interventions WHERE IsApproved = 1", conn);
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

        // Update dropdown borders after binding so initial UI reflects selection
        UpdateDropdownBorders();
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

        // Show legend only when there is content to display
        RenderColorLegend(reports != null && reports.Count > 0);

        // Ensure dropdown borders reflect current selections (runs on every report load/postback)
        UpdateDropdownBorders();

        // Bind chart view for performance trends
        //BindPerformanceChart(reports);
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
    // 1. Add helper methods `GetColorBadgeHtml` and `GetColorIconHtml` to render small colored badges/icons as HTML.
    // 2. Modify `gvReports_RowDataBound` header handling to append a "Legend" header cell containing the Planned/Actual badges and labels.
    // 3. For each DataRow, insert small color icons (with tooltip) inside the trend/chart cell so the planned (blue) and actual (orange) colors are visible next to the mini-chart.
    // 4. Provide `GetColorLegendHtml()` to return a reusable HTML snippet that can be used anywhere outside the GridView (e.g., a Literal or Panel in the .aspx).
    // 5. Add `RenderColorLegend()` to attempt to locate a literal/label/panel named `litColorLegend`, `lblColorLegend` or `pnlColorLegend` and set its InnerHtml/Text; this is a safe no-op if those controls aren't present.
    // 6. Keep existing behavior intact and guard against missing controls.

    private string GetColorBadgeHtml(string hex, string label)
    {
        // Badge + text (for use in headers/legend)
        return string.Format(
            "<span style='display:inline-block;width:12px;height:12px;background:{0};border-radius:50%;margin-right:6px;vertical-align:middle;'></span><span style='margin-right:12px;vertical-align:middle;font-size:0.9em;color:#333;'>{1}</span>",
            HttpUtility.HtmlEncode(hex),
            HttpUtility.HtmlEncode(label)
        );
    }

    private string GetColorIconHtml(string hex, string tooltip)
    {
        // Small icon only (for per-row compact display)
        return string.Format(
            "<span title='{1}' style='display:inline-block;width:10px;height:10px;background:{0};border-radius:50%;margin-right:6px;vertical-align:middle;'></span>",
            HttpUtility.HtmlEncode(hex),
            HttpUtility.HtmlEncode(tooltip)
        );
    }

    public string GetColorLegendHtml()
    {
        // Returns HTML that can be used anywhere in the page markup
        return "<div class='color-legend' style='font-family:Arial,sans-serif;font-size:12px;line-height:18px;'>" +
               GetColorBadgeHtml(PlannedHex, "Planned") +
               GetColorBadgeHtml(ActualHex, "Actual") +
               "</div>";
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root == null) return null;
        Control c = root.FindControl(id);
        if (c != null) return c;
        foreach (Control child in root.Controls)
        {
            c = FindControlRecursive(child, id);
            if (c != null) return c;
        }
        return null;
    }

    // Replace RenderColorLegend with a version that can add/update/remove a persistent legend control
    private void RenderColorLegend(bool visible)
    {
        string html = GetColorLegendHtml();

        // 1) Look for explicit literal control named 'litColorLegend'
        var lit = FindControlRecursive(Page, "litColorLegend") as Literal;
        if (lit != null)
        {
            lit.Text = visible ? html : string.Empty;
            return;
        }

        // 2) Look for explicit label control named 'lblColorLegend'
        var lbl = FindControlRecursive(Page, "lblColorLegend") as Label;
        if (lbl != null)
        {
            lbl.Text = visible ? html : string.Empty;
            return;
        }

        // 3) Look for explicit panel control named 'pnlColorLegend' and add/remove an inner Literal with fixed ID
        var pnl = FindControlRecursive(Page, "pnlColorLegend") as Panel;
        if (pnl != null)
        {
            var existing = pnl.FindControl("colorLegendInserted") as Literal;
            if (visible)
            {
                if (existing == null)
                {
                    pnl.Controls.Add(new Literal { ID = "colorLegendInserted", Text = html });
                }
                else
                {
                    existing.Text = html;
                }
            }
            else
            {
                if (existing != null) pnl.Controls.Remove(existing);
            }
            return;
        }

        // 4) Insert the legend immediately before the GridView (gvReports) in its parent's Controls collection.
        var gv = FindControlRecursive(Page, "gvReports") as GridView;
        if (gv != null && gv.Parent != null)
        {
            Control parent = gv.Parent;
            var existing = parent.FindControl("colorLegendInserted") as Literal;
            string wrappedHtml = "<div style='text-align:right;margin-bottom:8px;'>" + html + "</div>";

            if (visible)
            {
                if (existing == null)
                {
                    try
                    {
                        int idx = parent.Controls.IndexOf(gv);
                        if (idx >= 0)
                            parent.Controls.AddAt(idx, new Literal { ID = "colorLegendInserted", Text = wrappedHtml });
                        else
                            parent.Controls.Add(new Literal { ID = "colorLegendInserted", Text = wrappedHtml });
                    }
                    catch
                    {
                        // Safe no-op on failure to avoid breaking page rendering.
                    }
                }
                else
                {
                    existing.Text = wrappedHtml;
                }
            }
            else
            {
                try
                {
                    if (existing != null) parent.Controls.Remove(existing);
                }
                catch
                {
                    // safe-noop
                }
            }
            return;
        }

        // 5) If no suitable container was found, do nothing (safe no-op).
    }

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

    // NEW: Update dropdown border helpers
    private void SetDropdownBorder(DropDownList ddl)
    {
        if (ddl == null) return;

        try
        {
            // Consider the dropdown "selected" (should have red border) when:
            // - SelectedIndex > 0 (most typical when "All ..." placeholder is at index 0), OR
            // - SelectedValue is non-empty and not equal to "0" (covers cases where value is an empty string or "0")
            bool hasSelection = false;

            try
            {
                if (ddl.SelectedIndex > 0)
                    hasSelection = true;
                else
                {
                    var val = ddl.SelectedValue;
                    if (!string.IsNullOrEmpty(val) && val != "0")
                        hasSelection = true;
                }
            }
            catch
            {
                // If accessing SelectedIndex/SelectedValue fails for any reason, fall back to no selection.
                hasSelection = false;
            }

            if (hasSelection)
                //ddl.Style["border"] = "2px solid #ff0000";
                ddl.Style["border"] = "3px solid #03ac13";
            else
            {
                // Remove border if previously set
                if (ddl.Style["border"] != null)
                    ddl.Style.Remove("border");
            }
        }
        catch
        {
            // safe-noop: do not break page rendering if style operations fail
        }
    }

    private void UpdateDropdownBorders()
    {
        // Apply consistent red-border logic to all relevant dropdowns
        SetDropdownBorder(ddlCluster);
        SetDropdownBorder(ddlIntervention);
        SetDropdownBorder(ddlFinancialYear);
        SetDropdownBorder(ddlQuarter);
    }

    protected void gvReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Header handling: add Trend header and reset merge trackers
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableHeaderCell th = new TableHeaderCell();
            th.Text = "Trend";
            th.CssClass = "gv-trend-header";
            e.Row.Cells.Add(th);

            // Reset trackers at header creation so merging starts fresh per bind
            _interventionColIndex = -1;
            _lastInterventionRow = null;
            _lastInterventionName = null;
            return;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Status coloring based on DeviationPercent
            decimal deviation = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DeviationPercent"));
            if (Math.Abs(deviation) <= 5)
                e.Row.CssClass = "status-green";
            else if (Math.Abs(deviation) <= 15)
                e.Row.CssClass = "status-yellow";
            else
                e.Row.CssClass = "status-red";

            // Prepare the intervention name (source of truth for merging)
            string currentInterventionName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InterventionName") ?? string.Empty).Trim();

            // Replace the Intervention cell text with a hyperlink to the detail page (existing behavior)
            try
            {
                string interventionName = currentInterventionName;
                string interventionId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InterventionID") ?? string.Empty).Trim();
                string clusterId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ClusterID") ?? string.Empty).Trim();

                if (!string.IsNullOrEmpty(interventionName))
                {
                    var queryParts = new List<string>();
                    if (!string.IsNullOrEmpty(interventionId))
                        queryParts.Add("id=" + HttpUtility.UrlEncode(interventionId));
                    if (!string.IsNullOrEmpty(clusterId))
                        queryParts.Add("cls=" + HttpUtility.UrlEncode(clusterId));

                    string url = "pageInterventionsDirectDetail.aspx";
                    if (queryParts.Count > 0)
                        url += "?" + string.Join("&", queryParts);

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        string cellText = HttpUtility.HtmlDecode(e.Row.Cells[i].Text ?? string.Empty).Trim();
                        if (!string.IsNullOrEmpty(cellText) && cellText.Equals(interventionName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            e.Row.Cells[i].Controls.Clear();
                            var hl = new HyperLink
                            {
                                NavigateUrl = url,
                                Text = interventionName
                            };
                            e.Row.Cells[i].Controls.Add(hl);
                            break;
                        }
                    }
                }
            }
            catch
            {
                // ignore errors while creating hyperlink
            }

            // --- MERGE LOGIC FOR `InterventionName` COLUMN ONLY ---
            // 1) Ensure we know the column index for the InterventionName cell.
            if (_interventionColIndex == -1)
            {
                // Try to find by checking cells for either a HyperLink with matching text or cell.Text matching the current name.
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    // Check controls first (HyperLink scenario)
                    foreach (Control ctrl in e.Row.Cells[i].Controls)
                    {
                        var hl = ctrl as HyperLink;
                        if (hl != null && !string.IsNullOrEmpty(currentInterventionName) &&
                            hl.Text.Trim().Equals(currentInterventionName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            _interventionColIndex = i;
                            break;
                        }
                    }
                    if (_interventionColIndex != -1) break;

                    // Fallback: check cell text
                    string cellText = HttpUtility.HtmlDecode(e.Row.Cells[i].Text ?? string.Empty).Trim();
                    if (!string.IsNullOrEmpty(currentInterventionName) &&
                        cellText.Equals(currentInterventionName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _interventionColIndex = i;
                        break;
                    }
                }

                // Last resort: try to infer from header if header row exists and contains "Intervention"
                if (_interventionColIndex == -1 && gvReports.HeaderRow != null)
                {
                    for (int i = 0; i < gvReports.HeaderRow.Cells.Count; i++)
                    {
                        string headerText = HttpUtility.HtmlDecode(gvReports.HeaderRow.Cells[i].Text ?? string.Empty).Trim();
                        if (headerText.IndexOf("Intervention", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                            headerText.IndexOf("InterventionName", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            _interventionColIndex = i;
                            break;
                        }
                    }
                }
            }

            // 2) If we found the column index, perform vertical merging for consecutive identical `InterventionName`
            if (_interventionColIndex >= 0 && _interventionColIndex < e.Row.Cells.Count)
            {
                if (_lastInterventionRow == null)
                {
                    // First encountered row -- set as the current "master" for potential merges
                    _lastInterventionRow = e.Row;
                    _lastInterventionName = currentInterventionName;
                }
                else
                {
                    // If same name as previous encountered, merge current cell into the previous master cell
                    if (!string.IsNullOrEmpty(currentInterventionName) &&
                        string.Equals(_lastInterventionName, currentInterventionName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        try
                        {
                            TableCell lastCell = _lastInterventionRow.Cells[_interventionColIndex];
                            TableCell currCell = e.Row.Cells[_interventionColIndex];

                            // Ensure RowSpan starts at 1 if previously default
                            if (lastCell.RowSpan < 1) lastCell.RowSpan = 1;
                            int add = (currCell.RowSpan < 1) ? 1 : currCell.RowSpan;
                            lastCell.RowSpan += add;

                            // Hide the current duplicate cell so the visual effect is a merged cell
                            currCell.Visible = false;
                        }
                        catch
                        {
                            // Safe-noop on any merge failure; do not interrupt rendering
                        }
                    }
                    else
                    {
                        // Different intervention name -> start a new master row
                        _lastInterventionRow = e.Row;
                        _lastInterventionName = currentInterventionName;
                    }
                }
            }

            // --- Existing trend/chart insertion (unchanged) ---
            try
            {
                object plannedObj = DataBinder.Eval(e.Row.DataItem, "PerformancePlannedValue") ?? DataBinder.Eval(e.Row.DataItem, "PlannedExpenditure");
                object actualObj = DataBinder.Eval(e.Row.DataItem, "PerformanceActualValue") ?? DataBinder.Eval(e.Row.DataItem, "ActualExpenditure");

                double plannedVal = 0.0;
                double actualVal = 0.0;

                decimal pdec;
                decimal adec;

                if (plannedObj != null && decimal.TryParse(plannedObj.ToString(), out pdec))
                    plannedVal = Convert.ToDouble(pdec);

                if (actualObj != null && decimal.TryParse(actualObj.ToString(), out adec))
                    actualVal = Convert.ToDouble(adec);

                Chart miniChart = new Chart();
                miniChart.Width = 220;
                miniChart.Height = 80;
                miniChart.BackColor = Color.Transparent;
                miniChart.BorderlineDashStyle = ChartDashStyle.Solid;
                miniChart.BorderlineColor = Color.Transparent;

                ChartArea ca = new ChartArea("ca");
                ca.BackColor = Color.Transparent;
                ca.Position = new ElementPosition(5, 5, 90, 90);
                ca.InnerPlotPosition = new ElementPosition(10, 5, 85, 85);
                ca.AxisX.MajorGrid.Enabled = false;
                ca.AxisY.MajorGrid.Enabled = false;
                ca.AxisX.LabelStyle.Enabled = false;
                ca.AxisY.LabelStyle.Enabled = false;
                ca.AxisX.LineWidth = 0;
                ca.AxisY.LineWidth = 0;
                ca.AxisY.Enabled = AxisEnabled.False;
                miniChart.ChartAreas.Add(ca);

                Series sPlanned = new Series("Planned")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(91, 155, 213),
                    BorderWidth = 0,
                    IsValueShownAsLabel = false,
                    ToolTip = "Planned: #VAL"
                };

                Series sActual = new Series("Actual")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(237, 125, 49),
                    BorderWidth = 0,
                    IsValueShownAsLabel = false,
                    ToolTip = "Actual: #VAL"
                };

                sPlanned.Points.AddXY("P", plannedVal);
                sActual.Points.AddXY("A", actualVal);

                sPlanned["PointWidth"] = "0.6";
                sActual["PointWidth"] = "0.6";

                miniChart.Series.Add(sPlanned);
                miniChart.Series.Add(sActual);

                TableCell chartCell = new TableCell();

                string compactIconsHtml = "<div style='margin-bottom:4px;'>" +
                                      GetColorIconHtml(PlannedHex, "Planned") +
                                      "<span style='font-size:11px;color:#333;vertical-align:middle;margin-right:8px;'>" + HttpUtility.HtmlEncode(plannedVal.ToString("0.##")) + "</span>" +
                                      GetColorIconHtml(ActualHex, "Actual") +
                                      "<span style='font-size:11px;color:#333;vertical-align:middle;'>" + HttpUtility.HtmlEncode(actualVal.ToString("0.##")) + "</span>" +
                                      "</div>";

                chartCell.Controls.Add(new LiteralControl(compactIconsHtml));
                chartCell.Controls.Add(miniChart);
                chartCell.CssClass = "gv-trend-cell";
                e.Row.Cells.Add(chartCell);
            }
            catch
            {
                TableCell emptyCell = new TableCell();
                emptyCell.Text = "";
                e.Row.Cells.Add(emptyCell);
            }
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