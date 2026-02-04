<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserAcceptance.aspx.cs" Inherits="UserAcceptance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header">
            <strong>
                <asp:Label ID="lblTitle" runat="server">Evidence Acceptance Form</asp:Label>

            </strong>

        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <asp:HiddenField ID="hdnUserID" runat="server" />
                    <asp:HiddenField ID="HdnDivision" runat="server" />
                    <asp:HiddenField ID="Status" runat="server" Value="Assigned" />
                    <div class="col-12 col-md-6" runat="server" id="div_dept" visible="false">
                        <label class=" form-control-label">Department</label>
                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                    </div>
                    <%--<div class="col-sm-12 form-horizontal">
                        <label class=" form-control-label">Case No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_dfsee" Text="DFS/EE"></asp:TextBox>
                        <label class=" form-control-label">Year</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_year"></asp:TextBox>
                        <label class=" form-control-label" runat="server" id="lbl_div" visible="true">DIV</label>
                        <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_div"></asp:TextBox>
                        <label class=" form-control-label">No.</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_no"></asp:TextBox>
                    </div>--%>
                </div>
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
                        <asp:TextBox runat="server" CssClass="form-control" Visible="true" ID="txt_fpnumber" Onchange="FunctionRangeFP();"></asp:TextBox>
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
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">PDF Upload (Evidence Receipt)</label>
                        <asp:FileUpload ID="txtPDF" Style="width: 100%;" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">No of Exhibits</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNoOfExhibits" TextMode="Number" Onchange="FunctionChecknegative();"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Referance No</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtReferanceNo"></asp:TextBox>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <label class=" form-control-label">Police Station/Agency Name</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPoliceStation"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-12 form-horizontal">
                        <label class=" form-control-label">Notes</label>
                        <asp:TextBox ID="txtNotes" TextMode="multiline" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group m-t-30" id="BtnUpdate" runat="server" visible="false">
                    <asp:Button ID="btnUpdatedata" runat="server" Text="Update" CssClass="btn btn-dark btn-block" OnClientClick="return AcceptanceVal();"
                        OnClick="btnUpdate_Click" />
                </div>
                <div class="form-group m-t-30" id="BtnSave" runat="server">
                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-dark btn-block" OnClientClick="return AcceptanceVal();"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>


    <script>
        function FunctionChecknegative() {
            var NoofExhibits = document.getElementById("<%= txtNoOfExhibits.ClientID %>");
            if (NoofExhibits.value < "1") {
                alert("The number should be 1 or greater than 1.");
                NoofExhibits.value = "";
            }
        }
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
        function FunctionRange() {
            
            var Number = document.getElementById("<%= txt_no.ClientID %>");
          
            var FinalNumber = "";
            if (Number.value.length == "1") {
                FinalNumber = "000" + Number.value;
            } else if (Number.value.length == "2") {
                FinalNumber = "00" + Number.value;
            } else if (Number.value.length == "3") {
                FinalNumber = "0" + Number.value;
            } else {
                FinalNumber = Number.value;
            }

            Number.value = FinalNumber;
        }



        function FunctionRangeFP() {

            
             var FPNumber = document.getElementById("<%= txt_fpnumber.ClientID %>");
             var FinalNumber = "";
            if (FPNumber.value.length == "1") {
                FinalNumber = "000" + FPNumber.value;
            } else if (FPNumber.value.length == "2") {
                FinalNumber = "00" + FPNumber.value;
            } else if (FPNumber.value.length == "3") {
                FinalNumber = "0" + FPNumber.value;
             } else {
                FinalNumber = FPNumber.value;
             }

            FPNumber.value = FinalNumber;
         }

        function AcceptanceVal() {

            var year = document.getElementById("<%= txt_year.ClientID %>");
            var div = document.getElementById("<%= txt_div.ClientID %>");
            var no = document.getElementById("<%= txt_no.ClientID %>");

            var PDF = document.getElementById("<%= txtPDF.ClientID %>");
            var ReferanceNo = document.getElementById("<%= txtReferanceNo.ClientID %>");
            var PoliceStation = document.getElementById("<%= txtPoliceStation.ClientID %>");

            //    if (CaseNo.value == "") {
            //        CaseNo.classList.add('is-invalid');
            //    return false;
            //} else {
            //        CaseNo.classList.remove('is-invalid');
            //    }

            if (year.value == "") {
                year.classList.add('is-invalid');
                return false;
            } else {
                year.classList.remove('is-invalid');

            }



            if (no.value == "") {
                no.classList.add('is-invalid');
                return false;
            } else {
                no.classList.remove('is-invalid');
            }

            //    if (PDF.value == "") {
            //        PDF.classList.add('is-invalid');
            //    return false;
            //} else {
            //        PDF.classList.remove('is-invalid');
            //}

            if (ReferanceNo.value == "") {
                ReferanceNo.classList.add('is-invalid');
                return false;
            } else {
                ReferanceNo.classList.remove('is-invalid');
            }

            if (PoliceStation.value == "") {
                PoliceStation.classList.add('is-invalid');
                return false;
            } else {
                PoliceStation.classList.remove('is-invalid');
            }


            if (div.value == "") {
                div.classList.add('is-invalid');
                return false;
            } else {
                div.classList.remove('is-invalid');
            }

            return true;
        }
    </script>
</asp:Content>

