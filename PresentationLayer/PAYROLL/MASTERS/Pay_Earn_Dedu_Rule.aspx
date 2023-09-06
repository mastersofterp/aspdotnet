<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Earn_Dedu_Rule.aspx.cs" Inherits="PayRoll_Pay_Earn_Dedu_Rule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EARNING AND DEDUCTION RULES</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Earning and Deduction Rules</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Rule</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRule" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Rule" AppendDataBoundItems="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRule" runat="server" ControlToValidate="ddlRule"
                                                Display="None" ErrorMessage="Please Select Rule" ValidationGroup="payroll" SetFocusOnError="True"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From</label>
                                            </div>
                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter From" />
                                            <asp:RequiredFieldValidator ID="rfvtxtFrom" runat="server" ControlToValidate="txtFrom"
                                                Display="None" ErrorMessage="Please Select From" ValidationGroup="payroll" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngtxtFrom" runat="server" ControlToValidate="txtFrom" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter From Basic between 0 to 99999.99" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999.99" Type="Double">
                                            </asp:RangeValidator>
                                            <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlfrom" runat="server" ControlToValidate="ddlFrom"
                                                Display="None" ErrorMessage="Please Select From" ValidationGroup="payroll" SetFocusOnError="True"
                                                InitialValue="-1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Min Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtminlimit" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter Min Limit" />
                                            <asp:RequiredFieldValidator ID="rfvtxtminlimit" runat="server" ControlToValidate="txtminlimit"
                                                Display="None" ErrorMessage="Please Enter Min Limit" ValidationGroup="payroll"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngtxtminlimit" runat="server" ControlToValidate="txtminlimit"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Min Limit between 0 to 99999.99"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999.99" Type="Double">
                                            </asp:RangeValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Per(%)</label>
                                            </div>
                                            <asp:TextBox ID="txtper" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter Percentage" />
                                            <asp:RequiredFieldValidator ID="rvftxtper" runat="server" ControlToValidate="txtper"
                                                Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="payroll"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngtxtper" runat="server" ControlToValidate="txtper" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Percentage between 0 to 300" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="300" Type="Double">
                                            </asp:RangeValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Pay Head</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPayhead" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select Pay Head" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                                Display="None" ErrorMessage="Please Select Pay Head" ValidationGroup="payroll"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To</label>
                                            </div>
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select To" />
                                            <asp:RequiredFieldValidator ID="rvftxtTo" runat="server" ControlToValidate="txtTo"
                                                Display="None" ErrorMessage="Please Enter To" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngtxtTo" runat="server" ControlToValidate="txtTo" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter To Basic between 0 to 999999.99" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="999999.99" Type="Double"> </asp:RangeValidator>
                                            <asp:CompareValidator ID="cvtxtTo" runat="server" SetFocusOnError="True" ValidationGroup="payroll"
                                                Display="None" ErrorMessage="To Basic Must be greate than or equal to From Basic"
                                                ControlToCompare="txtFrom" ControlToValidate="txtTo" Operator="GreaterThanEqual"
                                                Type="Double"> </asp:CompareValidator>
                                            <asp:DropDownList ID="ddlTo" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlTo" runat="server" ControlToValidate="ddlTo"
                                                Display="None" ErrorMessage="Please Select To"
                                                ValidationGroup="payroll" SetFocusOnError="True" InitialValue="-1"> </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Max Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtmaxlimit" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter Max Limit" />
                                            <asp:RequiredFieldValidator ID="rfvtxtmaxlimit" runat="server" ControlToValidate="txtmaxlimit"
                                                Display="None" ErrorMessage="Please Enter Max Limit" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngtxtmaxlimit" runat="server" ControlToValidate="txtmaxlimit"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Max Limit between 0 to 99999.99"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999.99" Type="Double"> </asp:RangeValidator>
                                            <asp:CompareValidator ID="cvtxtmaxlimit" runat="server" SetFocusOnError="True" ValidationGroup="payroll"
                                                Display="None" ErrorMessage="Max limit Must be greate than or equal to Min limit"
                                                ControlToCompare="txtminlimit" ControlToValidate="txtmaxlimit" Operator="GreaterThanEqual"> </asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Fix Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtfixamount" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Fix Amount" />
                                            <asp:RequiredFieldValidator ID="rfvtxtfixamount" runat="server" ControlToValidate="txtfixamount"
                                                Display="None" ErrorMessage="Please Enter Fix Amount" ValidationGroup="payroll"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngtxtfixamount" runat="server" ControlToValidate="txtfixamount"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Fix Amount between 0 to 99999.99"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999.99" Type="Double">
                                            </asp:RangeValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Effect From Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtEffectFromDate" runat="server" TabIndex="9" ToolTip="Enter From Date" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtEffectFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtEffectFromDate" runat="server" ControlToValidate="txtEffectFromDate"
                                                    Display="None" ErrorMessage="Please Select Effect From Date" ValidationGroup="payroll"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="form-group col-md-4"></div>
                                        <div class="form-group col-md-4"></div>--%>
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </asp:Panel>
                            <div class="col-12" runat="server" id="pnlList">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Select College" data-select2-enable="true"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server"
                                            ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please select College" ValidationGroup="payroll" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Select Rule</label>
                                        </div>
                                        <asp:DropDownList ID="ddlpayruleselect" runat="server" AppendDataBoundItems="True" AutoPostBack="True" ToolTip="Select Rule" TabIndex="11" data-select2-enable="true" OnSelectedIndexChanged="ddlpayruleselect_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="col-12 btn-footer">
                                <p class="text-center">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" TabIndex="12" ToolTip="Click Add New To Enter Earnings And Deducations" CssClass="btn btn-primary" ValidationGroup="payroll"></asp:LinkButton>
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" TabIndex="13" ToolTip="Click to Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" ToolTip="Click to go to previous" TabIndex="14"
                                        CssClass="btn btn-primary" />
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" TabIndex="15" ToolTip="Click To show the Report" OnClick="btnShowReport_Click1" Visible="false" />                                   
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="16" ToolTip="Click to Reset"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />                               
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <div class="col-md-12">
                                    </div>
                            </div>
                            <div id="List" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvCalPay" runat="server">
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Earning and Deduction Rules</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Code
                                                        </th>
                                                        <%--<th>Type
                                                        </th>--%>
                                                        <th>From
                                                        </th>
                                                        <th>To
                                                        </th>
                                                        <th>Per
                                                        </th>
                                                        <th>Min_limit
                                                        </th>
                                                        <th>Max_limit
                                                        </th>
                                                        <th>Fix_Amt
                                                        </th>
                                                        <th>Pay Rule
                                                        </th>
                                                        <th>FromDate
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
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CALNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("CALNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("PAYHEAD")%>
                                                </td>
                                                <%-- <td>
                                                    <%# Eval("TYPE")%>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("B_MIN")%>
                                                </td>
                                                <td>
                                                    <%# Eval("B_MAX")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MIN")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MAX")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FIX")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAYRULE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FDT","{0:dd/MM/yyyy}")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                    <div class="vista-grid_datapager d-none">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvCalPay" PageSize="100"
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
             <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
              <script type="text/javascript">
                  //  keeps track of the delete button for the row
                  //  that is going to be removed
                  var _source;
                  // keep track of the popup div
                  var _popup;

                  function showConfirmDel(source) {
                      this._source = source;
                      this._popup = $find('mdlPopupDel');

                      //  find the confirm ModalPopup and show it    
                      this._popup.show();
                  }

                  function okDelClick() {
                      //  find the confirm ModalPopup and hide it    
                      this._popup.hide();
                      //  use the cached button as the postback source
                      __doPostBack(this._source.name, '');
                  }

                  function cancelDelClick() {
                      //  find the confirm ModalPopup and hide it 
                      this._popup.hide();
                      //  clear the event source
                      this._source = null;
                      this._popup = null;
                  }
    </script>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
