<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Complaint_Department.aspx.cs"
    Inherits="REPAIR_AND_MAINTENANCE_MASTER_Complaint_Department" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="updComplaintDepartment" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SERVICE DEPARTMENT</h3>
                        </div>
                        <div class="box-body">
                          
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">

                                         <div class="sub-heading">
                                                <h5>Add/Edit Service Department</h5>
                                            </div>
                                      
                                        <div class="row">
                                           
                                          
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label> Service Department </label>
                                            </div>                                                                                          
                                                    <asp:DropDownList ID="ddlDepartment" Enabled="true" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true" >                                                          
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvserviceDept" runat="server" 
                                                        ControlToValidate="ddlDepartment" Display="None" ErrorMessage="Please select Department Name."
                                                        ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                          
                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Service Code </label>
                                            </div>
                                                    <asp:TextBox ID="txtDeptCode" runat="server" CssClass="form-control" MaxLength="6"  TabIndex="1" 
                                                     onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDCode" runat="server" ControlToValidate="txtDeptCode" Display="None"
                                                        ErrorMessage="Please Enter Department Code." ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtDeptCode" FilterType="Custom, LowercaseLetters,UppercaseLetters"
                                                        ValidChars="">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Service Provider </label>
                                            </div>
                                            <asp:CheckBox ID="chkServiceProvider" runat="server" OnCheckedChanged="chkServiceProvider_CheckedChanged" TabIndex="1" />
                                                </div>
                                             </div>
                                          </div>
                                       <div class=" col-12 btn-footer">
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                        ValidationGroup="complaint" CssClass="btn btn-primary" TabIndex="1" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Cancel" OnClick="btnreset_Click"
                                                        CssClass="btn btn-warning" TabIndex="1" />
                                                    <asp:ValidationSummary ID="valsumEng" runat="server" DisplayMode="List" ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                          <div class="col-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                           <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <asp:Repeater ID="Repeater_Complainttype" runat="server" OnItemCommand="Repeater_Complainttype_ItemCommand">
                                                            <HeaderTemplate>
                                                                <div id="rpdiv">
                                                                    <div class="sub-heading"><h5>SERVICE DEPARTMENT LIST</h5></div>
                                                                   
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>EDIT
                                                                                </th>
                                                                                <th>SERVICE DEPARTMENT NAME
                                                                                </th>
                                                                                <th>SERVICE DEPARTMENT CODE
                                                                                </th>
                                                                                 <th>SERVICE PROVIDER
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                          <%--  <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>--%>
                                                                </div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("DEPTID")%>' CommandName="edit" ImageUrl="~/Images/edit.png" ToolTip="Edit Record" />
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblcomplainttype" runat="server" Text=' <%#Eval("DEPTNAME")%>' Style="text-align: left;"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDeptCode" runat="server" Text=' <%#Eval("DEPTCODE")%>' Style="text-align: left;"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblServiceProvider" runat="server" Text='<%#Eval("FLAG_SP").ToString() == "1" ? "YES" : "NO"%>' Style="text-align: left;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>

                                                            <%--<AlternatingItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td style="width: 2%; text-align: center">
                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("DEPTID")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Edit Record" />
                                                                    </td>

                                                                    <td style="width: 50%; text-align: center;">
                                                                        <asp:Label ID="lblcomplainttype" runat="server" Text=' <%#Eval("DEPTNAME")%>' Style="text-align: left;"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 50%; text-align: center;">
                                                                        <asp:Label ID="lblDeptCode" runat="server" Text=' <%#Eval("DEPTCODE")%>' Style="text-align: left;"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </AlternatingItemTemplate>--%>

                                                            <FooterTemplate>
                                                                </tbody>
                                                           
                                                            </FooterTemplate>

                                                        </asp:Repeater>
                                                          </table>
                                                    </asp:Panel>
                                                </div>
                                        
                                       
                                  
                                </asp:Panel>
                                <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                                    runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                                    OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                                    BackgroundCssClass="modalBackground" />
                                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                    <div class="text-center">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                         <%--       <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />--%>
                                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                                <div class="text-center">
                                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <%--<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                    <div style="text-align: center">
                                        <div class="form-group row">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                                </div>
                                                <div>
                                                    &nbsp;&nbsp;Are you sure you want to delete this record?
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnOkDel" runat="server" CssClass="btn btn-success" Text="Yes" Width="50px" />
                                                    <asp:Button ID="btnNoDel" runat="server" CssClass="btn btn-danger" Text="No" Width="50px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>--%>
                           
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

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

    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>

