<%@ Page Title="" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="AddReqDoc.aspx.cs" Inherits="PsyDep_AddReqDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .main-section {
            margin: 0 auto;
            padding: 20px;
            /*margin-top: 100px;*/
            background-color: #fff;
            box-shadow: 0px 0px 20px #c1c1c1;
        }

        .fileinput-remove,
        .fileinput-upload {
            display: none;
        }

        .file-preview-pdf {
            width: 188px !important;
        }

        .file-zoom-detail {
            width: 100% !important;
        }

        .btn-file {
            background: #4272d7;
            color: #fff;
        }

            .btn-file:hover {
                background: #3868cd;
            }
    </style>
    <link href="../css/fileinput.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>Required Document</strong> Details
       
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
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-password" class=" form-control-label">Required Documents</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlReq" runat="server" CssClass="form-control">
                            <asp:ListItem Value="FIR">FIR Copy</asp:ListItem>
                            <asp:ListItem Value="Aadhar">AADHAR Card</asp:ListItem>
                            <asp:ListItem Value="PAN">PAN Card</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">File</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <div class="file-loading">
                            <%--<input type="file" id="" class=" form-group file" />--%>
                            <asp:FileUpload ID="fuReq" runat="server"
                                CssClass="form-group file" />
                        </div>
                    </div>
                </div>
                <div class="m-t-40">
                    <asp:Button ID="btnAdd" runat="server" Text="Add"
                        CssClass="btn btn-dark btn-block" OnClick="btnAdd_Click" />
                </div>
                <script>
                    $("#fuReq").fileinput({
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
                <asp:Label ID="lblMsg" runat="server" Text=""
                    CssClass="col-12"></asp:Label>
                <div class="m-t-20" id="divcd" runat="server" visible="false">


                    <div class="table-responsive table--no-card m-t-20">
                        <asp:GridView ID="grdFile" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-borderless table-striped table-earning"
                            EmptyDataText="No Records Found." OnRowDataBound="grdFile_RowDataBound">
                            <Columns>

                                <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>

                                <%--<asp:BoundField DataField="date" HeaderText="Date" ItemStyle-Width="20%"></asp:BoundField>--%>
                                <asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="20%"></asp:BoundField>

                                <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
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
                    <div class="m-t-40">
                        <asp:Button ID="btnInsert" runat="server" Text="Insert"
                            CssClass="btn btn-dark btn-block" OnClick="btnInsert_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

