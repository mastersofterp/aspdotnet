<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeesRefundStudentwise.aspx.cs" Inherits="ACCOUNT_FeesRefundStudentwise"
    Title="Untitled Page" %>


<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 150px;
        }

        .auto-style1 {
            width: 161px;
        }

        .auto-style2 {
            width: 192px;
        }
    </style>
    <%--    <link href="../Css/UpdateProgress.css" rel="stylesheet" type="text/css" />
    --%>
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

    <script type="text/javascript">

        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var label = document.getElementById("<%=lblChkCount.ClientID %>");
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkFeesTransfer')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else {
                            e.checked = false;
                            label.innerHTML = "0";
                            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
                        }

                    }
                }
            }
            label.innerHTML = hdfTot.value;
            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
            if (headchk.checked == false) {
                hdfTot.value = "0";
                var count = $("[type='checkbox']:checked").length;
                label.innerHTML = count;
                //label.innerHTML = "0";
                document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
            }
        }

        function validateAssign() {
            debugger;
            var label = document.getElementById("<%=lblChkCount.ClientID %>");
            var count = $("[type='checkbox']:checked").length;
            label.innerHTML = count;
            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
        }

    </script>

    <%-- <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>--%>

    <%--  <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBank"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                   <%-- <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>--%>
    <%--  </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    <%--<div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="99%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    <%--<div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">--%>
    <%-- </td>
            </tr>
            <tr>
                <td style="padding: 10px" colspan="2"></td>
            </tr>
        </table>
    </div>--%>

    <%--    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBank"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updBank" runat="server" >
        <ContentTemplate>--%>
    <asp:Panel ID="updBank" Height="100%" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">FEES REFUND TRANSFER STUDENTWISE
                   <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" /></h3>--%>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="panel panel-info">
                                    <div class="panel-heading"></div>
                                    <div class="panel-body">
                                        <div class="form-group row">
                                            <div class="col-md-12">
                                                <br />
                                                <br />
                                                <label>Note : </label>
                                                <span style="color: red; font-weight: bold">Please select only 200 student for Single Voucher</span>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div id="Div2" class="form-group row" runat="server">
                                            <div id="Div3" class="col-md-3" runat="server">
                                                <span style="color: red; font-weight: bold;">*</span><label>From Date:</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                </div>
                                            </div>
                                            <div id="Div4" class="col-md-3" runat="server">
                                                <span style="color: red; font-weight: bold;">*</span><label>To Date :</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtTodate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                        TargetControlID="txtTodate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTodate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                </div>
                                            </div>
                                            <div id="Div5" class="col-md-3" runat="server">
                                                <span style="color: red; font-weight: bold;">*</span><label>Degree :</label>
                                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ValidationGroup="Submit" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                    Width="250px">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" InitialValue="Please Select" ControlToValidate="ddlDegree"
                                                    ErrorMessage="Please Select Degree" ValidationGroup="Submit" Display="None">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div id="Div6" class="col-md-3" runat="server">
                                                <span style="color: red; font-weight: bold;">*</span><label>Branch :</label>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ValidationGroup="Submit" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlBranch" ErrorMessage="Please Select Branch" ValidationGroup="Submit"
                                                    Display="None">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                        </div>


                                        <div class="col-lg-12 col-md-3 col-12" id="Div7" runat="server">
                                            <div class="row">
                                                <div id="Div8" class="form-group col-lg-3 col-md-3 col-12" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <span style="color: red; font-weight: bold;">*</span><label>Receipt Type :</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceipt" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit" AutoPostBack="true" OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged"
                                                        CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlReceipt" runat="server" InitialValue="Please Select"
                                                        ControlToValidate="ddlReceipt" ErrorMessage="Please Select Receipt Type" ValidationGroup="Submit"
                                                        Display="None">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div id="Divbatch" class="form-group col-lg-3 col-md-3 col-12" runat="server" >
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <span style="color: red; font-weight: bold;">*</span><label>Admission  Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" ValidationGroup="Submit" >
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                                        ControlToValidate="ddlbatch" ErrorMessage="Please Select Admission  Batch" ValidationGroup="Submit"
                                                        Display="None">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-3 col-12" id="DivSem" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <span style="color: red; font-weight: bold;">*</span><label>Semester</label>
                                                    </div>
                                                    <div id="Div9" runat="server">
                                                        <asp:DropDownList ID="ddsem" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="false" ValidationGroup="Submit">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                                            ControlToValidate="ddsem" ErrorMessage="Please Select Semester" ValidationGroup="Submit"
                                                            Display="None">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-4 text-center">
                                                <asp:RadioButtonList ID="rdbTransferType" AutoPostBack="true" OnSelectedIndexChanged="rdbTransferType_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="C" Selected="True">Cash Transfer Data&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="B">Bank Transfer Data</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Submit" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ValidationGroup="Submit"
                                                    ShowMessageBox="true" ShowSummary="false" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 mt-3">
                                <div class="col-md-12" id="Panel1" runat="server">
                                   
                                   <asp:Panel ID="Panel3" runat="server" Height="220px" ScrollBars="Vertical">
                                             <asp:ListView ID="lvFeeTransfer" runat="server" OnSelectedIndexChanged="lvFeeTransfer_SelectedIndexChanged">
                                            <LayoutTemplate>
                                                <%-- <div class="ui-widget-header">
                                                                        Fees Transfer
                                                                    </div>--%>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                                <th style="text-align: left; width: 3%; padding-left: 0px">
                                                                    <asp:CheckBox ID="chkBoxFeesTransfer" runat="server" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th style="text-align: left; width: 15%">
                                                                    NAME
                                                                </th>
                                                                <th style="text-align: left; width: 6%">
                                                                    REC NO
                                                                </th>
                                                                <th style="text-align: left; width: 6%">
                                                                    VCH NO
                                                                </th>
                                                                <th style="text-align: left; width: 6%">
                                                                    REC DATE
                                                                </th>
                                                                <th style="text-align: left; width: 6%">
                                                                    VCH DATE
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    DEGREE
                                                                </th>
                                                                <th style="text-align: left; width: 7%">
                                                                    AMOUNT
                                                                </th>
                                                                <th style="text-align: left; width: 5%">
                                                                    PAY TYPE
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    BRANCH
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
                                                         <th style="text-align: left; width: 3%; padding-left: 0px">
                                                            <asp:CheckBox ID="chkFeesTransfer" runat="server" onclick="validateAssign()" ToolTip='<%#Eval("Id") %>' />
                                                        </td>
                                                        <td style="text-align: left; width: 15%">
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 6%">
                                                            <asp:Label ID="lblRecptCode" runat="server" Text='<%# Eval("REC_NO") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 6%">
                                                            <asp:Label ID="lblDCRVoucherNo" runat="server" Text='<%# Eval("VOUCHER_NO") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 6%">
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("REC_DT", "{0:dd/MM/yyyy}")  %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 6%">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("VOUCHER_DATE", "{0:dd/MM/yyyy}")  %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("DEGREENAME")  %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 7%">
                                                            <asp:Label ID="lblDob" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 4%">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("PAY_TYPE") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                </div>
                            </div>
                            <div class="form-group row" id="trTotal" runat="server" visible="false">
                                <div class="col-md-5"></div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblCount" runat="server" Text="Receipt Count" Font-Bold="true" Font-Size="Medium">
                                    </asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblChkCount" runat="server" Font-Bold="true" Font-Size="Medium">
                                    </asp:Label>
                                </div>

                            </div>
                            <div class="form-group row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnCollect" runat="server" CssClass="btn btn-primary" Text="Collect the record" OnClick="btnCollect_Click1"
                                        Visible="false" />
                                </div>
                            </div>

                            <div id="TrFees" runat="server" visible="false" class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div id="divTransfer" runat="server" class="form-group row">
                                        <div class="col-md-6">
                                            <b>
                                                <h4>Fee Head :</h4>
                                            </b>
                                            <asp:ListView ID="lstFees" runat="server">
                                                <LayoutTemplate>
                                                    <%--<div class="vista-grid">--%>
                                                    <table id="tblHead" class="table table-responsive table-bordered" style="width: 100%">
                                                        <thead>
                                                            <tr style="background-color: #ADADAD;">
                                                                <th style="text-align: left;">F.NO
                                                                </th>
                                                                <th style="text-align: left;">FEE HEAD
                                                                </th>
                                                                <th style="text-align: left;">AMOUNT
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                    <%--</div>--%>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%# Eval("FEE_HEAD") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblFeeHeads" runat="server" Text='<%# Eval("FEE_LONGNAME") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </div>

                                        <div class="col-md-6">
                                            <b>
                                                <h4>Cash/Bank:</h4>
                                            </b>


                                            <div>
                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <label>Select Ledger :</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlLedger" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit"
                                                            CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                        <asp:RequiredFieldValidator ID="rfvLedger" runat="server" InitialValue="0" ControlToValidate="ddlLedger"
                                                            ErrorMessage="Please Select Ledger" ValidationGroup="Save" Display="None">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <label>Voucher Date :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtVoucherDate" Style="text-align: right" runat="server" CssClass="form-control" data-select2-enable="true" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                                TargetControlID="txtVoucherDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtVoucherDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <label>Total Amount</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="lblTotal" runat="server" CssClass="form-control" data-select2-enable="true" ForeColor="Red" Text="0" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                    </div>


                                    <div class="text-center">
                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-primary" Text="Transfer Fees" ValidationGroup="Save"
                                            OnClick="btnTransfer_Click" Visible="false"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait..';" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
                                    </div>

                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    </asp:Panel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
