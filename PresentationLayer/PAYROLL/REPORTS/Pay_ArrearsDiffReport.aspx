<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ArrearsDiffReport.aspx.cs" Inherits="PAYROLL_Pay_Report_Pay_ArrearsDiffReport"
    Title="" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 177px;
        }
        .style4
        {
            width: 326px;
        }
        .style5
        {
            width: 139px;
        }
        .style6
        {
            width: 110px;
        }
        .style8
        {
            width: 93px;
        }
        .style9
        {
            width: 9%;
        }
        .style10
        {
            width: 127px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <contenttemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ARREARS DIFFERENCE REPORT</h3>
                </div>
                <div class="box-body">
                        <asp:Panel ID="pnlsupl" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Arrears Transfer</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Arrears</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsuplarrear" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Arrears" data-select2-enable="true"
                                            AppendDataBoundItems="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlsuplarrear"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Arrears" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Order No.</label>
                                        </div>
                                        <asp:TextBox ID="txtSuplOrderNo" CssClass="form-control" ToolTip="Enter Order No" runat="server" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSuplOrderNo"
                                            Display="None" ErrorMessage="Please Enter Order No. " SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Supl. Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtSupldate" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="3" />
                                           <%-- <div class="input-group-addon">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupldate"
                                                Display="None" ErrorMessage="Please Enter Supl. Date" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSupldate"
                                                PopupButtonID="Image1" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtSupldate"
                                                MaskType="Date" Mask="99/99/9999">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Supl. Bill Head</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSuplBillHead" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true" data-select2-enable="true"
                                            AppendDataBoundItems="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSuplBillHead"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Supl. Bill Head" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSuplSave" runat="server" Text="Save" TabIndex="5" CssClass="btn btn-success"
                                    ValidationGroup="submit" OnClick="btnSuplSave_Click" />
                                <asp:Button ID="btnSuplCancel" runat="server" Text="Cancel" TabIndex="6" CssClass="btn btn-danger"
                                    OnClick="btnSuplCancel_Click" />
                                <asp:Button ID="btnSuplBack" runat="server" Text="Back" CssClass="btn btn-info" TabIndex="7"
                                    CausesValidation="false" OnClick="btnSuplBack_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlinfo" runat="server">
                            <%--<div class="col-12">
	                            <div class="row">
		                            <div class="col-12">
		                            <div class="sub-heading">
				                            <h5>Arrears Difference Report</h5>
			                            </div>
		                            </div>
	                            </div>
                            </div>--%>
                            <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Select College</label>
								            </div>
                                             <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                InitialValue="0" Display="None" ErrorMessage=" " SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" data-select2-enable="true"
                                                AutoPostBack="true" CssClass="form-control"
                                                AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
							            </div>                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Select Arrears</label>
								            </div>
                                            <asp:RequiredFieldValidator ID="rfvArrears" runat="server" ControlToValidate="ddlArrears"
                                                InitialValue="0" Display="None" ErrorMessage=" " SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlArrears" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlArrears_SelectedIndexChanged" />
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Gov. Order no.</label>
								            </div>
                                             <asp:RequiredFieldValidator ID="rfvCovOrdNo" runat="server" ControlToValidate="txtGovOrdNo"
                                                Display="None" ErrorMessage="Please Enter Gov. Order No." SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtGovOrdNo" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
							            </div>                                    
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Date</label>
								            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgGovdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtGovDt" runat="server" AutoPostBack="true" TabIndex="4" CssClass="form-control" />
                                               
                                                <ajaxToolKit:CalendarExtender ID="ceeGovDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtGovDt"
                                                    PopupButtonID="imgGovdt" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MeeGovDt" runat="server" TargetControlID="txtGovDt"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Office Order No.</label>
								            </div>
                                             <asp:RequiredFieldValidator ID="rfvOffOrdno" runat="server" ControlToValidate="txtOffOrdNo"
                                                Display="None" ErrorMessage="Please Enter Office Order No." SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                            <asp:TextBox ID="txtOffOrdNo" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Date</label>
								            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgoffDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtOffDt" runat="server" AutoPostBack="true" TabIndex="6" CssClass="form-control" />
                                                
                                                <ajaxToolKit:CalendarExtender ID="ceeoffDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOffDt"
                                                    PopupButtonID="imgoffDt" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeOffDt" runat="server" TargetControlID="txtOffDt"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
							            </div>                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Select Employee</label>
								            </div>
                                             <asp:RequiredFieldValidator ID="rfvddlEmp" runat="server" ControlToValidate="ddlEmp"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Employee name" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlEmp" runat="server" TabIndex="7" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"/>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Select Month</label>
								            </div>
                                            <asp:RequiredFieldValidator ID="rfvddlMonth" runat="server" ControlToValidate="ddlMonth"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Month" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlMonth" runat="server" TabIndex="8" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Basic</label>
								            </div>
                                             <asp:TextBox ID="txtBasic" runat="server" TabIndex="9" CssClass="form-control"></asp:TextBox>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>GradePay</label>
								            </div>
                                            <asp:TextBox ID="txtGradePay" runat="server" TabIndex="10" CssClass="form-control"></asp:TextBox>
							            </div>                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Pay</label>
								            </div>
                                            <asp:TextBox ID="txtPay" runat="server" TabIndex="11" CssClass="form-control"></asp:TextBox>
							            </div>                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Rate</label>
								            </div>
                                            <asp:TextBox ID="txtRate" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>To be Paid</label>
								            </div>
                                            <asp:TextBox ID="txttobePaid" runat="server" TabIndex="13" CssClass="form-control"></asp:TextBox>
							            </div>                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>New Gross</label>
								            </div>
                                            <asp:TextBox ID="txtNwGross" runat="server" TabIndex="14" CssClass="form-control"></asp:TextBox>
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>New Net</label>
								            </div>
                                            <asp:TextBox ID="txtNwRate" runat="server" TabIndex="15" CssClass="form-control"></asp:TextBox>
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Already Paid</label>
								            </div>
                                            <asp:TextBox ID="txtAlrdyPaid" runat="server" TabIndex="16" CssClass="form-control"></asp:TextBox>
							            </div>                                    
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Old Gross</label>
								            </div>
                                            <asp:TextBox ID="txtOldGross" runat="server" TabIndex="17" CssClass="form-control"></asp:TextBox>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Old Net</label>
								            </div>
                                            <asp:TextBox ID="txtOldRate" runat="server" TabIndex="18" CssClass="form-control"></asp:TextBox>
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Diff.Arrears</label>
								            </div>
                                            <asp:TextBox ID="txtDiffArrears" runat="server" TabIndex="19" CssClass="form-control"></asp:TextBox>
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Diff Gross</label>
								            </div>
                                            <asp:TextBox ID="txtDiffGross" runat="server" TabIndex="20" CssClass="form-control"></asp:TextBox>
							            </div>                                    
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Diff Net</label>
								            </div>
                                             <asp:TextBox ID="txtDiffNet" runat="server" TabIndex="21" CssClass="form-control"></asp:TextBox>
							            </div>                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Remark</label>
								            </div>
                                            <asp:TextBox ID="txtRemark" TabIndex="22" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
							            </div>                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
                                                <sup>* </sup>
									            <label>No.of Records on a Single page</label>
								            </div>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNoofRecords"
                                                Display="None" ErrorMessage="Please Enter No. of Records " SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtNoofRecords" TabIndex="23" runat="server" CssClass="form-control"></asp:TextBox>
							            </div>                                   
                                    </div>
                                </div>
                            <div class="col-md-12 text-center">
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                                <asp:Button ID="btnTransfer" runat="server" Text="Transfer" TabIndex="24" CssClass="btn btn-primary"
                                    ValidationGroup="submit" OnClick="btnTransfer_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="25" CssClass="btn btn-info"
                                    ValidationGroup="submit" OnClick="btnReport_Click" />                                
                                 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning" TabIndex="26" ValidationGroup="submit"
                                    CausesValidation="false" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="27"
                                    CausesValidation="false" OnClick="btnCancel_Click" />                               
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

    <%--</contenttemplate>--%>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
