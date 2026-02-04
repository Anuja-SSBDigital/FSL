<%@ Page Title="Assign Case" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="AssignCase.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="card">
        <div class="card-header">
            <strong>Assign</strong> Case
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-12 form-horizontal">
                <div class="form-header">

                    <asp:TextBox ID="txtCaseNo" runat="server"
                        CssClass="au-input au-input--xl" placeholder="Search Case By Id"></asp:TextBox>
                    <asp:HiddenField ID="hdnCaseId" runat="server" Value="" />
                    <%-- <button class="au-btn--submit" type="button">
                        <i class="zmdi zmdi-search"></i>
                    </button>--%>
                    <asp:LinkButton ID="btnSearch" runat="server"
                        CssClass="au-btn--submit" OnClick="btnSearch_Click">
                            <i class="zmdi zmdi-search"></i></asp:LinkButton>
                </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    $("[id$=txtCaseNo]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("AddReqDoc.aspx/BindAutoCompleteList") %>',
                                data: "{ 'prefix': '" + request.term.replace(/\\/g, "\\\\") + "'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.split('-')[0],
                                            val: item.split('-')[1]
                                        }
                                    }))
                                },
                                error: function (response) {
                                    alert(response.responseText);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });
                        },
                        select: function (e, i) {
                            $("[id$=hdnCaseId]").val(i.item.val);
                        },
                        minLength: 3
                    });
                });
            </script>


            <div id="divAttach" runat="server" visible="false" class="col-12 m-t-20">

                <div class="col-12" style="padding: 0px;">
                    <div class="m-t-20 card">
                        <div class="card-header">
                            <strong>Attachment</strong> Details
       
                        </div>
                        <div class="card-body">
                            <div class="table-responsive table--no-card">
                                <asp:GridView ID="grdAttach" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-borderless table-striped table-earning"
                                    EmptyDataText="No Records Found.">
                                    <Columns>

                                        <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>


                                        <asp:BoundField DataField="doc_type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                                        <asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="15%"></asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
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
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divDetails" runat="server" visible="false">
                    <div class="row form-group">
                        <div class="col col-md-3">
                            <label for="hf-password" class=" form-control-label">User</label>
                        </div>
                        <div class="col-12 col-md-9">
                            <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col col-md-3">
                            <label for="hf-password" class=" form-control-label">Type Of Test</label>
                        </div>
                        <div class="col-12 col-md-9">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="LVA">LVA</asp:ListItem>
                                <asp:ListItem Value="SDS">SDS</asp:ListItem>
                                <asp:ListItem Value="Narco">Narco</asp:ListItem>
                                <asp:ListItem Value="EyeDetect">Eye Detect</asp:ListItem>
                                <asp:ListItem Value="EyeDetectAudio">Eye Detect-Audio</asp:ListItem>
                                <asp:ListItem Value="MCTAudio">MCT Audio</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col col-md-3">
                            <label for="hf-email" class=" form-control-label">Date</label>
                        </div>
                        <div class="col-12 col-md-9">
                            <div class="row col-12" style="margin-right:0px !important; padding-right:0px !important;">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control col-11" placeholder="From Date"
                                    ReadOnly="true"></asp:TextBox>
                                <div class="input-group-append col-1" style="padding: 8px;">
                                    <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/cal.png" CssClass="menu-icon"
                                        Height="20" Width="20" />
                                    <%--<input type="text" class="form-control" id="txtDate" placeholder="Date" runat="server" disabled />--%>
                                    <asp:CalendarExtender ID="calExtFrom" runat="server" PopupButtonID="imgFromDate"
                                        TargetControlID="txtFromDate" Format="dd-MMM-yyyy" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col col-md-3">
                            <label for="hf-email" class=" form-control-label">Notes</label>
                        </div>
                        <div class="col-12 col-md-9">
                            <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="m-t-40">
                        <asp:Button ID="btnAssign" runat="server" Text="Assign"
                            CssClass="btn btn-dark btn-block" OnClick="btnAssign_Click" />
                    </div>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""
                    CssClass="col-12"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

