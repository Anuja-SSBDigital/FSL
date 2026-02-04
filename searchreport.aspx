<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="searchreport.aspx.cs" Inherits="searchreport" %>

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
                        <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-group">

                    <div class="col col-md-6">
                        <%--<asp:RadioButton ID="rdo_agencyname" runat="server" GroupName="rdo" OnCheckedChanged="rdo_agencyname_CheckedChanged" AutoPostBack="true"/>--%>
                        <label class="form-control-label">Agency Name/Police Station</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:TextBox runat="server" ID="txt_agencyname" CssClass="form-control m-b-10" Visible="false"></asp:TextBox>
                    </div>

                    <div class="col col-md-6">
                        <%--<asp:RadioButton ID="rdo_referenceno" runat="server" GroupName="rdo" OnCheckedChanged="rdo_referenceno_CheckedChanged" AutoPostBack="true"/>--%>
                        <label class="form-control-label">Reference No</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:TextBox runat="server" ID="txt_refernceno" CssClass="form-control m-b-10" Visible="false"></asp:TextBox>
                    </div>


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

            <div class="col-sm-12 form-horizontal m-t-30" runat="server" id="div_user" visible="false">
                <div class="row form-group" >

                    <div class="col col-md-6">

                        <label class="form-control-label">User</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:DropDownList ID="ddl_user" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>


            <div class="col-sm-12 form-horizontal">
                <div class="row form-group" runat="server" visible="true" id="div_normal">
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Case No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_dfsee" Text="RFSL/EE" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Year</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_year"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-horizontal" runat="server" id="lbl_div" visible="true">
                        <label class=" form-control-label">Division Code</label>
                        <span class="row_span" id="PSY_ToolTip" runat="server" visible="false" aria-label="You can only add LVA, BEOS, SDS, PSY, NARCO, P.Assessment Division.">
                            <i class="fa fa-info-circle"></i></span>
                        <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_div" Onchange="FunctionDivision();"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Number</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_no" Onchange="FunctionRange();"></asp:TextBox>
                        <%--<asp:RangeValidator ID="Rng_No" ControlToValidate="txt_no" runat="server" ErrorMessage="Enter"></asp:RangeValidator>--%>
                    </div>
                </div>

                <div class="row form-group" runat="server" id="div_fp" visible="false">
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Case No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_fp" Text="FP/CHP/OP" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Short Name</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_shortname"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-horizontal" runat="server" id="Div1" visible="true">
                        <label class=" form-control-label">Number</label>
                        <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_fpnumber"></asp:TextBox>
                        <%--<asp:RangeValidator ID="Rng_fpnumber" ControlToValidate="txt_fpnumber" MinimumValue="4" runat="server"></asp:RangeValidator>--%>
                    </div>
                    <div class="col-sm-2 form-horizontal">
                        <label class=" form-control-label">Year</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_fpyear"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 form-horizontal">
                        <label class=" form-control-label">Date</label>
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="txt_fpdate"></asp:TextBox>
                    </div>
                </div>
            </div>


            <div class="col-sm-3 form-horizontal" style="margin: auto;">
                <div class="row form-group">
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                    <asp:Button ID="btn_search" runat="server" OnClick="btn_search_Click" Text="Search"
                        CssClass="btn btn-dark btn-block" OnClientClick="return AcceptanceVal();" />
                </div>
            </div>
            <div class="main-content" runat="server" id="div_rpt" visible="false" style="padding-top: 35px;">
                <div class="section__content section__content--p30">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12" >
                                <asp:Button runat="server" ID="btn_generatepdf" CssClass="btn btn-primary" OnClick="btn_generatepdf_Click" Text="Generate PDF" Style="float: right; margin-bottom: 20px" />

                                <h3 id="title" runat="server" style="text-align: center;"></h3>
                                <div id="tblData">
                                <div class="table-responsive table--no-card m-b-30" >
                                    <table class="table table-borderless table-striped table-earning" id="tableID"  >
                                        <thead id="Header" runat="server">
                                            <tr>
                                                <th>No</th>
                                                <th>Case No</th>
                                                <th>Agency Name</th>
                                                <th >Reference No</th>
                                                <th >Status</th>
                                                <th >Notes</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rpt_details" OnItemDataBound="rpt_details_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                        <td><a href="Timeline.aspx?caseno=<%#Eval("caseno") %>"><%#Eval("caseno") %></a></td>
                                                        <td><%#Eval("agencyname") %></td>
                                                        <td ><%#Eval("agencyreferanceno") %></td>
                                                        <td>
                                                            <asp:HiddenField runat="server" ID="hf_status" Value='<%#Eval("status") %>' />
                                                            <asp:LinkButton runat="server" Visible="false" ID="lnk_pending" CssClass="text-danger">Pending</asp:LinkButton>
                                                            <asp:LinkButton runat="server" Visible="false" ID="lnk_completed" CssClass="text-success">Completed</asp:LinkButton>

                                                        </td>

                                                        <td ><%#Eval("notes") %></td>

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

        function FunctionDivision() {
            var HdnDivision = document.getElementById("<%= HdnDivision.ClientID %>");
            var Division = document.getElementById("<%= txt_div.ClientID %>");
            if (HdnDivision.value == "PSY") {
                if (Division.value == "LVA" || Division.value == "BEOS" || Division.value == "SDS"
                    || Division.value == "NARCO" || Division.value == "PSY" || Division.value == "P.Assessment") {

                } else {
                    alert("The Division code is not in Psychology Division.");
                    Division.value = "";
                }
            }
        }


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

