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
                        <label for="hf-email" class=" form-control-label">Division</label>
                        <asp:HiddenField ID="HdnDivision" runat="server" />
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row col-12 form-group">
                    <%--<div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case No</label>
                    </div>--%>
                 <div class="row form-group" runat="server" visible="true" id="div_normal">
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Case No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_dfsee" Text="RFSL/EE" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Year</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_year"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-horizontal" runat="server" id="lbl_div" visible="true">
                        <label class=" form-control-label">Division Code</label>
                        <span class="row_span" id="PSY_ToolTip" runat="server" visible="false" aria-label="You can only add LVA, BEOS, SDS, PSY, NARCO, P.Assessment Division.">
                            <i class="fa fa-info-circle"></i></span>
                        <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_div" Onchange="FunctionDivision();"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Number</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_no" Onchange="FunctionRange();"></asp:TextBox>
                        <%--<asp:RangeValidator ID="Rng_No" ControlToValidate="txt_no" runat="server" ErrorMessage="Enter"></asp:RangeValidator>--%>
                    </div>
                </div>
                    <div class="row form-group" runat="server" id="div_fp" visible="false">
                        <div class="col-sm-2 form-horizontal">
                            <label class=" form-control-label">Case No</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_fp" Text="FP/CHP/OP" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 form-horizontal">
                            <label class=" form-control-label">Short Name</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_shortname"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 form-horizontal" runat="server" id="Div1" visible="true">
                            <label class=" form-control-label">Number</label>
                            <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_fpnumber"></asp:TextBox>
                            <%--<asp:RangeValidator ID="Rng_fpnumber" ControlToValidate="txt_fpnumber" MinimumValue="4" runat="server"></asp:RangeValidator>--%>
                        </div>
                        <div class="col-sm-2 form-horizontal">
                            <label class=" form-control-label">Year</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_fpyear"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 form-horizontal">
                            <label class=" form-control-label">Date</label>
                            <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txt_fpdate"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row col-6 form-group">
                    <div class="col-4">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return val();"
                            CssClass="btn btn-dark btn-block" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-2"></div>
                    <%--<div class="col-4">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="Viewreport();"
                            CssClass="btn btn-dark btn-block" />
                    </div>--%>
                </div>
            </div>
            <div class="alert alert-primary" role="alert" id="timeline" runat="server" visible="false">
                This is a primary alert with
										
                					
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"
                ScriptMode="Release">
            </asp:ScriptManager>
            <div class="col-lg-12">
                <h3>
                    <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Record found.."></asp:Label></h3>
                <div class="table-responsive table--no-card">
                    <%--<asp:GridView ID="grdAttach" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdAttach_RowDataBound">
                        <Columns>--%>

                    <%--<asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>--%>
                    <%--<asp:TemplateField HeaderText="File Name" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnFile" runat="server" Value='<%# Eval("path") %>' />
                                    <asp:HiddenField ID="hdnHD_name" runat="server" Value='<%# Eval("hd_name") %>' />
                                    <asp:Label ID="lblfn" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>


                    <%--<asp:BoundField DataField="hd_name" HeaderText="Hard Drive" ItemStyle-Width="20%"></asp:BoundField>--%>

                    <%--<asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="hash" HeaderText="Hash" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="40%"></asp:BoundField>--%>
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
                    <%-- </Columns>
                    </asp:GridView>--%>
                </div>
            </div>
        </div>
    </div>
    <script>
        function FunctionChecknegative() {
            var No = document.getElementById("<%= txt_no.ClientID %>");
            if (No.value < "1") {
                alert("The number should be 1 or greater than 1.");
                No.value = "";
            }
        }
        function FunctionDivision() {
            var HdnDivision = document.getElementById("<%= HdnDivision.ClientID %>");
            var Division = document.getElementById("<%= txt_div.ClientID %>");
            if (HdnDivision.value == "PSY") {
                if (Division.value == "LVA" || Division.value == "BEOS" || Division.value == "SDS"
                    || Division.value == "NARCO" || Division.value == "PSY" || Division.value == "P.Assessment") {

                } else {
                    alert("The Division code is not in Psychology Division.");
                    Division.value = "";
                }
            }
        }
        <%--function val() {
            var txtDDI = document.getElementById("<%= txt_caseno.ClientID %>");

            if (txtDDI.value == "") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
            }

            return true;
        }--%>

        <%--function Viewreport() {
            if (val()) {
                var txtCaseNo = document.getElementById("<%=txt_caseno.ClientID%>");

                window.open("ReportViewer.aspx?caseno=" + txtCaseNo.value + "",
                    'CustomPopUp', 'width=1100, height=500, menubar=no, resizable=yes');
            }
        }--%>
    </script>
</asp:Content>

