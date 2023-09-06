<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MedalMaster.aspx.cs"
    Inherits="Sports_Masters_MedalMaster" Title="" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>   
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">MEDAL MASTER</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Add/Edit Medal Name</div>--%>
                                            <div class="sub-heading">
                                                    <h5>Add/Edit Medal Name</h5>
                                                </div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Sport Type  :</label>
                                                    <asp:DropDownList ID="ddlSportType" CssClass="form-control" ToolTip="Select Sport Type" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server" ErrorMessage="Please Select Sport Type." ControlToValidate="ddlSportType" InitialValue="0"
                                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Sport Name :</label>
                                                    <asp:DropDownList ID="ddlSportName" runat="server" AppendDataBoundItems="True" CssClass="form-control" ToolTip="Select Sport Name" TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSportName" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Sport Name...!!"
                                                        ValidationGroup="Submit" ControlToValidate="ddlSportName" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span> Medal Name :</label>
                                                    <asp:TextBox ID="txtMedalName" runat="server" MaxLength="30" CssClass="form-control" ToolTip="Enter Medal Name" 
                                                        onkeypress="return CheckAlphabet(event, this);" TabIndex="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMedal" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Medal Name...!!"
                                                        ValidationGroup="Submit" ControlToValidate="txtMedalName"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="4" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="5" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                        </p>
                        <div class="col-md-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvMedal" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                           <%-- <h4 class="box-title">MEDAL ENTRY LIST
                                            </h4>--%>
                                             <div class="sub-heading">
                                                    <h5>MEDAL ENTRY LIST</h5>
                                                </div>
                                            <table class="table table-striped table-bordered nowrap display">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Edit
                                                        </th>
                                                        <th>Sport 
                                                        </th>
                                                        <th>Medal Name
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("MEDALID") %>'  AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("SNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("MEDALNAME")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

