<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="logbook.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_logbook"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .numeric_button {
            background-color: #384B69;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
            border: none;
        }

        .current_page {
            background-color: #09151F;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
        }

        .next_button {
            background-color: #1F3548;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
        }

        .style1 {
            height: 28px;
        }
    </style>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">LOG BOOK ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle</label>
                                        </div>
                                        <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Vehicle" TabIndex="1" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" ErrorMessage="Please Select Vehicle"
                                            ValidationGroup="ScheduleDtl" InitialValue="0" ControlToValidate="ddlVehicle"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Trip Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTripTypr" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Trip Type" TabIndex="2" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTripTypr_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDriver1" runat="server" ErrorMessage="Please Select Trip Type."
                                            ValidationGroup="ScheduleDtl" InitialValue="0" ControlToValidate="ddlTripTypr"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Tour Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgPdOn">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTourDate" runat="server" TabIndex="3" ValidationGroup="ScheduleDtl"
                                                CssClass="form-control" ToolTip="Enter Tour Date" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtTourDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txtTourDate" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                ControlToValidate="txtTourDate" IsValidEmpty="false" EmptyValueMessage="Please Select Tour Date."
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                Display="None" Text="*" ValidationGroup="ScheduleDtl">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Arrival Time</label>
                                        </div>
                                        <asp:TextBox ID="txtArrTime" runat="server" TabIndex="5" CssClass="form-control"
                                            ToolTip="Enter Arrival Time"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="meeArrTime" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtArrTime" AcceptAMPM="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeArrTime"
                                            ControlToValidate="txtArrTime" Display="None" EmptyValueBlurredText="Empty"
                                            EmptyValueMessage="Please Enter Arrival Time" InvalidValueBlurredMessage="Invalid Time"
                                            InvalidValueMessage="Arrival Time is Invalid (Enter 12 Hour Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Arrival Time" ValidationGroup="ScheduleDtl"
                                            IsValidEmpty="false">
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Departure Time</label>
                                        </div>
                                        <asp:TextBox ID="txtDepTime" runat="server" TabIndex="4" CssClass="form-control"
                                            ToolTip="Enter Departure Time" onchange="Check_Time();" ></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="meDepTime" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtDepTime" AcceptAMPM="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevDepTime" runat="server" ControlExtender="meDepTime"
                                            ControlToValidate="txtDepTime" Display="None" EmptyValueBlurredText="Empty"
                                            InvalidValueMessage="Departure Time is Invalid (Enter 12 Hour Format)" EmptyValueMessage="Please Enter Departure Time"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Departure Time" ValidationGroup="ScheduleDtl"
                                            IsValidEmpty="false"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                 
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Location</label>
                                        </div>
                                        <asp:TextBox ID="txtFromLoc" runat="server" TabIndex="6" onkeypress="return CheckAlphabet(event,this);"
                                            CssClass="form-control" ToolTip="Enter From Location" MaxLength="60"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFromLoc" runat="server" ErrorMessage="Please Enter From Location"
                                            ValidationGroup="ScheduleDtl" ControlToValidate="txtFromLoc" Display="None" Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Location</label>
                                        </div>
                                        <asp:TextBox ID="txtToLoc" runat="server" TabIndex="7" onkeypress="return CheckAlphabet(event,this);"
                                            CssClass="form-control" ToolTip="Enter To Location" MaxLength="60"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter To Location"
                                            ValidationGroup="ScheduleDtl" ControlToValidate="txtToLoc" Display="None" Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="8" CssClass="form-control"
                                            ToolTip="Enter Remark If Any"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Start Meter Reading</label>
                                        </div>
                                        <asp:TextBox ID="txtSMeterReading" runat="server" TabIndex="9" onkeypress="return CheckNumeric(event,this);"
                                            CssClass="form-control" ToolTip="Enter Start Meter Reading" MaxLength="10" onchange="SReadTextChange();">
                                        </asp:TextBox>
                                        <%--  Enable the button so it can be played again --%>
                                        <asp:RequiredFieldValidator ID="rfvSMeterReading" runat="server" ErrorMessage="Please Enter Start Meter Reading"
                                            ValidationGroup="ScheduleDtl" ControlToValidate="txtSMeterReading" Display="None"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>End Meter Reading </label>
                                        </div>
                                        <asp:TextBox ID="txtEmeterReading" runat="server" onkeypress="return CheckNumeric(event,this);" TabIndex="10"
                                            CssClass="form-control" ToolTip="Enter End Meter Reading" MaxLength="10" onchange="EReadTextChange();">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmeterReading" runat="server" ErrorMessage="Please Enter End Meter Reading"
                                            ValidationGroup="ScheduleDtl" ControlToValidate="txtEmeterReading" Display="None"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Meter Reading </label>
                                        </div>
                                        <asp:TextBox ID="txtTotalKM" runat="server" TabIndex="11" CssClass="form-control" ToolTip="Total Meter Reading">
                                        </asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12" id="trHire" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Hire Charges</h5>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Waiting Charges :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblWaitingCharge" runat="server" ToolTip="Waiting Charges"
                                                        Enabled="false"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Hire Rate / KM :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblHireRate" runat="server" ToolTip="Hire Rate / KM"
                                                        Enabled="false"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Driver TA :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDriverTA" runat="server" ToolTip="Driver TA"
                                                        Enabled="false"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Total Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalAmount" runat="server" ToolTip="Total Amount"
                                                        Enabled="false"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="sub-heading">
                                    <h5>Tour Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Driver Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Driver Name" TabIndex="12">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDriver" runat="server" ErrorMessage="Please Select Driver Name"
                                            ValidationGroup="ScheduleDtl" InitialValue="0" ControlToValidate="ddlDriver"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Passenger Name</label>
                                        </div>
                                        <asp:TextBox ID="txtPassengerName" runat="server" TabIndex="13" onkeypress="return CheckAlphabetSpecial(event,this);"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Enter Passenger Name" MaxLength="60"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Tour Details </label>
                                        </div>
                                        <asp:TextBox ID="txtTourDetails" runat="server" TextMode="MultiLine" TabIndex="14"
                                            CssClass="form-control" ToolTip="Enter Tour Details"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="ScheduleDtl"
                                    OnClick="btnSubmit_Click" TabIndex="15" ToolTip="Click here to Submit" UseSubmitBehavior="false" OnClientClick="handleButtonClick()"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                    TabIndex="16" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="ScheduleDtl" HeaderText="Following Fields are mandatory" />
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvFitness" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Log Book Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>VEHICLE
                                                            </th>
                                                            <th>TOUR DATE
                                                            </th>
                                                            <th>FROM LOCATION
                                                            </th>
                                                            <th>TO LOCATION
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LOGBOOKID") %>'
                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png"
                                                CommandArgument='<%# Eval("LOGBOOKID") %>' ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("VEHICLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOURDATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FROM_LOCATION")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TO_LOCATION")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="vista-grid_datapager d-none">
                                        <div class="text-center">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvFitness" PageSize="10"
                                                OnPreRender="dpPager_PreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

          

            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <div class="col-md-12">
                <div class="text-center">
                    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                        <div class="text-center">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                    <div class="text-center">
                                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvFitness" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function SReadTextChange() {

            if (document.getElementById('<%=txtSMeterReading.ClientID %>').value != '') {

                var SMReading = parseFloat(document.getElementById('<%=txtSMeterReading.ClientID %>').value);
                var EMReading = 0;
                var tot = 0;
                if (document.getElementById('<%=txtEmeterReading.ClientID %>').value != '') {
                    EMReading = parseFloat(document.getElementById('<%=txtEmeterReading.ClientID %>').value);
                    //                    alert(EMReading);
                    //                    alert(SMReading);
                    if (EMReading > SMReading) {
                        tot = (EMReading - SMReading);
                    }
                    else {
                        alert('Start Reading Should be Smaller than End Reading.');

                        document.getElementById('<%=txtSMeterReading.ClientID %>').value = '';
                        window.setTimeout(function () {
                            document.getElementById('<%=txtSMeterReading.ClientID %>').focus();
                        }, 0);
                        return false;
                    }
                }
                document.getElementById('<%=txtTotalKM.ClientID %>').value = tot;

            }

        }
        function EReadTextChange() {

            if (document.getElementById('<%=txtEmeterReading.ClientID %>').value != '') {
                var EMReading = parseFloat(document.getElementById('<%=txtEmeterReading.ClientID %>').value);
                var SMReading = parseFloat(document.getElementById('<%=txtSMeterReading.ClientID %>').value);
                var tot = 0;
                if (EMReading != 0 && SMReading != 0) {
                    if (EMReading > SMReading) {
                        tot = (EMReading - SMReading);
                    }
                    else {

                        alert('Start Reading Should be Smaller than End Reading.');
                        document.getElementById('<%=txtEmeterReading.ClientID %>').value = '';
                        document.getElementById('<%=txtEmeterReading.ClientID %>').focus();
                        window.setTimeout(function () {
                            document.getElementById('<%=txtEmeterReading.ClientID %>').focus();
                        }, 0);
                        return false;

                    }
                }
                document.getElementById('<%=txtTotalKM.ClientID %>').value = tot;
            }
        }

        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

        //        function Calculate() {
        //            
        //            var ERead = document.getElementById("txtSMeterReading").value;
        //            var SRead = document.getElementById("txtSMeterReading").value;
        //            try 
        //            {
        //            double FKM  = ERead - SRead;
        //            
        //            document.getElementById("txtTotalKM").value = FKM;
        //            document.getElementById('ctl00_ContentPlaceHolder1_txtEMeterReading').focus();
        //            }
        //            catch (ex)
        //            {
        //                alert(ex);
        //            }
        //        }

        function CheckAlphabetSpecial(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 44) {  // k == 44 use for comma  

                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {

                obj.style.backgroundColor = "White";
                return true;
            }

            else {

                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

        function abcd() {
            alert('asffsfgfdgfgf');
        }
    </script>

    <script type="text/javascript">

        function Check_Time() {
             var T_Date = document.getElementById('<%=txtTourDate.ClientID%>').value;
             var time = document.getElementById('<%=txtDepTime.ClientID%>').value;
             var hours = Number(time.match(/^(\d+)/)[1]);
             var minutes = Number(time.match(/:(\d+)/)[1]);
             var AMPM = time.match(/\s(.*)$/)[1];
             if (AMPM == "PM" && hours < 12) hours = hours + 12;
             if (AMPM == "AM" && hours == 12) hours = hours - 12;
             var sHours = hours.toString();
             var sMinutes = minutes.toString();
             if (hours < 10) sHours = "0" + sHours;
             if (minutes < 10) sMinutes = "0" + sMinutes;
             var depatrtime = sHours + ":" + sMinutes;
            //alert(depatrtime);
             var time = document.getElementById('<%=txtArrTime.ClientID%>').value;
               var hours1 = Number(time.match(/^(\d+)/)[1]);
               var minutes1 = Number(time.match(/:(\d+)/)[1]);
               var AMPM1 = time.match(/\s(.*)$/)[1];
               if (AMPM1 == "PM" && hours1 < 12) hours1 = hours1 + 12;
               if (AMPM1 == "AM" && hours1 == 12) hours1 = hours1 - 12;
               var sHours1 = hours1.toString();
               var sMinutes1 = minutes1.toString();
               if (hours1 < 10) sHours1 = "0" + sHours1;
               if (minutes1 < 10) sMinutes1 = "0" + sMinutes1;
               var arrivaltime = sHours1 + ":" + sMinutes1;
             //  alert(arrivaltime);
             

                if ( arrivaltime > depatrtime) {
                    alert('Departure Time should be greater then Arrival Time..');
                    document.getElementById('<%=txtDepTime.ClientID%>').value = '';
   
            }
        }
        
    </script>
    <script>
        function handleButtonClick() {
            var button = document.getElementById('<%= btnSubmit.ClientID %>');

    // Disable the button and update text
    button.disabled = true;
    button.value = "Please Wait...";

    // Enable the button after 10 seconds
    setTimeout(function () {
        button.disabled = false;
        button.value = "Submit";
    }, 10000); // 10000 milliseconds = 10 seconds
}
</script>
</asp:Content>
