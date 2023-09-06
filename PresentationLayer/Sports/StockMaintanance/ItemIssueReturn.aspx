<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ItemIssueReturn.aspx.cs" Inherits="Sports_StockMaintanance_ItemIssueReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">ISSUE RETURN</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-10">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        <asp:Panel ID="pnlDesig" runat="server">
                                            <div class="panel panel-info">
                                                <%--<div class="panel panel-heading">Issue Return Details</div>--%>
                                                 <div class="sub-heading">
                                                    <h5>Issue Return Details</h5>
                                                </div>
                                                <div class="panel panel-body">

                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-3">

                                                            <label><span style="color: #FF0000"></span></label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:RadioButtonList ID="rdbIssueTo" runat="server" TabIndex="1" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbIssueTo_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Selected="True" Value="T">Teams</asp:ListItem>
                                                               <%-- <asp:ListItem Value="C">Clubs</asp:ListItem>--%>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>


                                                    <div id="trTeams" runat="server" class="form-group col-md-12">
                                                        <div class="col-md-3">
                                                            <label><span style="color: #FF0000">*</span>Teams :</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlTeams" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Team" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlTeams_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvTeams" runat="server" ValidationGroup="Submit"
                                                                InitialValue="0" ControlToValidate="ddlTeams" Display="None" ErrorMessage="Please Select Teams."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div id="trClubs" runat="server" visible="false" class="form-group col-md-12">
                                                        <div class="col-md-3">
                                                            <label><span style="color: #FF0000">*</span>Clubs :</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlClubs" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Club" TabIndex="3" OnSelectedIndexChanged="ddlClubs_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvClubs" runat="server" ValidationGroup="Submit" InitialValue="0"
                                                                ControlToValidate="ddlClubs" Display="None" ErrorMessage="Please Select Club.">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-3">
                                                            <label><span style="color: #FF0000">*</span>Issue Date :</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlIssueDate" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Issue Date" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueDate_SelectedIndexChanged" TabIndex="4" ></asp:DropDownList>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Submit"
                                                                InitialValue="0" ControlToValidate="ddlIssueDate" Display="None" ErrorMessage="Please Select Date"></asp:RequiredFieldValidator>
                                                           
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-10">
                                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvIssueList" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <%--<h4 class="box-title">ITEM ISSUE LIST
                                                        </h4>--%>
                                                         <div class="sub-heading">
                                                    <h5>ITEM ISSUE LIST</h5>
                                                </div>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>SrNo
                                                                    </th>
                                                                    <th>Item Name
                                                                    </th>
                                                                    <th>Issue Quantity
                                                                    </th>
                                                                    <th>Return Status
                                                                    </th>
                                                                    <th>Remark
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
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SRNO")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnIssueId" runat="server" Value='<%# Eval("ISSUE_ID")%>' />
                                                            <asp:HiddenField ID="hdnIssueDetailsId" runat="server" Value='<%# Eval("ISSUE_ITEM_ID")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnItemNo" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QUANTITY","{0:0.0}")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkReturn" runat="server" TabIndex="5" ToolTip="Check if Item is Return"
                                                                Checked='<%# (Eval("RETURN_STATUS").ToString() == "1" ? true : false) %>'
                                                                Enabled='<%# (Eval("RETURN_STATUS").ToString() == "1" ? false : true) %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRemark" runat="server" TabIndex="6" Text='<%# Eval("RETURN_REMARK")%>' ToolTip="Enter Remark"
                                                                Enabled='<%# (Eval("RETURN_STATUS").ToString() == "1" ? false : true) %>'></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-10">
                                        <div class="col-md-3">

                                            <label><span style="color: #FF0000"></span>Remark :</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtFinalRemark" runat="server" TabIndex="7" ToolTip="Enter Remark" CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit" TabIndex="8" CausesValidation="true" />
                              <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="9" Text="Cancel" ToolTip="Click here to Cancel" />
                            <asp:Button ID="btnReport" runat="server" CausesValidation="false" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="10" Text="Report" ToolTip="Click to get Report" />
                              <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            <p>
                                <%--  Modified by Saahil Trivedi 07-02-2022--%><%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" HeaderText="Following Fields are mandatory" />--%>
                            </p>

                        </p>
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
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
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
    </script>
</asp:Content>

