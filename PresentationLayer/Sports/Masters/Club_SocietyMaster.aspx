<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Club_SocietyMaster.aspx.cs"
    Inherits="Sports_Masters_Club_SocietyMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
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
                            <h3 class="box-title">CLUB/SOCIETY MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Club Name</label>
                                            </div>
                                             <asp:TextBox ID="txtClubName" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Club Name"
                                                        onkeypress="return CheckAlphabet(event, this);" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvClubName" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Club Name...!!"
                                                        ValidationGroup="Submit" ControlToValidate="txtClubName"></asp:RequiredFieldValidator>
                                                
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Club Address </label>
                                            </div>
                                             <asp:TextBox ID="txtClubAddress" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Club Address" TabIndex="2" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Club Address...!!"
                                                        ValidationGroup="Submit" ControlToValidate="txtClubAddress"></asp:RequiredFieldValidator>
                                             
                                        </div>

                                       
                                </asp:Panel>
                            </div>
                     
                    <div class="col-12 btn-footer">
                        
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="3" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="4" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                  </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvClub" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading"><h5>CLUB/SOCIETY ENTRY LIST</h5>
                                            </div>
                                           <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                               <thead class="bg-light-blue">
                                                 <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Club Name
                                                        </th>
                                                        <th>Club Address
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                    CommandArgument='<%# Eval("CLUBID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("CLUBNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CLUB_ADDRESS")%>
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
