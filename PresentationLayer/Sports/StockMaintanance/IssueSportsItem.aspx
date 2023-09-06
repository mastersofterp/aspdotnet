<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IssueSportsItem.aspx.cs" Inherits="Sports_StockMaintanance_IssueSportsItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ISSUE SPORTS ITEM</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbIssueTo" runat="server" TabIndex="1" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbIssueTo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="T">Teams</asp:ListItem>
                                                <%--<asp:ListItem Value="C">Clubs</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trTeams" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Teams</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTeams" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Team" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTeams" runat="server" ValidationGroup="Add" InitialValue="0" ControlToValidate="ddlTeams" Display="None" ErrorMessage="Please Select Teams."></asp:RequiredFieldValidator>

                                        </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12" id="trClubs" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Clubs</label>
                                            </div>
                                               <asp:DropDownList ID="ddlClubs" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Club" TabIndex="3">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvClubs" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlClubs" Display="None" ErrorMessage="Please Select Club.">
                                                </asp:RequiredFieldValidator>
                                              </div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Issue Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgPdOn" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="4" CssClass="form-control" ToolTip="Enter Issue Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtIssueDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meePdDt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtIssueDate" OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevpdsdt" runat="server" ControlExtender="meePdDt" ControlToValidate="txtIssueDate"
                                                    Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select Issue Date" InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true" TooltipMessage="Please Select Issue Date"
                                                    ValidationGroup="Add" IsValidEmpty="false">  </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                    <%--Modified by Saahil Trivedi 24-02-2022--%>
                                                <label>Sports Item </label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" TabIndex="5" ToolTip="Select Item to issue"></asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Sports Item." ValidationGroup="Add" ControlToValidate="ddlItem" InitialValue="0" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                             
                                                <label>Available Qty</label>
                                            </div>
                                            <asp:TextBox ID="txtAvailableQty" runat="server" CssClass="form-control" Enabled="false" ToolTip="Available Quantity of Selected Item"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Issue Quantity </label>
                                            </div>
                                            <asp:TextBox ID="txtQty" runat="server" TabIndex="6" onkeypress="return CheckNumeric(event,this);" CssClass="form-control" ToolTip="Enter Quantity" MaxLength="10"></asp:TextBox>
                                            <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom, Numbers" ValidChars="+- " TargetControlID="txtQty">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Please Enter Issue Quantity." ValidationGroup="Add" ControlToValidate="txtQty" Display="None">
                                            </asp:RequiredFieldValidator>
                                            
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="8" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>
                                           <%-- Modified by Saahil Trivedi 07-02-2022--%>
                                             <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ErrorMessage="Please Enter Remark." ValidationGroup="Add" ControlToValidate="txtRemark" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </asp:Panel>

                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvIssueList" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>ITEM ISSUE LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Delete
                                                            </th>
                                                            <th>Item Name
                                                            </th>
                                                            <th>Quantity
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
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                </td>
                                                <td>
                                                    <%# Eval("ITEM_NAME")%>
                                                    <asp:HiddenField ID="hdnItemNo" runat="server" Value='<%# Eval("ITEM_NO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("QUANTITY")%>                         
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 text-center mb-2">
                               <asp:Button ID="btnAdd" runat="server" Text="Add Item" OnClick="btnAdd_Click" CssClass="btn btn-primary" ToolTip="Click here to Add Item" TabIndex="7" ValidationGroup="Add" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="Add" HeaderText="Following Fields are mandatory" />

                            </div>
                            <div class=" col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" TabIndex="10" CausesValidation="true" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnReport_Click" TabIndex="12" CausesValidation="false" />
                                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="11" CausesValidation="false" />
                            
                                 <asp:Button ID="btnBalReport" runat="server" Text="Stock Balance Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnBalReport_Click" TabIndex="13" CausesValidation="false" Visible="false" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" HeaderText="Following Fields are mandatory" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvIssueEntryList" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>ITEM ISSUE ENTRY LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Team/Club
                                                            </th>
                                                            <th>Item Name
                                                            </th>
                                                            <th>Issue Qty.
                                                            </th>
                                                             <th>Available Qty.
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("ISSUE_ID") %>'
                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;                      
                                                </td>
                                                <td>
                                                    <%# Eval("TEAMNAME")%>
                                                </td>
                                               <%-- <td>
                                                    <%# Eval("ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK")%>                         
                                                </td>--%>
                                                  <td>
                                                    <%# Eval("ITEM_NAME")%>
                                                  
                                                </td><%--SHIAKH JUNED 13-06-2022--%>
                                                <td>
                                                    <%# Eval("QUANTITY")%>                         
                                                </td><%--SHIAKH JUNED 13-06-2022--%>
                                                  <td>
                                                    <%# Eval("QTY_BALANCE")%>                         
                                                </td><%--SHIAKH JUNED 13-06-2022--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvIssueEntryList" PageSize="10" OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField
                                                    FirstPageText="<<" PreviousPageText="<" ButtonType="Link" RenderDisabledButtonsAsLabels="true"
                                                    ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField
                                                    LastPageText=">>" NextPageText=">" ButtonType="Link" RenderDisabledButtonsAsLabels="true"
                                                    ShowFirstPageButton="false" ShowPreviousPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
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

        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfItemName').value = Name[0];
        }


    </script>
</asp:Content>

