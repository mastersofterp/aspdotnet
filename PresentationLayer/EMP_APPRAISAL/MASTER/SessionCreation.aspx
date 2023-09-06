<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SessionCreation.aspx.cs" Inherits="EMP_APPRAISAL_MASTER_SessionCreation" %>

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

    <div class="heading">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Appraisal Session Creation</h3>
                        </div>
                        <div class="box-body">

                            <div class="col-md-12">
                                <%-- Note : <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                                <div class="col-md-12">--%>
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Appraisal Session</h5>
                                    </div>
                                </div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <%--<div class="panel-heading">Add/Edit Appraisal Session   </div>--%>
                                        <div class="panel-body">
                                            <br />
                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlAdd" runat="server">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: #FF0000">*</span>Session Name :</label>
                                                            <asp:TextBox ID="txtSessionName" runat="server" MaxLength="50" ToolTip="Session Name" CssClass="form-control datepickerinput" TabIndex="1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvSessionName" runat="server" ControlToValidate="txtSessionName"
                                                                Display="None" ErrorMessage="Please Select Session Name"
                                                                SetFocusOnError="True" ValidationGroup="Submit">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>Session Short Name :</label>
                                                            <asp:TextBox ID="txtSessionShortName" runat="server" ToolTip="Session Short Name" CssClass="form-control datepickerinput" TabIndex="2"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: #FF0000">*</span>From Date :</label>
                                                            <div class='input-group date' id="Div1">
                                                              <div class="input-group-addon">
                                                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    </div>
                                                            
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepickerinput" onchange="CheckDate(1);" TabIndex="3"></asp:TextBox>
                                                                <%-- //onChange="return datediffExp();" onblur="return datediffExp();"--%>
                                                              
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
                                                            <label><span style="color: #FF0000">*</span>To Date :</label>
                                                            <div class='input-group date' id="Div2">
                                                                <div class="input-group-addon">
                                                                        <asp:Image ID="imgcal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    </div>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepickerinput" onchange="CheckDate(2);" TabIndex="4"></asp:TextBox>
                                                                <%--onChange="return datediffExp();" onblur="return datediffExp();"--%>
                                                                
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                PopupButtonID="imgcal1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
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
                                                            <label>Is Active :</label>
                                                            <asp:RadioButton ID="rdbActiveYes" runat="server" GroupName="gpactive" Text="Yes" Checked="True" TabIndex="5" />
                                                            <asp:RadioButton ID="rdbActiveNo" runat="server" GroupName="gpactive" Text="No" TabIndex="6" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>Is Special :</label>
                                                            <asp:RadioButton ID="rdbSpecialYes" runat="server" GroupName="gpSpeccial" Text="Yes" TabIndex="7" />
                                                            <asp:RadioButton ID="rdbSpecialNo" runat="server" GroupName="gpSpeccial" Text="No" Checked="True" TabIndex="8" />
                                                        </div>
                                                    </div>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary progress-button" TabIndex="9" />&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" TabIndex="10" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:Panel ID="pnlList" runat="server">
                                                            <asp:ListView ID="lvAppraisalDetails" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                         <div class="sub-heading">
                                                            <h5>Session Creation List</h5>
                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
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
                                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("SESSION_ID")%>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                            <asp:ImageButton ID="btnDelete" runat="server" Visible="false" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("SESSION_ID") %>'
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
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>


            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div style="text-align: center">
                    <div>

                        <div>
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                        </div>
                        <div>
                            &nbsp;&nbsp;Are you sure you want to delete this record?
                        </div>


                        <div>
                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-danger" />
                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
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
