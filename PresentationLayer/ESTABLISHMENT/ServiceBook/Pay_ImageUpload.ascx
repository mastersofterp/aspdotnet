<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_ImageUpload.ascx.cs"
    Inherits="PayRoll_Pay_ImageUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1 {
        width: 443px;
    }
</style>

<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Image Upload</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-6">
                                        <label><span style="color: #FF0000">*</span>Image Type :</label>
                                        <asp:DropDownList ID="ddlImageType" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                            OnSelectedIndexChanged="ddlImageType_SelectedIndexChanged" CssClass="form-control" ToolTip="Select Image Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvImageType" runat="server"
                                            ControlToValidate="ddlImageType" Display="None"
                                            ErrorMessage="Please select Image Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="ServiceBook">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <asp:Image ID="Image1" runat="server" BorderColor="White"
                                            ImageUrl="~/IMAGES/nophoto.jpg" Width="60%" Height="95%" />
                                        <%--<asp:Image ID="Image1" runat="server" BorderColor="White" Height="100"
                                            ImageUrl="~/IMAGES/nophoto.jpg" Width="100" />--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label><span style="color: #FF0000">*</span>Upload Image :</label>
                                        <asp:FileUpload ID="fuUploadImage" runat="server" TabIndex="5" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <br />
                                        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" TabIndex="6" CssClass="btn-primary"
                                            Text="Upload" ValidationGroup="ServiceBook" ToolTip="Click here to Upload Image" />
                                    </div>


                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="6"
                                                Text="Submit" ValidationGroup="ServiceBook" CssClass="btn btn-success" ToolTip="Click here to Submit" />
                                            &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" TabIndex="7"
                                            OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="ServiceBook" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnlImage" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvEmpImage" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text=" No Rows In Emp Image"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Employee Image
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <%--  <th width="10%">
                                                        Image Id
                                                     </th>--%>
                                                    <th>Image Type
                                                    </th>
                                                    <th>Image 
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("imageTrxId")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("imageTrxId") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <%-- <td width="10%">
                                        <%# Eval("imageid")%>
                                    </td>--%>
                                        <td>
                                            <%# Eval("imagetype")%>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgPhoto" Height="50px" Width="80px"
                                                runat="server" ImageUrl="~/IMAGES/nophoto.jpg" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td valign="top" width="50%">
            <%--<asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Image Upload</legend>
                    <br />

                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label" style="width: 20%">Image Type<span style="color: #FF0000">*</span></td>

                            <td><b>:</b></td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlImageType" runat="server" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="ddlImageType_SelectedIndexChanged" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvImageType" runat="server"
                                    ControlToValidate="ddlImageType" Display="None"
                                    ErrorMessage="Please select Image Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="ServiceBook">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="form_left_text" rowspan="5">
                                <asp:Image ID="Image1" runat="server" BorderColor="White" Height="100"
                                    ImageUrl="~/IMAGES/nophoto.jpg" Width="100" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Upload Image <span style="color: #FF0000">*</span> </td>
                            <td><b>:</b></td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="fuUploadImage" runat="server" />
                                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click"
                                    Text="Upload" ValidationGroup="ServiceBook" />


                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>

                            <td colspan="3"></td>
                        </tr>
                        <tr>

                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                    Text="Submit" ValidationGroup="ServiceBook" Width="80px" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false"
                                    OnClick="btnCancel_Click" Text="Cancel" Width="80px" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="ServiceBook" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
        </td>
        <td colspan="2" align="center" valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <%--<asp:ListView ID="lvEmpImage" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text=" No Rows In Emp Image"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Employee Image
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>--%>
                        <%--Already  Committed<th width="10%">
                                                    Image Id
                                                </th>--%>
                        <%--<th width="10%">Image Type
                                                </th>
                                                <th width="10%">Image 
                                                </th>
                                            </tr>
                                            <thead>
                                    </table>
                                </div>
                                <div class="listview-container-servicebook">
                                    <div id="Div1" class="vista-gridServiceBook">
                                        <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%" align="left">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("imageTrxId")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("imageTrxId") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>--%>
                        <%--Already Committed<td width="10%">
                                        <%# Eval("imageid")%>
                                    </td>--%>
                        <%--<td width="10%">
                                        <%# Eval("imagetype")%>
                                    </td>
                                    <td width="10%">
                                        <asp:Image ID="imgPhoto" Height="50px" Width="80px"
                                            runat="server" ImageUrl="~/IMAGES/nophoto.jpg" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("imageTrxId")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("imageTrxId") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>--%>
                        <%--Already Committed <td width="10%">
                                        <%# Eval("imageid")%>
                                    </td>--%>
                        <%--<td width="10%">
                                        <%# Eval("imagetype")%>
                                    </td>
                                    <td width="10%">
                                        <asp:Image ID="imgPhoto" Height="50px" Width="80px"
                                            runat="server" ImageUrl="~/IMAGES/nophoto.jpg" />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
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

