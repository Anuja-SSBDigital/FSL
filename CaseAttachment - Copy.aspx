<%@ Page Title="Case Attachment Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CaseAttachment.aspx.cs" Inherits="CaseAttachment" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Case Attachment</strong> Details
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row col-6 form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox ID="txtCaseNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6 form-group">
                    <div class="col-4">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return val();"
                            CssClass="btn btn-dark btn-block" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-2"></div>
                    <div class="col-4">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="Viewreport();"
                            CssClass="btn btn-dark btn-block" />
                    </div>
                </div>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"
                ScriptMode="Release">
            </asp:ScriptManager>
            <div class="col-lg-12">
                <h3>
                    <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Record found.."></asp:Label></h3>
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="grdAttach" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdAttach_RowDataBound">
                        <Columns>

                            <%--<asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>--%>
                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnFile" runat="server" Value='<%# Eval("path") %>' />
                                    <asp:HiddenField ID="hdnHD_name" runat="server" Value='<%# Eval("hd_name") %>' />
                                    <asp:Label ID="lblfn" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <%--<asp:BoundField DataField="hd_name" HeaderText="Hard Drive" ItemStyle-Width="20%"></asp:BoundField>--%>

                            <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="hash" HeaderText="Hash" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="40%"></asp:BoundField>
                            <%--<asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="15%"></asp:BoundField>--%>
                            <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                        id="btnEdit" runat="server">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                        id="btnDelete" runat="server">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <script>
        function val() {
            var txtDDI = document.getElementById("<%= txtCaseNo.ClientID %>");

            if (txtDDI.value == "") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
            }

            return true;
        }

        function Viewreport() {
            if (val()) {
                var txtCaseNo = document.getElementById("<%=txtCaseNo.ClientID%>");

                window.open("ReportViewer.aspx?caseno=" + txtCaseNo.value + "",
                    'CustomPopUp', 'width=1100, height=500, menubar=no, resizable=yes');
            }
        }
    </script>
</asp:Content>

