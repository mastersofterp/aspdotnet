<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DocumentMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_DocumentMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdMandatory" runat="server" ClientIDMode="Static" />
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
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <asp:UpdatePanel ID="updDocument" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Degree </label>--%>
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
                                                    <%--<label>Admission Type </label>--%>
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
                                                    <label>Document Name </label>
                                                </div>
                                                <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Please Select Document Name"
                                                    MaxLength="256" />
                                                <asp:RequiredFieldValidator ID="rfvDocumentName" runat="server" ControlToValidate="txtDocumentName"
                                                    Display="None" ErrorMessage="Please Enter Document Name" ValidationGroup="submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                    <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12 h-100">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Nationality </label>
                                </div>
                                <div class="form-group col-md-12 checkbox-list-box">
                                    <asp:CheckBox ID="chkNationality" runat="server" Text="All Nationality" onclick="SelectAllNationality()" />
                                    <asp:CheckBoxList ID="chkNationalityList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                    </asp:CheckBoxList>
                                </div>
                                <%-- <asp:DropDownList ID="ddlNationality" CssClass="form-control" runat="server" TabIndex="9" AppendDataBoundItems="True"
                                            ToolTip="Please Select Nationality" />
                                        <asp:RequiredFieldValidator ID="rfvddlNationality" runat="server" ControlToValidate="ddlNationality"
                                            Display="None" ErrorMessage="Please Select Nationality" SetFocusOnError="True"
                                            ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
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
                        <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="5" OnClientClick="return validate();" ValidationGroup="submit" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="6" CssClass="btn btn-info" Visible="false"
                            OnClick="btnReport_Click" CausesValidation="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="7"
                            CausesValidation="False" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
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
                                        <%# Eval("DOCUMENTNAME")%>
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

        function validate() {
            if (Page_ClientValidate()) {
                $('#hfdMandatory').val($('#rdMandatory').prop('checked'));
                $('#hfdActive').val($('#rdActive').prop('checked'));
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
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
    </script>
</asp:Content>

