<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AgendaApproval.aspx.cs" Inherits="MEETING_MANAGEMENT_TRANSACTION_AgendaApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">AGENDA APPROVAL</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12"> 
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Add/Edit Agenda Approval</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Committee :</label>
                                                     <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                 CssClass="form-control" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None" Text="*">                                        
                                            </asp:RequiredFieldValidator>
                                                    </div>


                                                 <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Meeting :</label>
                                                     <asp:DropDownList ID="ddlpremeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                         CssClass="form-control" ToolTip="Select Meeting" OnSelectedIndexChanged="ddlpremeeting_SelectedIndexChanged" TabIndex="2">
                                    </asp:DropDownList>
                                                </div>


                                                  <div  id="trDate" runat="server" visible="false" class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Meeting Date :</label>
                                                       <asp:Label ID="lblMDate" runat="server" Text=""></asp:Label>
                                                 </div>



                                                 <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Meeting Venue :</label>
                                                      <asp:Label ID="lblMVenue" runat="server" Text=""></asp:Label>
                                                 </div>




                                                 <div  id="trTime" runat="server" visible="false" class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Meeting Time :</label>
                                                     <asp:Label ID="lblMTime" runat="server" Text=""></asp:Label>
                                                 </div>
                                                 

                                                </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="3" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="4" />
                            &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Report" Visible="false"  CssClass="btn btn-info" ToolTip="Click to get Report" TabIndex="5" />
                             </p>              

                                  
                        <div class="col-md-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto"  Visible="false">
                                <asp:ListView ID="lvAgenda" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <h4 class="box-title"> MEETING AGENDA LIST
                                            </h4>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">

                                                    <th>Agenda Number       
                                                    </th>
                                                    <th>Agenda Title
                                                    </th>                                                  
                                                    <th>Remark
                                                    </th>

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
                                                <%# Eval("AGENDANO")%>
                                                <asp:HiddenField ID="hdnPK_AGENDA" runat="server" Value='<%# Eval("PK_AGENDA") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("AGENDATITAL")%>
                                            </td>                                        
                                            <td>
                                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" Width="300px" TabIndex="2" Text='<%# Eval("REMARK")%>'
                                                     Enabled='<%# (Eval("Lock").ToString() == "N" ? true : false) %>'></asp:TextBox>
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

           

           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

