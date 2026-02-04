<%@ Page Title="Create User" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>
                <asp:Label ID="lblTitle" runat="server">Create User</asp:Label>

            </strong>

        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <div class="col-sm-12 form-horizontal">
                        <asp:HiddenField ID="hdnUserID" runat="server" />
                        <label class=" form-control-label">Username</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUN"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Firstname</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtFN"></asp:TextBox>
                    </div>

                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Lastname</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtLN"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Email</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Designation</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDes"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Institute</label>
                        <asp:DropDownList ID="ddlInst" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlInst_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Role</label>
                        <asp:HiddenField ID="hdnRole" runat="server" Value="-1" />
                        <asp:DropDownList ID="ddlRole" runat="server"
                            CssClass="form-control" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group" id="DivDep" runat="server">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Department</label>
                        <asp:DropDownList ID="ddlDep" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlDep_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Division</label>
                        <asp:DropDownList ID="ddlDiv" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">

                        <label class=" form-control-label">Profile Photo</label>
                        <asp:FileUpload ID="fl_profile" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-sm-6 form-horizontal">

                        <label class=" form-control-label">Mobile No</label>
                        <asp:TextBox runat="server" ID="txt_mob" CssClass="form-control"></asp:TextBox>
                    </div>


                </div>
                <div class="row form-group" id="divpass">
                    <div class="col-sm-6 form-horizontal">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <label class=" form-control-label">Appointment Letter</label>
                        <asp:FileUpload ID="txt_appointmentletter" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Promotion Letter</label>
                        <asp:FileUpload ID="txt_promotionletter" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-sm-6 form-horizontal m-t-15">
                        <label class=" form-control-label">Password</label>

                        <div class="row" style="margin: 0px;">
                            <asp:TextBox ID="txtPass" CssClass="form-control row_pass" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="row_span" aria-label="Must contain at least one
                        number and one uppercase and lowercase letter, and at least 8 or more characters">
                                <i class="fa fa-info-circle recycle_style"></i></span>
                        </div>
                    </div>

                    <div class="col-sm-6 form-horizontal m-t-15">
                        <label class=" form-control-label">Confirm Password</label>
                        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtConPass"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group m-t-30">
                    <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn btn-dark btn-block" OnClientClick="return val();"
                        OnClick="btnCreate_Click" />
                </div>
            </div>
        </div>
    </div>


    <script>
        function val() {

            var txtEmail = document.getElementById("<%= txtEmail.ClientID %>");
            var txtFN = document.getElementById("<%= txtFN.ClientID %>");
            var txtLN = document.getElementById("<%= txtLN.ClientID %>");
            var txtUN = document.getElementById("<%= txtUN.ClientID %>");
            var txtDes = document.getElementById("<%= txtDes.ClientID %>");
            var txtPass = document.getElementById("<%= txtPass.ClientID %>");
            var txtConPass = document.getElementById("<%= txtConPass.ClientID %>");
            var ddlInst = document.getElementById("<%= ddlInst.ClientID %>");
            var ddlDep = document.getElementById("<%= ddlDep.ClientID %>");
            var AppointmentLetter = document.getElementById("<%= txt_appointmentletter.ClientID %>");
            //var ddlDiv = document.getElementById("<%= ddlDiv.ClientID %>");
            var ddlRole = document.getElementById("<%= ddlRole.ClientID %>");


            if (txtFN.value == "") {
                txtFN.classList.add('is-invalid');
                return false;
            } else {
                txtFN.classList.remove('is-invalid');
            }

            if (txtEmail.value == "") {
                txtEmail.classList.add('is-invalid');
                return false;
            } else {
                txtEmail.classList.remove('is-invalid');
            }

            if (txtLN.value == "") {
                txtLN.classList.add('is-invalid');
                return false;
            } else {
                txtLN.classList.remove('is-invalid');
            }

            if (txtUN.value == "") {
                txtUN.classList.add('is-invalid');
                return false;
            } else {
                txtUN.classList.remove('is-invalid');
            }

            if (txtDes.value == "") {
                txtDes.classList.add('is-invalid');
                return false;
            } else {
                txtDes.classList.remove('is-invalid');
            }

            pw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,15}$/;
            if (!pw.test(txtPass.value)) {
                txtPass.classList.add('is-invalid');
                return false;
            } else {
                txtPass.classList.remove('is-invalid');
            }

            if (txtConPass.value != txtPass.value) {
                txtConPass.classList.add('is-invalid');
                return false;
            } else {
                txtConPass.classList.remove('is-invalid');
            }

            if (ddlInst.value == "-1") {
                ddlInst.classList.add('is-invalid');
                return false;
            } else {
                ddlInst.classList.remove('is-invalid');
            }

            if (ddlDep.value == "-1") {
                ddlDep.classList.add('is-invalid');
                return false;
            } else {
                ddlDep.classList.remove('is-invalid');
            }

            if (ddlDiv.value == "-1") {
                ddlDiv.classList.add('is-invalid');
                return false;
            } else {
                ddlDiv.classList.remove('is-invalid');
            }

            if (ddlRole.value == "-1") {
                ddlRole.classList.add('is-invalid');
                return false;
            } else {
                ddlRole.classList.remove('is-invalid');
            }

            if (AppointmentLetter.value == "") {
                AppointmentLetter.classList.add('is-invalid');
                return false;
            } else {
                AppointmentLetter.classList.remove('is-invalid');
            }

            return true;
        }
    </script>

</asp:Content>


