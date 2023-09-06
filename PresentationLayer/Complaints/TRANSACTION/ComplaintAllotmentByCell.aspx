<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComplaintAllotmentByCell.aspx.cs"
    Inherits="Complaints_TRANSACTION_ComplaintAllotmentByCell" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <script src="../../plugins/datatables/dataTables.bootstrap.min.js"></script>
    <script src="../../plugins/datatables/jquery.dataTables.min.js"></script>--%>

    <%--<script type="text/javascript">
         //On Page Load
         $(document).ready(function () {
             $('#table2').DataTable();
         });

    </script>--%>
    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" id="tblMain" runat="server">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SERVICE REQUEST ALLOTMENT BY DEPARTMENT ADMIN </h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <asp:Panel ID="pnlCom" runat="server" Visible="false">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Service</label>
                                                </div>
                                                <asp:TextBox ID="txtComplainer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Service No</label>
                                                </div>
                                                <asp:TextBox ID="txtComplaintNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Detail</label>
                                                </div>
                                                <asp:TextBox ID="txtComplaint" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvComplaint" runat="server"
                                                    ControlToValidate="txtComplaint" Display="None" ErrorMessage="Please Select Complaint"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Service Allotment Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <%-- <div class="input-group-addon">
                                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>--%>
                                                    <asp:TextBox ID="txtComplaintDate" runat="server" CssClass="form-control" disabled="true"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtComplaintDate"  Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender> <%-- PopupButtonID="imgCal"--%>
                                                </div>
                                                <asp:Label ID="lblsample" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Location</label>
                                                </div>
                                                <asp:TextBox ID="txtLoc" runat="server" CssClass="form-control" disabled="true"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtLoc" runat="server"
                                                    ControlToValidate="txtLoc" Display="None" ErrorMessage="Please Select Area Location"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Address</label>
                                                </div>
                                                <asp:TextBox ID="txtComplaintAdd" runat="server" CssClass="form-control" disabled="true"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvtxtComplaintAdd" runat="server"
                                                    ControlToValidate="txtComplaintAdd" Display="None" ErrorMessage="Please Select Complaint Address"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRMDept" AppendDataBoundItems="false" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    OnSelectedIndexChanged="ddlRMDept_SelectedIndexChanged" AutoPostBack="false" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="ddlRMDept" Display="None" ErrorMessage="Please Select Department"
                                                    ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Category Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCompNature" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    Enabled="false">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvCompNature" runat="server" ControlToValidate="ddlCompNature" Display="None" ErrorMessage="Please Select Complaint Nature."
                                                    ValidationGroup="complaint" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                            </div>



                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Assigned To</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRMCompTo" AppendDataBoundItems="true" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="ddlRMCompTo" Display="None" ErrorMessage="Please Select Assign to"
                                                    ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtDetails" runat="server" TabIndex="2" CssClass="form-control" TextMode="MultiLine" MaxLength="350" />
                                                <asp:RequiredFieldValidator ID="rfvRemark" runat="server"
                                                    ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Remark"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Preferable Date for Visit/Contact</label>
                                                </div>
                                                <div class="input-group date">
                                                    <%--<div class="input-group-addon">
                                                        <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>--%>
                                                    <div class="input-group-addon">
                                                        <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtComplaintDt" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtComplaintDt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meReqdate" runat="server" TargetControlID="txtComplaintDt"
                                                        Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <%-- <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meReqdate"
                                                        ControlToValidate="txtComplaintDt" EmptyValueMessage="Please Enter Preferable date"
                                                        InvalidValueMessage="Preferable date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="complaint"
                                                        SetFocusOnError="True" ErrorMessage="mevIndDate" />--%>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Preferable Time From</label>
                                                </div>
                                                <asp:TextBox ID="txtPerferTime" runat="server" MaxLength="8" CssClass="form-control" Text="10:00 AM" TabIndex="4" />
                                                <%--  ajaxToolKit:FilteredTextBoxExtender ID="ftbeCompDetail" runat="server" TargetControlID="txtPerferTime" 
                                                        FilterType="Custom,Numbers,UppercaseLetters, LowercaseLetters" ValidChars=":, "></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                <%-- <span style="font-style: italic; font-size: smaller"></span>--%>
                                                <ajaxToolKit:MaskedEditExtender ID="meeEnterTime" runat="server" TargetControlID="txtPerferTime"
                                                    Mask="99:99" MaskType="Time" AcceptAMPM="True"
                                                    ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <%--<ajaxToolKit:MaskedEditValidator ID="mevEnterTime" runat="server" ControlExtender="meeEnterTime"
                                                    ControlToValidate="txtPerferTime" IsValidEmpty="False" EmptyValueMessage="Please Enter Preferable Time From."
                                                    InvalidValueMessage="Preferable From Time is invalid" Display="None" TooltipMessage="Input a time"
                                                    EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" 
                                                    ValidationGroup="complaint" ErrorMessage="mevEnterTime" /> --%>
                                                <asp:RequiredFieldValidator ID="rfvTimeFrom" runat="server"
                                                    ControlToValidate="txtPerferTime" Display="None" ErrorMessage="Please Enter Preferable Time From & To"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>And To</label>
                                                </div>
                                                <asp:TextBox ID="txtPerferTo" runat="server" MaxLength="8" CssClass="form-control" Text="06:00 PM" TabIndex="5" />
                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPerferTo" 
                                                    FilterType="Custom,Numbers,UppercaseLetters, LowercaseLetters" ValidChars=":, "></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                <ajaxToolKit:MaskedEditExtender ID="meeEnterTimeTo" runat="server" TargetControlID="txtPerferTo"
                                                    Mask="99:99" MaskType="Time" AcceptAMPM="True"
                                                    ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <%--<ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="meeEnterTimeTo"
                                                    ControlToValidate="txtPerferTo" IsValidEmpty="False" EmptyValueMessage="Please Enter Preferable Time To."
                                                    InvalidValueMessage="Preferable To Time is invalid" Display="None" TooltipMessage="Input a time"
                                                    EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="complaint" ErrorMessage="mevEnterTimeTo" />--%>
                                                <asp:RequiredFieldValidator ID="rfvTimeTo" runat="server"
                                                    ControlToValidate="txtPerferTo" Display="None" ErrorMessage="Please Enter Preferable Time From & To"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Priority Of Work</label>
                                                </div>

                                                <asp:DropDownList ID="ddlPriorityWork" AppendDataBoundItems="false" runat="server" TabIndex="6" CssClass="form-control" Enabled="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Priority 1</asp:ListItem>
                                                    <asp:ListItem Value="2">Priority 2</asp:ListItem>
                                                    <asp:ListItem Value="3">Priority 3</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Complaint Status</label>
                                                </div>
                                                <asp:RadioButton ID="rdocomp1" runat="server" GroupName="rdocomp" Text="Incomplete" Checked="true" TabIndex="7" />
                                                <asp:RadioButton ID="rdocomp2" runat="server" GroupName="rdocomp" Text="Completed" TabIndex="7" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divReply" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Requester Reply</label>
                                                </div>
                                                <asp:TextBox ID="txtReply" runat="server" TabIndex="8" CssClass="form-control" ReadOnly="true" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSuggestion" visible="false">
                                                <div class="label-dynamic">
                                                  <%--  <sup>* </sup>--%>
                                                    <label>Reallotment Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtReallotment" runat="server" TabIndex="9" CssClass="form-control" TextMode="MultiLine" MaxLength="300" />
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Remark"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="complaint" OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="7" />
                                        <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClick="btnDecline_Click" CssClass="btn btn-primary" TabIndex="8" CausesValidation="false" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="9" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </asp:Panel>

                                <div class="col-12">
                                    <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                        <asp:ListView ID="lvfile" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Download files</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>File Name
                                                                </th>
                                                                <th>Service No
                                                                </th>
                                                                <th>Download
                                                                </th>
                                                                <th>Creation Date 
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <%#GetFileNameCaseNo(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgFileDownload" runat="Server" ImageUrl="~/Images/action_down.png"
                                                                    AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                                    OnClick="imgFileDownload_Click" CausesValidation="false" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="imgFileDownload" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <%#GetFileDate(DataBinder.Eval(Container, "DataItem"))%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" id="divButtons" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-12 col-12">
                                            <asp:RadioButtonList ID="rdbStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatColumns="3" OnSelectedIndexChanged="rdbStatus_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">All &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">Pending &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Allotted &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3">In-Process &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="4">Declined &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="5">Completed &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="6">Reopened &nbsp;&nbsp;</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvComplaint" runat="server">
                                        <EmptyDataTemplate>
                                            <div class="text-center">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Service Request For Allotment"></asp:Label>
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Service Request Allotment</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SELECT</th>
                                                            <th>SERVICE REQUEST NO.
                                                                SERVICE REQUEST DATE
                                                            </th>
                                                            <%-- <th></th>  --%>
                                                            <th>REQUESTER</th>
                                                            <th>AREA & LOCATION</th>
                                                            <%--<th>LOCATION</th>--%>
                                                            <th>STATUS</th>
                                                            <th>SERVICE REQUEST CATEGORY</th>
                                                            <th>ALLOTTED TO</th>
                                                            <%--<th>DECLINE REMARK</th>--%>
                                                            <%--<th>REOPEN</th>--%>
                                                            <th>Print</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/uparrow.jpg"
                                                        CommandArgument='<%# Eval("COMPLAINTID") %>'
                                                        Enabled='<%#Eval("COMPLAINTSTATUS").ToString() == "IN PROCESS" || Eval("COMPLAINTSTATUS").ToString() == "COMPLETE" || Eval("COMPLAINTSTATUS").ToString() == "DECLINED" ? false : true %>'
                                                        AlternateText='<%# Eval("CELLALLOTMENTID") %>' ToolTip='<%# Eval("COMPLAINTNO") %>' OnClick="btnEdit_Click" />
                                                </td>
                                                <td><%# Eval("COMPLAINTNO")%>
                                                    <%# Eval("COMPLAINTDATE", "{0:d}")%>
                                                </td>
                                                <td><%# Eval("COMPLAINTEE_NAME")%></td>
                                                <td><%# Eval("AREANAME")%>
                                                    -> <%# Eval("COMPLAINTEE_ADDRESS")%></td>
                                                <td><%# Eval("COMPLAINTSTATUS")%> <%# Eval("COMPLAINTENDDATE", "{0:dd/MM/yyyy}")%></td>
                                                <td><%# Eval("TYPENAME")%></td>
                                                <td><%# Eval("ALLOT_TO_NAME")%></td>
                                                <%--<td><%# Eval("DECLINE_REMARK")%></td>--%>
                                               <%--<td><%# Eval("REOPEN")%></td>--%>
                                                <td>
                                                    <asp:Button ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print"
                                                        Text="Print" CommandArgument='<%# Eval("COMPLAINTID") %>' OnClick="btnPrint_Click" CssClass="btn btn-primary" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
