<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IncidentReportEntry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_IncidentReportEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
    <script src="jquery/jquery.js" type="text/javascript"></script>--%>
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
        function check_date() {


            var somedayString = document.getElementById('<%=txtinsdate.ClientID%>').value;
            var dateParts = somedayString.split("/");
            var day = dateParts[0];
            var month = dateParts[1] - 1;
            var year = dateParts[2];
            someday = new Date(year, month, day);
            var today = new Date();
            if (someday > today) {
                alert('Future date is not allowed');
                document.getElementById('<%=txtinsdate.ClientID%>').value = '';
                document.getElementById('<%=txtinsdate.ClientID%>').focus();
            }
        }
        function ClearDate(a) {
            if (a == 1) {
                document.getElementById('<%=txtformdate.ClientID%>').value = '';
                document.getElementById('<%=txttodate.ClientID%>').value = '';
            }
            else {
                document.getElementById('<%=txtrouteno.ClientID%>').value =
                document.getElementById('<%=txtinsdate.ClientID%>').value =
                document.getElementById('<%=txtincidenttime.ClientID%>').value =
                document.getElementById('<%=txtincidentplace.ClientID%>').value =
                document.getElementById('<%=txtfollwup.ClientID%>').value =
                document.getElementById('<%=txtNatureinc.ClientID%>').value = '';
                document.getElementById('<%=hfviewsate.ClientID%>').value = 'add';
            }
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
                            <h3 class="box-title">
                            INCIDENT REGISTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlPersInfo" runat="server">
                                <div id="complaint" runat="server" visible="true">
                                    <div class="col-12">
                                        <%--  <div class="panel panel-heading">Incident  Details</div>--%>
                                        <div class="row">
                                            <asp:Label ID="lable1" runat="server" Text=""></asp:Label>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Route No.</label>
                                                </div>
                                                <asp:TextBox ID="txtrouteno" runat="server" CssClass="form-control" MaxLength="10" ToolTip="Enter Route No" TabIndex="1" AutoPostBack="true" autocomplete="off" OnTextChanged="txtrouteno_TextChanged"></asp:TextBox>
                                                <asp:HiddenField ID="hfviewsate" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvrout" runat="server" ControlToValidate="txtrouteno" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Route No."></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Incident Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Div1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtinsdate" CssClass="form-control" ToolTip="Enter Incident Date" TabIndex="2"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date();"></asp:TextBox>

                                                    <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtinsdate" TargetControlID="txtinsdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtinsdate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtinsdate" EmptyValueMessage="Please Enter Incident Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Complaint">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Incident Time</label>
                                                </div>
                                                <asp:TextBox ID="txtincidenttime" CssClass="form-control" ToolTip="Enter Incident Time" TabIndex="3"
                                                    runat="server" Style="z-index: 0;" autocomplete="off"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtincidenttime"
                                                    Mask="99:99" MaskType="Time"
                                                    AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                    DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server"
                                                    ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtincidenttime"
                                                    EmptyValueBlurredText="Empty"
                                                    Display="None"
                                                    Text="*"
                                                    EmptyValueMessage="Please Enter Incident Time"
                                                    InvalidValueMessage="Incident Time is Invalid (Enter 12 Hour Format)"
                                                    SetFocusOnError="true"
                                                    TooltipMessage="Please Enter Incident Time"
                                                    IsValidEmpty="false"
                                                    ValidationGroup="Complaint" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Follow up</label>
                                                </div>
                                                <asp:TextBox ID="txtfollwup" runat="server" CssClass="form-control" MaxLength="225" ToolTip="Enter Follow Up" TabIndex="4" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfollwup" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Follow Up."></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Nature of Incident</label>
                                                </div>
                                                <asp:TextBox ID="txtNatureinc" runat="server" CssClass="form-control" MaxLength="500" TabIndex="5" ToolTip="Enter Nature of Incident" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNatureinc" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Nature of Incident."></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Incident Place</label>
                                                </div>
                                                <asp:TextBox ID="txtincidentplace" runat="server" CssClass="form-control" MaxLength="500" ToolTip="Enter Incident Place" TextMode="MultiLine" TabIndex="6" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvaction" runat="server" ControlToValidate="txtincidentplace" ValidationGroup="Complaint" Display="None" ErrorMessage="Please Enter Incident Place"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Panel ID="button" runat="server">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="7" ValidationGroup="Complaint" />
                                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="9" />
                                            <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClientClick="ClearDate(0);" TabIndex="8" />

                                            <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Complaint" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                        </asp:Panel>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvComplaint" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Action</th>
                                                                <th>Route No.</th>
                                                                <th>Nature of Incident</th>
                                                                <th>Incident Place</th>
                                                                <th>Incident Date</th>
                                                                <th>Incident Time</th>
                                                                <th>Follow Up</th>

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
                                                    <td><%#Eval("NATURE_OF_INCIDENT")%></td>
                                                    <td><%#Eval("INCIDENT_PLACE")%></td>
                                                    <td><%#Eval("INCIDENT_DATE","{0:dd/MM/yyyy}")%></td>
                                                    <td><%#Eval("INCIDENT_TIME","{0:hh:mm tt}")%></td>
                                                    <td><%#Eval("FOLLOW_UP")%></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <div id="datecontrols" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="sub-heading">Incident Report</div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtformdate1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtformdate" CssClass="form-control" ToolTip="Enter From Date"
                                                        runat="server" Style="z-index: 0;" autocomplete="off"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtformdate1" TargetControlID="txtformdate">
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
                                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="report" ForeColor="Red" runat="server" ControlToValidate="txtformdate"
                                                    ControlToCompare="txttodate" Operator="LessThanEqual" Type="Date" ErrorMessage="Please ensure that To Date is greater than or equal to  From Date." Display="None"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowReport" runat="server" CssClass="btn btn-info" Text="Show Report" ValidationGroup="report" OnClick="btnShowReport_Click" />
                                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnback_Click" />
                                        <asp:Button ID="btnDateClear" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClientClick="ClearDate(1);" />
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

