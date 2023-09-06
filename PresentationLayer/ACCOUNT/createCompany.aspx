<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="createCompany.aspx.cs" Inherits="Account_createCompany" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>
    <style>
        @media screen and (max-width: 480px) {
            .search_list {
                height: 150!important;
            }
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpan"
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

    <script language="javascript" type="text/javascript">

        //function AskConfirmation() {

        //    if (document.getElementById('ctl00_ctp_txtCode').value == '') {
        //        alert('Select a company/Cash Book For Dropping.');
        //        document.getElementById('ctl00_ctp_txtCode').focus();
        //        return false;

        //    }

        //    document.getElementById('ctl00_ctp_hdnConfirm').value = confirm('Your Are About To Delete The Selected Company/Cash Book, Do You Want To Continue ?');
        //    document.getElementById('ctl00_ctp_btndelete').focus();
        //    return true;

        //}

        function AskToDelete() {
            if (confirm('Do You Want To Drop Cash Book ? ') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnConfirm').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnConfirm').value = 0;
                return false;
            }
        }

        function filterDigits(txt) {
            var a = txt.value;
            var c = a.slice(-1)
            if (c == ',' || c == '!' || c == '#' || c == '$' || c == '%' || c == '^' || c == '&' || c == '*' || c == '@') {
                a = a.substring(0, a.length - 1);
                txt.value = a;
                alert('Special Charcters are Not Allowed');
            }
        }

    </script>

    <script type="text/javascript">
        function ValidateLength(event, value) {
            if (value.length > 149) {
                alert('Sorry, only 150 characters are allowed.');
                return false;
            }
            else {
                return true;
            }
        }
    </script>

    <script type="text/javascript">
        function ValidateLengthOfCode(event, value) {
            if (value.length > 3) {
                alert('Sorry, only 4 characters are allowed.');
                return false;
            }
            else {
                return true;
            }
            //alert(value.length);
        }
    </script>

    <asp:UpdatePanel ID="updpan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">CREATE COMPANY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="row">
                                       <%-- <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Create New Company</h5>
                                            </div>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Code</label>
                                            </div>
                                            <asp:TextBox ID="txtCode" ToolTip="Please Enter Code" Style="text-transform: uppercase"
                                                runat="server" MaxLength="8" CssClass="form-control" onkeypress="return ValidateLengthOfCode(event,this.value);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="Please Enter Code"
                                                ControlToValidate="txtCode" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtCode" FilterMode="InvalidChars"
                                                runat="server" InvalidChars="!@#$%^&*()_+|\}{][:';?/.,*-~=">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Name</label>
                                                <input id="hdnConfirm" runat="server" type="hidden" />
                                            </div>
                                            <asp:TextBox ID="txtName" ToolTip="Please Enter Name" Style="text-transform: uppercase" onkeypress="return ValidateLength(event,this.value);"
                                                CssClass="form-control" runat="server" onkeyup="filterDigits(this)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please Enter Name"
                                                ControlToValidate="txtName" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtName"
                                                FilterMode="InvalidChars" runat="server" InvalidChars="!@#$%^&*_+|\}{][:;?/,*~=">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Financial Year From</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" ToolTip="Please Enter From Date"
                                                    runat="server" CssClass="form-control"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True" ValidationGroup="submit" />
                                                <asp:RegularExpressionValidator ID="revFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" Display="None"
                                                    ErrorMessage="Please Enter Valid Date For Financial Years From!" SetFocusOnError="true" ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Financial Year To</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" ToolTip="Please Enter To Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="True" ValidationGroup="submit" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" Display="None"
                                                    ErrorMessage="Please Enter Valid Date for Financial Years To!" SetFocusOnError="true" ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                    TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <input id="hdnCompNo" runat="server" type="hidden" />
                                                <input id="hdnOldFinYr" runat="server" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Book Writing Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtBWDate" ToolTip="Please Enter Book Writing Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBWDate" runat="server" ControlToValidate="txtBWDate"
                                                    Display="None" ErrorMessage="Please Enter Book Writing Date" SetFocusOnError="True"
                                                    ValidationGroup="submit" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBWDate"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" Display="None"
                                                    ErrorMessage="Please Enter Valid Date for Book Writing Date!" SetFocusOnError="true" ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft"
                                                    TargetControlID="txtBWDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtBWDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Is Company Lock </label>
                                            </div>
                                            <asp:CheckBox ID="chkIsCompanyLock" runat="server" Text="" AutoPostBack="true" />
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btndelete" runat="server" Text="Drop Company" CausesValidation="false"
                                            CssClass="btn btn-warning" OnClick="btndelete_Click" OnClientClick="return AskToDelete()" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:ListBox ID="lstCompany" runat="server" SelectionMode="Single" CssClass="form-control search_list" Style="height: 300px!important;"
                                                    AutoPostBack="True" OnSelectedIndexChanged="lstCompany_SelectedIndexChanged"
                                                    TabIndex="-1"></asp:ListBox>

                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>--%>
</asp:Content>
