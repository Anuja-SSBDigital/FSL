<%@ Page Title="Case Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddCaseDetails.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/w3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="card">
        <div class="card-header">
            <strong>Active Case</strong> Details
        </div>
        <div class="card-body card-block">
            <div class="col-12">
                <div class="form-header" id="divsearch" runat="server">
                    <asp:HiddenField ID="HdnDivision" runat="server" />
                    <%--<asp:TextBox ID="txtCaseNo"  runat="server"
                        CssClass="au-input au-input--xl" placeholder="Search Case By Id"></asp:TextBox>--%>
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
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_no"></asp:TextBox>
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
                    <asp:HiddenField ID="hdnCaseId" runat="server" Value="" />
                    <%-- <button class="au-btn--submit" type="button">
                        <i class="zmdi zmdi-search"></i>
                    </button>--%>

                    <%--<div class="col-6">
                        <asp:Button ID="btnURL" runat="server" Text="Generate URL"
                                    CssClass="btn btn-dark btn-block" OnClick="btnURL_Click" />
                    </div>--%>
                </div>
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-file m-t-20" OnClick="btnSearch_Click" Style="color: white;">
                            <%--<i class="zmdi zmdi-search"></i>--%>
                    Search
                </asp:LinkButton>
                <%--  <div class="form-header row form-group m-t-25" id="DivStatus" runat="server" visible="false">
                    <div class="col-10">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Change Status" Value="-1" />
                            <asp:ListItem Text="Inprogress" Value="Inprogress" />
                            <asp:ListItem Text="Mismatch Found" Value="Mismatch Found" />
                            <asp:ListItem Text="Report Preparation" Value="Report Preparation" />
                            <asp:ListItem Text="Pending for Director/HOD Signature" Value="Pending for Director/HOD Signature" />
                            <asp:ListItem Text="Report Submission" Value="Report Submission" />
                        </asp:DropDownList>
                    </div>
                 
                </div>--%>

                <%-- <div class="form-header row form-group" visible="false" runat="server" id="DivRemarks">
                    <div class="col-10">
                        <label class=" form-control-label">Remarks</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                       <div class="col-2 m-t-50">
                        <asp:Button ID="btnStatus_Change" runat="server" Text="Change Status"
                            CssClass="btn btn-dark" OnClick="btnStatus_Change_Click" type="submit" />
                    </div>
                </div>--%>
                <script type="text/javascript">
              <%--      $(function () {
                        $("[id$=txtCaseNo]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/AddCaseDetails.aspx/BindAutoCompleteList") %>',
                                    //data: "{ 'prefix': '" + request.term.replace(/\\/g, "\\\\") + "'}",
                                    data: "{ 'prefix': '" + request.term + "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('_')[0],
                                                val: item.split('_')[1],
                                            }
                                        }))
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            },
                            select: function (e, i) {
                                $("[id$=hdnCaseId]").val(i.item.val);
                            },
                            minLength: 3
                        });
                    });--%>
                </script>
                <asp:Label ID="lblMsg" runat="server" Text=""
                    CssClass="col-12"></asp:Label>
            </div>

            <div class="col-12 m-t-20" id="divcd" runat="server" visible="false">
                <div class="alert alert-primary" role="alert" id="timeline" runat="server">
                </div>
                <%-- <div class="col-12 row m-b-20">
                    External URL : 
                    <asp:HyperLink ID="lblURL" runat="server" CssClass="text-info m-l-10"
                        Target="_blank">
                        </asp:HyperLink>
                </div>--%>
                <%--<div class="table-responsive table--no-card">
                    <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdView_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="case_id" HeaderText="Case Id" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="20%"></asp:BoundField>

                            <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="year" HeaderText="Year" ItemStyle-Width="15%"></asp:BoundField>
                            <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("userid") %>' />
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                        id="btnEdit" runat="server">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                        id="btnDelete" runat="server">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </ItemTemplate>
                            ////</asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>--%>
                <div class="col-12" style="padding: 0px;" id="divTab" runat="server" visible="false">
                    <div class="m-t-20 card">
                        <div class="card-header">
                            <strong>Attachment</strong> Details
       
                        </div>
                        <div class="card-body">
                            <div class="table-responsive table--no-card">
                                <div runat="server" id="div_grd"></div>
                            </div>

                            <%--<asp:GridView ID="grdAttach" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-borderless table-striped table-earning"
                                    EmptyDataText="No Records Found.">
                                    <Columns>

                                        <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>--%>

                            <%-- <asp:BoundField DataField="hd_name" HeaderText="Hard Drive" ItemStyle-Width="20%"></asp:BoundField>--%>

                            <%-- <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                                        <asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="15%"></asp:BoundField>--%>
                            <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("userid") %>' />
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                        id="btnEdit" runat="server">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                        id="btnDelete" runat="server">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--  </Columns>
                                </asp:GridView>--%>

                            <div class="w3-row m-t-15" style="border-bottom: 6px solid #f1f1f1!important;">
                                <a id="defaultOpen" onclick="openCity(event, 'tab1');"
                                    style="cursor: pointer; margin-right: -4px;">
                                    <div class="tablink w3-hover-light-grey w3-padding text">
                                        <p>Exhibits</p>
                                    </div>
                                </a>
                                <a onclick="openCity(event, 'tab2');"
                                    style="cursor: pointer; margin-right: -4px;">
                                    <div class="tablink w3-hover-light-grey w3-padding text">
                                        <p>Annexures</p>
                                    </div>
                                </a>
                                <a onclick="openCity(event, 'tab3');"
                                    style="cursor: pointer;">
                                    <div class="tablink w3-hover-light-grey w3-padding text">
                                        <p>Reports</p>
                                    </div>
                                </a>
                            </div>


                            <div id="tab1" class="tab mt-4" style="display: none">
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">Type</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <asp:TextBox ID="txtExhType" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">Notes</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <asp:TextBox ID="txtNoteExh" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">File</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <div class="file-loading">
                                            <%--<input type="file" id="" class=" form-group file" />--%>
                                            <asp:FileUpload ID="fuExb" runat="server"
                                                CssClass="form-group file" />
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="m-t-40">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add"
                                        CssClass="btn btn-dark btn-block" OnClick="btnAdd_Click" />
                                </div>--%>
                            </div>

                            <div id="tab2" class="tab mt-4" style="display: none">
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">Type</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <asp:TextBox ID="txtAnnType" runat="server" CssClass="form-control"
                                            ReadOnly="true" Text="Annexures"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">Notes</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <asp:TextBox ID="txtNoteAnn" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">File</label>
                                    </div>
                                    <div class="col-12 col-md-9 ">
                                        <div class="file-loading">
                                            <%--<input type="file" id="fuAnn" class="file form-group" />--%>
                                            <asp:FileUpload ID="fuAnn" runat="server"
                                                CssClass="form-group file" />
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="row form-group">
                                    <asp:Button ID="btnAddAnn" runat="server" Text="Add"
                                        CssClass="btn btn-dark btn-block" OnClick="btnAddAnn_Click" />
                                </div>--%>
                            </div>

                            <div id="tab3" class="tab mt-4" style="display: none">
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">Type</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <asp:TextBox ID="txtRepType" runat="server" CssClass="form-control"
                                            ReadOnly="true" Text="Reports"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">Notes</label>
                                    </div>
                                    <div class="col-12 col-md-9">
                                        <asp:TextBox ID="txtNoteRep" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group  col-12">
                                    <div class="col col-md-3">
                                        <label for="hf-email" class=" form-control-label">File</label>
                                    </div>
                                    <div class="col-12 col-md-9 ">
                                        <div class="file-loading">
                                            <%--<input type="file" id="fuRep" class="form-group file" />--%>
                                            <asp:FileUpload ID="fuRep" runat="server"
                                                CssClass="form-group file" />
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="row form-group">
                                    <asp:Button ID="btnAddRep" runat="server" Text="Add"
                                        CssClass="btn btn-dark btn-block" OnClick="btnAddRep_Click" />
                                </div>--%>
                            </div>
                            <div class="m-t-40 col-4 m-l-250">
                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                    CssClass="btn btn-dark btn-block" OnClick="btnAdd_Click" />
                            </div>
                            <script>
                                $("#fuCerti").fileinput({
                                    theme: 'fa',
                                    uploadUrl: '#',
                                    overwriteInitial: false,
                                    maxFileSize: 2000,
                                    maxFilesNum: 10,
                                    slugCallback: function (filename) {
                                        return filename.replace('(', '_').replace(']', '_');
                                    }
                                });
                            </script>
                        </div>
                    </div>
                </div>


                <div class="table-responsive table--no-card m-t-20">
                    <asp:GridView ID="grdFile" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdView_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>

                            <%--<asp:BoundField DataField="date" HeaderText="Date" ItemStyle-Width="20%"></asp:BoundField>--%>
                            <%--<asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="20%"></asp:BoundField>--%>

                            <%--<asp:BoundField DataField="hash" HeaderText="Hash" ItemStyle-Width="15%"></asp:BoundField>--%>
                            <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="upload" HeaderText="Upload" ItemStyle-Width="15%"></asp:BoundField>
                            <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("userid") %>' />
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-dark " title="Edit"
                                        id="btnEdit" runat="server">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-icons btn-rounded btn-outline-danger " title="Edit"
                                        id="btnDelete" runat="server">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="m-t-40">
                    <asp:Button ID="btnInsert" Visible="false" runat="server" Text="Submit"
                        CssClass="btn btn-success btn-block" OnClick="btnInsert_Click" />
                </div>
            </div>
        </div>
    </div>

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

        function openCity(evt, tabName) {
            var i, x, tablinks;
            x = document.getElementsByClassName("tab");
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablink");
            for (i = 0; i < x.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" w3-border-red", "");
            }
            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.firstElementChild.className += " w3-border-red";
        }
        document.getElementById("defaultOpen").click();
    </script>
    <script>
        function CaseDetails(userid, caseno, notes) {
        }
    </script>

</asp:Content>

