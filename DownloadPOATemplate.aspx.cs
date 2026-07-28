using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.Web.UI;

public partial class DownloadPOATemplate : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] == null) { Response.Redirect("login.aspx"); return; }

        ExcelPackage.License.SetNonCommercialPersonal("MnE Planning System");

        string templatePath = Server.MapPath("~/App_Data/Templates/POA_Upload_Template.xlsx");

        using (var pkg = new ExcelPackage())
        {
            BuildTemplateSheet(pkg);
            BuildInstructionsSheet(pkg);
            pkg.SaveAs(new System.IO.FileInfo(templatePath));
        }

        Response.Clear();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Disposition", "attachment; filename=POA_Upload_Template.xlsx");
        Response.TransmitFile(templatePath);
        Response.End();
    }

    // ── Colours ───────────────────────────────────────────────────────────
    private static readonly Color C_Primary    = ColorTranslator.FromHtml("#0C2D48");
    private static readonly Color C_Accent     = ColorTranslator.FromHtml("#12826A");
    private static readonly Color C_LabelBlue  = ColorTranslator.FromHtml("#1F4E79");
    private static readonly Color C_SubHdrBg   = ColorTranslator.FromHtml("#E8F5F1");
    private static readonly Color C_StripeBg   = ColorTranslator.FromHtml("#F5F9FF");
    private static readonly Color C_Grey       = ColorTranslator.FromHtml("#7F7F7F");
    private static readonly Color C_Border     = ColorTranslator.FromHtml("#CCCCCC");

    // ── POA sheet ─────────────────────────────────────────────────────────
    private void BuildTemplateSheet(ExcelPackage pkg)
    {
        var ws = pkg.Workbook.Worksheets.Add("POA");

        // ── 4 metadata rows ──────────────────────────────────────────────
        WriteMetaRow(ws, 1, "Provincial Development Plan Goal:",
            "[Enter the Provincial Development Plan goal statement here]");
        WriteMetaRow(ws, 2, "Priority Focus:",
            "[Enter the Priority Focus name here — e.g. Priority 1: Economic Transformation]");
        WriteMetaRow(ws, 3, "Integration Programme:",
            "[Enter the Integration Programme name here — this becomes the POA name]");
        WriteMetaRow(ws, 4, "Impact:",
            "[Enter the overall Impact Statement for this programme here]");

        // ── Row 5: Primary column headers ────────────────────────────────
        // Column order matches ReadPOAExcel expected layout:
        // A  Desired Outcome
        // B  Outcome Indicator
        // C  Indicator Type
        // D  PDP Fulfilment (merged D:E)
        // E  (merged with D)
        // F  Implementing Institution
        // G  # IE
        // H  Intervention
        // I  ID: IE
        // J  Intervention Indicator
        // K  Baseline 2023/24
        // L  Term Target 2025-2030
        // M  Target 2026/27
        // N  Annual Budget
        // O  Spatial Referencing

        string[] hdr = {
            "Desired Outcome",          // 1  A
            "Outcome Indicator",        // 2  B
            "Indicator Type",           // 3  C
            "PDP Fulfilment",           // 4  D  (merged D5:E5)
            "",                         // 5  E
            "Implementing Institution", // 6  F
            "# IE",                     // 7  G
            "Intervention",             // 8  H
            "ID: IE",                   // 9  I
            "Intervention Indicator",   // 10 J
            "Baseline 2023/24",         // 11 K
            "Term Target 2025-2030",    // 12 L
            "Target 2026/27",           // 13 M
            "Annual Budget",            // 14 N
            "Spatial Referencing"       // 15 O
        };

        for (int i = 0; i < hdr.Length; i++)
        {
            var cell = ws.Cells[5, i + 1];
            cell.Value = hdr[i];
            cell.Style.Font.Bold = true;
            cell.Style.Font.Size = 9;
            cell.Style.Font.Color.SetColor(Color.White);
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(C_Primary);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
            cell.Style.WrapText = true;
            ApplyThinBorder(cell);
        }
        ws.Cells["D5:E5"].Merge = true;
        ws.Row(5).Height = 30;

        // ── Row 6: PDP Fulfilment sub-headers ────────────────────────────
        ws.Cells["D6"].Value = "Baseline 2019/2020";
        ws.Cells["E6"].Value = "Target 2030";

        for (int c = 1; c <= 15; c++)
        {
            var cell = ws.Cells[6, c];
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(C_SubHdrBg);
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            cell.Style.Border.Bottom.Color.SetColor(C_Primary);
            ApplyThinBorder(cell);

            if (c == 4 || c == 5)
            {
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.Font.Color.SetColor(C_Accent);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        // ── Sample data rows 7-9 ─────────────────────────────────────────
        // Row 7: IE 1.1 — indicator 1 (full row, all fill-down cols populated)
        // Row 8: IE 1.1 — indicator 2 (fill-down cols left empty → system propagates)
        // Row 9: IE 1.2 — second intervention

        object[][] data = new object[][] {
            new object[] {
                "Increased economic participation and job creation",   // A Desired Outcome
                "Number of jobs created in IDZ/SEZ",                  // B Outcome Indicator
                "Output",                                              // C Indicator Type
                "45 000",                                             // D BaselinePDP
                "120 000",                                            // E TargetPDP2030
                "Eastern Cape Development Corporation",               // F Implementing Institution
                "IE 1.1",                                             // G # IE
                "IDZ and SEZ Infrastructure Development",             // H Intervention
                "IE-1.1",                                             // I ID: IE
                "Number of permanent jobs created in IDZ/SEZ",        // J Intervention Indicator
                "52 000",                                             // K Baseline 2023/24
                "120 000",                                            // L Term Target 2025-2030
                "65 000",                                             // M Target 2026/27
                "500 000 000",                                        // N Annual Budget
                "OR Tambo, Buffalo City Metro"                        // O Spatial Reference
            },
            new object[] {
                "",  // A fill-down
                "",  // B fill-down
                "",  // C fill-down
                "",  // D fill-down
                "",  // E fill-down
                "",  // F fill-down
                "",  // G fill-down
                "",  // H fill-down Intervention
                "",  // I fill-down ID
                "Number of SMMEs supported in IDZ/SEZ",  // J
                "120",       // K
                "500",       // L
                "180",       // M
                "25 000 000",// N
                "OR Tambo, Buffalo City Metro" // O
            },
            new object[] {
                "Improved access to basic services in rural areas",           // A
                "Percentage of rural households with access to basic services",// B
                "Outcome",                                                    // C
                "62%",                                                        // D
                "85%",                                                        // E
                "Dept of Rural Development and Land Reform (EC)",             // F
                "IE 1.2",                                                     // G
                "Rural Infrastructure Development Programme",                 // H
                "IE-1.2",                                                     // I
                "Number of rural roads upgraded to surfaced standard",        // J
                "15",         // K
                "50",         // L
                "22",         // M
                "300 000 000",// N
                "Alfred Nzo, Joe Gqabi"  // O
            }
        };

        for (int r = 0; r < data.Length; r++)
        {
            int rowNum = 7 + r;
            bool stripe = (r % 2 == 1);

            for (int c = 0; c < data[r].Length; c++)
            {
                var cell = ws.Cells[rowNum, c + 1];
                cell.Value = data[r][c];
                cell.Style.Font.Size = 9;
                cell.Style.WrapText = true;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                if (stripe)
                {
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(C_StripeBg);
                }

                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                cell.Style.Border.Bottom.Color.SetColor(C_Border);
                cell.Style.Border.Right.Style  = ExcelBorderStyle.Hair;
                cell.Style.Border.Right.Color.SetColor(C_Border);
            }
            ws.Row(rowNum).Height = 40;
        }

        // ── Blank input rows 10-30 ────────────────────────────────────────
        for (int r = 10; r <= 30; r++)
        {
            for (int c = 1; c <= 15; c++)
            {
                var cell = ws.Cells[r, c];
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                cell.Style.Border.Bottom.Color.SetColor(C_Border);
                cell.Style.Border.Right.Style  = ExcelBorderStyle.Hair;
                cell.Style.Border.Right.Color.SetColor(C_Border);
                cell.Style.WrapText = true;
            }
            ws.Row(r).Height = 30;
        }

        // ── Column widths ─────────────────────────────────────────────────
        ws.Column(1).Width  = 28; // Desired Outcome
        ws.Column(2).Width  = 28; // Outcome Indicator
        ws.Column(3).Width  = 13; // Indicator Type
        ws.Column(4).Width  = 17; // Baseline 2019/2020
        ws.Column(5).Width  = 13; // Target 2030
        ws.Column(6).Width  = 28; // Implementing Institution
        ws.Column(7).Width  = 9;  // # IE
        ws.Column(8).Width  = 34; // Intervention
        ws.Column(9).Width  = 10; // ID: IE
        ws.Column(10).Width = 34; // Intervention Indicator
        ws.Column(11).Width = 15; // Baseline 2023/24
        ws.Column(12).Width = 18; // Term Target 2025-2030
        ws.Column(13).Width = 14; // Target 2026/27
        ws.Column(14).Width = 16; // Annual Budget
        ws.Column(15).Width = 20; // Spatial Referencing

        // Freeze rows 1-6 (metadata + headers) so column labels stay visible while scrolling
        ws.View.FreezePanes(7, 1);
        ws.View.ShowGridLines = true;
    }

    // ── Instructions sheet ────────────────────────────────────────────────
    private void BuildInstructionsSheet(ExcelPackage pkg)
    {
        var ws = pkg.Workbook.Worksheets.Add("Instructions");
        ws.Column(1).Width = 22;
        ws.Column(2).Width = 70;

        Action<int, string, string, bool> row = (r, label, text, bold) =>
        {
            ws.Cells[r, 1].Value = label;
            ws.Cells[r, 2].Value = text;
            ws.Cells[r, 1].Style.Font.Bold = bold;
            ws.Cells[r, 2].Style.WrapText = true;
            ws.Row(r).Height = 30;
        };

        // Title
        ws.Cells["A1:B1"].Merge = true;
        ws.Cells["A1"].Value = "POA Upload Template — Instructions";
        ws.Cells["A1"].Style.Font.Bold = true;
        ws.Cells["A1"].Style.Font.Size = 14;
        ws.Cells["A1"].Style.Font.Color.SetColor(Color.White);
        ws.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells["A1"].Style.Fill.BackgroundColor.SetColor(C_Primary);
        ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Height = 32;

        int r2 = 3;
        row(r2++, "OVERVIEW", "", true);
        row(r2++, "", "This template is used to upload a Programme of Action (POA) into the Provincial M&E Planning System for review and approval. Fill in the POA sheet; do not rename it.", false);
        row(r2++, "", "", false);

        row(r2++, "METADATA (Rows 1–4)", "", true);
        row(r2++, "Row 1 — Goal",         "Replace the placeholder text in column B with the full Provincial Development Plan goal statement.", false);
        row(r2++, "Row 2 — Priority Focus","Enter the Priority Focus name exactly as it appears in the PMTDP (an approver will map it to the database record).", false);
        row(r2++, "Row 3 — Programme",    "Enter the Integration Programme name. This becomes the default POA name in the system — make it descriptive.", false);
        row(r2++, "Row 4 — Impact",       "Enter the overall impact statement for this programme.", false);
        row(r2++, "", "", false);

        row(r2++, "COLUMN HEADERS (Rows 5–6)", "", true);
        row(r2++, "Row 5",  "Primary headers. Do NOT change these column names — the system uses them to map data automatically.", false);
        row(r2++, "Row 6",  "Sub-headers under 'PDP Fulfilment'. Do NOT change.", false);
        row(r2++, "", "", false);

        row(r2++, "DATA (Row 7 onwards)", "", true);
        row(r2++, "One row = one indicator", "Each data row represents one Intervention Indicator. An intervention may have many indicators.", false);
        row(r2++, "", "", false);

        row(r2++, "FILL-DOWN COLUMNS", "", true);
        row(r2++, "",
            "The following columns use merged-cell fill-down: Desired Outcome, Outcome Indicator, Indicator Type, " +
            "Baseline 2019/2020, Target 2030, Implementing Institution, # IE, Intervention, ID: IE. " +
            "You only need to fill these in on the FIRST row of each group. Leave them blank on subsequent rows of the same Intervention — the system will carry the last value forward automatically.",
            false);
        row(r2++, "", "", false);

        row(r2++, "COLUMN GUIDE", "", true);
        string[][] cols2 = new string[][] {
            new string[] { "A  Desired Outcome",       "The high-level outcome this intervention contributes to. Fill on first row of each outcome group." },
            new string[] { "B  Outcome Indicator",     "The indicator used to measure the desired outcome. Fill on first row." },
            new string[] { "C  Indicator Type",        "Output, Outcome, or Impact." },
            new string[] { "D  Baseline 2019/2020",    "PDP Fulfilment baseline value at the start of the plan period." },
            new string[] { "E  Target 2030",           "PDP Fulfilment long-term target for 2030." },
            new string[] { "F  Institution",           "Full name of the implementing institution / department." },
            new string[] { "G  # IE",                  "Intervention number code, e.g. IE 1.1" },
            new string[] { "H  Intervention",          "Full name of the intervention. Fill on first indicator row; leave blank for subsequent rows of same intervention." },
            new string[] { "I  ID: IE",                "Short intervention ID code, e.g. IE-1.1" },
            new string[] { "J  Intervention Indicator","The specific measurable indicator for this intervention (one per row)." },
            new string[] { "K  Baseline 2023/24",      "Actual baseline value for the 2023/24 financial year." },
            new string[] { "L  Term Target 2025-2030", "Five-year term target description or value." },
            new string[] { "M  Target 2026/27",        "Annual target for the 2026/27 financial year." },
            new string[] { "N  Annual Budget",         "Indicative budget for this intervention (numeric, e.g. 500 000 000). Billions are supported." },
            new string[] { "O  Spatial Referencing",   "District municipality or geographic area of implementation." },
        };
        foreach (var pair in cols2)
        {
            ws.Cells[r2, 1].Value = pair[0];
            ws.Cells[r2, 1].Style.Font.Bold = true;
            ws.Cells[r2, 2].Value = pair[1];
            ws.Cells[r2, 2].Style.WrapText = true;
            ws.Row(r2).Height = 28;
            r2++;
        }

        // Colour all label cells
        for (int i = 3; i < r2; i++)
        {
            ws.Cells[i, 1].Style.Font.Color.SetColor(C_LabelBlue);
            ws.Cells[i, 2].Style.Font.Size = 9;
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────
    private void WriteMetaRow(ExcelWorksheet ws, int row, string label, string placeholder)
    {
        var labelCell = ws.Cells[row, 1];
        labelCell.Value = label;
        labelCell.Style.Font.Bold = true;
        labelCell.Style.Font.Color.SetColor(C_LabelBlue);
        labelCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        labelCell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF3FB"));

        var valCell = ws.Cells[row, 2];
        valCell.Value = placeholder;
        valCell.Style.Font.Italic = true;
        valCell.Style.Font.Color.SetColor(C_Grey);
        valCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        valCell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF3FB"));

        // Merge cols B through O so the placeholder text has room
        ws.Cells[row, 2, row, 15].Merge = true;
        ws.Cells[row, 2].Style.WrapText = false;

        ws.Row(row).Height = 20;
    }

    private void ApplyThinBorder(ExcelRange cell)
    {
        cell.Style.Border.Right.Style  = ExcelBorderStyle.Thin;
        cell.Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml("#1A4D6E"));
        cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        cell.Style.Border.Bottom.Color.SetColor(ColorTranslator.FromHtml("#1A4D6E"));
    }
}
