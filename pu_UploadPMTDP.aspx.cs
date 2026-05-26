using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pu_UploadPMTDP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindFrameworks();
    }

    private void BindFrameworks()
    {
        using (var cn = DBmain.Open())
        using (var da = new SqlDataAdapter("SELECT FrameworkId, Name FROM dbo.new_Framework ORDER BY Name", cn))
        {
            var dt = new DataTable();
            da.Fill(dt);
            ddlFramework.DataSource = dt;
            ddlFramework.DataTextField = "Name";
            ddlFramework.DataValueField = "FrameworkId";
            ddlFramework.DataBind();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //lblMsg.Text = ""; gvValidation.DataSource = null; gvValidation.DataBind(); btnCommit.Enabled = false;
        //if (!fuCsv.HasFile) { lblMsg.Text = "Please select a CSV file."; return; }
        //if (string.IsNullOrEmpty(ddlFramework.SelectedValue)) { lblMsg.Text = "Please select a Framework."; return; }

        //var dt = new DataTable();
        //dt.Columns.Add("FrameworkId", typeof(int));
        //dt.Columns.Add("IndicatorCode", typeof(string));
        //dt.Columns.Add("ProgramCode", typeof(string));
        //dt.Columns.Add("Year", typeof(int));
        //dt.Columns.Add("TargetValue", typeof(decimal));

        //using (var sr = new StreamReader(fuCsv.FileContent))
        //{
        //    string line; int row = 0;
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        if (row++ == 0 && line.ToLower().Contains("frameworkid")) continue; // header
        //        var c = line.Split(',').Select(x => x.Trim()).ToArray();
        //        if (c.Length < 5) continue;
        //        var r = dt.NewRow();
        //        r[0] = int.Parse(c[0]);
        //        r[1] = c[1];
        //        r[2] = c[2];
        //        r[3] = int.Parse(c[3]);
        //        r[4] = decimal.Parse(c[4]);
        //        dt.Rows.Add(r);
        //    }
        //}

        //using (var cn = Db.Open())
        //{
        //    using (var bulk = new SqlBulkCopy(cn))
        //    {
        //        bulk.DestinationTableName = "stg.PMTDP_Target";
        //        foreach (DataColumn col in dt.Columns)
        //            bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
        //        bulk.WriteToServer(dt);
        //    }
        //    using (var cmd = new SqlCommand("planning.PMTDP_Target_BulkStage_Validate", cn))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@FrameworkId", int.Parse(ddlFramework.SelectedValue));
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            var v = new DataTable();
        //            da.Fill(v);
        //            gvValidation.DataSource = v;
        //            gvValidation.DataBind();
        //            if (v.Rows.Count == 0)
        //            {
        //                lblMsg.Text = "Validation passed. Click Commit to save.";
        //                btnCommit.Enabled = true;
        //            }
        //            else lblMsg.Text = "Validation issues found. Fix and re-upload.";
        //        }
        //    }
        //}
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        //using (var cn = Db.Open())
        //using (var cmd = new SqlCommand("planning.PMTDP_Target_Bulk_Upsert", cn))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@FrameworkId", int.Parse(ddlFramework.SelectedValue));
        //    cmd.Parameters.AddWithValue("@UserId", CurrentUserId());
        //    cmd.ExecuteNonQuery();
        //}
        //lblMsg.Text = "PMTDP targets committed.";
        //btnCommit.Enabled = false;
    }
}