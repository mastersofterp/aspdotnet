<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="str_purchase_summary.aspx.cs" Inherits="STORES_Reports_str_purchase_summary"
    Title="Untitled Page" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../jquery/jquery-1.3.2.min.js" type="text/javascript"></script>--%>

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
    <script type="text/javascript">

        function chk() {
            var From_date = document.getElementById('<%=txtTodt.ClientID%>').value;
            var To_date = document.getElementById('<%=txtTodt.ClientID%>').value;
            var F_date = new Date(From_date);
            var T_date = new Date(To_date);


        }
    </script>





    <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpanel">
        <ProgressTemplate>
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none;
                background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />--%>
    <%--Progressing Request....  --%>
    <%--</td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--<asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>--%>
    <asp:Panel runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">PURCHASE SUMMARY</h3>

                    </div>
                    <div class="box-body">
                        <asp:Panel ID="pnlStrConfig" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <%--<div class="sub-heading">
                                        <h5>PURCHASE SUMMARY REPORT</h5>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCal">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="1" />

                                            <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrmDt"
                                                PopupButtonID="imgCal" Enabled="true" EnableViewState="true" BehaviorID="_Fromdate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFrmDt"
                                                Enabled="true" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true">
                                            </ajaxToolKit:MaskedEditExtender>

                                            <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server" ControlExtender="meQuotDate" ControlToValidate="txtFrmDt"
                                                EmptyValueMessage="Please Select From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                ValidationGroup="submit" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>


                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodt" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date" /><%--AutoPostBack="true" OnTextChanged="txtTodt_TextChanged"--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTodt" PopupButtonID="Image1" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtTodt"
                                                EmptyValueMessage="Please Select To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                ValidationGroup="submit" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTodt"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select Group </label>
                                        </div>
                                        <asp:RadioButtonList ID="rblGroup" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                            OnSelectedIndexChanged="rblGroup_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Selected="True" Text="Item Main Group" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Item Sub Group" Value="2"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Item" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rblSelectAllItem" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="rblSelectAllItem_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="All" Value="0" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="drpoDowntr" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Item</label>
                                        </div>
                                        <asp:DropDownList ID="ddlItem" data-select2-enable="true" runat="server" CssClass="form-control" ToolTip="Select Item"
                                            AppendDataBoundItems="true " TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCItem" runat="server" ControlToValidate="ddlItem"
                                            Display="None" ErrorMessage="Please Select Item" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-]12 btn-footer">
                            <asp:Button ID="btnreport" runat="server" Text="Report" TabIndex="5" ToolTip="Click To Report"
                                ValidationGroup="submit" CssClass="btn btn-info" OnClick="btnreport_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="6" ToolTip="Click To Cancel"
                                OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

            </div>

        </div>



    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
