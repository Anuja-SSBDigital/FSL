<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
         CodeFile="UploadExcel.aspx.cs" Inherits="UploadExcel" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h3>Bulk Case Assignment (Change User)</h3>

    <div class="card">
        <div class="card-body">

            <div class="row">
                <div class="col-md-4">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnValidate" runat="server" Text="Validate Cases" 
                        CssClass="btn btn-primary" OnClick="btnValidate_Click" />
                </div>
            </div>

            <br />
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>

            <!-- Validation Result -->
            <asp:GridView ID="gvValidation" runat="server" AutoGenerateColumns="false" 
                CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="CaseNo" HeaderText="Case Number" />
                    <asp:BoundField DataField="CurrentUser" HeaderText="Currently Assigned To" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
            </asp:GridView>

            <!-- Assign Section -->
            <asp:Panel ID="pnlAssign" runat="server" Visible="false">
                <hr />
                <h5>Select New User (Department Wise)</h5>6
                <div class="row">
                    <div class="col-md-5">
                        <asp:DropDownList ID="ddlNewUser" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnAssignAll" runat="server" Text="Assign All Cases" 
                            CssClass="btn btn-success" OnClick="btnAssignAll_Click" />
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>