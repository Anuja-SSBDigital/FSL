<%@ Page Title="Manage View Request" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GetViewRequest.aspx.cs" Inherits="GetViewRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .modal-center {
            width: 100%;
            margin: 10vh auto;
        }
        :disabled {
            cursor: no-drop;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Case Request</strong> Report
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row col-12 form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="P">Pending</asp:ListItem>
                            <asp:ListItem Value="A">Accepted</asp:ListItem>
                            <asp:ListItem Value="R">Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row col-6 form-group">
                    <div class="col-4">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return val();"
                            CssClass="btn btn-dark btn-block" OnClick="btnSearch_Click" />
                    </div>
                    <%--<div class="col-2"></div>
                    <div class="col-4">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="Viewreport();"
                            CssClass="btn btn-dark btn-block" />
                    </div>--%>
                </div>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"
                ScriptMode="Release">
            </asp:ScriptManager>
            <div class="col-lg-12">
                <h3>
                    <asp:Label ID="lblNoData" runat="server" Visible="false" Text="No Record found.."></asp:Label></h3>
                <div class="table-responsive table--no-card">
                    <asp:GridView ID="grdAttach" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdAttach_RowDataBound">
                        <Columns>

                            <%--<asp:BoundField DataField="hd_name" HeaderText="Hard Drive" ItemStyle-Width="20%"></asp:BoundField>--%>

                            <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="case_details" HeaderText="Case Details" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Width="40%"></asp:BoundField>
                            <asp:BoundField DataField="mail_id" HeaderText="Email Id" ItemStyle-Width="40%"></asp:BoundField>
                            <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="40%"></asp:BoundField>
                            <asp:BoundField DataField="notes" HeaderText="Remark" ItemStyle-Width="40%"></asp:BoundField>
                            <%--<asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="15%"></asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("status") %>' />
                                    <asp:HiddenField ID="hdnReqID" runat="server" Value='<%# Eval("req_id") %>' />
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("case_details") %>' />
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                        id="btnApprove" runat="server">
                                        <i class="fa fa-check"></i>
                                    </button>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                        id="btnRej" runat="server">
                                        <i class="fa fa-ban"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <script>
                        function ViewRequestReject(id, status, tomailid, code, reqid) {

                            document.getElementById("hdid").value = id;
                            document.getElementById("hdstatus").value = status;
                            document.getElementById("hdtomailid").value = tomailid;
                            document.getElementById("hdcode").value = code;
                            document.getElementById("hdreqid").value = reqid;

                            $('#modalRejectRequest').modal('show');
                        }
                        function ViewRequest(id, status, notes, tomailid, code, reqid) {
                            var ids = id.replaceAll("/","\\\\");
                            $.ajax({
                                url: '<%=ResolveUrl("~/GetViewRequest.aspx/SendMail") %>',
                                data: "{'Id': '" +
                                    ids + "', 'tomailid': '" +
                                    tomailid + "', 'code': '" +
                                    code + "','reqid': '" +
                                    reqid + "','status': '" +
                                    status + "','notes': '" +
                                    notes + "'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    alert(data.d);
                                },
                                error: function (response) {
                                    alert(response.responseText);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });
                            window.location.reload();
                        }
                    </script>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

