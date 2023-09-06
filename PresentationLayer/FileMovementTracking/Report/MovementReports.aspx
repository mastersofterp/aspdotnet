<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MovementReports.aspx.cs" Inherits="FileMovementTracking_Report_MovementReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlList .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FILE MOVEMENT REPORTS</h3>
                        </div>
                        <div class="box-body">

                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria For File Movement Report</h5>
                                    </div>


                                    <div class="row">
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Report Status</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReport" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" ToolTip="Select Report Status">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Pending Files</asp:ListItem>
                                                <asp:ListItem Value="2">In Process Files</asp:ListItem>
                                                <asp:ListItem Value="3">Complete Files</asp:ListItem>
                                                <asp:ListItem Value="4">File Movement Details</asp:ListItem>
                                                <asp:ListItem Value="5">Forward</asp:ListItem>
                                                <asp:ListItem Value="6">Return</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvReport" runat="server" ErrorMessage="Please Select Report."
                                                                     ControlToValidate="ddlReport" InitialValue="0" 
                                                             Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator> --%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="DIVSEC" runat="server" >
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Section</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSection" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Section">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvddlSec" runat="server" ErrorMessage="Please Select Section." ControlToValidate="ddlSection" InitialValue="-1" 
                                                             Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>      --%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="DIVUSER" runat="server"  visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Section</label>
                                            </div>
                                            <asp:DropDownList ID="ddluser" CssClass="form-control" runat="server" TabIndex="2" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Section">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvddlSec" runat="server" ErrorMessage="Please Select Section." ControlToValidate="ddlSection" InitialValue="-1" 
                                                             Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>      --%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Fdate" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImgBntCalc" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                    TabIndex="1"> </asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender> 

                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server"  ControlExtender="medt" ControlToValidate="txtFDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid From Date In Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid From Date In Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Report"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id ="Tdate" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date</label>
                                            </div>

                                            <div class="input-group date" >
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                    TabIndex="1"> </asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTDate" PopupButtonID="Image1">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtTDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid To Date In Format [dd/MM/yyyy] " ControlToValidate="txtTDate"
                                                    InvalidValueMessage="Please Enter Valid To Date In Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Report">
                                                </ajaxToolKit:MaskedEditValidator>

                                                 

                                                
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" TabIndex="1" ValidationGroup="Report" OnClick="btnShow_Click"
                                    CssClass="btn btn-info" CausesValidation="true" ToolTip="Click here to Show Report" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" CausesValidation="false" ToolTip="Click here to Reset" />

                                &nbsp;<asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" 
                                    ShowSummary="false" ValidationGroup="Report" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvFile" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>List Of Files</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>FILE NO.
                                                            </th>
                                                            <th>FILE NAME
                                                            </th>
                                                            <th>FILE PATH
                                                            </th>
                                                            <th>CREATOR NAME
                                                            </th>
                                                            <th>CREATION DATE
                                                            </th>
                                                            <th>DOCUMENT TYPE
                                                            </th>
                                                            <th>STATUS
                                                            </th>
                                                            <th>MOVEMENT STATUS
                                                            </th>
                                                            <th>DETAILS
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
                                                    <%# Eval("FILE_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILEPATH") %>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CREATION_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DOCUMENT_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS").ToString() == "0" ? "ACTIVE": "INACTIVE" %>
                                                </td>
                                                <td>
                                                    <%# Eval("MOVEMENT_STATUS")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDetails" runat="server" Text="Details" CommandName='<%# Eval("FILE_ID") %>'
                                                        OnClick="btnDetails_Click" CssClass="btn btn-primary" />
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

