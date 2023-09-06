<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LevelConfiguration.aspx.cs" Inherits="LEADMANAGEMENT_Masters_LevelConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();
        }
        function RepeaterDiv() {
            $(document).ready(function () {
                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });
        }
    </script>
    <%-- <script src="../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="upLevelConfiguration" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="upLevelConfiguration" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header with-border">
                    <span class="glyphicon glyphicon-detail text-blue"></span>
                    <h3 class="box-title"><b>ENQUIRY ALLOTMENT</b></h3>
                    <asp:Label ID="Label2" runat="server"><span  style="padding-left: 650px; color: Red; font-weight: bold;">Note : * marked fields are Mandatory</span></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <form role="form">
                                    <div id="divMsg" runat="server">
                                    </div>
                                    <div class="box-body">
                                        <fieldset>
                                            <legend>Level Configuration
                                  <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                                            </legend>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-4">
                                                                <label><span style="color: red">*</span>User Name</label>

                                                            </div>
                                                            <div class="form-group col-md-8">
                                                                <asp:DropDownList ID="ddlUserName" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select User Name" runat="server" OnSelectedIndexChanged="ddlUserName_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlUserName"
                                                                    Display="None" ErrorMessage="Please Select User Name" InitialValue="0" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="form-group col-md-4">
                                                                <label><span style="color: red">*</span>Reminder Day</label>
                                                            </div>
                                                            <div class="form-group col-md-8">
                                                                <asp:TextBox ID="txtReminderDay" CssClass="form-control" runat="server" TabIndex="1" MaxLength="50"
                                                                    ToolTip="Please Enter Level No." />
                                                                <asp:RequiredFieldValidator ID="rfvReminderDay" runat="server" ControlToValidate="txtReminderDay"
                                                                    Display="None" ErrorMessage="Please Enter Reminder Day" InitialValue="" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteReminderDay" runat="server" ValidChars="0123456789"
                                                                    TargetControlID="txtReminderDay">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-4">
                                                                <label><span style="color: red">*</span>Sequence No.</label>
                                                            </div>
                                                            <div class="form-group col-md-8">
                                                                <asp:DropDownList ID="ddlSequenceNo" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Sequence No." runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvLevelNo" runat="server" ControlToValidate="ddlSequenceNo"
                                                                    Display="None" ErrorMessage="Please Select Sequence No." InitialValue="0" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="panel-group">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">
                                                                            <h4 class="panel-title">Level 1<%--<a data-toggle="collapse" href="#collapse1" class="toggle-btn pull-right"><i class="fa-plus fa"></i></a>--%>
                                                                            </h4>
                                                                        </div>
                                                                        <div id="collapse1" runat="server" class="panel-collapse collapse">
                                                                            <div class="panel-body">
                                                                                <div class="form-group col-md-4">
                                                                                    <label>User Name</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:DropDownList ID="ddlUserNameLevel1" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select User Name" runat="server" OnSelectedIndexChanged="ddlUserNameLevel1_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>

                                                                                <div class="form-group col-md-4">
                                                                                    <label>Reminder Day</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:TextBox ID="txtReminderDayLevel1" CssClass="form-control" runat="server" TabIndex="1" MaxLength="50"
                                                                                        ToolTip="Please Enter Reminder Day" />
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbReminderDayLevel1" runat="server" ValidChars="0123456789"
                                                                                        TargetControlID="txtReminderDayLevel1">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReminderDayLevel1"
                                                                                        Display="None" ErrorMessage="Please Enter Level 1 Reminder Day" InitialValue="" ValidationGroup="submit"
                                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="panel-group">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">
                                                                            <h4 class="panel-title">Level 2<%--<a data-toggle="collapse" href="#collapse2" class="toggle-btn pull-right"><i class="fa-plus fa"></i></a>--%>
                                                                            </h4>
                                                                        </div>
                                                                        <div id="collapse2" runat="server" class="panel-collapse collapse">
                                                                            <div class="panel-body">
                                                                                <div class="form-group col-md-4">
                                                                                    <label>User Name</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:DropDownList ID="ddlUserNameLevel2" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select User Name" runat="server" OnSelectedIndexChanged="ddlUserNameLevel2_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>

                                                                                <div class="form-group col-md-4">
                                                                                    <label>Reminder Day</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:TextBox ID="txtReminderDayLevel2" CssClass="form-control" runat="server" TabIndex="1" MaxLength="50"
                                                                                        ToolTip="Please Enter Reminder Day" />
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                                                                        TargetControlID="txtReminderDayLevel2">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReminderDayLevel2"
                                                                                        Display="None" ErrorMessage="Please Enter Level 2 Reminder Day" InitialValue="" ValidationGroup="submit"
                                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <div class="panel-group">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">
                                                                            <h4 class="panel-title">Level 3<%--<a data-toggle="collapse" href="#collapse3" class="toggle-btn pull-right"><i class="fa-plus fa"></i></a>--%>
                                                                            </h4>
                                                                        </div>
                                                                        <div id="collapse3" runat="server" class="panel-collapse collapse">
                                                                            <div class="panel-body">
                                                                                <div class="form-group col-md-4">
                                                                                    <label>User Name</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:DropDownList ID="ddlUserNameLevel3" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select User Name" runat="server" OnSelectedIndexChanged="ddlUserNameLevel3_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>

                                                                                <div class="form-group col-md-4">
                                                                                    <label>Reminder Day</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:TextBox ID="txtReminderDayLevel3" CssClass="form-control" runat="server" TabIndex="1" MaxLength="50"
                                                                                        ToolTip="Please Enter Reminder Day" />
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789"
                                                                                        TargetControlID="txtReminderDayLevel3">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReminderDayLevel3"
                                                                                        Display="None" ErrorMessage="Please Enter Level 3 Reminder Day" InitialValue="" ValidationGroup="submit"
                                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="panel-group">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">
                                                                            <h4 class="panel-title">Level 4<%--<a data-toggle="collapse" href="#collapse4" class="toggle-btn pull-right"><i class="fa-plus fa"></i></a>--%>
                                                                            </h4>
                                                                        </div>
                                                                        <div id="collapse4" runat="server" class="panel-collapse collapse">
                                                                            <div class="panel-body">
                                                                                <div class="form-group col-md-4">
                                                                                    <label>User Name</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:DropDownList ID="ddlUserNameLevel4" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select User Name" runat="server" OnSelectedIndexChanged="ddlUserNameLevel4_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>

                                                                                <div class="form-group col-md-4">
                                                                                    <label>Reminder Day</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:TextBox ID="txtReminderDayLevel4" CssClass="form-control" runat="server" TabIndex="1" MaxLength="50"
                                                                                        ToolTip="Please Enter Reminder Day" />
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789"
                                                                                        TargetControlID="txtReminderDayLevel4">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReminderDayLevel4"
                                                                                        Display="None" ErrorMessage="Please Enter Level 4 Reminder Day" InitialValue="" ValidationGroup="submit"
                                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <div class="panel-group">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">
                                                                            <h4 class="panel-title">Level 5<%--<a data-toggle="collapse" href="#collapse5" class="toggle-btn pull-right"><i class="fa-plus fa"></i></a>--%>
                                                                            </h4>
                                                                        </div>
                                                                        <div id="collapse5" runat="server" class="panel-collapse collapse">
                                                                            <%--panel-collapse collapse--%>
                                                                            <div class="panel-body">
                                                                                <div class="form-group col-md-4">
                                                                                    <label>User Name</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:DropDownList ID="ddlUserNameLevel5" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select User Name" runat="server" OnSelectedIndexChanged="ddlUserNameLevel5_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>

                                                                                <div class="form-group col-md-4">
                                                                                    <label>Reminder Day</label>
                                                                                </div>
                                                                                <div class="form-group col-md-8">
                                                                                    <asp:TextBox ID="txtReminderDayLevel5" CssClass="form-control" runat="server" TabIndex="1" MaxLength="50"
                                                                                        ToolTip="Please Enter Reminder Day" />
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtReminderDayLevel5" runat="server" ValidChars="0123456789"
                                                                                        TargetControlID="txtReminderDayLevel5">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtReminderDayLevel5"
                                                                                        Display="None" ErrorMessage="Please Enter Level 5 Reminder Day" InitialValue="" ValidationGroup="submit"
                                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </form>
                                <div class="box-footer">
                                    <p class="text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                            CssClass="btn btn-success" OnClick="btnSave_Click" TabIndex="3" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click" TabIndex="4" />
                                        <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click"
                                            TabIndex="5" Text="Excel Report" ToolTip="Excel Report" CssClass="btn btn-info" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        <asp:HiddenField ID="hdfLC_NO" runat="server" />
                                        <asp:HiddenField ID="hdfLC_NO1" runat="server" />
                                        <asp:HiddenField ID="hdfLC_NO2" runat="server" />
                                        <asp:HiddenField ID="hdfLC_NO3" runat="server" />
                                        <asp:HiddenField ID="hdfLC_NO4" runat="server" />
                                        <asp:HiddenField ID="hdfLC_NO5" runat="server" />

                                    </p>

                                    <%-- <div class="col-md-12" style="display:none">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:Repeater ID="rptLevelConfiguration" runat="server">
                                        <HeaderTemplate>            
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Level No
                                                        </th>
                                                        <th>User Name
                                                        </th>
                                                        <th>Reminder Day
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LC_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                </td>
                                                <td>
                                                  <%# Eval("LEVELNO")%>
                                                </td>
                                                <td>
                                                  <%# Eval("UA_FULLNAME")%>
                                                </td>
                                                <td>
                                                   <%# Eval("REMINDER_DAY")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                </table>
                            </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

