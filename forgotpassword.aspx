<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- Required meta tags-->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="au theme template" />
    <meta name="author" content="Hau Nguyen" />
    <meta name="keywords" content="au theme template" />

    <!-- Title Page-->
    <title>Forgot Password</title>

    <!-- Fontfaces CSS-->
    <link href="css/font-face.css" rel="stylesheet" media="all" />
    <link href="vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all" />
    <link href="vendor/font-awesome-5/css/fontawesome-all.min.css" rel="stylesheet" media="all" />
    <link href="vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all" />

    <!-- Vendor CSS-->
    <link href="vendor/animsition/animsition.min.css" rel="stylesheet" media="all" />
    <link href="vendor/bootstrap-progressbar/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" media="all" />
    <link href="vendor/wow/animate.css" rel="stylesheet" media="all" />
    <link href="vendor/css-hamburgers/hamburgers.min.css" rel="stylesheet" media="all" />
    <link href="vendor/slick/slick.css" rel="stylesheet" media="all" />
    <link href="vendor/select2/select2.min.css" rel="stylesheet" media="all" />
    <link href="vendor/perfect-scrollbar/perfect-scrollbar.css" rel="stylesheet" media="all" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <!-- Main CSS-->
    <link href="css/theme.css" rel="stylesheet" media="all" />
    <%-- <link href="css/main.css" rel="stylesheet" />--%>
    <style>
        body {
            background: #000;
            color: #ccc;
        }

        .btn.color-3 {
            background: transparent;
            color: #FFF;
            border: 1px solid #fff;
        }

            .btn.color-3 .fa {
                color: #fff;
            }

        .center {
            width: 390px;
        }

        .invalid {
            border: 3px solid red !important;
        }
    </style>
</head>

<body>

    <div class="head-bg-img"></div>

    <div class="head-bg-content ">
        <div class="col-sm-6 col-sm-push-3 col-md-6 col-lg-4 col-lg-push-4">
            <div class="login-logo">
                <div class="image text-center img-250" style="height: auto; margin: 0 auto;">
                   <%-- <img src="images/logo.png" alt="DFS" />--%>
                </div>
                  <h2 style="color:white;" class="m-b-20">e-Sanrakhshan</h2>
                 <h4 style="color:white;" class="m-b-20">Forgot Password</h4>
            </div>

            <div class="login-form">
                <form runat="server">
                    <div class="form-group">
                        <label>Username</label>
                        <asp:TextBox ID="txtUN" runat="server"
                            CssClass="au-input au-input--full"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Enter Your Registered EmailID</label>
                        <asp:TextBox ID="txtEmailid" runat="server"
                            CssClass="au-input au-input--full" ></asp:TextBox>
                    </div>
                    <div class="login-checkbox" style="margin-bottom: 5px; text-align: left;">
                        
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                        CssClass="au-btn au-btn--block au-btn--green m-b-20" OnClick="btnSubmit_Click"  />
                    <a href="login.aspx"><b>Back To Login</b></a>
                   <%-- <div class="form-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White"
                            NavigateUrl="~/RegisterViewRequest.aspx">Request To View Case Report</asp:HyperLink>
                    </div>--%>
                </form>
            </div>

        </div>
         <div class="bg-white col-md-12 navbar-fixed-bottom">
        <div class="copyright" style="padding:13px 0;width: 30%;margin: auto;">
                   <h4 style="color: #28166f;">
                    <img class="header-logo" src="images/dfs_logo.png" runat="server" visible="false" alt="SSB Forensic Tool" style="vertical-align: middle; margin-left: -60px;">
                  Forensic Science Laboratory</h4>
             
        </div>
    </div>
    </div>



    <!-- Jquery JS-->
    <script src="vendor/jquery-3.2.1.min.js"></script>
    <!-- Bootstrap JS-->
    <script src="vendor/bootstrap-4.1/popper.min.js"></script>
    <script src="vendor/bootstrap-4.1/bootstrap.min.js"></script>
    <!-- Vendor JS       -->
    <script src="vendor/slick/slick.min.js">
    </script>
    <script src="vendor/wow/wow.min.js"></script>
    <script src="vendor/animsition/animsition.min.js"></script>
    <script src="vendor/bootstrap-progressbar/bootstrap-progressbar.min.js">
    </script>
    <script src="vendor/counter-up/jquery.waypoints.min.js"></script>
    <script src="vendor/counter-up/jquery.counterup.min.js">
    </script>
    <script src="vendor/circle-progress/circle-progress.min.js"></script>
    <script src="vendor/perfect-scrollbar/perfect-scrollbar.js"></script>
    <script src="vendor/chartjs/Chart.bundle.min.js"></script>
    <script src="vendor/select2/select2.min.js">
    </script>

    <!-- Main JS-->
    <script src="js/main.js"></script>

</body>

</html>
