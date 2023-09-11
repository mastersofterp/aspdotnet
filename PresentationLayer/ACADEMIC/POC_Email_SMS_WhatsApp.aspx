<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POC_Email_SMS_WhatsApp.aspx.cs" Inherits="ACADEMIC_POC_Email_SMS_WhatsApp" Title="" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
            </div>
            <asp:UpdatePanel ID="updRule" runat="server">
                <ContentTemplate>
                    <div class="col-12 mt-3">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Name</label>
                                </div>
                                <asp:TextBox ID="txtName" runat="server" ToolTip="Please enter name." TabIndex="1" MaxLength="32" onkeypress="return (event.charCode>64 && event.charCode<91 || event.charCode>96 && event.charCode<123 || event.charCode==32 || event.charCode==46)" AutoComplete="off"></asp:TextBox>
                            </div>
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Email Message</label>
                                </div>
                                <asp:TextBox ID="txtMessage" runat="server" ToolTip="Please enter email message." TabIndex="2" MaxLength="64" onkeypress="return (event.charCode>64 && event.charCode<91 || event.charCode>96 && event.charCode<123 || event.charCode==32)" AutoComplete="off"></asp:TextBox>
                            </div>
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Email Subject</label>
                                </div>
                                <asp:TextBox ID="txtSub" runat="server" ToolTip="Please enter email subject." TabIndex="3" MaxLength="64" onkeypress="return (event.charCode>64 && event.charCode<91 || event.charCode>96 && event.charCode<123 || event.charCode==32)" AutoComplete="off"></asp:TextBox>
                            </div>
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Email</label>
                                </div>
                                <asp:TextBox ID="txtMail" runat="server" ToolTip="Please enter email." TabIndex="4" MaxLength="64" onkeypress="return (event.charCode>96 && event.charCode<123 || event.charCode==64  || event.charCode==46 || event.charCode>47 && event.charCode<58)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Mobile No</label>
                                </div>
                                <asp:TextBox ID="txtMobile" runat="server" ToolTip="Please enter mobile no." TabIndex="5" MaxLength="10"
                                    onkeypress="return (event.charCode>47 && event.charCode<58)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <%--Added by Nikhil L. on 04-09-2023--%>
                              <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Attachment</label>
                                </div>
                                <asp:FileUpload ID="fuAttach" runat="server" ToolTip="Please select file." TabIndex="6" />
                            </div>
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>CC Mail</label>
                                </div>
                                <asp:TextBox ID="txtCC" runat="server" ToolTip="Please enter CC Mail." TabIndex="1" MaxLength="32" onkeypress="return (event.charCode>96 && event.charCode<123 || event.charCode==64  || event.charCode==46 || event.charCode>47 && event.charCode<58)" AutoComplete="off"></asp:TextBox>
                            </div>
                               <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>BCC Mail</label>
                                </div>
                                <asp:TextBox ID="txtBCC" runat="server" ToolTip="Please enter BCC Mail." TabIndex="1" MaxLength="32" onkeypress="return (event.charCode>96 && event.charCode<123 || event.charCode==64  || event.charCode==46 || event.charCode>47 && event.charCode<58)" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnEmail" runat="server" Text="Send Email" ToolTip="Click to send email." TabIndex="6" OnClick="btnEmail_Click" CssClass="btn btn-outline-primary" OnClientClick="return validateEmail();"/>
                        <asp:Button ID="btnSMS" runat="server" Text="Send SMS" ToolTip="Click to send SMS." TabIndex="7" OnClick="btnSMS_Click" CssClass="btn btn-outline-primary" OnClientClick="return validateSMS();"/> 
                        <asp:Button ID="btnWhatsapp" runat="server" Text="Send WhatsApp" ToolTip="Click to send whatsapp." TabIndex="8" OnClick="btnWhatsapp_Click" CssClass="btn btn-outline-success" OnClientClick="return validateSMS();" /> 
                         <asp:Button ID="btnClear" runat="server" Text="Clear" ToolTip="Click to clear" TabIndex="9" OnClientClick="Clear();"  CssClass="btn btn-outline-warning"/>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnEmail" />
                </Triggers>
            </asp:UpdatePanel>
            <script>
                function Clear() {
                    document.getElementById('<%= txtName.ClientID %>').value = "";
                    document.getElementById('<%= txtMail.ClientID %>').value = "";
                    document.getElementById('<%= txtMobile.ClientID %>').value = "";                   
                }
                function validateEmail()
                {
                    var name=document.getElementById('<%= txtName.ClientID %>').value;
                    var email = document.getElementById('<%= txtMail.ClientID %>').value;
                    var message = document.getElementById('<%= txtMessage.ClientID %>').value;
                    var subject = document.getElementById('<%= txtSub.ClientID %>').value;
                    var attach = document.getElementById('<%=fuAttach.ClientID%>');
                    var length;
                    length = attach.files.length;
                    var alertMsg = "";
                    if (name == "" || email == "" || message == "" || subject == "") {
                        if (name == "") {
                            alertMsg += "Please enter name.\n";
                        }
                        if (message == "") {
                            alertMsg += "Please enter email message.\n";
                        }
                        if (subject == "") {
                            alertMsg += "Please enter email subject.\n";
                        }
                        if (email == "") {
                            alertMsg += "Please enter email.\n";
                        }
                        //if (length == 0) {
                        //    alertMsg += "Please select file to upload.\n";
                        //}
                        alert(alertMsg);
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                function validateSMS() {
                    var name = document.getElementById('<%= txtName.ClientID %>').value;
                    var mobile = document.getElementById('<%= txtMobile.ClientID %>').value;
                    var alertMsg = "";
                    if (name == "" || mobile == "") {
                        if (name == "") {
                            alertMsg += "Please enter name.\n";
                        }
                        if (mobile == "") {
                            alertMsg += "Please enter mobile no.\n";
                        }
                        alert(alertMsg);
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            </script>

        </div>
    </div>
</div>
    </asp:Content>
