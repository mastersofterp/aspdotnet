<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Search.aspx.cs" Inherits="DCMNTSCN_Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DOCUMENTS SEARCH</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlSearch" runat="server">
                            <div class="row">
                              <%--  <div class="col-12">
                                    <div class="sub-heading">
                                    <h5>Search</h5>
                                </div>
                                </div>--%>
                                
                                <div class="form-group col-lg-4 col-md-6 col-12" id="trr" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButtonList ID="rbtn" RepeatDirection="Horizontal" AutoPostBack="true" runat="server"
                                        OnSelectedIndexChanged="rbtn_SelectedIndexChanged" TabIndex="1" ToolTip="Select Search Criteria">
                                        <asp:ListItem Text="Search By Title" Selected="True" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Search By Category" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Search By Keyword" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="TrTitle" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Search</label>
                                    </div>
                                    <asp:TextBox ID="txtSearch"  CssClass="form-control" ToolTip="Enter Search String"
                                        TabIndex="2" runat="server" onkeydown = "return (event.keyCode!=13);">
                                    </asp:TextBox>
                                    <asp:HiddenField ID="hfSearch" runat="server" />
                                    <ajaxToolKit:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server"
                                        Enabled="True" ServicePath="~/Autocomplete.asmx" TargetControlID="txtSearch"
                                        CompletionSetCount="6" ServiceMethod="GetDocumentKeyword" MinimumPrefixLength="1"
                                        CompletionInterval="0" CompletionListCssClass="autocomplete_completionListElement"
                                        OnClientItemSelected="SaveID" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="autocomplete_listItem">
                                    </ajaxToolKit:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch"
                                        Display="None" ErrorMessage="Enter Title to search" SetFocusOnError="true" ValidationGroup="Search">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" visible="false" id="TrKey" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Search</label>
                                    </div>
                                    <asp:TextBox ID="txtKey"  CssClass="form-control" ToolTip="Enter Search String"
                                        runat="server" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvKeyw" runat="server" ControlToValidate="txtKey"
                                        Display="None" ErrorMessage="Enter Keyword to search" SetFocusOnError="true"
                                        ValidationGroup="Search"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="TrCat" visible="false" runat="server">
                                    <div id="divDesc" runat="server" class="vista-grid" tabindex="4">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Category</label>
                                            <asp:TreeView ID="tv" OnTreeNodePopulate="pp" runat="server" OnSelectedNodeChanged="tv_SelectedNodeChanged">
                                            </asp:TreeView>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>

                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search" CssClass="btn btn-primary"
                                        OnClick="btnSearch_Click" ToolTip="Click here to Search" TabIndex="5"  />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Search"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:Panel ID="pnlDocList" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Document List</h5>
                            </div>
                            <asp:Panel ID="pnlDocument" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvSearch" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Title
                                                        </th>
                                                        <th>Created Date
                                                        </th>
                                                        <th>Uploaded By
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Action
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
                                                <%# Eval("TITLE") %>
                                            </td>
                                            <td>
                                                <%# Eval("CREATED_DATE","{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("DOCUMENTNAME")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSelect" runat="server" CommandArgument='<%# Eval("UPLNO") %>' CssClass="btn btn-primary"
                                                    Text="Select" ToolTip="Click to See Details" OnClick="btnSelect_Click" TabIndex="6" />
                                                <asp:HiddenField ID="hfDno" runat="server" Value='<%# Eval("DNO") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlDetails" runat="server">
                       <div class="col-12">
                        <div class="row">
                            
                                <div class="sub-heading">
                                    <h5>Document Details</h5>
                                </div>
                            
                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Created Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDate" runat="server"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>Category :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblCategory" runat="server"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Uploaded By  :</b>

                                        <a class="sub-label">
                                            <asp:Label ID="lblUploadBy" runat="server"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Title :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>Description :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label></a>
                                    </li>

                                </ul>
                            </div>

                            <div class="col-lg-4 col-md-6 col-12">
                                <asp:ListView ID="lvAttachments" runat="server">
                                    <LayoutTemplate>
                                        <table>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <label>Attachment :</label>
                                                <%--<img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%# Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME") %>">
                                                    <%# Eval("ORIGINAL_FILENAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1).ToString() %>&nbsp;KB)
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="col-12 btn-footer ">
                             
                                    <asp:Button ID="btnClear" runat="server" Text="Start New Search" CssClass="btn btn-warning"
                                        OnClick="btnClear_Click" TabIndex="7" ToolTip="Click here to Reset" />
                             
                            </div>
                        </div>
                    </div>
                    </asp:Panel>


                </div>
            </div>
         
        </div>
    </div>
    
   
    <script language="javascript" type="text/javascript">

      

        // show more file upload 
        function ShowHideFileUpload(id) {
            document.getElementById('divShowMore').style.display = '';
            document.getElementById(id).style.display = 'none';
        }

        function SaveID(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearch').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_hfSearch').value = idno[1];
            //ContentPlaceHolder1_hfSearch
        }

        function ShowImage() {

            document.getElementById('ctl00_ContentPlaceHolder1_txtAuthor1').style.backgroundImage = 'url(../images/ajax-loader.gif)';

            document.getElementById('ctl00_ContentPlaceHolder1_txtAuthor1').style.backgroundRepeat = 'no-repeat';

            document.getElementById('ctl00_ContentPlaceHolder1_txtAuthor1').style.backgroundPosition = 'right';
            alert(document.getElementById('ctl00_ContentPlaceHolder1_txtAuthor1').style.backgroundImage);
        }
        function HideImage() {

            document.getElementById('ctl00_ContentPlaceHolder1_txtAuthor1').style.backgroundImage = 'none';
        }

        function ShowIcon() {
            var e = document.getElementById('ctl00_ContentPlaceHolder1_ImageLoader');
            e.style.visibility = (e.style.visibilit == 'visible') ? 'hiden' : 'visible';
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
    
</asp:Content>
