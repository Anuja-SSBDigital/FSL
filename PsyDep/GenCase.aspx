<%@ Page Title="" Language="C#" MasterPageFile="~/PsyDep/MasterPage.master" AutoEventWireup="true" CodeFile="GenCase.aspx.cs" Inherits="PsyDep_GenCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .main-section {
            margin: 0 auto;
            padding: 20px;
            /*margin-top: 100px;*/
            background-color: #fff;
            box-shadow: 0px 0px 20px #c1c1c1;
        }

        .fileinput-remove,
        .fileinput-upload {
            display: none;
        }

        .file-preview-pdf {
            width: 188px !important;
        }

        .file-zoom-detail {
            width: 100% !important;
        }

        .btn-file {
            background: #4272d7;
            color: #fff;
        }

            .btn-file:hover {
                background: #3868cd;
            }
    </style>
    <link href="../css/fileinput.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Case</strong> Details
                                   
        </div>
        <div class="card-body card-block">
            <div class="col-sm-6 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <%--<div class="col-6 row">
                            <asp:Label ID="Label1" runat="server" Text="DFS/EE/2020/CF/"></asp:Label>
                        </div>
                        <div class="col-6 row">--%>
                        <asp:TextBox ID="txtCaseNo" runat="server" CssClass="form-control"
                            ReadOnly="true"></asp:TextBox>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Case ID</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <%--<div class="col-6 row">
                            <asp:Label ID="Label1" runat="server" Text="DFS/EE/2020/CF/"></asp:Label>
                        </div>
                        <div class="col-6 row">--%>
                        <asp:Label ID="txtCaseId" runat="server" Text=""
                            CssClass="form-control-label"></asp:Label>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">FIR No</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox ID="txtFIR" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-md-3">
                        <label for="hf-email" class=" form-control-label">Notes</label>
                    </div>
                    <div class="col-12 col-md-9">
                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <asp:Button ID="btnGen" runat="server" Text="Generate"
                        CssClass="btn btn-dark btn-block" OnClick="btnGen_Click"
                        OnClientClick="return val();" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function val() {
            var txtFIR = document.getElementById("<%= txtFIR.ClientID %>");

            if (txtFIR.value == "") {
                txtDDI.classList.add('is-invalid');
                return false;
            } else {
                txtDDI.classList.remove('is-invalid');
            }

            return true;
        }
    </script>
</asp:Content>

