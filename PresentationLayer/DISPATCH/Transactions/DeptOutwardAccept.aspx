<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DeptOutwardAccept.aspx.cs" Inherits="DISPATCH_Transactions_DeptOutwardAccept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js"></script>
      <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
               <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DEPARTMENT OUTWARD ACCEPT/ REJECT</h3>
                        </div>
                                <div class="box-body"> 
                                    <div class="col-12" id="divListview" runat="server">
                                        <asp:Panel ID="Panel7" runat="server">
                                            <asp:ListView ID="IvOutwardDispatch" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                       <div class="sub-heading">
                                                      <h5>Department Outward List</h5>  
                                                    </div>
                                                    </h4>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                           <%-- <th>Edit
                                                            </th>--%>
                                                            <th>RFID Number
                                                            </th>
                                                            <th>Sender Name
                                                            </th>
                                                            <th>Department
                                                            </th>
                                                            <th>Subject
                                                            </th>
                                                            <th>Post Type
                                                            </th>
                                                            <th>Letter Category
                                                            </th>
                                                            <th>Select
                                                            </th>
                                                            <th>Accept/Reject
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

                                                        <%--<td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                CommandArgument='<%# Eval("IOTRANNO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click"
                                                                ToolTip="Edit Record" />
                                                        </td>--%>
                                                        <td>
                                                            <%# Eval("RFID_NUMBER")%>
                                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("IOTRANNO") %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("UA_FULLNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDEPT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBJECT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("POSTTYPENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LETTERCAT")%>                                                            
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkLetter" runat="server" ToolTip='<%# Eval("IOTRANNO") %>' />
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" CssClass="form-control">
                                                               <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                               <asp:ListItem Value="1">Accept</asp:ListItem>
                                                               <asp:ListItem Value="2">Reject</asp:ListItem>
                                                           </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div> 
                                                                      
                                          
                              

                                <div class="col-12 btn-footer" id="divSubmit" runat="server">
                                  
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="33" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" TabIndex="34" />
                                          
                                 </div>
                                </div>
                           </div>
                            
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


