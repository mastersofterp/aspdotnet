<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConsumerMaster.aspx.cs" Inherits="ESTATE_Master_ConsumerMaster" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:UpdatePanel ID="updpnlge" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">RESIDENT MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Resident Master
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            Search Resident
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="form-group row">
                                                                <div class="col-md-3">
                                                                    <label>Search Resident:</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                    <asp:HiddenField ID="hfInvNo" runat="server" />
                                                                    <ajaxToolKit:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server"
                                                                        Enabled="True" ServicePath="~/Autocomplete.asmx" TargetControlID="txtSearch"
                                                                        CompletionSetCount="6" ServiceMethod="GetRetemployeeNo" MinimumPrefixLength="3" CompletionInterval="0"
                                                                        OnClientItemSelected="GetRetemployeeNo" DelimiterCharacters="" UseContextKey="true">
                                                                    </ajaxToolKit:AutoCompleteExtender>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:ImageButton ID="imgserch" runat="server" Height="20px" Width="20px" ImageUrl="~/images/search.png" OnClick="imgserch_Click" TabIndex="2" />
                                                                    Search
                                                                </div>
                                                                <div class="col-md-1">
                                                                    <asp:ImageButton ID="imgbtnclearname" runat="server" Width="20px" ImageUrl="~/images/refresh.gif" OnClick="imgbtnclearname_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            Residents Detail
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="box box-primary">
                                                                <div class="box-body">
                                                                    <div class="form-group row" id="trResidentType" runat="server" visible="false">
                                                                        <div class="col-md-2">
                                                                            <label>Resident Type<span style="color: red;">*</span>:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:DropDownList ID="dllconsumertype" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3">
                                                                            </asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator  ID="rfvdllconsumertype"  runat="server"  ControlToValidate ="dllconsumertype" ValidationGroup="consumer" Display="None" InitialValue="0" ErrorMessage="Please Select Resident Type.">
                                                                                        </asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Title:</label>

                                                                            <asp:DropDownList ID="ddltitle" runat="server" CssClass="form-control" TabIndex="4">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Mr.</asp:ListItem>
                                                                                <asp:ListItem Value="2">Miss.</asp:ListItem>
                                                                                <asp:ListItem Value="3">Mrs.</asp:ListItem>
                                                                                <asp:ListItem Value="4">Prof.</asp:ListItem>
                                                                                <asp:ListItem Value="5">PH.D</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvddltitle" runat="server" ControlToValidate="ddltitle" ValidationGroup="consumer" Display="None" InitialValue="-1" ErrorMessage="Please Select Title">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>First Name<span style="color: red;">*</span>:</label>
                                                                            <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" TabIndex="5"
                                                                                MaxLength="20"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvtxtfirstname" runat="server" ControlToValidate="txtfirstname"
                                                                                ErrorMessage="Please Enter First Name." Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID ="ftbeFirstName" runat="server" FilterType="UppercaseLetters" TargetControlID="txtfirstname">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label>Middle Name:</label>
                                                                            <asp:TextBox ID="txtmiddlename" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);"
                                                                                MaxLength="20" TabIndex="6"></asp:TextBox>
                                                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID ="ftbemiddleName" runat="server" FilterType="UppercaseLetters" TargetControlID="txtmiddlename">
                                                                                       </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                            <%--<asp:RequiredFieldValidator ID="rfvtxtmiddlename" runat="server" ControlToValidate="txtmiddlename"
                                                                                       ErrorMessage="Please Enter Middle Name " Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label>Last Name:</label>
                                                                            <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);"
                                                                                MaxLength="20" TabIndex="7"></asp:TextBox>
                                                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID ="ftbeLastName" runat="server" FilterType="LowercaseLetters"  TargetControlID="txtlastname">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                            <%--<asp:RequiredFieldValidator ID="rfvtxtlastname" runat="server" ControlToValidate="txtlastname"
                                                                                         ErrorMessage="Please Enter Last Name " Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="box box-primary">
                                                                <div class="box-body">
                                                                    <div class="form-group row">
                                                                        <div class="col-md-2">
                                                                            <label>Gender:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:RadioButton ID="rdoMale" runat="server" Text="Male" GroupName="Sex" Checked="True"
                                                                                Style="font-weight: bold" TabIndex="8" />
                                                                            <asp:RadioButton ID="rdoFemale" runat="server" Text="Female" GroupName="Sex" Style="font-weight: bold"
                                                                                TabIndex="9" />
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <label>Marital Status:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:RadioButton ID="rdoSingle" runat="server" GroupName="MartialStatus" Checked="True"
                                                                                TabIndex="10" Text="Single" Font-Bold="true" />
                                                                            <asp:RadioButton ID="rdoMarried" runat="server" GroupName="MartialStatus" Text="Married"
                                                                                TabIndex="11" Font-Bold="true" />
                                                                        </div>
                                                                    </div>
                                                                    
                                                                    <div class="form-group row">
                                                                        <div class="col-md-2">
                                                                            <label>Date of Birth:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="input-group date" style="width: 200px">
                                                                                <asp:TextBox ID="txtdateofbirth" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>
                                                                                <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtdateofbirth" PopupButtonID="imgbirthday"
                                                                                    Enabled="True">
                                                                                </ajaxToolKit:CalendarExtender>
                                                                                <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtdateofbirth"
                                                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                    CultureTimePlaceholder="" Enabled="True" />
                                                                                <div class="input-group-addon">
                                                                                   <%-- <asp:Image ID="imgbirthday" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"
                                                                                        Height="16px" />--%>
                                                                                      <asp:ImageButton runat="Server" ID="imgbirthday" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <label>Date of Joining:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="input-group date" style="width: 200px">
                                                                                <asp:TextBox ID="txtdatejoing" runat="server" TabIndex="13" align="left"
                                                                                    CssClass="form-control"></asp:TextBox>
                                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtdatejoing" PopupButtonID="imgcal" Enabled="True">
                                                                                </ajaxToolKit:CalendarExtender>
                                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtdatejoing"
                                                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                    CultureTimePlaceholder="" Enabled="True" />
                                                                                <div class="input-group-addon">
                                                                                   <%-- <asp:Image ID="imgcal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" Height="16px" />--%>
                                                                                     <asp:ImageButton runat="Server" ID="imgcal" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                                                        
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <div class="col-md-2">
                                                                            <label>Department<%--<span style="color: red;">*</span>--%>:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="14">
                                                                            </asp:DropDownList>
                                                                            <%--  <asp:RequiredFieldValidator  ID="rfvddldepartment" InitialValue="0" runat="server" Display="None" ControlToValidate ="ddldepartment" ValidationGroup="consumer"  ErrorMessage="Please Select Department.">
                                                                                   </asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <label>Designation<%--<span style="color: red;">*</span>--%>:</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:DropDownList ID="ddldessignation" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="15">
                                                                            </asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator  ID="rfvddldessignation"  Display="0" runat="server"  ControlToValidate ="ddldessignation" InitialValue="0" ValidationGroup="consumer"  ErrorMessage="Please Select Designation.">
                                                                                </asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="box box-primary">
                                                                <div class="box-body">
                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Local Address<%-- <span style="color: red;">*</span>--%>:</label>
                                                                            <asp:TextBox ID="txtlocaladdress" runat="server" CssClass="form-control"
                                                                                TextMode="MultiLine" TabIndex="16"></asp:TextBox>
                                                                            <%--<asp:RequiredFieldValidator ID="rfvtxtlocaladdress" runat="server" ControlToValidate="txtlocaladdress"
                                                                                     ErrorMessage="Please Enter Local Address " Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label>Permanent Address<%--<span style="color: red;">*</span>--%>:</label>
                                                                            <asp:TextBox ID="txtpermanentaddress" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="17"></asp:TextBox>
                                                                            <%-- <asp:RequiredFieldValidator ID="rfvtxtpermanentaddress" runat="server" ControlToValidate="txtpermanentaddress"
                                                                                      ErrorMessage="Please Enter Permanent Address " Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label>Phone Number<%-- <span style="color: red;">*</span>--%>:</label>
                                                                            <asp:TextBox ID="txtphonenumber" runat="server" CssClass="form-control" TabIndex="18" MaxLength="10"></asp:TextBox>
                                                                           <%-- <asp:RequiredFieldValidator ID="rfvtxtphonenumber" runat="server" ControlToValidate="txtphonenumber"
                                                                                     ErrorMessage="Please Enter Phone Number." Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtphonenumber"
                                                                                Display="None" ErrorMessage="Please Enter Valid Phone Number" SetFocusOnError="True" ValidationExpression="[0-9]{10}" ValidationGroup="consumer" />
                                                                          
                                                                              <ajaxToolKit:FilteredTextBoxExtender ID="ftxNumber" runat="server" FilterType="Numbers" TargetControlID="txtphonenumber" ValidChars="1234567890+-">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Email ID <%--<span style="color: red;">*</span>--%>:</label>
                                                                            <asp:TextBox ID="txtempemail" runat="server" TabIndex="19" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtempemail"
                                                                                Display="None" ErrorMessage="Please Enter Valid EmailID" SetFocusOnError="True"
                                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="consumer"></asp:RegularExpressionValidator>
                                                                            <%--  <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtempemail"
                                                                                       ErrorMessage="Please Email Address." Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label>PAN Number <%-- <span style="color: red;">*</span>--%>:</label>
                                                                            <asp:TextBox ID="txtpannumber" runat="server" CssClass="form-control" TabIndex="20" MaxLength="10" Style="text-transform: uppercase;"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtpannumber" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                                                ValidChars=" ">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                            <%--<asp:RequiredFieldValidator ID="rfvtxtpannumber" runat="server" ControlToValidate="txtpannumber"
                                                                                   ErrorMessage="Please Enter Pan Number." Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <label>Status:</label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:RadioButton ID="rdoActive" runat="server" Text="Active" Style="font-weight: bold;" GroupName="chkstatus" TabIndex="21" />
                                                                            <asp:RadioButton ID="rdoDeactive" runat="server" Text="DeActive" Style="font-weight: bold;" GroupName="chkstatus" TabIndex="22" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-12 text-center">
                                                                    <asp:Button ID="btnsumit" runat="server" CssClass="btn btn-primary" Text="Submit"
                                                                        OnClick="btnsumit_Click" ValidationGroup="consumer" TabIndex="23" />
                                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-warning" Text="Reset"
                                                                        OnClick="btnreset_Click" TabIndex="24" />
                                                                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-info"
                                                                        Text="Import Data PayRoll" OnClick="btnImport_Click" TabIndex="25" Visible="false" />
                                                                    <asp:ValidationSummary ID="vsConsumer" runat="server" ValidationGroup="consumer" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript">
        function GetRetemployeeNo(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearch').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_hfInvNo').value = idno[1];
        }

        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46) {
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
    </script>
</asp:Content>
