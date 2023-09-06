<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EventMaster.aspx.cs" Inherits="Sports_Masters_EventMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>  --%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EVENT MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="1"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Event Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEventType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Event Type" TabIndex="2"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEventtype" runat="server" ControlToValidate="ddlEventType" Display="None"
                                                ErrorMessage="Please Select Event Type." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Event Name </label>
                                            </div>
                                            <asp:TextBox ID="txtEventName" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Event Name" onkeypress="return CheckAlphabet(event, this);" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEventName" runat="server"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Event Name...!!" ValidationGroup="Submit" ControlToValidate="txtEventName"></asp:RequiredFieldValidator>

                                        </div>




                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="4" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" ToolTip="Click to get Report" CausesValidation="false" TabIndex="6" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="5" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvEvent" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>EVENT ENTRY LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Event Type
                                                            </th>
                                                            <th>Event Name
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
                                                        CommandArgument='<%# Eval("EVENTID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("EVENT_TYPE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EVENTNAME")%>
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

