<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComplaintRegister.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_ComplaintRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>--%>
    <script src="jquery/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">

        function checkIDAvailability() {
            $.ajax({
                type: "POST",
                url: "JqueryAjaxPost.aspx/checkUserName",
                data: "{IDVal: '" + $("#<% =txtrouteno.ClientID %>").val() + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onSuccess,
                failure: function (AjaxResponse) {
                    document.getElementById("Label1").innerHTML = "Dfgdfg";
                }
            });
            function onSuccess(AjaxResponse) {
                document.getElementById("Label1").innerHTML = AjaxResponse.d;
            }
        }
        function check_date(a) {

            var somedayString = document.getElementById('<%=txtcomdate.ClientID%>').value;
            var dateParts = somedayString.split("/");
            var day = dateParts[0];
            var month = dateParts[1] - 1;
            if (month > 11) {
                alert("Please Enter valid date");
                document.getElementById('<%=txtcomdate.ClientID%>').value = '';
                document.getElementById('<%=txtcomdate.ClientID%>').focus();
            }
            var year = dateParts[2];
            someday = new Date(year, month, day);

            if (a == 1) {
                var atString = document.getElementById('<%=txtactiontakendate.ClientID%>').value;
                var atdateParts = atString.split("/");
                var atday = atdateParts[0];
                var atmonth = atdateParts[1] - 1;
                if (atmonth > 11) {
                    alert("Please Enter valid date");
                    document.getElementById('<%=txtactiontakendate.ClientID%>').value = '';
                    document.getElementById('<%=txtactiontakendate.ClientID%>').focus();
                }
                var atyear = atdateParts[2];
                actiontaken = new Date(atyear, atmonth, atday);
                var today = new Date();
                if (actiontaken > today) {
                    alert('Future date is not allowed');
                    document.getElementById('<%=txtactiontakendate.ClientID%>').value = '';
                    document.getElementById('<%=txtactiontakendate.ClientID%>').focus();
                }
                if (actiontaken < someday) {
                    alert('Please ensure that the Action Date is greater than or equal to the Complaint Date.');
                    document.getElementById('<%=txtactiontakendate.ClientID%>').value = '';
                    document.getElementById('<%=txtactiontakendate.ClientID%>').focus();
                }
            }
            else {
                var today = new Date();
                if (someday > today) {
                    alert('Future date is not allowed');
                    document.getElementById('<%=txtcomdate.ClientID%>').value = '';
                    document.getElementById('<%=txtcomdate.ClientID%>').focus();
                }
            }
        }
        function ClearDate(a) {
            if (a == 1) {
                document.getElementById('<%=txtformdate.ClientID%>').value = '';
                document.getElementById('<%=txttodate.ClientID%>').value = '';
            }
            else {
                document.getElementById('<%=txtrouteno.ClientID%>').value =
                document.getElementById('<%=txtcomdate.ClientID%>').value =
                document.getElementById('<%=txtcomplaintreceviedth.ClientID%>').value =
                document.getElementById('<%=txtactiontakendate.ClientID%>').value =
                document.getElementById('<%=txtactiontaken.ClientID%>').value =
                document.getElementById('<%=txtNaturecom.ClientID%>').value =
                document.getElementById('<%=txtcomreg.ClientID%>').value = '';
                document.getElementById('<%=hfviewsate.ClientID%>').value = 'add';
            }
        }
        function Visibile() {

        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COMPLAINT REGISTER</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlPersInfo" runat="server">
                                <div id="complaint" runat="server" visible="true">
                                    <div class="col-12">
                                        <%-- <div class="panel panel-heading">Complaint Register Details</div>--%>
                                        <div class="row">
                                            <asp:Label ID="lable1" runat="server" Text=""></asp:Label>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Route No.</label>
                                                </div>
                                                <asp:TextBox ID="txtrouteno" runat="server" CssClass="form-control" MaxLength="10" TabIndex="1" ToolTip="Enter Route No" AutoPostBack="true" autocomplete="off" OnTextChanged="txtrouteno_TextChanged"></asp:TextBox>
                                                <asp:HiddenField ID="hfviewsate" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvrout" runat="server" ControlToValidate="txtrouteno" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Route No."></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Complaint Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtcomdate2">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtcomdate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="2"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(0);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtcomdate2" TargetControlID="txtcomdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtcomdate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtcomdate" EmptyValueMessage="Please Enter Complaint Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Complaint">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Complaint Registered by</label>
                                                </div>
                                                <asp:TextBox ID="txtcomreg" runat="server" CssClass="form-control" MaxLength="225" TabIndex="3" autocomplete="off" ToolTip="Enter Complaint Registered by"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcomreg" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Complaint Registered by."></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Complaint Received Through</label>
                                                </div>
                                                <asp:TextBox ID="txtcomplaintreceviedth" runat="server" CssClass="form-control" MaxLength="225" TabIndex="4" autocomplete="off" ToolTip="Enter Complaint Received Through"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvcompl" runat="server" ControlToValidate="txtcomplaintreceviedth" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Complaint Received Through"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Action Taken Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtactiontakendate1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtactiontakendate" CssClass="form-control" ToolTip="Enter Action Taken Date" TabIndex="5"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(1);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtactiontakendate1" TargetControlID="txtactiontakendate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtactiontakendate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtactiontakendate" EmptyValueMessage="Please Enter Action Taken" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Schedule">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Nature of Complaint</label>
                                                </div>
                                                <asp:TextBox ID="txtNaturecom" runat="server" CssClass="form-control" MaxLength="500" TabIndex="6" TextMode="MultiLine" autocomplete="off" ToolTip="Enter Nature of Complaint"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNaturecom" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Nature of Complaint."></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Action Taken</label>
                                                </div>
                                                <asp:TextBox ID="txtactiontaken" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" TabIndex="7" autocomplete="off" ToolTip="Enter Action Taken"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvaction" runat="server" ControlToValidate="txtactiontaken" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Action Taken"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-12 btn-footer">
                                        <asp:Panel ID="button" runat="server">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ToolTip="Submit" TabIndex="8" ValidationGroup="Complaint" />
                                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Report" OnClick="btnReport_Click" TabIndex="10" />
                                            <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Cancel" OnClientClick="ClearDate(0);  return false;" TabIndex="9" />

                                            <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Complaint" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                        </asp:Panel>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvComplaint" runat="server" OnSelectedIndexChanged="lvComplaint_SelectedIndexChanged">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                <th>Route No.</th>
                                                                <th>Nature of Complaint</th>
                                                                <th>Complaint Registered by</th>
                                                                <th>Complaint Date</th>
                                                                <th>Complaint Received Through</th>
                                                                <th>Action Taken</th>
                                                                <th>Action Taken Date</th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                            CommandArgument='<%# Eval("SNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" />

                                                    </td>
                                                    <td><%#Eval("ROUTE_NO")%></td>
                                                    <td><%#Eval("NATURE_OF_COMPLAINT")%></td>
                                                    <td><%#Eval("COMPLAINT_REGISTERED_BY")%></td>
                                                    <td><%#Eval("COMPLAINT_DATE","{0:dd/MM/yyyy}")%></td>
                                                    <td><%#Eval("COMPLAINT_RECEIVED_THROUGH")%></td>
                                                    <td><%#Eval("ACTION_TAKEN")%></td>
                                                    <td><%#Eval("ACTION_TAKEN_DATE","{0:dd/MM/yyyy}")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <div id="datecontrols" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class=" sub-heading">
                                            <h5>Comaplaint Register Report</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtformdate2">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtformdate" CssClass="form-control" ToolTip="Enter From Date"
                                                        runat="server" Style="z-index: 0;" autocomplete="off"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtformdate2" TargetControlID="txtformdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtformdate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtformdate" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="report">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txttodate2">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txttodate" CssClass="form-control" ToolTip="Enter To Date"
                                                        runat="server" Style="z-index: 0;" autocomplete="off"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txttodate2" TargetControlID="txttodate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txttodate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txttodate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="report">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="report" ForeColor="Red" runat="server" ControlToValidate="txtformdate" Display="None"
                                                    ControlToCompare="txttodate" Operator="LessThanEqual" Type="Date" ErrorMessage="Please ensure that To Date is greater than or equal to From Date."></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowReport" runat="server" CssClass="btn btn-info" Text="Show Report" ToolTip="Show Report" ValidationGroup="report" OnClick="btnShowReport_Click" />
                                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-info" Text="Back" ToolTip="Back" OnClick="btnback_Click" />
                                        <asp:Button ID="btnDateClear" runat="server" CssClass="btn btn-warning" Text="Cancel" ToolTip="Cancel" OnClientClick="ClearDate(1); return false;" />

                                        <asp:ValidationSummary ID="vsdate" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                                    </div>

                                </div>

                            </asp:Panel>
                        </div>
                    </div>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

