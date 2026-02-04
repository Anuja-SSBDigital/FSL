<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--  <style>
         .focus {
            background-color: #273b86;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            width:30px;
        }

        .pageNumber {
            padding: 2px;
        }

        table {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        td,
        th {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }

        tr:nth-child(even) {
            background-color: #dddddd;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Reset Password</strong> Details                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <asp:HiddenField ID="HdnDivision" runat="server" />
                <div class="row form-group">
                    <div class="col col-md-6">
                        <label class=" form-control-label">Institute</label>
                        <asp:DropDownList runat="server" ID="ddl_inst" OnSelectedIndexChanged="ddl_inst_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col col-md-6">
                        <label class=" form-control-label">Department</label>
                        <%--<asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control" >--%>
                        <asp:DropDownList runat="server" ID="ddl_department" CssClass="form-control"></asp:DropDownList>

                    </div>
                </div>
            </div>

            <div class="col-sm-3 form-horizontal" style="margin: auto;">
                <div class="row form-group">
                    <asp:HiddenField ID="HiddenField2" runat="server" Value="" />
                    <asp:Button ID="btn_search" runat="server" OnClick="btn_search_Click" Text="Search"
                        CssClass="btn btn-dark btn-block" />
                </div>
            </div>

            <div class="col-12 row">
                <div id="tableID">
                    <div class="table-responsive table--no-card" >
                        <asp:GridView ID="GrdReset" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-borderless table-striped table-earning"
                            OnRowDataBound="GrdReset_RowDataBound" EmptyDataText="No Records Found.">
                            <Columns>
                                <asp:BoundField DataField="username" HeaderText="Username" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="inst_name" HeaderText="Institute" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="dept_name" HeaderText="Department" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="role" HeaderText="Role" ItemStyle-Width="25%"></asp:BoundField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Eval("userid") %>' />
                                        <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Reset Password"
                                            id="btnReset" runat="server">
                                            <i class="fa fa-wrench"></i>
                                        </button>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        function ResetPassword(hdnUserID) {
            if (confirm("Are you sure you want to Reset password ?")) {
                $.ajax({
                    type: "POST",
                    url: "ResetPassword.aspx/AddResetPassword",
                    data: "{hdnUserID: '" + hdnUserID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        //something
                        if (!res.d.startsWith("Error")) {
                            if (res.d != '0') {
                                alert('We have Emailed your reset password for your registered account.');
                                window.location = "ResetPassword.aspx";


                            }
                        }
                        else {
                            alert('Unable to reset password request.Please try again.');
                        }
                    },
                    error: function (msg) {
                        alert(msg);
                    }
                });
            }
        }

        //$(document).ready(function () {
        //    $('#tableID').DataTable({});
        //});


    </script>
</asp:Content>

