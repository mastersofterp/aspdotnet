<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PayAdvance_Passing_Authority.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_PayAdvance_Passing_Authority" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

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
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ADVANCE PASSING AUTHORITY</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Add/Edit Advance Passing Authority</div>
                                                <div class="panel panel-body">
                                                    <asp:UpdatePanel ID="updAdd" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group col-md-12">
                                                                <div class="form-group col-md-4">
                                                                    <label><span style="color: #FF0000">*</span>College Name  :</label>
                                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College Name"
                                                                      OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"  AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true">
                                                                        <%----%>
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                                        Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <label><span style="color: #FF0000">*</span>Passing Authority :</label>
                                                                    <asp:TextBox ID="txtPAuthority" runat="server" MaxLength="50" CssClass="form-control"
                                                                        ToolTip="Enter Passing Name" TabIndex="2" />
                                                                    <asp:RequiredFieldValidator ID="rfvPAuthority" runat="server" ControlToValidate="txtPAuthority"
                                                                        Display="None" ErrorMessage="Please Enter Passing Authority" ValidationGroup="PAuthority"
                                                                        SetFocusOnError="True">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <label><span style="color: #FF0000">*</span>User :</label>
                                                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control" ToolTip="Select User"
                                                                        AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser"
                                                                        Display="None" ErrorMessage="Please Select User" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-md-12">
                                                                <div class="text-center text-bold">
                                                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" Text="Add New"
                                      CssClass="btn btn-primary" ToolTip="Click here to Add New Passing Authority" TabIndex="4" OnClick="btnAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                           
                            <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                 TabIndex="5" OnClick="btnShowReport_Click" Visible="false" />&nbsp;&nbsp;
                                
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAuthority" 
                                CssClass="btn btn-success" ToolTip="Click here to Submit" TabIndex="6" OnClick="btnSave_Click" />
                                    &nbsp;&nbsp;
                                    
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                CssClass="btn btn-danger" TabIndex="7" OnClick="btnCancel_Click" />&nbsp;&nbsp;
                                   
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false"
                                CssClass="btn btn-info" ToolTip="Click here to Return to Previous Menu" TabIndex="8" OnClick="btnBack_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAuthority"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <p></p>
                                    <p></p>                                  
                                    <p></p>                                   
                                    <p></p>                                   
                                    <p></p>                                   
                                    <p></p>                                   
                                    <p></p>                                   
                                    <p></p>                                   
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12 table-responsive">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:Repeater ID="lvPAuthority" runat="server">
                                            <HeaderTemplate>
                                                <h4 class="box-title">Advance Passing Authority List</h4>
                                                <table id="table2" class="table table-striped dt-responsive nowrap">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>College Name
                                                            </th>
                                                            <th>Passing Authority 
                                                            </th>
                                                            <th>User
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PANO") %>'
                                                          OnClick="btnEdit_Click"  AlternateText="Edit Record" ToolTip="Edit Record"  TabIndex="9" />&nbsp;
                                                        <%----%>
                                               <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COLLEGE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Passing_Authority_Name")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                </div>



                        </div>
                    </div>
                </div>
            </div>
     
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>

