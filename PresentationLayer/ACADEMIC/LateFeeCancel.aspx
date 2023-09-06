<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LateFeeCancel.aspx.cs" Inherits="ACADEMIC_LateFeeCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<style type="text/css">
        .list-group-item b {
            width: 200px;
            display: inline-block;
        }

        .list-group-item span {
            font-size: 14px!important;
            font-weight: 500;
        }

        .list-group-item input {
            font-size: 14px;
            font-weight: 500;
        }

        #ctl00_ContentPlaceHolder1_txtSearchText {
            z-index: 0;
        }
    </style>--%>

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Late Fee Cancellation</h3>--%>
                             <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:RadioButton ID="rdoCancelLateFee" runat="server" Text="Cancel Late Fees" AutoPostBack="true" OnCheckedChanged="rdoLateFee_CheckedChanged" GroupName="Chalan" Checked="true" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoModifyLateFee" runat="server" Text="Modify Late Fees" AutoPostBack="true" OnCheckedChanged="rdoModifyLateFee_CheckedChanged" GroupName="Chalan" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Search Challan</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Reg No.</label>
                                        </div>
                                        <asp:TextBox ID="txtSearchText" runat="server" ToolTip="Enter text to search." CssClass="form-control" />
                                        <span class="input-group-btn"></span>
                                        <%-- </div>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRecType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlRecType" runat="server" SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlRecType"
                                            Display="None" ErrorMessage="Please Select Receipt Type" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                        <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                            ValidationGroup="search" CssClass="btn-primary btn"></asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                            Display="None" ErrorMessage="Please Enter Registration No. To Search." SetFocusOnError="true"
                                            ValidationGroup="search" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="search" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divStud" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Student Information</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="divInfo" runat="server">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Reg. No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree / Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdfDegree" runat="server" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvSearchResults" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>Late Fee Details</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" Text="Select/Select all" onclick="SelectAll(this,1,'chkRegister');" />
                                                        </th>
                                                        <th>Receipt Title
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Total Late Fees
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <asp:CheckBox ID="chkRegister" runat="server" ToolTip='<%# Eval("DM_NO") %>' onclick="ChkHeader(this,'cbHead','chkRegister');" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblReceipttype" runat="server" Text='<%# Eval("RECIEPT_TITLE") %>' ToolTip='<%# Eval("FEE_HEAD") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLateFee" runat="server" Text='<%# Eval("LATEFEE")%>' Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12 mt-4" id="divRemark" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Reason For Cancelling Late Fees </label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" Rows="2" TextMode="MultiLine" MaxLength="400"
                                            runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <input id="btnCanLateFee" onclick="DeleteLateFee();" runat="server" type="button"
                                    value="Cancel Late Fee" disabled="disabled" class="btn btn-primary" />
                                <%--<asp:Button ID="btnCancel" runat="server" Text="Clear" />--%>
                                <asp:Button ID="btnModify" runat="server" Text="Modify Late Fees" OnClick="btnModify_Click" OnClientClick="return ValidModifyFees();" CssClass="btn btn-primary" Enabled="false" Visible="false" ValidationGroup="search" />
                                <input id="btnCancel" runat="server" type="button"
                                    value="Clear" onclick="Clear();" class="btn btn-warning" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" lang="javascript">

        function DeleteLateFee() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("DeleteLateFee", "");
                }
                else {
                    alert("Please select demand to Cancel Late Fees.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        function Clear() {
            try {
                __doPostBack("Clear", "");
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        function DeleteChalan() {
            try {
                if (ValidateRecordSelection()) {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                    if (confirm("If you delete this chalan, it will not be appear in the system.\n\nAre you sure you want to delete this chalan?")) {
                        __doPostBack("DeleteChalan", "");
                    }
                }
                else
                    alert('Please enter reason of deleting Late Fee.');
            }
            else {
                alert("Please select a demand to delete.");
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function ValidModifyFees() {
        if (ValidateRecordSelection()) {
            return true;
        }
        else {
            alert("Please select a demand to Modify Late Fees.");
            return false;
        }
    }

    function ShowRemark(rdoSelect) {
        try {
            if (rdoSelect != null && rdoSelect.nextSibling != null) {
                var hidRemark = rdoSelect.nextSibling;
                if (hidRemark != null)
                   <%-- document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;--%>
                    document.getElementById('<%= txtRemark.ClientID %>').value = "";
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function ValidateRecordSelection() {
        var isValid = false;
        try {
            var tbl = document.getElementById('tblSearchResults');
            var dataRows = tbl.getElementsByTagName('tr');
            // if (tbl != null && tbl.rows && tbl.rows.length > 0) {
            //for (i = 1; i < tbl.rows.length; i++) {
            //    var dataRow = tbl.rows[i];
            //    var dataCell = dataRow.firstChild;
            //    var rdo = dataCell.firstChild;
            //    if (rdo.checked) {
            //        isValid = true;
            //    }
            //}  //ctl00_ContentPlaceHolder1_lvSearchResults_ctrl0_chkRegister
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    var chkid = document.getElementById('ctl00_ContentPlaceHolder1_lvSearchResults_ctrl' + i + '_chkRegister');
                    if (chkid.checked)
                        isValid = true;
                }
            }
            //}
        }
        catch (e) {
            alert("Error: " + e.description);
        }
        return isValid;
    }

    function SelectAll(headerid, headid, chk) {
        var tbl = '';
        var list = '';

        tbl = document.getElementById('tblSearchResults');
        list = 'lvSearchResults';

        try {
            var dataRows = tbl.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    var chkid = 'ctl00_ContentPlaceHolder1_lvSearchResults_ctrl' + i + '_' + chk;
                    if (headerid.checked) {
                        document.getElementById(chkid).checked = true;
                    }
                    else {
                        document.getElementById(chkid).checked = false;
                    }
                    chkid = '';
                }
            }
        }
        catch (e) {
            alert(e);
        }
    }

    function ChkHeader(chklst, head, chk) {
        try {
            var headid = '';
            var tbl = '';
            var chkcnt = 0;

            tbl = document.getElementById('tblSearchResults');
            var dataRows = tbl.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    var chkid = document.getElementById('ctl00_ContentPlaceHolder1_lvSearchResults_ctrl' + i + '_' + chk);
                    if (chkid.checked)
                        chkcnt++;
                }
            }
            // var chkHdr = $('#ctl00_ContentPlaceHolder1_lvSearchResults_cbHead');
            var chkHdr = 'ctl00_ContentPlaceHolder1_lvSearchResults_cbHead';
            if ((dataRows.length - 1) === chkcnt) {
                document.getElementById(chkHdr).checked = true;
            }
            else
                document.getElementById(chkHdr).checked = false;


        }
        catch (e) {
            alert(e);
        }
    }

    </script>

</asp:Content>

