<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MessBillHeadEntry.aspx.cs" Inherits="HOSTEL_MASTERS_MessBillHeadEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--         <div class="row">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Mess Bill Head Entry</h3>
                                        <div class="box-tools pull-right"></div>
                                    </div>

                                    <div class="container">
                                          <div class="box-body row">

                                        <div class="form-group col-md-12">
                                                <asp:Panel ID="pnlfees" runat="server" Width="50%">
                    <asp:ListView ID="lvMessHead" runat="server">
                        <LayoutTemplate>
                            <div class="vista-grid">
                                <div class="titlebar">
                                    Mess Bill Heads Defination</div>
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
                                    <asp:Label ID="txtHead" runat="server" Text='<%#Eval("MESS_BILL_HEAD")%>' ToolTip='<%#Eval("MESS_BILL_NO")%>'
                                        Width="10px" Font-Bold="true" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtLName" MaxLength="30" runat="server" Text='<%#Eval("MESS_BILL_LONGNAME")%>'
                                        Width="200px" />
                                    <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                        Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                        ID="valShortName" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtSName" MaxLength="8" runat="server" Text='<%#Eval("MESS_BILL_SHORTNAME")%>'
                                        Width="100px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFD2'">
                                <td align="left">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("MESS_BILL_HEAD")%>' ToolTip='<%#Eval("MESS_BILL_NO")%>'
                                        Width="10px" Font-Bold="true" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="TextBox1" MaxLength="30" runat="server" Text='<%#Eval("MESS_BILL_LONGNAME")%>'
                                        Width="200px" />
                                    <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                        Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                        ID="CustomValidator1" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="TextBox2" MaxLength="8" runat="server" Text='<%#Eval("MESS_BILL_SHORTNAME")%>'
                                        Width="100px" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </asp:Panel>
                                            </div>

                                          
                                        </div>
                                    </div>

                                   <div class="box-footer">
                                        <p class="text-center">
                               <asp:Button ID="Button1" runat="server" Text="Submit" ToolTip="Submit" CssClass="btn btn-primary" ValidationGroup="Fees"
                    Width="60px" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="Button2" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                    Width="60px" OnClick="btnCancel_Click" CssClass="btn btn-warning"/>
                &nbsp;<asp:Button ID="btnReport" runat="server" TabIndex="9" Text="Report"
                                            OnClick="btnReport_Click" Enabled="False" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Fees"
                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
            
                                        <p>

                <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" />
           </div>
                               </div>
                                 </div>
          </div>--%>
     <script type="text/javascript">
         //On Page Load
         $(document).ready(function () {
             $('#tblMessHead').DataTable({
                 scrollY: "250px",
                 scrollX: true,
                 paging: false,
                 scrollCollapse: true,
                 filter: false
             });
         });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#tblMessHead').dataTable({
                        scrollY: "250px",
                        scrollX: true,
                        paging: false,
                        scrollCollapse: true,
                        filter: false
                    });
                }
            });
        };
    </script>

     <div style="z-index: 1; position: absolute; top: 300px; left: 150px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMessHead"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-spinner fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
     </div>

    
     <asp:UpdatePanel ID="updMessHead" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="container  center-block">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                                        <h3 class="box-title">MESS BILL HEAD ENTRY</h3>
                                        <div class="box-tools pull-right"></div>
                                    </div>
            
            <div class="box-footer">
                <div class="container center-block">
                <div class="col-md-7">
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
                                    <asp:Label ID="txtHead" runat="server" Text='<%#Eval("MESS_BILL_HEAD")%>' ToolTip='<%#Eval("MESS_BILL_NO")%>'
                                         Font-Bold="true" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLName" TabIndex="1" MaxLength="30" Width="100%" CssClass="form-control" runat="server" Text='<%#Eval("MESS_BILL_LONGNAME")%>'
                                       />
                                    <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                        Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                        ID="valShortName" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSName" MaxLength="8" TabIndex="2" CssClass="form-control" Width="100%" runat="server" Text='<%#Eval("MESS_BILL_SHORTNAME")%>'
                                         />
                                </td>
                            </tr>
                        </ItemTemplate>                        
                    </asp:ListView>
                    </div>  
                        </div>
                   
                     <div class="panel-footer text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Fees"
                    OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="3"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4"/>
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
               
            <div id="div2" runat="server">
    </div>
        </div>
    </div>
            </div>
</div>
            </ContentTemplate>
         </asp:UpdatePanel>
    <div id="div1" runat="server">
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
