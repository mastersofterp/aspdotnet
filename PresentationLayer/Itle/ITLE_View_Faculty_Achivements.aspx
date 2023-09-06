<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ITLE_View_Faculty_Achivements.aspx.cs" Inherits="Itle_ITLE_View_Faculty_Achivements"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>--%>
    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VIEW FACULTY ACHIEVEMENT</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlFaculty" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Faculty Achievement List</div>
                                <div class="panel panel-body">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>Session Term&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>Course&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divDesc" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="titlebar" style="color: Navy">
                                                        <b>Details About Faculty Achievement</b>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>Achievement Type&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6" id="tdSubject" runat="server">
                                                    <%# Eval("AWDTYPE")%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-group col-md-12">
                                                <div class="col-md-2">
                                                    <label>Description&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <CKEditor:CKEditorControl ID="ftbtxtDesc" runat="server" Height="200" BasePath="~/ckeditor">		                        
                                                    </CKEditor:CKEditorControl>
                                                    <%--<FTB:FreeTextBox ID="ftbtxtDesc" runat="server" Height="200px" Width="100%">
                                                    </FTB:FreeTextBox>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back"
                                                         OnClick="btnBack_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlAchievementlist" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvFAchivement" runat="server" DataKeyNames="FANO">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <h4 class="box-title">Faculty Achivement
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Achivement Type
                                                    </th>
                                                    <th>Achivement Date
                                                    </th>
                                                    <th>ATTACHMENT
                                                    </th>
                                                    <th>USERFULLNAME
                                                    </th>
                                                    <%--<th>Status</th>   --%>
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
                                            <asp:LinkButton ID="lnkPlan" runat="server" Text='<%#Eval("AWDTYPE")%>' CommandArgument='<%#Eval("FANO")%>'
                                                OnClick="lnkPlan_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("ACHIV_DATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>'
                                                ToolTip='<%# Eval("ATTACHMENT")%>' OnClick="lnkDownload_Click" CommandArgument='<%#Eval("FANO")%>'>
                                            </asp:LinkButton>
                                            <%-- <asp:HyperLink ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>' NavigateUrl='<%# GetFileName(Eval("ATTACHMENT"), Eval("FANO"))%>'
                                                Target="_blank">
                                            </asp:HyperLink>--%>
                                        </td>
                                        <td>
                                            <%# Eval("UA_FULLNAME")%>
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
    <%--</ContentTemplate>
 </asp:UpdatePanel>--%>
</asp:Content>
