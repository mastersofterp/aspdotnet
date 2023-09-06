<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalPassingAuthorityPath.aspx.cs" Inherits="EMP_APPRAISAL_AppraisalPassingAuthorityPath" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <script src="../../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../../Content/jquery.dataTables.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <%-- <img src="../images/ajax-loader.gif" alt="Loading" />
                Loading..--%>
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
                        <div id="div4" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Appraisal Passing Authority Path</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="PnlAdd2" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlsession" runat="server" CssClass="form-control" ToolTip="Select Institute" AppendDataBoundItems="false" Enabled="false"
                                                TabIndex="1" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCollege" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control datepickerinput" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="3" AutoPostBack="true" ToolTip="Select College Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcol" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAPath"
                                                SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trDept" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" ToolTip="Select Department" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" data-select2-enable="true" TabIndex="2"
                                                CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Reporting Authority 01</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA01" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Authority 01"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged" data-select2-enable="true" TabIndex="5">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddlPA01"
                                                Display="None" ErrorMessage="Please select Reporting Authority 01" SetFocusOnError="true" ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reporting Authority  02 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Authority 02"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged1" data-select2-enable="true" TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reporting Authority  03</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Authority 03"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged1" data-select2-enable="true" TabIndex="7">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reporting Authority  04</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Authority 04"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged" data-select2-enable="true" TabIndex="8">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reporting Authority  05 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Authority 05"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged" data-select2-enable="true" TabIndex="9">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Path</label>
                                            </div>
                                            <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                                           <%-- <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>session</label>
                                            </div>--%>
                                            <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true"
                                                ToolTip="Select Staff Type Name" data-select2-enable="true" TabIndex="2" Visible="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStaffType"
                                                Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Appraisal Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAppraisalType" runat="server" TabIndex="3" ToolTip="Select Department" AppendDataBoundItems="true" Visible="false"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAppraisalType_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAppraisalType"
                                                Display="None" ErrorMessage="Please Select Appraisal Type" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Employee</label>
                                            </div>
                                            <asp:Label ID="lblEmpName" runat="server" class="form-control" Font-Bold="true"></asp:Label>
                                        </div>
                                       
                                        

                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="divSubmit" runat="server">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAPath" OnClick="btnSave_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" TabIndex="10" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Back" TabIndex="13" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" ToolTip="Click here to Cancel" TabIndex="11" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                </div>
                            </asp:Panel>
                            <div class="col-12" id="DivEmployeeList" runat="server" visible="false">
                                <asp:ListView ID="lvEmployees" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl"
                                            Text="Employee Not Found!" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>List of Employees</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>
                                                            <asp:CheckBox ID="cbAl" runat="server" TabIndex="7" onclick="totAllSubjects(this)" />
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th></th>
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
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIdNo" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("NAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                            <asp:Panel ID="PnlAddNew" runat="server">
                                <div class="col-12">
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-outline-primary" SkinID="LinkAddNew" OnClick="btnAdd_Click" ToolTip="Click To Add New Authority Path" Text="Add New" TabIndex="14"></asp:Button>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeGrid" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollegeGrid_SelectedIndexChanged" TabIndex="15" Width=" 300px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-12 mt-3 mb-3">
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="rptPathList" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="sub-heading">
                                                        <h5>Employee wise Reporting Authority Path</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <%-- <th>Institute
                                                            </th>--%>
                                                                <th>Department
                                                                </th>
                                                                <th>Employee
                                                                </th>
                                                                <th>Authority 01
                                                                </th>
                                                                <th>Authority 02
                                                                </th>
                                                                <th>Authority 03
                                                                </th>
                                                                <th>Authority 04
                                                                </th>
                                                                <th>Authority 05
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <%-- <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>--%>
                                                    <td>
                                                        <%# Eval("SUBDEPT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlCollege" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="" Width="520">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <br />
                    <br />
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-outline-primary" TabIndex="16" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-outline-primary" TabIndex="17" />
                        <br />
                        <br />
                        <br />
                        <br />
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
                debugger;
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (j != 0) {
                            e.checked = true;
                        }
                        j++;
                    }
                    else
                        e.checked = false;
                }
            }

        }
        function Checkedfalse(headchk) {

        }
    </script>

</asp:Content>


