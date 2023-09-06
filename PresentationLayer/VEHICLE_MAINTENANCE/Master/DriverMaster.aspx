<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DriverMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_DriverMaster"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <style>
        #ctl00_ContentPlaceHolder1_UpdatePanel3 .input-group .input-group-addon{
            border-bottom: none!important;
        }
    </style>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DRIVER / CONDUCTOR MASTER</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                           <%-- <div class="sub-heading">
                                <h5>Driver / Conductor Details</h5>
                            </div>--%>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Driver / Conductor Type </label>
                                    </div>
                                    <asp:DropDownList ID="ddlDriConType" runat="server" TabIndex="1" AppendDataBoundItems="true" ValidationGroup="Submit"
                                        AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Driver or Conductor Type"
                                        OnSelectedIndexChanged="ddlDriConType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Driver</asp:ListItem>
                                        <asp:ListItem Value="2">Conductor</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvType" ValidationGroup="Submit" ControlToValidate="ddlDriConType" Display="None"
                                        ErrorMessage="Please Select Driver/Conductor Type." SetFocusOnError="true" runat="server"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Name </label>
                                    </div>
                                    <asp:TextBox ID="txtDrvrName" runat="server" MaxLength="40" CssClass="form-control" 
                                        ToolTip="Enter Characters Only"
                                        onkeypress="return CheckAlphabet(event, this);" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDrvrName" runat="server" SetFocusOnError="true" Display="None"
                                        ErrorMessage="Please Enter Name."
                                        ValidationGroup="Submit" ControlToValidate="txtDrvrName"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Contact No. </label>
                                    </div>
                                    <asp:TextBox ID="txtDrvrCntNo" runat="server" TabIndex="3" MaxLength="15" CssClass="form-control" 
                                        onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Contact Number"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        FilterType="Custom, Numbers" ValidChars="0123456789" TargetControlID="txtDrvrCntNo">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvDrvrCont" ValidationGroup="Submit" ControlToValidate="txtDrvrCntNo"
                                        Display="None" ErrorMessage="Please Enter Contact No." SetFocusOnError="true" runat="server">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Address </label>
                                    </div>
                                    <asp:TextBox ID="txtDrvrAdd1" TextMode="MultiLine" runat="server" MaxLength="5"
                                        TabIndex="4" CssClass="form-control"  ToolTip="Enter Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDrvrAdd1" Display="None" runat="server"
                                        SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Please Enter Address."
                                        ControlToValidate="txtDrvrAdd1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="tradd" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Address 2  </label>
                                    </div>
                                    <asp:TextBox ID="txtDrvrAdd2" TextMode="MultiLine" runat="server" MaxLength="5"
                                        CssClass="form-control" ToolTip="Enter Second Address"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trLicType" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Driving Licence Type </label>
                                    </div>
                                    <asp:TextBox ID="txtDrivingLicenceType" runat="server" MaxLength="50" CssClass="form-control" 
                                        TabIndex="5" ToolTip="Enter Characters Only" onkeypress="return CheckAlphabet(event, this);">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDrivingLicenceType" runat="server" SetFocusOnError="true"
                                        Display="None" ErrorMessage="Please Enter Driving Licence Type ."
                                        ValidationGroup="Submit" ControlToValidate="txtDrivingLicenceType"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trLicNo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Driving Licence No. </label>
                                    </div>
                                    <asp:TextBox ID="txtDrivingLicenceNo" runat="server" MaxLength="30" CssClass="form-control" 
                                        TabIndex="6" onkeypress="return CheckAlphaNumeric(event,this);" ToolTip="Enter Driving Licence Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtDrivingLicenceNo" runat="server" SetFocusOnError="true"
                                        Display="None" ErrorMessage="Please Enter Driving Licence Number."
                                        ValidationGroup="Submit" ControlToValidate="txtDrivingLicenceNo"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Category </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="7" AppendDataBoundItems="true"
                                        ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true" ToolTip="Select Category">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Regular</asp:ListItem>
                                        <asp:ListItem Value="2">Contract</asp:ListItem>
                                        <asp:ListItem Value="3">MR</asp:ListItem>
                                        <asp:ListItem Value="4">Hired</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCat" ValidationGroup="Submit" ControlToValidate="ddlCategory"
                                        Display="None" ErrorMessage="Please Select Category." SetFocusOnError="true" runat="server"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trFroDt" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date  </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="8" ToolTip="Enter From Date"
                                            CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                       

                                        <ajaxToolKit:CalendarExtender ID="cetxtIndentSlipDate" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtFromDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndentSlipDate" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please Enter From Date"
                                            SetFocusOnError="True" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:MaskedEditExtender ID="meIndDate" runat="server" TargetControlID="txtFromDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meIndDate"
                                            ControlToValidate="txtFromDate" EmptyValueMessage="Please From Date"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Submit" SetFocusOnError="True" />

                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trExpDt" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Expiry Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgExpiryDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtExpiryDate" runat="server" TabIndex="9" ToolTip="Enter Expiry Date"
                                            CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                     

                                         <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgExpiryDate" TargetControlID="txtExpiryDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpiryDate"
                                            Display="None" ErrorMessage="Please Enter Expiry Date"
                                            SetFocusOnError="True" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtExpiryDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meIndDate"
                                            ControlToValidate="txtExpiryDate" EmptyValueMessage="Please From Date"
                                            InvalidValueMessage="Expiry Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Expiry Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Submit" SetFocusOnError="True" />

                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Driver / Conductor Details</h5>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvDriverLicExp" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>LIST OF DRIVER LICENCE EXPIRE IN NEXT 15 DAYS</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" " style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>DRIVER NAME
                                                        </th>
                                                        <th>DRIVING LICENCE NUMBER
                                                        </th>
                                                        <th>LICENCE EXPIRY DATE
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
                                                <%# Eval("DNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("D_DRIVING_LICENCE_NO")%>
                                            </td>
                                            <td>
                                                <%# Eval("D_DRIVING_LICENCE_EXPIRY_DATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>

                    </asp:Panel>
                    <div class="col-12" id="file" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-10">
                                <label>
                                    <span style="color: #FF0000">Valid files : (.jpg, .bmp, .gif, .png, .pdf, .xls, .doc,.zip, .txt, .docx, .xlsx should be of 100 kb size.)</span>
                                </label>
                            </div>
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Attach File</label>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group date">
                                            <asp:FileUpload ID="FileUpload1" runat="server" ValidationGroup="complaint" ToolTip="Select file to upload" TabIndex="9" />
                                            <div class="input-group-addon">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                                    ValidationGroup="complaint" CssClass="btn btn-primary"
                                                    CausesValidation="False" TabIndex="10" ToolTip="Add Attach File"/>
                                                <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnAdd" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                                <img alt="" src="loader.gif" />
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                        </div>
                    </div>
                    <asp:Panel ID="pnlFile" runat="server" Visible="false">
                        <div class="col-12">
                            <asp:ListView ID="lvfile" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Download files</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" " style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>File Name
                                                    </th>
                                                    <th>Driving Licence No.
                                                    </th>
                                                    <th>Download
                                                    </th>
                                                    <%--<th>Creation Date 
                                                                                        </th>--%>
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
                                            <asp:ImageButton ID="btnAttachDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnAttachDelete_Click"
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                        </td>
                                        <td>
                                            <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                        </td>
                                        <td>
                                            <%#GetFileNameCaseNo(DataBinder.Eval(Container, "DataItem")) %>
                                        </td>
                                        <td>
                                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>--%>
                                            <asp:ImageButton ID="imgFileDownload" runat="Server" ImageUrl="~/Images/action_down.png"
                                                AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                OnClick="imgFileDownload_Click" />
                                            <%-- </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="imgFileDownload" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>--%>

                                        </td>
                                        <%--<td>
                                                                                <%#GetFileDate(DataBinder.Eval(Container, "DataItem"))%>
                                                                            </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                    </asp:Panel>
                    <div class="col-12 btn-footer mt-3">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="10" OnClick="btnSubmit_Click"
                            CausesValidation="true" CssClass="btn btn-primary" ToolTip="Click here to Submit"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait..';"/>
                        <asp:Button ID="btnExpiry" runat="server" Text="Licence Expiry Report" TabIndex="12"
                            CssClass="btn btn-info" ToolTip="Click here to Show Licence Expiry Report" OnClick="btnExpiry_Click" OnClientClick="return UserDeleteConfirmation();" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                        <asp:HiddenField ID="hdnexpiryinput" runat="server" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />

                    </div>
                    <div class="col-12 mt-4 mb-4">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvDriver" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>DRIVER/ CONDUCTOR DETAILS ENTRY LIST</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>DRIVER/ CONDUCTOR NAME
                                                    </th>
                                                    <th>DRIVER/ CONDUCTOR CONTACT NO.
                                                    </th>
                                                    <th>DRIVER/ CONDUCTOR ADDRESS 
                                                    </th>
                                                    <th>DRIVING LICENCE TYPE
                                                    </th>
                                                    <th>DRIVING LICENCE NUMBER
                                                    </th>
                                                    <th>CATEGORY
                                                    </th>
                                                    <th>DRIVER/ CONDUCTOR TYPE
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
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("DNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("DNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PHONE")%>
                                        </td>
                                        <td>
                                            <%# Eval("DADD1")%>
                                        </td>
                                        <td>
                                            <%# Eval("D_DRIVING_LICENCE_TYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("D_DRIVING_LICENCE_NO")%>
                                        </td>
                                        <td>
                                            <%# Eval("DCATEGORY")%>
                                        </td>
                                        <td>
                                            <%# Eval("DRIVER_CON_TYPE")%>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
 
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
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
