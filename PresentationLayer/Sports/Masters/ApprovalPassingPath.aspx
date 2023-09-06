<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApprovalPassingPath.aspx.cs" Inherits="Sports_Masters_ApprovalPassingPath" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <%-- <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();
        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });
        }
    </script>--%>

    <%--  <script src="../../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../../Content/jquery.dataTables.js" type="text/javascript"></script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Event Approval Authority Path</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="PAPath" SetFocusOnError="true"
                                                InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEvent" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Event</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEvent" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Event" TabIndex="5">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ControlToValidate="ddlEvent"
                                                Display="None" ErrorMessage="Please Select Event." SetFocusOnError="true" ValidationGroup="PAPath" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" runat="server" style="color: #FF0000;" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Event Name</label>
                                            </div>
                                            <asp:Label ID="lblEventName" runat="server" CssClass="form-control" Font-Bold="true"></asp:Label>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Approval Authority 01</label>
                                               <%-- <asp:Image ID="darrow1" runat="server" ImageUrl="~/images/action_down.png" />--%>

                                            </div>
                                            <asp:DropDownList ID="ddlPA01" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Authority 01"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged" TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddlPA01"
                                                Display="None" ErrorMessage="Please select Passing Authority 01" SetFocusOnError="true" ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Approval Authority  02  </label>
                                               <%-- <asp:Image ID="darrow2" runat="server" ImageUrl="~/images/action_down.png" />--%>
                                            </div>
                                            <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Authority 02"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged1" TabIndex="7">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Approval Authority  03</label>
                                               <%-- <asp:Image ID="darrow3" runat="server" ImageUrl="~/images/action_down.png" />--%>
                                            </div>
                                            <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Authority 03"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged1" TabIndex="8">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Approval Authority  04 </label>
                                                <%--<asp:Image ID="darrow4" runat="server" ImageUrl="~/images/action_down.png" />--%>
                                            </div>
                                            <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Authority 04"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged" TabIndex="9">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Approval Authority  05 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Authority 04"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged" TabIndex="10">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Path</label>
                                            </div>
                                            <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-4 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note (Please Select)</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Selection of 'Approval Authority 01' is mandatory.</span> </p>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer" id="divSubmit" runat="server" visible="false">

                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAPath" OnClick="btnSave_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="11" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" CssClass="btn btn-primary" ToolTip="Click here to Back" TabIndex="13" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" TabIndex="12" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            </div>


                            <div id="divAddNew" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollegeGrid" runat="server" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeGrid_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">

                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary" ToolTip="Click To Add New Authority Path" Text="Add New" TabIndex="1"></asp:LinkButton>
                                    <asp:Button ID="btnShowReport" runat="server" Text="Report" OnClick="btnShowReport_Click" CssClass="btn btn-info" ToolTip="Click here to Show the report" TabIndex="2" />

                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="rptPathList" runat="server">
                                        <LayoutTemplate>
                                            <div class="vista-grid">
                                                <div class="sub-heading">
                                                    <h5>APPROVAL AUTHORITY PATH LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>Action
                                                            </th>
                                                            <th>Institute
                                                            </th>
                                                            <th>Event Name
                                                            </th>
                                                            <th>Approval Authority 01
                                                            </th>
                                                            <th>Approval Authority 02
                                                            </th>
                                                            <th>Approval Authority 03
                                                            </th>
                                                            <th>Approval Authority 04
                                                            </th>
                                                            <th>Approval Authority 05
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EVENTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME1")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME2")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME3")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME4")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME5")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlCollege" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground">
    </ajaxToolKit:ModalPopupExtender>
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
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            var j = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                //ctl00_ContentPlaceHolder1_chkScaleStatus

                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (j != 0)//to not made any effect on chkStatus checkbox--swati-15-12-14
                        {
                            e.checked = true;

                        }
                        j++;
                    }
                    else
                        e.checked = false;
                }

            }

        }
    </script>

</asp:Content>

