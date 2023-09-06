<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_FamilyParticulars.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_FamilyParticulars" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <br />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">Heading</h3>--%>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Family Particulars</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="text-center text-bold">
                                    <asp:Label ID="lblFamilymsg" runat="server" SkinID="Msglbl"></asp:Label>
                                </p>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Family Member Name </label>
                                            </div>
                                            <asp:TextBox ID="txtFamilyMemberName" runat="server" onkeyup="validateAlphabet(this);" TabIndex="1"
                                                CssClass="form-control" ToolTip="Enter Family Member Name"></asp:TextBox>
                                            <asp:Label ID="lblname" runat="server" Text="Please Enter Name as Lastname Firstname Middlename"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvFamilyMemberName" runat="server" ControlToValidate="txtFamilyMemberName"
                                                Display="None" ErrorMessage="Please Enter   Family Member Name" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Relationship</label>
                                            </div>
                                            <asp:DropDownList ID="ddlrelationship" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Relationship ">
                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:TextBox ID="txtRelationShip" runat="server" onkeyup="validateAlphabet(this);"
                                                CssClass="form-control" ToolTip="Enter Relationship" TabIndex="2"></asp:TextBox>
                                            --%>   <%--<asp:RequiredFieldValidator ID="rfvRelationShip" runat="server" ControlToValidate="ddlrelationship"
                                                 ErrorMessage="Please Select  Relationship" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">--%>
                                            <asp:RequiredFieldValidator ID="rfvRelationShip" runat="server" ControlToValidate="ddlrelationship" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Relationship" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date Of Birth</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfBirth" runat="server" AutoPostBack="true" TabIndex="3" Style="z-index: 0;"
                                                    OnTextChanged="txtDateOfBirth_TextChanged" CssClass="form-control" ToolTip="Enter Date Of Birth"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvdate" runat="server" ControlToValidate="txtDateOfBirth"
                                                    Display="None" ErrorMessage="Please Select Date Of Birth" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateOfBirth" PopupButtonID="Image1" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" ControlExtender="meDateOfBirth"
                                                    ControlToValidate="txtDateOfBirth"
                                                    InvalidValueMessage="Date Of Birth is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Enter Date Of Birth" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="ServiceBook" SetFocusOnError="True" InitialValue="__/__/____" IsValidEmpty="false" /><%--EmptyValueMessage="Please Enter Date Of Birth"--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Age</label>
                                            </div>
                                            <asp:TextBox ID="txtAge" runat="server" MaxLength="3" onkeyup="validateNumeric(this);"
                                                CssClass="form-control" ToolTip="Total Age" TabIndex="4" Enabled="false"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                                        Display="None" ErrorMessage="Please Enter Age" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                            <asp:CompareValidator ID="cvAge" runat="server" ControlToValidate="txtAge" Display="None"
                                                ErrorMessage="Please Enter Numeric Value" SetFocusOnError="true" ValidationGroup="ServiceBook"
                                                Operator="DataTypeCheck" Type="Integer">  
                                            </asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label>Gender :</label>
                                            <asp:DropDownList ID="ddlgender" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                ToolTip="Select Gender" TabIndex="5" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Male</asp:ListItem>
                                                <asp:ListItem Value="2">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label>Education :</label>
                                            <asp:TextBox ID="txtEducation" runat="server" CssClass="form-control" ToolTip="Enter Education"
                                                TabIndex="6" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Employment :</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbEmployment" runat="server" AutoPostBack="true" TabIndex="7"
                                                RepeatDirection="Horizontal" ToolTip="Select Employment Yes/NO" OnSelectedIndexChanged="rdbEmployment_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Text="Yes" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Selected="True" Text="No" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3" id="divEmployment" runat="server" visible="false">
                                            <label>Name of Employment  :</label>
                                            <asp:TextBox ID="txtEmployment" runat="server" CssClass="form-control" ToolTip="Enter Employment" onkeyup="validateAlphabet(this);"
                                                TabIndex="8" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3" id="divPost" runat="server" visible="false">
                                            <label>Post :</label>
                                            <asp:TextBox ID="txtPost" runat="server" CssClass="form-control" ToolTip="Enter Post" onkeyup="validateAlphabet(this);"
                                                TabIndex="9" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3" id="divOrgName" runat="server" visible="false">
                                            <label>Organization Name and Address :</label>
                                            <asp:TextBox ID="txtOrgName" runat="server" CssClass="form-control" ToolTip="Enter Organization Name and Address"
                                                TabIndex="10" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3" id="divSalary" runat="server" visible="false">
                                            <label>Salary :</label>
                                            <asp:TextBox ID="txtSal" runat="server" CssClass="form-control" ToolTip="Enter Salary Details" onkeyup="validateNumeric(this);"
                                                TabIndex="11" MaxLength="10"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label>Marital Status :</label>
                                            <asp:DropDownList ID="ddlMarital" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                ToolTip="Select Marital Status" TabIndex="12" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Married</asp:ListItem>
                                                <asp:ListItem Value="2">Un-Married</asp:ListItem>
                                                <asp:ListItem Value="3">Widow</asp:ListItem>
                                                <asp:ListItem Value="4">Divorcee</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <sup></sup>
                                            <label>Aadhar no :</label>
                                            <asp:TextBox ID="txtAdharno" runat="server" CssClass="form-control" ToolTip="Enter Aadhar Number" onkeyup="validateNumeric(this);"
                                                TabIndex="13" MaxLength="12"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvAadhar" runat="server" ControlToValidate="txtAdharno"
                                                Display="None" ErrorMessage="Please Enter  Aadhar Number" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label>Mobile no :</label>
                                            <asp:TextBox ID="txtMob" runat="server" CssClass="form-control" ToolTip="Enter Mobile Number" onkeyup="validateNumeric(this);"
                                                TabIndex="14" MaxLength="10"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <label>Blood group :</label>
                                            <asp:DropDownList ID="ddlBloodGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                ToolTip="Select Blood group" TabIndex="15" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>



                                        <div class="form-group col-lg-3 ">
                                            <label>Select Address :</label>
                                            <br />
                                            <div class="form-group col-lg-12">

                                                <%-- <asp:RadioButton ID="rdbNewAddress" runat="server"  Text="New" Checked="false" TabIndex="11" AutoPostBack="true" OnCheckedChanged="rdbNewAddress_CheckedChanged"/>&nbsp;
                                                    <asp:RadioButton ID="rdbAsPermanentAdd" runat="server"  Text="Same As Permanent" Checked="false" TabIndex="11" AutoPostBack="true" OnCheckedChanged="rdbAsPermanentAdd_CheckedChanged"/>&nbsp;
                                                    <asp:RadioButton ID="rdbAsLocalAdd" runat="server"  Text="Same As Local" Checked="false" TabIndex="11" AutoPostBack="true" OnCheckedChanged="rdbAsLocalAdd_CheckedChanged"/>&nbsp;               
                                                --%>

                                                <asp:RadioButton ID="rdbNewAddress" runat="server" Text="New" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbNewAddress_CheckedChanged" />
                                                <asp:RadioButton ID="rdbAsPermanentAdd" runat="server" Text="Same As Permanent" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbAsPermanentAdd_CheckedChanged" />
                                                <asp:RadioButton ID="rdbAsLocalAdd" runat="server" Text="Same As Local" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbAsLocalAdd_CheckedChanged" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="troff" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Designation Of Attesting Officer</label>
                                        </div>
                                        <asp:TextBox ID="txtOfficer" runat="server" CssClass="form-control" ToolTip="Enter Designation Of Attesting Officer"
                                            onkeyup="validateAlphabet(this);" TabIndex="16"></asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <%--<div class="form-group col-lg-3 col-md-6 col-12">--%>


                                        <div class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>Country :</label>
                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" TabIndex="17" MaxLength="80"
                                                    ToolTip="Enter Country Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>State :</label>
                                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" TabIndex="18" MaxLength="80"
                                                    ToolTip="Enter State Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>City/Town :</label>
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TabIndex="19" MaxLength="100"
                                                    ToolTip="Enter City Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div id="taluka" class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>Taluka :</label>
                                                <asp:TextBox ID="txtTaluka" runat="server" CssClass="form-control" TabIndex="20" MaxLength="80"
                                                    ToolTip="Enter Taluka Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>District :</label>
                                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" TabIndex="21" MaxLength="80"
                                                    ToolTip="Enter District Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>Address :</label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TabIndex="22" MaxLength="200"
                                                ToolTip="Enter Address" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <div class="label-dynamic">
                                                <label>Pincode :</label>
                                                <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" TabIndex="23" MaxLength="6"
                                                    ToolTip="Enter Pincode" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="flupld" runat="server" TabIndex="24" ToolTip="Click to  Upload Document" />
                                            <span style="color: red">Upload Document Maximum Size 10 Mb</span>
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
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="25"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="26"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvFamilyInfo" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Family Details"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Family Particular Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Family Member 
                                                        </th>
                                                        <th>Relation
                                                        </th>
                                                        <th>DOB
                                                        </th>
                                                        <th>Age
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Approve Status
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("fnno")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("fnno") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("memname")%>
                                                </td>
                                                <td>
                                                    <%# Eval("relation")%>
                                                </td>
                                                <td>
                                                    <%# Eval("dob", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("age")%>
                                                </td>
                                                <td id="tdFolder" runat="server">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("FNNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
                                                <td>
                                                    <%# Eval("APPROVE_STATUS")%>
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

            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
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
                                <td>&nbsp;&nbsp;Are you sure you want to delete this record ?</td>
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
            <script type="text/javascript">
                function CheckDateEalier(sender, args) {
                    if (sender._selectedDate > new Date()) {
                        alert("Future Date Not Accepted for Date Of Birth.");
                        sender._selectedDate = new Date();
                        sender._textbox.set_Value("");
                    }
                }

                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = '';
                        alert('Only Numeric Characters Allowed!');
                        txt.focus();
                        return;
                    }
                }
            </script>


            </script>
            <%--  <script type="text/javascript" language="javascript">
                function checkdate(input) {
                    var data = document.getElementById("txtDateOfBirth");
                    if (data != null) {
                        var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
                        var returnval = false
                        if (!validformat.test(input.value)) {
                            alert("Invalid Date Format. Please Enter in DD/MM/YYYY Formate")
                            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                        }
                        else {
                            var monthfield = input.value.split("/")[0]

                            if (monthfield > 12 || monthfield <= 0) {
                                alert("Month Should be greate than 0 and less than 13");
                                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                            }
                        }
                    }
                }
    </script>--%>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

