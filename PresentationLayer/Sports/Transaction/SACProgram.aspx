<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SACProgram.aspx.cs"
    Inherits="Sports_Transaction_SACProgram" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
     <script type="text/javascript">                                                   <%--Modified by Saahil Trivedi 24-02-2022--%>
         function CheckDateEalier(sender, args) {
             if (sender._selectedDate < new Date()) {
                 alert("Back Dates Are Not Allowed");
                 sender._selectedDate = new Date();
                 sender._textbox.set_Value("");
             }
         }
    </script>
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
                            <h3 class="box-title">SAC PROGRAM</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Event Name </label>
                                        </div>
                                          <asp:DropDownList ID="ddlEvent" CssClass="form-control" data-select2-enable="true" ToolTip="Select Event" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ErrorMessage="Please Select Event Name." ControlToValidate="ddlEvent" InitialValue="0"
                                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                              
                                     </div>
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>From Date  </label>
                                        </div>
                                              <%--Modified by Saahil Trivedi 24-02-2022--%>
                                         <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="img3" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Select From Date" ValidationGroup="ScheduleDtl"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt" OnClientDateSelectionChanged="CheckDateEalier">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDt" ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ErrorMessage="Please Enter From Date." ControlToValidate="txtFrmDt"
                                                            Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>
                                          </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>To Date </label>
                                        </div>
                                             <%--Modified by Saahil Trivedi 24-02-2022--%>
                                         <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                   
                                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ToolTip="Select To Date" ValidationGroup="ScheduleDtl"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtToDt" OnClientDateSelectionChanged="CheckDateEalier">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            OnInvalidCssClass="errordate" TargetControlID="txtToDt" ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ErrorMessage="Please Enter To Date." ControlToValidate="txtToDt"
                                                            Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>
                                          </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Club/Society </label>
                                        </div>
                                          <asp:DropDownList ID="ddlClub" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Club" AppendDataBoundItems="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvClub" runat="server" ErrorMessage="Please Select Club Name." ControlToValidate="ddlClub" InitialValue="0"
                                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            
                                          </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Venue</label>
                                        </div>
                                          <asp:DropDownList ID="ddlVenue" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Venue" AppendDataBoundItems="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVenue" runat="server"
                                                        ErrorMessage="Please Select Venue." ControlToValidate="ddlVenue" InitialValue="0"
                                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                             
                                          </div>
                                   </div>
                                    </asp:Panel>
                                </div>
                        
                         <div class=" col-12 btn-footer">
                       
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"  ShowSummary="false" ValidationGroup="Submit" />
                   </div>

                        <div class="col-12">
                            <asp:Panel ID="pnlView" runat="server" ScrollBars="Auto">
                                 <asp:ListView ID="lvSAC" runat="server">
                                      <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading"><h5>SAC PROGRAM ENTRY LIST</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>EDIT
                                                        </th>
                                                        <th>EVENT NAME
                                                        </th>
                                                        <th>FROM DATE
                                                        </th>
                                                        <th>TO DATE
                                                        </th>
                                                        <th>CLUB/SOCIETY NAME
                                                        </th>
                                                        <th>VENUE
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
                                                        CommandArgument='<%# Eval("SACID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("EVENTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SAC_FROM_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SAC_TO_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CLUBNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("VENUENAME")%>
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
