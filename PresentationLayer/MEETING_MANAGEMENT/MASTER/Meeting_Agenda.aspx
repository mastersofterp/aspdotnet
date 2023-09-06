<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Meeting_Agenda.aspx.cs" Inherits="MEETING_MGT_Transaction_Meeting_Agenda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 6000px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">PLAN & SCHEDULE MEETINGS</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Add/Edit Plan & Schedule</div>
                                            <div class="panel panel-body">
                                                <div class="col-md-12">
                                                     <%-- Modified by Saahil Trivedi 15/1/2022--%>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000">*</span>Committee :</label>
                                                        <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                            CssClass="form-control" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                            ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None" Text="*"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-md-4" runat="server" visible="false">
                                                        <label><span style="color: #FF0000"></span>Previous Meeting :</label>
                                                        <asp:DropDownList ID="ddlpremeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Previous Meeting" OnSelectedIndexChanged="ddlpremeeting_SelectedIndexChanged" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>


                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Meeting Code :</label>
                                                        <asp:TextBox ID="txtcode" runat="server" Enabled="false" CssClass="form-control" ToolTip="Enter Meeting Code" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                   

                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000">*</span>Meeting Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/date.jpg" Height="17px" Width="17px" Style="cursor: pointer" CausesValidation="False" />
                                                            </div>
                                                            <asp:TextBox ID="txtdate" runat="server" TabIndex="4" ValidationGroup="ScheduleDtl" CssClass="form-control" ToolTip="Enter Meeting Date"></asp:TextBox>

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtdate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtdate" ClearMaskOnLostFocus="true">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="medt"
                                                                ControlToValidate="txtdate" Display="None" EmptyValueMessage="Please Enter Meeting  Date"
                                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                                IsValidEmpty="false" Text="*" ValidationGroup="Submit">
                                                            </ajaxToolKit:MaskedEditValidator>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000">*</span>From Time :</label>
                                                        <asp:TextBox ID="txttime" runat="server" CssClass="form-control" ToolTip="Enter Time" meta:resourcekey="txttimeResource1"
                                                            TabIndex="5"></asp:TextBox>
                                                        <%-- <ajaxToolKit:MaskedEditExtender ID="meinTime" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        OnInvalidCssClass="errordate" Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txttime" AcceptAMPM="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevinTime" runat="server" ControlExtender="meinTime"
                                                        ControlToValidate="txttime" Display="None" EmptyValueMessage="Please Enter Time."
                                                        ErrorMessage="Please Select Time" InvalidValueBlurredMessage="*" InvalidValueMessage="Time is invalid"
                                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Schedule"></ajaxToolKit:MaskedEditValidator>
                                                  
                                                    <asp:RequiredFieldValidator ID="rfvtime" runat="server" ControlToValidate="txttime"
                                                        Display="None" ErrorMessage="Please Enter From Meeting Time" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meinTime" runat="server" TargetControlID="txttime"
                                                            Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" AutoComplete="false" AutoCompleteValue="12:00:00"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" UserTimeFormat="None" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevinTime" runat="server" ControlExtender="meinTime"
                                                            ControlToValidate="txttime" IsValidEmpty="False" EmptyValueMessage="Please Enter From Time"
                                                            InvalidValueMessage="From Time is invalid" Display="None" TooltipMessage="Input a time"
                                                            EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="Submit" ErrorMessage="mevinTime" />
                                                        <asp:Label ID="lblTipp" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM" meta:resourcekey="lblTippResource1">
                                                        </asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvtime" runat="server" ControlToValidate="txttime"
                                                            Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Submit"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>



                                                        <%--   <ajaxToolKit:MaskedEditExtender ID="meetime" runat="server" TargetControlID="txtTravelTime"
                                                                    Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" MessageValidatorTip="true"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevTime" runat="server" ControlExtender="meetime" ControlToValidate="txtTravelTime"
                                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Time"
                                                                    InvalidValueMessage="Time is Invalid" SetFocusOnError="true"
                                                                    TooltipMessage="Please Enter Time" ValidationGroup="Travel">                                                    
                                                  
                                                                </ajaxToolKit:MaskedEditValidator>--%>
                                                    </div>

                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000">*</span>To Time :</label>
                                                        <asp:TextBox ID="txtToTime" runat="server" CssClass="form-control" ToolTip="Enter Time" meta:resourcekey="txttimeResource2"
                                                            TabIndex="6"></asp:TextBox>
                                                        <ajaxToolKit:MaskedEditExtender ID="meinTime1" runat="server" CultureAMPMPlaceholder="" AutoComplete="false" AutoCompleteValue="12:00:00"
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            OnInvalidCssClass="errordate" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtToTime" AcceptAMPM="True">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevinTime1" runat="server" ControlExtender="meinTime"
                                                            ControlToValidate="txtToTime" Display="None" EmptyValueMessage="Please Enter To Time."
                                                            ErrorMessage="Please Select To Time" InvalidValueBlurredMessage="*" InvalidValueMessage="To Time is invalid"
                                                            IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                        <asp:Label ID="lblTipp1" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM" meta:resourcekey="lblTippResource1"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvtotime" runat="server" ControlToValidate="txtToTime"
                                                            Display="None" ErrorMessage="Please Enter To Meeting Time" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>


                                                   <%-- Modified by Saahil Trivedi 15/1/2022--%>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000">*</span>Venue :</label>


                                                        <asp:DropDownList ID="ddlVenue" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                            CssClass="form-control" ToolTip="Select Venue" TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ErrorMessage="Please Select Venue"
                                                            ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlVenue" Display="None" Text="*"></asp:RequiredFieldValidator>

                                                        <%-- <asp:TextBox ID="txtvenue" runat="server" onkeypress="return CheckAlphaNumeric(event,this);"
                                                       CssClass="form-control" ToolTip="Enter Venue" TabIndex="8" MaxLength="300"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvvenue" runat="server" ControlToValidate="txtvenue"
                                                        Display="None" ErrorMessage="Please Enter Meeting Venue" ValidationGroup="Submit"  SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Last Date of Approval :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/date.jpg" Height="17px" Width="17px" Style="cursor: pointer" CausesValidation="False" />
                                                            </div>
                                                            <asp:TextBox ID="txtLastDate" runat="server" TabIndex="8" CssClass="form-control" ToolTip="Enter Last Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtLastDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                OnInvalidCssClass="errordate" TargetControlID="txtLastDate" ClearMaskOnLostFocus="true">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="medt"
                                                                ControlToValidate="txtLastDate" Display="None" EmptyValueMessage="Please Enter Last Date of Agenda Approval Submission"
                                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                                IsValidEmpty="false" Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000">*</span>Meeting Title :</label>
                                                        <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" ToolTip="Enter Meeting Title" TabIndex="9" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                                            Display="None" ErrorMessage="Please Enter Meeting Title" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div id="trAddress2" runat="server" visible="false" class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Address Line 2 :</label>
                                                        <asp:TextBox ID="txtAddLine" TextMode="SingleLine" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Enter Address" TabIndex="10"
                                                            onkeypress="return CheckAlphaNumeric(event,this);"></asp:TextBox>
                                                    </div>


                                                    <div id="trCity" runat="server" visible="false" class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>City :</label>
                                                        <asp:DropDownList ID="ddlCity" runat="server" TabIndex="11" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select City">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                                            InitialValue="0" Display="None" ErrorMessage="Please Select City" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div id="trState" runat="server" visible="false" class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>State/Province/Region :</label>
                                                        <asp:DropDownList ID="ddlState" runat="server" TabIndex="12" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select State">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                                            InitialValue="0" Display="None" ErrorMessage="Please Select State" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div id="trPostal" runat="server" visible="false" class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Zip/Postal Code :</label>
                                                        <asp:TextBox ID="txtZipCode" runat="server" MaxLength="6" TabIndex="13" CssClass="form-control" ToolTip="Enter Zip/Postal Code" onkeypress="return CheckNumeric(event,this);" />
                                                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode"
                                                            Display="None" ErrorMessage="Please Enter Zip/Postal Code." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div id="trCountry" runat="server" visible="false" class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Country :</label>
                                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" TabIndex="14" CssClass="form-control" ToolTip="Select Country"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                            InitialValue="0" Display="None" ErrorMessage="Please Select Country" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>



                                            </div>
                                        </div>
                                    </asp:Panel>



                                    <asp:Panel ID="pnlAgendaDetails" runat="server">
                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Agenda Details</div>--%>
                                            <div class="panel panel-heading">Upload Files</div>
                                            <div class="panel panel-body">
                                                <div class="col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Attach File :</label>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" ValidationGroup="submit" ToolTip="Select file to upload" TabIndex="10" />

                                                    </div>

                                                    <div class="form-group col-md-4">
                                                        <br />
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="11" CssClass="btn btn-primary" ToolTip="Click here to Add" />
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlFile" runat="server" ScrollBars="Auto" Visible="false">
                                                        <asp:ListView ID="lvfile" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">Download files
                                                                    </h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Action
                                                                                </th>
                                                                                <th>File Name
                                                                                </th>
                                                                                <th>Download
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
                                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                            OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                                    </td>
                                                                    <td>
                                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif"
                                                                            AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                                            OnClick="imgdownload_Click" />
                                                                    </td>



                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                                <div class="form-group col-md-4" id="divMNo" runat="server" visible="false">
                                                    <label><span style="color: #FF0000"></span>Meeting Number :</label>
                                                    <asp:TextBox ID="txtnumber" runat="server" Enabled="false" CssClass="form-control" ToolTip="Meeting Number" TabIndex="12"></asp:TextBox>
                                                </div>



                                            </div>





                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="13" CausesValidation="true" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="14" CausesValidation="false" />
                            <asp:Button ID="btnSendMail" runat="server" Text="Send Notifications" CssClass="btn btn-primary" ToolTip="Send Mail With Agenda Details" OnClick="btnSendMail_Click" TabIndex="15" CausesValidation="false" />
                            <asp:Button ID="btnReport" runat="server" Text="Agenda Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnReport_Click" TabIndex="16" CausesValidation="false" />
                            <asp:Button ID="btnCancelMeeting" runat="server" Text="Cancel Meeting" CssClass="btn btn-primary" ToolTip="Send Cancel Meeting Notification" OnClick="btnCancelMeeting_Click" TabIndex="17" CausesValidation="false" Visible="false" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />
                            <div class="col-md-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAgenda" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">MEETING TITLE LIST </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Edit </th>
                                                            <%--  <th>Agenda Number </th>
                                                            <th>Agenda Title </th>--%>
                                                            <th>Meeting Number </th>
                                                            <th>Meeting Title </th>
                                                            <th>Venue </th>
                                                            <th>Date </th>
                                                            <th>Time </th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("PK_AGENDA") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("MEETING_CODE")%></td>
                                                <td><%# Eval("AGENDATITAL")%></td>
                                                <td><%# Eval("VENUE")%></td>
                                                <td><%# Eval("MEETINGDATE","{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("MEETINGTIME")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
            </div>
            </div> 
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnSendMail" />
            <asp:PostBackTrigger ControlID="lvfile" />
        </Triggers>
    </asp:UpdatePanel>

    <div style="width: 90%; padding: 10px">
    </div>

    <script type="text/javascript" language="javascript">
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            } //../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }
    </script>

</asp:Content>
