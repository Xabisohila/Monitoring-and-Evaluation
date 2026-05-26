<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="poa_Interventions.aspx.cs" Inherits="POA_Interventions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--    <br />
    <br />
    <br />
    <br />--%>

    <div class="padding100">
        <div class="col-md-12">
            <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Interventions</strong> </span></h2>
        </div>

    </div>


































































    <br />
    <br />


    <style type="text/css">
        .form-container {
                   width: 500px;
                   margin: 40px auto;
                   padding: 25px;
                   border: 1px solid #ccc;
                   border-radius: 8px;
                   background-color: #f9f9f9;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
               
        }

            .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #333;
        }

            .form-table {
                   width: 100%;
               
        }

                .form-table td {
                       padding: 10px;
                       vertical-align: top;
                   
            }

                .form-table label {
                       font-weight: bold;
                       color: #444;
                   
            }

                .form-table input[type="text"],
                .form-table textarea {
                       width: 100%;
                       padding: 8px;
                       border: 1px solid #aaa;
                       border-radius: 4px;
                       font-size: 14px;
                   
            }

                .form-table textarea {
                       resize: vertical;
                       height: 60px;
                   
            }



            /* HTML: <div class="ribbon">Your text content</div> */
.ribbon {
  font-size: 28px;
  font-weight: bold;
  color: #fff;
}
.ribbon {
  --s: 1.8em; /* the ribbon size */
  --d: .8em;  /* the depth */
  --c: .8em;  /* the cutout part */
  
  padding: var(--d) calc(var(--s) + .5em) 0;
  line-height: 1.8;
  background:
    conic-gradient(from  45deg at left  var(--s) top var(--d),
     #0008 12.5%,#0000 0 37.5%,#0004 0) 0   /50% 100% no-repeat,
    conic-gradient(from -45deg at right var(--s) top var(--d),
     #0004 62.5%,#0000 0 87.5%,#0008 0) 100%/50% 100% no-repeat;
  clip-path: polygon(0 0,calc(var(--s) + var(--d)) 0,calc(var(--s) + var(--d)) var(--d),calc(100% - var(--s) - var(--d)) var(--d),calc(100% - var(--s) - var(--d)) 0,100% 0, calc(100% - var(--c)) calc(50% - var(--d)/2),100% calc(100% - var(--d)),calc(100% - var(--s)) calc(100% - var(--d)),calc(100% - var(--s)) 100%,var(--s) 100%,var(--s) calc(100% - var(--d)),0 calc(100% - var(--d)),var(--c) calc(50% - var(--d)/2));
  background-color: #7ad943; /* the main color */
  width: fit-content;
}


h2.double:after{
    width: 18%;
    
}

    </style>

    

    
<div class="form-container">
    <h2>Project Details</h2>
    <table class="form-table">
        <tr>
            <td><asp:Label ID="lblId" runat="server" Text="ID (for update/delete):" /></td>
            <td><asp:TextBox ID="txtId" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblName" runat="server" Text="Name:" /></td>
            <td><asp:TextBox ID="txtName" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescription" runat="server" Text="Description:" /></td>
            <td><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBudget" runat="server" Text="Budget:" /></td>
            <td><asp:TextBox ID="txtBudget" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBaseline" runat="server" Text="Baseline:" /></td>
            <td><asp:TextBox ID="txtBaseline" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTarget" runat="server" Text="Target:" /></td>
            <td><asp:TextBox ID="txtTarget" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblIndicator" runat="server" Text="Indicator:" /></td>
            <td><asp:TextBox ID="txtIndicator" runat="server" /></td>
        </tr>
    </table>
</div>


    <asp:Button ID="btnInsert" runat="server" Text="Insert"  />
    <asp:Button ID="btnUpdate" runat="server" Text="Update"  />
    <asp:Button ID="btnDelete" runat="server" Text="Delete"  />




    <br />
    <br />
    <br />

    

    <br />
    <br />


</asp:Content>

