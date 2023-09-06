<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="STR_WriteOff.aspx.cs" Inherits="STORES_Transactions_Quotation_STR_WriteOff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEM WRITE OFF</h3>

                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Item Write Off</div>
                                        <div class="panel-body">

                                            <asp:Button ID="btnWriteOff" runat="server" Text="Write Off Report" UseSubmitBehavior="false"
                                                CssClass="btn btn-info" OnClick="btnWriteOff_Click" />
                                            <asp:Button ID="btnDamaged" runat="server" Text="Damaged Items Report" UseSubmitBehavior="false"
                                                CssClass="btn btn-info" OnClick="btnDamaged_Click" />

                                            <br />
                                            <br />
                                            <h5><span style="color: red">Note</span>: Please Select only Write Off Items</h5>
                                            <div class="col-md-5">
                                                <%-- <label>Department</label>--%>
                                                <asp:DropDownList ID="ddlDepartment" Visible="false" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-12">
                                                <asp:Panel runat="server" ID="Panel1" Visible="false">

                                                    <asp:ListView ID="lvitemInvoice" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>ITEM WRITE OFF</h4>
                                                                </div>

                                                                <table class="table table-bordered table-hover table-responsive">
                                                                    <tr class="bg-light-blue">
                                                                        <th>Item Name</th>

                                                                        <th>Brand
                                                                        </th>
                                                                        <th>Serial No 
                                                                        </th>
                                                                        <th>Item Issue Date
                                                                        </th>
                                                                        <th>Purchased Department
                                                                        </th>
                                                                        <th>Warranty End Date</th>
                                                                        <th>Change the Condition
                                                                        </th>
                                                                        <th>Select
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("BRAND")%>'></asp:Label>
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("serial_no")%>'></asp:Label>
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ISSUE_DATE")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("MDNAME")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ENDOF_WARRENTY_PERIOD")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:DropDownList ID="ddlCondition" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                        <asp:ListItem Value="0" Text="Working"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Write Off"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>

                                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("ISSUE_TNO") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>


                                                    <div class="col-md-12 text-center">


                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" UseSubmitBehavior="false"
                                                            CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Leaveapp" />

                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" Visible="false"/>




                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
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


                </td>
                            </tr>
            </table>
            </fieldset>
                        </div>
                    </caption>
            </caption>
            </table>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>

