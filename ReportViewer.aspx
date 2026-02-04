<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report  </title>
    <style>
        .table td {
            padding: 5px 5px !important;
            vertical-align: top;
            border-top: 1px solid #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
        <div class="col-lg-12">
            <h3>
                <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Record found.."></asp:Label></h3>

            <rsweb:ReportViewer ID="rptViewer" runat="server"
                SizeToReportContent="true" HyperlinkTarget="_blank">
                <LocalReport EnableHyperlinks="True">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
