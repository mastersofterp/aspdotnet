<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Configuration.aspx.cs" Inherits="PAYROLL_PAY_Configuration_Pay_Configuration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>


    <contenttemplate>
            <style>
                .accordion-button {
                    background: #eee;
                    padding-top: 5px;
                    margin-bottom: 10px;
                    cursor: pointer;
                }

                .sub-heading {
                    padding-bottom: 0px;
                }

                    .sub-heading h5 {
                        margin-bottom: 5px;
                    }

                .more-less {
                    float: right;
                    color: #053769;
                    display: inline-block;
                    margin-top: 3px;
                }
            </style>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PAYROLL CONFIGURATION SETTINGS</h3>
                        </div>
                        <div class="box-body">
                            <div class="colapse-panel" id="accordion">
                                <div class="col-12 colapse-heading">
                                    <div class="row">
                                        <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divPayHeadConfig" aria-expanded="true" aria-controls="collapseOne">
                                            <i class="more-less fas fa-minus"></i>
                                            <div class="sub-heading">
                                                <h5>Pay Head Settings
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divPayHeadConfig" class="collapse show" data-parent="#accordion">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>DA</label>
                                                </div>
                                                <asp:TextBox ID="txtDAField" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter DA"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>HRA</label>
                                                </div>
                                                <asp:TextBox ID="txtHRAField" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter HRA"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>CLA</label>
                                                </div>
                                                <asp:TextBox ID="txtCLAField" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter CLA"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>TA</label>
                                                </div>
                                                <asp:TextBox ID="txtTAField" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Enter TA"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>DA ON TA</label>
                                                </div>
                                                <asp:TextBox ID="txtDAONTAField" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter DA ON TA"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>RECOVERY</label>
                                                </div>
                                                <asp:TextBox ID="txtRecoveryField" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter RECOVERY"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>GPF ADDITIONAL</label>
                                                </div>
                                                <asp:TextBox ID="txtGPFADDField" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter GPF ADDITIONAL"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>GPF ADVANCE</label>
                                                </div>
                                                <asp:TextBox ID="txtGPFADVField" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter GPF ADVANCE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>INCOME TAX</label>
                                                </div>
                                                <asp:TextBox ID="txtITField" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter INCOME TAX"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PROFESSIONAL TAX</label>
                                                </div>
                                                <asp:TextBox ID="txtPTField" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Enter  PROFESSIONAL TAX"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LIC</label>
                                                </div>
                                                <asp:TextBox ID="txtLICField" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Enter LIC"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>GROUP INSURANCE</label>
                                                </div>
                                                <asp:TextBox ID="txtGISField" runat="server" CssClass="form-control" TabIndex="12" ToolTip="Enter GROUP INSURANCE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LISENCE FEE</label>
                                                </div>
                                                <asp:TextBox ID="txtLISCFEEField" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Enter LISENCE FEE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>COMPANY</label>
                                                </div>
                                                <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" TabIndex="14" ToolTip="Enter COMPANY"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LWP FIELD</label>
                                                </div>
                                                <asp:TextBox ID="txtLWPField" runat="server" CssClass="form-control" TabIndex="15" ToolTip="Enter LWP FIELD"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>DE</label>
                                                </div>
                                                <asp:TextBox ID="txtDEField" runat="server" CssClass="form-control" TabIndex="16" ToolTip="Enter DE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>G80</label>
                                                </div>
                                                <asp:TextBox ID="txtG80Field" runat="server" CssClass="form-control" TabIndex="17" ToolTip="Enter G80"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>CPF</label>
                                                </div>
                                                <asp:TextBox ID="txtCPFField" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Enter CPF"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>CPF LOAN</label>
                                                </div>
                                                <asp:TextBox ID="txtCPFLOANField" runat="server" CssClass="form-control" TabIndex="19" ToolTip="Enter CPF LOAN"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PHONE</label>
                                                </div>
                                                <asp:TextBox ID="txtPHONEField" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter PHONE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>MEDICAL</label>
                                                </div>
                                                <asp:TextBox ID="txtMEDICALField" runat="server" CssClass="form-control" TabIndex="21" ToolTip="Enter MEDICAL"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>RD</label>
                                                </div>
                                                <asp:TextBox ID="txtRDField" runat="server" CssClass="form-control" TabIndex="22" ToolTip="Enter RD"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>HONO FIELD</label>
                                                </div>
                                                <asp:TextBox ID="txtHONOField" runat="server" CssClass="form-control" TabIndex="23" ToolTip="Enter HONO FIELD"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>GPFLOAN</label>
                                                </div>
                                                <asp:TextBox ID="txtGPFLOANField" runat="server" CssClass="form-control" TabIndex="24" ToolTip="Enter GPFLOAN"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>GPF</label>
                                                </div>
                                                <asp:TextBox ID="txtGPFField" runat="server" CssClass="form-control" TabIndex="25" ToolTip="Enter GPF"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ACCPW</label>
                                                </div>
                                                <asp:TextBox ID="txtACCPW" runat="server" CssClass="form-control" TabIndex="26" ToolTip="Enter ACCPW"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>SINGLE_INV</label>
                                                </div>
                                                <asp:TextBox ID="txtSINGLE_INV" runat="server" CssClass="form-control" TabIndex="27" ToolTip="Enter SINGLE_INV"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>R_WIDTH</label>
                                                </div>
                                                <asp:TextBox ID="txtRW_WIDTH" runat="server" CssClass="form-control" TabIndex="28" ToolTip="Enter R_Width"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>RHEIGHT</label>
                                                </div>
                                                <asp:TextBox ID="txtRHEIGTH" runat="server" CssClass="form-control" TabIndex="29" ToolTip="Enter Rheight"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LWP_CAL</label>
                                                </div>
                                                <asp:TextBox ID="txtLWP_CAL" runat="server" CssClass="form-control" TabIndex="30" ToolTip="Enter Lwp_Cal"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>COLNAME</label>
                                                </div>
                                                <asp:TextBox ID="txtCOLNAME" runat="server" CssClass="form-control" TabIndex="31" ToolTip="Enter ColName"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>SOC NAME</label>
                                                </div>
                                                <asp:TextBox ID="txtSOCNAME" runat="server" CssClass="form-control" TabIndex="32" ToolTip="Enter SOC Name"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>OVER TIME HEAD</label>
                                                </div>
                                                <asp:TextBox ID="txtOverTimeHead" runat="server" CssClass="form-control" TabIndex="33" ToolTip="Enter Over Time Head Name"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 colapse-heading">
                                    <div class="row">
                                        <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divCollegeInfo" aria-expanded="true" aria-controls="collapseTwo">
                                            <i class="more-less fas fa-plus"></i>
                                            <div class="sub-heading">
                                                <h5>Institute Information
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divCollegeInfo" class="collapse" data-parent="#accordion">
                                    <div class="col-12">
                                        <div class="row">


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PRINCIPAL NAME</label>
                                                </div>
                                                <asp:TextBox ID="txtPrincipalName" runat="server" CssClass="form-control" TabIndex="34" ToolTip="Enter PRINCIPAL NAME"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LINK_NO</label>
                                                </div>
                                                <asp:TextBox ID="txtLINKNO" runat="server" CssClass="form-control" TabIndex="35" ToolTip="Enter LINK_NO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>MODULE_NO</label>
                                                </div>
                                                <asp:TextBox ID="txtMODULENO" runat="server" CssClass="form-control" TabIndex="36" ToolTip="Enter MODULE_NO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LICEFEE_FIELD</label>
                                                </div>
                                                <asp:TextBox ID="txtLICEFEE_FIELD" runat="server" CssClass="form-control" TabIndex="37" ToolTip="Enter LICEFEE_FIELD"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>DOSFONT</label>
                                                </div>
                                                <asp:TextBox ID="txtDOSFONT" runat="server" CssClass="form-control" TabIndex="38" ToolTip="Enter DOSFONT"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LWP_PDAY</label>
                                                </div>
                                                <asp:TextBox ID="txtLWP_PDAY" runat="server" CssClass="form-control" TabIndex="39" ToolTip="Enter LWP_PDAY"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>FROM DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="40" ToolTip="Enter FROM DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>TO DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="41" ToolTip="Enter TO DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                        PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>CPF PERCENTAGE</label>
                                                </div>
                                                <asp:TextBox ID="txtCPFPercentage" runat="server" CssClass="form-control" TabIndex="42" ToolTip="Enter CPF PERCENTAGE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>RD_PER</label>
                                                </div>
                                                <asp:TextBox ID="txtRD_PER" runat="server" CssClass="form-control" TabIndex="43" ToolTip="Enter RD_PER"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LIC BRANCH</label>
                                                </div>
                                                <asp:TextBox ID="txtLICBranch" runat="server" CssClass="form-control" TabIndex="44" ToolTip="Enter LIC BRANCH"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ZERO</label>
                                                </div>
                                                <asp:TextBox ID="txtZERO" runat="server" CssClass="form-control" TabIndex="45" ToolTip="Enter ZERO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>TAN NUMBER</label>
                                                </div>
                                                <asp:TextBox ID="txtTANNo" runat="server" CssClass="form-control" TabIndex="46" ToolTip="Enter TAN NUMBER"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LIC PAC NO</label>
                                                </div>
                                                <asp:TextBox ID="txtLICPACNO" runat="server" CssClass="form-control" TabIndex="47" ToolTip="Enter LIC PAC NO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PROPOSED_SAL</label>
                                                </div>
                                                <asp:TextBox ID="txtPRO_SAL" runat="server" CssClass="form-control" TabIndex="48" ToolTip="Enter PROPOSED_SAL"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PAN NUMBER</label>
                                                </div>
                                                <asp:TextBox ID="txtPANNo" runat="server" CssClass="form-control" TabIndex="49" ToolTip="Enter PAN NUMBER"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>AMTSIZE</label>
                                                </div>
                                                <asp:TextBox ID="txtAMTSIZE" runat="server" CssClass="form-control" TabIndex="50" ToolTip="Enter AMTSIZE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>SALPASSWORD</label>
                                                </div>
                                                <asp:TextBox ID="txtSAL_PWD" runat="server" CssClass="form-control" TabIndex="51" ToolTip="Enter SALPASSWORD"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ANOSIZE</label>
                                                </div>
                                                <asp:TextBox ID="txtANOSIZE" runat="server" CssClass="form-control" TabIndex="52" ToolTip="Enter ANOSIZE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ENAMESIZE</label>
                                                </div>
                                                <asp:TextBox ID="txtENAMESIZE" runat="server" CssClass="form-control" TabIndex="53" ToolTip="Enter ENAMESIZE"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PFF START DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalPFFStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtPFFStartDate" runat="server" CssClass="form-control" TabIndex="54" ToolTip="Enter PFF START DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cePFFStartDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtPFFStartDate" PopupButtonID="imgCalPFFStartDate" Enabled="true"
                                                        EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PFF END DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalPFFEndDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtPFFEndDate" runat="server" CssClass="form-control" TabIndex="55" ToolTip="Enter PFF END DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cePFFEndDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtPFFEndDate" PopupButtonID="imgCalPFFEndDate" Enabled="true"
                                                        EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>CPF FROM DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalCPFFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtCPFFromDate" runat="server" CssClass="form-control" TabIndex="56" ToolTip="Enter CPF FROM DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceCPFFromDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtCPFFromDate" PopupButtonID="imgCalCPFFromDate" Enabled="true"
                                                        EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>CPF TO DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalCPFToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtCPFToDate" runat="server" CssClass="form-control" TabIndex="57" ToolTip="Enter CPF TO DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceCPFToDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtCPFToDate" PopupButtonID="imgCalCPFToDate" Enabled="true"
                                                        EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>DA MERGE INTO BASIC</label>
                                                </div>
                                                <asp:TextBox ID="txtDAMergeIntoBasic" runat="server" CssClass="form-control" TabIndex="58" ToolTip="Enter DA MERGE INTO BASIC"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>GPFPER</label>
                                                </div>
                                                <asp:TextBox ID="txtGPFPER" runat="server" CssClass="form-control" TabIndex="59" ToolTip="Enter GPFPER"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>LEAVE_APPROVAL</label>
                                                </div>
                                                <asp:TextBox ID="txtLEAVE_APP" runat="server" CssClass="form-control" TabIndex="60" ToolTip="Enter LEAVE_APPROVAL"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>PH_AMT</label>
                                                </div>
                                                <asp:TextBox ID="txtPH_AMT" runat="server" CssClass="form-control" TabIndex="61" ToolTip="Enter PH_AMT"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>STAFFNO</label>
                                                </div>
                                                <asp:TextBox ID="txtSTAFFNO" runat="server" CssClass="form-control" TabIndex="62" ToolTip="Enter STAFFNO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>BUS CHARGES</label>
                                                </div>
                                                <asp:TextBox ID="txtbuscharges" runat="server" CssClass="form-control" TabIndex="63" ToolTip="Enter BUS CHARGES"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>NPS</label>
                                                </div>
                                                <asp:TextBox ID="txtnps" runat="server" CssClass="form-control" TabIndex="64" ToolTip="Enter NPS"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>EXTRA EPF PER</label>
                                                </div>
                                                <asp:TextBox ID="txtEPFAmount" runat="server" CssClass="form-control" TabIndex="65" ToolTip="Enter EXTRA EPF PER"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:CheckBox ID="chkGradePay" runat="server" Checked="True" TabIndex="66" />&nbsp;&nbsp;Include Grade Pay (GP) in Basic</label>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:CheckBox ID="chkDP" runat="server" Checked="True" TabIndex="67" />&nbsp;&nbsp;Include Dearness Pay (DP) in Basic</label>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>QUARTE RENT APPLIED TO EMPLOYEE</label>
                                                </div>
                                                <asp:RadioButton ID="rdbhrazero" runat="server" Text=" HRA SHOULD BE ZERO" TabIndex="68" />
                                                <br />
                                                <asp:RadioButton ID="rdbhrasame" runat="server" Text=" HRA SAME AS QUARTER RENT" TabIndex="69" />
                                                <br />
                                                (WHICHEVER IS MINIMUM)
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:CheckBox ID="chkLinkWithAccounts" runat="server" TabIndex="70"></asp:CheckBox>&nbsp;&nbsp;LINK WITH ACCOUNTS</label>
                                                </div>
                                            </div>
                                            <%-- Amol sawarkar 04-03-2022--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>EMPLOYEE PAYSLIP EFFECT FROM</label>
                                                </div>
                                                <asp:TextBox ID="txtEMPLOYEEPAYSLIPEFFECTFROM" runat="server" CssClass="form-control" TabIndex="34" ToolTip=""></asp:TextBox>
                                            </div>

                                             <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Registrar Signature Upload</label>
                                                    </div>
                                                    <asp:Image ID="imgEmpSign" runat="server" ImageUrl="~/IMAGES/sign11.jpg" Height="58px"
                                                        Width="128px" /><br />
                                                    <asp:FileUpload ID="fuplEmpSign" runat="server" ToolTip="Please Browse Signature"
                                                        TabIndex="23" onchange="ShowpSignPreview(this);" />
                                                </div>



                                        </div>
                                    </div>
                                </div>

                                <%----Amol sawarkar add  04-03-2022 --%>

                                <div class="col-12 colapse-heading">
                                    <div class="row">
                                        <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divCollegeInfo1" aria-expanded="true" aria-controls="collapseThree">
                                            <i class="more-less fas fa-plus"></i>
                                            <div class="sub-heading">
                                                <h5>ESIC
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="divCollegeInfo1" class="collapse" data-parent="#accordion">
                                    <div class="col-12">
                                        <div class="row">


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESIC NO</label>
                                                </div>
                                                <asp:TextBox ID="txtESICNO" runat="server" CssClass="form-control" TabIndex="34" ToolTip="Enter PRINCIPAL NAME"></asp:TextBox>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESI_LIMIT_HP</label>
                                                </div>
                                                <asp:TextBox ID="txtESI_LIMIT_HP" runat="server" CssClass="form-control" TabIndex="35" ToolTip="Enter LINK_NO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESI_PER_HP</label>
                                                </div>
                                                <asp:TextBox ID="txtESI_PER_HP" runat="server" CssClass="form-control" TabIndex="36" ToolTip="Enter MODULE_NO"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Employer Contribution</label>
                                                </div>
                                                <asp:TextBox ID="txtEmployerContribution" runat="server" CssClass="form-control" TabIndex="37" ToolTip="Enter LICEFEE_FIELD"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESIC_LIMIT</label>
                                                </div>
                                                <asp:TextBox ID="txtESIC_LIMIT" runat="server" CssClass="form-control" TabIndex="37" ToolTip="Enter LICEFEE_FIELD"></asp:TextBox>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESI FIRST FROM DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalESIFIRSTFROMDATE" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtESIFIRSTFROMDATE" runat="server" CssClass="form-control" TabIndex="40" ToolTip="Enter FROM DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ESIFIRSTFROMDATE" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtESIFIRSTFROMDATE" PopupButtonID="imgCalESIFIRSTFROMDATE" Enabled="true" EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESI FIRST TO DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalESIFIRSTTODATE" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtESIFIRSTTODATE" runat="server" CssClass="form-control" TabIndex="41" ToolTip="Enter TO DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ESIFIRSTTODATE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtESIFIRSTTODATE"
                                                        PopupButtonID="imgCalESIFIRSTTODATE" Enabled="true" EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESI SECOND FROM DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalESISECONDFROMDATE" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtESISECONDFROMDATE" runat="server" CssClass="form-control" TabIndex="40" ToolTip="Enter FROM DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ESISECONDFROMDATE" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtESISECONDFROMDATE" PopupButtonID="imgCalESISECONDFROMDATE" Enabled="true" EnableViewState="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ESI SECOND TO DATE</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalESISECONDTODATE" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtESISECONDTODATE" runat="server" CssClass="form-control" TabIndex="41" ToolTip="Enter TO DATE"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ESISECONDTODATE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtESISECONDTODATE"
                                                        PopupButtonID="imgCalESISECONDTODATE" Enabled="true" EnableViewState="true" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 colapse-heading">
                                    <div class="row">
                                        <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divCollegeInfo2" aria-expanded="true" aria-controls="collapseFour">
                                            <i class="more-less fas fa-plus"></i>
                                            <div class="sub-heading">
                                                <h5>User Login Details
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="divCollegeInfo2" class="collapse" data-parent="#accordion">
                                    <div class="col-12">
                                        <div class="row">


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>User Login Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddluserLoginType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="null">-- Please Select --</asp:ListItem>
                                                    <asp:ListItem Value="PFILENO">Employee Code</asp:ListItem>
                                                    <asp:ListItem Value="UserId">UserId</asp:ListItem>
                                                    <asp:ListItem Value="EmployeeId">EmployeeNo</asp:ListItem>
                                                    <asp:ListItem Value="Alternet">Emailid</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Set Default Password</label>
                                                </div>
                                                <asp:TextBox ID="txtUserPass" runat="server" CssClass="form-control" TabIndex="35" ToolTip="Enter User Password"></asp:TextBox>
                                            </div>

                                               <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:CheckBox ID="ChkAutoUserCreated" runat="server"  TabIndex="66" />&nbsp;&nbsp;Auto User Created</label>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 colapse-heading">
                                    <div class="row">
                                        <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#pnlStaffCollapseDiv" aria-expanded="true" aria-controls="collapseThree">
                                            <i class="more-less fas fa-plus"></i>
                                            <div class="sub-heading">
                                                <h5>Staff Not Applicable For Conveyance Allowance
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="pnlStaffCollapseDiv" class="collapse" data-parent="#accordion">
                                    <div id="pnlStaff" runat="server">
                                        <div class="col-12">
                                            <asp:ListView ID="lvStatff" runat="server">
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Data Found" CssClass="text-center mt-3" />
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <%-- <div class="sub-heading">
                                                        <h5>Staff</h5>
                                                    </div>--%>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="checkid" onchange="CheckAll(this);" runat="server" TabIndex="70" />
                                                                    Staff </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 10%">
                                                            <asp:CheckBox ID="chkStaff" runat="server" TabIndex="71" AlternateText="Check Staff" ToolTip='<%# Eval("STAFFNO")%>' />
                                                            <%# Eval("STAFF")%></td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnSave" runat="server" TabIndex="72" OnClick="btnSave_Click" ToolTip="Click To Save" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" TabIndex="73" ToolTip="Click To Reset" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="payroll" />
                                    <div class="col-md-12">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl"></asp:Label>
            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
        </contenttemplate>
    <%--  </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">
        //function toggleExpansion(imageCtl, divId) {
        //    if (document.getElementById(divId).style.display == "block") {
        //        document.getElementById(divId).style.display = "none";
        //        imageCtl.src = "images/expand_blue.jpg";
        //    }
        //    else if (document.getElementById(divId).style.display == "none") {
        //        document.getElementById(divId).style.display = "block";
        //        imageCtl.src = "images/collapse_blue.jpg";
        //    }
        //}

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../images/collapse_blue.jpg";
            }
        }



        function totalStaff(chkcomplaint) {
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
    <script>
        function toggleIcon(e) {
            $(e.target)
                .prev('.colapse-heading')
                .find(".more-less")
                .toggleClass('fa-minus fa-plus');
        }
        $('.colapse-panel').on('hide.bs.collapse', toggleIcon);
        $('.colapse-panel').on('show.bs.collapse', toggleIcon);
    </script>
    <!--===<!--==== Table 100% width script ====-->
    <script>
        $('#accordion').on('shown.bs.collapse', function () {
            $($.fn.dataTable.tables(true)).DataTable()
               .columns.adjust();
        });
    </script>


    <%---- add 14-09-2022--%>
    <%--For Image Preview--%>
    <script type="text/javascript">
        //var jq = $.noConflict();

        function ShowpImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_imgEmpPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }


        function ShowpSignPreview(input) {
            debugger;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_imgEmpSign').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>


</asp:Content>

