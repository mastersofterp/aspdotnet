<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CoAndExtraCurricular_activity.aspx.cs" Inherits="ACADEMIC_CoAndExtraCurricular_activity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" 
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
    </div>--%>
    <%--  <asp:UpdatePanel ID="updSection" runat="server"   >
        <ContentTemplate>--%>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div id="divMsg" runat="server"></div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Co-Curricular and Extra Curricular Activity</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--  <label>College & Scheme</label>--%>
                                    <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                    OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" ValidationGroup="offered" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfid" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="validate">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Session</label>--%>
                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Please Select Session" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="validate"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblNameOfProgram" runat="server" Font-Bold="true"></asp:Label>
                                </div>

                                <asp:TextBox ID="txtProgramName" runat="server" MaxLength="256" ViewStateMode="Enabled" AppendDataBoundItems="True"
                                    TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Name of the program" TabIndex="3" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProgramName"
                                    Display="None" ErrorMessage="Please Enter Name of the program" SetFocusOnError="True"
                                    ValidationGroup="validate" />
                            </div>


                            <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server">
                                <div class="label-dynamic">
                                    <sup id="frmdtspan" runat="server">*</sup>
                                    <label>Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal1" runat="server" class="fa fa-calendar text-green"></i>
                                    </div>
                                    <asp:TextBox ID="txtDate" runat="server" ValidationGroup="Show" onpaste="return false;"
                                        TabIndex="4" ToolTip="Please Enter Date" CssClass="form-control" Style="z-index: 0;" />
                                    <%-- <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                    <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtDate" PopupButtonID="dvcal1" />
                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtDate"
                                        Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"
                                        ValidationGroup="validate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" OnInvalidCssClass="errordate"
                                        TargetControlID="txtDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                    <%-- <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                ControlToValidate="txtDate" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="validate" SetFocusOnError="True" />--%>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblGroupTeacher" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtGroupTeacher" runat="server" CssClass="form-control" MaxLength="150" ToolTip="Enter Group Teacher Details" TabIndex="5" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupTeacher"
                                    Display="None" ErrorMessage="Please Enter Group Teacher Details" SetFocusOnError="True"
                                    ValidationGroup="validate" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblPrincipal" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtPrincipal" runat="server" CssClass="form-control" ToolTip="Enter Principal Details" MaxLength="150" TabIndex="6" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrincipal"
                                    Display="None" ErrorMessage="Please Enter Principal Details" SetFocusOnError="True"
                                    ValidationGroup="validate" />
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" TabIndex="7" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="validate" />
                        <asp:Button ID="btnCoandExtReport" runat="server" TabIndex="8" Text="Report" OnClick="btnCoandExtReport_Click" CssClass="btn btn-primary" CausesValidation="false" />
                        <asp:Button ID="btnCancel" runat="server" TabIndex="9" Text="Cancel" OnClick="btnCancel_Click1" CssClass="btn btn-warning" CausesValidation="false" />

                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="validate" />

                    </div>


                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <asp:ListView ID="lvExtraActivity" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th style="text-align: center;">Edit</th>
                                                <th style="text-align: center;">Remove</th>
                                                <th style="text-align: center;">Scheme</th>
                                                <th style="text-align: center;">Session</th>
                                                <th style="text-align: center;">Name Of The Program</th>
                                                <th style="text-align: center;">Date</th>
                                                <th style="text-align: center;">Group Teacher Details</th>
                                                <th style="text-align: center;">Principal Details</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="btnRemove" runat="server" AlternateText="Delete Record" CausesValidation="false"
                                                CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record" OnClick="btnRemove_Click" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label1" Text='<%# Eval("COL_SCHEME_NAME")%>' runat="server" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label2" Text='<%# Eval("SESSION_NAME")%>' runat="server" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblTopicName" Text='<%# Eval("PROGRAM_NAME")%>' runat="server" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDate" Text='<%# Eval("DATE")%>' runat="server" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblContent" Text='<%# Eval("GROUP_TEACHER")%>' runat="server" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblMapping" Text='<%# Eval("PRINCIPAL_DETAILS")%>' runat="server" />
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
    <%--  </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

