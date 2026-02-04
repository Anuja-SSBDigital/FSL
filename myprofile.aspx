<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="myprofile.aspx.cs" Inherits="myprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        span:hover {
            position: relative;
        }

        span[aria-label]:hover:after {
            content: attr(aria-label);
            padding: 4px 8px;
            position: absolute;
            right: 0;
            width: 20em;
            top: 100%;
            z-index: 20;
            font-size: 13px;
            background: #000;
            color: #fff;
        }

        .recycle_style {
            color: #9a9a9a;
            border: 1px solid #ddd !important;
            border: 1px solid;
            vertical-align: middle;
            height: 38px;
            width: 45px;
            text-align: center;
            padding: 10px;
            font-size: 19px !important;
        }

        @media (max-width:500px) {
            .row_pass {
                width: 85% !important;
            }
        }

        @media (max-width:500px) {
            .row_span {
                width: 15% !important;
            }
        }

        @media (min-width:500px) {
            .row_pass {
                width: 88% !important;
                padding-right: 0px !important;
            }
        }

        @media (min-width:500px) {
            .row_span {
                width: 8% !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>
                <asp:Label ID="lblTitle" runat="server">Update Profile Details</asp:Label>

            </strong>

        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <div class="col-sm-12 form-horizontal">
                        <asp:HiddenField ID="hdnUserID" runat="server" />
                        <label class=" form-control-label">Username</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUN" ReadOnly="true"></asp:TextBox>
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
                          <asp:HiddenField ID="hdnProfileImg" runat="server" />
                        <label class=" form-control-label">Profile Photo</label>
                        <asp:FileUpload ID="fl_profile" CssClass="form-control" runat="server" />
                        <div class="controls" style="padding: 0px;">
                            <div class="span8">
                                <div id="Uploaded_img"></div>
                                <div id="imagepreview" runat="server"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 form-horizontal">

                        <label class=" form-control-label">Mobile No</label>
                        <asp:TextBox runat="server" ID="txt_mob" CssClass="form-control"></asp:TextBox>
                    </div>


                </div>
                <div class="row form-group" id="divpass">
                    <div class="col-sm-6 form-horizontal">
                        <asp:HiddenField ID="hdnAppoitmentLetter" runat="server" />
                        <label class=" form-control-label">Appointment Letter</label>
                        <asp:FileUpload ID="txt_appointmentletter" CssClass="form-control" runat="server" />
                   <div class="controls" style="padding: 0px;">
                            <div class="span8">
                                <div id="Appointment_Div"></div>
                                <div id="AppointmentLetter_Div" runat="server"></div>
                            </div>
                        </div> </div>
                    <div class="col-sm-6 form-horizontal">
                          <asp:HiddenField ID="hdnPromotionLetter" runat="server" />
                      <label class=" form-control-label">Promotion Letter</label>
                        <asp:FileUpload ID="txt_promotionletter" CssClass="form-control" runat="server" />
                   <div class="controls" style="padding: 0px;">
                            <div class="span8">
                                <div id="Promotion_Div"></div>
                                <div id="PromotionLetter_Div" runat="server"></div>
                            </div>
                        </div> </div>
                </div>

                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Institute</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtInst" ReadOnly="true"></asp:TextBox>
                        <%--   <asp:DropDownList ID="ddlInst" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlInst_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Role</label>
                        <asp:HiddenField ID="hdnRole" runat="server" Value="-1" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRole" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlRole" runat="server"
                            CssClass="form-control" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>
                </div>
                <div class="row form-group" id="DivDep" runat="server">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Department</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDep" ReadOnly="true"></asp:TextBox>
                        <%-- <asp:DropDownList ID="ddlDep" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlDep_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Division</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDiv" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlDiv" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                    </div>
                </div>
                <div class="form-group m-t-30">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-dark btn-block" OnClientClick="return val();"
                        OnClick="btnUpdate_Click" />
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
            var ddlInst = document.getElementById("<%= txtInst.ClientID %>");
            var ddlDep = document.getElementById("<%= txtDep.ClientID %>");
            var AppointmentLetter = document.getElementById("<%= txt_appointmentletter.ClientID %>");
            //var ddlDiv = document.getElementById("<%= txtDiv.ClientID %>");
            var ddlRole = document.getElementById("<%= txtRole.ClientID %>");

            if (hdnUserID.value != "") {
                if (AppointmentLetter.value == "") {
                    AppointmentLetter.classList.add('is-invalid');
                    return false;
                } else {
                    AppointmentLetter.classList.remove('is-invalid');
                }
            }

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

            return true;
        }
    </script>
</asp:Content>

