<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_IndicatorAdmin.aspx.cs" Inherits="i_IndicatorAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        :root {
            --bg: #f7f8fb;
            --surface: #ffffff;
            --border: #d9dee5;
            --text: #1f2937;
            --muted: #64748b;
            --primary: #0b5ed7;
            --primary-dark: #094db0;
            --danger: #dc3545;
            --radius: 10px;
            --shadow: 0 2px 10px rgba(16, 24, 40, 0.08);
            --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        }

        body { background: var(--bg); font-family: var(--font); color: var(--text); }

        /*.container { max-width: 1000px; margin: 0 auto; padding: 16px; }*/
        .section-title h2 { margin: 0 0 12px 0; font-size: 26px; font-weight: 700; color: var(--text); letter-spacing: .2px; }
        h2 { margin: 0 0 12px 0; font-size: 26px; font-weight: 700; color: var(--text); letter-spacing: .2px; }

        /* Form card + aligned grid */
        /*.form-card {
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: var(--radius);
            box-shadow: var(--shadow);
            padding: 18px;
            margin-bottom: 16px;
        }*/
        .form-grid {
            display: grid;
            /*grid-template-columns: 220px 1fr;*/
            /*gap: 10px 16px;*/
            /*align-items: center;*/
            /*max-width: 820px;*/
        }
        .full-row { grid-column: 1 / -1; }

        /* Form visuals */
        asp\:label, label { display: inline-block; font-size: 13px; color: var(--muted); }
        asp\:textbox, input[type="text"], textarea, select, asp\:dropdownlist {
            display: inline-block;
            width: 100%;
            min-width: 220px;
            padding: 9px 12px;
            border: 1px solid var(--border);
            border-radius: 8px;
            background: var(--surface);
            color: var(--text);
            transition: border-color .15s ease, box-shadow .15s ease;
        }
        asp\:textbox:focus, input[type="text"]:focus, textarea:focus, select:focus, asp\:dropdownlist:focus {
            outline: none; border-color: var(--primary); box-shadow: 0 0 0 3px rgba(11,94,215,.15);
        }
        small { color: var(--muted); }

        /* Validation and buttons */
        asp\:validationsummary, .validation-summary-errors { display: block; margin-bottom: 10px; color: var(--danger); font-size: 13px; }
        asp\:button, input[type="submit"], button {
            display: inline-block; margin-top: 8px; padding: 9px 14px; font-size: 14px; font-weight: 600;
            border-radius: 8px; border: 1px solid var(--primary); background: var(--primary); color: #fff;
            cursor: pointer; transition: background-color .15s ease, border-color .15s ease, box-shadow .15s ease;
        }
        asp\:button:hover, input[type="submit"]:hover, button:hover { background: var(--primary-dark); border-color: var(--primary-dark); }
        asp\:button:focus-visible, input[type="submit"]:focus-visible, button:focus-visible { outline: none; box-shadow: 0 0 0 3px rgba(11,94,215,.25); }

        hr { border: none; border-top: 1px solid var(--border); margin: 18px 0; }

        /* GridView */
        table, asp\:gridview {
            width: 100%; border-collapse: collapse; background: var(--surface);
            border: 1px solid var(--border); border-radius: 10px; overflow: hidden; box-shadow: var(--shadow); font-family: var(--font);
        }
        table th, table td { padding: 10px 12px; font-size: 13px; color: var(--text); border-bottom: 1px solid var(--border); text-align: left; vertical-align: top; }
        table th { background: #f1f5f9; font-weight: 600; }
        table tr:last-child td { border-bottom: none; }

        /* Responsive */
        @media (max-width: 640px) {
            .container { padding: 12px; }
            .form-grid { grid-template-columns: 1fr; }
            asp\:textbox, input[type="text"], textarea, select { width: 100%; }
            table th, table td { padding: 8px 10px; font-size: 12px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br /><br /><br /><br /><br />
    <div class="container">
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s">
                    <span><strong>Planning: Indicators</strong></span>
                </h2>
            </div>
        </div>
        <br /><br />

        <div class="form-card">
            <%--<h2>Indicators</h2>--%>
            <asp:ValidationSummary runat="server" ID="valSummary" ForeColor="Red"/>
            <asp:HiddenField runat="server" ID="hfID" />

            <div class="form-grid">
                <asp:Label runat="server" Text="Desired Outcome:" AssociatedControlID="ddlOutcome" style="max-width:fit-content;"/>
                 <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlOutcome" InitialValue="" ErrorMessage="Select Outcome" ForeColor="Red" CssClass="full-row1" style="max-width:fit-content;"/>
                <asp:DropDownList runat="server" ID="ddlOutcome" Width="450" />
                <span></span>
               

                <asp:Label runat="server" Text="Outcome Indicator:" AssociatedControlID="txtName" style="max-width:fit-content;"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="Indicator name required" ForeColor="Red" CssClass="full-row1" style="max-width:fit-content;"/>
                <asp:TextBox runat="server" ID="txtName" Width="600" />
                <span></span>
                

                <asp:Label runat="server" Text="Indicator Type:" AssociatedControlID="txtType" />
                <div class="full-row">
                    <asp:TextBox runat="server" ID="txtType" Width="250" />
                    <small>(e.g., MTDP / P-MTDP)</small>
                </div>

                <asp:Label runat="server" Text="Baseline Value:" AssociatedControlID="txtBaseline" />
                <asp:TextBox runat="server" ID="txtBaseline" Width="200" />

                <asp:Label runat="server" Text="Term Target Value:" AssociatedControlID="txtTermTarget" />
                <asp:TextBox runat="server" ID="txtTermTarget" Width="200" />

                <asp:Label runat="server" Text="Annual Budget:" AssociatedControlID="txtBudget" />
                <asp:TextBox runat="server" ID="txtBudget" Width="150" />

                <asp:Label runat="server" Text="Implementing Institution:" AssociatedControlID="txtImpl" />
                <asp:TextBox runat="server" ID="txtImpl" Width="450" />

                <asp:Label runat="server" Text="Supporting Institutions:" AssociatedControlID="txtSupp" />
                <asp:TextBox runat="server" ID="txtSupp" Width="450" />

                <asp:Label runat="server" Text="Calculation Type:" AssociatedControlID="txtCalc" />
                <asp:TextBox runat="server" ID="txtCalc" Width="250" />

                <asp:Label runat="server" Text="Reporting Cycle:" AssociatedControlID="txtCycle" />
                <asp:TextBox runat="server" ID="txtCycle" Width="200" />

                <span class="full-row">
                    <asp:CheckBox runat="server" ID="chkCumulative" Text="Is Cumulative" />
                    &nbsp;&nbsp;
                    <asp:CheckBox runat="server" ID="chkPercentage" Text="Is Percentage" />
                </span>

                <span class="full-row">
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                </span>
            </div>
        </div>

        <hr />

        <asp:GridView
            runat="server"
            ID="gv"
            AutoGenerateColumns="false"
            DataKeyNames="IndicatorID"
            AllowSorting="true"
            OnSorting="gv_Sorting"
            OnRowCommand="gv_RowCommand"
            OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IndicatorID" HeaderText="ID" Visible="false" SortExpression="IndicatorID" />
                <asp:BoundField DataField="IndicatorName" HeaderText="Indicator" SortExpression="IndicatorName" />
                <asp:BoundField DataField="IndicatorType" HeaderText="Type" SortExpression="IndicatorType" />
                <asp:TemplateField HeaderText="Outcome" SortExpression="OutcomeID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblOutcomeName" />
                        <asp:HiddenField runat="server" ID="hfOutcomeID" Value='<%# Eval("OutcomeID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsCumulative" HeaderText="Cumulative" SortExpression="IsCumulative" />
                <asp:BoundField DataField="IsPercentage" HeaderText="%" SortExpression="IsPercentage" />
                <asp:ButtonField CommandName="EditRow" Text="Edit" />
                <asp:ButtonField CommandName="DeleteRow" Text="Delete" />
            </Columns>
        </asp:GridView>
    </div>
    <br /><br />
</asp:Content>

