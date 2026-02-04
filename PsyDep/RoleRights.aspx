<%@ Page Title="Role Rights" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="RoleRights.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="overview-wrap m-b-30">
                <strong>
                    <h2 class="title-1">Define Rights</h2>
                </strong>
            </div>
        </div>
    </div>
    <div class="row ">
        <div class="col-sm-12 col-md-6 col-lg-4 col-xl-4 form-horizontal">
            <asp:HiddenField ID="hdnRole" runat="server" Value="-1" />
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"
                 OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-12 col-md-6 col-lg-8 col-xl-8 row form-group">
            <asp:Button ID="btnRights" runat="server" Text="Update" OnClick="btnRights_Click"
                CssClass="au-btn au-btn-icon au-btn--blue selbut"  />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="title-5 m-b-20">
                <h5>Select Pages:</h5>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">

            <div class="table-responsive table-responsive-data2">
                <table class="table table-data2" id="tblPages">

                    <tbody>

                        <tr class="tr-shadow">
                            <td>
                                <table>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="Dashboard" id="chkDashboard" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Dashboard</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="CreateUser" id="chkCreateUser" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Create User</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="RoleRights" id="chkRoleRights" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Role Rights</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="InstMas" id="chkInstMaster" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Institute Master</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="DepMas" id="chkDepatMaster" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Department Master</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="DivMas" id="chkDivMaster" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Division Master</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="AddReqDoc" id="chkReqDoc" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Add Required Document</td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="CreateRole" id="chkCreateRole" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Create Role</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="GenCase" id="chkHDDetails" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Generate Case</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="AssignCase" id="chkAssignCase" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Assign Case</td>
                                    </tr>
                                    <tr class="spacer"></tr>
                                    <tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="DivMas" id="chkAddCaseDetails" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Add Case Details</td>
                                    </tr>
                                    <%--<tr class="tr-shadow">
                                        <td>
                                            <label class="au-checkbox">
                                                <input type="checkbox" value="Report" id="chkReport" runat="server" />
                                                <span class="au-checkmark"></span>
                                            </label>
                                        </td>
                                        <td>Report</td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

