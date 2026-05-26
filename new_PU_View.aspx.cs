using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class new_PU_View : System.Web.UI.Page
{
        public class Intervention
        {
            public string InterventionName { get; set; }
            public string KeyIndicator { get; set; }
        }

        public class Framework
        {
            public string FocusArea { get; set; }
            public List<Intervention> Interventions { get; set; }
        }

        public class SectorData
        {
            public string Sector { get; set; }
            public List<string> WorkingGroups { get; set; }
            public List<string> Priorities { get; set; }
            public List<Framework> Frameworks { get; set; }
        }

        static List<SectorData> data;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                BindSectors();
            }
        }

        void LoadData()
        {
            data = new List<SectorData>
            {
                new SectorData
                {
                    Sector = "ESIEID",
                    WorkingGroups = new List<string>
                    {
                        "Infrastructure",
                        "Inclusive Economic Growth"
                    },
                    Priorities = new List<string>
                    {
                        "Drive Inclusive Economic Growth",
                        "Reduce Poverty"
                    },
                    Frameworks = new List<Framework>
                    {
                        new Framework
                        {
                            FocusArea = "Infrastructure Development and Service Delivery",
                            Interventions = new List<Intervention>
                            {
                                new Intervention { InterventionName = "Road Construction", KeyIndicator = "KM of roads built" },
                                new Intervention { InterventionName = "Water Supply", KeyIndicator = "Households served" }
                            }
                        }
                    }
                },

                new SectorData
                {
                    Sector = "SPCHD",
                    WorkingGroups = new List<string>
                    {
                        "Social Protection",
                        "Health Development"
                    },
                    Priorities = new List<string>
                    {
                        "Reduce Poverty",
                        "Improve Health Services"
                    },
                    Frameworks = new List<Framework>
                    {
                        new Framework
                        {
                            FocusArea = "Social Cohesion and Moral Regeneration",
                            Interventions = new List<Intervention>
                            {
                                new Intervention { InterventionName = "Community Safety Programs", KeyIndicator = "Crime rate reduction %" },
                                new Intervention { InterventionName = "GBVF Awareness", KeyIndicator = "Campaign reach" }
                            }
                        }
                    }
                }
            };
        }

        void BindSectors()
        {
            ddlSector.DataSource = data;
            ddlSector.DataTextField = "Sector";
            ddlSector.DataValueField = "Sector";
            ddlSector.DataBind();

            ddlSector.Items.Insert(0, new ListItem("-- Select Sector --", ""));
        }

        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = data.Find(x => x.Sector == ddlSector.SelectedValue);

            if (selected == null) return;

            ddlWG.DataSource = selected.WorkingGroups;
            ddlWG.DataBind();

            ddlPriority.DataSource = selected.Priorities;
            ddlPriority.DataBind();

            if (selected.Frameworks.Count > 0)
            {
                lblFocusArea1.Text = selected.Frameworks[0].FocusArea;

                gvIntervention1.DataSource = selected.Frameworks[0].Interventions;
                gvIntervention1.DataBind();
            }
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewIndicator")
            {
                string indicator = e.CommandArgument.ToString();

                Response.Write("<script>alert('Indicator: " + indicator + "');</script>");
            }
        }
    }

