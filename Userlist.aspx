<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Userlist.aspx.cs" Inherits="Userlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

     <link rel="stylesheet" href="https://cdn.datatables.net/1.10.22/css/jquery.dataTables.min.css" />
    <%-- <style>
         .focus {
            background-color: #273b86;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            width:30px;
        }

        .pageNumber {
            padding: 2px;
        }

        table {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        td,
        th {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }

        tr:nth-child(even) {
            background-color: #dddddd;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Activate/DeActivate User</strong> list                       
        </div>
        <div class="card-body card-block">
            
            <div class="col-sm-12 form-horizontal">
                <asp:HiddenField ID="HdnDivision" runat="server" />
                <div class="row form-group">
                    <div class="col col-md-6">
                        <label class=" form-control-label">Institute</label>
                       <asp:DropDownList runat="server" ID="ddl_inst" OnSelectedIndexChanged="ddl_inst_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col col-md-6">
                        <label class=" form-control-label">Department</label>
                        <%--<asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control" >--%>
                     <asp:DropDownList runat="server" ID="ddl_department"  CssClass="form-control"></asp:DropDownList>

                    </div>
                </div>
            </div>

               <div class="col-sm-3 form-horizontal" style="margin: auto;">
                <div class="row form-group">
                    <asp:HiddenField ID="HiddenField2" runat="server" Value="" />
                    <asp:Button ID="btn_search" runat="server" OnClick="btn_search_Click"  Text="Search"
                        CssClass="btn btn-dark btn-block" />
                </div>
            </div>

            <div class="col-sm-3 form-horizontal" style="margin: auto;">
                <div class="row form-group">
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />

                </div>
            </div>
            <div class="main-content" runat="server" id="div_rpt" style="padding-top: 35px;">
                <div class="section__content section__content--p30">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <h3 id="title" runat="server" style="text-align: center;"></h3>
                                  <div id="tblData">
                                <div class="table-responsive table--no-card m-b-30">
                                    <table class="table table-borderless table-striped table-earning" id="tableID" >
                                        <thead id="Header" runat="server">
                                            <tr>
                                                <th>No</th>
                                                <th>UserName</th>
                                                <th>Name</th>
                                                <th>Designation</th>
                                                <%--<th>Department</th>--%>
                                                <th>Division</th>
                                                <%-- <th>Email</th>--%>
                                                <th class="text-right">Action</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rpt_details" OnItemCommand="rpt_details_ItemCommand" OnItemDataBound="rpt_details_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                        <td>
                                                            <asp:HiddenField ID="hf_userid" runat="server" Value='<%#Eval("userid") %>' />
                                                            <%#Eval("username") %></td>
                                                        <td><%#Eval("firstname") %>  <%#Eval("lastname") %> </td>
                                                        <td><%#Eval("designation") %></td>
                                                        <%--<td><%#Eval("dept_name") %> </td>--%>
                                                        <td><%#Eval("div_name") %></td>
                                                        <%--<td class="text-right"><%#Eval("email") %></td>--%>
                                                        <%--<td class="text-right"> <asp:LinkButton ID="lnk_edit" CommandName="lnk_edit" CommandArgument='<%#Eval("userid") %>' runat="server" CssClass="btn btn-primary btn-sm"><span class="feather icon-edit"></span>Edit</asp:LinkButton></td>--%>
                                                        <td class="text-right">
                                                            <%--  <%#Eval("isactive").ToString() == "1" ?"Active":"Deactive" %>--%>
                                                            <asp:HiddenField runat="server" ID="Hdn_IsActive" Value='<%#Eval("isactive") %>' />

                                                            <asp:LinkButton ID="link_Active" CommandName="link_Active"
                                                                CommandArgument='<%#Eval("userid") %>' runat="server"
                                                                CssClass="btn btn-icons btn-rounded btn-outline-success" Visible="false">
                                                           Active <i class="fa fa-shield-alt"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="link_DeActive" CommandName="link_DeActive"
                                                                CommandArgument='<%#Eval("userid") %>' runat="server"
                                                                CssClass="btn btn-icons btn-rounded btn-outline-danger" Visible="false">
                                                           DeActive 
                                                            
                                                            <i class="fa fa-shield-alt"></i></asp:LinkButton>


                                                            <asp:LinkButton ID="lnk_edit" CommandName="lnk_edit"
                                                                CommandArgument='<%#Eval("userid") %>' runat="server"
                                                                CssClass="btn btn-icons btn-rounded btn-outline-primary">
                                                           <i class="fa fa-edit"></i></asp:LinkButton>

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
    </div>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#tableID').DataTable({});
        });
    </script>

     
</asp:Content>

