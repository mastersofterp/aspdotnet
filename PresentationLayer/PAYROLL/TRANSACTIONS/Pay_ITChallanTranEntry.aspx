<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ITChallanTranEntry.aspx.cs"
    Inherits="Pay_ITChallanTranEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">INCOME TAX CHALLAN DETAIL ENTRY</h3>
                        </div>

                        <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Challan No.</label>
                                                </div>
                                                <asp:DropDownList ID="ddlChallanNo" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                    AppendDataBoundItems="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlChallanNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvChallanTrnNo" runat="server" ControlToValidate="ddlChallanNo" InitialValue="0"
                                                    ErrorMessage="Please Select Challan No." SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>&nbsp;</label>
                                                </div>
                                                <asp:CheckBox ID="chkIT" runat="server" Text="IT > 0" Enabled="false" />             
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlNewEmp" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Add Employee Details</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Employee Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <%--<ajaxToolKit:ListSearchExtender ID ="lstExtender" runat ="server" TargetControlID ="ddlEmployee"  PromptCssClass ="ClassA" PromptPosition ="Top"  ></ajaxToolKit:ListSearchExtender>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Gross Amt.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtGS" runat="server" CssClass="form-control" TabIndex="1" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                    <asp:CompareValidator ID="cvGS" runat="server" ControlToValidate="txtGS" Display="None"
                                                        ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                        ValidationGroup="submit" SetFocusOnError="true"></asp:CompareValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Challan Amt.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtChalAmt" runat="server" CssClass="form-control" TabIndex="1" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                    <asp:CompareValidator ID="cvChalanAmt" runat="server" ControlToValidate="txtChalAmt" Display="None"
                                                        ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                        ValidationGroup="submit" SetFocusOnError="true"></asp:CompareValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Charges</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCharges" runat="server" CssClass="form-control" TabIndex="1" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                    <asp:CompareValidator ID="cvCharges" runat="server" ControlToValidate="txtCharges" Display="None"
                                                        ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                        ValidationGroup="submit" SetFocusOnError="true"></asp:CompareValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Edu.Cess</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEduCess" runat="server" CssClass="form-control" TabIndex="1" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                    <asp:CompareValidator ID="cvEducess" runat="server" ControlToValidate="txtEduCess" Display="None"
                                                        ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                        ValidationGroup="submit" SetFocusOnError="true"></asp:CompareValidator>
                                                </div>

                                                <%--<div class="form-group row">
                                                            <div class="col-md-2">
                                                                <label>Total:</label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                                                            </div>
                                                        </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="1"
                                                ToolTip="Click To Add Employee Detail" OnClick="btnAdd_Click" />
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" TabIndex="1"
                                                ToolTip="Click To Go Back" OnClick="btnBack_Click" />
                                        </div>
                                    </asp:Panel>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Select Month</label>
                                                </div>
                                                <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalMonYear" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtMonYear" runat="server" CssClass="form-control" TabIndex="1"
                                                    AutoPostBack="True" Enabled="false" />
                                                <%--<asp:Image ID="imgCalMonYear" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" Visible="false"/>--%>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="MMMyyyy" TargetControlID="txtMonYear"
                                                    PopupButtonID="imgCalMonYear" Enabled="true" EnableViewState="true" />
                                            </div>
                                                </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Challan Date</label>
                                                </div>
                                                <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalChDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtChDate" runat="server" CssClass="form-control" Text="" Enabled="false" TabIndex="1"/>
                                                <%--<asp:Image ID="imgCalChDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" Visible="false" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceChDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChDate"
                                                    PopupButtonID="imgCalChDate" Enabled="true" EnableViewState="true" />
                                            </div>
                                                </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Challan No</label>
                                                </div>
                                                <asp:TextBox ID="txtChallanNo" CssClass="form-control" runat="server" ToolTip="Enter Challan Number" Enabled="false" TabIndex="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Cheque/DD No</label>
                                                </div>
                                                <asp:TextBox ID="txtChequeDDNo" CssClass="form-control" runat="server" ToolTip="Enter Cheque/DD Number" Enabled="false" TabIndex="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Tax Deposited</label>
                                                </div>
                                                <asp:TextBox ID="txtTaxDeposited" CssClass="form-control" runat="server" ToolTip="Enter Tax Deposited" Enabled="false" TabIndex="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Surcharge</label>
                                                </div>
                                                <asp:TextBox ID="txtSurcharge" CssClass="form-control" runat="server" ToolTip="Enter Surcharge" Enabled="false" TabIndex="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Education Cess</label>
                                                </div>
                                                <asp:TextBox ID="txtEducationCess" CssClass="form-control" runat="server" ToolTip="Enter EducationCess" Enabled="false" TabIndex="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>B.S.R Code</label>
                                                </div>
                                                <asp:TextBox ID="txtBSRCode" CssClass="form-control" runat="server" ToolTip="Enter BSR Code" Enabled="false" TabIndex="1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowEmployees" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="1"
                                            ToolTip="" OnClick="btnShowEmployees_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="1"
                                            ToolTip="" OnClick="btnSave_Click" />
                                         <asp:Button ID="btnPrint" runat="server" Text="Print" ToolTip="" CssClass="btn btn-info" OnClick="btnPrint_Click"  TabIndex="1"/>
                                                    <asp:LinkButton ID="lnkAddNew" runat="server" SkinID="LinkAddEMP" CssClass="btn btn-info" OnClick="lnkAddNew_Click" Visible="false" TabIndex="1">Add Employee</asp:LinkButton>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="1" ToolTip="Click To Reset"
                                            Width="70px" OnClick="btnCancel_Click" />                                       
                                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" runat="server" />
                                    </div>
                                   <%-- <div class="form-group row">
                                        <div class="text-center">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                    Please wait.........................................
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>--%>
                                </asp:Panel>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Challan Entries</h5>
                                    </div>
                                    <asp:ListView ID="lvChallan" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Challan Entry Exists" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr style="font-weight: bold; background-color: #808080; color: white">
                                                        <%-- <th style="width: 2%; padding-left: 8px; text-align: left">Action
                                                        </th>--%>
                                                        <th>IDNO
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Designation
                                                        </th>
                                                        <th>Gross Amt.
                                                        </th>
                                                        <th>Challan Amt.
                                                        </th>
                                                        <th>Charges
                                                        </th>
                                                        <th>Edu.Cess
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <%--<td style="width: 2%">--%>
                                                <%--<asp:CheckBox ID="chkPrint" runat="server" ToolTip='<%# Eval("CHIDNO") %>'/>--%>
                                                <%--<asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SCHIDNO")%>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("SCHIDNO") %>'
                                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                OnClientClick="showConfirmDel(this); return false;" />--%>
                                                <%--</td>--%>

                                                <td style="display: none;">
                                                    <asp:Label ID="lblSchIdno" runat="server" Text='<%# Eval("SCHIDNO") %>' />
                                                </td>
                                                <td>

                                                    <%--<%# Eval("IDNO")%>--%>
                                                    <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<%# Eval("NAME")%>--%>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                </td>
                                                <td>

                                                    <%--<%# Eval("DESIGNATION")%>--%>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("DESIGNATION")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossAmt" runat="server" Text='<%# Eval("GSAMT")%>'></asp:TextBox>
                                                    <%--<%# Eval("GSAMT")%>--%>
                                                       
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCHAMT" runat="server" Text='<%# Eval("CHAMT")%>'></asp:TextBox>
                                                    <%--<%# Eval("CHAMT")%>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCHSCHARGE" runat="server" Text='<%# Eval("CHSCHARGE")%>'></asp:TextBox>
                                                    <%--<%# Eval("CHSCHARGE")%>--%>
                                                        
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCHEDUCESS" runat="server" Text='<%# Eval("CHEDUCESS")%>'></asp:TextBox>
                                                    <%--<%# Eval("CHEDUCESS")%>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                            <div id="pnlPrint" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Chalan no. / Month</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkSelectChNo" runat="server" AutoPostBack="true"
                                            Text="Select Chalan No." OnCheckedChanged="chkSelectChNo_CheckedChanged" />&nbsp;
                                            <asp:CheckBox ID="chkSelectMonth" runat="server" AutoPostBack="true"
                                                Text="Select Month" OnCheckedChanged="chkSelectMonth_CheckedChanged" />&nbsp;
                                            <asp:CheckBox ID="chkPeriod" runat="server" Text="Period" AutoPostBack="true"
                                                OnCheckedChanged="chkPeriod_CheckedChanged" Visible="false" />
                                    </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Challan No.</h5>
                                </div>
                                <asp:ListView ID="lvChalanNo" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Data Found" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Select</th>
                                                    <th>Chalan no.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:CheckBox ID="chkChalan" runat="server" AlternateText="Check to Select Chalan No."
                                                    ToolTip='<%# Eval("CHALANNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblch" runat="server" Text=' <%# Eval("CHALANNO")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="col-12" visible="false">
                                <div class="sub-heading" id="Div2" runat="server" visible="false">
                                    <h5>Month</h5>
                                </div>
                                <div id="Div3" runat="server" visible="false">
                                    <asp:ListView ID="lvMonth" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Data Found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th></th>
                                                        <th>Month And Year
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <asp:CheckBox ID="ChkMonth" runat="server" AlternateText="Check to allocate Payhead"
                                                        ToolTip='<%# Eval("MON")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMonth" runat="server" Text=' <%# Eval("MON")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div id="tdFrmTo" runat="server">
                                <div id="b" runat="server" visible="false">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>From</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgFrom" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <%--<div class="input-group-addon">
                                                <asp:Image ID="imgFrom1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>--%>
                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" AutoPostBack="True" TabIndex="1"/>
                                            <ajaxToolKit:CalendarExtender ID="ceFrom" runat="server" Format="MMMyyyy" TargetControlID="txtFrom"
                                                PopupButtonID="imgFrom" Enabled="true" EnableViewState="true" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>To</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgTo" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <%--<div class="input-group-addon">
                                                <asp:Image ID="imgTo1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>--%>
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtTo_TextChanged" TabIndex="1"/>
                                            <ajaxToolKit:CalendarExtender ID="ceTo" runat="server" Format="MMMyyyy" TargetControlID="txtTo"
                                                PopupButtonID="imgTo" Enabled="true" EnableViewState="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show Report" ToolTip="Click To Show Annexure-I" CssClass="btn btn-primary" TabIndex="1"
                                        OnClick="btnShow_Click" />
                                    <asp:Button ID="btnPBack" runat="server" Text="Back" ToolTip="Click To Show Annexure-I" CssClass="btn btn-primary" TabIndex="1"
                                        OnClick="btnPBack_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="form-group row">
                        <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                            runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                            OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                            BackgroundCssClass="modalBackground" />
                        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                            <div style="text-align: center">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="text-center">
                                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                        </div>
                                        <div>
                                            Are you sure you want to delete this Challan Entry?
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="text-center">
                                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-success" />
                                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-danger" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>--%>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

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

                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Numeric Characters allowed !");
                        return false;
                    }
                    else
                        return true;
                }

                function validateAlphabet(txt) {
                    var expAlphabet = /^[A-Za-z]+$/;
                    if (txt.value.search(expAlphabet) == -1) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Alphabets allowed!");
                        return false;
                    }
                    else
                        return true;
                }
                function Amount(txt) {
                    if (ValidateNumeric(txt) == true) {
                        var TotalAmount = 0.00, GSAmount = 0.00, Charges = 0.00, ChalanAmount = 0.00, EducationCess = 0.00;
                        GSAmount = document.getElementById("ctl00_ctp_txtGS").value;
                        ChalanAmount = document.getElementById("ctl00_ctp_txtChalAmt").value;
                        Charges = document.getElementById("ctl00_ctp_txtCharges").value;
                        EducationCess = document.getElementById("ctl00_ctp_txtEduCess").value;

                        //              for(i = 0; i <= Number(document.getElementById("ctl00_ctp_hidTotalRecordsCount").value)-1; i++)
                        //              {    
                        //                 TotalAmount=Number(TotalAmount)+Number(document.getElementById("ctl00_ctp_txtGS").value);
                        //              } 
                        alert(TotalAmount);
                    }
                }
            </script>
            <input type="hidden" id="hidTotalRecordsCount" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

