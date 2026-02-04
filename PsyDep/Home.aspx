<%@ Page Title="" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="overview-wrap">
                <h2 class="title-1">overview</h2>
            </div>
        </div>
    </div>
    <div class="row m-t-25">
        <div class="col-sm-6 col-lg-6">
            <div class="overview-item overview-item--c1">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas  fa-file-alt"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>No. of Cases</span>
                            </p>
                            <asp:Label ID="lblCases" runat="server" Text=""></asp:Label>
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
                            <i class="fas fa-hdd"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>No. of Pending Cases</span>
                            </p>
                            <asp:Label ID="lblHD" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="col-sm-6 col-lg-3">
            <div class="overview-item overview-item--c3">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-copy"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>No. of Open Cases</span>
                            </p>
                            <asp:Label ID="lblOpenCases" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <%--<div class="col-sm-6 col-lg-4">
            <div class="overview-item overview-item--c4">
                <div class="overview__inner">
                    <div class="overview-box clearfix row" style="margin: 0px">
                        <div class="icon">
                            <i class="fas fa-users"></i>
                        </div>
                        <div class="text">
                            <p>
                                <span>No of Users</span>
                            </p>
                            <asp:Label ID="lblUsers" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
</asp:Content>

