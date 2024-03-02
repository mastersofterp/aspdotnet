<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DocumentMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_DocumentMaster" Title="" %>


<%-----------------------------------------------------------------------------------------------------------------------------
Created By  : 
Created On  : 
Purpose     :  
Version     : 
---------------------------------------------------------------------------------------------------------------------------
Version     Modified On     Modified By       Purpose
---------------------------------------------------------------------------------------------------------------------------
1.0.1       28-02-2024      Anurag Baghele    [53807]-Make two tabs for Document Name and Document Mapping
------------------------------------------- ---------------------------------------------------------------------------------%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdMandatory" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive_tab1" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdfDoc_Id" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDocument"
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

    <style>
        .switch.madtory label {
            width: 140px !important;
        }

        .switch.madtory input:checked + label:after {
            transform: translateX(128px);
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">

                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Document Name</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Document Mapping</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">

                                <div class="box-body">
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Document Name </label>
                                                    </div>
                                                    <asp:TextBox ID="txtDocumentName_tab1" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Please Enter Document Name" MaxLength="256" />
                                                    <asp:RequiredFieldValidator ID="rfvDocumentName_tab1" runat="server" ControlToValidate="txtDocumentName_tab1"
                                                        Display="None" ErrorMessage="Please Enter Document Name" ValidationGroup="ValidationSummary_tab1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Active Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdActive_tab1" name="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="rdActive_tab1"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit_tab1" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="5" OnClick="btnSubmit_tab1_Click" OnClientClick="return validate_tab1();" ValidationGroup="ValidationSummary_tab1" />

                                    <asp:Button ID="btnCancel_tab1" runat="server" Text="Cancel" TabIndex="7" CausesValidation="False" CssClass="btn btn-warning" OnClick="btnCancel_tab1_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ValidationSummary_tab1" />
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvDoc" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Documents</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Document Name</th>
                                                            <th>Active Status</th>
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
                                                <td align="center">
                                                    <asp:ImageButton ID="btnEdit_tab1" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_tab1_Click" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <%# Eval("Docname")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblactinestatus_tab1" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                            </div>

                            <div class="tab-pane" id="tab_2">

                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="updDocument" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Document Name </label>
                                                            </div>

                                                            <asp:DropDownList ID="ddlDocumentName" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                                ToolTip="Please Select Document Name" AutoPostBack="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDocumentName" runat="server" ControlToValidate="ddlDocumentName"
                                                                Display="None" ErrorMessage="Please Select Document Name" InitialValue="0" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="blDYddlAdmType" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmType" runat="server" AppendDataBoundItems="true" ToolTip="Please Select Admission Type"
                                                                CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvAdmType" runat="server" ControlToValidate="ddlAdmType"
                                                                Display="None" ErrorMessage="Please Select Admission Type" InitialValue="0"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Document Sr. No. </label>
                                                            </div>
                                                            <asp:TextBox ID="txtDocumentSrNo" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Please Select Document Sr. No."
                                                                MaxLength="3" onkeyup="validateNumeric(this);" />
                                                            <asp:RequiredFieldValidator ID="rfvDocumentSrNo" runat="server" ControlToValidate="txtDocumentSrNo"
                                                                Display="None" ErrorMessage="Please Enter Document Sr. No." ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12 h-100" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Nationality </label>
                                            </div>
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBox ID="chkNationality" runat="server" Text="All Nationality" onclick="SelectAllNationality()" />
                                                <asp:CheckBoxList ID="chkNationalityList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 h-100">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Country Category </label>
                                            </div>

                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBox ID="chkCountryCategory" runat="server" Text="All Category" onclick="SelectCountryCategory()" />
                                                <asp:CheckBoxList ID="chkCountryCategoryList" RepeatColumns="2" runat="server" RepeatDirection="Horizontal" Width="100%" ToolTip="Please Select Country Category">
                                                    <asp:ListItem Value="1">Indian</asp:ListItem>
                                                    <asp:ListItem Value="2">Foreign National (FN)</asp:ListItem>
                                                    <asp:ListItem Value="3">Non-Resident Indian (NRI)</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 h-100" runat="server" id="divCasteCategory">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Caste Category </label>
                                            </div>
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBox ID="chkCategory" runat="server" Text="All Category" onclick="SelectAllCategory()" />
                                                <asp:CheckBoxList ID="chkCategoryList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 h-100" runat="server" id="divAdmCategory" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Category </label>
                                            </div>
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBox ID="chkAdmCategory" runat="server" Text="All Category" onclick="SelectAllAdmCategory()" />
                                                <asp:CheckBoxList ID="chkAdmCategoryList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Mandatory Status</label>
                                            </div>
                                            <div class="switch form-inline madtory">
                                                <input type="checkbox" id="rdMandatory" name="switch" checked />
                                                <label data-on="Mandatory" data-off="NonMandatory" for="rdMandatory"></label>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="row">
                                                <div class="form-group col-6">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Active Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="5" ValidationGroup="submit" OnClientClick="return validate();" />

                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="6" CssClass="btn btn-info" Visible="false" OnClick="btnReport_Click" CausesValidation="false" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="7" CausesValidation="False" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvBatchName" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Document List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Document Name
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="blDYddlAdmType" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>Sr. No.
                                                            </th>
                                                            <th>Active Status
                                                            </th>
                                                            <th align="center">Doc Status
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
                                                <td align="center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DOCUMENTNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <%# Eval("Docname")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ID_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SR_NO")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblactinestatus" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldocstatus" runat="server" Text='<%# Eval("MANDATORY")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatMandat(val) {
            $('#rdMandatory').prop('checked', val);
        }
        //Added by Anurag Baghele on 28-02-2024 
        function SetStatActive_Tab1(val) {
            $('#rdActive_tab1').prop('checked', val);
        }
        //End by Anurag Baghele

        function validate() {
            if (Page_ClientValidate('submit')) {
                $('#hfdMandatory').val($('#rdMandatory').prop('checked'));
                $('#hfdActive').val($('#rdActive').prop('checked'));
                return true;
            } else {
                return false;
            }
        }

        //Added by Anurag Baghele on 28-02-2024 
        function validate_tab1() {
            if (Page_ClientValidate('ValidationSummary_tab1')) {
                $('#hfdActive_tab1').val($('#rdActive_tab1').prop('checked'));
                return true;
            } else {
                return false;
            }
        }
        //End by Anurag Baghele

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    validate();
                });

                $('#btnSubmit_tab1').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }


        function SelectAllCategory() {

            var CHK = document.getElementById("<%=chkCategoryList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");


            var chkBranch = document.getElementById('ctl00_ContentPlaceHolder1_chkCategory');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkCategoryList_' + i);
                if (chkBranch.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }


        function SelectAllNationality() {

            var CHK = document.getElementById("<%=chkNationalityList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");


            var chkNationality = document.getElementById('ctl00_ContentPlaceHolder1_chkNationality');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkNationalityList_' + i);
                if (chkNationality.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }
    </script>

    <script>
        function SelectAllAdmCategory() {

            var CHK = document.getElementById("<%=chkAdmCategoryList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");


            var chkBranch = document.getElementById('ctl00_ContentPlaceHolder1_chkAdmCategory');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkAdmCategoryList_' + i);
                if (chkBranch.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }

        //Added by Anurag Baghele on 29-02-2024 
        function SelectCountryCategory() {
            var CHK = document.getElementById("<%=chkCountryCategoryList.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");

                var chkBranch = document.getElementById('ctl00_ContentPlaceHolder1_chkCountryCategory');

                for (var i = 0; i < checkbox.length; i++) {
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkCountryCategoryList_' + i);
                    if (chkBranch.checked == true) {
                        chk.checked = true;
                    } else {
                        chk.checked = false;
                    }
                }
            }
        //End by Anurag Baghele
    </script>

    <script>
        function TabShow(tabName)
        {
            $('.nav-tabs a[href="#' + tabName + '"]').tab('show');
        }
    </script>

</asp:Content>

