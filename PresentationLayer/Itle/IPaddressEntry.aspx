<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="IPaddressEntry.aspx.cs" Inherits="Itle_IPaddressEntry" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updIPAddress"
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
    <asp:UpdatePanel ID="updIPAddress" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">IP ADDRESS ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlIP" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlForumDetail" runat="server">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-6 col-12 mb-3" style="display: none;">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>session:</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>


                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-3 col-md-6 col-12 mb-3" style="display: none;">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label>


                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Computer Name</label>
                                                </div>
                                                <asp:TextBox ID="txtcompterName" runat="server" MaxLength="100"
                                                    CssClass="form-control" TabIndex="1" ToolTip="Enter Computer Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtcompterName" ErrorMessage="Enter the Computer Name"
                                                    ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>IP Address</label>
                                                </div>
                                                <asp:TextBox ID="txtipaddress" runat="server" MaxLength="100"
                                                    CssClass="form-control" TabIndex="2" ToolTip="Enter IP Address"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtipaddress" ErrorMessage="Enter the IP Address"
                                                    ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnsubmitentry" runat="server" Text="Submit" OnClick="btnsubmitentry_Click" TabIndex="3"
                                                ValidationGroup="submit" CssClass="btn btn-primary" ToolTip="Click here to Submit"></asp:Button>
                                            <asp:Button ID="btnresetentry" runat="server" Text="Cancel" TabIndex="4" CssClass="btn btn-warning"
                                                OnClick="btnresetentry_Click" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                DisplayMode="List" ValidationGroup="submit" />

                                        </div>

                                        <div class="col-12">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl" Font-Bold="True"
                                                ForeColor="#FF9900" Font-Size="Large"></asp:Label>
                                            <asp:Label ID="lblError" runat="server" Visible="False"
                                                EnableViewState="False" SkinID="Msglbl">Error: Entry is not created. Please fill all the fields.</asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" runat="server" id="fldipadd">
                                    <div class="sub-heading">
                                        <h5>IP Address List</h5>
                                    </div>
                                    <asp:Panel ID="PnlReader" runat="server">
                                        <asp:ListView ID="lvIpAdd" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Computer Name </th>
                                                            <th>IP Address</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgbtnedit" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/edit1.png"
                                                            CommandArgument='<%#Eval("SRNO")%>' ToolTip='<%#Eval("SRNO")%>' OnClick="btnEdit_Click" />
                                                        <asp:ImageButton ID="imgdelete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/delete.png"
                                                            OnClientClick="showConfirmDel(this); return false;" CommandArgument='<%#Eval("SRNO")%>'
                                                            ToolTip='<%#Eval("SRNO")%>' OnClick="btnDelete_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcomputername" runat="server" Text='<%#Eval("COMPUTERNAME")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblipaddress" runat="server" Text='<%#Eval("IPADD")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>

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
            </script>

            <script type="text/javascript">


                function chkconfirm() {
                    var result = confirm('Are You Sure To Delete?');
                    if (result == true) {

                    }
                    else {
                        return false;
                    }
                }
    
    
    
                function ValidateIPaddress(inputText)  
                {  
                    var ipformat = /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;  
                    if(inputText.value.match(ipformat))  
                    {  
                        document.frmMaster.txtipaddress.focus();  
                        return true;  
                    }  
                    else  
                    {  
                        alert("You have entered an invalid IP address!");  
                        document.frmMaster.txtipaddress.focus();<br>return false;  
                    }  
                }  
    
    
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
