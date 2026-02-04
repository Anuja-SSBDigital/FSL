<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewDetails.aspx.cs" Inherits="ViewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-primary" role="alert" id="timeline" runat="server" visible="false">
    </div>
    <div class="card">
        <div class="card-header" runat="server" id="caseno_title">
            <strong>Case Details</strong>
            <button class="btn btn-primary" type="button" style="display: none; float: right" onclick="GetPDF();"><span>Generate PDF</span></button>
            <asp:Button ID="bttnzip" runat="server" class="btn btn-primary" Visible="false" Style="float: right" Text="Generate Report" Font-Bold="True" OnClick="bttnzip_Click" />

            <h3>
                <asp:Label ID="lblNoData" CssClass="m-l-190 m-b-30" runat="server" Visible="false" Text="No Record found..">
                          <%--<div class='alert alert-danger' role='alert'>*No Records Found.</div>--%>
                </asp:Label>
            </h3>
        </div>
        <div class="card-body card-block" id='tbl_details' runat="server" visible="false">
            <div class="col-sm-12 form-horizontal">
                <div class="row">

                    <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 col-12">
                        <p class="m-t-10">
                            <b>CaseNo:</b><span class="ml-2"><asp:Label runat="server" ID="lbl_caseno"></asp:Label>
                            </span>
                        </p>

                        <p class="m-t-10"><b>Case AssignBy:</b><span class="ml-2"><asp:Label runat="server" ID="lbl_assignby"></asp:Label></span></p>

                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 col-12">
                        <p class="m-t-10">
                            <b>Agencyname:</b><span class="ml-2"><asp:Label runat="server" ID="lbl_agencyname"></asp:Label>
                            </span>
                        </p>
                        <p class="mb-0 m-t-10"><b>Case AssignTo:</b><span class="ml-2"><asp:Label runat="server" ID="lbl_assignto"></asp:Label></span></p>

                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 col-12">
                        <p class="m-t-10"><b>Reference No:</b><span class="ml-2"><asp:Label runat="server" ID="lbl_referenceno"></asp:Label></span></p>


                    </div>
                </div>

            </div>
            <input type="hidden" id="hdnReport" value="" />
            <asp:HiddenField runat="server" ID="hdn_requestid" />

            <div id="div_Pdf" class="col-12">
            </div>
            <div style="text-align: center" class="m-t-35" id="title" runat="server" visible="false">

                <h4><u>Report Details</u></h4>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release">
            </asp:ScriptManager>
            <div class="col-lg-12">

                <div class="table-responsive table--no-card">
                    <%--<h3 id="lblNoData" runat="server" style="text-align:center;"></h3>--%>


                    <div runat="server" id="div_grd" visible="false"></div>
                    <%--<asp:GridView ID="grdAttach" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-borderless table-striped table-earning"
                        EmptyDataText="No Records Found." OnRowDataBound="grdAttach_RowDataBound">
                        <Columns>--%>

                    <%--<asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="20%"></asp:BoundField>--%>
                    <%-- <asp:TemplateField HeaderText="File Name" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnFile" runat="server" Value='<%# Eval("path") %>' />
                                    <asp:HiddenField ID="hdnHD_name" runat="server" Value='<%# Eval("hd_name") %>' />
                                    <asp:Label ID="lblfn" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>


                    <%--<asp:BoundField DataField="hd_name" HeaderText="Hard Drive" ItemStyle-Width="20%"></asp:BoundField>--%>

                    <%--<asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="createddate" HeaderText="Created Date" ItemStyle-Width="10%"></asp:BoundField>--%>

                    <%--<asp:BoundField DataField="hash" HeaderText="Hash" ItemStyle-Width="30%"></asp:BoundField>--%>
                    <%--<asp:BoundField DataField="notes" HeaderText="Notes" ItemStyle-Width="40%"></asp:BoundField>--%>
                    <%--<asp:BoundField DataField="path" HeaderText="Path" ItemStyle-Width="15%"></asp:BoundField>--%>
                    <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="17%">
                                <ItemTemplate>
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
                    <%-- </Columns>
                    </asp:GridView>--%>
                    <asp:LinkButton runat="server" Visible="false" ID="btn_request" Style="float: right" class="btn btn-info btn-lg m-t-10" data-toggle="modal" data-target="#modalrequest">Request for Generating Report</asp:LinkButton>
                    <%--<asp:Button style="float:right" ID="btn_request" Visible="false" runat="server" class="btn btn-primary btn-lg m-t-10" Text="Request for Generating Report" data-toggle="modal" data-target="#modalrequest"/>--%>
                    <%--<button type="button" runat="server" id="btn_request1" style="float:right;" Visible="true" class="btn btn-info btn-lg m-t-10" data-toggle="modal" data-target="#modalrequest">Request for Generating Zip</button>--%>
                </div>


                <div id="div_extrafiles" runat="server" class="col-12">
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalrequest" role="dialog" style="margin-left: 25%; width: 50%;">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <h4 class="modal-title">Send Request</h4>
                </div>
                <div class="modal-body">
                    <div class="col col-md-12">
                        <p>File Upload</p>
                        <asp:FileUpload runat="server" ID="fl_request" CssClass="form-control" />
                    </div>
                    <div class="col col-md-12 m-t-10">
                        <p>Remarks</p>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_remarks" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="req_txt_remarks" ControlToValidate="txt_remarks" ErrorMessage="Please Enter Remarks" ValidationGroup="rem" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn btn-primary" ValidationGroup="rem" />
                    <button type="button" class="btn btn-default btn-dark" data-dismiss="modal">Close</button>

                </div>
            </div>
        </div>
    </div>



    <script>
        $(document).ready(function () {
            document.getElementById("DivFooter").style.display = 'none';
        });


        function GetPDF1() {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();
            document.getElementById("hdnReport").value = "Report";

            today = dd + '/' + mm + '/' + yyyy;
            const opt = {
                jsPDF: { format: 'letter', orientation: 'landscape' }
            };
            var div = "<div class='card'>" +
                "<div class='card-header pt-4 border-0 text-center'>" +
                "<img src='images/logo.png' alt='Logo'/>" +
                "<h4>e-Sanrakhshan</h4>" +
                "</div>" +
                "<div class='row mt-4'>" +
                "<div class='col-sm-12'>" +
                "<div class='font-18 font-bold mb-2 p-3 text-center'" +
                "style='font-color:#337ab7;border-top: 1px solid;font-size: 30px;border-bottom: 1px solid;color:#337ab7;background:lightblue;'>" +
                "Case Details</div>" +
                "</div>" +
                "</div>";
            var data = div + document.getElementById("report_details").innerHTML;

            var html = data + "<div class='bg-white border-0 card-footer mb-4'>"
                + "<p class='float-right'><b>Date: </b>" + today + "</p>"
                + "</div>";


            const element = html;
            html2pdf().set(opt)
                .from(element)
                .save();



        }








    </script>

</asp:Content>

