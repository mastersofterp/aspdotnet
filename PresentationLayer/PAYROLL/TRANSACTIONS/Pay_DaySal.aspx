<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_DaySal.aspx.cs" Inherits="PayRoll_Transactions_Pay_DaySal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <contenttemplate>
           
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">DONATION ENTRY</h3>
                    </div>

                    <div class="box-body">                           
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="col-12">
	                            <div class="row">
		                            <div class="col-12">
		                            <div class="sub-heading">
				                            <h5>ADD/EDIT DONATION ENTRY</h5>
			                            </div>
		                            </div>
	                            </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Select Month For Salary Donation</label>
								        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="ImgMonYear" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtMonthYear" CssClass="form-control" TabIndex="1" ToolTip="Enter Date" runat="server"  />
                                                                                                                
                                        <ajaxToolKit:CalendarExtender ID="ceMonthYear" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtMonthYear" PopupButtonID="ImgMonYear" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeMonthYear" runat="server" TargetControlID="txtMonthYear"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevMonthYear" runat="server" ControlExtender="meeMonthYear"
                                            ControlToValidate="txtMonthYear" EmptyValueMessage="Please Enter Month Year "
                                            InvalidValueMessage="Month Year is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter Retirement Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="payroll" SetFocusOnError="True" />                                                                
                                        </div>
							        </div>
                                            
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>College</label>
								        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
							        </div>
                                          
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <%--<label>Staff</label>--%>
                                            <label>Scheme/Staff</label>
								        </div>
                                            <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                    CssClass="form-control" TabIndex="3" ToolTip="Select Staff">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Scheme/Staff" InitialValue="0"
                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
							        </div>
                                           
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Give Days Of Salary Deduct</label>
								        </div>
                                            <asp:TextBox ID="txtdays" runat="server" MaxLength="3" CssClass="form-control" TabIndex="4" ToolTip="Enter Days" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtdays" runat="server" ControlToValidate="txtdays"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Days to Salary  Deduct"
                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbAccNo" runat="server"
                                    TargetControlID="txtdays"
                                        FilterType="Custom,Numbers"
                                        FilterMode="ValidChars"
                                            ValidChars="">
                                    </ajaxToolKit:FilteredTextBoxExtender>
							        </div>
                                            
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
                                             <sup>* </sup>
									        <label>Payhead For Transfer Salary</label>
								        </div>
                                        <asp:DropDownList ID="ddlPayhead" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter Payhead For Transfer Salary" AppendDataBoundItems="true" data-select2-enable="true"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                    Display="None" ErrorMessage="Please Select Payhead For Transfer Salary" ValidationGroup="payroll"
                                    SetFocusOnError="True" InitialValue="0">
                                </asp:RequiredFieldValidator>
							        </div>                                                              
                                </div>                                                                            
                            </div>
                            <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" OnClick="btnSave_Click"
                                CssClass="btn btn-primary" TabIndex="6" ToolTip="Click To Save"/>
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Go To Previous"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="8" ToolTip="Click To Reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlList" runat="server">
                            <div class="col-12 btn-footer">
	                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary" ToolTip="Click Add New To Enter Donation Entry" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New"></asp:LinkButton>
                            </div>                                      
                            <div class="col-12">
                                <asp:ListView ID="lvDalSal" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" /></EmptyDataTemplate>
                                    <LayoutTemplate>
                                            <div class="sub-heading">
	                                            <h5>Donation Entry Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                    <th>
                                                        Action
                                                    </th>
                                                    <th>
                                                        Staff Name
                                                    </th>
                                                    <th>
                                                        Month Year
                                                    </th>
                                                    <th>
                                                        Days
                                                    </th>
                                                    <th>
                                                        Payhead
                                                    </th>
                                                    <th>
                                                        College Name
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
                                            <td class="text-center">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DSNO")%>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("DSNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("STAFF")%>
                                            </td>
                                            <td>
                                                <%# Eval("MONYEAR")%>
                                            </td>
                                            <td>
                                                <%# Eval("DAYS")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAYSHORT")%>
                                            </td>
                                            <td>
                                                <%# Eval("COLLEGE_NAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                            
                                </asp:ListView>
                                <div class="vista-grid_datapager text-center d-none">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvDalSal" PageSize="10"
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
                                </div>                                  
                        </asp:Panel>
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
            </script>

            <script type="text/javascript" language="javascript">
                function checkdate(input) {
                    var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
                    var returnval = false
                    if (input.value != "") {
                        if (!validformat.test(input.value)) {
                            alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                        }
                    }
                    else if (input.value != "") {
                        {
                            var monthfield = input.value.split("/")[0]

                            if (monthfield > 12 || monthfield <= 0) {
                                alert("Month Should be greate than 0 and less than 13");
                                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                            }
                        }
                    }


                    function ValidateNumeric(txt) {


                        if (isNaN(txt.value)) {
                            txt.value = txt.value.substring(0, (txt.value.length) - 1);
                            txt.value = "";
                            txt.focus();
                            alert("Only Numeric Characters alloewd");
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                }

            </script>
<script type="text/javascript" language="javascript">
    function check(me) {
        if (ValidateNumeric(me) == true) {
            var myArr = new Array();
            var myArrdays = new Array();
            myString = "" + me.id + "";
            myArr = myString.split("_");
            var index = myArr[3].substring(4, myArr[3].length);
            var Attenddays = document.getElementById("ctl00_ContentPlaceHolder1_txtdays" + index + "_txtDays");
            var Attend_days = Attenddays.value;
            myArrdays = Attend_days.split(".");

            if (!(Attend_days > 31)) {
                if (myArrdays[1] > 0) {
                    if (myArrdays[1] > 5 || myArrdays[1] < 5) {
                        alert("Please enter 5 only after decimal");
                        document.getElementById("ctl00_ContentPlaceHolder1_txtdays" + index + "_txtDays").value = "";
                        document.getElementById("ctl00_ContentPlaceHolder1_txtdays" + index + "_txtDays").focus();
                    }
                }
            }
            else {
                alert("Please enter days less than 32");
                me.value = "";
                me.focus();
            }
        }
    }
    </script>
        </contenttemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
