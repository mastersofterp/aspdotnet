<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="news" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function checkDate(sender, args) {

            //Check if the date selected is less than todays date
            if (sender._selectedDate < new Date()) {
                //show the alert message
                alert("You cannot select a day earlier than today!");
                //set the selected date to todays date in calendar extender control
                sender._selectedDate = new Date();

                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

            }
        }
    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"><b>CREATE NEWS MANAGEMENT</b></h3>
                    <div class="box-tools pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                    </div>
                </div>                
                    <div class="box-body">             
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="col-md-3">
                                <label><span style="color:red;">*</span> News Title </label>
                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                    Display="None" ErrorMessage="Please Enter News Title." ValidationGroup="News">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTitle" runat="server" TargetControlID="txtTitle"
                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*+|=\{}[]:;<>">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-3">
                                <label>Link Name</label>
                                <asp:TextBox ID="txtLinkName" runat="server" MaxLength="60" CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <label><span style="color:red;">*</span> Expiry Date</label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal" runat="server" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtExpiryDate" runat="server" onchange="CheckDate(this);" CssClass="form-control" />
                                    <%--<asp:Image ID="imgDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                    <ajaxToolKit:CalendarExtender ID="ceExpDate" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtExpiryDate" PopupButtonID="dvcal" OnClientDateSelectionChanged="checkDate" >
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExpiryDate"
                                        Display="None" ErrorMessage="Please Select/Enter Expiry Date." ValidationGroup="News"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Upload File</label>
                                <asp:FileUpload ID="fuFile" runat="server" CssClass="form-control" /><asp:HiddenField ID="hdnFile" runat="server" />
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-4">
                                <label><span style="color:red;">*</span> Description</label>
                                <FTB:FreeTextBox ID="ftbDesc" runat="Server" Height="200px" Width="500px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ftbDesc"
                                    Display="None" ErrorMessage="Please Enter Description." ValidationGroup="News"></asp:RequiredFieldValidator>
                            </div>
                            <div class="box-footer col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                        ValidationGroup="News" CssClass="btn btn-success" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CausesValidation="False" CssClass="btn btn-danger" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="News" />
                                </p>
                                <div>
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" /></div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlList" runat="server">
                       
                                <div class="box-footer col-md-12">
                                    <p class="text-center">
                                         <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                                    </p>
                                </div>
                            <div class="col-md-12">
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvNews" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <h4>News List</h4>
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action</th>
                                                    <th>News Title</th>
                                                    <th>Link</th>
                                                    <th>Expiry Date</th>
                                                    <th>Status</th>
                                                </tr>  </thead>
                                                <tbody> <tr id="itemPlaceholder" runat="server" /></tbody>                                             
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                                    CommandArgument='<%# Eval("newsid") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"
                                                    CommandArgument='<%# Eval("newsid") %>' AlternateText="Delete Record" ToolTip="Delete Record"
                                                    OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td><%# Eval("Title") %></td>
                                            <td>
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("filename"))%>'><%#  GetFileName(Eval("filename"))%></asp:HyperLink></td>
                                            <td><%# Eval("Expiry_Date","{0:dd-MMM-yyyy}") %></td>
                                            <td><%# GetStatus(Eval("status")) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                    </asp:Panel>
                            </div> 
                                    <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvNews" PageSize="10"
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
                        </asp:Panel>

                        <div id="div2" runat="server">
                        </div>
                    </div>
                      
                <asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup">
                    <div style="text-align: center">
                        <table>
                            <tr>
                                <td align="center">
                                    <img align="middle" src="images/warning.gif" alt="" />
                                </td>
                                <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="Button1" runat="server" Text="Yes" Width="50px" CssClass="btn btn-warning"  />
                                    <asp:Button ID="Button2" runat="server" Text="No" Width="50px" CssClass="btn btn-danger" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center; background-color:darkgray;">
            <table>
                <tr>
                    <td align="center">
                        <img align="middle" src="images/warning.gif" alt="" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" CssClass="btn btn-warning" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" CssClass="btn btn-danger"/>
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

   
    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>

