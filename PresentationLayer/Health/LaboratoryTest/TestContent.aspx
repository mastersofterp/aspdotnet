<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TestContent.aspx.cs" Inherits="Health_LaboratoryTest_TestContent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEST CONTENTS DEFINATION</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlTitle" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Title</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Test Title</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTitle" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Test Title"
                                                OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Select Test Title."
                                                ValidationGroup="Submit" ControlToValidate="ddlTitle" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlTestContents" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add Test Contents</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trGroup" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Group Name</label>
                                            </div>
                                            <asp:TextBox ID="txtGrpName" runat="server" ToolTip="Enter group name" TabIndex="2"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeGrpName" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtGrpName"
                                                InvalidChars="0123456789" ValidChars="()-  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvGrpName" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter group name."
                                                ValidationGroup="Add" ControlToValidate="txtGrpName"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Content Name</label>
                                            </div>
                                            <asp:TextBox ID="txtContent" runat="server" ToolTip="Enter content" TabIndex="3"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeContent" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers" TargetControlID="txtContent"
                                                ValidChars="()-.  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvContent" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter content name."
                                                ValidationGroup="Add" ControlToValidate="txtContent">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Unit</label>
                                            </div>
                                            <asp:TextBox ID="txtUnit" runat="server" ToolTip="Enter Unit Of Content" TabIndex="4"
                                                MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeUnit" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers" TargetControlID="txtUnit" ValidChars="%./\  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvUnit" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter unit of content."
                                                ValidationGroup="Add" ControlToValidate="txtUnit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Normal Range</label>
                                            </div>
                                            <asp:TextBox ID="txtNormalValue" runat="server" ToolTip="Enter Normal value" TabIndex="5"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeNorValues" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers" TargetControlID="txtNormalValue"
                                                ValidChars="%.-/\  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvNormalValue" runat="server" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please enter normal value of content."
                                                ValidationGroup="Add" ControlToValidate="txtNormalValue"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add Contents" ValidationGroup="Add" TabIndex="6"
                                                OnClick="btnAdd_Click" CssClass="btn btn-primary" ToolTip="Click here To Add Contents"
                                                CausesValidation="true" />

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 mt-3">
                                    <asp:Panel ID="pnlTestContentList" runat="server">
                                        <asp:ListView ID="lvContent" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Test Contents Entry List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>

                                                                <th>Test Content
                                                                </th>
                                                                <th>Unit
                                                                </th>
                                                                <th>Normal Range
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                            ImageUrl="~/images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblContentName" runat="server" Text='<%# Eval("CONTENT_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNorValue" runat="server" Text='<%# Eval("NORMAL_RANGE") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="7"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="9" CssClass="btn btn-outline-info"
                                    OnClick="btnRport_Click" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="8"
                                    CssClass="btn btn-outline-danger" ToolTip="CLick here to Reset" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                <asp:ValidationSummary ID="vsAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>

</asp:Content>

