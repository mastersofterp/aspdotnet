<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="NewMessage.aspx.cs" Inherits="JumpyForum.NewMessage" MaintainScrollPositionOnPostback="true" %>

<%--<%@ Page language="c#" Inherits="JumpyForum.NewMessage" CodeFile="NewMessage.aspx.cs" %>--%>

<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >--%>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

<%--    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE NEW QUESTION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDiscussion" runat="server">
                                    <div class="row">
                                        <%--<div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Create New Question</h5>
                                            </div>
                                        </div>--%>
                                        <div class="box-tools pull-right">
                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Visible="false">Add a comment</asp:Label>
                                        </div>
                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Message Type :</label>
                                            </div>
                                            <div tabindex="1">
                                                <font face="Arial" size="2"> 								
										                    <input type="radio" name="MsgType" ID="MsgType_4" value="4" runat="server">
										                    <Label for="MsgType_4">
                                                                Question &nbsp;&nbsp;
										                    </Label> 
											
										                    <input type="radio" name="MsgType" ID="MsgType_1" value="1" checked runat="server">
										                    <Label for="MsgType_1">
                                                             General 
										                    </Label> 
											
										                    <input type="radio" name="MsgType" ID="MsgType_2" value="2" runat="server">
										                    <Label for="MsgType_2">
										                        News 
										                    </Label> 
											
									                        <input type="radio" name="MsgType" ID="MsgType_3" value="3" runat="server">
										                        <Label for="MsgType_8">
                                                                   Answer
										                        </Label> 
											
										                    <input type="radio" name="MsgType" ID="MsgType_5" value="5" runat="server" onserverchange="MsgType_5_ServerChange">
										                    <Label for="MsgType_16">
                                                               Joke/Game
										                    </Label></font>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Title</label>
                                            </div>
                                            <asp:TextBox ID="txtsubject" runat="server" CssClass="form-control" TabIndex="2"
                                                placeholder="Give a title that best describes your assignment (Max 128 characters)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                ControlToValidate="txtsubject" ErrorMessage="Title Required"></asp:RequiredFieldValidator>

                                        </div>
                                           <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Attachment Files</label>
                                            </div>
                                            <asp:FileUpload ID="fuForum" runat="server" TabIndex="4" />                             
                                                         <asp:HiddenField ID="hdnFile" runat="server" />
                                            <asp:Label ID="lblPreAttach" runat="server" Height="21px" Text="Label" Visible="False"></asp:Label>

                                            (Max.Size 10MB<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)
                                                     
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Reply</label>
                                            </div>
                                            <CKEditor:CKEditorControl ID="txtcomment" runat="server" Height="200" BasePath="~/plugins/ckeditor" TabIndex="3">		                                     
                                            </CKEditor:CKEditorControl>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Comment required"
                                                ControlToValidate="txtcomment" Font-Size="10pt"></asp:RequiredFieldValidator>

                                        </div>
                                      </div>
                                         <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnAddForum" runat="server" TabIndex="5" Text="Add Forum" CssClass="btn btn-primary"
                                                                    OnClick="btnAddForum_Click" ToolTip="Click here to Add Forum"></asp:Button>
                                                                <asp:Button ID="btnBack" runat="server" TabIndex="5" Text="Back" CausesValidation="False"
                                                                    OnClick="btnBack_Click" CssClass="btn btn-primary" ToolTip="Click here to Go Back"></asp:Button>
                                                           
                                                        </div>
                                   
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
