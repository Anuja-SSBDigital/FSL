<%@ Page Title="Hard Drive Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HDDetails.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Hard Drive</strong> Details
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Alias</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <%--<div class="col-6 row">
                            <asp:Label ID="Label1" runat="server" Text="DFS/EE/2020/CF/"></asp:Label>
                        </div>
                        <div class="col-6 row">--%>
                        <asp:TextBox ID="txtalias" runat="server" CssClass="form-control"></asp:TextBox>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Serial No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <%--<div class="col-6 row">
                            <asp:Label ID="Label1" runat="server" Text="DFS/EE/2020/CF/"></asp:Label>
                        </div>
                        <div class="col-6 row">--%>
                        <asp:TextBox ID="txtSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Capacity</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <%--<div class="col-6 row">
                            <asp:Label ID="Label1" runat="server" Text="DFS/EE/2020/CF/"></asp:Label>
                        </div>
                        <div class="col-6 row">--%>
                        <asp:TextBox ID="txtCap" runat="server" CssClass="form-control"></asp:TextBox>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Brand</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <%--<div class="col-6 row">
                            <asp:Label ID="Label1" runat="server" Text="DFS/EE/2020/CF/"></asp:Label>
                        </div>
                        <div class="col-6 row">--%>
                        <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control"></asp:TextBox>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row form-group" id="divUser" runat="server">
                    <div class="col col-md-3">
                        <label for="hf-password" class=" form-control-label">User</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:HiddenField ID="hdnUId" runat="server" Value="" />
                    <asp:Button ID="btnAssign" runat="server" Text="Add" OnClientClick="return val();"
                        CssClass="btn btn-dark btn-block" OnClick="btnAssign_Click" />
                </div>
            </div>
                <div class="col-12 row m-t-20">
                    <div class="table-responsive table--no-card">
                        <asp:GridView ID="grdHD" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-borderless table-striped table-earning"
                            OnRowDataBound="grdHD_RowDataBound">
                            <Columns>

                                <asp:BoundField DataField="hd_name" HeaderText="Name" ItemStyle-Width="25%"></asp:BoundField>

                                <asp:BoundField DataField="sr_num" HeaderText="Serial No" ItemStyle-Width="20%"></asp:BoundField>

                                <asp:BoundField DataField="capacity" HeaderText="Capacity" ItemStyle-Width="15%"></asp:BoundField>
                                <asp:BoundField DataField="brand" HeaderText="Brand" ItemStyle-Width="15%"></asp:BoundField>
                                <asp:BoundField DataField="username" HeaderText="User" ItemStyle-Width="15%"></asp:BoundField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("hd_id") %>' />
                                        <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                            id="btnEdit" runat="server">
                                            <i class="fa fa-edit"></i>
                                        </button>
                                        <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                            id="btnDelete" runat="server">
                                            <i class="fa fa-trash"></i>
                                        </button>
                                        <%--<button type="button" class="btn btn-icons btn-rounded btn-outline-danger" title="Reject"
                                            id="btnRej" runat="server">
                                            <i class="fa fa-close"></i>
                                        </button>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            <div class="col-12 row m-t-20">
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning">
                        <Columns>

                            <asp:BoundField DataField="hd_name" HeaderText="Name" ItemStyle-Width="25%"></asp:BoundField>

                            <asp:BoundField DataField="sr_num" HeaderText="Serial No" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:BoundField DataField="capacity" HeaderText="Capacity" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="brand" HeaderText="Brand" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="userid" HeaderText="User" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("userid") %>' />
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                        id="btnEdit" runat="server">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                        id="btnDelete" runat="server">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                    <%--<button type="button" class="btn btn-icons btn-rounded btn-outline-danger" title="Reject"
                                            id="btnRej" runat="server">
                                            <i class="fa fa-close"></i>
                                        </button>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-12 row">
                <asp:HiddenField ID="hdnHdid" runat="server" Value="" />
                <asp:Button ID="btnInsert" runat="server" Text="Insert" Visible="false"
                    CssClass="btn btn-dark btn-block" OnClick="btnInsert_Click" />
            </div>

        </div>
    </div>


    <script>
        function val() {
            var txtalias = document.getElementById("<%= txtalias.ClientID %>");
            var txtSrNo = document.getElementById("<%= txtSrNo.ClientID %>");
            var txtCap = document.getElementById("<%= txtCap.ClientID %>");
            var txtBrand = document.getElementById("<%= txtBrand.ClientID %>");
            var ddlUser = document.getElementById("<%= ddlUser.ClientID %>");
            var hdnUID = document.getElementById("<%= hdnUId.ClientID %>");

            if (txtalias.value == "") {
                txtalias.classList.add('is-invalid');
                return false;
            } else {
                txtalias.classList.remove('is-invalid');
            }

            if (txtSrNo.value == "") {
                txtSrNo.classList.add('is-invalid');
                return false;
            } else {
                txtSrNo.classList.remove('is-invalid');
            }

            if (txtCap.value == "") {
                txtCap.classList.add('is-invalid');
                return false;
            } else {
                txtCap.classList.remove('is-invalid');
            }

            if (txtBrand.value == "") {
                txtBrand.classList.add('is-invalid');
                return false;
            } else {
                txtBrand.classList.remove('is-invalid');
            }

            if (hdnUID.value == "") {
                if (ddlUser.value == "-1") {
                    ddlUser.classList.add('is-invalid');
                    return false;
                } else {
                    ddlUser.classList.remove('is-invalid');
                }

            }
            return true;
        }

        function EditData(hdid) {
            $.ajax({
                type: "POST",
                url: "HDDetails.aspx/GetHDData",
                data: "{id: '" + hdid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    //something
                    if (!res.d.startsWith("Error")) {
                        var jb = JSON.parse(res.d);
                        if (jb.length > 0) {
                            var hdnuid = document.getElementById("<%=hdnUId.ClientID %>");
                            document.getElementById("<%=txtalias.ClientID %>").value = jb[0].hd_name;
                            document.getElementById("<%=txtBrand.ClientID %>").value = jb[0].brand;
                            document.getElementById("<%=txtCap.ClientID %>").value = jb[0].capacity;
                            document.getElementById("<%=txtSrNo.ClientID %>").value = jb[0].sr_num;
                            document.getElementById("<%=hdnHdid.ClientID %>").value = jb[0].hd_id;
                            if (hdnuid.value == "")
                                document.getElementById("<%=ddlUser.ClientID %>").value = jb[0].userid.userid;

                            document.getElementById("<%= btnInsert.ClientID %>").innerText = "Update";
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


        function DeleteData(hdid) {
            if (confirm("Are you sure you want to delete this record?")) {
                $.ajax({
                    type: "POST",
                    url: "HDDetails.aspx/DeleteData",
                    data: "{id: '" + hdid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        //something
                        if (res.d != '0') {
                            alert('Data deleted successfully.');
                            window.location = "HDDetails.aspx";
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

