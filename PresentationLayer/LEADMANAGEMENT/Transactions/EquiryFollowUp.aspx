<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EquiryFollowUp.aspx.cs" Inherits="LEADMANAGEMENT_Transactions_EquiryFollowUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontroltoolkit" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../JAVASCRIPTS/ScrollableTablePlugin_1.0_min.js"></script>

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            width: 40%;
            border: 3px solid #0DA9D0;
        }

            .modalPopup .header
            {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body
            {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
            }

            .modalPopup .footer
            {
                padding: 3px;
            }

            .modalPopup .button
            {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

            .modalPopup td
            {
                text-align: left;
            }
    </style>

    <script type="text/javascript">
        function ScrollRepeater() {
            var GridId = "<%=pnlEquiryList.ClientID %>";
            var grid = document.getElementById(GridId);
            var gridHeight = grid.offsetHeight;
            if (gridHeight > 300) {
                $('#Table1').Scrollable({
                    ScrollHeight: 300
                });
            }
        }
    </script>

    <script type="text/javascript">
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
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

      <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="upLeadEnquiryFollowUp" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div  id="divProgressbar" runat="server" style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
     <asp:UpdatePanel ID="upLeadEnquiryFollowUp" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header with-border">
                    <span class="glyphicon glyphicon-detail text-blue"></span>
                    <h3 class="box-title"><b>ENQUIRY FOLLOW UP</b></h3>
                    <asp:Label ID="Label2" runat="server"><span  style="padding-left: 650px; color: Red; font-weight: bold;">Note : * marked fields are Mandatory</span></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <form role="form">
                                    <div id="divMsg" runat="server">
                                    </div>
                                    <div class="box-body">
                                        <fieldset>
                                            <legend>Enquiry Follow Up
                                            </legend>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-4">
                                                                <label>Admission Batch</label>
                                                            </div>
                                                            <div class="form-group col-md-8">
                                                                <asp:DropDownList ID="ddlAdmissionBatch" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Admission Batch" runat="server" OnSelectedIndexChanged="ddlAdmissionBatch_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="box-footer" id="divButton" runat="server">
                                                <p class="text-center">
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                        CssClass="btn btn-success" OnClick="btnSave_Click" TabIndex="2" />
                                                    <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click"
                                                        TabIndex="3" Text="Excel Report" ToolTip="Excel Report" CssClass="btn btn-info" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </p>
                                            </div>

                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlEquiryList" runat="server">
                                                    <div class="table table-responsive">
                                                        <asp:ListView ID="lvEquiryList" runat="server" OnItemDataBound="lvEquiryList_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <h4>Equiry Follow Up List</h4>
                                                                <table id="Table1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Select</th>
                                                                            <th>Batch</th>
                                                                            <th>Equiry Name</th>
                                                                            <th>End Date & Time</th>
                                                                            <th>Equiry Status</th>
                                                                            <th>Interested Degree</th>
                                                                            <th>Interested Course</th>
                                                                            <th>Remark</th>
                                                                            <th>Enquiry Detail</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox ID="chkEnquiryStatus" runat="server" Enabled='<%# (Eval("ENQUIRYSTATUS").ToString() == "3" ? false : true) %>' />
                                                                        <asp:HiddenField ID="hdfEnquiryNo" Value='<%#Eval("ENQUIRYNO") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdfDegreeNo" Value='<%#Eval("DEGREENO") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdfBranchNo" Value='<%#Eval("BRANCHNO") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdfEnquiryStatuNo" Value='<%#Eval("ENQUIRYSTATUS") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdfLevelNo" Value='<%#Eval("LEVELNO") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdfLead_UA_No" Value='<%#Eval("LEAD_UA_NO") %>' runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BATCHNAME")%>  
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STUDENTNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("END_DATE")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlEnquiryStatus" runat="server" AppendDataBoundItems="True" Enabled="true" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Enabled="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" Enabled="true" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRemark" runat="server" Text='<%# Eval("REMARKS")%>' Rows="1" Columns="5" CssClass="form-control" Height="33px" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="imgShow" CommandArgument='<%#Eval("ENQUIRYNO")%>' AlternateText="View" CssClass="btn btn-success" runat="server" OnClick="ShowModal" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </asp:Panel>
                                                </table>
                                            </div>

                                            <%-- Enquiry Done Status List --%>

                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlEnquiryDoneStatus" runat="server">
                                                    <div class="table table-responsive">
                                                        <asp:ListView ID="lstEnquiryDoneStatus" runat="server">
                                                            <LayoutTemplate>
                                                                <h4>Equiry Follow Up Done List</h4>
                                                                <table id="Table1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Sr. No.</th>
                                                                            <th>Batch</th>
                                                                            <th>Equiry Name</th>
                                                                            <th>Equiry Status</th>
                                                                             <th>Enquiry Done Date & Time</th>
                                                                            <th>Interested Degree</th>
                                                                            <th>Interested Course</th>
                                                                            <th>Remark</th>
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
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BATCHNAME")%>  
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STUDENTNAME")%>
                                                                    </td>
                                                                    <td style="color:green">
                                                                        <%#Eval("ENQUIRYSTATUSNAME") %>
                                                                    </td>
                                                                     <td>
                                                                        <%# Eval("DONE_DATE")%>
                                                                    </td>
                                                                     <td>
                                                                        <%# Eval("INTERESTEDDEGREE")%>
                                                                    </td>
                                                                     <td>
                                                                        <%# Eval("INTERESTEDBRANCH")%>
                                                                    </td>
                                                                    <td>
                                                                       <%# Eval("REMARKS")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </asp:Panel>
                                                </table>
                                            </div>

                                            <%-- Enquiry Done Status List --%>


                                            <%-- Model Pop up Panel --%>

                                            <asp:ImageButton ID="lnkFake" runat="server" Style="display: none" />
                                            <ajaxcontroltoolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                            </ajaxcontroltoolkit:ModalPopupExtender>
                                            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                <div class="header">
                                                    Enquiry Details
                                                </div>
                                                <div class="body">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid #ccc">
                                                        <tr>
                                                            <td style="width: 125px; padding-left: 5px">
                                                                <b>Enquiry Id </b>
                                                            </td>
                                                            <td>
                                                                <b>: </b>
                                                                <asp:Label Style="padding-left: 5px" ID="lblId" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 5px">
                                                                <b>Enquiry Name</b>
                                                            </td>
                                                            <td>
                                                                <b>: </b>
                                                                <asp:Label Style="padding-left: 5px" ID="lblName" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 5px">
                                                                <b>Mobile No. </b>
                                                            </td>
                                                            <td>
                                                                <b>: </b>
                                                                <asp:Label Style="padding-left: 5px" ID="lblMobileno" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 5px">
                                                                <b>Email ID</b>
                                                            </td>
                                                            <td>
                                                                <b>: </b>
                                                                <asp:Label Style="padding-left: 5px" ID="lblEmailID" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 5px">
                                                                <b>Parent Mobile No. </b>
                                                            </td>
                                                            <td>
                                                                <b>: </b>
                                                                <asp:Label Style="padding-left: 5px" ID="lblParentMobileNo" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 5px">
                                                                <b>Parent Email ID </b>
                                                            </td>
                                                            <td>
                                                                <b>: </b>
                                                                <asp:Label Style="padding-left: 5px" ID="lblParentEmailID" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="footer" align="right">
                                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" />
                                                    <%--CssClass="button"--%>
                                                </div>
                                            </asp:Panel>

                                            <%-- Model Pop up Panel --%>

                                        </fieldset>
                                    </div>
                                </form>
                              
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

  


</asp:Content>

