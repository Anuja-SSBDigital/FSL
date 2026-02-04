<%@ Page Title="Insititute" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InstMaster.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>Insititute</strong> Details                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Name</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtInsName"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Location</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtInsLoc"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Code</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtInsCode"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:HiddenField ID="hdnInsId" runat="server" Value="" />
                    <asp:Button ID="btnAddInst" runat="server" Text="Add" CssClass="btn btn-dark btn-block" OnClientClick="return val();"
                        OnClick="btnAddInst_Click" />
                </div>
            </div>



            <div class="col-12">
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="GrdInst" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        OnRowDataBound="GrdInst_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="inst_name" HeaderText="Name" ItemStyle-Width="20%"></asp:BoundField>
                            <asp:BoundField DataField="inst_code" HeaderText="Code" ItemStyle-Width="20%"></asp:BoundField>
                            <asp:BoundField DataField="location" HeaderText="Location" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("inst_id") %>' />
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
            var txtIN = document.getElementById("<%= txtInsName.ClientID %>");
            var txtIL = document.getElementById("<%= txtInsLoc.ClientID %>");
            var txtIC = document.getElementById("<%= txtInsCode.ClientID %>");

            if (txtIN.value == "") {
                txtIN.classList.add('is-invalid');
                return false;
            } else {
                txtIN.classList.remove('is-invalid');
            }

            if (txtIL.value == "") {
                txtIL.classList.add('is-invalid');
                return false;
            } else {
                txtIL.classList.remove('is-invalid');
            }

            if (txtIC.value == "") {
                txtIC.classList.add('is-invalid');
                return false;
            } else {
                txtIC.classList.remove('is-invalid');
            }
            return true;
        }

        
        function EditData(instid) {
            $.ajax({
                type: "POST",
                url: "InstMaster.aspx/GetDeptData",
                data: "{id: '" + instid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    //something
                    if (!res.d.startsWith("Error")) {
                        var jb = JSON.parse(res.d);
                        if (jb.length > 0) {
                            document.getElementById("<%=txtInsCode.ClientID %>").value = jb[0].inst_code;
                            document.getElementById("<%=txtInsLoc.ClientID %>").value = jb[0].location;
                            document.getElementById("<%=hdnInsId.ClientID %>").value = jb[0].inst_id;
                            document.getElementById("<%=txtInsName.ClientID %>").value = jb[0].inst_name;
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


        function DeleteData(instid) {
            if (confirm("Are you sure you want to delete this record?")) {
                $.ajax({
                    type: "POST",
                    url: "InstMaster.aspx/DeleteData",
                    data: "{id: '" + instid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        //something
                        if (res.d != '0') {
                            alert('Data deleted successfully.');
                            window.location = "InstMaster.aspx";
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

