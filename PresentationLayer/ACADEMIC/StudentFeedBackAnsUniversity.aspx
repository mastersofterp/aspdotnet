<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentFeedBackAnsUniversity.aspx.cs" Inherits="ACADEMIC_StudentFeedBackAns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 40%; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <img src="../IMAGES/anim_loading_75x75.gif" alt="Loading" />
                    <%--Please Wait..--%>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>
             <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT FEEDBACK ANSWERS</h3>
                        </div>

                        <div class="box-body">
        
               
           
                        <asp:Panel ID="pnlStudInfo" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>Name:</label><br />
                                                <asp:Label ID="lblName" runat="server" Font-Bold="true" style="color:green"></asp:Label>
                                            </div>
                                             <div class="col-md-4">
                                                <label>Academic Year:</label><br />
                                                 <asp:Label ID="lblSession" runat="server" Font-Bold="true" style="color:green"></asp:Label>
                                            </div>
                                             <div class="col-md-4">
                                                <label>Scheme:</label><br />
                                                   <asp:Label ID="lblScheme" runat="server" Font-Bold="true" style="color:green"></asp:Label>
                                        <asp:HiddenField ID="hdnFinalSem" runat="server" Value="0" />
                                            </div>
                                             <div class="col-md-4">
                                                <label>Semester:</label><br />
                                                   <asp:Label ID="lblSemester" runat="server" Font-Bold="true" style="color:green"></asp:Label>
                                            </div>
                                             <div class="col-md-4">
                                                <label>College:</label><br />
                                                 <asp:Label ID="lblCollegeName" runat="server" Font-Bold="true" style="color:green"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                          <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                    </div>
                                </div>
                            </div>
                            <div class="row text-center">
                                <asp:Label ID="lblMsg" runat="server" Visible="false"> 
                                             <%--<span ID="spMsg" style="color:Red;"></span>--%>
                                            </asp:Label>
                            </div>
                            <div class="row text-center">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report"
                                                ValidationGroup="Report" Visible="false" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Cancel" Visible="false" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                            </div>
                             </asp:Panel>
                         <%--   <table cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td style="width: 15%;">Name :
                                    </td>
                                    <td style="width: 30%;">
                                        
                                    </td>
                                    <td rowspan="6" style="width: 100px" valign="top">
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">Academic Year :
                                    </td>
                                    <td style="width: 30%;">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>Scheme :
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>Semester :
                                    </td>
                                    <td>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>College :
                                    </td>
                                    <td>
                                        <b>
                                            
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <b>
                                            
                                        </b>
                                    </td>
                                    <tr>
                                        <td align="center" colspan="3">
                                            
                                        </td>
                                    </tr>
                                </tr>
                            </table>--%>
                       
                        <%-- </fieldset>--%>
                   <%-- </td>
                </tr>--%>
                <%--<tr>
                        <td style="background-color: White" width="100%">
                            <b>Ratings :</b>&nbsp;&nbsp;&nbsp;5-Strongly Agree &nbsp;&nbsp; 4-Agree &nbsp;&nbsp;
                            3-Niether agree nor disagree &nbsp;&nbsp; 2-Disagree &nbsp;&nbsp; 1-Strongly disagree
                        </td>
                    </tr>--%>
                <asp:Panel ID="pnlFeedback" runat="server" Visible="false">
                    <div class="row">
                        <div class="container-fluid">
                            <div class="col-md-12">
                                 <asp:ListView ID="lvUniversity" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="titlebar">
                                           
                                            <h4> UNIVERSITY</h4>
                                        </div>
                                        <table class="table table-bordered table-hovered">
                                            <%--<thead>
                                                    <tr class="header">
                                                        <th width="5%">
                                                            &nbsp;Sr.No.&nbsp;
                                                        </th>
                                                        <th width="95%" style="text-align: center;">
                                                            &nbsp;Questions&nbsp;
                                                        </th>
                                                        <<th width="45%" style="text-align: center;">
                                                            &nbsp;Options&nbsp;
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </thead>--%>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                        <%--<table cellpadding="2" cellspacing="2" width="100%">
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>--%>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td width="5%">Q&nbsp;<asp:Label ID="lblUniversity" runat="server" Text='<%#Eval("SRNO") %>' ToolTip='<%# Eval("QUESTIONID") %>'>
                                        </asp:Label>.
                                        </td>
                                        <td width="95%">
                                            <%# Eval("QUESTIONNAME") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">Ans:&nbsp;
                                        </td>
                                        <td width="95%">
                                            <asp:RadioButtonList ID="rblUniversity" runat="server" RepeatDirection="Horizontal" TabIndex="4"
                                                RepeatLayout="Flow" Width="100%" ToolTip="Click to select this answer">
                                            </asp:RadioButtonList>
                                            <asp:HiddenField ID="hdnUniversity" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                            <hr style="width: 100%" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                             <h4>Please write your suggestions/comments (max. 200 characters) if any to improve the
                                        teaching-learning process:</h4>
                        </div>
                        <div class="form-group col-md-4">
                            <label>(a) Name one student activity you like best ?</label>
                             <asp:TextBox ID="txtSuggestionA" runat="server" MaxLength="200" TextMode="MultiLine"
                                            Width="98%" TabIndex="5"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-8">
                            <label>(b) Give a maximum of three brief specific suggestion (s) point-wise for improvement on any of the above parameters, if any ?</label>
                            <asp:TextBox ID="txtSuggestionB" runat="server" MaxLength="200" TextMode="MultiLine"
                                            Width="98%" TabIndex="6"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4" style="display:none">
                            <label>(c) What you liked least about course ?</label>
                            <asp:TextBox ID="txtSuggestionC" runat="server" MaxLength="200" TextMode="MultiLine"
                                            Width="98%" TabIndex="7"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4" style="display:none">
                            <label>(d) What you think to improve the course ?</label>
                             <asp:TextBox ID="txtSuggestionD" runat="server" MaxLength="200" TextMode="MultiLine"
                                            Width="98%" TabIndex="8"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4" style="display:none">
                            <label>(e) Any other Remark</label>
                             <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TextMode="MultiLine" Width="98%" TabIndex="9">
                                        </asp:TextBox>
                        </div>

                    </div>
                           <%-- <table class="datatable" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td width="100%" colspan="3">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td width="44%">
                                    </td>
                                    <td width="1%">:
                                    </td>
                                    <td width="55%">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                            </table>--%>
            <div class="row text-center">
                  <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                ValidationGroup="Submit" CssClass="btn btn-primary" TabIndex="10"/>
                            &nbsp;
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" TabIndex="11"/>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
            </div>
                        <%--</td>
                    </tr>
                    <tr>
                        <td align="center" width="100%">
                          
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">--%>
                       <%--     
                        </td>
                    </tr>--%>
                </asp:Panel>
                <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                    <tr>
                        <td align="center" valign="middle">
                            <span style="font-size: large; color: Red;"><b>Teacher Not Allot!! You Cann't Give FeedBack!<br />
                                Please Contact Administrator! </b></span>
                        </td>
                    </tr>
                </asp:Panel>
            </div>
                        </div>
                    </div>
                 </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
