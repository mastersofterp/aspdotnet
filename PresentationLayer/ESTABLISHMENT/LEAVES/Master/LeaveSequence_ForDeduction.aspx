<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveSequence_ForDeduction.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_LeaveSequence_ForDeduction" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
 
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE SEQUENCE FOR DEDUCTION</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select College Name"
                                        AppendDataBoundItems="true" TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="PAPath"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        CssClass="form-control" data-select2-enable="true" ToolTip="Select Staff Type" TabIndex="2"
                                        OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                        Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true"
                                        ValidationGroup="PAPath" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Appointment Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlAppoint" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        AutoPostBack="True" TabIndex="3" ToolTip="Select Appointment Type">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAppoint"
                                        Display="None" ErrorMessage="Please Select Appointment Type" SetFocusOnError="true"
                                        ValidationGroup="PAPath" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Leave 01</label>
                                    </div>
                                    <asp:DropDownList ID="ddllv1" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddllv1_SelectedIndexChanged" ToolTip="Select Leave 01">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddllv1"
                                        Display="None" ErrorMessage="Please Select Leave 01" SetFocusOnError="true"
                                        ValidationGroup="PAPath" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <%--  <div class="form-group col-md-12">
                                    <asp:Image ID="darrow1" runat="server" ImageUrl="~/Images/action_down.png" />
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Leave 02</label>
                                    </div>
                                    <asp:DropDownList ID="ddllv2" runat="server" AppendDataBoundItems="true" ToolTip="Select Leave 02" TabIndex="5"
                                        Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddllv2_SelectedIndexChanged1" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%--   <div class="form-group col-md-12">
                                    <asp:Image ID="darrow2" runat="server" ImageUrl="~/Images/action_down.png" />
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Leave 03</label>
                                    </div>
                                    <asp:DropDownList ID="ddllv3" runat="server" AppendDataBoundItems="true" ToolTip="Select Leave 03" TabIndex="6"
                                        Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddllv3_SelectedIndexChanged1" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%--  <div class="form-group col-md-12">
                                    <asp:Image ID="darrow3" runat="server" ImageUrl="~/Images/action_down.png" />
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Leave 04</label>
                                    </div>
                                    <asp:DropDownList ID="ddllv4" runat="server" AppendDataBoundItems="true" TabIndex="7" CssClass="form-control" data-select2-enable="true"
                                        Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddllv4_SelectedIndexChanged" ToolTip="Select Leave 04">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%-- <div class="form-group col-md-12">
                                    <asp:Image ID="darrow4" runat="server" ImageUrl="~/Images/action_down.png" />
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Leave 05</label>
                                    </div>
                                    <asp:DropDownList ID="ddllv5" runat="server" AppendDataBoundItems="true" TabIndex="8" CssClass="form-control" data-select2-enable="true"
                                        Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddllv5_SelectedIndexChanged" ToolTip="Select Leave 05">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Sequence</label>
                                    </div>
                                    <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" TabIndex="9"
                                        ReadOnly="true" TextMode="MultiLine" ToolTip="Leave Sequence for Deduction" Enabled="false">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-6 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note </h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Marked Is Mandatory & Selection of 'Leave 01' is mandatory ! </span></p>

                                    </div>
                                </div>

                                <div class="form-group col-md-12">
                                    <p class="text-center">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12  btn-footer">
                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" TabIndex="10"
                            ToolTip="Click here to Add New Leave Sequence for Deduction" CssClass="btn btn-primary"></asp:LinkButton>
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info"
                            ToolTip="Click here to Show Report" Visible="false" TabIndex="11" />
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAPath" OnClick="btnSave_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Save" TabIndex="12" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Go Back" TabIndex="14" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" TabIndex="13" />

                    </div>

                    <div class="col-12 mt-3 mb-3">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvLeaveSeq" runat="server">
                                <EmptyDataTemplate>

                                    <div class="text-center">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Passing Authority Path" />
                                    </div>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Leave Sequence for Deduction Details</h5>

                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>College Name
                                                    </th>
                                                    <th>StaffType
                                                    </th>
                                                    <th>Appointment Type
                                                    </th>
                                                    <th>Leave 01
                                                    </th>
                                                    <th>Leave 02
                                                    </th>
                                                    <th>Leave 03
                                                    </th>
                                                    <th>Leave 04
                                                    </th>
                                                    <th>Leave 05
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LSNO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                               <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LSNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                        </td>
                                        <td>
                                            <%# Eval("COLLEGE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("STAFFTYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("APPOINT")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVE1")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVE2")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVE3")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVE4")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVE5")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvLeaveSeq" PageSize="1000"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
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
            </div>
        </div>
    </div>


    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />

    <div class="col-md-12">
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
            <div class="text-center">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                        <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                        <div class="text-center">
                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-outline-primary" />
                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-outline-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

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

