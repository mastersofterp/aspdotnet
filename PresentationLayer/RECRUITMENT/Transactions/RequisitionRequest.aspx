<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="RequisitionRequest.aspx.cs" Inherits="RECRUITMENT_Transactions_RequisitionRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
        .dataTables_scrollHeadInner {
        width: max-content!important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Requisition Request</h3>
                        </div>

                        <div class="box-body">
                         <asp:Panel ID="pnlAdd" runat="server">
                         <div class="col-12">
                                    <div class="row">
                                         <%--<div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Requisition Request</h5>
                                            </div>
                                        </div>--%>
                                                <div id="Div1" class="form-group col-lg-3 col-md-6 col-12"  runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Requisition No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtReqNo" runat="server" CssClass="form-control" ToolTip="Activity Code"
                                                        Width="100%" MaxLength="100" Enabled="false" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvReqNo" runat="server"
                                                        ControlToValidate="txtReqNo" Display="None"
                                                        ErrorMessage="Please Enter Requisition No" SetFocusOnError="true"
                                                        ValidationGroup="submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Department" data-select2-enable="true"
                                                TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" InitialValue="0" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Post Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlpostType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" ToolTip="Select Post Type" data-select2-enable="true"
                                                TabIndex="3" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                               <asp:ListItem Value="1">Teaching</asp:ListItem>
                                               <asp:ListItem Value="2">Non Teaching</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPostType" runat="server" InitialValue="0" ControlToValidate="ddlpostType"
                                                Display="None" ErrorMessage="Please Select Post Type" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Post</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPost" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" ToolTip="Select Post" data-select2-enable="true"
                                                TabIndex="3" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPost" runat="server" InitialValue="0" ControlToValidate="ddlPost"
                                                Display="None" ErrorMessage="Please Select Post" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>No. of Position</label>
                                            </div>
                                            <asp:TextBox ID="txtNoofPosition" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"  MaxLength="10" onKeyUp="validateNumeric(this)"
                                                TabIndex="4" ToolTip="Please Enter No. of Position" >
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNoofPosition" ControlToValidate="txtNoofPosition" runat="server" ErrorMessage="Please Enter No. of Position" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Description</label>
                                            </div>
                                            <asp:TextBox ID="txtDescription" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"  MaxLength="500" 
                                                TabIndex="4" ToolTip="Please Enter Description" TextMode="MultiLine">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDescription" ControlToValidate="txtDescription" runat="server" ErrorMessage="Please Enter Description" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Requisition Approval levels</label>
                                            </div>
                                            <asp:TextBox ID="txtReqAppLvl" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"  MaxLength="500" 
                                                TabIndex="4" ToolTip="Budget Passing Path" TextMode="MultiLine" Enabled="false">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReqAppLvl" ControlToValidate="txtReqAppLvl" runat="server" ErrorMessage="Please Enter Requisition Approval levels" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        </div>
                             </asp:Panel>
                            <div class="col-12 btn-footer mt-4">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" ToolTip="Click Add New to Add Requisition Request" Text="Add New" TabIndex="10" CssClass="btn btn-primary"></asp:LinkButton>
                                    <%--<asp:Button ID="btnShowReport" TabIndex="11" runat="server" Text="Show Report" CssClass="btn btn-outline-info"
                                        OnClick="btnShowReport_Click" ToolTip="Click here to Show the report" Style="display: none" />--%>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlbtn" runat="server">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="12" ToolTip="Click here to Submit" ValidationGroup="submit"
                                        OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnBack" runat="server" TabIndex="14" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" 
                                       OnClick="btnBack_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="13" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </asp:Panel>
                            </div>
                             
                            <div class="col-12">
                                <asp:Panel ID="pnlRequiReqList" runat="server">
                                  <div class="sub-heading">
                                      <h5>Requisition Request List</h5>
                                  </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                        <asp:Repeater ID="lvRequiReq" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue" >
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Requisition No
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Post Type
                                                        </th>
                                                         <th>Post
                                                        </th>
                                                        <th>Request Date
                                                        </th>
                                                        <th>No. of Position
                                                        </th>
                                                       <%--  <th>Description
                                                        </th>--%>
                                                        <th>Approval Status
                                                        </th>
                                                        <th>Approved Date
                                                        </th>
                                                        <th>Approval Remark
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("REQ_ID") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record"  TabIndex="15" OnClick="btnEdit_Click" />
                                                        <asp:ImageButton ID="btnDelete" Visible="true" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("REQ_ID") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REQUISITION_NO")%>                                                                 
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPT_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("POSTTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("POST")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REQUEST_DATE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NO_OF_POSITION")%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("DESCRIPTION")%>
                                                    </td>--%>
                                                       <td>
                                                        <%# Eval("APPROVAL_STATUS")%>
                                                    </td> 
                                                    <td>
                                                        <%# Eval("APPROVED_DATE")%>
                                                    </td>
                                                       <td>
                                                        <%# Eval("FINAL_REMARK")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                             </div>
                          </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
           <%-- <asp:PostBackTrigger ControlID="btnShowReport" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

    <div id="divMsg" runat="server">
    </div>

   <%-- <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />--%>
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete?
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
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>
     <script type="text/javascript">
         function lettersOnly() {
             var charCode = event.keyCode;

             if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8))

                 return true;
             else
                 return false;
             alert("Only Alphabets allowed");
         }
            </script>
</asp:Content>