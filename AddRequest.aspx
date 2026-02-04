<%@ Page Title="Add View Request" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddRequest.aspx.cs" Inherits="AddRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>
                <asp:Label ID="lblTitle" runat="server">Add Request For Case Report</asp:Label>
            </strong>
        </div>

        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <asp:HiddenField ID="hdnReqId" runat="server" />
                        <label class=" form-control-label">Name</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtName"></asp:TextBox>
                    </div>

                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Department</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDepartment"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Designation</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDesignation"></asp:TextBox>
                    </div>

                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Case Details(XXXX/XXX/20XX/XX/(SrNo))</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseDet"></asp:TextBox>
                    </div>
                </div>
                 <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Police Station/Agency Name</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_agencyname"></asp:TextBox>
                    </div>

                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Reference No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_referenceno"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group" id="divpass">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Email Id</label>

                        <div class="row" style="margin: 0px;">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
                            <%--<asp:TextBox ID="txtPass" CssClass="form-control row_pass" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="row_span" aria-label="Must contain at least one
                        number and one uppercase and lowercase letter, and at least 8 or more characters">
                                <i class="fa fa-info-circle recycle_style"></i></span>--%>
                        </div>
                    </div>

                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Mobile No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMobNo"></asp:TextBox>
                        <%--<asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtConPass"></asp:TextBox>--%>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-12 form-horizontal">
                        <label class=" form-control-label">Notes</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNotes"
                            TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlInst" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlInst_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>

<%--                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Department</label>
                        <asp:DropDownList ID="ddlDep" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlDep_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>--%>
                </div>
                <div class="form-group m-t-30">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-dark btn-block" 
                        OnClientClick="return val();"
                        OnClick="btnAdd_Click" disabled="true"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

