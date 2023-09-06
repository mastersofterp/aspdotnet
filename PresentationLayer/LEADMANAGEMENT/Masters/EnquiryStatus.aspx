<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EnquiryStatus.aspx.cs" Inherits="LEADMANAGEMENT_Masters_EnquiryStatus" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>--%>
    <%-- <script src="../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updBatch" runat="server" >
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ENQUIRY STATUS</h3>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Enquiry Status Name</label>
                                        </div>
                                        <asp:TextBox ID="txtEnquiryStatus" runat="server" TabIndex="1" MaxLength="50"
                                            ToolTip="Please Enter Enquiry Status" />
                                        <asp:RequiredFieldValidator ID="rfvEnquirystatus" runat="server" ControlToValidate="txtEnquiryStatus"
                                            Display="None" ErrorMessage="Please Enter Enquiry Status Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Active Status</label>
                                        </div>
                                        <asp:CheckBox ID="chknlstatus" runat="server" TextAlign="Left" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked:Status -Active </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked:Status -DeActive </span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="3" />
                               <%-- <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click"
                                    TabIndex="5" Text="Report" ToolTip="Show Report" CssClass="btn btn-info" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="4" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-md-12">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:ListView ID="lvEnquiryStatus" runat="server">
                                                            <LayoutTemplate>
                                                                <%--<div class="sub-heading">
                                                                    <h5>Lead Allotment List</h5>
                                                                </div>--%>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divAllotment">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Enquiry Name
                                                                        </th>
                                                                        <th>Enquiry Status
                                                                        </th>
                                                                        
                                                                    </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                    <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ENQUIRYSTATUSNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ENQUIRYSTATUSNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="divstatus" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

