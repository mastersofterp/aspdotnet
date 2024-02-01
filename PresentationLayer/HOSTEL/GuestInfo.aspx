<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GuestInfo.aspx.cs" Inherits="Hostel_GuestInfo" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //if (prm != null) {
        //    prm.add_endRequest(function (sender, e) {
        //        if (sender._postBackSettings.panelsToUpdate != null) {
        //            $('#table2').dataTable();
        //        }
        //    });
        //};

        //Below two function added by Saurabh L on 28/09/2022
        //Purpose: for validation feilds and from date and To date
        function AlphabetsOnly(e, element) {
            var regex = new RegExp(/^[a-zA-Z\s]+$/);
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        };

        function CheckInteger(event, element) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 8) return true;
            if ((charCode < 48 || charCode > 57))
                return false;
            return true;
        };

        function checkDate(event, element) {
            //Below code updated by Saurabh L on 30/09/2022
            var startDate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            startDate = startDate.split("/");
            //  var startDate = $('#ctl00_ContentPlaceHolder1_txtFromDate').val().split("/");
            var sd = new Date(startDate[2], startDate[1] - 1, startDate[0]);
            //  var endDate = $('#ctl00_ContentPlaceHolder1_txtToDate').val().split("/");
            var endDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            endDate = endDate.split("/");
            var ed = new Date(endDate[2], endDate[1] - 1, endDate[0]);
            if (sd > ed || startDate[0] == "") {
                //iziToast.warning({ message: "End Date Should be Greater Than Start Date." });
                alert('To Date should be greater than From Date.');
                // $("#ctl00_ContentPlaceHolder1_txtToDate").val("");
                document.getElementById('<%=txtToDate.ClientID%>').value = "";
                return false;
            }
        }

        //----------End by Saurabh L ----------------------------------

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
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
    </script>

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">GUEST INFORMATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12 col-lg-6">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Guest Information</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Resident Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlResidentType" runat="server" TabIndex="1"
                                            AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlResidentType_SelectedIndexChanged" AutoPostBack="True" />
                                        <asp:RequiredFieldValidator ID="valBlock" runat="server" ControlToValidate="ddlResidentType"
                                            Display="None" ErrorMessage="Please select room." ValidationGroup="submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Guest Name </label>
                                        </div>
                                        <asp:TextBox ID="txtGuestName" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" onkeypress="return AlphabetsOnly(event,this);" />
                                        <asp:RequiredFieldValidator ID="valGuestName" runat="server" ControlToValidate="txtGuestName"
                                            Display="None" ErrorMessage="Please enter guest name." ValidationGroup="submit"
                                            SetFocusOnError="True" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Guest Address </label>
                                        </div>
                                        <asp:TextBox ID="txtGuestAddress" runat="server" TabIndex="3" CssClass="form-control"  Rows="1"
                                            MaxLength="150" TextMode="MultiLine" />  <%--onkeypress="return AlphabetsOnly(event,this);" comment by Ticket 51115--%>
                                        <asp:RequiredFieldValidator ID="valGuestAddress" runat="server" ControlToValidate="txtGuestAddress"
                                            Display="None" ErrorMessage="Please enter guest address." ValidationGroup="submit"
                                            SetFocusOnError="True" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Contact No. </label>
                                        </div>
                                        <asp:TextBox ID="txtContactNo" runat="server" TabIndex="4" MaxLength="10" CssClass="form-control" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftb" runat="server" TargetControlID="txtContactNo" ValidChars="1234567890+-"></ajaxToolKit:FilteredTextBoxExtender>
                                        <%--<asp:RequiredFieldValidator ID="rfvContactNo" runat="server" ControlToValidate="txtContactNo"
                                            Display="None" ErrorMessage="Please enter contact no." ValidationGroup="submit"
                                            SetFocusOnError="True" />--%>
                                        <asp:RegularExpressionValidator ID="rfvContactNo" runat="server"
                                            ControlToValidate="txtContactNo" ErrorMessage="Please Enter Guest Contact No. Proper" ValidationGroup="submit"
                                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>        
                                    </div>
                                    <%--Added BY Preeti A on date 080323 --%>
                                       <div id="divSerchstaff" class="form-group col-lg-6 col-md-6 col-12" runat="server" >                                         
                                        <div class="label-dynamic" >
                                            <sup></sup>
                                            <label>Search Staff User Here</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSearchStaff" runat="server" TabIndex="1"  AppendDataBoundItems="True"
                                           CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchStaff_SelectedIndexChanged" />
                                        <%--<asp:RequiredFieldValidator ID="rfvddlSearchStaff" runat="server" ControlToValidate="ddlSearchStaff"
                                            Display="None" ErrorMessage="Please select Staff User" ValidationGroup="submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                            
                                            END--%>                                         
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Purpose </label>
                                        </div>
                                        <asp:TextBox ID="txtPurpose" runat="server" TabIndex="5" CssClass="form-control" Rows="1"
                                            MaxLength="200" TextMode="MultiLine" onkeypress="return AlphabetsOnly(event,this);" />
                                        <asp:RequiredFieldValidator ID="rfvPurpose" runat="server" ControlToValidate="txtPurpose"
                                            Display="None" ErrorMessage="Please enter purpose." ValidationGroup="submit"
                                            SetFocusOnError="True" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 col-lg-6">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Company Info</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Company Name </label>
                                        </div>
                                        <asp:TextBox ID="txtCompanyName" runat="server" TabIndex="6" MaxLength="100" CssClass="form-control" onkeypress="return AlphabetsOnly(event,this);" />
                                        <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                            Display="None" ErrorMessage="Please enter company name." ValidationGroup="submit"
                                            SetFocusOnError="True" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Company Address </label>
                                        </div>
                                        <asp:TextBox ID="txtCompanyAddress" runat="server" TabIndex="7" Rows="1" CssClass="form-control"
                                            MaxLength="150" TextMode="MultiLine" />  <%--onkeypress="return AlphabetsOnly(event,this);" comment by Ticket 51115--%>
                                        <asp:RequiredFieldValidator ID="rfvCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress"
                                            Display="None" ErrorMessage="Please enter company address." ValidationGroup="submit"
                                            SetFocusOnError="True" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Company Contact No. </label>
                                        </div>
                                        <asp:TextBox ID="txtCContactNo" runat="server" TabIndex="8" MaxLength="10" CssClass="form-control" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCContactNo" ValidChars="1234567890+-"></ajaxToolKit:FilteredTextBoxExtender>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCContactNo"
                                            Display="None" ErrorMessage="Please enter company contact no." ValidationGroup="submit"
                                            SetFocusOnError="True" />--%>
                                        <asp:RegularExpressionValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtCContactNo" ErrorMessage="Please Enter Company Contact No. Proper" ValidationGroup="submit"
                                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="9" CssClass="form-control"
                                                ToolTip="Enter From Date" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" />
                                            <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgFromDate" Enabled="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" ErrorTooltipEnabled="false" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please enter from date."
                                                ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="10"
                                                ToolTip="EnterTO Date" AutoPostBack="true"  OnTextChanged="txtToDate_TextChanged" />
                                            <%-- onchange="checkDate(event,this);"--%>
                                            <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgFromDate" Enabled="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" ErrorTooltipEnabled="false" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" EmptyValueMessage="Please enter TO date."
                                                ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                InvalidValueMessage="TO Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                            OnClick="btnSubmit_Click" TabIndex="11" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="12" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvGuest" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Guest Details</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Guest Name
                                            </th>
                                            <th>Guest Contact No.
                                            </th>
                                            <th>Company Name
                                            </th>
                                            <th>Company Contact No.
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("GUEST_NO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                             <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                    </td>
                                    <td>
                                        <%# Eval("GUEST_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("CONTACT_NO")%>
                                    </td>
                                    <td>
                                        <%# Eval("COMPANY_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("COMPANY_CONTACT_NO")%>
                                    </td>

                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
