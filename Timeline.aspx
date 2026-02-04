<%@ Page Title="Timeline" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Timeline.aspx.cs" Inherits="Timeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="css/timeline.css" rel="stylesheet" />
    <script src="js/timeline.js"></script>

    <div class="row">
        <div class="col-md-12">
            <div class="overview-wrap">
                <h3 id="timeline_title" runat="server"></h3>
            </div>
        </div>
    </div>

    <section id="timeline-timeline" class="timeline-container">
        <div class="timeline-timeline-block">
            <div runat="server" id="div_timeline">
            </div>
            <link href="css/timeline.css" rel="stylesheet" />
            <div id="cd-timeline" class="cd-container">
                <div runat="server" id="genDIV"></div>
            </div>
        </div>
    </section>
</asp:Content>

