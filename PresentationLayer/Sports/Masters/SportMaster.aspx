<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SportMaster.aspx.cs"
    Inherits="Sports_Masters_SportMaster" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
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
                            <h3 class="box-title">SPORT MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                  <asp:Panel ID="pnlDesig" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Sport Type </label>
                                        </div>
                                         <asp:DropDownList ID="ddlSportType" CssClass="form-control" ToolTip="Select Sport Type"  data-select2-enable="true" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server" ErrorMessage="Please Select Sport Type." ControlToValidate="ddlSportType" InitialValue="0"
                                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                     </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Sport Name</label>
                                        </div>
                                         <asp:TextBox ID="txtSportName" runat="server" MaxLength="30" CssClass="form-control" ToolTip="Enter Sports Name"
                                                      onkeypress="return CheckAlphabet(event, this);" OnTextChanged="txtSportName_TextChanged" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSportName" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Sport Name...!!"
                                                        ValidationGroup="Submit" ControlToValidate="txtSportName"></asp:RequiredFieldValidator>
                                             
                                     </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Number Of Players </label>
                                        </div>
                                         <asp:TextBox ID="txtNoOfplayers" runat="server"  MaxLength="2" CssClass="form-control" ToolTip="Enter No.of Players" onkeypress="return CheckNumeric(event, this);" TabIndex="3"></asp:TextBox>                                                        
                                                    <asp:RequiredFieldValidator ID="rfvNoOfPlayers" ValidationGroup="Submit" ControlToValidate="txtNoOfplayers"
                                                        Display="None" ErrorMessage="Please Enter No. of Players..!!"  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                           
                                     </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>No. Of Reserve Players </label>
                                        </div>
                                         <asp:TextBox ID="txtNoOfReserve" runat="server" MaxLength="2" CssClass="form-control" ToolTip="Enter No.of Reserve Players"
                                                        onkeypress="return CheckNumeric(event, this);" TabIndex="4" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNoOfReserve" Display="None" runat="server"
                                                        SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Please Enter No. of Reserve Players ..!!"  ControlToValidate="txtNoOfReserve"></asp:RequiredFieldValidator>
                                          
                                     </div>
                                   
                                    </div>
                                    </asp:Panel>
                                </div>
                           
                    <div class=" col-12 btn-footer">
                    
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="5" />
                            <asp:Button ID="btnShowReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" TabIndex="7"  />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="6" />
                             <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                      </div>
                        <div class="col-md-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvSport" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading"><h5>SPORTS NAME ENTRY LIST</h5>
                                            </div>
                                           <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Sport Name
                                                        </th>
                                                        <th>No.Of Players
                                                        </th>
                                                        <th>No.Of Reserve Players
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
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                    CommandArgument='<%# Eval("SPID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("SNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("NO_OF_PLAYERS")%>
                                            </td>
                                            <td>
                                                <%# Eval("NO_OF_RESERVE")%>
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

