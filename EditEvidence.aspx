<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditEvidence.aspx.cs" Inherits="EditEvidence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.22/css/jquery.dataTables.min.css" />
       <%--<style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
       <div class="card">
        <div class="card-header">
            <strong>Edit</strong> Evidence                       
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">From Date</label>
                    </div>
                    <div class="col-12 col-md-6">
                        <asp:TextBox ID="txt_fromdate" TextMode="Date" runat="server" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">To Date</label>
                    </div>
                    <div class="col-12 col-md-6">
                         <asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label class=" form-control-label">Division</label>
                    </div>
                    <div class="col-12 col-md-6">
                           <asp:DropDownList ID="ddlDep" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
             <div class="col-sm-3 form-horizontal" style="margin:auto;">
                <div class="row form-group">
                     <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                    <asp:Button ID="Button1" runat="server" OnClick="btn_Search_Click" Text="Search" 
                        CssClass="btn btn-dark btn-block"/>
                </div>
            </div>
            <div class="main-content" runat="server" id="div_rpt" visible="false" style="padding-top:35px;">
                <div class="section__content section__content--p30">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                    <h3 id="title" runat="server"  style="text-align:center;"></h3>
                                  <div id="tblData">
                              <div class="table-responsive table--no-card m-b-30" >
                                    <table class="table table-borderless table-striped table-earning" id="tableID">
                                        <thead id="Header" runat="server">
                                            <tr>
                                                <th class="text-right">No</th>
                                                <th>Case No</th>
                                                <th class="text-right">Agency Name</th>
                                                <th>Refernce No</th>
                                                <th>No of Exhibits</th>
                                                <th class="text-right">Action</th>
                                              
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rpt_details" OnItemCommand="rpt_details_ItemCommand">
                                                <ItemTemplate>
                                            <tr>
                                                <td> <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                <td><%#Eval("caseno") %></td>
                                                <td><%#Eval("agencyname") %></td>
                                                <td class="text-right"><%#Eval("agencyreferanceno") %></td>
                                                <td class="text-right"><%#Eval("noof_exhibits") %></td>
                                                <td class="text-right"> 
                                                    <asp:LinkButton ID="lnk_edit" CommandName="lnk_edit" CommandArgument='<%#Eval("evidenceid") %>' runat="server" CssClass="btn btn-primary btn-sm" Text="Edit">
                                                         <i class="fa fa-edit"></i></asp:LinkButton></td>
                                               
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
         //$(document).ready(function () {
         //    var totalRows = $('#tblData').find('tbody tr:has(td)').length;
         //    var recordPerPage = 10;
         //    var totalPages = Math.ceil(totalRows / recordPerPage);
         //    var $pages = $('<div id="pages" class="float-xl-right floating-buttons"></div>');
         //    for (i = 0; i < totalPages; i++) {

         //        if (i == 0) {

         //            $('<span class="pageNumber btn-kv focus" id="SrNo_' + (i + 1) + '">&nbsp;' + (i + 1) + '</span>').appendTo($pages);
         //        } else {
         //            $('<span class="pageNumber btn-kv" id="SrNo_' + (i + 1) + '">&nbsp;' + (i + 1) + '</span>').appendTo($pages);
         //        }
         //    }
         //    $pages.appendTo('#tblData');

         //    $('.pageNumber').hover(
         //        function () {
         //            $(this).addClass('focus');


         //        },
         //        function () {
         //            $(this).removeClass('focus');

         //        }
         //    );

         //    $('table').find('tbody tr:has(td)').hide();
         //    var tr = $('table tbody tr:has(td)');
         //    for (var i = 0; i <= recordPerPage - 1; i++) {
         //        $(tr[i]).show();
         //    }
         //    $('span').click(function (event) {
         //        $('#tblData').find('tbody tr:has(td)').hide();
         //        //var currentText = $(this).text();
         //        var currentText = $(this).text().trim();
         //        $(this).addClass('focus');

         //        //$('<span class="pageNumber btn-kv focus" id="SrNo_"' + (currentText) +'>' + (currentText) + '</span>');
         //        var nBegin = ($(this).text() - 1) * recordPerPage;
         //        var nEnd = $(this).text() * recordPerPage - 1;
         //        for (var i = nBegin; i <= nEnd; i++) {
         //            $(tr[i]).show();
         //        }
         //    });
         //});
     </script>

   
</asp:Content>

