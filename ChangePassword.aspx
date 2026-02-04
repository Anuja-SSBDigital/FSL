<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Change</strong> Password                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Current Password</label>
                    </div>
                    <div class="col-12 col-md-7">
                        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtCurrentPass"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">New Password</label>
                    </div>
                    <div class="col-12 col-md-7">
                        <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Confirm New Password</label>
                    </div>
                    <div class="col-12 col-md-7">
                        <asp:TextBox ID="txtConfNewPass" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div class="col-sm-3 form-horizontal" style="margin:auto;">
                <div class="row form-group">
                    <asp:Button runat="server" Text="Change Password" ID="btnChangePass"
                        OnClick="btnChangePass_Click" CssClass="btn btn-dark btn-block" OnClientClick="return val();" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function val() {
            var CurrentPassword = document.getElementById("<%= txtCurrentPass.ClientID %>");
             var NewPassword = document.getElementById("<%= txtNewPass.ClientID %>");
             var ConfNewPassword = document.getElementById("<%= txtConfNewPass.ClientID %>");

            if (CurrentPassword.value == "") {
                CurrentPassword.classList.add('is-invalid');
                return false;
            } else {
                CurrentPassword.classList.remove('is-invalid');
            }

            pw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,15}$/;
            if (!pw.test(NewPassword.value)) {
                NewPassword.classList.add('is-invalid');
                return false;
            } else {
                NewPassword.classList.remove('is-invalid');
            }

            if (ConfNewPassword.value != NewPassword.value) {
                ConfNewPassword.classList.add('is-invalid');
                return false;
            } else {
                ConfNewPassword.classList.remove('is-invalid');
            }

            return true;
        }
    </script>
</asp:Content>

