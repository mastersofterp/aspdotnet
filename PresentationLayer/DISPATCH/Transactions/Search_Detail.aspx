<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Search_Detail.aspx.cs" Inherits="Dispatch_Transactions_Search_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DETAIL SEARCH</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="divPanel" runat="server">
                                <asp:Panel ID="Panel1" runat="server">
                                  

                                    <div class="row">
                                          <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Search Criteria</h5>
                                        </div>
                                    </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="radlSelect" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radlSelect_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="I">Inward</asp:ListItem>
                                                <asp:ListItem Value="O">Outward</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Dispatch From Date </label>
                                            </div>
                                             <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgFrmDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Select Dispatch From Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgFrmDt" TargetControlID="txtFrmDate">
                                                </ajaxToolKit:CalendarExtender>
                                                   <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFrmDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtFrmDate"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Dispatch To Date </label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="18" CssClass="form-control" ToolTip="Select Dispatch To Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                  <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtToDate"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From/To User</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFrmTo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select From/To User">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>User From Date</label>
                                            </div>
                                               <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtUFrmDt" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Select User From Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ce" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtUFrmDt">
                                                </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtUFrmDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtUFrmDt"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>User To Date </label>
                                            </div>
                                             <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image3" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtUToDt" runat="server" MaxLength="18" CssClass="form-control" ToolTip="Select User To Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cee2" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image3" TargetControlID="txtUToDt">
                                                </ajaxToolKit:CalendarExtender>
                                                   <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtUToDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtUToDt"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reference No</label>
                                            </div>
                                               <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" ToolTip="Enter Reference No." ></asp:TextBox>
                                        </div>

                                       <div class="form-group col-lg-3 col-md-6 col-12" id="divUT" runat="server">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label>User Type </label>
                                        </div>
                                             <asp:DropDownList ID="ddlUserType" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Select User Type" TabIndex="7" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="P">Principal</asp:ListItem>
                                                <asp:ListItem Value="S">Secretary</asp:ListItem>
                                                <asp:ListItem Value="C">Chairman</asp:ListItem>
                                                <asp:ListItem Value="H">HOD</asp:ListItem>
                                              <%--  <asp:ListItem Value="V">SVCE</asp:ListItem>--%>
                                            </asp:DropDownList>
                                     </div>
                                    </div>
                                </asp:Panel>
                            </div>


                            <div class=" col-12 btn-footer" id="divAddTo" runat="server">

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" ToolTip="Click here to Report" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                                <asp:Label ID="lblReq" runat="server" SkinID="Errorlbl" />

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlCourtName" runat="server">

                                    <asp:ListView ID="lvLetterDetails" OnItemDataBound="lvLetterDetails_ItemDatabound"
                                        runat="server">
                                        <LayoutTemplate>
                                         
                                                <div class="sub-heading">
                                                    <h5>Letter Details</h5>
                                                </div>
                                            <div class="table table-responsive">
                                       <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Dispatch Reference No
                                                            </th>
                                                            <th>From User
                                                            </th>
                                                            <th>To User
                                                            </th>
                                                            <th>Subject
                                                            </th>
                                                            <th>City
                                                            </th>
                                                            <th>Received/Send Date
                                                            </th>
                                                            <th>Status
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
                                                    <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/action_down.png" />
                                                    </asp:Panel>
                                                    &nbsp;
                                                                <asp:Label ID="lbIoNo" runat="server" Visible="false" Text='<%# Eval("IOTRANNO") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("CENTRALREFERENCENO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IOFROM") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TOUSER") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CITY") %>
                                                </td>
                                                <td>
                                                    <%# Eval("CENTRALRECSENTDT","{0:dd-MMM-yyyy}") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7">
                                                    <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel" ScrollBars="Auto">
                                                        <div class="sub-heading">
                                                            <h5>Receiver Details</h5>
                                                        </div>
                                                        <asp:ListView ID="lvDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="table table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" >
                                                                        <thead>
                                                                            <tr  class="bg-light-blue">
                                                                                <th>To User Name
                                                                                </th>
                                                                                <th>Department Name
                                                                                </th>
                                                                                <th>User Receive Date
                                                                                </th>
                                                                                <th>Remark
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center;">
                                                                    No Record Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Eval("UA_FULLNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("LONGNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DEPTENTRYDT","{0:dd-MMM-yyyy}") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REMARK") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STATUS") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <div class="text-center">
                                                        <div class="vista-grid_datapager">
                                                            <asp:DataPager ID="dpUserDet" runat="server" PagedControlID="lvDetails" PageSize="20"
                                                                OnPreRender="dpUserDet_PreRender">
                                                                <Fields>
                                                                    <asp:NumericPagerField ButtonCount="1" ButtonType="Link" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>--%>
                                        
                                                           
                                                    <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" TargetControlID="pnlShowCDetails"
                                                        ExpandControlID="pnlDetails" CollapseControlID="pnlDetails" CollapsedImage="~/Images/action_down.png"
                                                        ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                                    </ajaxToolKit:CollapsiblePanelExtender>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <%-- <div class="text-center">
                                                <div class="vista-grid_datapager">
                                                    <asp:DataPager ID="dpLett" runat="server" PagedControlID="lvLetterDetails" PageSize="20"
                                                        OnPreRender="dpLett_PreRender">
                                                        <Fields>
                                                            <asp:NumericPagerField ButtonCount="1" ButtonType="Link" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </div>--%>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            </div>


            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = 0;
                }
            }
        }

        function totAllIDs(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                {
                    if (e.type == 'checkbox') {


                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            }//../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }
    </script>
</asp:Content>

