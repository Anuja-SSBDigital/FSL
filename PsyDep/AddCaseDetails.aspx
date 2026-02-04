<%@ Page Title="Case Details" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="AddCaseDetails.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="card">
        <div class="card-header">
            <strong>Active Case</strong> Details
       
        </div>
        <div class="card-body card-block">
            <div class="col-12">
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
                <asp:Label ID="lblMsg" runat="server" Text=""
                    CssClass="col-12"></asp:Label>
            </div>
            <div class="col-12 m-t-20" id="divcd" runat="server">
                <div class="col-12 row m-b-20">
                    External URL : 
                    <asp:HyperLink ID="lblURL" runat="server" CssClass="text-info m-l-10"
                        Target="_blank">
                        </asp:HyperLink>
                </div>
                 <div class="col-12 row m-b-20">
                     <div class="row form-group col-12">
                        <div class="col col-md-2">
                            <label for="hf-password" class=" form-control-label">Status</label>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                         <div class="col col-md-3">
                             <asp:Button ID="btnUpdate" runat="server" Text="Update"
                                 CssClass="btn btn-dark" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                 </div>
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdView_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="case_id" HeaderText="Case Id" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="20%"></asp:BoundField>


                        </Columns>
                    </asp:GridView>
                </div>
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


                                        <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                                        <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="15%"></asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row form-group  col-12 m-t-20">
                                <div class="col col-md-3">
                                    <label for="hf-email" class=" form-control-label">Type</label>
                                </div>
                                <div class="col-12 col-md-9">
                                    <asp:TextBox ID="txtRepType" runat="server" CssClass="form-control"
                                        ReadOnly="true" Text="Reports"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group  col-12">
                                <div class="col col-md-3">
                                    <label for="hf-email" class=" form-control-label">Notes</label>
                                </div>
                                <div class="col-12 col-md-9">
                                    <asp:TextBox ID="txtNoteRep" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group  col-12">
                                <div class="col col-md-3">
                                    <label for="hf-email" class=" form-control-label">File</label>
                                </div>
                                <div class="col-12 col-md-9 ">
                                    <div class="file-loading">
                                        <%--<input type="file" id="fuRep" class="form-group file" />--%>
                                        <asp:FileUpload ID="fuRep" runat="server"
                                            CssClass="form-group file" />
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row form-group">
                                    <asp:Button ID="btnAddRep" runat="server" Text="Add"
                                        CssClass="btn btn-dark btn-block" OnClick="btnAddRep_Click" />
                                </div>--%>
                            <div class="m-t-40">
                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                    CssClass="btn btn-dark btn-block" OnClick="btnAdd_Click" />
                            </div>

                            <script src="File_Input/fileinput.js"></script>
                            <script src="File_Input/theme.js"></script>
                            <script src="File_Input/popper.min.js"></script>
                            <script src="File_Input/bootstrap.min.js"></script>
                            <script>
                                $("#fuCerti").fileinput({
                                    theme: 'fa',
                                    uploadUrl: '#',
                                    overwriteInitial: false,
                                    maxFileSize: 2000,
                                    maxFilesNum: 10,
                                    slugCallback: function (filename) {
                                        return filename.replace('(', '_').replace(']', '_');
                                    }
                                });
                            </script>
                        </div>
                    </div>
                </div>


                <div class="table-responsive table--no-card m-t-20">
                    <asp:GridView ID="grdFile" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdView_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>

                            <%--<asp:BoundField DataField="date" HeaderText="Date" ItemStyle-Width="20%"></asp:BoundField>--%>
                            <asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:BoundField DataField="hash" HeaderText="Hash" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="upload" HeaderText="Upload" ItemStyle-Width="15%"></asp:BoundField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="m-t-40">
                    <asp:Button ID="btnInsert" runat="server" Text="Insert"
                        CssClass="btn btn-dark btn-block" OnClick="btnInsert_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function openCity(evt, tabName) {
            var i, x, tablinks;
            x = document.getElementsByClassName("tab");
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablink");
            for (i = 0; i < x.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" w3-border-red", "");
            }
            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.firstElementChild.className += " w3-border-red";
        }
        document.getElementById("defaultOpen").click();
    </script>
    <script>
        function CaseDetails(userid, caseno, notes) {
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtCaseNo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("AddCaseDetails.aspx/BindAutoCompleteList") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
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
</asp:Content>

