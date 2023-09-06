<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CarryForwardLeave.aspx.cs" Inherits="CarryForwardLeave" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">CARRY FORWARD LEAVE</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>College Name</label>
									</div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true"
                                        ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="Leaveapp"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
								</div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trStaff" runat="server">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>Select Staff Type</label>
									</div>
                                    <asp:DropDownList ID="ddlStafftype" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" data-select2-enable="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged" ToolTip="Select Staff Type">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStafftype"
                                        Display="None" ErrorMessage="Please select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                        InitialValue="0"></asp:RequiredFieldValidator>
								</div>
                                
                                <div class="form-group col-lg-3 col-md-6 col-12">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>Old Period</label>
									</div>
                                    <asp:DropDownList ID="ddlPeriod" runat="server" AppendDataBoundItems="true" TabIndex="3" data-select2-enable="true"
                                        CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ControlToValidate="ddlPeriod"
                                        Display="None" ErrorMessage="Please Select Old Period" ValidationGroup="Leaveapp" SetFocusOnError="true"
                                        InitialValue="0"></asp:RequiredFieldValidator>
								</div>
                              
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>Old Year</label>
									</div>
                                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" TabIndex="4" data-select2-enable="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="Leaveapp" SetFocusOnError="true"
                                        InitialValue="0"></asp:RequiredFieldValidator>
								</div>
                               
                                <div class="form-group col-lg-3 col-md-6 col-12">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>New Period</label>
									</div>
                                    <asp:DropDownList ID="ddlNewPeriod" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlNewPeriod_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNewPeriod"
                                        Display="None" ErrorMessage="Please Select New Period" ValidationGroup="Leaveapp" SetFocusOnError="true"
                                        InitialValue="0"></asp:RequiredFieldValidator>
								</div>
                              
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>New Year</label>
									</div>
                                    <asp:DropDownList ID="ddlNewYear" runat="server" CssClass="form-control" TabIndex="6" data-select2-enable="true"
                                         AutoPostBack="true" OnSelectedIndexChanged="ddlNewYear_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNewYear"
                                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="Leaveapp" SetFocusOnError="true"
                                        InitialValue="0"></asp:RequiredFieldValidator>
								</div>
                                
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divFrm" runat="server" visible="false">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>From Date</label>
									</div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control"
                                            TabIndex="7" ToolTip="Enter From Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Leave"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter  From Date"
                                            InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
								</div>
                                
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTo" runat="server" visible="false">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>To Date</label>
									</div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="8"
                                            ToolTip="Enter To Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter  To Date" ValidationGroup="Leave"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter  To Date"
                                            InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter  To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                        <%--<asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                                            CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                            ValidationGroup="submit" ControlToCompare="txtFromDt" />--%>
                                    </div>
								</div>
                                
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trleave" runat="server">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>Select Leave Type</label>
									</div>
                                    <asp:DropDownList ID="ddlLeavename" runat="server" AppendDataBoundItems="true" TabIndex="9" data-select2-enable="true"
                                        ToolTip="Select Leave Type" CssClass="form-control" OnSelectedIndexChanged="ddlLeavename_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlLeavename" runat="server" ControlToValidate="ddlLeavename"
                                        Display="None" ErrorMessage="Please select Leave Type" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                        InitialValue="0"></asp:RequiredFieldValidator>
								</div>
                               
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">
									<div class="label-dynamic">
										<sup>* </sup>
										<label>Type</label>
									</div>
                                    <asp:RadioButtonList ID="rblType" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblType_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="10">
                                        <asp:ListItem Selected="True" Text="Already Carry" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Pending Carry" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
								</div>                               
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Leaveapp" TabIndex="11"
                                OnClick="btnShow_Click" ToolTip="Click here to Show" CssClass="btn btn-primary" />
                            <asp:Button ID="btnSave" runat="server" Text="Transfer" ValidationGroup="Leaveapp" TabIndex="12"
                                OnClick="btnSave_Click" ToolTip="Click here to Show" CssClass="btn btn-primary" />
                            <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" TabIndex="13"
                                OnClick="btnCancelAdd_Click" ToolTip="Click here to Reset" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                ValidationGroup="Leaveapp" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlEmpList" runat="server">
                            <asp:ListView ID="lvEmployees" runat="server">
                                <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of Leaves for Approval" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                        <div class="sub-heading">
	                                        <h5>Employee Leave Record To Carry Forward</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No
                                                    </th>
                                                    <%-- <th>
                                                  <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />  Select
                                                </th>--%>
                                                    <th>Employee Name
                                                    </th>
                                                    <th>Leaves
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <%-- <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                            <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                        </td>--%>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVES")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-md-12">
                        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                            <div class="text-center">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                        <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                        <div class="text-center">
                                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
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

        //    function showLvstatus(source)
        //    {   
        //        this._source=source;
        //        
        //        //__doPostBack(this._source.name, '');
        //        __doPostBack(this._source.name,'');
        //        this._popup=$find('mdlPopupView');
        //        
        //        this._popup.show();
        //    }    
        //    function backClick()
        //    {
        //       this._popup.hide();        
        //       this._source = null;
        //       this._popup = null; 
        //    }
        //    
    </script>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>

