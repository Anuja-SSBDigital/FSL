<%@ Page Title="" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="RoleMaster.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="card">
        <div class="card-header">
            <asp:Label ID="lblRoleTitle" runat="server" Text="Role Details"></asp:Label>
            <strong>Role</strong> Details                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Role</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRole"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:HiddenField ID="hdnRoleID" runat="server" Value="" />
                    <asp:Button ID="btnAddInst" runat="server" Text="Submit" CssClass="btn btn-dark btn-block"
                        OnClick="btnRole_Click" OnClientClick="return val();" />
                </div>
            </div>
       


    <div class="col-6 row">
        <div class="table-responsive table--no-card">
            <asp:GridView ID="GrdRole" runat="server" AutoGenerateColumns="false"
                CssClass="table table-borderless table-striped table-earning"
                OnRowDataBound="GrdRole_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="role" HeaderText="Role" ItemStyle-Width="20%"></asp:BoundField>
                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("role_id") %>' />
                            <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                id="btnEdit" runat="server">
                                <i class="fa fa-edit"></i>
                            </button>
                            <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                id="btnDelete" runat="server">
                                <i class="fa fa-trash"></i>
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
             </div>
    </div>
    <script>
        function val() {
            var txtRole = document.getElementById("<%= txtRole.ClientID %>");

            if (txtRole.value == "") {
                txtRole.classList.add('is-invalid');
                return false;
            } else {
                txtRole.classList.remove('is-invalid');
            }
            return true;
        }
        
        function EditData(roleid) {
            $.ajax({
                type: "POST",
                url: "RoleMaster.aspx/GetRoleData",
                data: "{id: '" + roleid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    //something
                    if (!res.d.startsWith("Error")) {
                        var jb = JSON.parse(res.d);
                        if (jb.length > 0) {
                            document.getElementById("<%=txtRole.ClientID %>").value = jb[0].role;
                            document.getElementById("<%=hdnRoleID.ClientID %>").value = jb[0].role_id;
                            document.getElementById("<%= btnAddInst.ClientID %>").innerText = "Update";
                        }
                    }
                    else
                        alert('Data not edited.');
                },
                error: function (msg) {
                    alert(msg);
                }

            });
        }


        function DeleteData(roleid) {
            if (confirm("Are you sure you want to delete this record?")) {
                $.ajax({
                    type: "POST",
                    url: "RoleMaster.aspx/DeleteData",
                    data: "{id: '" + roleid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        //something
                        if (res.d != '0') {
                            alert('Data deleted successfully.');
                            window.location = "RoleMaster.aspx";
                        }
                        else
                            alert('Data not deleted.');
                    },
                    error: function (msg) {
                        alert(msg);
                    }

                });
            }
        }

    </script>
</asp:Content>

