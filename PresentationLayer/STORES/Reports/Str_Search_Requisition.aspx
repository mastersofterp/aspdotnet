<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Search_Requisition.aspx.cs" Inherits="STORES_Reports_Str_Search_Requisition" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function update(obj) {

            try {
                var mvar = obj.split('¤');
                document.getElementById(mvar[1]).value = mvar[0];
                document.getElementById('ctl00_ctp_hdnId').value = mvar[0] + "  ";
                setTimeout('__doPostBack(\'' + mvar[1] + '\',\'\')', 0);
                //document.forms.submit;
            }
            catch (e) {
                alert(e);
            }
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpanel">
        <ProgressTemplate>
            <div>
                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
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
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Search Requisition</h3>

                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row">
                                        <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                            Visible="false">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <td>
                                                            <img src="../../../Images/error.png" align="absmiddle" alt="Error" />
                                                        </td>
                                                        <td>
                                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                                         </font>
                                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                            Visible="false">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <td>
                                                            <img src="../../../images/confirm.gif" align="absmiddle" alt="confirm" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                            </table>
                                        </asp:Panel>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note:</h5>
                                                <p>
                                                    <i class="fa fa-star" aria-hidden="true"></i><span>Select Department for department wise report otherwise do not select department
                                                    </span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Enter From Date" CssClass="form-control" TabIndex="1" Text=""></asp:TextBox>
                                                <%--  <div class="input-group-addon">
                                                               <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>--%>

                                                <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                    PopupButtonID="imgCal" Enabled="true" EnableViewState="true" BehaviorID="_Fromdate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFromDate"
                                                    Enabled="true" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server" ControlExtender="meQuotDate" ControlToValidate="txtFromDate"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                     
                                                    ValidationGroup="Store" IsValidEmpty="true"> </ajaxToolKit:MaskedEditValidator> <%--SetFocusOnError="True" InvalidValueBlurredMessage="Invalid Date" EmptyValueMessage="Please Select From Date" EmptyValueBlurredText="Empty"  Display="None" --%> 

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgToDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ToolTip="Enter To Date" CssClass="form-control" TabIndex="2" Text=""></asp:TextBox>
                                                <%-- <div class="input-group-addon">
                                                                    <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                                </div>--%>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                    PopupPosition="BottomRight" TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate" ControlToValidate="txtToDate"
                                                   InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    
                                                    ValidationGroup="Store" IsValidEmpty="true"> </ajaxToolKit:MaskedEditValidator><%--SetFocusOnError="True" InvalidValueBlurredMessage="Invalid Date"  EmptyValueMessage="Please Select To Date" EmptyValueBlurredText="Empty"  Display="None" --%>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" data-select2-enable="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="3" ToolTip="Select Department">
                                                <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnRpt_Click" Visible="false"
                                                CssClass="btn btn-primary" TabIndex="4" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="SHOW" OnClick="btnSubmit_Click"
                                                CssClass="btn btn-primary" TabIndex="4" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                CssClass="btn btn-warning" TabIndex="5" ToolTip="Click To Reset" />
                                            <asp:ValidationSummary ID="vsstore" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="Store" />
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlDepartment" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvDepartment" runat="server">
                                        <EmptyDataTemplate>
                                            <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Requisition Details:</h5>
                                            </div>
                                           <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Requisition slip No.
                                                        </th>
                                                        <th>Requisition Date
                                                        </th>
                                                        <th>Requester Person Name
                                                        </th>
                                                        <th>Requisition Status
                                                        </th>
                                                        <th>Requisition For
                                                        </th>
                                                        <th>Print Report
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                
                                                <td>
                                                      <%# Eval("REQ_NO")%>
                                                </td>
                                                <td>
                                                      <%# Eval("REQ_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>
                                                <td>
                                                     <%# Eval("APPROVAL_STATUS")%>
                                                </td>     
                                                <td>
                                                    <%# Eval("REQ_FOR")%>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnEditPartyCategory" runat="server" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("REQTRNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEditPartyCategory_Click" />&nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </div>
                            </asp:Panel>
                        </div>


                    </div>

                </div>

            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>





