<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EventDetails.aspx.cs"
    Inherits="Sports_Transaction_EventDetails" Title="" %>

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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SPORTS & TEAM ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Event Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlEventType" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" ToolTip="Select Event Type" AutoPostBack="true" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvET" runat="server" ControlToValidate="ddlEventType" Display="None"
                                                ErrorMessage="Please Select Event Type." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Event Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlEvent" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" ToolTip="Select Event"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ControlToValidate="ddlEvent" Display="None"
                                                ErrorMessage="Please Select Event From The List." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sport Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportType" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control" ToolTip="Select Sport Type" AutoPostBack="true" OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSportType" runat="server" ControlToValidate="ddlSportType" Display="None"
                                                ErrorMessage="Please Select Sport Type." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sport Name  </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportName" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Sport Name" TabIndex="4"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSportName" runat="server" ControlToValidate="ddlSportName" Display="None"
                                                ErrorMessage="Please Select Sport Name." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Team Name  </label>
                                            </div>
                                            <asp:DropDownList ID="ddlTeam" runat="server" data-select2-enable="true" AppendDataBoundItems="True" CssClass="form-control" ToolTip="Select Team" TabIndex="5"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlTeam" Display="None"
                                                ErrorMessage="Please Select Team Name." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="6" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" TabIndex="8" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" TabIndex="7" />
                               
                                 </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvEventDetails" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                    <h5>SPORTS & TEAM ENTRY LIST</h5>
                                                </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Edit
                                                                            </th>
                                                                            <th>Event Name
                                                                            </th>
                                                                            <th>Sport Name
                                                                            </th>
                                                                            <th>Team
                                                                            </th>
                                                                            <th>Institute
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
                                                            CommandArgument='<%# Eval("EDID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("EVENTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TEAMNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COLLEGE_NAME")%>
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

