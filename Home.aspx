<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .overview-box .text {
            font-weight: 600 !important;
            display: inline-block;
        }

            .overview-box .text span {
                font-size: 14px;
                font-weight: 500;
                color: rgba(255, 255, 255, 0.7);
            }

        .icon1 {
            margin-right: 15px;
            margin-top: 8px;
        }

        .itag {
            font-size: 40px;
            color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="overview-wrap">
                <h2 class="title-1">Dashboard</h2>
            </div>
        </div>
    </div>
    <div class="row m-t-25" id="DivHOD" runat="server" visible="false">
        <%-- <div class="col-sm-6 col-lg-3">
            <div class="overview-item overview-item--b1">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas  fa-file-alt"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span id="lblTotalCase" runat="server">Total Cases</span>
                            </p>
                            <asp:Label ID="lblCases" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="ccol-sm-6 col-lg-3">
            <div class="overview-item overview-item--b2">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-spinner"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Pending Cases</span>
                            </p>
                            <asp:Label ID="lblPendingCases" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-lg-3">
            <div class="overview-item overview-item--b3">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-copy"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Case in Preparation</span>
                            </p>
                            <asp:Label ID="lblPrepareCases" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <%--  <div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--b4">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-check-square"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Completed Cases</span>
                            </p>
                            <asp:Label ID="lblComplete" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--b1">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px;">
                        <div class="icon1">
                            <i class="fas fa-check-square itag"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span style="color: #fff; font-weight: bold;">Total Completed Cases</span>
                            </p>
                            <asp:Label ID="lblComplete" runat="server" Text="" Style="color: #fff; font-weight: bold; font-size: 22px;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="ccol-sm-6 col-lg-6">
            <div class="overview-item overview-item--b2">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px;">
                        <div class="icon1">
                            <i class="fas fa-check-square itag"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span style="color: #fff; font-weight: bold;">Completed Cases By Me</span>
                            </p>
                            <asp:Label ID="lblComplete_Officer" runat="server" Text="" Style="color: #fff; font-weight: bold;font-size: 22px;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- <div class="row" id="DivUser" runat="server" visible="false">
        <div class="col-md-12">
            <div class="overview-wrap">
                <h4>HOD Dashboard</h4>
            </div>
        </div>
    </div>--%>
    <div class="row m-t-25" id="DivOfficer" runat="server" visible="false">
        <%--<div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--c1">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas  fa-file-alt"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span id="lblTotalCase_Officer" runat="server">Total Cases</span>
                            </p>
                            <asp:Label ID="lblCases_Officer" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--c2">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-spinner"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Pending Cases</span>
                            </p>
                            <asp:Label ID="lblPendingCases_Officer" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--c3">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-copy"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Cases in Preparation</span>
                            </p>
                            <asp:Label ID="lblPrepareCases_Officer" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      <div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--c4">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-check-square"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Completed Cases</span>
                            </p>
                            <asp:Label ID="lblComplete_Officer" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="ccol-sm-12 col-lg-12">
            <div class="overview-item overview-item--b1">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-check-square itag" style="font-size: 40px;"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span  style="color: #fff; font-weight: bold;">Completed Cases By Me</span>
                            </p>
                            <asp:Label ID="lblComplete_Officer1" runat="server" Text="" style="color: #fff; font-weight: bold;font-size: 22px;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--//Director Dashboard Start//--%>
    <div class="row m-t-25" runat="server" visible="false" id="DivTab">
        <div class="col-sm-4 col-lg-4">
            <div class="overview-item overview-item--c1">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-building"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Total Departments</span>
                            </p>
                            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-4 col-lg-4">
            <div class="overview-item overview-item--c2">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-users"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>Total Users</span>
                            </p>
                            <asp:Label ID="lblTotalUsers" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-4 col-lg-4">
            <a href="ViewTimeline.aspx" target="_blank">
                <div class="overview-item overview-item--c4">
                    <div class="overview__inner">
                        <div class="overview-box clearfix row" style="margin: 0px">
                            <div class="icon">
                                <i class="fas fa-file-text"></i>
                            </div>
                            <div class="text">
                                <p>
                                    <span>Click here to view all department</span>
                                </p>
                                <asp:Label ID="Label1" runat="server" Text="wise timeline"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </a>
        </div>
    </div>


    <div runat="server" visible="false" id="DivDirector">
        <%--<h4>Details available from Jan-2022</h4>--%>
        <div class="au-card au-card--no-shadow au-card--no-pad m-b-40">
            <div class="au-card-title" style="background-image: url('images/bg-title-01.jpg');">
                <div class="bg-overlay bg-overlay--blue"></div>
                <h3 id="DFSDirector_Title" runat="server">
                    <i class="zmdi zmdi-account-calendar"></i></h3>
            </div>
            <div class="col-sm-12 form-horizontal m-t-10">
                <div class="row form-group">
                    <div class="col col-md-4 m-l-20">
                        <label class=" form-control-label">From Date</label>
                        <asp:TextBox ID="txt_FromDate" TextMode="Date" runat="server" CssClass="form-control">
                        </asp:TextBox>
                    </div>

                    <div class="col col-md-4">
                        <label class=" form-control-label">To Date</label>
                        <%--<asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control" >--%>
                        <asp:TextBox ID="txt_ToDate" TextMode="Date" runat="server" CssClass="form-control" onchange="myChangeFunction();">
                        </asp:TextBox>
                    </div>
                    <div class="form-horizontal">
                        <div class="m-t-35">
                            <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btn btn-dark btn-block"
                                OnClientClick="return FunctionVal();" OnClick="btn_search_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="au-task js-list-load">
                <div class="m-b-30">
                    <div class="table-responsive table-data">
                        <label id="title" runat="server" class="m-l-235 m-t-20"
                            style="text-align: center; border-bottom: 1px solid #b3afaf; font-weight: bold; color: black;">
                        </label>
                        <table class="table">
                            <thead>
                                <tr>
                                    <%--<td>#</td>--%>
                                    <td style="font-size: 16px;">Departments</td>
                                    <%--  <td><span class="role completed">Total Cases</span></td>
                                <td><span class="role pending">Case Pending</span></td>
                                <td><span class="role preparation">Report Preparation</span></td>
                                <td><span class="role signature">Pending for Sign</span></td>
                                <td><span class="role assigned">Report Submitted</span></td>--%>
                                    <%--   <td style="text-align: center;"><span class="role completed">Total Cases</span></td>
                                    <td style="text-align: center;"><span class="role pending">Pending Cases</span></td>--%>
                                    <td style="text-align: center;"><span class="role assigned" style="font-size: 16px;">Completed Cases</span></td>
                                </tr>
                            </thead>
                            <tbody>
                                <div id="DivTable" runat="server"></div>
                                <tr style="background-color: #f0f0f0">
                                    <td>Total Data</td>
                                    <%--  <td style="text-align: center !important;"><span class="totalcases" id="lblAvgTotalCases" runat="server"></span></td>
                                    <td style="text-align: center !important;"><span class="totalpending" id="lblAvgPendingCases" runat="server"></span></td>--%>
                                    <td style="text-align: center !important;"><span class="totalcompleted" id="lblAvgCompletedCases" runat="server"></span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="au-task__footer">
                    <button class="au-btn au-btn-load js-load-btn" style="display: none;">Loading...</button>
                </div>
            </div>
        </div>
    </div>
    <%--Director Dashboard End--%>




    <%--<div class="col-xl-12">
        <!-- PAGE CONTENT-->
        <div class="page-content">
            <div class="row">
                <div class="col-lg-12">
                     <div class="col-lg-12 grid-margin stretch-card">
                         <input type="hidden" id="Hdn_Jan" name="Hdn_Jan" value=""/>
                         <input type="hidden" id="Hdn_Feb" name="Hdn_Feb" value=""/>
                         <input type="hidden" id="Hdn_Mar" name="Hdn_Mar" value=""/>
                         <input type="hidden" id="Hdn_Apr" name="Hdn_Apr" value=""/>
                         <input type="hidden" id="Hdn_May" name="Hdn_May" value=""/>
                         <input type="hidden" id="Hdn_Jun" name="Hdn_Jun" value=""/>
                         <input type="hidden" id="Hdn_Jul" name="Hdn_Jul" value=""/>
                         <input type="hidden" id="Hdn_Aug" name="Hdn_Aug" value=""/>
                         <input type="hidden" id="Hdn_Sep" name="Hdn_Sep" value=""/>
                         <input type="hidden" id="Hdn_Oct" name="Hdn_Oct" value=""/>
                         <input type="hidden" id="Hdn_Nov" name="Hdn_Nov" value=""/>
                         <input type="hidden" id="Hdn_Dec" name="Hdn_Dec" value=""/>--%>
    <%--<asp:HiddenField ID="Hdn_Jan" runat="server" value=""/>--%>
    <%-- <asp:HiddenField ID="Hdn_Feb" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Mar" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Apr" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_May" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Jun" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Jul" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Aug" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Sep" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Oct" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Nov" runat="server" value=""/>
                         <asp:HiddenField ID="Hdn_Dec" runat="server" value=""/>--%>

    <%-- <div class="card">
                <div class="card-body">
                  <h4 class="card-title"> <i class="fas fa fa-chart-line"></i> Completed Cases for the Year <b id="txtYear" runat="server"></b></h4>
                  <div id="morris-line-example"></div>
                </div>
              </div>
            </div>
                </div>
            </div>
        </div>
    </div>--%>

    <script>
        function FunctionVal() {

            var FromDate = document.getElementById("<%= txt_FromDate.ClientID %>");
            var ToDate = document.getElementById("<%= txt_ToDate.ClientID %>");

            if (FromDate.value == "") {
                FromDate.classList.add('is-invalid');
                return false;
            } else {
                FromDate.classList.remove('is-invalid');
            }

            if (ToDate.value == "") {
                ToDate.classList.add('is-invalid');
                return false;
            } else {
                ToDate.classList.remove('is-invalid');
            }
            return true;
        }
    </script>
</asp:Content>

