<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Reply.aspx.cs" Inherits="JumpyForum.Reply" MaintainScrollPositionOnPostback="true" %>

<%--<%@ Page Language="c#" Inherits="JumpyForum.Reply" CodeFile="Reply.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >--%>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
  

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
                            <asp:Panel ID="pnlDiscussion" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="panel-heading col-12">
                                            <div class="box-tools pull-right">
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Visible="false">Add a comment</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>View comment</h5>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Title:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lbltitle" runat="server" Font-Size="Medium" Font-Names="Arial">Label</asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblname" runat="server" Font-Size="Medium" Font-Names="Arial">Label</asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Email :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblemail" runat="server" Font-Size="Medium" Font-Names="Arial">Label</asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Date Added  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDate" runat="server" Font-Size="Medium" Font-Names="Arial">Label</asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Comments :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblComment" runat="server" Font-Size="Medium" Font-Names="Arial">Label</asp:Label>
                                                    </a>
                                                </li>

                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row mt-4">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Reply to Question</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Message Type</label>
                                            </div>
                                            <font face="Arial" size="2"> 								
										        <input type="radio" name="MsgType" ID="MsgType_4" value="4" runat="server">
										        <Label for="MsgType_4">
                                                    <img title='Question' align="absMiddle" src="../Images/question.gif">Question 
										        </Label> 
											
										        <input type="radio" name="MsgType" ID="MsgType_1" value="1" checked runat="server">
										        <Label for="MsgType_1">
                                                    <img title='General Comment' align="absMiddle" src="../Images/general.gif">General 
										        </Label> 

										        <input type="radio" name="MsgType" ID="MsgType_2" value="2" runat="server">
										        <Label for="MsgType_2">
										            <img title='News' align="absMiddle" src="../Images/info.gif">News
										        </Label> 
											
									            <input type="radio" name="MsgType" ID="MsgType_3" value="3" runat="server">
										            <Label for="MsgType_8">
                                                        <img title='Answer' align="absMiddle" src="../Images/answer.gif">Answer
										            </Label> 
											
										        <input type="radio" name="MsgType" ID="MsgType_5" value="5" runat="server" onserverchange="MsgType_5_ServerChange">
										        <Label for="MsgType_16">
                                                    <img title='Joke / Game' align="absMiddle" src="../Images/game.gif">Joke/Game
										        </Label>

                                              </font>
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
                                                <label>Attachment Files </label>
                                            </div>
                                            <asp:FileUpload ID="fuForum" runat="server" TabIndex="4" />&nbsp;&nbsp;&nbsp;                                
                                                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                            <asp:Label ID="lblPreAttach" runat="server" Height="21px" Text="Label" Visible="False"></asp:Label>

                                            <div class=" text-bold">
                                                (Max.Size 10MB<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-9 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Reply</label>
                                            </div>
                                            <CKEditor:CKEditorControl ID="txtcomment" runat="server" Height="150" BasePath="~/plugins/ckeditor" TabIndex="3">		                                     
                                            </CKEditor:CKEditorControl>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Comment required"
                                                ControlToValidate="txtcomment" Font-Size="10pt"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnAddReply" runat="server" TabIndex="5" Text="Add Forum" CssClass="btn btn-primary"
                                        OnClick="btnAddReply_Click" ToolTip="Click here to Add Forum"></asp:Button>
                                    <asp:Button ID="btnBack" runat="server" TabIndex="5" Text="Back" CausesValidation="False"
                                        OnClick="btnBack_Click" CssClass="btn btn-warning" ToolTip="Click here to Go Back"></asp:Button>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


