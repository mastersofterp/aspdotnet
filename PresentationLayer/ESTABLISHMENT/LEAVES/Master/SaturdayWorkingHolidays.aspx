<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaturdayWorkingHolidays.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_SaturdayWorkingHolidays"
    MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SATURDAY AS WORKING DAY ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Working Day Details</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                            <div class="label-dynamic">
                                                <label>Is Employee Wise Saturday</label>
                                            </div>
                                            <asp:CheckBox ID="chkEmp" runat="server" TabIndex="12"
                                                ToolTip="Select to Add Employee Wise Saturday" OnCheckedChanged="chkEmp_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" ToolTip="Select Staff Type" TabIndex="2"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Working Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCaldt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtdt" runat="server" AutoPostBack="true" MaxLength="10" Style="z-index: 0;" TabIndex="1"
                                                    OnTextChanged="txtdt_TextChanged" CssClass="form-control" ToolTip="Please select Working Date" />
                                                <ajaxToolKit:CalendarExtender ID="Cedt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCaldt" TargetControlID="txtdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meedt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtdt" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevdt" runat="server" ControlExtender="meedt" ControlToValidate="txtdt"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Working Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                    TooltipMessage="Please Select Working Date" ValidationGroup="Leave">
                                                </ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator ID="rfvdt" runat="server"
                                                    ControlToValidate="txtdt" Display="None" ErrorMessage="Please Enter Working Date"
                                                    SetFocusOnError="true" ValidationGroup="Leave"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="500" Text="" ToolTip="Enter Remark" TabIndex="3"
                                                CssClass="textbox form-control" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvremark" runat="server" ControlToValidate="txtRemark"
                                                Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Leave"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Is Working</label>
                                            </div>
                                            <asp:CheckBox ID="chkIsworking" runat="server" TabIndex="4"
                                                ToolTip="Check for working day" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer" id="Tr1" align="center" runat="server">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" CssClass="btn btn-primary"></asp:LinkButton>
                                <asp:Button ID="btnShowReport" runat="server" Text=" Report" ToolTip="Click here to show the Report" CausesValidation="false" OnClick="btnShowReport_Click"
                                    CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Submit"
                                    ValidationGroup="Leave" TabIndex="5" ToolTip="Click here to Submit" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false"
                                    OnClick="btnCancel_Click" Text="Cancel" TabIndex="6" ToolTip="Click here to Reset" CssClass="btn btn-warning" />

                                <asp:Button ID="btnBack" runat="server" CausesValidation="false"
                                    OnClick="btnBack_Click" Text="Back" TabIndex="7" ToolTip="Click to go to previous" CssClass="btn btn-info" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="Leave" />
                                <asp:Button ID="btnReport" runat="server" Text="Employee Wise Report" ToolTip="Click here to show the Report" CausesValidation="false"
                                    CssClass="btn btn-info" OnClick="btnReport_Click" Visible="false" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlDeptList" runat="server">
                                    <%--ScrollBars="Auto" Style="height: 300px;"--%>
                                    <asp:ListView ID="lvDept" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Department List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <%--class="table table-striped table-bordered nowrap display"--%>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>
                                                            <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />&nbsp Select
                                                        </th>
                                                        <th>Department
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
                                                <td><%#Container.DataItemIndex+1 %></td>
                                                <td>
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("SUBDEPTNO") %>' />
                                                    <asp:HiddenField ID="hdfsubdeptno" runat="server" Value='<%# Eval("SUBDEPTNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SUBDEPT")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlEmpList" runat="server">
                                    <%--ScrollBars="Auto" Style="height: 300px;"--%>
                                    <asp:ListView ID="lvEmployee" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Employee List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                       <%-- <th>Sr.No
                                                        </th>--%>
                                                        <th>
                                                            <asp:CheckBox ID="cbEmp" runat="server" onclick="totAllEmployee(this)" />&nbsp Select
                                                        </th>
                                                        <th>
                                                            Department
                                                        </th>
                                                        <th>Employee Name
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
                                                <%--<td><%#Container.DataItemIndex+1 %></td>--%>
                                                <td>
                                                    <asp:CheckBox ID="chkEmpSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SUBDEPT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EMPNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>
                            <%--<asp:Panel ID="pnlList" runat="server">--%>
                            <div class="col-md-12 table-responsive" id="divworking" runat="server" visible="false">
                                <asp:ListView ID="lvsatwrk" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Working Holiday Entry</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Working Date
                                                    </th>
                                                    <th>Staff Type
                                                    </th>
                                                    <th>Remark
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SWDID") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />

                                            </td>
                                            <td>
                                                <%# Eval("WORKINGDATE") %>
                                            </td>
                                            <td>
                                                <%# Eval("STAFFTYPE") %>
                                            </td>
                                            <td>
                                                <%# Eval("REMARK") %>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <%--<div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvsatwrk">
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
                                </div>--%>
                                <%-- </asp:Panel>--%>
                            </div>


                            <div class="col-md-12 table-responsive" id="divworkingEmployee" runat="server" visible="false">
                                <asp:ListView ID="LstEmployeewise" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Working Holiday Entry</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Working Date
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Remark
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
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif" CommandArgument='<%# Eval("SWDID") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click" TabIndex="7" />

                                            </td>
                                            <td>
                                                <%# Eval("WORKINGDATE") %>
                                            </td>
                                            <td>
                                                <%# Eval("NAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("REMARK") %>
                                            </td>

                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>


    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>


    <script type="text/javascript">
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 44 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
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
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkSelect')) {
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

    <script type="text/javascript" language="javascript">
        function totAllEmployee(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkEmpSelect')) {
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




