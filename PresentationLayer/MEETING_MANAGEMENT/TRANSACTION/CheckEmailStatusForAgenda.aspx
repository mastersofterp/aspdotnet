<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CheckEmailStatusForAgenda.aspx.cs" Inherits="MEETING_MANAGEMENT_TRANSACTION_CheckEmailStatusForAgenda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">Agenda Email Received Confirmation</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12"> 
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Email Received Confirmation</div>
                                            <div class="panel panel-body">
                                           <%--   <div class="form-group col-md-4">
                                                   <label><span style="color: #FF0000"></span>Committee Type :</label>
                                                 <asp:RadioButtonList ID="rdbCommitteeType" runat="server" RepeatDirection="Horizontal" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="rdbCommitteeType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="U">University </asp:ListItem>
                                                <asp:ListItem Value="C">Institute</asp:ListItem>
                                               </asp:RadioButtonList>
                                              </div>--%>

                                                   <div id="trCollegeName" runat="server" visible="false" class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Institute Name  :</label>
                                                         <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="1"
                                          AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"></asp:DropDownList>                                   
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                             </div>


                                                 <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000"></span>Committee :</label>
                                                     <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        CssClass="form-control" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None"  Text="*"></asp:RequiredFieldValidator>
                                                     </div>
                                                

                                                      <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000"></span>Select Meeting :</label>
                                                           <asp:DropDownList ID="ddlpremeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                        CssClass="form-control" ToolTip="Select Meeting" TabIndex="2" OnSelectedIndexChanged="ddlpremeeting_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                    </asp:DropDownList>
                                                          </div>

                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                             <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"  CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="3" Visible="false" />                                 
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="4" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                         </p>

                                  
                        <div class="col-md-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvReceivedList" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <h4 class="box-title">Email Received Confirmation List
                                            </h4>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                    <th>
                                                              SrNo
                                                            </th> 
                                                            <th>
                                                               Member Name
                                                            </th>
                                                             <th>
                                                               Status
                                                            </th>
                                                             <th>
                                                               Received Date
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
                                                        <%# Eval("Row")%>
                                                    </td>                                                 
                                                    <td>
                                                        <%# Eval("MEMBER_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECEIVE_STATUS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECEIVED_DATE","{0:dd-MMM-yyyy}")%>
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

