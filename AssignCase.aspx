<%@ Page Title="Assign Case" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AssignCase.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Assign Case</strong>

        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group" runat="server" id="div_department" visible="false">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Department Name</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-12 form-horizontal">
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
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_no" OnTextChanged="txt_no_TextChanged" AutoPostBack="true" ></asp:TextBox>
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
                            <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_fpnumber" ></asp:TextBox>
                            <%--<asp:RangeValidator ID="Rng_fpnumber" ControlToValidate="txt_fpnumber" MinimumValue="4" runat="server"></asp:RangeValidator>--%>
                        </div>
                        <div class="col-sm-2 form-horizontal">
                            <label class=" form-control-label">Year</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_fpyear"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 form-horizontal">
                            <label class=" form-control-label">Date</label>
                            <asp:TextBox runat="server" CssClass="form-control" OnTextChanged="txt_fpdate_TextChanged" AutoPostBack="true" TextMode="Date" ID="txt_fpdate"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <%-- <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Case No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlCaseNo" OnSelectedIndexChanged="ddlCaseNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                   </div>
                </div>--%>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Reference No</label>
                    </div>
                    <div class="col-12 col-md-5">
                        <asp:TextBox ID="txt_Referenceno" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Police Station</label>
                    </div>
                    <div class="col-12 col-md-5">
                        <asp:TextBox ID="txt_agencyname" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">View File</label>
                    </div>
                    <div class="col-12 col-md-5" id="view_pdf" runat="server">
                    </div>
                </div>


                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Referred by</label>
                    </div>
                    <div class="col-12 col-md-5">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRefBy"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-password" class=" form-control-label">User</label>
                    </div>
                    <div class="col-12 col-md-5">
                        <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Notes</label>
                    </div>
                    <div class="col-12 col-md-5">
                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 row form-group" style="margin-left: 225px;">
                    <asp:Button ID="btnAssign" runat="server" Text="Assign"
                        CssClass="btn btn-dark btn-block" OnClick="btnAssign_Click" OnClientClick="return val();" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function val() {

            var txtRefBy = document.getElementById("<%= txtRefBy.ClientID %>");
           <%-- var ddlCaseNo = document.getElementById("<%= ddlCaseNo.ClientID %>");--%>
            var txtDDI = document.getElementById("<%= ddlUser.ClientID %>");


            if (ddlCaseNo.value == "-1") {
                ddlCaseNo.classList.add('is-invalid');
                return false;
            } else {
                ddlCaseNo.classList.remove('is-invalid');
            }
            if (txtRefBy.value == "") {
                txtRefBy.classList.add('is-invalid');
                return false;
            } else {
                txtRefBy.classList.remove('is-invalid');
            }



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

