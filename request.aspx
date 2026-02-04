<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="request.aspx.cs" Inherits="request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Report Request</strong> Details                              
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">

                    <div class="col-12 col-md-6">
                        <label class=" form-control-label">Select Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="ALL Status" Value="-1" />
                            <asp:ListItem Text="Approve" Value="Approve" />
                            <asp:ListItem Text="Reject" Value="Reject" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6 mt-auto">
                        <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="Search" CssClass="btn btn-dark" />
                    </div>
                </div>
            </div>
            <div runat="server" id="div_rpt" style="padding-top: 35px;">
                <div class="section__content section__content--p30">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <h3 id="title" runat="server" style="text-align: center;"></h3>
                                <div class="table-responsive table--no-card m-b-30">
                                    <table class="table table-borderless table-striped table-earning">
                                        <thead id="Header" runat="server" visible="false">
                                            <tr>
                                                <th>No</th>
                                                <th>Case No</th>
                                                <th>Status</th>
                                                <th>Requested By</th>
                                                <th>Remarks by Officer</th>
                                                <%--<th class="text-right">Requested On</th>--%>
                                                <th >File</th>
                                                <th class="text-right">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Label ID="grdFile" runat="server" CssClass="table table-borderless table-striped table-earning"
                                                EmptyDataText="No Records Found." OnRowDataBound="grdView_RowDataBound"></asp:Label>
                                            <asp:Repeater runat="server" ID="rpt_details" OnItemDataBound="rpt_details_ItemDataBound" >
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                        <%--<td><%#Eval("caseno") %></td>--%>
                                                        <td><a href="Timeline.aspx?caseno=<%#Eval("caseno") %>"><%#Eval("caseno") %></a></td>
                                                        <td><%#Eval("officerstatus") %></td>
                                                        <td><%#Eval("requestedby") %></td>
                                                        <td class="text-right"><%#Eval("remarksbyofficer") %></td>
                                                      <%--  <td><asp:Label runat="server" Visible="false" ID="hf_date" Text='<%#Eval("requestedon") %>' ></asp:Label>
                                                            <asp:Label runat="server" ID="lbl_date"></asp:Label>
                                                        </td>--%>
                                                         <td><asp:HiddenField runat="server" ID="hdf_fileupload" Value='<%#Eval("fileupload") %>' />
                                                             <asp:HyperLink target="_blank"  runat="server" Text="View File" ID="hpl_fileupload" NavigateUrl='<%#Eval("fileupload") %>'></asp:HyperLink>
                                                             <%--<a href='<%#Eval("fileupload") %>' target='_blank'>View File</a>--%>

                                                         </td>
                                                       
                                                        <asp:HiddenField runat="server" ID="hdn_hodstatus" Value='<%#Eval("hodstatus") %>' />
                                                        <%--<td></td>--%>
                                                        <td>
                                                            <asp:LinkButton ID="lnk_edit" Visible="true" CssClass="btn btn-primary btn-sm btnApprove" CommandName="lnk_edit" data-id='<%#Eval("requestid") %> ' data-toggle="modal" data-target="#modalapprove" runat="server"><span class="feather icon-edit"></span>Approve/Reject</asp:LinkButton>

                                                            <asp:Label ID="lblapprove" ForeColor="Green" Text="Approve" runat="server" Visible="false"></asp:Label>

                                                            <asp:Label ID="lblreject" Text="Reject" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>




    <%-- <div class="modal fade" id="modalapprove" role="dialog" style="margin-left: 25%;width: 50%;">
            <div class="modal-dialog">--%>
    <!-- Modal content-->
    <%--<div class="modal-content">
        <div class="modal-header">--%>
    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
    <%-- <h4 class="modal-title">Send Request</h4>
        </div>
        <div class="modal-body">
          <p>Approve/Reject Request</p>
            <asp:HiddenField runat="server" ID="hf_evidenceid" />
            <div class="row">
                <div class="col-12">
            <asp:DropDownList runat="server" ID="ddl_status" CssClass="form-control">
                 <asp:ListItem Text="Select Status" Value="-1" />
                 <asp:ListItem Text="Approve" Value="Approve" />
                 <asp:ListItem Text="Reject" Value="Reject" />
             </asp:DropDownList>
                    </div>
               <div class="col-12"> 
            
            <asp:TextBox runat="server" CssClass="form-control" ID="txt_remarks" TextMode="MultiLine"></asp:TextBox>
                </div>
                </div>
        </div>
        <div class="modal-footer">
            <asp:Button runat="server" Text="Submit" ID="btn_submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            
        </div>
      </div>
            </div>
        </div>--%>



    <script>


</script>
</asp:Content>

