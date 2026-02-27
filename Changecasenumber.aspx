<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Changecasenumber.aspx.cs" Inherits="Changecasenumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>
                <asp:Label ID="lblTitle" runat="server">Search Case</asp:Label>
            </strong>
        </div>

        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">

                <!-- Hidden Field -->
                <asp:HiddenField ID="hdnCaseID" runat="server" />

                <!-- Case Number Row -->
                <div class="row form-group" runat="server" visible="true" id="div_dept">
                    <div class="col col-md-6">
                        <label class="form-control-label">Division</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-6">
                        <label class="form-control-label">Case Number</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:TextBox runat="server"
                            CssClass="form-control"
                            ID="txtCaseNo"
                            placeholder="Enter Case Number">
                        </asp:TextBox>
                    </div>

                    <div class="col-sm-4 form-horizontal" style="margin-top: 30px;">
                        <asp:Button ID="btnSearchCase"
                            runat="server"
                            Text="Search"
                            CssClass="btn btn-primary btn-block"
                            OnClick="btnSearchCase_Click" />
                    </div>
                </div>

                <!-- Result Section -->
                <div class="row form-group">
                    <div class="col-sm-12">
                        <asp:Label ID="lblMessage"
                            runat="server"
                            Font-Bold="true">
                        </asp:Label>
                    </div>
                </div>

                <!-- Case Details (Optional – Visible after search) -->

                <div class="row justify-content-center" runat="server" id="div_rpt" visible="false">
                    <div class="col-lg-12">
                        <asp:HiddenField ID="hdn_idofcase" runat="server"  />
                        <asp:HiddenField ID="hdncasenum" runat="server"  />
                        <div class="card shadow p-4">
                            <h4 class="text-center mb-2" id="title" runat="server" style="color: green;"></h4>

                            <div class="form-group mb-3" id="casedata" runat="server" visible="false">
                                <label>Case Number</label>

                                <div class="d-flex gap-2">

                                    <!-- Prefix -->
                                    <asp:TextBox ID="txtPrefix" runat="server"
                                        CssClass="form-control"
                                        Style="max-width: 120px;"
                                        ReadOnly="true">
                                    </asp:TextBox>

                                    <!-- Year (editable) -->
                                    <asp:TextBox ID="txtYear" runat="server"
                                        CssClass="form-control"
                                        Style="max-width: 100px;"
                                        MaxLength="4">
                                    </asp:TextBox>

                                    <!-- Section -->
                                    <asp:TextBox ID="txtSection" runat="server"
                                        CssClass="form-control"
                                        Style="max-width: 100px;"
                                        ReadOnly="true">
                                    </asp:TextBox>

                                    <!-- Serial -->
                                    <asp:TextBox ID="txtSerial" runat="server"
                                        CssClass="form-control"
                                        Style="max-width: 120px;"
                                        ReadOnly="true">
                                    </asp:TextBox>

                                </div>


                                <div class="text-center mt-2">
                                    <asp:Button ID="btnAjaxSearch" runat="server" CssClass="btn btn-primary px-4"
                                        Text="Update Case"
                                        OnClick="btnAjaxSearch_Click" />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

