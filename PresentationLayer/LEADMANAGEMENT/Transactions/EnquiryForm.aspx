<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EnquiryForm.aspx.cs" Inherits="LEADMANAGEMENT_Transactions_EnquiryForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Content/jquery.js"></script>--%>
    <script type="text/javascript">
        function ValidNumeric() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57)
            { return true; }
            else
            { return false; }
        }

        function ValidProsNo() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 45)
            { return true; }
            else
            { return false; }
        }

    </script>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
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

    <asp:UpdatePanel ID="updBatch" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="temp" runat="server" visible="true">
                 <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Student Details</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Enquiry Excel Import</a>
                                        </li>
                                    </ul>

                                    <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <div>
                                                <asp:UpdateProgress ID="UpdEntry" runat="server" AssociatedUpdatePanelID="updEnquiryEntry"
                                                    DynamicLayout="true" DisplayAfter="0">
                                                    <ProgressTemplate>
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
                                            <asp:UpdatePanel ID="updEnquiryEntry" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Admission Batch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmBatchtab1" runat="server" TabIndex="1" ToolTip="Please Select Admission Batch." CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ViewStateMode="Enabled">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmBatch" Display="None"
                                                                    ErrorMessage="Please Select Admission Batch." ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtfName" InvalidChars="1234567890~`!@#$%^&*()-_+={[}}:;<,>?/|\'" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>First Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtfName" runat="server" TabIndex="2" MaxLength="50" ToolTip="Please Enter First Name" CssClass="form-control" autocomplete="off" />
                                                                <asp:RequiredFieldValidator ID="rfvfName" runat="server" ControlToValidate="txtfName" Display="None"
                                                                    ErrorMessage="Please Enter First Name." ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtfName" InvalidChars="1234567890~`.!@#$%^&*()-_+={[}}:;<,>?/|\'" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Last Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtlName" runat="server" TabIndex="3" MaxLength="50" ToolTip="Please Enter Last Name" CssClass="form-control" autocomplete="off" />
                                                                <asp:RequiredFieldValidator ID="rfvlname" runat="server" ControlToValidate="txtlName" Display="None"
                                                                    ErrorMessage="Please Enter Last Name." ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbxName" runat="server" TargetControlID="txtlName" InvalidChars="0123456789~`!@#$%^&*()-_+={[}}:.;<,>?/|\'" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Email</label>
                                                                </div>
                                                                <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" AppendDataBoundItems="True" ToolTip="Please Enter Email" ValidationGroup="submit" ViewStateMode="Enabled"
                                                                    CausesValidation="True" MaxLength="128" CssClass="form-control" autocomplete="off">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Please Enter Email."
                                                                    ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtEmail"
                                                                    ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" SetFocusOnError="True" Display="None" ValidationGroup="submit" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Mobile Number</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMobile" runat="server" TabIndex="5" AppendDataBoundItems="True" ToolTip="Please Enter Mobile Number" ViewStateMode="Enabled"
                                                                    CausesValidation="True" CssClass="form-control" MaxLength="10" ValidationGroup="submit" onkeypress="return ValidNumeric()" autocomplete="off">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvmobile" runat="server" ControlToValidate="txtMobile" Display="None"
                                                                    ErrorMessage="Please Enter Mobile Number." ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Date of Birth</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon" id="imgDate">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="6" ToolTip="Please Enter Date Of Birth" CssClass="form-control" autocomplete="off" onchange="return checkDOB();"></asp:TextBox>
                                                                    <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                                    <%--<ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateOfBirth" PopupButtonID="imgCalDateOfBirth" Enabled="true"></ajaxToolKit:CalendarExtender>--%>
                                                                    <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtDateOfBirth" PopupButtonID="imgDate" Enabled="true">
                                                                    </ajaxToolKit:CalendarExtender>

                                                                    <%-- <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True">
                                                                    </ajaxToolKit:MaskedEditExtender>--%>

                                                                    <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" />


                                                                    <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                                                                        Display="None" ErrorMessage="Please Enter Date Of Birth." SetFocusOnError="True"
                                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Select Gender</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rblgender" runat="server" TabIndex="7" ToolTip="Please Select Gender" RepeatDirection="Horizontal">
                                                                    <asp:ListItem>Male</asp:ListItem>
                                                                    <asp:ListItem>Female</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="rfvgender" runat="server" ControlToValidate="rblgender" Display="None"
                                                                    ErrorMessage="Please Select Gender." ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Address</label>
                                                                </div>
                                                                <asp:TextBox ID="txtaddress" runat="server" TabIndex="8" MaxLength="50" ToolTip="Please Enter Address" CssClass="form-control" autocomplete="off" />
                                                                <asp:RequiredFieldValidator ID="rfvaddress" runat="server" ControlToValidate="txtaddress" Display="None"
                                                                    ErrorMessage="Please Enter Address." ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtaddress" InvalidChars="1234567890~`!@#$%^&*()-_+={[}}:;<,>?/|\'" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Program Type</label>
                                                                    <asp:Label ID="lblprogramtype" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlprogramt" runat="server" TabIndex="9" AppendDataBoundItems="True" ToolTip="Please Select Program Type" ViewStateMode="Enabled"
                                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlprogramt_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvtype" runat="server" ControlToValidate="ddlprogramt" Display="None"
                                                                    ErrorMessage="Please Select Program Type." ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Degree</label>
                                                                    <asp:Label ID="lblddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="10" AppendDataBoundItems="True" ToolTip="Please Select Degree" ViewStateMode="Enabled"
                                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                                                    ErrorMessage="Please Select Degree." ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Programme/Branch</label>
                                                                    <%--<asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="11" AppendDataBoundItems="True" ToolTip="Please Select Programme/Branch." CssClass="form-control" data-select2-enable="true" ViewStateMode="Enabled" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None"
                                                                    ErrorMessage="Please Select Programme/Branch" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Type of Source</label>
                                                                    <asp:Label ID="lblsource" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlsource" runat="server" TabIndex="12" AppendDataBoundItems="True" ToolTip="Please Select Type of Source" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ViewStateMode="Enabled">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvsource" runat="server" ControlToValidate="ddlsource" Display="None"
                                                                    ErrorMessage="Please Select Type of Source." ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group col-lg-5 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <asp:CheckBox ID="chkConfirm" runat="server" Text="&nbsp;I confirm that the above information is correct." TabIndex="1" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                                    CssClass="btn btn-primary" TabIndex="13" OnClientClick="validateCaseSensitiveEmail();" OnClick="btnSave_Click1" />
                                                                <%-- <asp:Button ID="btnShowReport" runat="server" OnClick="btnShowReport_Click" TabIndex="11" Text="Prospectus Receipt" ToolTip="Prospectus Receipt"
                                                            CssClass="btn btn-info" Enabled="false" />--%>
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-warning" TabIndex="14" OnClick="btncancel_Click" />&nbsp;
                                                        
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false"
                                                            DisplayMode="List" />
                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit1" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                   
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>
                                        <div class="tab-pane fade" id="tab_2">
                                            <div>
                                                <asp:UpdateProgress ID="updExcelEntry" runat="server" AssociatedUpdatePanelID="updpnl"
                                                    DynamicLayout="true" DisplayAfter="0">
                                                    <ProgressTemplate>
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
                                            <asp:UpdatePanel ID="updpnl" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group col-lg-12 col-md-12 col-12 mt-4">
                                                        <div class=" note-div">
                                                            <h5 class="heading">Note</h5>
                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Before import excel, kinldy ensure that Source available in ERP master. If not available then do the Master entry in ERP for Source then upload excel. </span></p>

                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnl" runat="server">
                                                        <div class="col-12">
                                                            <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Admission Batch</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ViewStateMode="Enabled"
                                                                        TabIndex="1">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Attach Excel File</label>
                                                                    </div>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload" TabIndex="2" />

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary margin" TabIndex="3"
                                                                Text="Download Blank Excel Sheet" OnClick="btnExport_Click" ToolTip="Click to download blank excel format file" Enabled="true"><i class="fa fa-file-excel-o"></i> Download Blank Excel Sheet</asp:LinkButton>
                                                            <asp:LinkButton ID="btnUpload" runat="server" ValidationGroup="report" CssClass="btn btn-primary margin" TabIndex="4"
                                                                Text="Upload Excel Sheet" OnClick="btnUpload_Click" ToolTip="Click to Upload" Enabled="true"><i class="fa fa-upload"> Upload Excel</i></asp:LinkButton>
                                                            <%--    <asp:Button ID="btnReport" runat="server" Text="Report"  CssClass="btn btn-info" TabIndex="5" OnClick="btnReport_Click" />--%>

                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="report" Style="text-align: center" />

                                                        </div>
                                                        <div class="col-12">
                                                            <asp:ListView ID="lvEnquiry" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Lead Generation Info</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Student Name
                                                                                    </th>
                                                                                    <th>Last Name
                                                                                    </th>
                                                                                    <th>Student Mobile Number
                                                                                    </th>
                                                                                    <th>Email
                                                                                    </th>
                                                                                    <th>Gender
                                                                                    </th>
                                                                                    <th>DOB
                                                                                    </th>

                                                                                    <th>Type of Source
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
                                                                        <%-- <td style="width: 15%">
                                                                <%# Eval("ENQGENNO")%>
                                                            
                                                            </td>--%>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblStuname" Text='<%# Eval("FirstName")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="Label4" Text='<%# Eval("LastName")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblStuMobile" Text='<%# Eval("Mobile")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblMail" Text='<%# Eval("Email")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblGender" Text='<%# Eval("Gender")%>'></asp:Label>
                                                                        </td>
                                                                        <td>

                                                                            <asp:Label runat="server" ID="lblDOB" Text='<%# Eval("DateofBirth" ,"{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbllead_source" Text='<%# Eval("SourceType")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                    </asp:Panel>


                                                </ContentTemplate>

                                                <Triggers>
                                                    <%-- <asp:PostBackTrigger ControlID="btnDownload" />--%>
                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                    <asp:PostBackTrigger ControlID="btnExport" />
                                                    <%-- <asp:PostBackTrigger ControlID="btnReport" />--%>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>


                                        <div id="divMsg" runat="server"></div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    debugger;
                    var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab1";
                    $('#dvtab a[href="#' + tabName + '"]').tab('show');
                    $("#dvtab a").click(function () {
                        $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                    });
                });
                $(function () {
                    $('#btnreprint').click(function (e) { e.preventDefault(); }).click();
                });



                function validateCaseSensitiveEmail(txtEmail) {
                    var reg = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
                    if (reg.test(txtEmail)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

                function checkDOB() {
                    // alert("in");
                    //alert($("#txtDateOfBirth").val());
                    //var givenDate = document.getElementById('<%=txtDateOfBirth.Text.ToString()%>');
                    //var givenDate = document.getElementById("txtDateOfBirth").value;
                    //alert(givenDate);
                    var CurrentDate = new Date;
                    givenDate = new Date(givenDate);
                    if (givenDate > CurrentDate) {
                        alert("You can't select the fututre date.");
                        return false;
                    }
                }
                function tab() {
                    $('#tab2').tab('show')
                };
            </script>
        </ContentTemplate>

        <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnreprint" />
        </Triggers>--%>
    </asp:UpdatePanel>


</asp:Content>
