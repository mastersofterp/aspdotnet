<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OA_Offline_Fees_Collection.aspx.cs" Inherits="ACADEMIC_OA_Offline_Fees_Collection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <label>Online Admission Offline Fees Collection</label></h3>
                </div>

                <div class="box-body">
                    <div id="divStudentSearch" runat="server" class="col-12">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label><b>Application ID</b></label>
                                                    </div>
                                                    <asp:TextBox ID="txtApplicationID" runat="server" CssClass="form-control" MaxLength="15" ToolTip="Please Enter Application ID" AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvApplicationID" runat="server" ControlToValidate="txtApplicationID" Display="None" ErrorMessage="Please Enter ApplicationID" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="form-group col-lg-3 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="Submit" CssClass="btn btn-primary" />
                                            <asp:ValidationSummary ID="valShow" ValidationGroup="Submit" ShowSummary="false" runat="server" ShowMessageBox="true" DisplayMode="List" />
                                        </div>
                                    </div>

                                    <div class="row mt-3" id="divStudentinfo" runat="server" visible="false">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered ipad-view">
                                                <li class="list-group-item" id="trApplicationid" runat="server"><b>Application ID :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblApplicationid" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item" id="trName" runat="server"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item" id="trFees" runat="server"><b>Fees:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFees" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered ipad-view">
                                                <li class="list-group-item" id="trDegree" runat="server"><b>Degree :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item" id="trBranch" runat="server"><b>Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                 <li class="list-group-item" id="trMode" runat="server"><b>Pay Mode:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMode" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row mt-2" id="divfee" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Fees</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdoFees" runat="server"  RepeatDirection="Horizontal" ToolTip="Please Select Fees" AutoPostBack="true" OnSelectedIndexChanged="rdoFees_SelectedIndexChanged">
                                                <asp:ListItem Value="1">&nbsp;Application Fee&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;Admission Fee </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPayMode" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Payment Mode</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" ToolTip="Please Select Payment Mode" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">CASH</asp:ListItem>
                                                <asp:ListItem Value="2">DD/Cheque/Trans-Ref.No</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPayment" runat="server" ErrorMessage="Please Select Payment Mode." Display="None" ControlToValidate="ddlPaymentMode"
                                                InitialValue="0" ValidationGroup="Collect"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divAmount" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" CssClass="form-control" ToolTip="Please Enter Amount." Enabled="false"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Amount." Display="None" ControlToValidate="txtAmount"
                                                ValidationGroup="Collect"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbAmount" runat="server" ValidChars="0123456789." FilterMode="ValidChars" TargetControlID="txtAmount"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                    </div>
                                    <div class="row" id="divfeesMode" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="txtDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDate" runat="server" TabIndex="19" ValidationGroup="academic" CssClass="form-control" placeholder="Enter Date" Enabled="false"/>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Date." Display="None" ControlToValidate="txtDate"
                                                ValidationGroup="Collect"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDate" PopupButtonID="txtDate1" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDate"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />

                                                <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter Date."
                                                    ControlExtender="meeDateOfBirth" ControlToValidate="txtDate" IsValidEmpty="true"
                                                    InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"/>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>City</label>
                                            </div>
                                            <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="form-control" ToolTip="Please Enter City." placeholder="Enter City" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter City." Display="None" ControlToValidate="txtCity"
                                                ValidationGroup="Collect"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtFilter" runat="server" TargetControlID="txtCity" InvalidChars="~`!@#$%^&*_+={[}}|\:;<,>.?/1234567890" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Bank</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Bank">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Bank." Display="None" ControlToValidate="ddlBank"
                                                ValidationGroup="Collect" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 ">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Amount to be paid</label>
                                            </div>
                                            <asp:TextBox ID="txtAmounttobepaid" runat="server" ToolTip="Please Enter Amount." MaxLength="5" CssClass="form-control" Enabled="false"></asp:TextBox>
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Amount." Display="None" ControlToValidate="txtAmounttobepaid"
                                                ValidationGroup="Collect"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmounttobepaid" ValidChars="1234567890." FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="divButton" runat="server" visible="false">
                                        <asp:Button ID="btnCollect" runat="server" CssClass="btn btn-primary" OnClick="btnCollect_Click" Font-Bold="true" Text="Collect" ValidationGroup="Collect" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Font-Bold="true" Text="Cancel" />
                                        <asp:ValidationSummary ID="vsCollect" runat="server" ValidationGroup="Collect" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



