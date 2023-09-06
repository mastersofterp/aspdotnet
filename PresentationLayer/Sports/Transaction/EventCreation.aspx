<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EventCreation.aspx.cs"
    Inherits="Sports_Transaction_EventCreation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>   --%>
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


 <script type="text/javascript">                                               //Shaikh Juned 24-03-2022
     function CheckDateEalier(sender, args) {
         if (sender._selectedDate < new Date()) {
             alert("Back Dates Are Not Allowed");
             sender._selectedDate = new Date();
             sender._textbox.set_Value("");
         }
     }
    </script>


    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
             <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ORGANIZE THE EVENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                    <asp:Panel ID="pnlDesig" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Academic Year </label>
                                        </div>
                                          <asp:DropDownList ID="ddlAcadYear" runat="server" AppendDataBoundItems="true" TabIndex="1"   CssClass="form-control" data-select2-enable="true" ToolTip="Select Academic Year">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAcadyr" runat="server"
                                                        ControlToValidate="ddlAcadYear" Display="None"
                                                        ErrorMessage="Please Select Academic Year." InitialValue="0"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                     </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Institute </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server"   CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select Institute." ValidationGroup="Submit" SetFocusOnError="true"  InitialValue="0"></asp:RequiredFieldValidator>
                                              
                                        </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Event Name </label>
                                        </div>
                                          <asp:DropDownList ID="ddlEvent" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Event" AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ControlToValidate="ddlEvent"
                                                        Display="None" ErrorMessage="Please Select Event" ValidationGroup="Submit" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                               
                                         </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Sport Type  </label>
                                        </div>
                                           <asp:DropDownList ID="ddlSportType" runat="server"  CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport Type" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSportType" runat="server" ControlToValidate="ddlSportType"
                                                        Display="None" ErrorMessage="Please Select Sport Type" ValidationGroup="Submit" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                               
                                         </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Sport Name   </label>
                                        </div>
                                         <asp:DropDownList ID="ddlSportName" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport Name"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSportName" runat="server"  ControlToValidate="ddlSportName" Display="None"
                                                        ErrorMessage="Please Select Sport Name." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvSNAd" runat="server"  ControlToValidate="ddlSportName" Display="None"
                                                        ErrorMessage="Please Select Sport Name." InitialValue="0"  ValidationGroup="Add"></asp:RequiredFieldValidator>
                                             
                                        </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Venue</label>
                                        </div>
                                           <asp:DropDownList ID="ddlVenue" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" ToolTip="Select Venue" TabIndex="6"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue" Display="None"
                                                        ErrorMessage="Please Select Venue for the Event." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvVNAd" runat="server" ControlToValidate="ddlVenue" Display="None"
                                                        ErrorMessage="Please Select Venue for the Event." InitialValue="0"  ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <asp:ValidationSummary ID="vsAd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                                             
                                         </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Event (From Date) </label>
                                        </div>
                                              <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="img3" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Select From Date" TabIndex="7"></asp:TextBox>
                                                        <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                                            ClearMaskOnLostFocus="true" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDt">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFrmDt" Display="None"
                                                            ErrorMessage="Please Enter From Date." ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrmDt" Display="None"
                                                            ErrorMessage="Please Enter From Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3"
                                                            TargetControlID="txtFrmDt"  OnClientDateSelectionChanged="CheckDateEalier">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                                            ClearMaskOnLostFocus="true" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDt">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtFrmDt"
                                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                            InvalidValueMessage="Please Enter Valid From Date In Format [dd/MM/yyyy] " Display="None" Text="*"
                                                            ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>



                                                    </div>
                                            </div>
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Event (To Date)  </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ToolTip="Select To Date" TabIndex="8"></asp:TextBox>
                                                        <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtToDt">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" ClearMaskOnLostFocus="true" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDt">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDt" Display="None" ErrorMessage="Please Enter To Date."
                                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>

                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDt" Display="None"
                                                            ErrorMessage="Please Enter To Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtToDt"  OnClientDateSelectionChanged="CheckDateEalier">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                            AcceptNegative="Left" ClearMaskOnLostFocus="true" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtToDt">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtToDt"
                                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid To Date In Format [dd/MM/yyyy] "
                                                            InvalidValueMessage="Please Enter Valid To Date In Format [dd/MM/yyyy] " Display="None" Text="*"
                                                            ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>




                                                    </div>
                                          </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Event Details </label>
                                        </div>
                                              <asp:TextBox ID="txtDetails" runat="server" Height="35px" TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Event Details" TabIndex="9"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEventDetails" runat="server" ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Event Details."
                                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                         </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Remark </label>
                                        </div>
                                              <asp:TextBox ID="txtRemark" runat="server" Height="35px" MaxLength="60" TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Remark" TabIndex="10"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark" Display="None" ErrorMessage="Please Enter Remark."
                                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>


           
                                </div>
                                    </asp:Panel>
                                </div>
                        <div class=" col-12 btn-footer">
                       
                            <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" TabIndex="11" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit"  CssClass="btn btn-primary" ToolTip="Click here to Submit"/>
                           <asp:Button ID="btnShowReport" runat="server" Text="Report"  CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" TabIndex="9" />
                             <asp:Button ID="btnCancel" runat="server" CausesValidation="false" TabIndex="12" OnClick="btnCancel_Click" Text="Cancel"  CssClass="btn btn-warning" ToolTip="Click here to Cancel" />
                         
                              <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvEventOrganize" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading"><h5>EVENT ACTIVITY LIST</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Academic Year
                                                        </th>
                                                        <th>Event Name
                                                        </th>
                                                        <th>Sport Name
                                                        </th>
                                                        <th>Venue Name
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th>Institute
                                                        </th>
                                                        <th>Event Details
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
                                                    CommandArgument='<%# Eval("EORGID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("BATCHNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("EVENTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("VENUENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("FROM_DATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("TO_DATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("COLLEGE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("EVENT_DETAILS")%>
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

