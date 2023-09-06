<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PlanScheduleEvent.aspx.cs" Inherits="Sports_Transaction_PlanScheduleEvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript">
      
    </script>
    <script type="text/javascript">                                               //Saahil Trivedi 14-01-2021
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
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">PLAN & SCHEDULE THE EVENT</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                  <%--  Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Add/Edit Plan & Schedule</div>--%>
                                            <div class="panel panel-body">
                                                <div class="row">
                                                <div class="form-group col-md-3">
                                                    <label><span style="color: #FF0000">*</span>Institute  :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="1"
                                                        AutoPostBack="false" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label><span style="color: #FF0000">*</span>Event Type :</label>
                                                    <asp:DropDownList ID="ddlEventType" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Event Type"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged" TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEventtype" runat="server" ControlToValidate="ddlEventType" Display="None"
                                                        ErrorMessage="Please Select Event Type." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>

                                               

                                                <%-- Modified by Saahil Trivedi 10/1/2022--%>
                                                <div class="form-group col-md-3">
                                                    <label><span style="color: #FF0000">*</span>Event (From Date) :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="img3" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFrmDt" runat="server" ValidationGroup="ScheduleDtl" CssClass="form-control" ToolTip="Select From Date" TabIndex="4"></asp:TextBox>
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
                                                            InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                            ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>


                                                <div class="form-group col-md-3">
                                                    <label><span style="color: #FF0000">*</span>Event(To Date) :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                             <i id="i1" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDt" runat="server" ValidationGroup="ScheduleDtl" CssClass="form-control" ToolTip="Select To Date" TabIndex="5"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDt" Display="None"
                                                            ErrorMessage="Please Enter To Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="i1" TargetControlID="txtToDt"  OnClientDateSelectionChanged="CheckDateEalier">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                            AcceptNegative="Left" ClearMaskOnLostFocus="true" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtToDt">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtToDt"
                                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                            InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                            ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>
</div>
                                                 <div class="row">
                                                <div class="form-group col-md-3">
                                                    <label><span style="color: #FF0000">*</span>Organizing Team :</label>
                                                    <asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Event Organizing Team" TabIndex="6"></asp:DropDownList>
                                                        <%--Modified by Saahil Trivedi 24-02-2022--%>
                                                    <asp:RequiredFieldValidator ID="rfvTeam" runat="server" ControlToValidate="ddlTeam" Display="None"
                                                        ErrorMessage="Please Select Organizing Team." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
                                                
                                                     

                                                <div class="form-group col-md-3">
                                                    <label><span style="color: #FF0000">*</span>Approval Authority Path :</label>
                                                    <asp:TextBox ID="txtAAPath" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                      <div class="form-group col-md-3" id="trEventName" runat="server" visible="false">
                                                    <label><span style="color: #FF0000">*</span>Event Name :</label>
                                                    <asp:DropDownList ID="ddlEvent" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Event" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" TabIndex="3">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ControlToValidate="ddlEvent" Display="None"
                                                        ErrorMessage="Please Select Event." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
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
                            <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="7" />
                            <%--Modified by Saahil Trivedi 08-02-2022--%>
                            &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" Text="Cancel" TabIndex="8" />
                            &nbsp;<asp:Button ID="btnReport" runat="server" CausesValidation="false" OnClick="btnReport_Click" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" TabIndex="9" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            <div class="col-md-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvEvent" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <%--<h4 class="box-title">EVENT ENTRY LIST </h4>--%>
                                                 <div class="sub-heading">
                                                                        <h5>EVENT ENTRY LIST</h5>
                                                                    </div>
                                                <table class="table table-striped table-bordered nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Edit </th>
                                                            <th>Event Name </th>
                                                            <th>From Date </th>
                                                            <th>To Date </th>
                                                            <th>Organizing Team </th>
                                                            <th>Institute </th>
                                                            <th>Status </th>
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
                                                    <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("PSID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("EVENTNAME")%></td>
                                                <td><%# Eval("FROM_DATE", "{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("TO_DATE", "{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("TEAMNAME")%></td>
                                                <td><%# Eval("COLLEGE_NAME")%></td>
                                                <td><%# Eval("ASTATUS")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

