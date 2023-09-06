<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Student_Assignment_result.aspx.cs" Inherits="Itle_Student_Assignment_result" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ASSIGNMENT RESULT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12 mb-3">
                        <asp:Panel ID="pnlStudent" runat="server">
                            <div class="row">
                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session Term :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-7 col-md-12 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Subject Name:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCourseName" Font-Bold="true" runat="server"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudentAssignmentList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvRewsult" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Assignment Result</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Assignment Topic
                                                        </th>
                                                        <th>Total Marks
                                                        </th>
                                                        <th>Marks Obtained
                                                        </th>
                                                        <th>Assignment Date
                                                        </th>
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
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ASSIGNMENT_MARKS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENT_MARKS")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("ASSIGNDATE", "{0:MMMM d, yyyy H:mm:ss}") %>' />
                                                    <%--<%# Eval("TESTDATE")%>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server"></div>

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

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function totAllIDs(headchk) {
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
        //    function totAllIDs(headchk) {
        //      

        //        var frm = document.forms[0]
        //        for (i = 0; i < document.forms[0].elements.length; i++) {
        //            var e = frm.elements[i];
        //            if (e.type == 'checkbox') {
        //                if (e.name.endsWith('chkAccept')) {
        //                    if (headchk.checked == true) {
        //                        e.checked = true;
        //                        
        //                    }
        //                    else
        //                        e.checked = false;

        //                }
        //            }
        //        }


        //            var frm = document.forms[0]
        //            for (i = 0; i < document.forms[0].elements.length; i++) {
        //                var e = frm.elements[i];
        //                if (e.type == 'checkbox') {
        //                    if (headchk.checked == true)
        //                        e.checked = true;
        //                   
        //                    else
        //                        e.checked = false;
        //                }
        //            }



    </script>

    <%--  Enable the button so it can be played again --%>
</asp:Content>
