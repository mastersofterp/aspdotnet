<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ReprintReceipts.aspx.cs" Inherits="Academic_ReprintReceipts" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">REPRINT OR CANCEL RECEIPT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:UpdatePanel ID="updEdit" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                        <div class="row">

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Search Criteria</label>
                                                </div>

                                                <%--onchange=" return ddlSearch_change();"--%>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                <asp:Panel ID="pnltextbox" runat="server">
                                                    <div id="divtxt" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                    <div id="divDropDown" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <%-- <label id="lblDropdown"></label>--%>
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                        <div class="col-6 offset-md-3 btn-footer">
                                            <%-- OnClientClick="return submitPopup(this.name);"--%>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                        </div>
                                        <div class="col-6 offset-md-3 btn-footer">
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                        </div>

                                        <div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlLV" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>IdNo
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Father Name
                                                                                </th>
                                                                                <th>Mother Name
                                                                                </th>
                                                                                <th>Mobile No.
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                        OnClick="lnkId_Click"></asp:LinkButton>  
                                                                                                                                                                   
                                                                </td>
                                                                <td>
                                                                    <%# Eval("idno")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTERNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FATHERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MOTHERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("STUDENTMOBILE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group col-lg-4 col-md-12 col-12"></div>
                                    <div class="form-group col-lg-4 col-md-12 col-12"></div>



                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <asp:Panel ID="pnlmain" runat="server">
                                
                                <div class="col-12">
                                    <asp:ListView ID="lvPaidReceipts" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="vista-grid">
                                                <div class="sub-heading">
                                                    <h5>Search Results</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Select
                                                            </th>

                                                            <th>Receipt No
                                                            </th>
                                                            <th>Receipt Date
                                                            </th>
                                                            <th>Receipt Type
                                                            </th>
                                                            <th>
                                                                <%--<asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                Semester
                                                            </th>
                                                            <th>Mode
                                                            </th>
                                                            <th>Amount
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
                                                        <input
                                                            id="rdoSelectRecord" value='<%# Eval("DCR_NO") %>' name="Receipts" type="radio" onclick="ShowRemark(this);" /><asp:HiddenField
                                                                ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                        <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                       <%-- <asp:HiddenField ID="hidcan" runat="server" Value='<%# Eval("CAN") %>' />--%>
                                                        <asp:HiddenField ID="hidPaymodecode" runat="server" Value='<%# Eval("PAY_MODE_CODE") %>' />
                                                    </td>

                                                <td>
                                                    <%# Eval("REC_NO") %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblReceiptDt" runat="server" Text='<%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <%# Eval("RECIEPT_TITLE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAY_MODE_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_AMT")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <asp:HiddenField ID="hidDcrNo1" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                </div>
                                <div class="form-group col-lg-4 col-md-12 col-12" id="divreason" runat="server">
                                    <div class="label-dynamic">
                                        <label>Reason of Cancelling Receipt</label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnldetails" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="panelEditing" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>DD/Check No.</label>
                                                </div>
                                                <asp:TextBox ID="txtDDNo" runat="server" TabIndex="7" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                    Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="8"
                                                    CssClass="form-control" Enabled="False" />
                                                <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                    Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Date </label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="imgCalDDDate">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDDDate" runat="server" TabIndex="9" CssClass="form-control" />
                                                    <%--<asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                        PopupButtonID="imgCalDDDate" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                        OnInvalidCssClass="errordate" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                        ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                        InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                        InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>City </label>
                                                </div>
                                                <asp:TextBox ID="txtDDCity" runat="server" TabIndex="10" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Bank </label>
                                                </div>
                                                <asp:DropDownList ID="ddlBank" AppendDataBoundItems="True" TabIndex="11" runat="server"
                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                                <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                    Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                    InitialValue="0" SetFocusOnError="true" />

                                                <asp:HiddenField ID="hfDcrNum" runat="server" />
                                                <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="dd_info" />
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:HiddenField ID="hfIdno" runat="server" />
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnDD_Info" runat="server" OnClick="btnDD_Info_Click"
                                                    TabIndex="12" Text="Update Demand Draft" ValidationGroup="dd_info" CssClass="btn btn-primary" />

                                                <asp:Button ID="BtnCancelDD" runat="server" OnClick="BtnCancelDD_Click"
                                                    Text="Cancel" CssClass="btn btn-warning" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer" id="divfooter" runat="server">
                                <input id="btnReprint" onclick="ReprintReceipt();" runat="server" type="button" value="Reprint Receipt"
                                    disabled="disabled" class="btn btn-primary" />
                                <%--<input id="btnCancel" onclick="CancelReceipt();" runat="server" type="button" value="Cancel Receipt"
                                disabled="disabled" onclick="return btnCancel_onclick()" />&nbsp;--%>
                                <input id="btnEdit" onclick="EditReceipt();" runat="server" type="button" value="Edit Receipt" visible="false"
                                    disabled="disabled" class="btn btn-primary" />
                                <input id="btnCancel" onclick="CancelReceipt();" runat="server" type="button" value="Cancel Receipt"
                                    disabled="disabled" class="btn btn-warning" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnDD_Info" />
            <asp:PostBackTrigger ControlID="BtnCancelDD" />
            <asp:PostBackTrigger ControlID="btnReprint" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnEdit" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">

        function ReprintReceipt() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("ReprintReceipt", "");
                }
                else {
                    alert("Please select a receipt to reprint.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function CancelReceipt() {
            try {
                if (ValidateRecordSelection()) {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("Do you really want to cancel this receipt.") && confirm("If you cancel this receipt, it will not be considered as paid.")) {
                            __doPostBack("CancelReceipt", "");
                        }
                    }
                    else
                        alert('Please enter reason of cancelling receipt.');
                }
                else {
                    alert("Please select a receipt to cancel.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function EditReceipt() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("EditReceipt", "");
                }
                else {
                    alert("Please select a receipt to Edit.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        //function ValidateRecordSelection()
        //{
        //    var isValid = false;
        //    try
        //    {        
        //        var tbl = document.getElementById('tblSearchResults');
        //        if(tbl != null && tbl.rows && tbl.rows.length > 0)
        //        {
        //            for(i = 1; i < tbl.rows.length; i++)
        //            {
        //                var dataRow = tbl.rows[i];
        //                var dataCell = dataRow.firstChild;
        //                var rdo = dataCell.firstChild;
        //                if(rdo.checked)
        //                {
        //                    isValid = true;
        //                }                    
        //            }
        //        }
        //    }
        //    catch(e)
        //    {
        //        alert("Error: " + e.description);
        //    }        
        //    return isValid;
        //}
        function ValidateRecordSelection() {
            var check = false;
            var gridView = document.getElementById("tblSearchResults");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "radio") {
                    if (checkBoxes[i].checked) {
                        check = true;
                    }

                }
            }
            return check;
        }
        function ShowRemark(rdoSelect) {
                try {
                    if (rdoSelect != null && rdoSelect.nextSibling != null) {
                        var hidRemark = rdoSelect.nextSibling;
                        if (hidRemark != null)
                            document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;
                }
        }
        catch (e) { 
            alert("Error: " + e.description);
        }
    }
    function btnEdit_onclick() {

    }

    function btnCancel_onclick() {

    }



    //////////added by Amit B. on date 12/01/22


    $(document).ready(function () {
        debugger
       // $("#<%= pnltextbox.ClientID %>").hide();

        $("#<%= pnlDropdown.ClientID %>").hide();
    });

    function submitPopup(btnsearch) {

        debugger
        var rbText;
        var searchtxt;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;
        var ddlname = e.options[e.selectedIndex].text;
        if (rbText == "Please Select") {
            alert('Please Select Search Criteria.')
            $(e).focus();
            return false;
        }

        else {


            if (rbText == "ddl") {
                var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                var searchtxt = skillsSelect;
                if (searchtxt == "0") {
                    alert('Please Select ' + ddlname + '..!');
                }
                else {
                    __doPostBack(btnsearch, rbText + ',' + searchtxt);
                    return true;
                    //$("#<%= divpanel.ClientID %>").hide();
                    //$("#<%= pnltextbox.ClientID %>").hide();

                }
            }
            else if (rbText == "BRANCH") {

                if (searchtxt == "Please Select") {
                    alert('Please Select Branch..!');

                }
                else {
                    __doPostBack(btnsearch, rbText + ',' + searchtxt);

                    return true;
                }

            }
            else {
                searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes prevent from postback
                if (e.target.className != "searchtextbox") {
                    if (e.keyCode == 13) { //if this is enter key
                        document.getElementById('<%=btnSearch.ClientID%>').click();
                           e.preventDefault();
                           return true;
                       }
                       else
                           return true;
                   }
                   else
                       return true;
               });
           });

           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               $(function () {
                   $(':text').bind('keydown', function (e) {
                       //on keydown for all textboxes
                       if (e.target.className != "searchtextbox") {
                           if (e.keyCode == 13) { //if this is enter key
                               document.getElementById('<%=btnSearch.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>
</asp:Content>
