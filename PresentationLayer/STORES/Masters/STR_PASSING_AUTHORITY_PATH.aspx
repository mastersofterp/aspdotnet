<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="STR_PASSING_AUTHORITY_PATH.aspx.cs" Inherits="STORES_Masters_STR_PASSING_AUTHORITY_PATH" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlMessage"
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
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="pnlMessage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PASSING AUTHORITY PATH</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Add/Edit</h5></div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanStage" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Stage Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstage" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Select Stage Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvLeavename" runat="server" ControlToValidate="ddlstage"
                                                Display="None" ErrorMessage="Please Select Stage Name" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanDept" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" TabIndex="2" ToolTip="Select Department" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPathFor" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Path For</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbPathFor" runat="server" RepeatDirection="Horizontal" >
                                                <asp:ListItem Value="P" Selected="True">Purchase&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="I">Issue</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Employee</label>
                                            </div>
                                            <asp:Label ID="lblEmpName" runat="server" class="form-control" Font-Bold="true" TabIndex="3"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanPA0">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Passing Authority 01</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA01" runat="server" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select Passing Authority 01"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddlPA01"
                                                Display="None" ErrorMessage="Please Select Passing Authority 01" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Passing Authority 02</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" TabIndex="4" ToolTip="Select Passing Authority 02"
                                                Enabled="false" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Passing Authority 03</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Passing Authority 03"
                                                Enabled="false" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Passing Authority 04</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" TabIndex="6" ToolTip="Passing Authority 04"
                                                Enabled="false" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Passing Authority 05</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" TabIndex="7" ToolTip="Enter Passing Authority 05"
                                                Enabled="false" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Path</label>
                                            </div>
                                            <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Path" ReadOnly="true" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlEmpList" runat="server" Visible="false">
                                        <div class="sub-heading"><h5>Employees</h5></div>
                                            <asp:Panel ID="pnlEmp" runat="server">
                                                <asp:ListView ID="lvEmployees" runat="server">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl"
                                                            Text="Employee Not Found!" />
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid">
                                                            <div class="sub-heading"><h5>List of Employees</h5></div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr.No
                                                                        </th>
                                                                        <th>
                                                                            <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" TabIndex="8" />
                                                                        </th>
                                                                        <th>Employee Name
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
                                                                <%#Container.DataItemIndex+1 %>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkIdNo" runat="server" ToolTip='<%# Eval("IDNO") %>' TabIndex="9" />
                                                                <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME")%>    ( <%# Eval("APLT")%>)
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAPath"
                                        OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="8" ToolTip="Click To Submit" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                        CssClass="btn btn-info" TabIndex="10" ToolTip="Click To Go Back" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="10" ToolTip="Click To Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary" Text="Add New" TabIndex="1" ToolTip="Click Add New To Enter Passing Authority Path"></asp:LinkButton>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:RadioButtonList ID="rdbListPathFor" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbListPathFor_SelectedIndexChanged" >
                                        <asp:ListItem Value="A" Selected="True">All&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="P">Purchase Paths&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="I">Issue Paths</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvPAPath" runat="server">
                                        <EmptyDataTemplate>
                                            <center>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                            </center>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading"><h5>Passing Authority Path Details</h5></div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Path For
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Passing Authority 01
                                                        </th>
                                                        <th>Passing Authority 02
                                                        </th>
                                                        <th>Passing Authority 03
                                                        </th>
                                                        <th>Passing Authority 04
                                                        </th>
                                                        <th>Passing Authority 05
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="javascript:return confirm('Are You Sure You Want To Delete This Record?')" />
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>                                                           
                                                </td>
                                                <td>                                                           
                                                    <%# Eval("PATH_FOR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBDEPT")%>
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
                                    <%-- <div class="vista-grid_datapager text-center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPAPath" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkIdNo')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

    </script>

</asp:Content>
