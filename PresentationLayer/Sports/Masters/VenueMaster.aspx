<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VenueMaster.aspx.cs"
    Inherits="Sports_Masters_VenueMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>   --%>
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
                            <h3 class="box-title">VENUE MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                  <asp:Panel ID="pnlDesig" runat="server">
                                             <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Venue Name</label>
                                        </div>
                                         <asp:TextBox ID="txtVenueName" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Venue"
                                                        onkeypress="return CheckAlphabet(event, this);" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvVenue" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Venue Name...!!"
                                                        ValidationGroup="Submit" ControlToValidate="txtVenueName"></asp:RequiredFieldValidator>
                                              
                                     </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Venue Address</label>
                                        </div>
                                           <asp:TextBox ID="txtVenueAddress" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Venue Address"
                                                        Height="48px" TabIndex="2" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Venue Address...!!"
                                                        ValidationGroup="Submit" ControlToValidate="txtVenueAddress"></asp:RequiredFieldValidator>
                                              
                                     </div>
                                  
                                      </div>
                                       <div class=" col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="3" />
                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="4" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
               
                    </div>
                                                   
                                    </asp:Panel>
                                </div>
                                  <div class="col-12">
                                                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvVenue" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading"><h5>VENUE ENTRY LIST</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Venue Name
                                                        </th>
                                                        <th>Venue Address
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
                                                    CommandArgument='<%# Eval("VENUEID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("VENUENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("VENUE_ADDRESS")%>
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

