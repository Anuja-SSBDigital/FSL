<%@ Page Title="Division Master" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="DivMaster.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>Division</strong> Details                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Insititute</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlInst" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlInst_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Department</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:HiddenField ID="hdnDeptId" runat="server" Value="" />
                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Name</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDivName"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Code</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDivCode"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:HiddenField ID="hdnDivId" runat="server" Value="" />
                    <asp:Button ID="btnAddDiv" runat="server" Text="Submit" CssClass="btn btn-dark btn-block" OnClientClick="return val();"
                        OnClick="btnAddDiv_Click" />
                </div>
            </div>

            <div class="col-12">
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="GrdDiv" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        OnRowDataBound="GrdDiv_RowDataBound">
                        <Columns>

                            <%--<asp:BoundField DataField="inst_name" HeaderText="Insititute Name" ItemStyle-Width="20%"></asp:BoundField>--%>
                            <asp:BoundField DataField="div_name" HeaderText="Division Name" ItemStyle-Width="20%"></asp:BoundField>
                            <asp:BoundField DataField="div_code" HeaderText="Division Code" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("div_id") %>' />
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
            var txtDDI = document.getElementById("<%= ddlInst.ClientID %>");
            var txtDDD = document.getElementById("<%= ddlDept.ClientID %>");
            var txtDN = document.getElementById("<%= txtDivName.ClientID %>");
            var txtDC = document.getElementById("<%= txtDivCode.ClientID %>");

            if (txtDDI.value == "-1") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
            }

            if (txtDDD.value == "-1") {
                txtDDD.classList.add('is-invalid');
                return false;
            } else {
                txtDDD.classList.remove('is-invalid');
            }

            if (txtDN.value == "") {
                txtDN.classList.add('is-invalid');
                return false;
            } else {
                txtDN.classList.remove('is-invalid');
            }

            if (txtDC.value == "") {
                txtDC.classList.add('is-invalid');
                return false;
            } else {
                txtDC.classList.remove('is-invalid');
            }
            return true;
        }

        function EditData(deptid) {
            $.ajax({
                type: "POST",
                url: "DivMaster.aspx/GetDivData",
                data: "{id: '" + deptid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    //something
                    if (!res.d.startsWith("Error")) {
                        var jb = JSON.parse(res.d);
                        if (jb[1].dep.length > 0) {
                            var ddl = "<option value='-1'>-- Select Department -- </option>";
                            for (var i = 0; i < jb[1].dep.length; i++) {
                                ddl += "<option value='" + jb[1].dep[i].dept_id + "'";
                                if (jb[0].res.length > 0)
                                    if (jb[0].res[0].dept_id.dept_id == jb[1].dep[i].dept_id)
                                        ddl += " selected ='true' ";
                                ddl += ">" +
                                    jb[1].dep[i].dept_name + "</option>";
                            }
                            document.getElementById("<%= ddlDept.ClientID %>").innerHTML = ddl;
                        }
                        if (jb[0].res.length > 0) {
                            document.getElementById("<%=txtDivCode.ClientID %>").value = jb[0].res[0].div_code;
                    document.getElementById("<%=txtDivName.ClientID %>").value = jb[0].res[0].div_name;
                    document.getElementById("<%=hdnDivId.ClientID %>").value = jb[0].res[0].div_id;
                    document.getElementById("<%=ddlInst.ClientID %>").value = jb[0].res[0].inst_id.inst_id;
                    document.getElementById("<%=ddlDept.ClientID %>").value = jb[0].res[0].dept_id.dept_id;
                    document.getElementById("<%=hdnDeptId.ClientID %>").value = jb[0].res[0].dept_id.dept_id;
                    document.getElementById("<%= btnAddDiv.ClientID %>").innerText = "Update";
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


function DeleteData(deptid) {
    if (confirm("Are you sure you want to delete this record?")) {
        $.ajax({
            type: "POST",
            url: "DivMaster.aspx/DeleteData",
            data: "{id: '" + deptid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                //something
                if (res.d != '0') {
                    alert('Data deleted successfully.');
                    window.location = "DivMaster.aspx";
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

