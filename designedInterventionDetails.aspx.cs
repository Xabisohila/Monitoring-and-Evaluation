using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.UI;

public partial class designedInterventionDetails : System.Web.UI.Page
{
    private int _interventionId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!TryGetId(out _interventionId))
            {
                ShowError("Missing or invalid id parameter.");
                pnlDetails.Visible = false;
                pnlEmpty.Visible = true;
                return;
            }
            lblId.Text = "ID: " + _interventionId;
            LoadIntervention();
        }
    }

    private bool TryGetId(out int id)
    {
        id = 0;
        var raw = Request.QueryString["id"];
        return int.TryParse(raw, out id) && id > 0;
    }

    private void LoadIntervention()
    {
        pnlLoading.Visible = true;
        pnlDetails.Visible = false;
        pnlEmpty.Visible = false;
        lblError.Visible = false;

        if (_interventionId == 0 && !TryGetId(out _interventionId))
        {
            ShowError("Invalid intervention id.");
            pnlLoading.Visible = false;
            pnlEmpty.Visible = true;
            return;
        }

        var csEntry = ConfigurationManager.ConnectionStrings["ConnectionString"];
        string cs = csEntry != null ? csEntry.ConnectionString : null;
        if (string.IsNullOrWhiteSpace(cs))
        {
            ShowError("Connection string 'AppConnectionString' not configured.");
            pnlLoading.Visible = false;
            return;
        }

        const string sql = @"
SELECT InterventionID,
       Title,
       Description,
       StartDate,
       EndDate,
       Status,
       LastUpdated
FROM Interventions
WHERE InterventionID = @Id";

        try
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = _interventionId;
                conn.Open();
                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!rdr.Read())
                    {
                        pnlEmpty.Visible = true;
                        pnlDetails.Visible = false;
                        return;
                    }

                    valTitle.Text = SafeGet(rdr, "Title");
                    lblTitle.Text = valTitle.Text;

                    var status = SafeGet(rdr, "Status");
                    valStatus.Text = status;
                    lblStatus.Text = status;
                    lblStatus.Visible = !string.IsNullOrEmpty(status);

                    var desc = SafeGet(rdr, "Description");
                    valDescription.Text = FormatDescription(desc);

                    valStartDate.Text = FormatDate(rdr, "StartDate");
                    valEndDate.Text = FormatDate(rdr, "EndDate");
                    valLastUpdated.Text = FormatDate(rdr, "LastUpdated");

                    pnlDetails.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ShowError("Error loading intervention: " + ex.Message);
        }
        finally
        {
            pnlLoading.Visible = false;
        }
    }

    private string SafeGet(IDataRecord r, string field)
    {
        int ordinal = r.GetOrdinal(field);
        if (r.IsDBNull(ordinal)) return string.Empty;
        return Convert.ToString(r.GetValue(ordinal));
    }

    private string FormatDate(IDataRecord r, string field)
    {
        int ordinal = r.GetOrdinal(field);
        if (r.IsDBNull(ordinal)) return "";
        var dt = Convert.ToDateTime(r.GetValue(ordinal));
        return dt.ToString("yyyy-MM-dd");
    }

    private string FormatDescription(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "<em>No description provided.</em>";
        var sb = new StringBuilder(Server.HtmlEncode(raw));
        return sb
            .Replace("\r\n", "<br />")
            .Replace("\n", "<br />")
            .ToString();
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        _interventionId = 0; // force re-parse
        LoadIntervention();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int id;
        if (!TryGetId(out id))
        {
            ShowError("Cannot navigate to edit, invalid id.");
            return;
        }
        Response.Redirect("pageInterventionsEdit.aspx?id=" + id);
    }
}