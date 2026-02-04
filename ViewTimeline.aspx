<%@ Page Title="View Timeline" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewTimeline.aspx.cs" Inherits="ViewTimeline" %>

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
    <div class="card">
        <div class="card-header">
            <strong>Department wise Timeline</strong> Details
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row col-6 form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Department</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
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
                <%--    <div class="row col-6 form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case No</label>
                    </div>
                    <div class="col-12 col-md-9">
                         <asp:DropDownList ID="ddlcaseno" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>--%>
                <div class="row col-6 form-group">
                    <div class="col-4">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"
                            OnClientClick="return val();"
                            CssClass="btn btn-dark btn-block" OnClick="btnSearch_Click" />
                    </div>

                </div>
                <h3 id="title" runat="server" style="text-align: center;"></h3>
            </div>
        </div>
    </div>
    <section id="timeline-timeline" class="timeline-container">
        <div class="timeline-timeline-block">
            <div runat="server" id="div_timeline">
            </div>
            <div runat="server" visible="false" id="Timeline">
                <div id="cd-timeline" class="cd-container">
                    <div runat="server" id="genDIV"></div>
                </div>
            </div>
        </div>
    </section>
    <script>
        function val() {
            <%--var txtDDI = document.getElementById("<%= ddlcaseno.ClientID %>");

            if (txtDDI.value == "") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
            }

            return true;--%>
        }
    </script>
</asp:Content>

