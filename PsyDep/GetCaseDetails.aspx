<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetCaseDetails.aspx.cs" Inherits="GetCaseDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags-->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <!-- Title Page-->
    <title>Case Details</title>

    <!-- Fontfaces CSS-->
    <link href="../css/font-face.css" rel="stylesheet" media="all" />
    <link href="../vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all" />
    <link href="../vendor/font-awesome-5/css/fontawesome-all.min.css" rel="stylesheet" media="all" />
    <link href="../vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all" />

    <!-- Bootstrap CSS-->
    <link href="../vendor/bootstrap-4.1/bootstrap.min.css" rel="stylesheet" media="all" />

    <!-- Vendor CSS-->
    <link href="../vendor/animsition/animsition.min.css" rel="stylesheet" media="all" />
    <link href="../vendor/bootstrap-progressbar/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" media="all" />
    <link href="../vendor/wow/animate.css" rel="stylesheet" media="all" />
    <link href="../vendor/css-hamburgers/hamburgers.min.css" rel="stylesheet" media="all" />
    <link href="../vendor/slick/slick.css" rel="stylesheet" media="all" />
    <link href="../vendor/select2/select2.min.css" rel="stylesheet" media="all" />
    <link href="../vendor/perfect-scrollbar/perfect-scrollbar.css" rel="stylesheet" media="all" />

    <!-- Main CSS-->
    <link href="../css/theme.css" rel="stylesheet" media="all" />
</head>
<body class="animsition">
    <form id="form1" runat="server">
        <div class="page-wrapper">

            <!-- HEADER DESKTOP-->
            <header class="header-desktop3 d-none d-lg-block">
                <div class="section__content section__content--p35">
                    <div class="header3-wrap">
                        <div class="header__logo">
                            <h2 class="text-light">SFT</h2>
                        </div>
                    </div>
                </div>
            </header>
            <!-- END HEADER DESKTOP-->
        </div>
        <div class="card">
            <div class="card-header">
                <strong>Case</strong> Details
       
            </div>
            <div class="card-body card-block">
                <div class="col-12">
                    <div class="form-header">
                        <div class="row form-group  col-6">
                            <div class="col-12 col-md-3">
                                <label for="hf-email" class=" form-control-label">Case No</label>
                            </div>
                            <div class="col-12 col-md-9">
                                <asp:TextBox ID="txtCaseNo" runat="server"
                                    CssClass="au-input au-input--xl" placeholder="Search Case By Id"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text=""
                        CssClass="col-12"></asp:Label>
                </div>


                <div class="col-12 m-t-20" id="divcd" runat="server">
                    <div class="table-responsive table--no-card">
                        <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-borderless table-striped table-earning"
                            EmptyDataText="No Records Found." OnRowDataBound="grdView_RowDataBound">
                            <Columns>

                                <asp:BoundField DataField="case_id" HeaderText="Case Id" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-Width="15%"></asp:BoundField>

                                <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="20%"></asp:BoundField>

                                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Width="15%"></asp:BoundField>

                            </Columns>
                        </asp:GridView>
                    </div>
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
                                    EmptyDataText="No Records Found." OnRowDataBound="grdAttach_RowDataBound">
                                    <Columns>

                                        <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>

                                        <asp:BoundField DataField="createddate" HeaderText="Date" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:BoundField DataField="hash" HeaderText="Hash" ItemStyle-Width="20%"></asp:BoundField>

                                        <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                                        <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="15%"></asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Jquery JS-->
        <script src="../vendor/jquery-3.2.1.min.js"></script>
        <!-- Bootstrap JS-->
        <script src="../vendor/bootstrap-4.1/popper.min.js"></script>
        <script src="../vendor/bootstrap-4.1/bootstrap.min.js"></script>
        <!-- Vendor JS       -->
        <script src="../vendor/slick/slick.min.js">
    </script>
        <script src="../vendor/wow/wow.min.js"></script>
        <script src="../vendor/animsition/animsition.min.js"></script>
        <script src="../vendor/bootstrap-progressbar/bootstrap-progressbar.min.js">
    </script>
        <script src="../vendor/counter-up/jquery.waypoints.min.js"></script>
        <script src="../vendor/counter-up/jquery.counterup.min.js">
    </script>
        <script src="../vendor/circle-progress/circle-progress.min.js"></script>
        <script src="../vendor/perfect-scrollbar/perfect-scrollbar.js"></script>
        <script src="../vendor/chartjs/Chart.bundle.min.js"></script>
        <script src="../vendor/select2/select2.min.js">
    </script>

        <!-- Main JS-->
        <script src="../js/main.js"></script>

    </form>
</body>
</html>
