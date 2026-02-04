<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="statuschange.aspx.cs" Inherits="statuschange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header">
            <strong>Search</strong> Report                 
        </div>
        <div class="card-body card-block">
            <div class="col-sm-12 form-horizontal">
                <div class="row form-group">
                    <div class="col col-md-6">
                        <label class=" form-control-label">Status</label>
                        <asp:DropDownList runat="server" ID="ddl_status" CssClass="form-control">
                            <asp:ListItem Text="Change Status" Value="-1" />
                            <asp:ListItem Text="Assigned" Value="Assigned" />
                            <asp:ListItem Text="Inprogress" Value="Inprogress" />
                            <asp:ListItem Text="Mismatch Found" Value="Mismatch Found" />
                            <asp:ListItem Text="Report Preparation" Value="Report Preparation" />
                            <asp:ListItem Text="Pending for Director/HOD Signature" Value="Pending for Director/HOD Signature" />
                            <asp:ListItem Text="Report Submission" Value="Report Submission" />
                        </asp:DropDownList>

                    </div>

                    <%--   <div class="col col-md-6">
                        <label class=" form-control-label">To DateName</label>
                     
                        <asp:TextBox ID="txt_todate" TextMode="Date" runat="server" CssClass="form-control" onchange="myChangeFunction();">
                        </asp:TextBox>
                    </div>--%>
                </div>
            </div>

            <div class="col-sm-12 form-horizontal m-t-30">
                <div class="row form-group">
                    <%-- <div class="col col-md-6">
                        <asp:RadioButton ID="rdo_caseno" runat="server" GroupName="rdo" OnCheckedChanged="rdo_caseno_CheckedChanged" AutoPostBack="true"/>
                        <label class="form-control-label">DFS CaseNo</label>
                    </div>
                    <div class="col col-md-6">
                        <asp:TextBox runat="server" ID="txt_caseno" CssClass="form-control m-b-10" Visible="false"></asp:TextBox>

                    </div>--%>

                    <%-- <div class="col col-md-6">
                        <asp:RadioButton ID="rdo_agencyname" runat="server" GroupName="rdo" OnCheckedChanged="rdo_agencyname_CheckedChanged" AutoPostBack="true"/>
                        <label class="form-control-label">AgencyName/Police Station</label>
                         </div>
                       <div class="col col-md-6">
                        <asp:TextBox runat="server" ID="txt_agencyname" CssClass="form-control m-b-10" Visible="false"></asp:TextBox>
                    </div>--%>

                    <%--   <div class="col col-md-6">
                        <asp:RadioButton ID="rdo_referenceno" runat="server" GroupName="rdo" OnCheckedChanged="rdo_referenceno_CheckedChanged" AutoPostBack="true"/>
                        <label class="form-control-label">Reference No</label>
                           </div>
                          <div class="col col-md-6">
                        <asp:TextBox runat="server" ID="txt_refernceno" CssClass="form-control m-b-10" Visible="false"></asp:TextBox>
                    </div>--%>
                </div>
            </div>

            <div class="col-sm-3 form-horizontal" style="margin: auto;">
                <div class="row form-group">
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                    <asp:Button ID="btn_search" runat="server" OnClick="btn_search_Click" Text="Search"
                        CssClass="btn btn-dark btn-block" />
                </div>
            </div>
            <div class="main-content" runat="server" id="div_rpt" style="padding-top: 35px;">
                <div class="section__content section__content--p30">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <h3 id="title" runat="server" style="text-align: center;"></h3>
                                <div class="table-responsive table--no-card m-b-30">
                                    <table class="table table-borderless table-striped table-earning">
                                        <thead id="Header" visible="false" runat="server">
                                            <tr>
                                                <th>No</th>
                                                <th class="allCheckbox"><asp:CheckBox runat="server" ID="allchk"></asp:CheckBox> Case No</th>
                                                <th>Agency No</th>
                                                <th>Reference No</th>
                                                <th>Current Status</th>


                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rpt_details">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                        <td class="singleCheckbox">
                                                            <asp:CheckBox runat="server" ID="chk_caseno"></asp:CheckBox> <asp:Label runat="server" ID="lbl_caseno" Text='<%#Eval("caseno") %>'></asp:Label></td>
                                                        <td><%#Eval("agencyname") %></td>
                                                        <td class="text-right"><%#Eval("agencyreferanceno") %></td>
                                                         <td class="text-right"><%#Eval("status") %></td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>


                                        </tbody>
                                    </table>

                                </div>
                                <div class="row">
                                    <div class="col-6">
                                <asp:DropDownList runat="server" ID="ddl_bulkstatus" Visible="false" CssClass="form-control">
                                    <asp:ListItem Text="Change Status" Value="-1" />
                                    <asp:ListItem Text="Inprogress" Value="Inprogress" />
                                    <asp:ListItem Text="Mismatch Found" Value="Mismatch Found" />
                                    <asp:ListItem Text="Report Preparation" Value="Report Preparation" />
                                    <asp:ListItem Text="Pending for Director/HOD Signature" Value="Pending for Director/HOD Signature" />
                                    <asp:ListItem Text="Report Submission" Value="Report Submission" />
                                </asp:DropDownList>
                                        </div>
                                  
                                <asp:Button runat="server" ID="btn_submit" Visible="false" CssClass="btn btn-success" Text="Submit" OnClick="btn_submit_Click" />
                            </div>
                                  </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script>
        $(function () {
            var $allCheckbox = $('.allCheckbox :checkbox');
            var $checkboxes = $('.singleCheckbox :checkbox');
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeAttr('checked');
                }
                else {
                    $allCheckbox.attr('checked', 'checked');
                }
            });
        });
    </script>

</asp:Content>

