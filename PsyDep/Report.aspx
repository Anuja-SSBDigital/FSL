<%@ Page Title="" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Case</strong> Details
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox ID="txtCaseNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Year</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox ID="txtYear" runat="server" CssClass="form-control"></asp:TextBox>
                       
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-password" class=" form-control-label">User</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                        CssClass="btn btn-dark btn-block" OnClick="btnSearch_Click" />
                </div>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"
                ScriptMode="Release">
            </asp:ScriptManager>
            <div class="col-lg-12">
                <h3>
                    <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Record found.."></asp:Label></h3>

                <rsweb:reportviewer id="rptViewer" runat="server"
                    sizetoreportcontent="true" hyperlinktarget="_blank">
                    <localreport enablehyperlinks="True">
                </localreport>
                </rsweb:reportviewer>
            </div>
        </div>
    </div>
    <script>
        function val() {
            var txtDDI = document.getElementById("<%= ddlUser.ClientID %>");

            if (txtDDI.value == "-1") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
            }

            return true;
        }
    </script>
</asp:Content>

