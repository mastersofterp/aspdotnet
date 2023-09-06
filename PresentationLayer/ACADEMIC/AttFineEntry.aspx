<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AttFineEntry.aspx.cs" Inherits="Academic_AttFineEntry" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>Attendnace Fine Entry</b></h3>
                            <div class="box-tools pull-right">
                                 <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                            </div>
                        </div>
                       
                            <div class="box-body">
                                <div class="form-group col-md-3">
                                    <label><span style="color:red;">*</span> Session</label> 
                                     <asp:DropDownList ID="ddlSession" runat="server" TabIndex="6" ToolTip="Please Select Session" CssClass="form-control" 
                                         OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSess" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" ValidationGroup="show" InitialValue="0"/>
                                    </div>
                                                              
                                 <div class="form-group col-md-3" >                                     
                                     <label><span style="color:red;">*</span> Scheme</label>
                                      <asp:DropDownList ID="ddlScheme" runat="server"  TabIndex="7" ToolTip="Please Select Scheme" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                      </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Scheme"
                                            ControlToValidate="ddlScheme" Display="None" ValidationGroup="show" InitialValue="0"/>
                                 </div>
                                 <div class="form-group col-md-3" >                                     
                                     <label><span style="color:red;">&nbsp;</span> Semester</label>
                                      <asp:DropDownList ID="ddlSem" runat="server"  TabIndex="7" ToolTip="Please Select Semester" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                      </asp:DropDownList>
                                   <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Semester"
                                            ControlToValidate="ddlSem" Display="None" ValidationGroup="show" InitialValue="0"/>--%>
                                 </div>
                                <div class="form-group col-md-3" >                                     
                                     <label><span style="color:red;">&nbsp;</span> Attendance Percentage</label>
                                      <asp:DropDownList ID="ddlAttPer" runat="server"  TabIndex="7" ToolTip="Please Select Attendance Percentage" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAttPer_SelectedIndexChanged">
                                          <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Attendance Percentage"
                                            ControlToValidate="ddlAttPer" Display="None" ValidationGroup="show" InitialValue="0"/>--%>
                                 </div>
                                <div class="col-md-12">
                                     <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                </div>
                            </div>
                        
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show Students" ValidationGroup="show"
                                    TabIndex="9" CssClass="btn btn-primary" OnClick="btnShow_Click"/>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="show"
                                    TabIndex="9" CssClass="btn btn-success" OnClick="btnSubmit_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="10" CssClass="btn btn-danger" OnClick="btnCancel_Click"/>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="show" />
                            </p>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlStud" runat="server"  Visible="false">
                                <asp:ListView ID="lvStudList" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-hover table-bordered table-striped" id="divStudlist" style="width:100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="text-align:center;"><asp:CheckBox ID="cbHead" runat="server" onclick="totAll(this)" ToolTip="Select/Select all"
                                                         OnCheckedChanged="cbHead_CheckedChanged" AutoPostBack="true"/>Select
                                                    </th>
                                                    <th>Enrollment No.
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <%--<th>CCODE
                                                    </th>--%>
                                                    <th>Overall Attendance Percentage
                                                    </th>
                                                    <th>Enter Fine Amount(Rs.)
                                                    </th>
                                                </tr>
                                                </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align:center;">
                                                <asp:CheckBox ID="chkStudent" OnCheckedChanged="chkStudent_CheckedChanged" runat="server"
                                                    ToolTip='<%# Eval("IDNO")%>'
                                                     AutoPostBack="true"/>
                                            </td>
                                            <td>
                                               <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblStudname" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                               
                                            </td>
                                            <%--<td>
                                                <%# Eval("CCODE")%>
                                            </td>--%>
                                            <td>
                                                <%# Eval("TH_PERCENTAGE")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFine" TabIndex="10" runat="server" CssClass="form-control" Text="" Enabled="false" MaxLength="10" />
                                            </td>
                                            
                                        </tr>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                        </tbody></table>
                                    </FooterTemplate>--%>
                                </asp:ListView>
                                    </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

   <script type="text/javascript">
       function totAll(headchk) {
           var frm = document.forms[0]
           for (i = 0; i < document.forms[0].elements.length; i++) {
               var e = frm.elements[i];
              
               if (e.type == 'checkbox') {
                   if (headchk.checked == true) {
                       e.checked = true;
                       
                   }
                   else {
                       e.checked = false;
                       
                   }
               }
           }
       }
    </script>

       <script>
           $(document).ready(function () {

               bindDataTable();
               Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
           });

           function bindDataTable() {
               var myDT = $('#divsessionlist').DataTable({
                scrollX: 'true'
               });
           }

        </script>

</asp:Content>
