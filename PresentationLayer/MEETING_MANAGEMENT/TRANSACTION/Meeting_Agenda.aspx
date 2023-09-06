<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Meeting_Agenda.aspx.cs" Inherits="MEETING_MGT_Transaction_Meeting_Agenda" %>

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
     <script type="text/javascript">                                               //Saahil Trivedi 14-01-2021
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


               if ( TO_Time < From_Time ) {
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

                        <div class="box-body">
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                  <sup>*</sup>
                                                 <%-- Modified by Saahil Trivedi 27/01/2022--%>
                                                <label>Committee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <%-- Modified by Saahil Trivedi 27/01/2022--%>
                                            <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None" Text="*"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Previous Meeting</label>
                                            </div>
                                            <asp:DropDownList ID="ddlpremeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Previous Meeting" OnSelectedIndexChanged="ddlpremeeting_SelectedIndexChanged" TabIndex="2">
                                            </asp:DropDownList>
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
                                                <asp:TextBox ID="txtdate" runat="server" TabIndex="4" CssClass="form-control" ToolTip="Enter Meeting Date" AutoPostBack="true"></asp:TextBox> <%--ValidationGroup="ScheduleDtl"--%>

                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Meeting Date"
                                                ValidationGroup="Submit" ControlToValidate="txtdate" Display="None" Text="*"></asp:RequiredFieldValidator>
                                                 <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" 
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtdate"> <%--OnClientDateSelectionChanged="CheckDateEalier"--%>
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtdate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtdate" Display="None" EmptyValueMessage="Please Select Meeting Date" 
                                                    ErrorMessage="Please Select Meeting Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Select Meeting Date In [dd/MM/yyyy] format"
                                                    IsValidEmpty="false" Text="*" ValidationGroup="Submit" SetFocusOnError="true" EmptyValueBlurredText="Empty">
                                                </ajaxToolKit:MaskedEditValidator>



                         <%--                        <asp:RequiredFieldValidator runat="server" ID="rfvexpectarrival" ControlToValidate="txtdate" Display="None"
                                                        ErrorMessage="Please Select Expected Arrival Date " ValidationGroup="Submit" />
                                               <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" OnClientDateSelectionChanged="CheckDateEalier"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgArrival" TargetControlID="txtdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtdate" ClearMaskOnLostFocus="true">--%> <%--ClearMaskOnLostFocus="true"--%>
<%--                                                    </ajaxToolKit:MaskedEditExtender>
                                                     <ajaxToolKit:MaskedEditValidator ID="mevDateOfarrival" runat="server" ControlExtender="MaskedEditExtender2"
                                                            ControlToValidate="txtdate" EmptyValueMessage="Please Select Expected Arrival Date "
                                                            InvalidValueMessage="Expected Arrival Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Select Expected Arrival Date " EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="Submit" SetFocusOnError="True" IsValidEmpty="false"/>--%>
                                                   


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
                                                ValidationGroup="Submit" ErrorMessage="mevinTime" />
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
                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                            <asp:Label ID="lblTipp1" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM" meta:resourcekey="lblTippResource1"></asp:Label>
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
                                                <label>Last Date of Approval</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
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
                                                    ControlToValidate="txtLastDate" Display="None" 
                                                    ErrorMessage="Please Enter Last Date of Approval In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Last Date of Approval In [dd/MM/yyyy] format"
                                                    IsValidEmpty="false" Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastDate"
                                                Display="None" ErrorMessage="Please Enter Last Date of Approval" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Title</label>
                                            </div>
                                            <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" ToolTip="Enter Meeting Title" TabIndex="9" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                           <%-- Modified by Saahil Trivedi 27/01/2022--%>
                                            <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                                Display="None" ErrorMessage="Please Enter Meeting Title" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAddress2" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Address Line 2 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddLine" TextMode="SingleLine" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Enter Address" TabIndex="10"
                                                onkeypress="return CheckAlphaNumeric(event,this);"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCity" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>City</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCity" runat="server" TabIndex="11" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select City">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select City" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trState" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>State/Province/Region </label>
                                            </div>
                                            <asp:DropDownList ID="ddlState" runat="server" TabIndex="12" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select State">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select State" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trPostal" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Zip/Postal Code </label>
                                            </div>
                                            <asp:TextBox ID="txtZipCode" runat="server" MaxLength="6" TabIndex="13" CssClass="form-control" ToolTip="Enter Zip/Postal Code" onkeypress="return CheckNumeric(event,this);" />
                                            <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode"
                                                Display="None" ErrorMessage="Please Enter Zip/Postal Code." ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCountry" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Country</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" TabIndex="14" CssClass="form-control" ToolTip="Select Country"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Country" ValidationGroup="Submit"></asp:RequiredFieldValidator>

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
                                            <asp:FileUpload ID="FileUpload1" runat="server" ValidationGroup="submit" ToolTip="Select file to upload" TabIndex="10" />

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
                                        <div class="form-group col-md-4"><%--col-12 btn-footer--%>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="11" CssClass="btn btn-primary" ToolTip="Click here to Add" />
                                        </div>
                                   
                                <div class="col-12">
                                    <asp:Panel ID="pnlFile" runat="server" ScrollBars="Auto" Visible="false">
                                        <asp:ListView ID="lvfile" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Download files</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>

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
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                    </td>
                                                    <td>
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                         <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/Images/action_down.png" AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownload_Click" />
                                                    </td>



                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer mt-5">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="13" CausesValidation="true" />
                                <asp:Button ID="btnSendMail" runat="server" Text="Send Notifications" CssClass="btn btn-primary" ToolTip="Send Mail With Agenda Details" OnClick="btnSendMail_Click" TabIndex="15" CausesValidation="false"  Visible="false"/>
                                <asp:Button ID="btnReport" runat="server" Text="Agenda Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnReport_Click" TabIndex="16" CausesValidation="false" Visible="false" />
                                <asp:Button ID="btnCancelMeeting" runat="server" Text="Cancel Meeting" CssClass="btn btn-primary" ToolTip="Send Cancel Meeting Notification" OnClick="btnCancelMeeting_Click" TabIndex="17" CausesValidation="false" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="14" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("PK_AGENDA") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
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
