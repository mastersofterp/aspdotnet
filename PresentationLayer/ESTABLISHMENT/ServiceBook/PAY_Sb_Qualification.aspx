<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="PAY_Sb_Qualification.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_PAY_Sb_Qualification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">


    <%--<link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />--%>

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <form role="form">
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Qualification</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Qualification Level :</label>
                                            </div>
                                            <asp:DropDownList ID="ddlLevel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Qualification Level" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlQualification" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Qualification Level" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trqualification" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Qualification/Degree :</label>
                                            </div>
                                            <asp:DropDownList ID="ddlQualification" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Qualification" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlQualification" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Qualification" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Board/University Name :</label>
                                            </div>
                                            <asp:TextBox ID="txtUniversity" runat="server" CssClass="form-control" ToolTip="Enter University Name" MaxLength="49"
                                                TabIndex="3" IsValidate="True" ControlValidated="True" DataType="VarCharType" IsRequired="True"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvUniversity" runat="server" ControlToValidate="txtUniversity"
                                                    Display="None" ErrorMessage="Please Enter University Name" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>University Type :</label>
                                            <asp:DropDownList ID="ddlunitype" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" ToolTip="Please Select Type" TabIndex="4" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%--<asp:ListItem Value="1">Deemed</asp:ListItem>
                                                    <asp:DropDownList ID="ddlLevel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="1" 
                                                CssClass="form-control" ToolTip="Select Qualification Level"
                                                <asp:ListItem Value="2">Private</asp:ListItem>
                                                <asp:ListItem Value="3">Government</asp:ListItem>
                                                <asp:ListItem Value="4">Semi-Government</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Institute/School/College Name :</label>
                                            </div>
                                            <asp:TextBox ID="txtInstituteName" runat="server" CssClass="form-control" ToolTip="Enter Institute Name" TabIndex="5" MaxLength="120"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Institute Type :</label>
                                            <asp:DropDownList ID="ddlinstitype" AppendDataBoundItems="true" runat="server"
                                                CssClass="form-control" ToolTip="Please Select Institute Type" TabIndex="6" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Private</asp:ListItem>
                                                <asp:ListItem Value="2">Government</asp:ListItem>
                                                <asp:ListItem Value="3">SCHOOL</asp:ListItem>
                                                <asp:ListItem Value="4">UNIVERSITY COLLEGE</asp:ListItem>
                                                <asp:ListItem Value="5">POLYTECHNIC</asp:ListItem>
                                                <asp:ListItem Value="6">COLLEGE</asp:ListItem>
                                                <asp:ListItem Value="7">AUTONOMOUS</asp:ListItem>
                                                <asp:ListItem Value="8">ITI</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Address :</label>
                                            </div>
                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" ToolTip="Enter Location" TabIndex="7" MaxLength="100"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Year of passing :</label>
                                            </div>
                                            <asp:TextBox ID="txtYearOfPassing" runat="server" MaxLength="4" CssClass="form-control" ToolTip="Enter Year of passing"
                                                TabIndex="8"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revyear" runat="server" ControlToValidate="txtYearOfPassing" ValidationExpression="^[0-9]{4}$"
                                                ErrorMessage="Please Enter Valid Year" ValidationGroup="ServiceBook" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>

                                            <%--onkeypress="return CheckNumeric(event,this);"--%>
                                            <%--  <asp:RequiredFieldValidator ID="rfvYearOfPassing" runat="server" ControlToValidate="txtYearOfPassing"
                                                    Display="None" ErrorMessage="Please Enter Year of passing" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Council Reg.no :</label>
                                            </div>
                                            <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" ToolTip="Enter Council Registration Number"
                                                TabIndex="9"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvtxtRegNo" runat="server" ControlToValidate="txtRegNo"
                                                    Display="None" ErrorMessage="Please Enter Reg.no" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>DCI/MCI ID Card No :</label>
                                            </div>
                                            <asp:TextBox ID="txtIDCardNo" runat="server" CssClass="form-control" ToolTip="Enter DCI/MCI ID Card No"
                                                TabIndex="10"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvtxtRegNo" runat="server" ControlToValidate="txtRegNo"
                                                    Display="None" ErrorMessage="Please Enter Reg.no" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Reg. Council Name :</label>
                                            </div>
                                            <asp:TextBox ID="txtRegName" runat="server" CssClass="form-control" TabIndex="11"
                                                ToolTip="Enter Registration Council Name:"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trDate" runat="server">
                                            <div class="label-dynamic">
                                                <label>Result Date :</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Enter Date" Enabled="true" ValidationGroup="emp"
                                                        TabIndex="12" Style="z-index: 0;" />
                                                    <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeBirthDate"
                                                        ControlToValidate="txtDate"
                                                        InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Result Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divMonth" runat="server">
                                            <label>Month :</label>
                                            <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" ToolTip="Select Month" TabIndex="16" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Percentage Of Mark :</label>
                                            <asp:TextBox ID="txtpercent" runat="server" CssClass="form-control" ToolTip="Enter Percentage" MaxLength="5" TabIndex="13"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="mePercent" runat="server" TargetControlID="txtpercent"
                                                Mask="99.99" MaskType="Number" InputDirection="LeftToRight" MessageValidatorTip="true" ErrorTooltipEnabled="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>CGPA :</label>
                                            <asp:TextBox ID="txtCGPA" runat="server" CssClass="form-control" ToolTip="Enter CGPA" MaxLength="4" onkeyup="validateNumeric(this);" TabIndex="14"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Grade/Class Obtain :</label>
                                            <asp:TextBox ID="txtgrade" runat="server" CssClass="form-control" ToolTip="Enter Grade" MaxLength="50"
                                                TabIndex="15"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Specialization :</label>
                                            </div>
                                            <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" ToolTip="Enter Specialization"
                                                onkeypress="return CheckAlphabet(event,this);" TabIndex="16"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Highest Qualification Status :</label>
                                            <asp:CheckBox ID="chkhighest" runat="server" ToolTip="Check mark for Highest Qualification status" TabIndex="17" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="18" />
                                            <span style="color: red">Upload File Maximum Size 10 Mb</span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divExm" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Examination Passed :</label>
                                            </div>
                                            <asp:TextBox ID="txtExam" runat="server" CssClass="form-control" ToolTip="Enter Examination Passed" MaxLength="100"
                                                TabIndex="19"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="txtExam"
                                                            Display="None" ErrorMessage="Please Enter Examination Passed" ValidationGroup="ServiceBook"
                                                             SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="20"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="21"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlQuali" runat="server">
                                    <asp:ListView ID="lvQuali" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Qualification"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Qualification Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Level Name
                                                        </th>
                                                        <th>Qualification/Degree
                                                        </th>
                                                        <%-- <th width="15%" >
                                                   Qualification
                                                </th>--%>
                                                        <th>University Name
                                                        </th>
                                                        <th>Institute Name
                                                        </th>
                                                        <th>Passing Year
                                                        </th>
                                                        <%--<th>Reg. No
                                                                </th>--%>
                                                        <th>Specialization
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("QNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("QNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("QUALILEVELNAME")%>
                                                </td>
                                                <td>
                                                    <%--<%# Eval("ExamName")%>--%>
                                                    <%# Eval("QUALI") %>
                                                </td>
                                                <%-- <td width="10%" >
                                        <%# Eval("QUALI")%>
                                    </td>--%>
                                                <td>
                                                    <%# Eval("UNIVERSITY_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("inst")%>
                                                </td>
                                                <td>
                                                    <%# Eval("passyear")%>
                                                </td>
                                                <%-- <td>
                                                        <%# Eval("regno")%>
                                                    </td>--%>
                                                <td>
                                                    <%# Eval("speci")%>
                                                </td>
                                                <td id="tdFolder" runat="server">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("QNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                </td>
                                                <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </form>
                </div>
            </div>

            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <div class="col-md-12">
                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                    <div class="text-center">
                        <div class="modal-content">
                            <div class="modal-body">
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                <div class="text-center">
                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
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

                function CheckNumeric(event, obj) {
                    var k = (window.event) ? event.keyCode : event.which;
                    //alert(k);
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                        obj.style.backgroundColor = "White";
                        return true;
                    }
                    if (k > 45 && k < 58) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter numeric Value');
                        obj.focus();
                    }
                    return false;
                }
                onkeypress = "return CheckAlphabet(event,this);"
                function CheckAlphabet(event, obj) {

                    var k = (window.event) ? event.keyCode : event.which;
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter Alphabets Only!');
                        obj.focus();
                    }
                    return false;
                }

                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = '';
                        alert('Only Numeric Characters Allowed!');
                        txt.focus();
                        return;
                    }
                }

                function validateAlphabet(txt) {
                    var expAlphabet = /^[A-Za-z .]+$/;
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
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

