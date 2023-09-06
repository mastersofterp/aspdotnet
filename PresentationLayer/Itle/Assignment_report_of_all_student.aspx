<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Assignment_report_of_all_student.aspx.cs" Inherits="Itle_Assignment_report_of_all_student" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>





<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <link href="../CSS/master.css" rel="stylesheet" />

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" style="width: 100%">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGNMENT RESULT REPORT OF ALL STUDENT</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body" style="height:480px;" >
                                    <div class="col-md-12">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        <asp:Panel ID="panelStudent" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Assignment Report Of All Student</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">



                                                           <div class="col-md-2">
                                                            <label>Session&nbsp;&nbsp;:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                             <asp:ListBox ID="ddlsession" runat="server" AppendDataBoundItems="true"   CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>  
                                   
                                                          
                                                        </div>
                                                    </div>
                                                  
                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-2">
                                                            <label></label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnSubmit" runat="server" Visible="false" CssClass="btn btn-primary" Text="Show Result" TabIndex="2"
                                                                OnClick="btnSubmit_Click" ToolTip="Clcik here to Show Result" />&nbsp

                                                            <asp:Button ID ="btnexport" runat ="server"  CssClass="btn btn-primary" Text="Export" OnClick ="btnexport_Click" ToolTip="Click Here To Export To Excel." />
                                                            <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" 
                                                                TabIndex="3" ToolTip="Click here to Reset" />
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
                TargetControlID="div" PopupControlID="div"
                OkControlID="btnOkDel" OnOkScript="okDelClick();"
                CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
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

                function GetnName(source, eventArgs) {
                    var idno = eventArgs.get_value().split("*");
                    var Name = idno[0].split("-");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Name').value = idno[1];
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value = Name[0];
                    document.getElementById('ctl00_ContentPlaceHolder1_hdn1').value = idno[2];
                }

                function GetName(source, eventArgs) {
                    var idno = eventArgs.get_value().split("*");
                    var Name = idno[0].split("-");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Name').value = Name[0];
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value = idno[1];
                    document.getElementById('ctl00_ContentPlaceHolder1_hdn2').value = idno[2];
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

             <script type="text/javascript">
                 $(document).ready(function () {
                     $('.multi-select-demo').multiselect({
                         includeSelectAllOption: true,
                         maxHeight: 200
                     });
                 });
                 var parameter = Sys.WebForms.PageRequestManager.getInstance();
                 parameter.add_endRequest(function () {
                     $('.multi-select-demo').multiselect({
                         includeSelectAllOption: true,
                         maxHeight: 200
                     });
                 });


    </script>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnexport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


