using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

public static class cls_ExportHelper
{
    public static void ExportToExcel(List<reviewMonitoring> reports, string fileName)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Quarterly Reports");

            // Header
            worksheet.Cell(1, 1).Value = "Intervention";
            worksheet.Cell(1, 2).Value = "Quarter";
            worksheet.Cell(1, 3).Value = "Planned Expenditure";
            worksheet.Cell(1, 4).Value = "Actual Expenditure";
            worksheet.Cell(1, 5).Value = "Target";
            worksheet.Cell(1, 6).Value = "Achieved";
            worksheet.Cell(1, 7).Value = "Deviation (%)";
            worksheet.Cell(1, 8).Value = "Document";

            // Data
            int row = 2;
            foreach (var r in reports)
            {
                worksheet.Cell(row, 1).Value = r.InterventionName;
                worksheet.Cell(row, 2).Value = r.Quarter;
                worksheet.Cell(row, 3).Value = r.PlannedExpenditure;
                worksheet.Cell(row, 4).Value = r.ActualExpenditure;
                worksheet.Cell(row, 5).Value = r.PerformancePlannedValue;
                worksheet.Cell(row, 6).Value = r.PerformanceActualValue;
                worksheet.Cell(row, 7).Value = r.DeviationPercent;
                worksheet.Cell(row, 8).Value = r.UploadFilePath;
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                HttpContext.Current.Response.End();
            }
        }
    }

    public static void ExportToPDF(List<reviewMonitoring> reports, string fileName)
    {
        Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
        using (var stream = new MemoryStream())
        {
            PdfWriter.GetInstance(doc, stream);
            doc.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            doc.Add(new Paragraph("Quarterly Report Review", titleFont));
            doc.Add(new Paragraph(" "));

            foreach (var r in reports)
            {
                doc.Add(new Paragraph("Intervention: " + r.InterventionName, normalFont));
                doc.Add(new Paragraph("Quarter: " + r.Quarter, normalFont));
                doc.Add(new Paragraph("Planned Expenditure: R " + r.PlannedExpenditure.ToString("N2"), normalFont));
                doc.Add(new Paragraph("Actual Expenditure: R " + r.ActualExpenditure.ToString("N2"), normalFont));
                doc.Add(new Paragraph("Target: " + r.PerformancePlannedValue, normalFont));
                doc.Add(new Paragraph("Achieved: " + r.PerformanceActualValue, normalFont));
                doc.Add(new Paragraph("Deviation (%): " + r.DeviationPercent.ToString("F2"), normalFont));
                doc.Add(new Paragraph("Document: " + r.UploadFilePath, normalFont));
                doc.Add(new Paragraph(" "));
            }

            doc.Close();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.End();
        }
    }
}