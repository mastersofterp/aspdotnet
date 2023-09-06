<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mis_Account_Signing.aspx.cs"
    Inherits="ADMINISTRATION_Mis_Account_Signing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UC/feed_back.ascx" TagName="feedback" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GEC-MIS Account Signing Form</title>
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <!-- **** layout stylesheet **** -->
    <link rel="stylesheet" type="text/css" href="../CSS/style.css" />
    <!-- **** colour scheme stylesheet **** -->
    <link rel="stylesheet" type="text/css" href="../CSS/blue.css" />
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="updmain" runat="server">
        <ContentTemplate>
            <div id="main">
                <div id="logo">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="padding-left: 5px; padding-right: 5px">
                                <br />
                                &nbsp;<asp:Image ID="imgGec" runat="server" ImageUrl="~/IMAGES/head_01.jpg" />
                            </td>
                            <td align="left">
                                <br />
                                <h3>
                                    GEC- MIS SECTION</h3>
                                Academic Building, First Floor<br />
                                Goa College of Engineering, Farmagudi-403401<br />
                                Tel: (91) 832 2336406 Fax: (91) 832 2335021
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="content">
                    <div id="column2">
                        <center>
                            <h1>
                                GEC-MIS Account Signing Form
                            </h1>
                        </center>
                        <!-- **** INSERT PAGE CONTENT HERE **** -->
                        <ul>
                            <li><b>ID Card Number :</b>
                                <asp:Label ID="lblIDCardNumber" runat="server">
                                </asp:Label>
                            </li>
                            <li><b>Name :</b>
                                <asp:Label ID="lblName" runat="server">
                                </asp:Label>
                            </li>
                            <li><b>Designation :</b><asp:Label ID="lblDesignation" runat="server">
                            </asp:Label></li>
                            <li><b>Department :</b><asp:Label ID="lblDepartment" runat="server">
                            </asp:Label></li>
                        </ul>
                        <h1>
                            GEC-MIS Account Guideline</h1>
                        <p>
                            All full-time GEC staffs/students submitted Employee/student information or registration
                            form are entitled to a personal MIS account on joining the Institute (email account
                            only to staff). This account allows you to access the campus network, email system,
                            personal data file and print services.</p>
                        <ol>
                            <li><b>Account Termination</b></li>
                            <p>
                                <br />
                                1.1 Your GEC-MIS account and email id will be terminated on your last day of service.
                                You are advised to clear your mailbox and keep backup of other personal information
                                before your last day of service.
                            </p>
                            <li><b>User&#8217;s Responsibilities</b></li>
                            <p>
                                <br />
                                2.1 It is your responsibility to ensure that the use of your GEC-MIS account and
                                email id account abides with the terms and condition mentioned below. Non compliance
                                or deliberate breach of these terms and conditions is subject to disciplinary action.
                            </p>
                            <p>
                                2.2 You are required to keep your user ID and password confidential at all times.
                                Should you suspect that another person is using your account, change your password
                                immediately and report it to the MIS Section.
                            </p>
                            <p>
                                2.3 For security reasons, you are required to change your password frequently.
                            </p>
                            <li><b>Email</b></li>
                            <p>
                                <br />
                                3.1 Staff should not misuse their email accounts. Misuse includes the following:
                            </p>
                            <p>
                                &nbsp; 3.1.1 Using E-mail for purposes of defamation or personal attack.
                            </p>
                            <p>
                                &nbsp; 3.1.2 Send offensive or seditious material to other users, either within
                                the organization or outside.
                            </p>
                            <p>
                                &nbsp; 3.1.3 Create and send advertisements, chain letters and other unsolicited
                                type of messages.
                            </p>
                        </ol>
                        <h1>
                            Terms & Conditions<br />
                        </h1>
                        <p>
                            At all times the Goa College of Engineering MIS Section reserves the right to review,
                            suspend or terminate any User Account
                        </p>
                        <ol>
                            <li>The Computer Facilities provided to User Account holders by the college shall include
                                hardware, software and computing domains under the control of college including
                                the provision to Users of remote access to computer facilities. </li>
                            <li>Any act prohibited by the rules will be construed as grounds for the suspension
                                or termination of the User Account. Such action shall not exclude any action taken
                                by College to refer the matter to the proper authorities for the prosecution of
                                any breaches of the laws of India. </li>
                            <li>The following are PROHIBITED on the computer facilities: </li>
                            <ol type="a">
                                <li>Anything that may be considered as illegal or used to encourage any act that will
                                    be an offence under the laws of India. The laws of India include without limitation
                                    the Information technology act 2000, Indian Evidence Act, 1872.</li>
                                <li>Accessing, storage or downloading from any source or displaying, creating or transmitting
                                    in any form and language, of any obscene, distasteful, vulgar or sexually suggestive
                                    electronic pictures or graphics prohibited by the laws of INDIA. </li>
                                <li>Any use of obscene, distasteful, derogatory, vulgar or sexually suggestive or discriminatory
                                    language. </li>
                                <li>The provision by the User of on-line services or web pages for commercial purposes
                                    or disseminating information regarding politics and religion without the proper
                                    licenses from AND the written approval of the Head of the institute. </li>
                                <li>Permitting other persons to use the User Account or the computing facilities, whether
                                    that other person is a registered or unregistered User. </li>
                                <li>Unauthorized access, copying, destroying or deleting and altering or amending of
                                    data or software programs. </li>
                                <li>The introduction of &#8216;virus&#8217;, &#8216;worm&#8217; or any software program
                                    designed to alter any data or software in the computing and database facilities.
                                </li>
                                <li>The tapping of the computer facilities or its network without the written permission
                                    from the Head of the institute. </li>
                                <li>Copy, upload, post, publish, store, transmit, reproduce or distribute and use of
                                    unlicensed copyrighted software or materials, or information which is protected
                                    by copyright or other intellectual property laws. </li>
                                <li>The transmission, display or broadcasting of electronic messages or the use the
                                    computer facilities in any manner:</li>
                                <ol type="i">
                                    <li>to denigrate, satirise, degrade or defame any person, family, organization, nation,
                                        race, community, political or religious group; </li>
                                    <li>to affect or prevent any registered Users&#8217; use of the computer facilities;</li>
                                    <li>for commercial purposes (as in carrying on a business) without obtaining prior written
                                        permission from the Head of the institute.</li>
                                    <li>for or on behalf of any person, party, organisation or Principal that is not from
                                        GEC without obtaining prior written authorisation from the person, party, organisation
                                        or Principal AND the written permission from the Head of the institute.</li>
                                    <li>To send messages causing threat, harassment, annoyance, inconvenience or needless
                                        anxiety to any person whomsoever.</li>
                                </ol>
                            </ol>
                            <li>The User shall be personally liable for the maintenance of the User Account to prevent
                                the occurrence of any of the above mentioned events. </li>
                            <li>The receipt of any transmission or electronic message of a kind that is prohibited
                                must be reported immediately to helpdesk (phone: 300 email using feeback form).
                                Any failure to do so may result in the recipient being construed as a party to the
                                prohibited act and may suffer the sanctions mentioned above in paragraph 3 above.
                            </li>
                            <li>The User shall protect the secrecy of the password assigned at all times and shall
                                ensure that the same is not revealed or disclosed in any manner. The User shall
                                be fully responsible for and shall bear all charges, losses or damages arising from
                                any use of the User Account/or password howsoever the same may arise.</li>
                            <li>The User shall change User Account password from time to time to enhance its security.</li>
                            <li>The MIS section reserves the right to access files review or investigate any User
                                Account for the purposes of compliance of the Information technology act 2000. The
                                MIS section may disclose to the proper authorities the findings of any breaches
                                of the Information technology act 2000 and the Computer Misuse. Users are reminded
                                that any violation of the Computer Misuse will be liable to a fine or imprisonment
                                or both.</li>
                            <li>While every care would be taken to provide proper service with the Computing Facilities,
                                the MIS disclaims all liability whatsoever, for any loss of data howsoever caused
                                including without limitation, non-deliveries, misuses, misdeliveries or for the
                                contents, the accuracy or quality of information or resources available, received
                                or transmitted as a result of any disruption, interruption, suspension, and including
                                termination of the User Account.</li>
                        </ol>
                        <br />
                        <center>
                            <asp:CheckBox ID="chkSign" runat="server" Checked="true" Text="I accept all terms and conditions" />
                          <%--  <asp:RequiredFieldValidator ID="rfvchkSign" runat="server" ControlToValidate="chkSign"
                                Display="None" ErrorMessage="Please accept all terms and conditions" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                        </center>
                        <br />
                        <center>
                            <%-- <asp:Button ID="butsubmit" runat="server" Text="Submit" OnClick="butsubmit_Click"
                                OnClientClick="showConfirmDel(this); return false;" />--%>
                            <asp:ImageButton ID="imgSubmit" runat="server" ImageUrl="~/IMAGES/submit.gif" ToolTip="Submit"
                                OnClientClick="showConfirmDel(this); return false;" OnClick="imgSubmit_Click" />
                        </center>
                    </div>
                </div>
                <br />
                <div id="footer">
                    copyright &copy; GEC GOA | Website:www.gec.ac.in, Email:ppl@gec.ac.in |design by
                    : It Is The Master's Software
                </div>
            </div>
            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div style="text-align: center">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                            </td>
                            <td>
                                &nbsp;&nbsp;Are you sure you have accepted all terms and conditions?
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;
    
    function showConfirmDel(source){
        this._source = source;
        this._popup = $find('mdlPopupDel');
        
        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }
    
    function okDelClick(){
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }
    
    function cancelDelClick(){
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
