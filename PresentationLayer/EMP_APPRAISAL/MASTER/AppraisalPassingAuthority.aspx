<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalPassingAuthority.aspx.cs" Inherits="EmpAppraisal_AppraisalPassingAuthority" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script src="../../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../../Content/jquery.dataTables.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAll"
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
    <style>
        #ctl00_ContentPlaceHolder1_div .div .modalPopup {
            padding: 0px!important;
        }
    </style>
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div6" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPRAISAL PASSING AUTHORITY</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <asp:UpdatePanel ID="updAdd" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>College Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" Width="100%" CssClass="form-control" data-select2-enable="true"
                                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                                InitialValue="0"></asp:RequiredFieldValidator>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Passing Authority</label>
                                                            </div>
                                                            <asp:TextBox ID="txtPAuthority" runat="server" MaxLength="50" CssClass="form-control datepickerinput" TabIndex="2" ToolTip="Enter Designation of Authority" />
                                                            <asp:RequiredFieldValidator ID="rfvPAuthority" runat="server" ControlToValidate="txtPAuthority"
                                                                Display="None" ErrorMessage="Please Enter Passing Authority" ValidationGroup="PAuthority"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md- col-12" id="div3" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>User</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="3"
                                                                ToolTip="Select User Name" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser"
                                                                Display="None" ErrorMessage="Please Select User" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                         <div class="form-group col-lg-3 col-md- col-12" id="div4" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Common Authority</label>
                                                            </div>
                                                            <asp:CheckBox ID="cbauthority" runat="server" AutoPostBack="true" />
                                                        </div>
                                                    </div>
                                                   
                                                </div>
                                                <div class="col-12 btn-footer" id="divSubmit" runat="server">
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAuthority" OnClick="btnSave_Click" TabIndex="4" ToolTip="Click to Save Record"
                                                        CssClass="btn btn-outline-primary" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="5" CssClass="btn btn-outline-danger"
                                                        OnClick="btnCancel_Click" ToolTip="Click to Clear Record" />
                                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" TabIndex="6" CssClass="btn btn-outline-primary"
                                                        onblur="return OnSetChange();" ToolTip="Click for Back To Main Page" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAuthority"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                                <div class="col-12">
                                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="pnlList" runat="server">
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-outline-primary" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New"></asp:LinkButton>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-md-3" id="divInstitute" runat="server">
                                                <label>Institute Name :</label>
                                                <asp:DropDownList ID="ddlCollegeGrid" runat="server"
                                                    AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCollegeGrid_SelectedIndexChanged" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-3 mb-3">
                                        <asp:ListView ID="lvPAuthority" runat="server">
                                            <EmptyDataTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Passing Authority" />
                                                </div>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <%--<h4 class="box-title">Passing Authority List</h4>--%>
                                                    <div class="sub-heading">
                                                        <h5>Passing Authority List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>College Name
                                                                </th>
                                                                <th>Passing Authority 
                                                                </th>
                                                                <th>User
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
                                                <tr class="item">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PANO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        <asp:ImageButton ID="btnDelete" Visible="true" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PANO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COLLEGE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PANO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                                    <asp:ImageButton ID="btnDelete" Visible="true" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PANO") %>'
                                                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COLLEGE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME")%>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
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
    <asp:Panel ID="div" runat="server" Style="display: none ;height:100px" Width="520" >
        <div style="text-align: center">
            <div class="modal-content" style="height:200px">
                <div class="modal-body">
                    <br />
                    <br />
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-outline-primary" TabIndex="16" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-outline-primary" TabIndex="17" />
                        <br />

                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <script type="text/javascript">

        function OnSetChange() {
            document.getElementById('<%=ddlCollege.ClientID %>').focus();

        }
    </script>

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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-info {
            text-align: center;
        }

        #divSubmit {
            text-align: center;
        }
    </style>
</asp:Content>


