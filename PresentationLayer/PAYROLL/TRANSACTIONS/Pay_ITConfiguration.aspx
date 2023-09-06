<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ITConfiguration.aspx.cs" Inherits="Pay_ITConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="~/includes/prototype.js" type="text/javascript"></script>

    <script src="~/includes/scriptaculous.js" type="text/javascript"></script>

    <script src="~/includes/modalbox.js" type="text/javascript"></script>--%>


    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CONFIGURATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Edit Configuration</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>College Name</label>
                                            </div>
                                            <asp:TextBox ID="txtCollegeName" runat="server" Text="" CssClass="form-control textbox"
                                                TabIndex="1" ToolTip="Please Enter College Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>TAN No</label>
                                            </div>
                                            <asp:TextBox ID="txtTANNo" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>PAN No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPANNo" runat="server" CssClass="form-control" TabIndex="3" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Signature Name</label>
                                            </div>
                                            <asp:TextBox ID="txtSignature" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Section</label>
                                            </div>
                                            <asp:TextBox ID="txtSection" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Son of</label>
                                            </div>
                                            <asp:TextBox ID="txtSonOf" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Designation</label>
                                            </div>
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>EMPLOYER ADDRESS</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Flat / Block No.</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpBlockNo" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Building Name</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpBuildingName" runat="server" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Road/Street/Lane</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpRoad" runat="server" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Area / Location</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpArea" runat="server" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Town/City/Dist.</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpCity" runat="server" CssClass="form-control" TabIndex="12"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>State</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpState" runat="server" CssClass="form-control" TabIndex="13"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Pin Code</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpPinCode" runat="server" CssClass="form-control" TabIndex="14" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvPinCode" runat="server" ControlToValidate="txtEmpPinCode" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Integer"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Telephone No.</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpTelNo" runat="server" CssClass="form-control" TabIndex="15" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvEmpTelNo" runat="server" ControlToValidate="txtEmpTelNo" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Integer"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>E-mail</label>
                                            </div>
                                            <asp:TextBox ID="txtEmpEmail" runat="server" CssClass="form-control" TabIndex="16"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmpEmail" ErrorMessage="Invalid Email Format"
                                                ValidationGroup="emp" SetFocusOnError="true" Display="None"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>RESPONSIBLE PERSON ADDRESS</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Flat Block No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPerBlockNo" runat="server" CssClass="form-control" TabIndex="17"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Building Name</label>
                                            </div>
                                            <asp:TextBox ID="txtPerBuildingName" runat="server" CssClass="form-control" TabIndex="18"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Road/Street/Lane</label>
                                            </div>
                                            <asp:TextBox ID="txtPerRoad" runat="server" CssClass="form-control" TabIndex="19"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Area / Location</label>
                                            </div>
                                            <asp:TextBox ID="txtPerArea" runat="server" CssClass="form-control" TabIndex="20"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Town/City/Dist</label>
                                            </div>
                                            <asp:TextBox ID="txtPerCity" runat="server" CssClass="form-control" TabIndex="21"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>State</label>
                                            </div>
                                            <asp:TextBox ID="txtPerState" runat="server" CssClass="form-control" TabIndex="22"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Pin Code</label>
                                            </div>
                                            <asp:TextBox ID="txtPerPinCode" runat="server" CssClass="form-control" TabIndex="23" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvPerPinCode" runat="server" ControlToValidate="txtPerPinCode" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Integer"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Telephone No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPerTelNo" runat="server" CssClass="form-control" TabIndex="24" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvPerTelNo" runat="server" ControlToValidate="txtPerTelNo" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Integer"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>E-mail</label>
                                            </div>
                                            <asp:TextBox ID="txtPerEmail" runat="server" CssClass="form-control" TabIndex="25"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>OTHER DETAILS</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bank Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" TabIndex="26"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Branch Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="27"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Print Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgPrintDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                   
                                                <asp:TextBox ID="txtPrintdate" runat="server" CssClass="form-control" TabIndex="28"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cePrintdate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtPrintdate" PopupButtonID="imgPrintDate" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meePrinDate" runat="server" TargetControlID="txtPrintdate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bank Place</label>
                                            </div>
                                            <asp:TextBox ID="txtBankPlace" runat="server" CssClass="form-control" TabIndex="29"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Relief :: Limit:</label>
                                            </div>
                                            <asp:TextBox ID="txtRlimit" runat="server" CssClass="form-control" TabIndex="30" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvReliefLimit" runat="server" ControlToValidate="txtRlimit" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Female Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtFemaleLimit" runat="server" CssClass="form-control" TabIndex="31" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvFemaleLimit" runat="server" ControlToValidate="txtFemaleLimit" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bond Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtBondLimit" runat="server" CssClass="form-control" TabIndex="32" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvBondLimit" runat="server" ControlToValidate="txtBondLimit" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Surcharge(%)</label>
                                            </div>
                                            <asp:TextBox ID="txtSurcharge" runat="server" CssClass="form-control" TabIndex="33" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvSurcharge" runat="server" ControlToValidate="txtSurcharge" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>if more than</label>
                                            </div>
                                            <asp:TextBox ID="txtMorethan" runat="server" CssClass="form-control" TabIndex="34" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvMoreThan" runat="server" ControlToValidate="txtMorethan" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Std. Ded. Male</label>
                                            </div>
                                            <asp:TextBox ID="txtMale" runat="server" CssClass="form-control" TabIndex="35" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvStddedMale" runat="server" ControlToValidate="txtMale" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Tax Inc. Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtLimit" runat="server" CssClass="form-control" TabIndex="36" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvITLimit" runat="server" ControlToValidate="txtLimit" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Edu Cess(%)</label>
                                            </div>
                                            <asp:TextBox ID="txtEducess" runat="server" CssClass="form-control" TabIndex="37" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvEducess" runat="server" ControlToValidate="txtEducess" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Std. Ded. Female</label>
                                            </div>
                                            <asp:TextBox ID="txtFemale" runat="server" CssClass="form-control" TabIndex="38" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvStddedFemale" runat="server" ControlToValidate="txtFemale" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Tax Inc. UpLimit</label>
                                            </div>
                                            <asp:TextBox ID="txtUpLimit" runat="server" CssClass="form-control" TabIndex="39" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvITUplimit" runat="server" ControlToValidate="txtUpLimit" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Additional PT</label>
                                            </div>
                                            <asp:TextBox ID="txtPT" runat="server" CssClass="form-control" TabIndex="40" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvADDPT" runat="server" ControlToValidate="txtPT" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Medical Exemption</label>
                                            </div>
                                            <asp:TextBox ID="txtMedExeption" runat="server" CssClass="form-control" TabIndex="41" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="cvMedExem" runat="server" ControlToValidate="txtMedExeption" Display="None"
                                                ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Higher Educess</label>
                                            </div>
                                            <asp:TextBox ID="txthigheducess" runat="server" CssClass="form-control" TabIndex="42" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>B.S.R. Code</label>
                                            </div>
                                            <asp:TextBox ID="txtBSRCode" runat="server" CssClass="form-control" TabIndex="43"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Fin Year</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="44"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgFromDate" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>To</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgTodate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="45"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="imgTodate" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>TA Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtTALimit" runat="server" CssClass="form-control" TabIndex="45"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Income Tax Month Show in Form No. 16:</label>
                                            </div>
                                            <asp:CheckBox ID="chkFormNo16" runat="server" TabIndex="45" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Add NPS In Gross Amount for IT</label>
                                            </div>
                                            <asp:CheckBox ID="chkNPSGrossIT" runat="server" TabIndex="46" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Tax Rebate Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtTaxRebateLimit" runat="server" CssClass="form-control" TabIndex="47"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Do you want to Add Proposed Salary in Inc. Tax:</label>
                                            </div>
                                            <asp:CheckBox ID="chkProposedSalary" runat="server" TabIndex="48" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>House Loan Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtHouseAmtLimit" runat="server" CssClass="form-control" TabIndex="49"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Consider Previous Month Salary</label>
                                            </div>
                                            <asp:CheckBox ID="chkPreviousSalary" runat="server" TabIndex="50" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>80CCD NPS Limit</label>
                                            </div>
                                            <asp:TextBox ID="txt80CCDNPSLimit" runat="server" CssClass="form-control" TabIndex="51"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>RGESS 80CCG Limit</label>
                                            </div>
                                            <asp:TextBox ID="txtRGESSLimit" runat="server" CssClass="form-control" TabIndex="52"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Visible="True" Text="Save"
                                        ValidationGroup="submit" TabIndex="53" ToolTip="Click To Save Details" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning" Visible="True" Text="Cancel"
                                        ValidationGroup="submit" TabIndex="54" ToolTip="Click To Clear Details" OnClick="btnClear_Click" />

                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

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
    </script>
</asp:Content>

