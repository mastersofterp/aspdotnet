<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RoleMaster.aspx.cs"
    Inherits="Sports_Masters_RoleMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ROLE MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sport Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportType" CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport Type" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged" TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSportType" runat="server" ErrorMessage="Please Select Sport Type." ControlToValidate="ddlSportType" InitialValue="0"
                                                Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sport Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportName" CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport Name" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSportName_SelectedIndexChanged" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddl" runat="server" ErrorMessage="Please Select Sport Name." ControlToValidate="ddlSportName" InitialValue="0"
                                                Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Role Name  </label>
                                            </div>
                                            <asp:TextBox ID="txtRoleName" runat="server" MaxLength="30" CssClass="form-control" ToolTip="Enter Role"
                                                onkeypress="return CheckAlphabet(event, this);" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Role Name...!!"
                                                ValidationGroup="Submit" ControlToValidate="txtRoleName"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class=" col-12 btn-footer">
                      
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click to Submit" CausesValidation="true" TabIndex="4" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="5" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                       </div>
                            <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto" Visible="false">
                                <asp:ListView ID="lvRole" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                           <%-- <h4 class="sub-heading"><h5>ROLE ENTRY LIST</h5>
                                            </h4>--%>
                                              <div class="sub-heading">
                                                    <h5>ROLE ENTRY LIST</h5>
                                                </div>
                                             <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                   <thead class="bg-light-blue">
                                                     <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Sport Name
                                                        </th>
                                                        <th>Role Name
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
                                                    CommandArgument='<%# Eval("ROLEID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("SNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ROLENAME")%>
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
