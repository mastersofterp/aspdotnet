<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ToolkitMaster.aspx.cs" Inherits="STORES_Masters_ToolkitMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function totAllIDs(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TOOLKIT DEFINITION</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Add/Edit ToolKit</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnl" runat="server">

                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                            <label>ToolKit Name:<span id="span1" style="color: #FF0000">*</span></label>
                                                            <asp:TextBox ID="txttoolkitname" runat="server" CssClass="form-control"
                                                                onKeyUp="this.style.textTransform='uppercase'" TabIndex="1" ToolTip="Enter Toolkit Name">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="txttoolkitname"
                                                                Display="None" ErrorMessage="Please Enter Toolkit Name" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-md-10">
                                                            <label>Item Sub Group:<span id="span2" style="color: #FF0000">*</span></label>
                                                            <asp:DropDownList ID="ddlItemSubGrp" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                                TabIndex="2" ToolTip="Select Item Subgroup" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlItemSubGrp_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemSubGrp"
                                                                Display="None" ErrorMessage="Please Select Item Subgroup" ValidationGroup="store"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>


                                                    </div>
                                                    <div class="form-group col-md-6">
                                                    </div>

                                                    <div class="form-group col-md-12 table-responsive">
                                                        <asp:ListView ID="lvitemInvoice" runat="server">
                                                            <EmptyDataTemplate>
                                                                <br />
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="demo-grid" class="vista-grid">
                                                                    <div class="titlebar">
                                                                        <h4>ITEMS</h4>
                                                                    </div>

                                                                    <table id="tblSelectAll" class="table table-bordered table-hover table-responsive">
                                                                        <tr class="bg-light-blue">
                                                                            <th>Select All &nbsp<asp:CheckBox ID="chkSelectAll" runat="server"  onclick="totAllIDs(this)"  
                                                                                AutoPostBack="true"/>

                                                                            </th>

                                                                            <th>Item Name
                                                                            </th>

                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                   </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("ITEM_NO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                                    </td>

                                                                </tr>

                                                            </ItemTemplate>
                                                       
                                                        </asp:ListView>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Submit"
                                                    OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="4" ToolTip="Click To Cancel" />
                                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" CssClass="btn btn-info" OnClick="btnshowrpt_Click" Visible="false" TabIndex="5" ToolTip="Click To Report" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlDeptUser" runat="server">
                                        <div class="col-md-12 table-responsive">
                                            <asp:ListView ID="lvStage" runat="server">
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
                                                                            <th>Toolkit Name
                                                                            </th>
                                                                            <%-- <th>Item Name
                                                                            </th>--%>
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
                                                        <td style="width: 10px">
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("TK_NO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("TK_NAME")%>
                                                        </td>
                                                        <%-- <td>
                                                            <%# Eval("ITEM_NAME")%>
                                                        </td>--%>
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
                            <br />
                            <table cellpadding="0" cellspacing="0" style="width: 80%;">
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="padding-left: 10px"></td>
                                </tr>
                                <tr>
                                    <td class="form_left_label"></td>
                                    <td class="form_left_text " colspan="2">
                                        <br />
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <br />
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
