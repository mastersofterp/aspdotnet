<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="str_stage.aspx.cs" Inherits="STORES_Masters_str_stage" Title="Untitled Page" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STAGE MASTER</h3>

                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Add/Edit STAGE</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnl" runat="server">

                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                            <label>Stage Name:<span id="span1" style="color: #FF0000">*</span></label>
                                                            <asp:TextBox ID="txtStname" runat="server" CssClass="form-control"
                                                                onKeyUp="this.style.textTransform='uppercase'" TabIndex="1" ToolTip="Enter Stage Name">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtStname"
                                                                Display="None" ErrorMessage="Please Enter Stage Name" ValidationGroup="store"
                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Table Involved:<span id="spanDept" style="color: #FF0000">*</span> </label>

                                                            <asp:DropDownList ID="ddlTable" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                                TabIndex="2" ToolTip="Select Table Involved">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0">
                                                                </asp:ListItem>
                                                            </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTable"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Table Involved" ValidationGroup="store"
                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Submit"
                                                    OnClick="butSubmit_Click" CausesValidation="true" />
                                                <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" OnClick="butCancel_Click" TabIndex="4" ToolTip="Click To Cancel" ValidationGroup="submit" />
                                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" CssClass="btn btn-info" OnClick="butCancel_Click" Visible="false" TabIndex="5" ToolTip="Click To Report" />

                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlDeptUser" runat="server">
                                        <div class="col-md-12 table-responsive">
                                            <asp:ListView ID="lvStage" runat="server"
                                                OnSelectedIndexChanged="lvStage_SelectedIndexChanged">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <center>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                    </center>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="padding-left: 10px; padding-right: 10px">
                                                                <div id="demo-grid" class="vista-grid">
                                                                    <div class="titlebar">
                                                                        <h4>Stage</h4>
                                                                    </div>
                                                                    <table class="table table-bordered table-hover table-responsive">
                                                                        <tr class="bg-light-blue">
                                                                            <th>Action
                                                                            </th>
                                                                            <th>Stage Name
                                                                            </th>
                                                                            <th>Table Involved
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("STNO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("SNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Table_INV")%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                            <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvStage" PageSize="10"
                                                    OnPreRender="dpPager_PreRender">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                        </form>
                    </div>

                </div>

                <div class="col-md-12">
                </div>
            </div>





            <table cellpadding="0" cellspacing="0" width="100%">
                <%--<tr>
                    <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                        colspan="6">
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                    </td>

                </tr>--%>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record
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
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="width: 97%; padding-left: 10px;">
                            <%--<fieldset class="fieldset">
                                <legend class="legend"></legend>--%>
                            <br />
                            <table cellpadding="0" cellspacing="0" style="width: 80%;">
                                <tr>
                                    <td colspan="3">
                                        <%--<div style="color: Red; font-weight: bold">
                                                &nbsp;Note : * marked field is Mandatory
                                            </div>--%>
                                    </td>
                                </tr>

                                <%--<tr>
                                    <td class="form_left_label" style="padding-left: 10px">

                                        <span id="span1" style="color: #FF0000">*</span>  Stage Name
                                    </td>
                                    <td style="width: 2%"><b>:</b></td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtStname" runat="server" CssClass="texbox"
                                            onKeyUp="this.style.textTransform='uppercase'" Width="250px">
                                        </asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="form_left_label" style="padding-left: 10px">

                                        <%-- <span id="spanDept" style="color: #FF0000">*</span> Table Involved
                                    </td>
                                    <td style="width: 2%"><b>:</b></td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlTable" AppendDataBoundItems="true" runat="server" CssClass="dropdownlist"
                                            Width="255px">
                                            <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0">
                                            </asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label"></td>
                                    <td class="form_left_text " colspan="2">
                                        <br />
                                        <%--<asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" Width="70px"
                                            OnClick="butSubmit_Click" />
                                        <asp:Button ID="butCancel" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                                        <asp:Button ID="btnshowrpt" Text="Report" runat="server" Width="70px" OnClick="butCancel_Click" Visible="false" />--%>
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                                <%-- </table>
                    </fieldset>
                </div>
            </td>
        </tr>--%>
                                <tr>
                                    <td colspan="3" align="center">
                                        <br />
                                        <%--<asp:Panel ID="pnlDeptUser" runat="server">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ListView ID="lvStage" runat="server"
                                                            OnSelectedIndexChanged="lvStage_SelectedIndexChanged">
                                                            <EmptyDataTemplate>
                                                                <br />
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="padding-left: 10px; padding-right: 10px" align="center">
                                                                            <div id="demo-grid" class="vista-grid">
                                                                                <div class="titlebar">
                                                                                    Stage
                                                                                </div>
                                                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr class="header">
                                                                                        <th>Action
                                                                                        </th>
                                                                                        <th>Stage Name
                                                                                        </th>
                                                                                        <th>Table Involved
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("STNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Table_INV")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("STNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Table_INV")%>
                                                                    </td>

                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                        <div class="vista-grid_datapager">
                                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvStage" PageSize="10"
                                                                OnPreRender="dpPager_PreRender">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>--%>
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


