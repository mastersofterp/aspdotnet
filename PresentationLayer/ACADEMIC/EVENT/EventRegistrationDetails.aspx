<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventRegistrationDetails.aspx.cs" Inherits="ACADEMIC_EVENT_EventRegistrationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>MAKAUT,West Bengal</title>
    <link rel="shortcut icon" href="../../imgnew1/fevicon.ico" type="image/x-icon">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- CSS -->
    <link href="../../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../dist/css/AdminLTE.css" rel="stylesheet" />
    <script src="../../bootstrap/js/bootstrap.min.js"></script>

    <script src='steps.js' type='text/javascript'></script>
    <style>
        body {
            background-color: #F2F2F2;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server" method="post">
        <div class="wrapper">


            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updEvent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="container">
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-12">
                                <%--<div class="col-md-2"></div>--%>
                                <%--<div class="col-md-8">--%>
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <div class="col-md-2 col-xs-4">
                                            <%--<img src="../../imgnew1/MAKAUAT_LOGO.png" class="img-responsive" width="75%" />--%>
                                            <img src='<%=ResolveUrl("~/imgnew1/MAKAUAT_LOGO.png")%>' class="img-responsive" width="75%" />
                                        </div>
                                        <div class="col-md-9 col-xs-12" style="text-align: center;">
                                            <h2 style="margin-top: 3px;">Maulana Abul Kalam Azad University of Technology</h2>
                                            <p class="text-muted">
                                                <h4>SIMHAT, HARINGHATA, NADIA, WEST  BENGAL, INDIA - 741249</h4>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-md-9" id="divHead" runat="server" visible="true" style="text-align: left">
                                        <h3>
                                            <div class="label label-default">Event Registration Details</div>
                                        </h3>

                                    </div>
                                    <%--<hr style="margin: 1px;" />--%>

                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-12 table table-responsive">
                                                <asp:Panel ID="pnlEventReg" runat="server" Visible="true">
                                                    <asp:ListView ID="lvEventReg" runat="server" Visible="true">
                                                        <LayoutTemplate>
                                                            <%--<div>--%>
                                                            <table class="table table-hover table-bordered">
                                                                <thead>
                                                                    <tr class="bg-light-blue" style="background-color: #337ab7; color: white">
                                                                        <th>Sr No.</th>
                                                                        <th style="width: 30%">Event Title
                                                                        </th>
                                                                        <th style="width: 20%">Event Date
                                                                        </th>
                                                                        <th style="width: 9%">Registration End Date
                                                                        </th>
                                                                        <th style="width: 35%; text-align: center">Event Description
                                                                        </th>
                                                                        <th style="width: 5%">Brochure
                                                                        </th>
                                                                        <th style="width: 10%">Registration Link
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                            <%--</div>--%>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:Label ID="lblEventTitle" runat="server" Text='<%# Eval("EVENT_TITLE") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:Label ID="lblEventDate" runat="server" Text='<%# Eval("EVENT_DATE") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 9%">
                                                                    <asp:Label ID="lblEventRegisterEndDate" runat="server" Text='<%# Eval("EVENT_REG_END_DATE") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblEventDesc" runat="server" Text='<%# Eval("EVENT_DESC") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center; width: 5%">
                                                                    <asp:ImageButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" Visible='<%# Convert.ToString(Eval("EVENT_BROCHURE"))== string.Empty ? false:true %>' ImageUrl="~/IMAGES/down-arrow.png" CommandArgument='<%# Eval("EVENT_BROCHURE") %>' />
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary" Text="Click Here To Register" OnClick="btnRegister_Click" CommandArgument='<%# Eval("EVENT_TITLE_ID") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <asp:HiddenField ID="hdnRegister" runat="server" />
                                            <div class="col-md-12" style="text-align: center" id="divHome" runat="server" visible="true">
                                                <asp:Button ID="btnHome" runat="server" Text="Go To Home" CssClass="btn btn-primary" OnClick="btnHome_Click" />
                                            </div>

                                            <div id="divRegister" runat="server" visible="false">
                                                <div class="col-md-9" style="margin-left: 18px; text-align: left">
                                                    <h3>
                                                        <div class="label label-default">Event Registration Form</div>
                                                    </h3>
                                                </div>
                                                <%--<div></div>--%>
                                                <div class="col-md-12">
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Candidate Name(To be printed on certificate)</label>
                                                        <asp:TextBox ID="txtName" runat="server" TabIndex="1" ValidationGroup="Save" CssClass="form-control" MaxLength="128" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Enter Candidate Name"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" InvalidChars="`~!@#$%^&*()_-++{[}}:;'|\,>?/01234567890"
                                                            FilterMode="InvalidChars" TargetControlID="txtName">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Mobile</label>
                                                        <asp:TextBox ID="txtMobile" runat="server" TabIndex="2" ValidationGroup="Save" CssClass="form-control" MaxLength="10" onblur="CheckAvailabilityMobile()" onchange="return Mobile(this);" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Enter Mobile"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                                            FilterMode="ValidChars" TargetControlID="txtMobile">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <span id="message1" style="font-size: small;"></span>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Email</label>
                                                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="3" ValidationGroup="Save" CssClass="form-control" MaxLength="64" TextMode="Email" onblur="CheckAvailability()" onchange="return checkEmail(this);" autocomplete="off"></asp:TextBox>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="reEmail" runat="server" ErrorMessage="Invalid Email ID" Display="None" ControlToValidate="txtEmail"
                                                            ValidationGroup="Save" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"></asp:RegularExpressionValidator>
                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" InvalidChars="`~!#$%^&*()_-+={[}]:;'<,>?/"
                                            FilterMode="InvalidChars" TargetControlID="txtEmail">--%>
                                                        <%-- </ajaxToolKit:FilteredTextBoxExtender>--%>

                                                        <span id="message" style="font-size: small;"></span>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Gender</label>
                                                        <asp:RadioButtonList ID="rdoGender" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" TabIndex="4" ValidationGroup="Save">
                                                            <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rdoGender" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Select Gender"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Participant</label>
                                                        <asp:DropDownList ID="ddlParticipant" runat="server" OnSelectedIndexChanged="ddlParticipant_SelectedIndexChanged" AutoPostBack="true" TabIndex="5" CssClass="form-control" ValidationGroup="Save" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvParticipant" runat="server" ControlToValidate="ddlParticipant" ValidationGroup="Save" Display="None"
                                                            InitialValue="0" ErrorMessage="Please Select Participant"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>State</label>
                                                        <asp:DropDownList ID="ddlState" runat="server" TabIndex="6" CssClass="form-control" ValidationGroup="Save" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlState" ValidationGroup="Save" Display="None"
                                                            InitialValue="0" ErrorMessage="Please Select State"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>City</label>
                                                        <asp:TextBox ID="txtCity" runat="server" TabIndex="7" CssClass="form-control" ValidationGroup="Save" MaxLength="64" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Enter City"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" InvalidChars="1234567890`~!@#$%^&*()_-+={[}]:;'<,>?/"
                                                            FilterMode="InvalidChars" TargetControlID="txtCity">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <div style="padding-top:20px;">
                                                            <label>Payment : </label>
                                                            <asp:Label ID="lblPayment" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Organisation Name</label>
                                                        <asp:TextBox ID="txtOrgName" runat="server" TabIndex="8" CssClass="form-control" ValidationGroup="Save" MaxLength="164" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOrgName" runat="server" ControlToValidate="txtOrgName" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Enter Organization Name"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" InvalidChars="1234567890`~!@#$%^&*()_-+={[}]:;'<,>?/"
                                                            FilterMode="InvalidChars" TargetControlID="txtOrgName">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <span style="color: red">* </span>
                                                        <label>Organisation Address</label>
                                                        <asp:TextBox ID="txtAddress" runat="server" TabIndex="9" CssClass="form-control" ValidationGroup="Save" MaxLength="256" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" ValidationGroup="Save" Display="None"
                                                            ErrorMessage="Please Enter Organization Address"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" InvalidChars="`~!@#$%^&*()_+={[}]:;'<>?/"
                                                            FilterMode="InvalidChars" TargetControlID="txtAddress">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="text-align: center">
                                                    <asp:Button ID="btnSave" runat="server" Visible="false" Text="Submit Registration" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSave_Click" TabIndex="10" />
                                                    <asp:Button ID="btnOnlinePayment" runat="server" Visible="false" Text="Submit Registration & Payment" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnOnlinePayment_Click" TabIndex="10" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" TabIndex="11" />
                                                    <asp:Button ID="btnBack" runat="server" Text="Back To Event Registration Details" CssClass="btn btn-primary" OnClick="btnBack_Click" TabIndex="12" />
                                                    <asp:ValidationSummary ID="vsSave" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" DisplayMode="List" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--</div>--%>
                            </div>
                        </div>
                        <script type="text/javascript" lang="javascript">
                            function CheckAvailability() {
                                //alert('a');
                                var username = document.getElementById("txtEmail").value;
                                var titleid = document.getElementById("hdnRegister").value;
                                //PageMethods.CheckEmail(username, function (response)
                                PageMethods.CheckEmail(username, titleid, function (response) {

                                    var message = document.getElementById("message");

                                    if (response) {
                                        //Username available.
                                        //ClearMessage();
                                        document.getElementById("message").innerHTML = "";

                                    }
                                    else {

                                        //Username not available.
                                        //message.style.color = "red";
                                        //message.innerHTML = "Email Id is Already Exits";
                                        alert('Email Id is Already Exists');
                                        document.getElementById('txtEmail').value = "";
                                        document.getElementById('txtEmail').focus();
                                    }
                                });
                            };

                            function CheckAvailabilityMobile() {

                                var mobile = document.getElementById("txtMobile").value;
                                var titleid = document.getElementById("hdnRegister").value;
                                //alert(mobile);
                                //alert(titleid);
                                PageMethods.CheckMobile(mobile, titleid, function (response) {
                                    var message = document.getElementById("message1");
                                    if (response) {
                                        //Username available.
                                        // ClearMessage();
                                        document.getElementById("message1").innerHTML = "";
                                    }
                                    else {

                                        //Username not available.
                                        //message.style.color = "red";
                                        //message.innerHTML = "Mobile No is Already Exits";
                                        alert('Mobile No is Already Exists');
                                        document.getElementById('txtMobile').value = "";
                                        document.getElementById('txtMobile').focus();
                                    }
                                });
                            };
                        </script>
                        <script type="text/javascript">
                            function Mobile() {
                                var MobileNo = document.getElementById('<%=txtMobile.ClientID%>').value;
                    if (MobileNo.length == 10) {
                    }
                    else {
                        alert('Please Enter Valid Mobile No.');
                        document.getElementById('txtMobile').value = "";
                        document.getElementById('txtMobile').focus();
                    }
                }


                function checkEmail(txt) {
                    var email = document.getElementById('txtEmail');
                    var filter = /^([a-z-0-9_\.\-])+\@(([a-z-0-9\-])+\.)+([a-z-0-9]{2,4})+$/;

                    //alert(txt.value);
                    if (!filter.test(txt.value)) {
                        alert('Please Enter Valid Email Address');
                        //email.focus;
                        document.getElementById('txtEmail').value = "";
                        document.getElementById('txtEmail').focus();
                        return false;
                    }
                }
                        </script>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnOnlinePayment" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="lvEventReg" />
                </Triggers>
            </asp:UpdatePanel>
            <footer class="text-center main-footer navbar-fixed-bottom " style="width: 100%; margin-left: 3px">
                <strong>Designed and Developed By:<a href="http://www.iitms.co.in" target="_blank"> MasterSoft </a>Copyright &copy; 2016.</strong> All rights reserved.
            </footer>

        </div>
    </form>
</body>
