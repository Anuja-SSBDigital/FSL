<%@ Page Title="Department" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="DeptMaster.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>Department</strong> Details                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Insititute</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlInst" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Name</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDeptName"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Code</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDeptCode"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:HiddenField ID="hdnDepId" runat="server" Value="" />
                    <asp:Button ID="btnAddDept" runat="server" Text="Submit" CssClass="btn btn-dark btn-block" OnClientClick="return val();"
                        OnClick="btnAddDept_Click" />
                </div>
            </div>

            <div class="col-12">
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="GrdDept" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        OnRowDataBound="GrdDept_RowDataBound">
                        <Columns>

                            <%--<asp:BoundField DataField="inst_name" HeaderText="Insititute Name" ItemStyle-Width="20%"></asp:BoundField>--%>
                            <asp:BoundField DataField="dept_name" HeaderText="Department Name" ItemStyle-Width="20%"></asp:BoundField>
                            <asp:BoundField DataField="dept_code" HeaderText="Department Code" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnDeptId" runat="server" Value='<%# Eval("dept_id") %>' />
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
            var txtDN = document.getElementById("<%= txtDeptName.ClientID %>");
            var txtDC = document.getElementById("<%= txtDeptCode.ClientID %>");

            if (txtDDI.value == "-1") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
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
                url: "DeptMaster.aspx/GetDeptData",
                data: "{id: '" + deptid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    //something
                    if (!res.d.startsWith("Error")) {
                        var jb = JSON.parse(res.d);
                        if (jb.length > 0) {
                            document.getElementById("<%=txtDeptCode.ClientID %>").value = jb[0].dept_code;
                    document.getElementById("<%=txtDeptName.ClientID %>").value = jb[0].dept_name;
                    document.getElementById("<%=hdnDepId.ClientID %>").value = jb[0].dept_id;
                    document.getElementById("<%=ddlInst.ClientID %>").value = jb[0].inst_id.inst_id;
                    document.getElementById("<%= btnAddDept.ClientID %>").innerText = "Update";
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
            url: "DeptMaster.aspx/DeleteData",
            data: "{id: '" + deptid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                //something
                if (res.d != '0') {
                    alert('Data deleted successfully.');
                    window.location = "DeptMaster.aspx";
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

