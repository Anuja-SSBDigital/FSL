<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterViewRequest.aspx.cs" Inherits="RegisterViewRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags-->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />


    <!-- Title Page-->
    <title>Add View Request</title>

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
                                        <asp:Label ID="lblTitle" runat="server">Request For Case Report</asp:Label>
                                    </strong>
                                </div>

                                <div class="card-body card-block">
                                    <div class="col-sm-12 form-horizontal">
                                        <div class="row form-group">
                                            <div class="col-sm-6 form-horizontal">
                                                <asp:HiddenField ID="hdnReqId" runat="server" />
                                                <label class=" form-control-label">Name</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtName"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-6 form-horizontal">
                                                <label class=" form-control-label">Department</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDepartment"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-sm-6 form-horizontal">
                                                <label class=" form-control-label">Designation</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDesignation"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-6 form-horizontal">
                                                <label class=" form-control-label">Case Details</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseDet"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group" id="divpass">
                                            <div class="col-sm-6 form-horizontal">
                                                <label class=" form-control-label">Email Id</label>

                                                <div class="row" style="margin: 0px;">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
                                                    <%--<asp:TextBox ID="txtPass" CssClass="form-control row_pass" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="row_span" aria-label="Must contain at least one
                        number and one uppercase and lowercase letter, and at least 8 or more characters">
                                <i class="fa fa-info-circle recycle_style"></i></span>--%>
                                                </div>
                                            </div>

                                            <div class="col-sm-6 form-horizontal">
                                                <label class=" form-control-label">Mobile No</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtMobNo"></asp:TextBox>
                                                <%--<asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtConPass"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-sm-12 form-horizontal">
                                                <label class=" form-control-label">Notes</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNotes"
                                                    TextMode="MultiLine"></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlInst" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlInst_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>--%>
                                            </div>

                                            <%--                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Department</label>
                        <asp:DropDownList ID="ddlDep" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlDep_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>--%>
                                        </div>
                                        <div class="form-group m-t-30">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-dark btn-block"
                                                OnClientClick="return val();"
                                                OnClick="btnAdd_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
