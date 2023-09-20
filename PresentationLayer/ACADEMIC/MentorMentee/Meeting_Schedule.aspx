<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Meeting_Schedule.aspx.cs" Inherits="ACADEMIC_MentorMentee_Meeting_Schedule" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <div class="modal fade" id="preview" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Document</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <!-- Modal body -->
                        <div class="modal-body text-center">
                            <asp:Literal ID="ltEmbed" runat="server" />
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal" id="btnclose">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("Back Dates Are Not Allowed");
                sender._selectedDate = new Date();
                sender._textbox.set_Value("");
            }
        }
        //Shaikh Juned
        function Check_Time() {

            var M_Date = document.getElementById('<%=txtdate.ClientID%>').value;

            var f_time = document.getElementById('<%=txttime.ClientID%>').value;
            var to_time = document.getElementById('<%=txtToTime.ClientID%>').value;
            var hours = Number(to_time.match(/^(\d+)/)[1]);
            var minutes = Number(to_time.match(/:(\d+)/)[1]);
            var AMPM = to_time.match(/\s(.*)$/)[1];
            if (AMPM == "PM" && hours < 12) hours = hours + 12;
            if (AMPM == "AM" && hours == 12) hours = hours - 12;
            var sHours = hours.toString();
            var sMinutes = minutes.toString();
            if (hours < 10) sHours = "0" + sHours;
            if (minutes < 10) sMinutes = "0" + sMinutes;
            var TO_Time = sHours + "." + sMinutes;

            var time = document.getElementById('<%=txttime.ClientID%>').value;
            var hours1 = Number(time.match(/^(\d+)/)[1]);
            var minutes1 = Number(time.match(/:(\d+)/)[1]);
            var AMPM1 = time.match(/\s(.*)$/)[1];
            if (AMPM1 == "PM" && hours1 < 12) hours1 = hours1 + 12;
            if (AMPM1 == "AM" && hours1 == 12) hours1 = hours1 - 12;
            var sHours1 = hours1.toString();
            var sMinutes1 = minutes1.toString();
            if (hours1 < 10) sHours1 = "0" + sHours1;
            if (minutes1 < 10) sMinutes1 = "0" + sMinutes1;
            var From_Time = sHours1 + "." + sMinutes1;

            //if (From_Time == From_Time)
            //{
            //    alert('bbbbbbb');
            //}


            if (TO_Time < From_Time) {
                alert('Meeting From Time Should not be Greater Than To Time ..');
                document.getElementById('<%=txttime.ClientID%>').value = '';
                document.getElementById('<%=txtToTime.ClientID%>').value = '';
            }
            if (To_Time == From_Time) {
                alert('Meeting From Time and To Time Should not be Same..');
                document.getElementById('<%=txttime.ClientID%>').value = '';
                 document.getElementById('<%=txtToTime.ClientID%>').value = '';

             }


         }


    </script>
    <%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PLAN & SCHEDULE MEETINGS</h3>
                        </div>
                        <%--<div class="col-12 mt-lg-4">--%>
                        <div class="note-div">
                            <h5 class="heading">Note</h5>
                            <i class="fa fa-star" aria-hidden="true"></i>
                            <span>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="On Selection of Committee, ListView is Visible." ForeColor="Red"></asp:Label></span>
                        </div>
                        <%--</div>--%>
                        <div class="box-body">
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>

                                                <label>Committee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None" Text="*"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Code</label>
                                            </div>
                                            <asp:TextBox ID="txtcode" runat="server" Enabled="false" CssClass="form-control" ToolTip="Enter Meeting Code" TabIndex="3"></asp:TextBox>

                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtdate" runat="server" TabIndex="4" CssClass="form-control" ToolTip="Enter Meeting Date" AutoPostBack="true"></asp:TextBox>
                                                <%--ValidationGroup="ScheduleDtl"--%>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Meeting Date"
                                                    ValidationGroup="Submit" ControlToValidate="txtdate" Display="None" Text="*"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtdate">
                                                    <%--OnClientDateSelectionChanged="CheckDateEalier"--%>
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtdate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                            </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Time</label>
                                                </div>
                                                <asp:TextBox ID="txttime" runat="server" CssClass="form-control" ToolTip="Enter Time" meta:resourcekey="txttimeResource1"
                                                    TabIndex="5"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meinTime" runat="server" TargetControlID="txttime"
                                                    Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" AutoComplete="false" AutoCompleteValue="12:00:00"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" UserTimeFormat="None" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevinTime" runat="server" ControlExtender="meinTime"
                                                    ControlToValidate="txttime" IsValidEmpty="False"
                                                    InvalidValueMessage="From Time is invalid" Display="None" TooltipMessage="Input a time"
                                                    EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ErrorMessage="mevinTime" />
                                                <%--ValidationGroup="Submit" --%>
                                                <asp:Label ID="lblTipp" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM" meta:resourcekey="lblTippResource1">
                                                </asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvtime" runat="server" ControlToValidate="txttime"
                                                    Display="None" ErrorMessage="Please Enter From Time" ValidationGroup="Submit"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>To Time</label>
                                                </div>
                                                <asp:TextBox ID="txtToTime" runat="server" CssClass="form-control" ToolTip="Enter Time" meta:resourcekey="txttimeResource2" onchange="Check_Time();"
                                                    TabIndex="6"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meinTime1" runat="server" CultureAMPMPlaceholder="" AutoComplete="false" AutoCompleteValue="12:00:00"
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    OnInvalidCssClass="errordate" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtToTime" AcceptAMPM="True">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevinTime1" runat="server" ControlExtender="meinTime"
                                                    ControlToValidate="txtToTime" Display="None"
                                                    ErrorMessage="Please Select Time" InvalidValueBlurredMessage="*" InvalidValueMessage="To Time is invalid"
                                                    IsValidEmpty="False" SetFocusOnError="True"></ajaxToolKit:MaskedEditValidator>
                                                <asp:Label ID="lblTipp1" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM" meta:resourcekey="lblTippResource1"></asp:Label>
                                                <asp:RegularExpressionValidator ID="revstarttime" runat="server" ControlToValidate="txtToTime"
                                                    ValidationGroup="Submit" ValidationExpression="(((0[1-9])|(1[0-2])):([0-5])([0-9])\s(A|P)M)">
                                                </asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvtotime" runat="server" ControlToValidate="txtToTime"
                                                    Display="None" ErrorMessage="Please Enter To Time" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%-- Modified by Saahil Trivedi 27/01/2022--%>
                                                    <sup>*</sup>
                                                    <label>Venue</label>
                                                </div>
                                                <asp:DropDownList ID="ddlVenue" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Venue" TabIndex="7">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- Modified by Saahil Trivedi 27/01/2022--%>
                                                <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ErrorMessage="Please Select Venue"
                                                    ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlVenue" Display="None" Text="*"></asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Meeting Title</label>
                                                </div>
                                                <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" ToolTip="Enter Meeting Title" TabIndex="9" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                                    Display="None" ErrorMessage="Please Enter Meeting Title" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%--  <label><span style="color: #FF0000">*</span>Agenda Contents :</label>--%>
                                                    <label><span style="color: #FF0000">*</span>Agenda Contents </label>
                                                    <asp:TextBox ID="txtAgendaDetails" runat="server" TabIndex="5" ToolTip="Add Agenda Details" CssClass="form-control" TextMode="MultiLine" ValidationGroup="Submit"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAgendaDetails" runat="server" ControlToValidate="txtAgendaDetails" Display="None" ErrorMessage="Please Add Agenda Contents." ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                </div>

                                            </div>
                                        </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlAgendaDetails" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Upload Files</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Attach File </label>
                                            </div>
                                            <asp:FileUpload ID="fumeeting" runat="server" ValidationGroup="submit" ToolTip="Select file to upload" TabIndex="10" accept=".pdf" />
                                            <asp:Label ID="lblfile" runat="server" />

                                        </div>
                                        <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Status</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="chkStatus" name="switch" checked />
                                                <label data-on="Active" data-off="InActive" for="chkStatus"></label>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divMNo" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Number</label>
                                            </div>
                                            <asp:TextBox ID="txtnumber" runat="server" Enabled="false" CssClass="form-control" ToolTip="Meeting Number" TabIndex="12"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-md-4">
                                    <%--col-12 btn-footer--%>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="11" CssClass="btn btn-primary" ToolTip="Click here to Add" />
                                </div>

                            </asp:Panel>
                            <div class="col-12 btn-footer mt-5">
                                <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="13" CausesValidation="true" OnClientClick="return validate_Active();"></asp:LinkButton>

                                <asp:LinkButton ID="btnplanScheduleExcelReports" runat="server" TabIndex="21" OnClick="btnplanScheduleExcelReports_Click" ValidationGroup="SubPercentage" CssClass="btn btn-info">
                                     <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Meeting Schedule Details Excel</asp:LinkButton>

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="14" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12 mb-4">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAgenda" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">

                                                    <h5>MEETING TITLE LIST </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>Edit </th>
                                                            <th>Meeting Number </th>
                                                            <th>Meeting Title </th>
                                                            <th>Venue </th>
                                                            <th>Meeting Date </th>
                                                            <th>From Time </th>
                                                            <th>To Time </th>
                                                            <th>Document Preview</th>
                                                            <th>Status</th>

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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("PK_AGENDA") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("MEETING_CODE")%></td>
                                                <td><%# Eval("AGENDATITAL")%></td>
                                                <td><%# Eval("VENUE")%></td>
                                                <td><%# Eval("MEETINGDATE","{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("MEETINGTIME")%></td>
                                                <td><%# Eval("MEETINGTOTIME")%></td>


                                                <td>
                                                    <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                        Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("FILE_NAME") %>' data-toggle="modal" data-target="#preview"
                                                        CommandArgument='<%# Eval("FILE_NAME") %>' Visible='<%# Convert.ToString(Eval("FILE_NAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnplanScheduleExcelReports" />
        </Triggers>
    </asp:UpdatePanel>
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
    <script>
        function SetActive(val) {
            $('#chkStatus').prop('checked', val);

        }
        function validate_Active() {
            $('#hfdActive').val($('#chkStatus').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate_Active();
                });
            });
        });
    </script>

</asp:Content>
