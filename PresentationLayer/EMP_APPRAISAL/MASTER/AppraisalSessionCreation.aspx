<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalSessionCreation.aspx.cs" Inherits="EMP_APPRAISAL_MASTER_AppraisalSessionCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function datediffExp() {
            //debugger;
            var datt = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var datt2 = document.getElementById('<%=txtToDate.ClientID%>').value;

            if (datt != '' && datt2 != '') {
                if (datt2 < datt) {
                    alert('To Date Shuld Be Greater Than Or Equal To From Date');
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    document.getElementById('<%=txtToDate.ClientID%>').focus();
                    return;
                }
            }
        }


        function CheckDate(a) {
            if (a == 1) {
                document.getElementById('<%=txtToDate.ClientID%>').value = '';
                document.getElementById('<%=txtToDate.ClientID%>').focus();
            }
            else



                var Fromdate = document.getElementById('<%=txtFromDate.ClientID%>').value.split("/");
            var Todate = document.getElementById('<%=txtToDate.ClientID%>').value.split("/");
            var FromDate = new Date(Fromdate[2], Fromdate[1] - 1, Fromdate[0]);
            var ToDate = new Date(Todate[2], Todate[1] - 1, Todate[0]);
            if (FromDate > ToDate) {
                alert('To Date Should Be Greater Than Or Equal To From Date');
                document.getElementById('<%=txtToDate.ClientID%>').value = '';
                document.getElementById('<%=txtToDate.ClientID%>').focus();
            }
        }
    </script>

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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div4" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPRAISAL SESSION CREATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session Name</label>
                                                </div>
                                                <asp:TextBox ID="txtSessionName" runat="server" MaxLength="50" ToolTip="Session Name" CssClass="form-control datepickerinput" TabIndex="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSessionName" runat="server" ControlToValidate="txtSessionName"
                                                    Display="None" ErrorMessage="Please Select Session Name"
                                                    SetFocusOnError="True" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Session Short Name</label>
                                                </div>
                                                <asp:TextBox ID="txtSessionShortName" runat="server" ToolTip="Session Short Name" CssClass="form-control datepickerinput" TabIndex="2"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class='input-group date'>
                                                    <div class="input-group-addon" id="imgCal" runat="server">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepickerinput" onchange="CheckDate(1);" TabIndex="3"></asp:TextBox>

                                                </div>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Enter From Date"
                                                    ValidationGroup="Submit" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Submit" SetFocusOnError="True" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                                                    ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                    ErrorMessage="Invalid date format" ValidationGroup="seminars" Display="None" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image1" runat="server">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepickerinput" onchange="CheckDate(2);" TabIndex="4"></asp:TextBox>

                                                </div>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="Submit"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Submit" SetFocusOnError="True" />
                                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date Should be Greater than to from Date"
                                                    ValidationGroup="Submit" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                                                    ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                    ErrorMessage="Invalid date format" ValidationGroup="seminars" Display="None" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Is Active</label>
                                                </div>
                                                <asp:RadioButton ID="rdbActiveYes" runat="server" GroupName="gpactive" Text="Yes" Checked="True" TabIndex="5" />
                                                <asp:RadioButton ID="rdbActiveNo" runat="server" GroupName="gpactive" Text="No" TabIndex="6" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Is Special</label>
                                                </div>
                                                <asp:RadioButton ID="rdbSpecialYes" runat="server" GroupName="gpSpeccial" Text="Yes" TabIndex="7" />

                                                <asp:RadioButton ID="rdbSpecialNo" runat="server" GroupName="gpSpeccial" Text="No" Checked="True" TabIndex="8" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSave_Click" CssClass="btn btn-outline-primary" TabIndex="9" />&nbsp;
                                         <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" TabIndex="10" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    </div>
                                    <div class="col-12 mt-3 mb-3">
                                        <asp:Panel ID="pnlList" runat="server">
                                            <asp:ListView ID="lvAppraisalDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Session Creation List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Action
                                                                    </th>
                                                                    <th>Session Name
                                                                    </th>
                                                                    <th>From Date
                                                                    </th>
                                                                    <th>To Date
                                                                    </th>
                                                                    <th>IS_SPECIAL
                                                                    </th>
                                                                    <th>IS_ACTIVE
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
                                                    <tr class="item">
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SESSION_ID")%>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" Visible="false" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SESSION_ID") %>'
                                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                OnClientClick="showConfirmDel(this); return false;" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("SESSION_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FROM_DATE", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TO_DATE", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("IS_SPECIAL")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblActive" runat="server" Text='<%# Eval("IS_ACTIVE")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div style="text-align: center">
                    <div>

                        <div>
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                        </div>
                        <div>
                            &nbsp;&nbsp;Are you sure you want to delete this record?
                        </div>


                        <div>
                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-outline-primary" />
                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-outline-primary" />
                        </div>

                    </div>
                </div>
            </asp:Panel>

            <script type="text/javascript">
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




                function checkdate(input) {
                    var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
                    var returnval = false
                    if (!validformat.test(input.value)) {
                        alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                        document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                        document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                    }
                    else {
                        var monthfield = input.value.split("/")[0]

                        if (monthfield > 12 || monthfield <= 0) {
                            alert("Month Should be greate than 0 and less than 13");
                            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                        }
                    }
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

