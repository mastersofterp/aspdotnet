<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhdStudentList.aspx.cs" Inherits="Academic_REPORTS_PhdStudentList" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title"> PHD STUDENT LIST ALLOTED SUPERVISOR</h3>
                <div class="box-tools pull-right">
                </div>
            </div>
         
                <div class="box-body">
                     <fieldset>
                    <legend>Selection Criteria</legend>
                         <div class="col-md-4">
                             <label>Degree</label>
                             <asp:DropDownList ID="ddlDegree" runat="server" 
                                    AppendDataBoundItems="true" AutoPostBack="True" 
                                    onselectedindexchanged="ddlDegree_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                         </div>
                         <div class="col-md-4">
                             <label>Branch</label>
                              <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                         </div>
                         <div class="col-md-4">
                             <label>Year</label>
                             <asp:DropDownList ID="ddlYear" runat="server" 
                                    AppendDataBoundItems="true" TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                    Display="None" ErrorMessage="Please Select Year" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                         </div>

                     </fieldset>
                </div>
            <div class="box-footer">
                <p class="text-center">
                      <asp:Button ID="btnReport1" runat="server" Text="Report" ValidationGroup="report"
                                 onclick="btnReport1_Click" CssClass="btn btn-info"/>                            
                                <asp:Button ID="btnEnrollReport" runat="server" Text="Enrollment No. Report" 
                                    ValidationGroup="report" onclick="btnEnrollReport_Click"  CssClass="btn btn-info"/>                                  
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                    onclick="btnCancel_Click"  CssClass="btn btn-warning"/>
                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                </p>
            </div>
        </div>
    </div>
</div>






    <div id="divMsg" runat="server">
    </div>


   <%-- <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                PHD STUDENT LIST ALLOTED SUPERVISOR
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />--%>
         
   <%-- <table cellpadding="2" cellspacing="2" style="width: 100%">
        <tr>
            <td style="padding-top: 5px;width:100%" >
                <fieldset class="fieldset">
                    <legend class="legend">Selection Criteria</legend>
                    <table style="width: 100%" cellpadding="2" cellspacing="2" width="100%">
                                               
                        <tr>
                            <td style="width: 13%">
                                Degree :</td>
                            <td>
                                <asp:DropDownList ID="ddlDegree" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" AutoPostBack="True" 
                                    onselectedindexchanged="ddlDegree_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                   
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="width: 13%">
                                Branch :</td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" Width="30%" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                   
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="width: 13%">
                                Year :</td>
                            <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="20%" 
                                    AppendDataBoundItems="true" TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                    Display="None" ErrorMessage="Please Select Year" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                
                               </td>
                        </tr>
                       
                        <tr>
                       
                            <td style="width: 5%">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                               
                            </td>
                            <td>
                                <asp:Button ID="btnReport1" runat="server" Text="Report" ValidationGroup="report"
                                 onclick="btnReport1_Click" />
                                &nbsp;
                                <asp:Button ID="btnEnrollReport" runat="server" Text="Enrollment No. Report" 
                                    ValidationGroup="report" onclick="btnEnrollReport_Click"/>
                                    &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                    onclick="btnCancel_Click" />
                            </td>
                        </tr>
                       <tr>
                       
                            <td style="width: 5%">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                       
                            <td style="width: 5%">
                                &nbsp;</td>
                            <td style="color:RED">
                                &nbsp;</td>
                        </tr>
                        </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>--%>
</asp:Content>
