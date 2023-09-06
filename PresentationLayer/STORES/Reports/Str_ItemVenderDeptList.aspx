<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_ItemVenderDeptList.aspx.cs" Inherits="STORES_Reports_Str_ItemVenderDeptList" Title="" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none; background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />
                            <%--Progressing Request....  --%>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEMS, VEDOR AND DEPARTMENT LIST</h3>

                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                    <asp:Panel ID="pnlStrConfig" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Select Type</div>
                                            <div class="panel-body">
                                                <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                                    Visible="false">
                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr>
                                                            <td style="width: 3%; vertical-align: top">
                                                                <img src="../../../images/error.gif" align="absmiddle" alt="Error" />
                                                            </td>
                                                            <td>
                                                                <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                                         </font>
                                                                <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                    Visible="false">
                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr>
                                                            <td style="width: 3%; vertical-align: top">
                                                                <img src="../../../images/confirm.gif" align="absmiddle" alt="confirm" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>


                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                            <label>Select Type: <span style="color: #FF0000">*</span></label>

                                                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" TabIndex="1"
                                                                AppendDataBoundItems="true" ToolTip="Select Type" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Item List" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Vendor List" Value="2"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Department List" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                             <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                                                                Display="None" ErrorMessage="Please Select Type" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10" id="trgrp" runat="server">
                                                            <label></label>

                                                            <asp:RadioButtonList ID="rblGroup" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="true" OnSelectedIndexChanged="rblGroup_SelectedIndexChanged" TabIndex="2">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Main Group Wise" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Sub Group Wise" Value="2"></asp:ListItem>

                                                            </asp:RadioButtonList>

                                                        </div>
                                                        <div class="form-group col-md-10" id="drpoDowntr" runat="server">
                                                            <label>Select Group :<span style="color: #FF0000">*</span></label>

                                                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" ToolTip="Please Select Group"
                                                                AppendDataBoundItems="true " TabIndex="3">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCItem" runat="server" ControlToValidate="ddlItem"
                                                                Display="None" ErrorMessage="Please Select Group name" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                           <%-- <ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                Enabled="True" TargetControlID="rfvCItem">
                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                        </div>
                                                        <div class="form-group col-md-10" id="trrblVendor" runat="server">
                                                            <label>Select Group:</label>

                                                            <asp:RadioButtonList ID="rblAllParticularVendor" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" TabIndex="4"
                                                                OnSelectedIndexChanged="rblAllParticularVendor_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Text="Category Wise Vendor" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="All Vendor" Value="0" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                        <div class="form-group col-md-10" id="trCateg" runat="server">
                                                            <label>Select Category :<span style="color: #FF0000">*</span></label>

                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" ToolTip="Please Select Category"
                                                                AppendDataBoundItems="true " TabIndex="5" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                                                Display="None" ErrorMessage="Please Select Category name" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <%--<ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                Enabled="True" TargetControlID="rfvCategory">
                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>

                                                        </div>
                                                        <div class="form-group col-md-10" id="trrblDept" runat="server">
                                                            <label>Select Group:</label>
                                                            <asp:RadioButtonList ID="rblDeptWise" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" TabIndex="6"
                                                                OnSelectedIndexChanged="rblDeptWise_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="All Department" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Main Department Wise" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                        <div class="form-group col-md-10" id="trMainDept" runat="server">
                                                            <label>Select Main Department :<span style="color: #FF0000">*</span></label>

                                                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" ToolTip="Please Select main department name"
                                                                AppendDataBoundItems="true " TabIndex="7">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"
                                                                Display="None" ErrorMessage="Please Select Main Department" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <%--<ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                Enabled="True" TargetControlID="rfvddlDept">
                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="submit" runat="server" />

                                                    <asp:Button ID="btnreport" runat="server" Text="Report" TabIndex="8" ToolTip="Click To Report"
                                                        ValidationGroup="submit" OnClick="btnreport_Click" CssClass="btn btn-info" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" ToolTip="Click To Cancel"
                                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                                </div>

                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>

                            </div>
                        </form>
                    </div>

                </div>


            </div>


            <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                <tr>
                    <%--<td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                        colspan="6">.
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div2" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                    </td>--%>
                    <%--<td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x;
                        border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;" colspan="6">
                        ITEMS, VEDOR AND DEPARTMENT LIST
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="6">
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <%--  Shrink the info panel out of view --%>
                                </p>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                </p>
                            </div>
                        </div>

                        <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize) {
                                    top.style.height = bottom.offsetHeight + 'px';
                                    top.style.width = bottom.offsetWidth + 'px';
                                }
                            }
                        </script>

                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>

                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td colspan="3"></td>
                    <td style="width: 35%"></td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td colspan="3" style="padding-left: 10px">
                        <%--<asp:Panel  Style="text-align: left; width: 100%;">--%>
                        <%--<fieldset style="padding-left: 30px; padding-right: 30px">
                                <legend class="legend"></legend>--%>
                        <br />
                        <table style="width: 100%" cellpadding="0" cellspacing="0">

                            <tr>
                                <td style="width: 20%"></td>
                            </tr>

                            <tr>
                                <td style="width: 20%"></td>
                                <td style="width: 2%"></td>
                                <td style="width: 35%"></td>
                            </tr>

                            <%-- <tr id="trgrp" runat="server">
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 80%">
                                        <asp:RadioButtonList ID="rblGroup" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblGroup_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Selected="True" Text="Main Group Wise" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Sub Group wise" Value="2"></asp:ListItem>
                                           
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>--%>


                            <%--<tr id="drpoDowntr" runat="server">
                                    <td style="width: 20%">Select Group <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList ID="ddlItem" runat="server" Width="90%" ToolTip="Please Select Group"
                                            AppendDataBoundItems="true " TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCItem" runat="server" ControlToValidate="ddlItem"
                                            Display="None" ErrorMessage="Please Select Group name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            Enabled="True" TargetControlID="rfvCItem">
                                        </ajaxToolKit:ValidatorCalloutExtender>
                                    </td>
                                </tr>--%>


                            <%--<tr id="trrblVendor" runat="server">
                                    <td style="width: 20%">Select Group
                                    </td>
                                    <td style="width: 2%">
                                        <b></b>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:RadioButtonList ID="rblAllParticularVendor" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" TabIndex="3"
                                            OnSelectedIndexChanged="rblAllParticularVendor_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Text="Category Wise Vendor" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="All Vendor" Value="0" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>--%>


                            <%--<tr id="trCateg" runat="server">
                                    <td style="width: 20%">Select Category <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="75%" ToolTip="Please Select Category"
                                            AppendDataBoundItems="true " TabIndex="4" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                            Display="None" ErrorMessage="Please Select Category name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                            Enabled="True" TargetControlID="rfvCategory">
                                        </ajaxToolKit:ValidatorCalloutExtender>
                                    </td>
                                </tr>--%>

                            <%--<tr id="trrblDept" runat="server">
                                    <td style="width: 20%">Select Group
                                    </td>
                                    <td style="width: 2%">
                                        <b></b>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:RadioButtonList ID="rblDeptWise" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" TabIndex="3"
                                            OnSelectedIndexChanged="rblDeptWise_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Selected="True" Text="All Department" Value="0"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Main Department Wise" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>--%>

                            <%--<tr id="trMainDept" runat="server">
                                    <td style="width: 20%">Select MainDepartment <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="100%" ToolTip="Please Select main department name"
                                            AppendDataBoundItems="true " TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Main Department name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            Enabled="True" TargetControlID="rfvddlDept">
                                        </ajaxToolKit:ValidatorCalloutExtender>
                                    </td>
                                </tr>--%>

                            <tr>
                                <td style="width: 20%">
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" runat="server" />
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 50%">
                                        <asp:Button ID="btnreport" runat="server" Text="Report" TabIndex="8" ToolTip="Click To Cancel"
                                            Width="70px" Style="font-family: Verdana; font-size: 11px; width: 52; padding: 2px"
                                            ValidationGroup="submit" OnClick="btnreport_Click" />
                                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" ToolTip="Click To Cancel"
                                            Width="70px" Style="font-family: Verdana; font-size: 11px; width: 52; padding: 2px"
                                            OnClick="btnCancel_Click" />--%>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </td>
                    <td style="width: 35%"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

