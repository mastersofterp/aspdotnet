<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="STR_Budget_Head_Master.aspx.cs" Inherits="Stores_Masters_STR_Budget_HeadNAME_MASTER" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUDGET HEAD MASTER</h3>
                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Add/Edit Budget Name</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="form-group col-md-5">
                                                    <label>Budget Name:<span style="color: Red">*</span></label>

                                                    <asp:TextBox ID="txtBudName" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Budget Name"
                                                        onKeyUp="LovercaseToUppercase(this);" MaxLength="100"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtBudName" runat="server" ControlToValidate="txtBudName"
                                                        Display="None" ErrorMessage="Please Budget Head Name" ValidationGroup="store"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                </div>

                                            </asp:Panel>
                                            <div class="col-md-12 text-center">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary" TabIndex="2" ToolTip="Click To Submit" OnClick="butSubmit_Click" />
                                                <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="3" ToolTip="Click To Reset" OnClick="butCancel_Click" />
                                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" CssClass="btn btn-info" TabIndex="4" ToolTip="Click To Show Report"
                                                    OnClick="btnshowrpt_Click" />
                                            </div>
                                        </div>
                                    </div>


                                    <asp:Panel ID="pnlNewsPaper" runat="server">
                                        <div class="col-md-12  table-responsive">
                                            <asp:ListView ID="lvbudname" runat="server"
                                                OnSelectedIndexChanged="lvbudname_SelectedIndexChanged"
                                                OnPreRender="lvbudname_PreRender">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <center>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            <h4>BUDGET HEAD Details</h4>
                                                        </div>
                                                        <table class="table table-bordered table-hover table-responsive">
                                                            <tr class="bg-light-blue">
                                                                <th style="width: 10%">Action
                                                                </th>
                                                                <th>Budget Name
                                                                </th>

                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("BHNO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("BHNAME")%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                            <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvbudname" PageSize="5"
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


            </div>




            <table cellpadding="0" cellspacing="0" width="100%">
                <%--<tr>

                    <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                        colspan="6">BUDGET  MASTER
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
                            <%-- <fieldset class="fieldset">
                                <legend class="legend">Add/Edit Budget Name</legend>--%>


                            <table cellpadding="0" cellspacing="0" style="width: 70%;">
                                <tr>
                                    <%-- <td colspan="3">
                                            <div style="color: Red; font-weight: bold">
                                                &nbsp;Note : * marked field is Mandatory
                                            </div>
                                            <br />
                                        </td>--%>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="padding-left: 10px"><%--Budget Name 
                                    </td>
                                    <td style="width: 2%"><b>:</b></td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtBudName" runat="server" CssClass="texbox" Width="300px"
                                            onKeyUp="LovercaseToUppercase(this);" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtBudName" runat="server" ControlToValidate="txtBudName"
                                            Display="None" ErrorMessage="Please Budget Head Name" ValidationGroup="store"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="3" align="center">
                                        <br />
                                        <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" Width="70px" OnClick="butSubmit_Click" />
                                        <asp:Button ID="butCancel" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                                        <asp:Button ID="btnshowrpt" Text="Report" runat="server" Width="70px"
                                            OnClick="btnshowrpt_Click" />--%>
                                    </td>
                                </tr>
                                <%--  </table>
                    </fieldset>
                </div>
            </td>
        </tr>--%>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding-left: 10px"></td>
                                </tr>
                            </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function LovercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

    </script>

</asp:Content>

