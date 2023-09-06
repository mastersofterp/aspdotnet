<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="complaint_type.aspx.cs" Inherits="Estate_complaint_type"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SERVICE REQUEST CATEGORY TYPE</h3>
                        </div>
                        <div class="box-body">

                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Service Request Category Type</h5>
                                    </div>
                                    <div class="row">
                                     

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartmentName"  runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDepartmentName_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="ddlDepartmentName" Display="None" ErrorMessage="Please Enter Department Name."
                                                ValidationGroup="Complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Category Type</label>
                                            </div>

                                            <asp:TextBox ID="txtComplaintType" runat="server" CssClass="form-control" MaxLength="60" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTye" runat="server" ErrorMessage="Please Enter Service Category Type."
                                                ControlToValidate="txtComplaintType" Display="None" ValidationGroup="Complaint"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtComplaintType" FilterType="Custom, LowercaseLetters,UppercaseLetters"
                                                ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Category Code </label>
                                            </div>
                                            <asp:TextBox ID="txtCatCode" runat="server" CssClass="form-control" MaxLength="5" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCatCode" runat="server" ErrorMessage="Please Enter Service Category Code."
                                                ControlToValidate="txtCatCode" Display="None" ValidationGroup="Complaint"></asp:RequiredFieldValidator>
                                           <%-- <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatCode" runat="server" TargetControlID="txtCatCode" FilterType="LowercaseLetters,UppercaseLetters">
                                            </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Complaint" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="1" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" CssClass="btn btn-warning" TabIndex="1" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server" >
                                        <asp:ListView ID="lvComplaintType" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading"><h5>SERVICE REQUEST CATEGORY TYPE LIST</h5></div>
                                                 
                                                    <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>ACTION</th>
                                                                <th>SERVICE DEPARTMENT</th>
                                                                <th>SERVICE REQUEST CATEGORY TYPE</th>
                                                                <th>CATEGORY TYPE CODE</th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" TabIndex="1"
                                                            CommandArgument='<%# Eval("TYPEID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TYPENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TYPE_CODE")%>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

