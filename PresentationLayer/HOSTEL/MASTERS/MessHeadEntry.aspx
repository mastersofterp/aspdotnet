<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MessHeadEntry.aspx.cs" Inherits="HOSTEL_MASTERS_MessHeadEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
</script>

<script type="text/javascript">
    //On UpdatePanel Refresh
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                $('#table2').dataTable();
            }
        });
    };

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
</script>


<%--<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Mess Expenditure Head Entry</h3>
                <div class="box-tools pull-right"></div>
            </div>

            <div >
              &nbsp;&nbsp;&nbsp;Edit Record
               <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.png" AlternateText="Edit Record" />
            </div>

            <div class="container justify-content-center">
                <asp:Panel ID="pnlList" runat="server">
                    <div class="col-12">
                        

                        <asp:Panel ID="pnlfees" runat="server" Width="50%">
                    <asp:ListView ID="lvMessHead" runat="server">
                        <LayoutTemplate>
                            <div class="vista-grid">
                                <div class="titlebar">
                                    Mess  Expenditure Heads Defination</div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">
                                        <th align="center">
                                            Head
                                        </th>
                                        <th align="center">
                                            Long Name
                                        </th>
                                        <th align="center">
                                            Short Name
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                <td align="left">
                                    <asp:Label ID="txtHead" runat="server" Text='<%#Eval("MESS_HEAD")%>' ToolTip='<%#Eval("MESS_HEAD_NO")%>'
                                        Width="10px" Font-Bold="true" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtLName" MaxLength="30" runat="server" Text='<%#Eval("MESS_LONGNAME")%>'
                                        Width="200px" />
                                    <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                        Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                        ID="valShortName" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtSName" MaxLength="8" runat="server" Text='<%#Eval("MESS_SHORTNAME")%>'
                                        Width="100px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFD2'">
                                <td align="left">
                                    <asp:Label ID="txtHead" runat="server" Text='<%#Eval("MESS_HEAD")%>' ToolTip='<%#Eval("MESS_HEAD_NO")%>'
                                        Width="10px" Font-Bold="true" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtLName" MaxLength="30" runat="server" Text='<%#Eval("MESS_LONGNAME")%>'
                                        Width="200px" />
                                    <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                        Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                        ID="CustomValidator1" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtSName" MaxLength="8" runat="server" Text='<%#Eval("MESS_SHORTNAME")%>'
                                        Width="100px" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </asp:Panel>
                    </div>
                </asp:Panel>
                
            </div>

            <div class="box-footer">
                <p class="text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Fees" CssClass="btn btn-primary"
                    Width="60px" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                    Width="60px" OnClick="btnCancel_Click" />
                &nbsp;<asp:Button ID="btnReport" runat="server" TabIndex="9" Text="Report"
                                            OnClick="btnReport_Click" Enabled="False" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Fees"
                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                </p>

      <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" />


            </div>
        </div>
    </div>
</div>--%>

    <asp:UpdatePanel ID="updMessHead" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="container  center-block">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                                        <h3 class="box-title"> MESS BILL HEAD ENTRY</h3>
                                        <div class="box-tools pull-right"></div>
                                    </div>
           
            <div class="box-footer">
                <div class="container center-block">
                <div class="col-md-8">
               <div class="panel panel-default center-block">
                    <div class="panel-body">
                    <div ID="pnlfees" runat="server" >    
                    <asp:ListView ID="lvMessHead" runat="server">
                        <LayoutTemplate>
                            <div class="vista-grid">                               
                               <fieldset><h4><legend>Mess Bill Heads Definition</legend></h4></fieldset>

                                <table id="tblMessHead" class="table table-hover table-bordered table-responsive " style="border:none;width: 100%">
                                    <thead>
                                    <tr class="bg-light-blue">
                                        <th>
                                            Head
                                        </th>
                                        <th>
                                            Long Name
                                        </th>
                                        <th>
                                            Short Name
                                        </th>
                                    </tr></thead>
                                    <tbody>
                                    <tr id="itemPlaceholder" runat="server" />
                               </tbody></table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="txtHead" runat="server" Text='<%#Eval("MESS_HEAD")%>' ToolTip='<%#Eval("MESS_HEAD_NO")%>'
                                         Font-Bold="true" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLName" TabIndex="1" MaxLength="30" Width="100%" CssClass="form-control" runat="server" Text='<%#Eval("MESS_LONGNAME")%>'
                                       />
                                    <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                        Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                        ID="valShortName" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSName" MaxLength="8" TabIndex="2" CssClass="form-control" Width="100%" runat="server" Text='<%#Eval("MESS_SHORTNAME")%>'
                                         />
                                </td>
                            </tr>
                        </ItemTemplate>                        
                    </asp:ListView>
                    </div>  
                        </div>
                   
                     <div class="panel-footer text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Fees" CssClass="btn btn-primary"
                    Width="60px" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                    Width="60px" OnClick="btnCancel_Click" />
                <%--&nbsp;<asp:Button ID="btnReport" runat="server" TabIndex="9" Text="Report"
                                            OnClick="btnReport_Click" Enabled="False" />--%>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Fees"
                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                </div>
                     <p>
                      <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />                      
                </p>
                <p>  <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" /></p>
                    <%--<asp:Panel ID="pnlfees" runat="server">--%>
                        
                <%--</asp:Panel>--%>

                </div>
               </div>
                    </div>
             </div>  
               
            <div id="div1" runat="server">
    </div>
        </div>
    </div>
            </div>
</div>
            </ContentTemplate>
         </asp:UpdatePanel>

    <div id="divMsg" runat="server">
        
    </div>
</asp:Content>
