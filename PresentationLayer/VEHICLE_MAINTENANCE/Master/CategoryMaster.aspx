<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CategoryMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_CategoryMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
     <div>
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
    </div>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE CATEGORY MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Create Category</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCollege" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            ValidationGroup="Submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College/School Name" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSession" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session." ValidationGroup="Submit" InitialValue="0" SetFocusOnError="true" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Category Name </label>
                                        </div>
                                        <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="50" CssClass="form-control"
                                            ToolTip="Enter Category Name" TabIndex="3" AutoComplete="off"
                                            onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatName" runat="server"
                                            FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers" TargetControlID="txtCategoryName" ValidChars=" ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvCatName" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Category Name."
                                            ValidationGroup="Submit" ControlToValidate="txtCategoryName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="8" CssClass="form-control"
                                            ToolTip="Enter Amount" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server"
                                            FilterType="Numbers,Custom" TargetControlID="txtAmount" ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>


                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="5"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="6"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                    <%-- &nbsp;<asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="6"   Width="80px" onclick="btnRport_Click" Visible="false" /> --%>
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvVCategory" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>CATEGORY LIST</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <%-- <th style="width: 30%">College/School Name
                                                    </th>
                                                    <th style="width: 20%">Session
                                                    </th>--%>
                                                                <th>Category Name
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="Tr1" runat="server" />
                                                        </tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                            CommandArgument='<%# Eval("VCID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" />
                                                    </td>
                                                    <%-- <td style="width: 30%">
                                            <%# Eval("COLLEGE_NAME")%>
                                        </td>--%>
                                                    <%--<td style="width: 20%">
                                            <%# Eval("SESSION_NAME")%>
                                        </td>--%>
                                                    <td>
                                                        <%# Eval("CATEGORYNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
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

