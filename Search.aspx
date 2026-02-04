<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags-->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />


    <!-- Title Page-->
    <title>Search Report</title>

    <!-- Main CSS-->
    <link href="css/theme.css" rel="stylesheet" media="all" />
    <link href="css/font-face.css" rel="stylesheet" media="all" />
    <link href="vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all" />
    <link href="vendor/font-awesome-5/css/fontawesome-all.min.css" rel="stylesheet" media="all" />
    <link href="vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all" />

    <!-- Bootstrap CSS-->
    <link href="vendor/bootstrap-4.1/bootstrap.min.css" rel="stylesheet" media="all" />

    <!-- Vendor CSS-->
    <link href="vendor/animsition/animsition.min.css" rel="stylesheet" media="all" />
    <link href="vendor/bootstrap-progressbar/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" media="all" />
    <link href="vendor/wow/animate.css" rel="stylesheet" media="all" />
    <link href="vendor/css-hamburgers/hamburgers.min.css" rel="stylesheet" media="all" />
    <link href="vendor/slick/slick.css" rel="stylesheet" media="all" />
    <link href="vendor/select2/select2.min.css" rel="stylesheet" media="all" />
    <link href="vendor/perfect-scrollbar/perfect-scrollbar.css" rel="stylesheet" media="all" />

    <link href="css/jquery-ui.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.22/css/jquery.dataTables.min.css" />

  <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <style>
        .wrapper {
            margin: 30px auto;
            text-align: center;
        }

        h1 {
            margin-bottom: 1em;
            color: #007bff;
        }

        #pagination-demo {
            display: inline-block;
            margin-bottom: 1em;
            margin-top: 1em;
        }

            #pagination-demo li {
                display: inline-block;
            }

        #data td, #data th {
            border: 1px solid #ddd;
            padding: 6px;
        }

        #data tr:hover {
            background-color: #ddd;
        }

        .page-content {
            background: #eee;
            display: inline-block;
            padding: 10px;
            width: 100%;
            max-width: 660px;
        }

        #data th {
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: left;
            background-color: #007bff;
            color: white;
        }

        table, th, td {
            border: 1px solid black;
        }

        #page-content {
            color: white;
            background-color: #007bff;
        }
    </style>--%>


    


    <script src="vendor/jquery-3.2.1.min.js"></script>
    <link href="css/bootstrap-glyphicons.css" rel="stylesheet" />
    <link href="css/bootstrap-glyphicons.min.css" rel="stylesheet" />
    <script src="js/jquey-1.10.0.min.js"></script>
    <script src="js/jquery-ui.min.js"></script>
    <style>
        .content {
            padding-top: 30px;
            min-height: 100vh;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="page-wrapper">
                <div class="content">
                    <div class="section__content section__content--p30">
                        <div class="container-fluid">
                            <div class="card">
                                <div class="card-header">
                                    <strong>
                                        <asp:Label ID="lblTitle" runat="server">Search Report</asp:Label>
                                    </strong>
                                </div>

                                <div class="card-body card-block">
                                    <div class="col-sm-12 form-horizontal">
                                        <div class="row form-group">
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
                                        </div>
                                        <div class="col-sm-12 form-horizontal m-t-30">
                                            <div class="row form-group" runat="server" id="div_dept">
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
                                                    <asp:TextBox runat="server" ID="txt_agencyname" CssClass="form-control m-b-10"></asp:TextBox>
                                                </div>

                                                <div class="col col-md-6">
                                                    <%--<asp:RadioButton ID="rdo_referenceno" runat="server" GroupName="rdo" OnCheckedChanged="rdo_referenceno_CheckedChanged" AutoPostBack="true"/>--%>
                                                    <label class="form-control-label">Reference No</label>
                                                </div>
                                                <div class="col col-md-6">
                                                    <asp:TextBox runat="server" ID="txt_refernceno" CssClass="form-control m-b-10"></asp:TextBox>
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
                                        <div class="col-sm-12 form-horizontal m-t-30" runat="server" id="div_user">
                                            <div class="row form-group">

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
                                        <div class="form-group m-t-30">

                                            <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                                            <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click"
                                                CssClass="btn btn-dark btn-block" Style="width: 500px; margin-left: 500px;" />
                                        </div>

                                        <div class="main-content" runat="server" id="div_rpt" visible="false" style="padding-top: 35px;">
                                            <div class="section__content section__content--p30">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-lg-12">

                                                            <h3 id="title" runat="server" style="text-align: center;"></h3>
                                                            <div id="tblData">
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <table class="table table-borderless table-striped table-earning"  id="tableID" >
                                                                        <thead id="Header" runat="server">
                                                                            <tr>
                                                                                <th>No</th>
                                                                                <th>Case No</th>
                                                                                <th>Agency Name</th>
                                                                                <th class="text-right">Refernce No</th>
                                                                                <th class="text-right">Status</th>
                                                                                <th class="text-right">Notes</th>

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
                                                                                        <td class="text-right"><%#Eval("agencyreferanceno") %></td>
                                                                                        <td>
                                                                                            <asp:HiddenField runat="server" ID="hf_status" Value='<%#Eval("status") %>' />
                                                                                            <asp:LinkButton runat="server" Visible="false" ID="lnk_pending" CssClass="text-danger">Pending</asp:LinkButton>
                                                                                            <asp:LinkButton runat="server" Visible="false" ID="lnk_completed" CssClass="text-success">Completed</asp:LinkButton>

                                                                                        </td>

                                                                                        <td class="text-right"><%#Eval("notes") %></td>

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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

         <%--//<script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>--%>

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

            /* Initialization of datatable */
            $(document).ready(function () {
                $('#tableID').DataTable({});
            });




            //(function ($) {
            //    debugger;
            //    'use strict';
            //    $(function () {
            //        $('#order-listing').DataTable({
            //            "aLengthMenu": [
            //                [5, 10, 15, -1],
            //                [5, 10, 15, "All"]
            //            ],
            //            "iDisplayLength": 10,
            //            "language": {
            //                search: ""
            //            }
            //        });
            //        $('#order-listing').each(function () {
            //            var datatable = $(this);
            //            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            //            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            //            search_input.attr('placeholder', 'Search');
            //            search_input.removeClass('form-control-sm');
            //            // LENGTH - Inline-Form control
            //            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            //            length_sel.removeClass('form-control-sm');
            //        });
            //    });
            //})(jQuery);





        </script>
    </form>
</body>
</html>
