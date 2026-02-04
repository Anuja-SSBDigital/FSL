<%@ Page Title="Validate Certificates - Blockchain | SSB Digital" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="validatecert.aspx.cs" Inherits="validatecert" %>

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
    <link href="css/fileinput.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="overview-wrap">
                <h2 class="title-1 m-b-30">Verify File</h2>
                <asp:Button ID="btn_pdf" runat="server" Visible="false" Text="Generate PDF" CssClass="btn btn-primary" Onclick="btn_pdf_Click" />
            </div>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:HiddenField runat="server" ID="hdn_caseno" />
            <asp:HiddenField runat="server" ID="hdn_hash" />
            <div class="form-group files color">
                <asp:FileUpload ID="fuCerti" runat="server" />
            </div>
        </div>
    </div>
    <div class="user-data__footer">
        <asp:Button ID="btnVerify" runat="server" CssClass="au-btn au-btn-icon au-btn--blue" Text="Verify"
            OnClick="btnVerify_Click" />
    </div>
</asp:Content>

