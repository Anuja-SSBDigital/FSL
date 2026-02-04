<%@ Page Title="Insititute" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="InstMaster.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                    <asp:textbox runat="server" CssClass="form-control" ID="txtInsName"></asp:textbox>
                </div>
            </div>
             <div class="row form-group">
                <div class="col col-md-3">
                    <label class=" form-control-label">Location</label>
                </div>
                <div class="col-12 col-md-9">
                    <asp:textbox runat="server" CssClass="form-control" ID="txtInsLoc"></asp:textbox>
                </div>
            </div>
             <div class="row form-group">
                <div class="col col-md-3">
                    <label class=" form-control-label">Code</label>
                </div>
                <div class="col-12 col-md-9">
                    <asp:textbox runat="server" CssClass="form-control" ID="txtInsCode"></asp:textbox>
                </div>
            </div>
            <div class="row form-group">
                <asp:button ID="btnAddInst" runat="server" text="Submit" CssClass="btn btn-dark btn-block"  OnClientClick="return val();"
                 OnClick="btnAddInst_Click" />
            </div>
        </div>


        
    <div class="col-12">
        <div class="table-responsive table--no-card">
            <asp:GridView ID="GrdInst" runat="server" AutoGenerateColumns="false"
                CssClass="table table-borderless table-striped table-earning">
                <Columns>

                    <asp:BoundField DataField="inst_name" HeaderText="Name" ItemStyle-Width="20%"></asp:BoundField>
                    <asp:BoundField DataField="inst_code" HeaderText="Code" ItemStyle-Width="20%"></asp:BoundField>
                    <asp:BoundField DataField="location" HeaderText="Location" ItemStyle-Width="20%"></asp:BoundField>

                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("inst_id") %>' />
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
    </script>
</asp:Content>

