<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_VacationEntry.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Pay_VacationEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">VACATION DAYS ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="panel panel-info">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Selection Criteria</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server" visible="true">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1" ToolTip="Select College Name">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="Shift"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="tr2" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged" data-select2-enable="true"
                                                    AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Staff Type">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStaffType" runat="server" ControlToValidate="ddlStafftype"
                                                    Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Shift"
                                                    SetFocusOnError="True" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                                <div class="label-dynamic">
                                                    <label>Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                    AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Department"
                                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Shift" OnClick="btnSave_Click" TabIndex="7"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" TabIndex="6"
                                    OnClick="btnShowReport_Click" Visible="false" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="8"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <%--   <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">                    
                             <div class="panel panel-info">
                                        <div class="panel panel-heading">Employee List</div>
                                            
                            </div>
                     
                                </div>
                            </div>
                        </form>--%>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvEmpList" runat="server">
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Record Not Found" />
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Employees</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                                onclick="checkAllEmployees(this)" ToolTip="Click here to Select All Employee" TabIndex="9" />
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
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
                                                    <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' ToolTip='<%#Eval("Idno")%>' />
                                                </td>
                                                <td>
                                                    <%#Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control"
                                                            ToolTip="Enter Effect From Date" TabIndex="4" Style="z-index: 0;" />
                                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday Date"
                                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control"
                                                            ToolTip="Enter Effect To Date" Style="z-index: 0;" TabIndex="4" />

                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday Date"
                                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                            <%-- <div class="panel panel-info">--%>
                            <%--   <div class="panel panel-heading"> Vacation Alloted List</div>--%>
                            <asp:Panel ID="pnlAlloted" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvView" runat="server">
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees " />
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Vacation Alloted List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                            </div>                                                
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("VACNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFromdt" runat="server" Text='<%# Eval("FROMDT","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTodt" runat="server" Text='<%# Eval("TODT","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                            <%--</div>--%>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                <script type="text/javascript">
                    //  keeps track of the delete button for the row
                    //  that is going to be removed
                    var _source;
                    // keep track of the popup div
                    var _popup;


                    function checkAllEmployees(chkcomplaint) {
                        var frm = document.forms[0];
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            if (e.type == 'checkbox') {
                                if (chkcomplaint.checked == true)
                                    e.checked = true;
                                else
                                    e.checked = false;
                            }
                        }
                    }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

