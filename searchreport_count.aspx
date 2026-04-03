<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="searchreport_count.aspx.cs" Inherits="searchreport_count" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.22/css/jquery.dataTables.min.css" />
    <style>
        #loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.4);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 9999;
        }

        .spinner {
            border: 6px solid #f3f3f3;
            border-top: 6px solid #3498db;
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 1s linear infinite;
        }

        @keyframes spin {
            100% {
                transform: rotate(360deg);
            }
        }
    </style>
    <div id="loader" style="display: none;">
        <div class="spinner"></div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Search</strong> Report                 
        </div>
        <div class="card-body card-block">
            <%--<div class="col-sm-12 form-horizontal">
             <div class="row form-group" runat="server" visible="false" id="div_dept">
                 <div class="col col-md-6">
                     <label class=" form-control-label">Division</label>
                       <div class="col col-md-6">
                  <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                 </div>
                 </div>
             </div>
         </div>--%>

            <div class="col-sm-12 form-horizontal">
                <asp:HiddenField ID="HdnDivision" runat="server" />
                <div class="row form-group">
                    <div class="col col-md-6">
                        <label class=" form-control-label">From Date</label>
                        <asp:TextBox ID="txt_fromdate" TextMode="Date" runat="server" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                    <div class="col col-md-6">
                        <label class=" form-control-label">To Date</label>
                        <%--<asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control" >--%>
                        <asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control" onchange="myChangeFunction();">
                        </asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 form-horizontal m-t-30">
                <div class="row form-group" runat="server" visible="false" id="div_dept">
                    <div class="col col-md-6">
                        <label class="form-control-label">Division</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">



                    <div class="col col-md-6">
                        <label class="form-control-label">Status</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                            <asp:ListItem Value="-1">Select Status</asp:ListItem>

                            <asp:ListItem Value="Pending for Assign">Pending</asp:ListItem>
                            <asp:ListItem Value="Report Submission">Completed</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>






            <div class="col-sm-3 form-horizontal" style="margin: auto;">
                <div class="row form-group">
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                    <asp:Button ID="btn_search" runat="server" OnClick="btn_search_Click" Text="Search"
                        CssClass="btn btn-dark btn-block" OnClientClick="showLoader();" />

                </div>
            </div>
            <h4 id="title" runat="server" style="text-align: center;"></h4>
            <!-- ==================== USER-WISE CASE SUMMARY TABLE ==================== -->
            <div id="div_userSummary" runat="server" visible="false" style="margin-top: 25px;">
                <h4>User-wise Case Summary</h4>
                <asp:Button runat="server" ID="btn_generatepdf" CssClass="btn btn-primary" OnClick="btn_generatepdf_Click1" Text="Generate PDF" Style="float: right; margin-bottom: 20px" />

                <table class="table table-borderless table-striped table-earning" id="userSummaryTable">
                    <thead id="userHeader" runat="server">
                        <tr>
                            <th style="width: 50px;">No</th>
                            <th>User Name</th>
                            <th>Department</th>
                            <%--<th style="text-align: center;">Total Cases</th>--%>
                            <th style="text-align: center; color: green;">Completed</th>
                            <%--<th style="text-align: center; color: #ff9800;">Pending</th>--%>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Repeater_userSummary" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>' />
                                    </td>
                                    <td><%# Eval("UserName") %></td>
                                    <td><%# Eval("Department") %></td>
                                    <%-- <td style="text-align: center; font-weight: bold;">
                                        <%# Eval("TotalCases") %>
                                    </td>--%>
                                    <td style="text-align: center; color: green; font-weight: bold;">
                                        <%# Eval("CompleteCases") %>
                                    </td>
                                    <%-- <td style="text-align: center; color: #ff9800; font-weight: bold;">
                                        <%# Eval("PendingCases") %>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="https://cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementById("loader").style.display = "flex";
        });

        window.onload = function () {
            setTimeout(function () {
                document.getElementById("loader").style.display = "none";
            }, 300); // small delay for smooth hide
        };
        function showLoader() {
            document.getElementById("loader").style.display = "flex";
        }
    </script>
    <script>




        $(document).ready(function () {
            $('#userSummaryTable').DataTable({});
        });
        $(document).ready(function () {
            $('#tableID1').DataTable({});
        });

        //$(document).ready(function () {
        //    var totalRows = $('#tblData').find('tbody tr:has(td)').length;
        //    var recordPerPage = 10;
        //    var totalPages = Math.ceil(totalRows / recordPerPage);
        //    var $pages = $('<div id="pages" class="float-xl-right floating-buttons"></div>');
        //    for (i = 0; i < totalPages; i++) {

        //        if (i == 0) {

        //            $('<span class="pageNumber btn-kv focus" id="SrNo_'+(i + 1)+'">&nbsp;' + (i + 1) + '</span>').appendTo($pages);
        //        } else {
        //            $('<span class="pageNumber btn-kv" id="SrNo_'+ (i + 1) +'">&nbsp;' + (i + 1) + '</span>').appendTo($pages);
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



        /* Initialization of datatable */


  <%--   function AcceptanceVal() {

         var year = document.getElementById("<%= txt_year.ClientID %>");
          var div = document.getElementById("<%= txt_div.ClientID %>");
          var no = document.getElementById("<%= txt_no.ClientID %>");
              
          if (year.value == "") {
              year.classList.add('is-invalid');
              return false;
          } else {
              year.classList.remove('is-invalid');

          }



          if (no.value == "") {
              no.classList.add('is-invalid');
              return false;
          } else {
              no.classList.remove('is-invalid');
          }

        

          if (ReferanceNo.value == "") {
              ReferanceNo.classList.add('is-invalid');
              return false;
          } else {
              ReferanceNo.classList.remove('is-invalid');
          }

          if (PoliceStation.value == "") {
              PoliceStation.classList.add('is-invalid');
              return false;
          } else {
              PoliceStation.classList.remove('is-invalid');
          }


          if (div.value == "") {
              div.classList.add('is-invalid');
              return false;
          } else {
              div.classList.remove('is-invalid');
          }

          return true;
      }--%>

    </script>
</asp:Content>

